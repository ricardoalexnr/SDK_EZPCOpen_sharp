using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Ricardo Rodrigues
/// ricardoanr@gmail.com
/// 2023
/// based on original c++ files
/// </summary>
/// 


namespace SDK_Ezviz
{
    public delegate void AtividadeEventHandler(string mensagem);

    
   public class SDK
    {
        public event AtividadeEventHandler Atividade;
        public string m_sessionId;

        public SDK()
            {
            
            }


        public bool Iniciar(string szAuthAddr, string szPlatform, string szAppId, string szAccessToken)
        {
            bool _resultado = false;
            Atividade?.Invoke("Start SDK");
            if (OpenNetStream.OpenSDK_InitLib( szAuthAddr,  szPlatform,  szAppId) == 0)
            {
                Atividade?.Invoke("SDK success!");
                OpenNetStream.OpenSDK_SetMessageCallback(my_MessageHandler);
                if (OpenNetStream.OpenSDK_SetAccessToken(szAccessToken) == 0)
                {
                    Atividade?.Invoke("Set Token, ok!");
                    //SetConfigInfo();
                    _resultado = true;   
                }

            }
            return _resultado;
        }

      public void my_MessageHandler(string szSessionId, uint iMsgType, uint iErrorCode, string pMessageInfo, IntPtr pUser)
        {
            Atividade?.Invoke("pMessageInfo:" + pMessageInfo);           
        }


        public void SetConfigInfo()
        {
            OpenNetStream.OpenSDK_SetConfigInfo(OpenNetStream.ConfigKey.CONFIG_OPEN_STREAMTRANS, 1);
        }

        public void AllocSessionEx()
        {
            IntPtr pSession;
            int iSessionLen = 0;
            IntPtr pUser = new IntPtr();
            int _i = OpenNetStream.OpenSDK_AllocSessionEx(my_MessageHandler, pUser, out pSession, out iSessionLen);

            if (_i == 0)
            {
                Atividade?.Invoke("Session, ok!");
                m_sessionId = Marshal.PtrToStringAnsi(pSession, iSessionLen);  
                OpenNetStream.OpenSDK_Data_Free(pSession);              

            }
            else
            {
                Atividade?.Invoke("failure session!");
            }


        }

        
        public int startRealPlay(string session, IntPtr hPlayWnd, string devSerial, int iChannelNo, string safekey)
        {
            return OpenNetStream.OpenSDK_StartRealPlayEx(session, hPlayWnd, devSerial, iChannelNo, safekey);
        }


        public int SetVideoLevel(string szDevSerial, int iChannelNo, int iVideoLevel)
        {
            // Vedio quality, enter a number ranging from 0 to 2 
            return OpenNetStream.OpenSDK_SetVideoLevel(szDevSerial, iChannelNo, iVideoLevel);
        }



        public int setDataCallBack(string szSessionId, OpenNetStream.OpenSDK_DataCallBack pDataCallBack, IntPtr pUser)
        {
            return OpenNetStream.OpenSDK_SetDataCallBack(szSessionId, pDataCallBack, pUser);
        }

        public int DecryptPicture(string szAccessToken, string szPicURL, string szSerail, string szSafeKey, out IntPtr pPicBuf, out int iPicLen)
        {
            return OpenNetStream.OpenSDK_DecryptPicture(szAccessToken, szPicURL, szSerail, szSafeKey, out pPicBuf, out iPicLen);
        }


        public void saveImagem(IntPtr pPicBuf, int iPicLen, string FilePath)
        {            
            FileStream stm = File.Create(FilePath); 
            byte[] data = new byte[iPicLen]; 
            Marshal.Copy(pPicBuf, data,0, iPicLen); 
            stm.Write(data, 0, data.Length);         
            stm.Close();

        }

        public Image CriarImagem(IntPtr pPicBuf, int iPicLen)
        {
            byte[] data = new byte[iPicLen];
            Marshal.Copy(pPicBuf, data, 0, iPicLen);

            MemoryStream ms = new MemoryStream(data, 0, data.Length);
            ms.Write(data, 0, data.Length);
            Image newImage = Image.FromStream(ms, true);
            return newImage;
        }




    }
}
