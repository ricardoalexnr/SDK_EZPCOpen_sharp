
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace SDK_Ezviz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           _SDK = new SDK();
        }

        string m_sessionId;
        string areaID;

        public class DeviceTableViewInfo
        {
            public String strSubserial;
            public int iChannelNo;
            public bool bEncrypt;
            public int iVideoLevel;
        }

        private SDK _SDK_;
        public SDK _SDK
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SDK_;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SDK_ != null)
                {
                    _SDK_.Atividade -= SDK_Atividade;                   
                }
                _SDK_ = value;

                if (_SDK_ != null)
                {
                    _SDK_.Atividade += SDK_Atividade;
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (_SDK.Iniciar("url AuthAddr", "url Platform", "AppId", "token") == true)
                
            {
                _SDK.AllocSessionEx(); 
                
            }
        }
        


        private void SDK_Atividade(string mensagem)
        {
            if (textBox1.InvokeRequired)
            {
                this.Invoke(new Action(() => { textBox1.AppendText(mensagem + Environment.NewLine); }));
            }else
                textBox1.AppendText(mensagem + Environment.NewLine);     
            
        }
        

        void my_DataCallBack(OpenNetStream.DataType enType, string pData, uint iLen, IntPtr pUser)
        {
            textBox1.AppendText("my_DataCallBack: enType " + enType.ToString() + Environment.NewLine);
            textBox1.AppendText("pData:" + pData + Environment.NewLine + " iLen:" + iLen + Environment.NewLine + " pUser:" + pUser.ToString() +  Environment.NewLine);
            
        }
        
        

        public int startRealPlay(int videoLevel)
        {
            try
            {
                bool bEncrypt = false;
                String devSerial = "AAAAAAAAA"; //Serial Device
                int iChannelNo = 1; //Chanel nun
                String safekey = ""; //CODE CRYPT

                IntPtr pUser = new IntPtr();
               
                _SDK.setDataCallBack(m_sessionId, my_DataCallBack, pUser);             


                int iRet = _SDK.startRealPlay(_SDK.m_sessionId, pictureBox1.Handle, devSerial, iChannelNo, safekey);
                if (0 != iRet)
                {
                    textBox1.AppendText("Start Player, success" + Environment.NewLine);
                    return -1;
                }

                textBox1.AppendText("Start Player, failure " + Environment.NewLine);
                return 0;
            }
            catch (Exception e)
            {
                textBox1.AppendText("error: " + e.Message + Environment.NewLine);

                return -1;

            }

            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            startRealPlay(0);
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //no need to start/locate the session

            IntPtr _img;
            int _len;
           int  _i = _SDK.DecryptPicture("token acesses", "pic url", "serial device", "crypt key", out _img, out _len);


            if (_i == 0)
            {
                textBox1.AppendText("Image, success!" + Environment.NewLine);
                textBox1.AppendText("Buffer: " + _len + Environment.NewLine);

                //_SDK.saveImagem(_img, _len, @"...\frame_c.jpg");

                //or

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = _SDK.CriarImagem(_img, _len);



            } else
            {
                textBox1.AppendText("image, failure!" + Environment.NewLine);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_SDK.SetVideoLevel("serial device", 6, 1) == 0)
            {
                textBox1.AppendText("Set Video Level, ok" + Environment.NewLine);
            }
        }
    }
}
