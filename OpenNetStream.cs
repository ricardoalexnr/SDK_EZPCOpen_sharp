using System;
using System.Runtime.InteropServices;

/// <summary>
/// Ricardo Rodrigues
/// ricardoanr@gmail.com
/// 2023
/// based on original c++ files
/// </summary>
/// 
namespace SDK_Ezviz
{    
   public class OpenNetStream
    {

        public OpenNetStream()
        {

        }


        const string pathDLL = @"\win64\OpenNetStream.dll";

        #region Define

        public enum MessageType
        {
            INS_PLAY_EXCEPTION = 0, ///< 【0】A reprodução é anormal, geralmente causada por desconexão do dispositivo ou anormalidade na rede
            INS_PLAY_RECONNECT = 1, ///< 【1】Reconectar, ele se reconectará automaticamente internamente durante a transmissão em tempo real
            INS_PLAY_RECONNECT_EXCEPTION = 2, ///< 【2】Exceção de reconexão
            INS_PLAY_START = 3, ///< 【3】jogo começa
            INS_PLAY_STOP = 4, ///< 【4】Jogo encerrado
            INS_PLAY_ARCHIVE_END = 5, ///< 【5】A reprodução acabou, haverá esta mensagem quando a reprodução terminar
            INS_DOWNLOAD_START = 13, ///< 【13】Início do download
            INS_DOWNLOAD_STOP = 14, ///< 【14】Download termina
            INS_DOWNLOAD_EXCEPTION = 15, ///< 【15】Exceção de download, como tempo limite de recebimento, erro de dados e falha na gravação de arquivo
            INS_VOICETALK_START = 16, ///< 【16】Início do intercomunicador de voz
            INS_VOICETALK_STOP = 17, ///< 【17】Parada do intercomunicador de voz
            INS_VOICETALK_EXCEPTION = 18, ///< 【18】Exceção de intercomunicador de voz
            INS_PTZ_EXCEPTION = 19, ///< 【19】Exceção de controle PTZ
            INS_RECORD_FILE = 20, ///< 【20】arquivo de registro para consulta (resultado da pesquisa de registro)
            INS_RECORD_SEARCH_END = 21, ///< 【21】Fim da pesquisa de vídeo (temporariamente não usado)
            INS_RECORD_SEARCH_FAILED = 22, ///< 【22】Falha na pesquisa de registro
            INS_DEFENSE_SUCCESS = 23, ///< 【23】armado com sucesso
            INS_DEFENSE_FAILED = 24, ///< 【24】armamento falhou
            INS_PLAY_ARCHIVE_EXCEPTION = 28, ///< 【28】A reprodução terminou de forma anormal, pode ser que o tempo de recebimento dos dados tenha expirado
            INS_PTZCTRL_SUCCESS = 46, ///< 【46】Comando de controle PTZ enviado com sucesso
            INS_PTZCTRL_FAILED = 47, ///< 【47】Falha no controle PTZ

            INS_PLAYREAL_STAT = 91, ///< 【91】Visualizar as estatísticas do stream
            //INS_PLAYBACK_STAT             = 92,	  ///< 【92】Estatísticas de streaming de reprodução
            //INS_PRE_CONNECTION_STAT       = 97,     ///< 【97】estatísticas de pré-conexão
            //INS_PLAY_SWITCH_STAT		  = 99,	      ///< 【99】Alterne para obter estatísticas de transmissão	
            //INS_NAT_INFO_REPORT		  = 101,      ///< 【101】Relatório de informações do tipo NAT

            INS_USER_STOP_DOWNLOAD = 108, ///< 【108】Interromper o download ativamente
            INS_VTDU_STREAM_EXCEPTION = 115 ///< 【115】O link entre o dispositivo e o serviço de streaming é desconectado de forma anormal durante o processo de streaming
        }

        /** \struct STREAM_TIME
         *  \brief  开放SDK获取OSD时间格式定义
         */
        public class _STREAM_TIME
        {
            public uint iYear; ///< 年
            public uint iMonth; ///< 月
            public uint iDay; ///< 日
            public uint iHour; ///< 时
            public uint iMinute; ///< 分
            public uint iSecond; ///< 秒
        }

        /** \struct NetStreamCallbackMessage
         *  \brief 经过NetStream处理所反馈的信息                                                                    
         */
        public class _NetStreamCallBackMessage
        {
            public _NetStreamCallBackMessage()
            {
                this.iErrorCode = 0;
                this.pMessageInfo = null;
            }
            public int iErrorCode; ///< 消息回调反馈的错误码
            public string pMessageInfo; ///< 消息回调反馈的信息
        }

        /** \enum  AlarmType 
         *  \brief 开放SDK告警类型定义
         */
        public enum AlarmType
        {
            ALARM_TYPE_ALL = -1, ///< ALL
            BODY_SENSOR_EVENT = 10000, ///< human sensor event
            EMERGENCY_BUTTON_EVENT = 10001, ///< emergency remote button event
            MOTION_DETECT_ALARM = 10002, ///< motion detection alarm
            BABY_CRY_ALARM = 10003, ///< baby crying alarm
            MAGNETIC_ALARM = 10004, ///< door magnetic alarm
            SMOKE_DETECTOR_ALARM = 10005, ///< smoke alarm
            COMBUSTIBLE_GAS_ALARM = 10006, ///< combustible gas alarm
            FLOOD_IN_ALARM = 10008, ///< flood alarm
            EMERGENCY_BUTTON_ALARM = 10009, ///< emergency button alarm
            BODY_SENSOR_ALARM = 10010, ///< human sensor alarm
            SHELTER_ALARM = 10011, ///< Shelter alarm
            VIDEO_LOSS_ALARM = 10012, ///< video loss
            LINE_DETECTION_ALARM = 10013, ///< cross-border detection
            FIELD_DETECTION_ALARM = 10014, ///< field intrusion
            FACE_DETECTION_ALARM = 10015, ///< face detection event
            DOOR_BELL_ALARM = 10016, ///< smart doorbell alarm
            DEVOFFLINE_ALARM = 10017, ///< Camera lost association alarm
            CURTAIN_ALARM = 10018, ///< curtain alarm
            MOVE_MAGNETOMETER_ALARM = 10019, ///< single door magnetic alarm
            SCENE_CHANGE_DETECTION_ALARM = 10020, ///< scene change detection
            DEFOCUS_ALARM = 10021, ///< defocus detection
            AUDIO_EXCEPTION_ALARM = 10022, ///< audio exception detection
            LEFT_DETECTION_ALARM = 10023, ///< item left detection
            TAKE_DETECTION_ALARM = 10024, ///< item detection
            PARKING_DETECTION_ALARM = 10025, ///< illegal parking detection
            HIGH_DENSITY_DETECTION_ALARM = 10026, ///< People gathering detection
            LOITER_DETECTION_ALARM = 10027, ///< loitering detection detection
            RUN_DETECTION_ALARM = 10028, ///< fast motion detection
            ENTER_AREA_DETECTION_ALARM = 10029, ///< enter area detection
            EXIT_AREA_DETECTION_ALARM = 10030, ///< leave area detection
            MAG_GIM_ALARM = 10031, ///< magnetic interference alarm
            UNDER_VOLTAGE_ALARM = 10032, ///< Battery undervoltage alarm
            INTRUSION_ALARM = 10033, ///< Intrusion alarm
            IO_00_ALARM = 10100, ///< IOalarm
            IO_01_ALARM = 10101, ///< IO-1alarm
            IO_02_ALARM = 10102, ///< IO-2alarm
            IO_03_ALARM = 10103, ///< IO-3alarm
            IO_04_ALARM = 10104, ///< IO-4alarm
            IO_05_ALARM = 10105, ///< IO-5alarm
            IO_06_ALARM = 10106, ///< IO-6alarm
            IO_07_ALARM = 10107, ///< IO-7alarm
            IO_08_ALARM = 10108, ///< IO-8alarm
            IO_09_ALARM = 10109, ///< IO-9alarm
            IO_10_ALARM = 10110, ///< IO-10alarm
            IO_11_ALARM = 10111, ///< IO-11alarm
            IO_12_ALARM = 10112, ///< IO-12alarm
            IO_13_ALARM = 10113, ///< IO-13alarm
            IO_14_ALARM = 10114, ///< IO-14alarm
            IO_15_ALARM = 10115, ///< IO-15alarm
            IO_16_ALARM = 10116 ///< IO-16alarm
        }

        /** \enum  AlarmNotifyType
         *  \brief 开放SDK告警通知类型定义
         */
        public enum _AlarmNotifyType
        {
            ALARM_NOTIFY_CONNECT_EXCEPTION = 100, ///< SDK同萤石平台连接异常
            ALARM_NOTIFY_RECONNECT_SUCCESS, ///< 重连成功
            ALARM_NOTIFY_RECONNECT_FAILED ///< 重连失败
        }

        /** \enum PTZCommand
         *  \brief 云台控制命令
         */
        public enum PTZCommand
        {
            UP = 0, ///< 上
            DOWN, ///< 下
            LEFT, ///< 左
            RIGHT, ///< 右
            UPLEFT, ///< 上左
            DOWNLEFT, ///< 下左
            UPRIGHT, ///< 上右
            DOWNRIGHT, ///< 下右
            ZOOMIN,
            ZOOMOUT,
            FOCUSNEAR,
            FOCUSFAR,
            IRISSTARTUP,
            IRISSTOPDOWN,
            LIGHT,
            WIPER,
            AUTO
        }

        /** \enum PTZAction
         *  \brief 云台操作命令
         *  一般情况下，鼠标按下代表开始，鼠标松开代表停止
         */
        public enum PTZAction
        {
            START = 0, ///< 开始
            STOP ///< 停止
        }

        /** \enum  DefenceType 
         *  \brief 布撤防告警类型
         */
        public enum DefenceType
        {
            PIR = 0, ///< 红外
            ATHOME, ///< 在家，A1设备
            OUTDOOR, ///< 外出
            BABYCRY, ///< 婴儿啼哭
            MOTIONDETECT, ///< 移动侦测
            GLOBAL ///< 全部
        }

        /** \enum  DefenceStatus 
         *  \brief 布撤防状态
         */
        public enum DefenceStatus
        {
            UN_DEFENCE = 0, ///< 撤防
            DEFENCE, ///< 布防
            UN_SUPPORT, ///< 不支持
            FORCE_DEFENCE ///< 强制布防，A1设备
        }

        public enum DefenceActor
        {
            D = 0, ///< 设备
            V, ///< 视频通道
            I ///< IO通道
        }

        /** \enum  DataType 
         *  \brief 音视频流数据类型
         */
        public enum DataType
        {
            NET_DVR_SYSHEAD = 1, ///< 流头
            NET_DVR_STREAMDATA = 2, ///< 流数据
            NET_DVR_RECV_END = 3, ///< 结束标记
            NET_DVR_RECV_201 = 4, ///< (云存储倍速回放)服务器返回的快放模式切换标记，当收到此标记时，快放模式从全帧快放切换成抽帧快放(网络质量不足以满足全帧快放时)
            NET_DVR_RECV_202 = 5 ///< (服务器返回的降低快放倍速消息,云存储服务器发来这个数据后开始降低快放倍速(云存储倍速回放是由云存储服务对码流做了倍速效果的处理,客户端直接播放码流即可，不需自己实现倍速播放)
        }

        /** \enum  LoginParamKey 
         *  \brief 登录相关参数类型
         */
        public enum LoginParamKey
        {
            LOGIN_AREAID = 1, ///< 区域号, 目前只有海外使用
            LOGIN_VER = 2, ///< 登录页面版本信息
            LOGIN_AREADOMAIN = 3, ///< 登录后,重定向的域名
            LOGIN_TOKEN_EXPIRETIME = 4, ///< 登录后,Token失效时间
            LOGIN_ACCESS_TOKEN = 5 ///< 登录后,获取AccessToken
        }

        /** \enum  ConfigKey 
         *  \brief 配置类型
         */
        public enum ConfigKey
        {
            CONFIG_DATA_UTF8 = 1, ///< 数据输出使用UTF8
            CONFIG_OPEN_STREAMTRANS = 2, ///< 码流数据输出经过转封装处理, 用于录像存储
            CONFIG_CLOSE_P2P = 3, ///< P2P开关, 用于关闭P2P
            CONFIG_LOG_LEVEL = 4, ///< 配置日志等级,参见 #OpenSDK_LogLevel
            CONFIG_P2P_MAXNUM = 6, ///< 设置预链接支持的最大路数
            CONFIG_CLOSE_REPORT = 9, ///< 关闭取流上报, value = 1;
            CONFIG_TALK_CHANNEL = 10, ///< 开启通道对讲, 1:开启， 0：关闭
            CONFIG_FAST_STREAM = 13, ///< 快速出流开关 0: 关闭   1: 打开
            CONFIG_CLOSE_BLONG_SERIAL = 14, ///< 是否关联设备 0: 关联   1: 不关联
            CONFIG_ClOSE_KEY_AUTH = 15 ///< 关闭密钥验证 0: 不关闭 1: 关闭
        }

        public class ST_EZ_RECORD_INFO
        {
            public string szStartTime = new string(new char[32]); ///< 录像开始时间
            public string szStopTime = new string(new char[32]); ///< 录像结束时间
            public int iRecType; ///< 录像来源,  1:本地录像.2:云录像
            public string szServerIp = new string(new char[64]); ///< 服务Ip地址 云录像时使用
            public int iServerPort; ///< 服务Ip地址 云录像时使用
            public int iFileType; ///< 类型  云录像时使用
            public int iVideoType; ///< 录像类型  云录像时使用
            public int istorageVersion; ///< 云录像版本 云录像时使用
        }


        /** \enum  OpenSDK_LogLevel 
         *  \brief 日志级别
         */
        public enum EZOPENSDK_LogLevel
        {
            EZOPENSDK_ALL = 0, ///< 【0】打印全部日志
            EZOPENSDK_TRACE, ///< 【1】
            EZOPENSDK_DEBUG, ///< 【2】调试日志
            EZOPENSDK_INFO, ///< 【3】基本信息级别
            EZOPENSDK_WARN, ///< 【4】警告日志
            EZOPENSDK_ERROR, ///< 【5】基本错误级别
            EZOPENSDK_OFF = 7 ///< 【7】无日志级别

        }

        /** \enum  EZOPENSDK_AUDIO_CODE 
         *  \brief 定义语音编码类型
         */
        public enum EZOPENSDK_AUDIO_CODE
        {
            EZOPENSDK_AUDIO_CODE_G722_1 = 0, ///< G722_1
            EZOPENSDK_AUDIO_CODE_G711_MU = 1, ///< G711_MU
            EZOPENSDK_AUDIO_CODE_G711_A = 2, ///< G711_A
            EZOPENSDK_AUDIO_CODE_G723 = 3, ///< G723
            EZOPENSDK_AUDIO_CODE_MP1L2 = 4, ///< MP1L2
            EZOPENSDK_AUDIO_CODE_MP2L2 = 5, ///< MP2L2
            EZOPENSDK_AUDIO_CODE_G726 = 6, ///< G726
            EZOPENSDK_AUDIO_CODE_AAC = 7, ///< AAC
            EZOPENSDK_AUDIO_CODE_G726_A = 8, ///< G726
            EZOPENSDK_AUDIO_CODE_G726_16 = 9, ///< G726_16
            EZOPENSDK_AUDIO_CODE_G729 = 10, ///< G729
            EZOPENSDK_AUDIO_CODE_ADPCM = 11, ///< ADPCM
            EZOPENSDK_AUDIO_CODE_AMR_NB = 12, ///< AMR_NB
            EZOPENSDK_AUDIO_CODE_RAW_DATA8 = 13, ///< 采样率为8k的原始数据
            EZOPENSDK_AUDIO_CODE_RAW_UDATA16 = 14, ///< 采样率16K的原始数据,即L16
            EZOPENSDK_AUDIO_CODE_MP2L2_32 = 15, ///<
            EZOPENSDK_AUDIO_CODE_MP2L2_64 = 16, ///<
            EZOPENSDK_AUDIO_CODE_AAC_32 = 17, ///<
            EZOPENSDK_AUDIO_CODE_AAC_64 = 18, ///<
            EZOPENSDK_AUDIO_CODE_OPUS_8 = 19, ///< OPUS 8K
            EZOPENSDK_AUDIO_CODE_OPUS_16 = 20, ///< OPUS 16K
            EZOPENSDK_AUDIO_CODE_OPUS_48 = 21, ///< OPUS 48K
            EZOPENSDK_AUDIO_CODE_G729_A = 22, ///< G729_A
            EZOPENSDK_AUDIO_CODE_G729_B = 23, ///< G729_B
            EZOPENSDK_AUDIO_CODE_PCM = 24, ///< PCM
            EZOPENSDK_AUDIO_CODE_MP3 = 25, ///< MP3
            EZOPENSDK_AUDIO_CODE_AC3 = 26, ///< AC3
            EZOPENSDK_AUDIO_CODE_RAW = 99 ///< RAW
        }

        /** \enum  EZOPENSDK_RECORD_TYPE  
         *  \brief 定义录像文件类型
         */
        public enum EZOPENSDK_RECORD_TYPE
        {
            EZOPENSDK_RECORD_AUTO = 0, ///< 【0】设备和本地同时查询
            EZOPENSDK_RECORD_CLOUD = 1, ///< 【1】查询云录像
            EZOPENSDK_RECORD_DEVICE = 2 ///< 【2】查询设备本地录像
        }

        /** \enum  EZOPENSDK_PLAYBACK_SPEED_TYPE  
         *  \brief 定义倍速回放速度类型
         */
        public enum EZOPENSDK_PLAYBACK_SPEED_TYPE
        {
            EZOPENSDK_PLAYBACK_SPEED_NONE = 1, ///< 【1】正常速率
            EZOPENSDK_PLAYBACK_SPEED_X2, ///< 【2】2倍速
            EZOPENSDK_PLAYBACK_SPEED_S2, ///< 【3】1/2倍速
            EZOPENSDK_PLAYBACK_SPEED_X4, ///< 【4】4倍速
            EZOPENSDK_PLAYBACK_SPEED_S4, ///< 【5】1/4倍速
            EZOPENSDK_PLAYBACK_SPEED_X8, ///< 【6】8倍速
            EZOPENSDK_PLAYBACK_SPEED_S8, ///< 【7】1/8倍速
            EZOPENSDK_PLAYBACK_SPEED_X16, ///< 【8】16倍速
            EZOPENSDK_PLAYBACK_SPEED_S16 ///< 【9】1/16倍速
        }

        #endregion


        #region Interface


        public delegate void OpenSDK_MessageHandler(string szSessionId, uint iMsgType, uint iErrorCode, string pMessageInfo, IntPtr pUser);

        [DllImport(pathDLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenSDK_InitLib(string szAuthAddr, string szPlatform, string szAppId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Init(string szAppId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_FiniLib();


        [DllImport(pathDLL)]
        public static extern void OpenSDK_SetConfigInfo(ConfigKey iKey, int iValue);


        [DllImport(pathDLL)]
        public static extern void OpenSDK_SetMessageCallback(OpenSDK_MessageHandler pHandle);


        [DllImport(pathDLL)]
        public static extern void OpenSDK_SetAuthAddr(string szAuthAddr);

        [DllImport(pathDLL)]
        public static extern void OpenSDK_SetPlatformAddr(string szPlatform);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetAppID(string szAppId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetAccessToken(string szAccessToken);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetClientVer(string szClientVer);


        [DllImport(pathDLL, CallingConvention = CallingConvention.Cdecl)] //StdCall Cdecl
        public static extern int OpenSDK_AllocSessionEx(OpenSDK_MessageHandler pHandle, IntPtr pUser, out IntPtr pSession, out int iSessionLen);




        [DllImport(pathDLL)]
        public static extern int OpenSDK_FreeSession(string szSessionId);


        [DllImport(pathDLL)]
        public static extern void OpenSDK_SetSessionConfig(string szSessionId, ConfigKey iKey, int iValue);


        public delegate void OpenSDK_DataCallBack(DataType enType, string pData, uint iLen, IntPtr pUser);


        [DllImport(pathDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenSDK_SetDataCallBack(string szSessionId, OpenSDK_DataCallBack pDataCallBack, IntPtr pUser);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetVideoLevel(string szDevSerial, int iChannelNo, int iVideoLevel);


        [DllImport(pathDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenSDK_StartRealPlayEx(string szSessionId, IntPtr hPlayWnd, string szDevSerial, int iChannelNo, string szSafeKey);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartRealPlayExtend(string szSessionId, IntPtr hPlayWnd, string szDevSerial, string szSuperSerial, int iChannelNo, string szSafeKey);



        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopRealPlayEx(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartSearchExtend(string szSessionId, string szDevSerial, int iChannelNo, string szStartTime, string szStopTime, int iSearchType);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartSearchCouldExtend(string szSessionId, string szDevSerial, int iChannelNo, string szStartTime, string szStopTime);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartPlayBackEx(string szSessionId, IntPtr hPlayWnd, string szDevSerial, int iChannelNo, string szSafeKey, string szStartTime, string szStopTime);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartPlayBackExtend(string szSessionId, IntPtr hPlayWnd, string szDevSerial, string szSuperSerial, int iChannelNo, string szSafeKey, string szStartTime, string szStopTime);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartPlayBackNoCheck(string szSessionId, IntPtr hPlayWnd, string szDevSerial, string szSuperSerial, int iChannelNo, string szSafeKey, ST_EZ_RECORD_INFO stRecInfo);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_PlayBackResume(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_PlayBackPause(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetPlayBackScale(string szSessionId, int iScale, string szOsdTime = null, int mode = 0);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopPlayBackEx(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartDownloadCloudFile(string szSessionId, string szDevSerial, int iChannelNo, string szSafeKey, string szStartTime, string szStopTime);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopDownloadCloudFile(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartDownload(string szSessionId, string szDevSerial, int iChannelNo, string szSafeKey, string szStartTime, string szStopTime);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopDownload(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_GetP2PDeviceInfo(string szAccessToken, string szDeviceSerial, IntPtr pBuf, ref int iLength);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartP2PDownload(string szSessionId, string szDevSerial, int iChannelNo, string szSafeKey, string szStartTime, string szStopTime);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopP2PDownload(string szSessionId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartDownloadEx(string szSessionId, string szDevSerial, int iChannelNo, string szSafeKey, string szStartTime, string szStopTime);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopDownloadEx(string szSessionId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_GetOSDTime(string szSessionId, _STREAM_TIME pTime);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_OpenSound(string szSessionId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_CloseSound(string szSessionId);


        [DllImport(pathDLL)]
        public static extern ushort OpenSDK_GetVolume(string szSessionId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetVolume(string szSessionId, ushort uVolume);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_GetVoiceTalkCapture(string szSessionId, IntPtr pBuf, ref int iLength);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SetVoiceTalkCapture(string szSessionId, int iCapture);



        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartVoiceTalkEx(string szSessionId, string szDevSerial, int iChannelNo);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopVoiceTalk(string szSessionId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_StartAudioTransport(string szSessionId, string szDevSerial, int iChannelNo, ref int pAudioCodeType);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_StopAudioTransport(string szSessionId);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_SendAudioData(string szSessionId, object pAudioBuf, uint iDataSize);



        [DllImport(pathDLL)]
        public static extern void OpenSDK_SetLoginParams(LoginParamKey iKey, string szValue);



        [DllImport(pathDLL)]
        public static extern string OpenSDK_GetLoginResponseParams(LoginParamKey iKey);


        [DllImport(pathDLL)]
        public static extern void OpenSDK_Logout();



        [DllImport(pathDLL)]
        public static extern int OpenSDK_Mid_Login(IntPtr szAccessToken, ref int iAccessTokenLen);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_GetDevListEx(int iPageStart, int iPageSize, IntPtr pBuf, ref int iLength);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_GetSharedDevList(int iPageStart, int iPageSize, IntPtr pBuf, ref int iLength);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_GetDevDetailInfo(string szDevSerial, int iChannelNo, bool bUpdate, IntPtr pDevDetailInfo, ref int iLength);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_GetDeviceInfo(string szAccessToken, string szDeviceSerial, IntPtr pBuf, ref int iLength);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_GetAlarmListEx(string szDevSerial, int iChannelNo, string szStartTime, string szEndTime, AlarmType iAlarmType, int iStatus, int iPageStart, int iPageSize, IntPtr pBuf, ref int iLength);


        [DllImport(pathDLL, CallingConvention = CallingConvention.Cdecl)] //StdCall Cdecl
        public static extern int OpenSDK_DecryptPicture(string szAccessToken, string szPicURL, string szSerail, string szSafeKey,out IntPtr pPicBuf, out int iPicLen);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_SetAlarmRead(string szAccessToken, string szAlarmId);

        [DllImport(pathDLL)]
        public static extern int OpenSDK_Data_Free(IntPtr pBuf);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_HttpSendWithWait(string szUri, string szHeaderParam, string szBody, IntPtr pBuf, ref int iLength);




        public delegate void OpenSDK_Push_MessageHandler(string szDesc, string szContent, string szDetail, IntPtr pUser);



        [DllImport(pathDLL)]
        public static extern int OpenSDK_Push_SetAlarmCallBack(OpenSDK_Push_MessageHandler pHandle, IntPtr pUser);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Push_SetDeviceStatusCallBack(OpenSDK_Push_MessageHandler pHandle, IntPtr pUser);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Push_SetTransparentChannelCallBack(OpenSDK_Push_MessageHandler pHandle, IntPtr pUser);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Push_StartRecvEx(string szPushSecret);


        [DllImport(pathDLL)]
        public static extern int OpenSDK_Push_StopRecv();


        [DllImport(pathDLL)]
        public static extern int OpenSDK_GetLastErrorCode();

        [DllImport(pathDLL)]
        public static extern string OpenSDK_GetLastErrorDesc();


        [DllImport(pathDLL)]
        public static extern string OpenSDK_GetSdkVersion();


        #endregion


        #region Erros

        public const int OPEN_SDK_GENERAL_ERROR = -1; ///< 【-1】general (generic) error
        public const int OPEN_SDK_BASE = 10000; ///< open SDK base error
        public const int OPEN_SDK_NETWORK_SETUP_BASE = 100000; ///< Network level error codes, CURL feedback, all call the open platform interface to report errors (when testing, first ensure that the login is successful)
        public const int OPEN_SDK_USER_OPERATION_BASE = 200000; ///< user operation level error code
        public const int OPEN_SDK_OPENAPI_BASE = 300000; ///< OpenApi interface level error code, for details refer to https://open.ys7.com/doc/book/index/api-code.html
        public const int OPEN_SDK_SYSTEM_RESOURCE_BASE = 400000; ///< system resource level error
        public const int OPEN_SDK_NETSTREAM_BASE = 500000; ///< NetStream error code
        public const int OPEN_SDK_NOT_SUPPORT_BASE = 600000; ///< unsupported error code
        public const int OPEN_SDK_PUSH_BASE = 700000; ///< push level error
        public const int OPEN_SDK_UNDEFINE_BASE = 900000; ///< undefined error code

        public enum EZOPENSDK_ERROR_CODE
        {
            OPEN_SDK_NOERROR = 0, ///< 【0】没有错误

            OPEN_SDK_JSON_ERROR = DefineConstants.OPEN_SDK_BASE + 1, ///< 【10001】Json解析出错
            OPEN_SDK_ERROR, ///< 【10002】获取平台数据出错
            OPEN_SDK_DEV_NO_SUPPORT, ///< 【10003】不支持的设备
            OPEN_SDK_ALLOC_ERROR, ///< 【10004】申请内存失败
            OPEN_SDK_PARAM_ERROR, ///< 【10005】传入参数非法
            OPEN_SDK_SAFE_KEY_ERROR, ///< 【10006】安全密钥出错
            OPEN_SDK_SEARCHING_ERROR, ///< 【10007】录像搜索出错
            OPEN_SDK_SYNC_ERROR, ///< 【10008】同步参数出错
            OPEN_SDK_INTERFACE_NO_IMPL, ///< 【10009】接口未实现，主要针对平台
            OPEN_SDK_ORDER_ERROR, ///< 【10010】接口调用顺序出错
            OPEN_SDK_SESSION_EXPIRED, ///< 【10011】SESSION已失效，Session对象被释放

            OPEN_SDK_COULDNT_RESOLVE_HOST = DefineConstants.OPEN_SDK_NETWORK_SETUP_BASE + 6, ///< 【100006】给定的远程主机没有得到解析，这里是指platform域名无法正常解析,可能是DNS没有配置或者机器没有连网。
            OPEN_SDK_COULDNT_CONNECT, ///< 【100007】远程主机不可达，这里是指无法访问platform,可能是platform地址配置错误。
            OPEN_SDK_OPERATION_TIMEOUT, ///< 【100028】请求操作超时, 超时时间为20s, 请求平台超时，请检查platform地址配置错误。

            OPEN_SDK_BAD_PARAMS = DefineConstants.OPEN_SDK_USER_OPERATION_BASE + 1, ///< 【200001】接口传入参数不符合要求
            OPEN_SDK_SESSION_INVALID, ///< 【200002】当前Session不存在或者被释放,可能是SessionId传入值错误或者是Session已经被释放。
            OPEN_SDK_VIDEO_RECORD_NOT_EXIST, ///< 【200003】指定时间段内录像记录不存在
            OPEN_SDK_VIDEO_RECORD_SEARCHING, ///< 【200004】录像记录正在搜索
            OPEN_SDK_STOP_ALARM_REC_FAILED, ///< 【200005】关闭告警失败, 可能是没有开启告警或者已经关闭告警
            OPEN_SDK_PERMANENT_KEY_INVALID, ///< 【200006】验证码不正确
            OPEN_SDK_PIC_DECRYPT_FAILED, ///< 【200007】图片解码失败
            OPEN_SDK_PIC_CONTENT_INVALID, ///< 【200008】图片内容无效
            OPEN_SDK_PIC_NO_NEED_DECRYPT, ///< 【200009】图片无需解码
            OPEN_SDK_PIC_COULDNT_ALLOC_BUFFERS, ///< 【200010】无法分配图片资源内存，可能内存不足或者图片过大
            OPEN_SDK_SDK_LOAD_FAILED, ///< 【200011】依赖库没有加载
            OPEN_SDK_SDK_DEVICE_NOT_SUPPORT, ///< 【200012】设备不支持的操作, 一般是指能力级上不支持
            OPEN_SDK_NOT_SUPPORT_PAUSEPLAYBACK, ///< 【200013】设备能力集不支持暂停回放功能，如需暂停请使用停止回放接口
            OPEN_SDK_NOT_SUPPORT_SPEED_PLAYBACK, ///< 【200014】设备能力集不支持该速率的倍速回放

            OPEN_SDK_RESPINFO_BAD = DefineConstants.OPEN_SDK_OPENAPI_BASE + 1, ///< 【300001】请求返回的信息,json无法正常解析,可能是PlatformAddr配置有问题
            OPEN_SDK_RESPINFO_INVALID = DefineConstants.OPEN_SDK_OPENAPI_BASE + 2, ///< 【300002】请求返回信息格式有误
            OPEN_SDK_DEVICE_RSP_TIMEOUT = DefineConstants.OPEN_SDK_OPENAPI_BASE + 2009, ///< 【302009】设备请求响应超时异常, 检测设备网络或者稍后重试
            OPEN_SDK_DEVICE_SAFE_INVALID = DefineConstants.OPEN_SDK_OPENAPI_BASE + 5002, ///< 【305002】设备验证码错误, 请重新确认设备验证码
            OPEN_SDK_ACCESSTOKEN_INVALID = DefineConstants.OPEN_SDK_OPENAPI_BASE + 10002, ///< 【310002】accesstoken异常或者过期,accessToken异常或请求方法不存在
            OPEN_SDK_SIGNATURE_ERROR = DefineConstants.OPEN_SDK_OPENAPI_BASE + 10008, ///< 【310008】表示输入参数有问题。平台显示签名错误
            OPEN_SDK_APPKEY_NOMATCH_TOKEN_ERROR = DefineConstants.OPEN_SDK_OPENAPI_BASE + 10018, ///< 【310018】AccessToken与Appkey不匹配, 请检查获取accessToken对应的appKey和SDK中设置的appKey是否一致
            OPEN_SDK_USERID_PHONE_UNBIND = DefineConstants.OPEN_SDK_OPENAPI_BASE + 10014, ///< 【310014】APPKEY下对应的第三方userId和phone未绑定
            OPEN_SDK_CHANNEL_NOT_EXIST = DefineConstants.OPEN_SDK_OPENAPI_BASE + 20001, ///< 【320001】通道不存在,通道对应某一监控点
            OPEN_SDK_DEVICE_OFFLINE = DefineConstants.OPEN_SDK_OPENAPI_BASE + 20007, ///< 【320007】设备不在线
            OPEN_SDK_USER_NOTOWN_DEVICE = DefineConstants.OPEN_SDK_OPENAPI_BASE + 20018, ///< 【320018】该用户不拥有该设备
            OPEN_SDK_SERVER_DATA_BAD = DefineConstants.OPEN_SDK_OPENAPI_BASE + 49999, ///< 【349999】数据异常, 反馈开发进一步确认问题
            OPEN_SDK_SERVER_ERROR = DefineConstants.OPEN_SDK_OPENAPI_BASE + 50000, ///< 【350000】开平服务器异常, 反馈开发进一步确认问题

            OPEN_SDK_COULDNT_CREATE_THREAD = DefineConstants.OPEN_SDK_SYSTEM_RESOURCE_BASE + 1, ///< 【400001】创建线程失败
            OPEN_SDK_COULDNT_ALLOC_BUFFERS = DefineConstants.OPEN_SDK_SYSTEM_RESOURCE_BASE + 2, ///< 【400002】申请内存资源失败

            OPEN_SDK_DEV_NOT_SUPPORT = DefineConstants.OPEN_SDK_NOT_SUPPORT_BASE + 1, ///< 【600001】不支持非1.7设备
            OPEN_SDK_API_NO_IMPLEMENT = DefineConstants.OPEN_SDK_NOT_SUPPORT_BASE + 2, ///< 【600002】接口未实现

            OPEN_SDK_START_TALK_FAILED = DefineConstants.OPEN_SDK_NETSTREAM_BASE + 1, ///< 【500001】对讲开启传入参数有误
            OPEN_SDK_TALK_OPENED = DefineConstants.OPEN_SDK_NETSTREAM_BASE + 2, ///< 【500002】对讲已经开启

            OPEN_SDK_PUSH_PARAM_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 10001, ///< 【710001】传入参数非法
            OPEN_SDK_PUSH_DATA_UNINIT_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 10002, ///< 【710002】数据未初始化，请先调用Init接口初始化
            OPEN_SDK_PUSH_NO_REGISTER_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 10003, ///< 【710003】未向Push平台注册，即未调register接口
            OPEN_SDK_PUSH_NO_MQTT_CREATE_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 10004, ///< 【710004】未创建创建推送对象，即未调create接口
            /* MQTT Client错误码 */
            OPEN_SDK_PUSH_MQTT_DISCONNECTED_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 20003, ///< 【720003】sdk同push服务器断开连接
            OPEN_SDK_PUSH_MQTT_MAX_MESSAGES_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 20004, ///< 【720004】达到消息接收上限
            OPEN_SDK_PUSH_MQTT_BAD_UTF8_STRING_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 20005, ///< 【720005】不合法的UTF-8字符串
            OPEN_SDK_PUSH_MQTT_NULL_PARAMETER_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 20006, ///< 【720006】传入参数为空指针
            /* MQTT Protocol错误码 */
            OPEN_SDK_PUSH_MQTT_VERSION_INVALID_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 30001, ///< 【730001】连接失败，协议版本不支持
            OPEN_SDK_PUSH_MQTT_IDENTIFIER_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 30002, ///< 【730002】连接失败，唯一标识不正确
            OPEN_SDK_PUSH_MQTT_SERVER_UNAVAILABLE_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 30003, ///< 【730003】连接失败，服务不存在
            OPEN_SDK_PUSH_MQTT_BAD_USERNAME_PASSWORD_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 30004, ///< 【730004】连接失败，mqtt用户名和密码不正确
            OPEN_SDK_PUSH_MQTT_NOT_AUTHORIZED_ERRO = DefineConstants.OPEN_SDK_PUSH_BASE + 30005, ///< 【730005】连接失败，未授权
            /* Push Platform错误码 */
            OPEN_SDK_PUSH_PLATFORM_RESPINFO_BAD = DefineConstants.OPEN_SDK_PUSH_BASE + 40001, ///< 【740001】请求返回的信息,json无法正常解析,可能是PlatformAddr配置有问题
            OPEN_SDK_PUSH_PLATFORM_RESPINFO_INVALID = DefineConstants.OPEN_SDK_PUSH_BASE + 40002, ///< 【740002】请求返回信息格式有误
            OPEN_SDK_PUSH_PLATFORM_SESSION_INVALID_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 40003, ///< 【740003】Session Invalid
            OPEN_SDK_PUSH_PLATFORM_UNAUTHORIZED_ERROR = DefineConstants.OPEN_SDK_PUSH_BASE + 40401, ///< 【740401】Bad credentials, 可能是PushId有误或者不支持
            /* Push 系统资源错误码 */
            OPEN_SDK_PUSH_COULDNT_CREATE_THREAD = DefineConstants.OPEN_SDK_PUSH_BASE + 50001, ///< 【750001】创建线程失败
            OPEN_SDK_PUSH_COULDNT_ALLOC_BUFFERS = DefineConstants.OPEN_SDK_PUSH_BASE + 50002, ///< 【750002】申请内存资源失败
            /* Push 网络级别错误码 */
            OPEN_SDK_PUSH_COULDNT_RESOLVE_HOST = DefineConstants.OPEN_SDK_PUSH_BASE + 60006, ///< 【760006】给定的远程主机没有得到解析，这里是指platform域名无法正常解析,可能是DNS没有配置或者机器没有连网。
            OPEN_SDK_PUSH_COULDNT_CONNECT = DefineConstants.OPEN_SDK_PUSH_BASE + 60007, ///< 【760007】远程主机不可达，这里是指无法访问platform,可能是platform地址配置错误。
            OPEN_SDK_PUSH_OPERATION_TIMEOUT = DefineConstants.OPEN_SDK_PUSH_BASE + 60028, ///< 【760028】请求操作超时, 超时时间为20s, 请求平台超时，请检查platform地址配置错误。

            OPEN_SDK_ALLOCSESSION_FAILED = DefineConstants.OPEN_SDK_UNDEFINE_BASE + 1, ///< 【900001】AllocSession 失败
            OPEN_SDK_SEARCH_RECORD_FAILED, ///< 【900002】查询回放记录失败
            OPEN_SDK_START_ALARM_REC_FAILED ///< 【900003】开启告警失败
        }





        public const string OPEN_SDK_STREAM_ACCESSTOKEN_ERROR_OR_EXPIRE = "UE001"; ///< accesstoken is invalid, please get accesstoken again
        public const string OPEN_SDK_STREAM_PU_NO_RESOURCE = "UE101"; ///< The number of device connections is too large, upgrade the firmware version of the device, Hikvision devices can consult customer service for the upgrade process
        public const string OPEN_SDK_STREAM_TRANSF_DEVICE_OFFLINE = "UE102"; ///< The device is offline, please try again after confirming that the device is online
        public const string OPEN_SDK_STREAM_INNER_TIMEOUT = "UE103"; ///< The playback failed, the request to connect to the device timed out, check whether the network connection of the device is normal
        public const string OPEN_SDK_STREAM_INNER_VERIFYCODE_ERROR = "UE104"; ///< The video verification code is wrong, it is recommended to check the verification code marked on the device
        public const string OPEN_SDK_STREAM_PLAY_FAIL = "UE105"; ///< The video playback failed, please check the specific error code iErrorCode in the message callback
        public const string OPEN_SDK_STREAM_TRANSF_TERMINAL_BINDING = "UE106"; ///< Terminal binding is enabled for the current account, and only specified devices are allowed to log in
        public const string OPEN_SDK_STREAM_VIDEO_RECORD_NOTEXIST = "UE108"; ///< no video file found
        public const string OPEN_SDK_STREAM_VTDU_CONCURRENT_LIMIT = "UE109"; ///< The limit on the number of concurrent ways to fetch streams, please upgrade to the enterprise version
        public const string OPEN_SDK_STREAM_UNSUPPORTED = "UE110"; ///< The resolution type not supported by the device, please select according to the preview capability level of the device.
        public const string OPEN_SDK_STREAM_DEVICE_RETURN_ON_VIDEO_SOURCE = "UE111"; ///< The device returns no video source, please check if the device is in good contact.



        //define  MessageInfo 
        public const string INS_EXECUTION_PROCESS_ADDTASK = "7_1"; ///< ���񱻳ɹ�����������"
        public const string INS_EXECUTION_PROCESS_TASKDO = "7_2"; ///< ����ʼ��ִ��"
        public const string INS_EXECUTION_PROCESS_PARSE_PARAM = "7_3"; ///< ����������Ϣ"
        public const string INS_EXECUTION_PROCESS_LOCAL = "7_4"; ///< :�жϾ�����"
        public const string INS_EXECUTION_PROCESS_CHECKPWD = "7_5"; ///< :��֤�豸����"
        public const string INS_EXECUTION_PROCESS_NETTYPE = "7_5"; ///< :ѡ��ȡ����ʽ"
        public const string INS_EXECUTION_PROCESS_PSIA = "7_6_1"; ///<:����ʹ��PSIA��ʽȡ��"
        public const string INS_EXECUTION_PROCESS_P2P = "7_6_2"; ///<:����ʹ��P2P��ʽȡ��"
        public const string INS_EXECUTION_PROCESS_VTDU = "7_6_3"; ///<:����ʹ��VTDU��ʽȡ��"
        public const string INS_EXECUTION_PROCESS_TASKEND = "7_7_1"; ///<:����ȡ��ʧ�ܣ�����ȡ��"
        public const string INS_EXECUTION_PROCESS_TASKEND_SUCC = "7_7_2"; ///<:����ȡ���ɹ�������������ȴ�����������"
        public const string INS_EXECUTION_PROCESS_NEXT = "7_8"; ///<:����ȡ��ʧ�ܣ��л�ȡ����ʽ"
        public const string INS_EXECUTION_PROCESS_PSIA_PORT = "7_9"; ///<:��ȡ����PSIAȡ���˿�"


        public const int INS_ERROR_NOERROR = 0; ///<�޴���
        public const int INS_ERROR_LOAD_RTSP_FAILED = 1; ///<����rtsp��ʧ��
        public const int INS_ERROR_LOAD_PLAYCTRL_FAILED = 2; ///<���ز��ſ�ʧ��
        public const int INS_ERROR_LOAD_SYSTRANSFORM_FAILED = 3; ///<����ת��װ��ʧ��
        public const int INS_ERROR_LOAD_HTTPCLIENT_FAILED = 4; ///<����http��ʧ��
        public const int INS_ERROR_PARAMETER_ERROR = 5; ///<��������
        public const int INS_ERROR_ORDER_ERROR = 6; ///<����˳�����
        public const int INS_ERROR_ALLOC_RESOURCE_FAILED = 7; ///<������Դʧ��
        public const int INS_ERROR_NOT_INITLIB = 8; ///<û�г�ʼ��
        public const int INS_ERROR_OPERTION_NOSUPPORT = 9; ///<������֧��
        public const int INS_ERROR_OPENFILE_ERROR = 10; ///<���ļ�ʧ��
        public const int INS_ERROR_WRITEFILE_ERROR = 11; ///<д�ļ�ʧ��
        public const int INS_ERROR_READFILE_ERROR = 12; ///<���ļ�ʧ��
        public const int INS_ERROR_INIT_HPR_FAILED = 13; ///<��ʼ��hpr��ʧ��
        public const int INS_ERROR_AUDIO_MONOPOLIZED = 14; ///<��������ռ
        public const int INS_ERROR_CREATE_SOCKET_ERROR = 15; ///<����socketʧ��
        public const int INS_ERROR_NETWORK_CONNECT_FAILED = 16; ///<����ʧ��
        public const int INS_ERROR_NETWORK_SEND_ERROR = 17; ///<����ʧ��
        public const int INS_ERROR_NETWORK_RECV_ERROR = 18; ///<����ʧ��
        public const int INS_ERROR_NETWORK_SEND_TIMEOUT = 19; ///<���ͳ�ʱ
        public const int INS_ERROR_NETWORK_RECV_TIMEOUT = 20; ///<���ճ�ʱ
        public const int INS_ERROR_NETWORK_RESOLVE_FAILED = 21; ///<������������
        public const int INS_ERROR_XML_PARSE_ERROR = 22; ///<xml��������
        public const int INS_ERROR_XML_NODE_ERROR = 23; ///<xml������
        public const int INS_ERROR_NO_EXCEL_DRIVER_ERROR = 24; ///<û�а�װExcel����
        public const int INS_ERROR_PARSE_URL_FAILED = 25; ///<URL����ʧ��
        public const int INS_ERROR_LOADRTSPSDKPROC_ERROR = 26; ///<�Ҳ���rtsp�ӿڵ�ַ
        public const int INS_ERROR_LOADPLAYERSDKPROC_ERROR = 27; ///<�Ҳ������ſ�ӿڵ�ַ
        public const int INS_ERROR_LOADSYSTRANSFORMPROC_ERROR = 28; ///<�Ҳ���ת��װ��ӿڵ�ַ
        public const int INS_ERROR_LOADHTTPSDKPROC_ERROR = 29; ///<�Ҳ���http��ӿڵ�ַ
        public const int INS_ERROR_START_WAVEIN_FAILED = 30; ///<��ʼ��Ƶ�ɼ�ʧ��
        public const int INS_ERROR_START_WAVEOUT_FAILED = 31; ///<��ʼ��Ƶ����ʧ��
        public const int INS_ERROR_INIT_G722_CODEC_FAILED = 32; ///<��ʼ��G722�����ʧ��
        public const int INS_ERROR_NOT_ENOUGH_DISK_FREESPACE = 33; ///<���̿ռ䲻��
        public const int INS_ERROR_FILE_ALREADY_EXIST = 34; ///<�ļ��Ѵ���
        public const int INS_ERROR_LOAD_PPV_FAILED = 35; ///<����ppv��ʧ��
        public const int INS_ERROR_LOADPPVSDKPROC_ERROR = 36; ///<�Ҳ���libPPVClient�ӿڵ�ַ
        public const int INS_ERROR_LOAD_STUN_FAILED = 37; ///<����stun��ʧ��
        public const int INS_ERROR_LOADSTUNSDKPROC_ERROR = 38; ///<�Ҳ���StunClientLib�ӿڵ�ַ
        public const int INS_ERROR_RECORD_FILE_NOT_EXIST = 39; ///< �Ҳ���ָ��ʱ�䷶Χ�ڵ�¼���ļ�
        public const int INS_ERROR_FAILED_RTSP_PORT = 40; ///< ͨ��PSIAȡ������ȡRTSP�˿�ʧ��
        public const int INS_ERROR_SSL_CONNECT_FAILED = 41; ///<SSL��ʽ����ʧ��
        public const int INS_ERROR_JSON_PARSE_ERROR = 42; ///<JSON����ʧ��
        public const int INS_ERROR_LOADCASSDKPROC_ERROR = 43; ///<�Ҳ���libCASClient�ӿڵ�ַ
        public const int INS_ERROR_LOAD_CAS_FAILED = 44; ///<����CASClient��ʧ��
        public const int INS_ERROR_OPERATIONCODE_FAILED = 45; ///<��ȡ������ʧ��
        public const int INS_ERROR_LOAD_GETHDSIGN_FAILED = 46; ///<����libGetHDSign��ʧ��
        public const int INS_ERROR_HDSIGN_FAILED = 47; ///<��ȡӲ��������ʧ��
        public const int INS_ERROR_NULL_ADDRESS = 48; ///<GetIpAddress����IPΪ��

        //rtsp

        public const int INS_ERROR_RTSP_INIT_FAILED = 50;
        public const int INS_ERROR_RTSP_NOT_INIT = 51;
        public const int INS_ERROR_RTSP_PARSE_ERROR = 52;
        public const int INS_ERROR_RTSP_STATUS_ERROR = 53;
        public const int INS_ERROR_RTSP_INIT_RTP_ERROR = 54;
        public const int INS_ERROR_RTSP_CREATE_SOCKET_ERROR = 55;
        public const int INS_ERROR_RTSP_CONNECT_FAILED = 56;
        public const int INS_ERROR_RTSP_HTTP_GET_ERROR = 57;
        public const int INS_ERROR_RTSP_HTTP_POST_ERROR = 58;
        public const int INS_ERROR_RTSP_GETPORT_FAILED = 59;


        //play

        public const int INS_ERROR_PLAYM4_NOERROR = 100; ///<no error
        public const int INS_ERROR_PLAYM4_PARA_OVER = 101; ///<input parameter is invalid;
        public const int INS_ERROR_PLAYM4_ORDER_ERROR = 102; ///<The order of the function to be called is error.
        public const int INS_ERROR_PLAYM4_TIMER_ERROR = 103; ///<Create multimedia clock failed;
        public const int INS_ERROR_PLAYM4_DEC_VIDEO_ERROR = 104; ///<Decode video data failed.
        public const int INS_ERROR_PLAYM4_DEC_AUDIO_ERROR = 105; ///<Decode audio data failed.
        public const int INS_ERROR_PLAYM4_ALLOC_MEMORY_ERROR = 106; ///<Allocate memory failed.
        public const int INS_ERROR_PLAYM4_OPEN_FILE_ERROR = 107; ///<Open the file failed.
        public const int INS_ERROR_PLAYM4_CREATE_OBJ_ERROR = 108; ///<Create thread or event failed
        public const int INS_ERROR_PLAYM4_CREATE_DDRAW_ERROR = 109; ///<Create DirectDraw object failed.
        public const int INS_ERROR_PLAYM4_CREATE_OFFSCREEN_ERROR = 110; ///<failed when creating off-screen surface.
        public const int INS_ERROR_PLAYM4_BUF_OVER = 111; ///<buffer is overflow
        public const int INS_ERROR_PLAYM4_CREATE_SOUND_ERROR = 112; ///<failed when creating audio device.
        public const int INS_ERROR_PLAYM4_SET_VOLUME_ERROR = 113; ///<Set volume failed
        public const int INS_ERROR_PLAYM4_SUPPORT_FILE_ONLY = 114; ///<The function only support play file.
        public const int INS_ERROR_PLAYM4_SUPPORT_STREAM_ONLY = 115; ///<The function only support play stream.
        public const int INS_ERROR_PLAYM4_SYS_NOT_SUPPORT = 116; ///<System not support.
        public const int INS_ERROR_PLAYM4_FILEHEADER_UNKNOWN = 117; ///<No file header.
        public const int INS_ERROR_PLAYM4_VERSION_INCORRECT = 118; ///<The version of decoder and encoder is not adapted.
        public const int INS_ERROR_PLAYM4_INIT_DECODER_ERROR = 119; ///<Initialize decoder failed.
        public const int INS_ERROR_PLAYM4_CHECK_FILE_ERROR = 120; ///<The file data is unknown.
        public const int INS_ERROR_PLAYM4_INIT_TIMER_ERROR = 121; ///<Initialize multimedia clock failed.
        public const int INS_ERROR_PLAYM4_BLT_ERROR = 122; ///<Blt failed.
        public const int INS_ERROR_PLAYM4_UPDATE_ERROR = 123; ///<Update failed.
        public const int INS_ERROR_PLAYM4_OPEN_FILE_ERROR_MULTI = 124; ///<openfile error, streamtype is multi
        public const int INS_ERROR_PLAYM4_OPEN_FILE_ERROR_VIDEO = 125; ///<openfile error, streamtype is video
        public const int INS_ERROR_PLAYM4_JPEG_COMPRESS_ERROR = 126; ///<JPEG compress error
        public const int INS_ERROR_PLAYM4_EXTRACT_NOT_SUPPORT = 127; ///<Don't support the version of this file.
        public const int INS_ERROR_PLAYM4_EXTRACT_DATA_ERROR = 128; ///<extract video data failed.
        public const int INS_ERROR_PLAYM4_SECRET_KEY_ERROR = 129; ///<Secret key is error ///<add 20071218
        public const int INS_ERROR_PLAYM4_DECODE_KEYFRAME_ERROR = 130; ///< ����ؼ�֡ʧ��
        public const int INS_ERROR_PLAYM4_NEED_MORE_DATA = 131; ///< ���ݲ���
        public const int INS_ERROR_PLAYM4_INVALID_PORT = 132; ///< ��Ч�˿ں�
        public const int INS_ERROR_PLAYM4_NOT_FIND = 133; ///< ����ʧ��
        public const int INS_ERROR_PLAYM4_FAIL_UNKNOWN = 199; ///< δ֪�Ĵ���


        public const int INS_ERROR_PWD_ERROR = 401; //�����
        public const int INS_ERROR_FORMAT_ERROR = 501; //��ʽ����
        public const int INS_ERROR_FORMATING = 400; //��ʽ����
        public const int INS_ERROR_STREAM_LIMIT = 410; //ȡ��·������
        public const int INS_ERROR_NO_RECORD = 402; //��ʱ��ط���¼��
        public const int INS_ERROR_EDEFENSE_NO_SUPPORT = 402; //��֧�ֲ���
        public const int INS_ERROR_FAILED = 500; //����ִ��ʧ��
        public const int INS_ERROR_DEV_NOT_ONLINE = 801; //�豸������
        public const int INS_ERROR_PRIVACY = 409; //��˽����״̬

        // common   error  2000~3000

        public const int INS_ERROR_V17_COMMON_BASE = 2000;
        public const int INS_ERROR_V17_GET_OPERATIONCODE_ERROR = 2001; ///<��ȡ������ʧ��
        public const int INS_ERROR_V17_GET_TOKEN_ERROR = 2002; ///<��ȡȡ��tokenʧ��
        public const int INS_ERROR_V17_SETAUDIOINCALLBACK_ERROR = 2003; ///<���������ɼ��ص�����ʧ��
        public const int INS_ERROR_V17_STARTPLAY_ERROR = 2004; ///<������������ʧ��
        public const int INS_ERROR_V17_STARTAUDIOIN_ERROR = 2005; ///<�����ɼ���������ʧ��
        public const int INS_ERROR_V17_TTS_CREATETALK_ERROR = 2006; ///<tts TALK_VTDU CreateTalk error
        public const int INS_ERROR_V17_TTS_SETDATACALLBACK_ERROR = 2007; ///<tts TALK_VTDU SetDataCallBack error
        public const int INS_ERROR_V17_TTS_SETMSGCALLBACK_ERROR = 2008; ///<tts TALK_VTDU SetMsgCallBack error
        public const int INS_ERROR_V17_TTS_STARTTALK_ERROR = 2009; ///<tts TALK_VTDU StartTalk error
        public const int INS_ERROR_V17_CAS_CREATESESSION_ERROR = 2010; ///<cas TALK_TCP/TALK_UDP CreateSession error
        public const int INS_ERROR_V17_CAS_VOICETALKSTART_ERROR = 2011; ///<cas TALK_TCP/TALK_UDP VoiceTalkStart error
        public const int INS_ERROR_V17_PERMANENTKEY_EXCEPTION = 2012; ///<����������Կ  1.������Կ������MD5��Կ�����
        public const int INS_ERROR_V17_VTDU_OPERKEY_ERROR = 2013; ///<VTDU���صĲ���������������Կ����
        public const int INS_ERROR_V17_STOPAUDIOIN_ERROR = 2014; ///<ֹͣ�����ɼ�ʧ��
        public const int INS_ERROR_V17_STOPPLAY_ERROR = 2015; ///<ֹͣ��������ʧ��
        public const int INS_ERROR_V17_STOPAUDIOIN_SUCC = 2016; ///<ֹͣ�����ɼ��ɹ�
        public const int INS_ERROR_V17_STOPPLAY_SUCC = 2017; ///<ֹͣ�������ųɹ�
        public const int INS_ERROR_V17_VTDU_TIMEOUT = 2021; ///<��ý�����豸���ͻ�������ʱ
        public const int INS_ERROR_V17_VTDU_CKECK_TOKEN = 2022; ///<��֤tokenʧ��
        public const int INS_ERROR_V17_CLIENT_URL_ERROR = 2023; ///<�ͻ��˵�URL��ʽ����
        public const int INS_ERROR_V17_VTDU_CLIENT_TIMEOUT = 2025; ///<vtdu�ͻ��˽��ջ�Ӧ��ʱ
        public const int INS_ERROR_V17_STREAM_SESSION_ERROR = 2026; ///<ȡ��session����
        public const int INS_ERROR_V17_WAIT_HEADER_TIMEOUT = 2027; ///<�ȴ���ͷ��ʱ
        public const int INS_ERROR_V17_USER_STOP = 2028; ///<�û�ֹͣ

        public const int INS_ERROR_V17_VTDU_CREATE_SESSION = 2031; ///<vtduȡ������sessionʧ��
        public const int INS_ERROR_V17_VTDU_DESTORY_SESSION = 2032; ///<vtduȡ������sessionʧ��
        public const int INS_ERROR_V17_VTDU_START = 2033; ///<vtduȡ����ʼʧ�ܣ�����-1ʱ����
        public const int INS_ERROR_V17_VTDU_STOP = 2034; ///<vtduȡ��ֹͣʧ��
        public const int INS_ERROR_V17_VTDU_PAUSE = 2035; ///<vtduȡ����ͣʧ��
        public const int INS_ERROR_V17_VTDU_RESUME = 2036; ///<vtduȡ���ָ�ʧ��
        public const int INS_ERROR_V17_VTDU_CHANGE_RATE = 2037; ///<vtduȡ���ı�����ʧ��


        public const int INS_ERROR_V17_VTDU_TOKEN_NO_AUTHORITY = 2044; ///<VTDU token��Ȩ��
        public const int INS_ERROR_V17_VTDU_SESSION_NO_EXIST = 2045; ///<VTDU session������
        public const int INS_ERROR_V17_VTDU_TOKEN_OTHER = 2046; ///<VTDU ��֤token�����쳣�������壩
        public const int INS_ERROR_V17_VTDU_TOKEN_NOCONNECT_VTM = 2047; ///<VTDU �ͻ������Ӳ���VTM
        public const int INS_ERROR_V17_VTDU_TOKEN_NOCONNECT_VTDU = 2048; ///<VTDU �ͻ������Ӳ���VTDU
        public const int INS_ERROR_V17_INVALID_DEVICE_CHANNAL = 2049; ///<�豸ͨ����
        public const int INS_ERROR_V17_DEVICE_UNSUPPORT_STREAMTYPE = 2050; ///<�豸��֧�ֵ���������
        public const int INS_ERROR_V17_DEVICE_UNCONNECT_VTDU = 2051; ///<�豸���Ӳ�����ý��
        public const int INS_ERROR_V17_CLIENT_ERROR_CASIP = 2052; ///<�ͻ��˸���cas��ַ��Ϣ����
        public const int INS_ERROR_V17_VIDEO_SHARED_TIMEEND = 2053; ///<��Ƶ����ʱ���Ѿ�����
        public const int INS_ERROR_V17_VTDU_RECV_HEADER_TIMEOUT = 2054; ///<��ý�������ͷ��ʱ8s
        public const int INS_ERROR_V17_VTDU_VTDU_IPARAMTYPE_ERROR = 2055; ///< Vtdu iParamType error
        public const int INS_ERROR_V17_GET_OPERATIONCODE_PARAMETER_ERROR = 2056; ///< ��ȡ�������������
        public const int INS_ERROR_V17_REGET_OPERATIONCODE_DEV_NOT_FOUND = 2057; ///< �޷��ҵ��豸
        public const int INS_ERROR_V17_GETTOKEN_URL_OR_CLIENTSESSION_NULL = 2058; ///< ��ȡToken��������
        public const int INS_ERROR_V17_GETTOKEN_EMPTY_TOKEN = 2059; ///< ��ȡ��TokenΪ��
        public const int INS_ERROR_V17_FIND_FILE_FAILED = 2060; ///< �Ҳ��������ļ�                    2037        ///<vtduȡ���ı�����ʧ��


        /************************************************************************/
        /*note: vtdu client libary error code base on INS_ERROR_PRIVATE_VTDU_CLN_ERR_CODE
        /*tranform rule: the code given by the libary add INS_ERROR_PRIVATE_VTDU_CLN_ERR_CODE (eg.2200+code)
        /*so we reserve 2200-2600 for these code
        /************************************************************************/

        public const int INS_ERROR_PRIVATE_VTDU_CLN_ERR_CODE = 2200;
        public const int INS_ERROR_PRIVATE_VTDU_BAD_MSG = 2204; //������Ϣ�����Ƿ�
        public const int INS_ERROR_PRIVATE_VTDU_NO_ENOUGH_ROOM = 2205; //�ڴ���Դ����
        public const int INS_ERROR_PRIVATE_VTDU_INVALID_VTDU_HOST = 2208; //����vtm����vtdu��ַ���Ϸ�
        public const int INS_ERROR_PRIVATE_VTDU_INVALID_SSN_STREAMKEY = 2210; //����vtm���ػỰ��ʶ���Ȳ��Ϸ�
        public const int INS_ERROR_PRIVATE_VTDU_ALLOCATE_SOCKET_FAIL = 2222; //��ȡϵͳsocket��Դʧ��
        public const int INS_ERROR_PRIVATE_VTDU_CONNECT_SRV_FAIL = 2224; //���ӷ�����ʧ��
        public const int INS_ERROR_PRIVATE_VTDU_REQUEST_TIMEOUT = 2225; //�ͻ�������δ�յ������Ӧ��
        public const int INS_ERROR_PRIVATE_VTDU_DISCONNECTED_LINK = 2226; //��·�Ͽ�
        public const int NS_ERROR_PRIVATE_VTDU_ERR = 2201; //ͨ�ô��󷵻�
        public const int NS_ERROR_PRIVATE_VTDU_NULL_PTR = 2202; //���Ϊ��ָ��
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_PARAS = 2203; //���ֵ��Ч
        public const int NS_ERROR_PRIVATE_VTDU_BAD_MSG = 2204; //������Ϣ�����Ƿ�
        public const int NS_ERROR_PRIVATE_VTDU_NO_ENOUGH_ROOM = 2205; //�ڴ���Դ����
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_MSGHEAD = 2206; //Э���ʽ���Ի�����Ϣ�峤�ȳ���STREAM_MAX_MSGBODY_LEN
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_SERIAL = 2207; //�豸���кų��Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAMURL = 2208; //ȡ��url���Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_VTDU_HOST = 2209; //����vtm����vtdu��ַ���Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_PEER_HOST = 2210; //����vtm���ؼ���vtdu��ַ���Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_SSN_STREAMKEY = 2211; //����vtm���ػỰ��ʶ���Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAM_HEAD = 2212; //vtdu������ͷ���Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAM_SSN = 2213; //vtdu�Ự���ȷǷ�
        public const int NS_ERROR_PRIVATE_VTDU_DATAOUT_CALLBACK_UNREG = 2214; //�ص�����δע��
        public const int NS_ERROR_PRIVATE_VTDU_NO_STREAM_SSN = 2215; //vtdu�ɹ���ӦδЯ���Ự��ʶ
        public const int NS_ERROR_PRIVATE_VTDU_NO_STREAM_HEAD = 2216; //vtdu�ɹ���ӦδЯ����ͷ
        public const int NS_ERROR_PRIVATE_VTDU_NO_STREAM = 2217; //������������δʹ��
        public const int NS_ERROR_PRIVATE_VTDU_PB_PARSE_FAILURE = 2218; //������Ϣ��PB����ʧ��
        public const int NS_ERROR_PRIVATE_VTDU_PB_ENCAPSULATE_FAILURE = 2219; //������Ϣ��PB��װʧ��
        public const int NS_ERROR_PRIVATE_VTDU_MEMALLOC_FAIL = 2220; //����ϵͳ�ڴ���Դʧ��
        public const int NS_ERROR_PRIVATE_VTDU_VTDUSRV_NOT_SET = 2221; //vtdu��ַ��δ��ȡ��
        public const int NS_ERROR_PRIVATE_VTDU_NOT_SUPPORTED = 2222; //�ͻ�����δ֧��
        public const int NS_ERROR_PRIVATE_VTDU_ALLOCATE_SOCKET_FAIL = 2223; //��ȡϵͳsocket��Դʧ��
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAM_SSN_ID = 2224; //�ϲ�����StreamSsnId��ƥ��
        public const int NS_ERROR_PRIVATE_VTDU_CONNECT_SRV_FAIL = 2225; //���ӷ�����ʧ��
        public const int NS_ERROR_PRIVATE_VTDU_REQUEST_TIMEOUT = 2226; //�ͻ�������δ�յ������Ӧ��
        public const int NS_ERROR_PRIVATE_VTDU_DISCONNECTED_LINK = 2227; //��·�Ͽ�
        public const int NS_ERROR_PRIVATE_VTDU_NO_CONNECTION = 2228; //û��ȡ������
        public const int NS_ERROR_PRIVATE_VTDU_STREAM_END_SUCC = 2229; //���ɹ�ֹͣ
        public const int NS_ERROR_PRIVATE_VTDU_STREAM_DATAKEY_CHECK_FAIL = 2230; //�ͻ��˷�����У��ʧ��
        public const int NS_ERROR_PRIVATE_VTDU_TCP_BUFFER_FULL = 2231; //Ӧ�ò�tcpճ��������������
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STATUS_CHANGE = 2232; //��Ч״̬Ǩ��
        public const int NS_ERROR_PRIVATE_VTDU_BAD_STATUS = 2233; //��Ч�ͻ���״̬
        public const int NS_ERROR_PRIVATE_VTDU_VTM_VTDUINFO_REQ_TMOUT = 2234; //��vtmȡ����ý����Ϣ����ʱ
        public const int NS_ERROR_PRIVATE_VTDU_PROXY_STARTSTREAM_REQ_TMOUT = 2235; //�����ȡ������ʱ
        public const int NS_ERROR_PRIVATE_VTDU_PROXY_KEEPALIVE_REQ_TMOUT = 2236; //���������ȡ������ʱ
        public const int NS_ERROR_PRIVATE_VTDU_STARTSTREAM_REQ_TMOUT = 2237; //��vtduȡ������ʱ
        public const int NS_ERROR_PRIVATE_VTDU_KEEPALIVE_REQ_TMOUT = 2238; //��vtdu����ȡ������ʱ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_402 = 2602; //�ط��ڲ���¼���ļ�
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_403 = 2603; //�������������Կ���豸��ƥ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_404 = 2604; //�豸������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_405 = 2605; //��ý�����豸���ͻ�������ʱ/cas��Ӧ��ʱ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_406 = 2606; //tokenʧЧ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_407 = 2607; //�ͻ��˵�URL��ʽ����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_409 = 2609; //Ԥ��������˽����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_410 = 2610; //�豸�ﵽ���������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_411 = 2611; //token��Ȩ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_412 = 2612; //session������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_413 = 2613; //��֤token�������쳣�������壩
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_415 = 2615; //�豸ͨ����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_451 = 2651; //�豸��֧�ֵ���������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_452 = 2652; //�豸����Ԥ����ý�������ʧ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_454 = 2654; //��Ƶ����ʱ���Ѿ�����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_491 = 2691; //��ͬ�������ڴ������ܾ����δ���
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_500 = 2700; //��ý��������ڲ���������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_503 = 2703; //vtm����vtdu������ʧ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_544 = 2744; //�豸��������ƵԴ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_545 = 2745; //��Ƶ����ʱ���Ѿ�����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_546 = 2746; //VTDUȡ������2·����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_556 = 2756; //ticketУ��ʧ��


        // CASLIb  error  3000~4000





        public const int INS_ERROR_CASLIB_BASE = 3000;
        public const int INS_ERROR_CASLIB_MSG_UNKNOW_ERROR = 3001; ///<δ֪����
        public const int INS_ERROR_CASLIB_MSG_PARAMS_ERROR = 3002; ///<���Ĳ�������
        public const int INS_ERROR_CASLIB_MSG_PARSE_FAILED = 3003; ///<���Ľ�������
        public const int INS_ERROR_CASLIB_MSG_COMMAND_UNKNOW = 3006; ///<�Ƿ�����
        public const int INS_ERROR_CASLIB_MSG_COMMAND_NO_LONGER_SUPPORTED = 3007; ///<��ʱ����
        public const int INS_ERROR_CASLIB_MSG_COMMAND_NOT_SUITABLE = 3008; ///<��������
        public const int INS_ERROR_CASLIB_MSG_CHECKSUM_ERROR = 3011; ///<У�������
        public const int INS_ERROR_CASLIB_MSG_VERSION_UNKNOW = 3016; ///<Э��汾����
        public const int INS_ERROR_CASLIB_MSG_VERSION_NO_LONGER_SUPPORTED = 3017; ///<Э��汾����
        public const int INS_ERROR_CASLIB_MSG_VERSION_FORBIDDEN = 3018; ///<Э��汾����ֹ
        public const int INS_ERROR_CASLIB_MSG_SERIAL_NOT_FOR_CIVIL = 3021; ///<���кŽ���ʧ��
        public const int INS_ERROR_CASLIB_MSG_SERIAL_FORBIDDEN = 3022; ///<���кű���ֹ
        public const int INS_ERROR_CASLIB_MSG_SERIAL_DUPLICATE = 3023; ///<���к��ظ�
        public const int INS_ERROR_CASLIB_MSG_SERIAL_FLUSHED_IN_A_SECOND = 3024; ///<��ͬ���кŶ�ʱ���ڴ����ظ�����
        public const int INS_ERROR_CASLIB_MSG_SERIAL_NO_LONGER_SUPPORTED = 3025; ///<���кŲ���֧��
        public const int INS_ERROR_CASLIB_MSG_LOCAL_SERVER_BUSY = 3031; ///<�����޷���Ӧ
        public const int INS_ERROR_CASLIB_MSG_LOCAL_SERVER_REFUSED = 3032; ///<���������ܾ�
        public const int INS_ERROR_CASLIB_REG_CANNOT_AFFORD_PU = 3033; ///<�޷���������
        public const int INS_ERROR_CASLIB_REG_CRYPTO_UNMATCHED = 3034; ///<�豸�����㷨��ƥ��
        public const int INS_ERROR_CASLIB_MSG_DEV_TYPE_INVAILED = 3036; ///<�豸���ʹ���
        public const int INS_ERROR_CASLIB_MSG_DEV_TYPE_NO_LONGGER_SUPPORTED = 3037; ///<�豸���Ͳ���֧��
        public const int INS_ERROR_CASLIB_MSG_PU_BUSY = 3041; ///<�豸�޷���Ӧ
        public const int INS_ERROR_CASLIB_MSG_OPERATION_FAILED = 3042; ///<���������
        public const int INS_ERROR_CASLIB_PU_NO_CRYPTO_FOUND = 3043; ///<�豸��ƽ̨δ�ҵ���Ӧ�ļ����㷨
        public const int INS_ERROR_CASLIB_MSG_PU_REFUSED = 3044; ///<�ܾ�
        public const int INS_ERROR_CASLIB_MSG_PU_NO_RESOURCE = 3045; ///<û�п�����Դ     �豸��������
        public const int INS_ERROR_CASLIB_MSG_PU_CHANNEL_ERROR = 3046; ///<ͨ����
        public const int INS_ERROR_CASLIB_SYSTEM_COMMAND_PU_COMMAND_UNSUPPORTED = 3047; ///<��֧�ֵ�����
        public const int INS_ERROR_CASLIB_SYSTEM_COMMAND_PU_NO_RIGHTS_TO_DO_COMMAND = 3048; ///<û��Ȩ��
        public const int INS_ERROR_CASLIB_MSG_NO_SESSION_FOUND = 3049; ///<û���ҵ��Ự
        public const int INS_ERROR_CASLIB_PREVIEW_CHANNEL_BUSY = 3051; ///<��ͨ�����ڷ���
        public const int INS_ERROR_CASLIB_PREVIEW_CLIENT_BUSY = 3052; ///<ȡ����ַ�ظ�
        public const int INS_ERROR_CASLIB_PREVIEW_STREAM_UNSUPPORTED = 3053; ///<��֧�ֵ���������
        public const int INS_ERROR_CASLIB_PREVIEW_TRANSPORT_UNSUPPORTED = 3054; ///<��֧�ֵĴ��䷽ʽ
        public const int INS_ERROR_CASLIB_PREVIEW_CONNECT_SERVER_FAIL = 3055; ///<����Ԥ����ý�������ʧ�� +
        public const int INS_ERROR_CASLIB_PREVIEW_QUERY_WLAN_INFO_FAIL = 3056; ///<��ѯ�豸�������ڵ�ַʧ��
        public const int INS_ERROR_CASLIB_RECORD_SEARCH_START_TIME_ERROR = 3061; ///<����¼��ʼʱ���
        public const int INS_ERROR_CASLIB_RECORD_SEARCH_STOP_TIME_ERROR = 3062; ///<����¼�����ʱ���
        public const int INS_ERROR_CASLIB_RECORD_SEARCH_FAIL = 3063; ///<����¼��ʧ��    +
        public const int INS_ERROR_CASLIB_PLAYBACK_TYPE_UNSUPPORTED = 3066; ///<��֧�ֵĻط�����
        public const int INS_ERROR_CASLIB_PLAYBACK_NO_FILE_MATCHED = 3067; ///<û���ҵ��ļ�
        public const int INS_ERROR_CASLIB_PLAYBACK_START_TIME_ERROR = 3068; ///<��ʼʱ�����
        public const int INS_ERROR_CASLIB_PLAYBACK_STOP_TIME_ERROR = 3069; ///<����Ľ���ʱ��
        public const int INS_ERROR_CASLIB_PLAYBACK_NO_FILE_FOUND = 3070; ///<��ʱ�����û��¼��
        public const int INS_ERROR_CASLIB_PLAYBACK_CONNECT_SERVER_FAIL = 3071; ///<���ӻطŷ�������ʧ��
        public const int INS_ERROR_CASLIB_TALK_ENCODE_TYPE_UNSUPPORTED = 3076; ///<��֧�ֵ�������������
        public const int INS_ERROR_CASLIB_TALK_CHANNEL_BUSY = 3077; ///<��ͨ�����ڶԽ�
        public const int INS_ERROR_CASLIB_TALK_CLIENT_BUSY = 3078; ///<��Ŀ�ĵ�ַ��������
        public const int INS_ERROR_CASLIB_TALK_UNSUPPORTED = 3079; ///<not support talk
        public const int INS_ERROR_CASLIB_TALK_CHANNO_ERROR = 3080; ///<ͨ���Ŵ���
        public const int INS_ERROR_CASLIB_TALK_CONNECT_SERVER_FAILED = 3081; ///<��������������ʧ��
        public const int INS_ERROR_CASLIB_TALK_CONNECT_REFUSED = 3082; ///<�豸�ܾ�
        public const int INS_ERROR_CASLIB_TALK_CONNECT_CAPACITY_LIMITED = 3083; ///<�豸��Դ����
        public const int INS_ERROR_CASLIB_FORMAT_NO_LOCAL_STORAGE = 3086; ///<û�б��ش洢
        public const int INS_ERROR_CASLIB_FORMAT_FORMATING = 3087; ///<���ڸ�ʽ����
        public const int INS_ERROR_CASLIB_FORMAT_FAILED = 3088; ///<���Ը�ʽ��ʧ��
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_REFUSED = 3091; ///<�������ܾ��豸��������
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_VERSION_NOT_FOUND = 3092; ///<û���ҵ�����汾
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_UNNEEDED = 3093; ///<����Ҫ����
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_NO_SERVER_ONLINE = 3094; ///<û����������������
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_ALL_SERVER_BUSY = 3095; ///<�����������������ﵽ�����
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRADING = 3101; ///<������������
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRAD_FAILED = 3102; ///<����ʧ�ܣ�����δ֪����
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRAD_WRITE_FLASH_FAILED = 3103; ///<����дFlashʧ��
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRAD_LANGUAGE_DISMATCH = 3104; ///<�������Բ�ƥ��
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_NO_USER_MATHCED = 3106; ///<�������ʧ�ܣ�û�ж�Ӧ�û�
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_ORIGINAL_PASSWORD_ERROR = 3107; ///<�������ʧ�ܣ�ԭʼ�������
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_NEW_PASSWORD_DECRYPTE_FAILED = 3108; ///<�������ʧ�ܣ����������ʧ��
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_NEW_PASSWORD_CHECK_FAILED = 3109; ///<�������ʧ�ܣ������벻���Ϲ���
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_WRITE_FLASH_FAILED = 3110; ///<��������ʧ�ܣ�дflashʧ��
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_OTHER_FALIURE = 3111; ///<��������ʧ�ܣ�����ԭ��
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_NO_PU_FOUNDED = 3121; ///<������豸������
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_REFUSED_TO_PROTECT_PU = 3122; ///<Ϊ�˱����豸���ܾ�����
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_PU_LIMIT_REACHED = 3123; ///<�豸�ﵽ���ӵĿͻ�������
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_TEARDOWN_PU_CONNECTION = 3124; ///<Ҫ��ͻ��˶Ͽ����豸����
        public const int INS_ERROR_CASLIB_PU_REFUSE_CLIENT_CONNECTION = 3125; ///<�豸�ܾ�ƽ̨���͵Ŀͻ�����������
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_VERIFY_AUTH_ERROR = 3126; ///<CAS����֤������֤�û�Ȩ��ʧ��
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_PU_OPEN_PRIVACY = 3127; ///<�豸������˽����
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_NO_SIGN_RELEATED = 3128; ///</<û�й���������
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_UNSUPPORTED = 3131; ///<��֧�ֵĲ���������
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_FAILED = 3132; ///</<������ʧ��
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_FORCE_FAILED = 3133; ///</<ǿ�Ʋ�����ʧ��
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_NEED_FORCE = 3134; ///</<��Ҫǿ�Ʋ�����
        public const int INS_ERROR_CASLIB_CLOUD_NOT_FOUND = 3141; ///<û���ҵ��ƴ洢������
        public const int INS_ERROR_CASLIB_CLOUD_NO_USER = 3142; ///<�û�δ��ͨ�ƴ洢
        public const int INS_ERROR_CASLIB_CLOUD_FILE_TAIL_REACHED = 3145; ///<�ļ��ѵ���β
        public const int INS_ERROR_CASLIB_CLOUD_INVALID_SESSION = 3146; ///<��Ч��session
        public const int INS_ERROR_CASLIB_CLOUD_INVALID_HANDLE = 3147; ///<��Ч���ļ�
        public const int INS_ERROR_CASLIB_CLOUD_UNKNOWN_CLOUD = 3148; ///<δ֪���ƴ洢����
        public const int INS_ERROR_CASLIB_CLOUD_UNSUPPORT_FILETYPE = 3149; ///<��֧�ֵ��ļ�����
        public const int INS_ERROR_CASLIB_CLOUD_INVALID_FILE = 3150; ///<��Ч���ļ�
        public const int INS_ERROR_CASLIB_CLOUD_QUOTA_IS_FULL = 3151; ///<�������
        public const int INS_ERROR_CASLIB_CLOUD_FILE_IS_FULL = 3152; ///<�ļ�����
        public const int INS_ERROR_CASLIB_CLIENT_BASE = 3200; ///<�ͻ��˴����
        public const int INS_ERROR_CASLIB_CLIENT_PARAMETER = 3201; ///<��������
        public const int INS_ERROR_CASLIB_CLIENT_ALLOC_RESOURCE = 3202; ///<������Դʧ��
        public const int INS_ERROR_CASLIB_CLIENT_SEND_FAILED = 3203; ///<���ʹ���
        public const int INS_ERROR_CASLIB_CLIENT_RECV_FAILED = 3204; ///<���մ���
        public const int INS_ERROR_CASLIB_CLIENT_PARSE_XML = 3205; ///<�������Ĵ���
        public const int INS_ERROR_CASLIB_CLIENT_CREATE_XML = 3206; ///<���ɱ��Ĵ���
        public const int INS_ERROR_CASLIB_CLIENT_INIT_SOCKET = 3207; ///<��ʼ��Socketʧ��
        public const int INS_ERROR_CASLIB_CLIENT_CREATE_SOCKET = 3208; ///<����socketʧ��
        public const int INS_ERROR_CASLIB_CLIENT_CONNECT_FAILED = 3209; ///<���ӷ�����ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_NO_INIT = 3210; ///<CASLIB.dll not init
        public const int INS_ERROR_CASLIB_CLIENT_OVER_MAX_SESSION = 3211; ///<����CASCLIENT��֧�ֵ������
        public const int INS_ERROR_CASLIB_CLIENT_SENDTIMEOUT = 3212; ///<����ͳ�ʱ
        public const int INS_ERROR_CASLIB_CLIENT_RECV_TIMEOUT = 3213; ///<������ճ�ʱ
        public const int INS_ERROR_CASLIB_CLIENT_CREATE_PACKET = 3214; ///<create data packet failed
        public const int INS_ERROR_CASLIB_CLIENT_PARSE_PACKET = 3215; ///<�������ݰ�����
        public const int INS_ERROR_CASLIB_CLIENT_FORCE_STOP = 3216; ///<�û���;ǿ���˳�
        public const int INS_ERROR_CASLIB_CLIENT_GETPORT_FAILED = 3217; ///<��ȡ���ض˿ڴ���
        public const int INS_ERROR_CASLIB_CLIENT_BASE64_ENCODE = 3218; ///<base64�������
        public const int INS_ERROR_CASLIB_CLIENT_BASE64_DECODE = 3219; ///<base64 decode failed
        public const int INS_ERROR_CASLIB_CLIENT_RECV_DATAERROR = 3220; ///<�������ݴ���
        public const int INS_ERROR_CASLIB_CLIENT_AES_ENCRYPT_FAILED = 3221; ///<AES���ܳ���
        public const int INS_ERROR_CASLIB_CLIENT_AES_DECRYPT_FAILED = 3222; ///<AES���ܳ���
        public const int INS_ERROR_CASLIB_CLIENT_OPERATION_UNSUPPORTED = 3223; ///<��֧�ֵĲ���
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_P2P_FAILED = 3224; ///<p2p��ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_SEND_KEEPLIVE_FAILED = 3225; ///<���ʹ򶴰�ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_INIT_SSL = 3228; ///<��ʼ��sslʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_CONNECT_SSL = 3229; ///<ssl����ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_OTHER_ERROR = 3249; ///<��֤����������
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_DB_ERROR = 3250; ///<��֤�����ݿ����
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_PARAMS_ERROR = 3251; ///<��֤�Ĳ�������
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_EXEC_ERROR = 3252; ///<��֤��ִ���쳣
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_SESSION_ERROR = 3253; ///<��֤��session������
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_CACHE_ERROR = 3254; ///<��֤�Ļ����쳣
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_AUTH_NONE = 3255; ///<��֤����Ȩ��



        //reason:


        public const int INS_ERROR_CASLIB_CLIENT_NO_VALID_PRELINK = 3290; ///< û�п��õ�P2PԤ���ӣ���֮ǰ�����������3045����������
        public const int INS_ERROR_CASLIB_CLIENT_NO_INNER_RESOURCE = 3291; ///< ��·ֱ��/P2P֮�󣬵���·ֱ���豸���صĴ�����
        public const int INS_ERROR_CASLIB_CLIENT_NO_P2P_RESOURCE = 3255; ///<��֤����Ȩ��





#endregion


    }

    internal static class DefineConstants
    {
        public const string INS_EXECUTION_PROCESS_ADDTASK = "7_1"; ///< ���񱻳ɹ����������"
        public const string INS_EXECUTION_PROCESS_TASKDO = "7_2"; ///< ����ʼ��ִ��"
        public const string INS_EXECUTION_PROCESS_PARSE_PARAM = "7_3"; ///< ����������Ϣ"
        public const string INS_EXECUTION_PROCESS_LOCAL = "7_4"; ///< :�жϾ�����"
        public const string INS_EXECUTION_PROCESS_CHECKPWD = "7_5"; ///< :��֤�豸����"
        public const string INS_EXECUTION_PROCESS_NETTYPE = "7_5"; ///< :ѡ��ȡ����ʽ"
        public const string INS_EXECUTION_PROCESS_PSIA = "7_6_1"; ///<:����ʹ��PSIA��ʽȡ��"
        public const string INS_EXECUTION_PROCESS_P2P = "7_6_2"; ///<:����ʹ��P2P��ʽȡ��"
        public const string INS_EXECUTION_PROCESS_VTDU = "7_6_3"; ///<:����ʹ��VTDU��ʽȡ��"
        public const string INS_EXECUTION_PROCESS_TASKEND = "7_7_1"; ///<:����ȡ��ʧ�ܣ�����ȡ��"
        public const string INS_EXECUTION_PROCESS_TASKEND_SUCC = "7_7_2"; ///<:����ȡ���ɹ�������������ȴ�����������"
        public const string INS_EXECUTION_PROCESS_NEXT = "7_8"; ///<:����ȡ��ʧ�ܣ��л�ȡ����ʽ"
        public const string INS_EXECUTION_PROCESS_PSIA_PORT = "7_9"; ///<:��ȡ����PSIAȡ���˿�"
        public const int INS_ERROR_NOERROR = 0; ///<�޴���
        public const int INS_ERROR_LOAD_RTSP_FAILED = 1; ///<����rtsp��ʧ��
        public const int INS_ERROR_LOAD_PLAYCTRL_FAILED = 2; ///<���ز��ſ�ʧ��
        public const int INS_ERROR_LOAD_SYSTRANSFORM_FAILED = 3; ///<����ת��װ��ʧ��
        public const int INS_ERROR_LOAD_HTTPCLIENT_FAILED = 4; ///<����http��ʧ��
        public const int INS_ERROR_PARAMETER_ERROR = 5; ///<��������
        public const int INS_ERROR_ORDER_ERROR = 6; ///<����˳�����
        public const int INS_ERROR_ALLOC_RESOURCE_FAILED = 7; ///<������Դʧ��
        public const int INS_ERROR_NOT_INITLIB = 8; ///<û�г�ʼ��
        public const int INS_ERROR_OPERTION_NOSUPPORT = 9; ///<������֧��
        public const int INS_ERROR_OPENFILE_ERROR = 10; ///<���ļ�ʧ��
        public const int INS_ERROR_WRITEFILE_ERROR = 11; ///<д�ļ�ʧ��
        public const int INS_ERROR_READFILE_ERROR = 12; ///<���ļ�ʧ��
        public const int INS_ERROR_INIT_HPR_FAILED = 13; ///<��ʼ��hpr��ʧ��
        public const int INS_ERROR_AUDIO_MONOPOLIZED = 14; ///<��������ռ
        public const int INS_ERROR_CREATE_SOCKET_ERROR = 15; ///<����socketʧ��
        public const int INS_ERROR_NETWORK_CONNECT_FAILED = 16; ///<����ʧ��
        public const int INS_ERROR_NETWORK_SEND_ERROR = 17; ///<����ʧ��
        public const int INS_ERROR_NETWORK_RECV_ERROR = 18; ///<����ʧ��
        public const int INS_ERROR_NETWORK_SEND_TIMEOUT = 19; ///<���ͳ�ʱ
        public const int INS_ERROR_NETWORK_RECV_TIMEOUT = 20; ///<���ճ�ʱ
        public const int INS_ERROR_NETWORK_RESOLVE_FAILED = 21; ///<������������
        public const int INS_ERROR_XML_PARSE_ERROR = 22; ///<xml��������
        public const int INS_ERROR_XML_NODE_ERROR = 23; ///<xml������
        public const int INS_ERROR_NO_EXCEL_DRIVER_ERROR = 24; ///<û�а�װExcel����
        public const int INS_ERROR_PARSE_URL_FAILED = 25; ///<URL����ʧ��
        public const int INS_ERROR_LOADRTSPSDKPROC_ERROR = 26; ///<�Ҳ���rtsp�ӿڵ�ַ
        public const int INS_ERROR_LOADPLAYERSDKPROC_ERROR = 27; ///<�Ҳ������ſ�ӿڵ�ַ
        public const int INS_ERROR_LOADSYSTRANSFORMPROC_ERROR = 28; ///<�Ҳ���ת��װ��ӿڵ�ַ
        public const int INS_ERROR_LOADHTTPSDKPROC_ERROR = 29; ///<�Ҳ���http��ӿڵ�ַ
        public const int INS_ERROR_START_WAVEIN_FAILED = 30; ///<��ʼ��Ƶ�ɼ�ʧ��
        public const int INS_ERROR_START_WAVEOUT_FAILED = 31; ///<��ʼ��Ƶ����ʧ��
        public const int INS_ERROR_INIT_G722_CODEC_FAILED = 32; ///<��ʼ��G722�����ʧ��
        public const int INS_ERROR_NOT_ENOUGH_DISK_FREESPACE = 33; ///<���̿ռ䲻��
        public const int INS_ERROR_FILE_ALREADY_EXIST = 34; ///<�ļ��Ѵ���
        public const int INS_ERROR_LOAD_PPV_FAILED = 35; ///<����ppv��ʧ��
        public const int INS_ERROR_LOADPPVSDKPROC_ERROR = 36; ///<�Ҳ���libPPVClient�ӿڵ�ַ
        public const int INS_ERROR_LOAD_STUN_FAILED = 37; ///<����stun��ʧ��
        public const int INS_ERROR_LOADSTUNSDKPROC_ERROR = 38; ///<�Ҳ���StunClientLib�ӿڵ�ַ
        public const int INS_ERROR_RECORD_FILE_NOT_EXIST = 39; ///< �Ҳ���ָ��ʱ�䷶Χ�ڵ�¼���ļ�
        public const int INS_ERROR_FAILED_RTSP_PORT = 40; ///< ͨ��PSIAȡ������ȡRTSP�˿�ʧ��
        public const int INS_ERROR_SSL_CONNECT_FAILED = 41; ///<SSL��ʽ����ʧ��
        public const int INS_ERROR_JSON_PARSE_ERROR = 42; ///<JSON����ʧ��
        public const int INS_ERROR_LOADCASSDKPROC_ERROR = 43; ///<�Ҳ���libCASClient�ӿڵ�ַ
        public const int INS_ERROR_LOAD_CAS_FAILED = 44; ///<����CASClient��ʧ��
        public const int INS_ERROR_OPERATIONCODE_FAILED = 45; ///<��ȡ������ʧ��
        public const int INS_ERROR_LOAD_GETHDSIGN_FAILED = 46; ///<����libGetHDSign��ʧ��
        public const int INS_ERROR_HDSIGN_FAILED = 47; ///<��ȡӲ��������ʧ��
        public const int INS_ERROR_NULL_ADDRESS = 48; ///<GetIpAddress����IPΪ��
        public const int INS_ERROR_RTSP_INIT_FAILED = 50;
        public const int INS_ERROR_RTSP_NOT_INIT = 51;
        public const int INS_ERROR_RTSP_PARSE_ERROR = 52;
        public const int INS_ERROR_RTSP_STATUS_ERROR = 53;
        public const int INS_ERROR_RTSP_INIT_RTP_ERROR = 54;
        public const int INS_ERROR_RTSP_CREATE_SOCKET_ERROR = 55;
        public const int INS_ERROR_RTSP_CONNECT_FAILED = 56;
        public const int INS_ERROR_RTSP_HTTP_GET_ERROR = 57;
        public const int INS_ERROR_RTSP_HTTP_POST_ERROR = 58;
        public const int INS_ERROR_RTSP_GETPORT_FAILED = 59;
        public const int INS_ERROR_PLAYM4_NOERROR = 100; ///<no error
        public const int INS_ERROR_PLAYM4_PARA_OVER = 101; ///<input parameter is invalid;
        public const int INS_ERROR_PLAYM4_ORDER_ERROR = 102; ///<The order of the function to be called is error.
        public const int INS_ERROR_PLAYM4_TIMER_ERROR = 103; ///<Create multimedia clock failed;
        public const int INS_ERROR_PLAYM4_DEC_VIDEO_ERROR = 104; ///<Decode video data failed.
        public const int INS_ERROR_PLAYM4_DEC_AUDIO_ERROR = 105; ///<Decode audio data failed.
        public const int INS_ERROR_PLAYM4_ALLOC_MEMORY_ERROR = 106; ///<Allocate memory failed.
        public const int INS_ERROR_PLAYM4_OPEN_FILE_ERROR = 107; ///<Open the file failed.
        public const int INS_ERROR_PLAYM4_CREATE_OBJ_ERROR = 108; ///<Create thread or event failed
        public const int INS_ERROR_PLAYM4_CREATE_DDRAW_ERROR = 109; ///<Create DirectDraw object failed.
        public const int INS_ERROR_PLAYM4_CREATE_OFFSCREEN_ERROR = 110; ///<failed when creating off-screen surface.
        public const int INS_ERROR_PLAYM4_BUF_OVER = 111; ///<buffer is overflow
        public const int INS_ERROR_PLAYM4_CREATE_SOUND_ERROR = 112; ///<failed when creating audio device.
        public const int INS_ERROR_PLAYM4_SET_VOLUME_ERROR = 113; ///<Set volume failed
        public const int INS_ERROR_PLAYM4_SUPPORT_FILE_ONLY = 114; ///<The function only support play file.
        public const int INS_ERROR_PLAYM4_SUPPORT_STREAM_ONLY = 115; ///<The function only support play stream.
        public const int INS_ERROR_PLAYM4_SYS_NOT_SUPPORT = 116; ///<System not support.
        public const int INS_ERROR_PLAYM4_FILEHEADER_UNKNOWN = 117; ///<No file header.
        public const int INS_ERROR_PLAYM4_VERSION_INCORRECT = 118; ///<The version of decoder and encoder is not adapted.
        public const int INS_ERROR_PLAYM4_INIT_DECODER_ERROR = 119; ///<Initialize decoder failed.
        public const int INS_ERROR_PLAYM4_CHECK_FILE_ERROR = 120; ///<The file data is unknown.
        public const int INS_ERROR_PLAYM4_INIT_TIMER_ERROR = 121; ///<Initialize multimedia clock failed.
        public const int INS_ERROR_PLAYM4_BLT_ERROR = 122; ///<Blt failed.
        public const int INS_ERROR_PLAYM4_UPDATE_ERROR = 123; ///<Update failed.
        public const int INS_ERROR_PLAYM4_OPEN_FILE_ERROR_MULTI = 124; ///<openfile error, streamtype is multi
        public const int INS_ERROR_PLAYM4_OPEN_FILE_ERROR_VIDEO = 125; ///<openfile error, streamtype is video
        public const int INS_ERROR_PLAYM4_JPEG_COMPRESS_ERROR = 126; ///<JPEG compress error
        public const int INS_ERROR_PLAYM4_EXTRACT_NOT_SUPPORT = 127; ///<Don't support the version of this file.
        public const int INS_ERROR_PLAYM4_EXTRACT_DATA_ERROR = 128; ///<extract video data failed.
        public const int INS_ERROR_PLAYM4_SECRET_KEY_ERROR = 129; ///<Secret key is error ///<add 20071218
        public const int INS_ERROR_PLAYM4_DECODE_KEYFRAME_ERROR = 130; ///< ����ؼ�֡ʧ��
        public const int INS_ERROR_PLAYM4_NEED_MORE_DATA = 131; ///< ���ݲ���
        public const int INS_ERROR_PLAYM4_INVALID_PORT = 132; ///< ��Ч�˿ں�
        public const int INS_ERROR_PLAYM4_NOT_FIND = 133; ///< ����ʧ��
        public const int INS_ERROR_PLAYM4_FAIL_UNKNOWN = 199; ///< δ֪�Ĵ���
        public const int INS_ERROR_PWD_ERROR = 401; //�����
        public const int INS_ERROR_FORMAT_ERROR = 501; //��ʽ����
        public const int INS_ERROR_FORMATING = 400; //��ʽ����
        public const int INS_ERROR_STREAM_LIMIT = 410; //ȡ��·������
        public const int INS_ERROR_NO_RECORD = 402; //��ʱ��ط���¼��
        public const int INS_ERROR_EDEFENSE_NO_SUPPORT = 402; //��֧�ֲ���
        public const int INS_ERROR_FAILED = 500; //����ִ��ʧ��
        public const int INS_ERROR_DEV_NOT_ONLINE = 801; //�豸������
        public const int INS_ERROR_PRIVACY = 409; //��˽����״̬
        public const int INS_ERROR_V17_COMMON_BASE = 2000;
        public const int INS_ERROR_V17_GET_OPERATIONCODE_ERROR = 2001; ///<��ȡ������ʧ��
        public const int INS_ERROR_V17_GET_TOKEN_ERROR = 2002; ///<��ȡȡ��tokenʧ��
        public const int INS_ERROR_V17_SETAUDIOINCALLBACK_ERROR = 2003; ///<��������ɼ��ص�����ʧ��
        public const int INS_ERROR_V17_STARTPLAY_ERROR = 2004; ///<�����������ʧ��
        public const int INS_ERROR_V17_STARTAUDIOIN_ERROR = 2005; ///<����ɼ��������ʧ��
        public const int INS_ERROR_V17_TTS_CREATETALK_ERROR = 2006; ///<tts TALK_VTDU CreateTalk error
        public const int INS_ERROR_V17_TTS_SETDATACALLBACK_ERROR = 2007; ///<tts TALK_VTDU SetDataCallBack error
        public const int INS_ERROR_V17_TTS_SETMSGCALLBACK_ERROR = 2008; ///<tts TALK_VTDU SetMsgCallBack error
        public const int INS_ERROR_V17_TTS_STARTTALK_ERROR = 2009; ///<tts TALK_VTDU StartTalk error
        public const int INS_ERROR_V17_CAS_CREATESESSION_ERROR = 2010; ///<cas TALK_TCP/TALK_UDP CreateSession error
        public const int INS_ERROR_V17_CAS_VOICETALKSTART_ERROR = 2011; ///<cas TALK_TCP/TALK_UDP VoiceTalkStart error
        public const int INS_ERROR_V17_PERMANENTKEY_EXCEPTION = 2012; ///<����������Կ  1.������Կ������MD5��Կ�����
        public const int INS_ERROR_V17_VTDU_OPERKEY_ERROR = 2013; ///<VTDU���صĲ���������������Կ����
        public const int INS_ERROR_V17_STOPAUDIOIN_ERROR = 2014; ///<ֹͣ����ɼ�ʧ��
        public const int INS_ERROR_V17_STOPPLAY_ERROR = 2015; ///<ֹͣ�������ʧ��
        public const int INS_ERROR_V17_STOPAUDIOIN_SUCC = 2016; ///<ֹͣ����ɼ��ɹ�
        public const int INS_ERROR_V17_STOPPLAY_SUCC = 2017; ///<ֹͣ������ųɹ�
        public const int INS_ERROR_V17_VTDU_TIMEOUT = 2021; ///<��ý�����豸���ͻ�������ʱ
        public const int INS_ERROR_V17_VTDU_CKECK_TOKEN = 2022; ///<��֤tokenʧ��
        public const int INS_ERROR_V17_CLIENT_URL_ERROR = 2023; ///<�ͻ��˵�URL��ʽ����
        public const int INS_ERROR_V17_VTDU_CLIENT_TIMEOUT = 2025; ///<vtdu�ͻ��˽��ջ�Ӧ��ʱ
        public const int INS_ERROR_V17_STREAM_SESSION_ERROR = 2026; ///<ȡ��session����
        public const int INS_ERROR_V17_WAIT_HEADER_TIMEOUT = 2027; ///<�ȴ���ͷ��ʱ
        public const int INS_ERROR_V17_USER_STOP = 2028; ///<�û�ֹͣ
        public const int INS_ERROR_V17_DECRYPT_ERROR = 2029; ///<����ʧ��
        public const int INS_ERROR_V17_VTDU_CREATE_SESSION = 2031; ///<vtduȡ������sessionʧ��
        public const int INS_ERROR_V17_VTDU_DESTORY_SESSION = 2032; ///<vtduȡ������sessionʧ��
        public const int INS_ERROR_V17_VTDU_START = 2033; ///<vtduȡ����ʼʧ�ܣ�����-1ʱ����
        public const int INS_ERROR_V17_VTDU_STOP = 2034; ///<vtduȡ��ֹͣʧ��
        public const int INS_ERROR_V17_VTDU_PAUSE = 2035; ///<vtduȡ����ͣʧ��
        public const int INS_ERROR_V17_VTDU_RESUME = 2036; ///<vtduȡ���ָ�ʧ��
        public const int INS_ERROR_V17_VTDU_CHANGE_RATE = 2037; ///<vtduȡ���ı�����ʧ��
        public const int INS_ERROR_V17_VTDU_TOKEN_NO_AUTHORITY = 2044; ///<VTDU token��Ȩ��
        public const int INS_ERROR_V17_VTDU_SESSION_NO_EXIST = 2045; ///<VTDU session������
        public const int INS_ERROR_V17_VTDU_TOKEN_OTHER = 2046; ///<VTDU ��֤token�����쳣�������壩
        public const int INS_ERROR_V17_VTDU_TOKEN_NOCONNECT_VTM = 2047; ///<VTDU �ͻ������Ӳ���VTM
        public const int INS_ERROR_V17_VTDU_TOKEN_NOCONNECT_VTDU = 2048; ///<VTDU �ͻ������Ӳ���VTDU
        public const int INS_ERROR_V17_INVALID_DEVICE_CHANNAL = 2049; ///<�豸ͨ����
        public const int INS_ERROR_V17_DEVICE_UNSUPPORT_STREAMTYPE = 2050; ///<�豸��֧�ֵ���������
        public const int INS_ERROR_V17_DEVICE_UNCONNECT_VTDU = 2051; ///<�豸���Ӳ�����ý��
        public const int INS_ERROR_V17_CLIENT_ERROR_CASIP = 2052; ///<�ͻ��˸���cas��ַ��Ϣ����
        public const int INS_ERROR_V17_VIDEO_SHARED_TIMEEND = 2053; ///<��Ƶ����ʱ���Ѿ�����
        public const int INS_ERROR_V17_VTDU_RECV_HEADER_TIMEOUT = 2054; ///<��ý�������ͷ��ʱ8s
        public const int INS_ERROR_V17_VTDU_VTDU_IPARAMTYPE_ERROR = 2055; ///< Vtdu iParamType error
        public const int INS_ERROR_V17_GET_OPERATIONCODE_PARAMETER_ERROR = 2056; ///< ��ȡ�������������
        public const int INS_ERROR_V17_REGET_OPERATIONCODE_DEV_NOT_FOUND = 2057; ///< �޷��ҵ��豸
        public const int INS_ERROR_V17_GETTOKEN_URL_OR_CLIENTSESSION_NULL = 2058; ///< ��ȡToken��������
        public const int INS_ERROR_V17_GETTOKEN_EMPTY_TOKEN = 2059; ///< ��ȡ��TokenΪ��
        public const int INS_ERROR_V17_FIND_FILE_FAILED = 2060; ///< �Ҳ��������ļ�
        public const int INS_ERROR_PRIVATE_VTDU_CLN_ERR_CODE = 2200;
        public const int INS_ERROR_PRIVATE_VTDU_BAD_MSG = 2204; //������Ϣ�����Ƿ�
        public const int INS_ERROR_PRIVATE_VTDU_NO_ENOUGH_ROOM = 2205; //�ڴ���Դ����
        public const int INS_ERROR_PRIVATE_VTDU_INVALID_VTDU_HOST = 2208; //����vtm����vtdu��ַ���Ϸ�
        public const int INS_ERROR_PRIVATE_VTDU_INVALID_SSN_STREAMKEY = 2210; //����vtm���ػỰ��ʶ���Ȳ��Ϸ�
        public const int INS_ERROR_PRIVATE_VTDU_ALLOCATE_SOCKET_FAIL = 2222; //��ȡϵͳsocket��Դʧ��
        public const int INS_ERROR_PRIVATE_VTDU_CONNECT_SRV_FAIL = 2224; //���ӷ�����ʧ��
        public const int INS_ERROR_PRIVATE_VTDU_REQUEST_TIMEOUT = 2225; //�ͻ�������δ�յ������Ӧ��
        public const int INS_ERROR_PRIVATE_VTDU_DISCONNECTED_LINK = 2226; //��·�Ͽ�
        public const int NS_ERROR_PRIVATE_VTDU_ERR = 2201; //ͨ�ô��󷵻�
        public const int NS_ERROR_PRIVATE_VTDU_NULL_PTR = 2202; //���Ϊ��ָ��
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_PARAS = 2203; //���ֵ��Ч
        public const int NS_ERROR_PRIVATE_VTDU_BAD_MSG = 2204; //������Ϣ�����Ƿ�
        public const int NS_ERROR_PRIVATE_VTDU_NO_ENOUGH_ROOM = 2205; //�ڴ���Դ����
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_MSGHEAD = 2206; //Э���ʽ���Ի�����Ϣ�峤�ȳ���STREAM_MAX_MSGBODY_LEN
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_SERIAL = 2207; //�豸���кų��Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAMURL = 2208; //ȡ��url���Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_VTDU_HOST = 2209; //����vtm����vtdu��ַ���Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_PEER_HOST = 2210; //����vtm���ؼ���vtdu��ַ���Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_SSN_STREAMKEY = 2211; //����vtm���ػỰ��ʶ���Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAM_HEAD = 2212; //vtdu������ͷ���Ȳ��Ϸ�
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAM_SSN = 2213; //vtdu�Ự���ȷǷ�
        public const int NS_ERROR_PRIVATE_VTDU_DATAOUT_CALLBACK_UNREG = 2214; //�ص�����δע��
        public const int NS_ERROR_PRIVATE_VTDU_NO_STREAM_SSN = 2215; //vtdu�ɹ���ӦδЯ���Ự��ʶ
        public const int NS_ERROR_PRIVATE_VTDU_NO_STREAM_HEAD = 2216; //vtdu�ɹ���ӦδЯ����ͷ
        public const int NS_ERROR_PRIVATE_VTDU_NO_STREAM = 2217; //������������δʹ��
        public const int NS_ERROR_PRIVATE_VTDU_PB_PARSE_FAILURE = 2218; //������Ϣ��PB����ʧ��
        public const int NS_ERROR_PRIVATE_VTDU_PB_ENCAPSULATE_FAILURE = 2219; //������Ϣ��PB��װʧ��
        public const int NS_ERROR_PRIVATE_VTDU_MEMALLOC_FAIL = 2220; //����ϵͳ�ڴ���Դʧ��
        public const int NS_ERROR_PRIVATE_VTDU_VTDUSRV_NOT_SET = 2221; //vtdu��ַ��δ��ȡ��
        public const int NS_ERROR_PRIVATE_VTDU_NOT_SUPPORTED = 2222; //�ͻ�����δ֧��
        public const int NS_ERROR_PRIVATE_VTDU_ALLOCATE_SOCKET_FAIL = 2223; //��ȡϵͳsocket��Դʧ��
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STREAM_SSN_ID = 2224; //�ϲ�����StreamSsnId��ƥ��
        public const int NS_ERROR_PRIVATE_VTDU_CONNECT_SRV_FAIL = 2225; //���ӷ�����ʧ��
        public const int NS_ERROR_PRIVATE_VTDU_REQUEST_TIMEOUT = 2226; //�ͻ�������δ�յ������Ӧ��
        public const int NS_ERROR_PRIVATE_VTDU_DISCONNECTED_LINK = 2227; //��·�Ͽ�
        public const int NS_ERROR_PRIVATE_VTDU_NO_CONNECTION = 2228; //û��ȡ������
        public const int NS_ERROR_PRIVATE_VTDU_STREAM_END_SUCC = 2229; //���ɹ�ֹͣ
        public const int NS_ERROR_PRIVATE_VTDU_STREAM_DATAKEY_CHECK_FAIL = 2230; //�ͻ��˷�����У��ʧ��
        public const int NS_ERROR_PRIVATE_VTDU_TCP_BUFFER_FULL = 2231; //Ӧ�ò�tcpճ�������������
        public const int NS_ERROR_PRIVATE_VTDU_INVALID_STATUS_CHANGE = 2232; //��Ч״̬Ǩ��
        public const int NS_ERROR_PRIVATE_VTDU_BAD_STATUS = 2233; //��Ч�ͻ���״̬
        public const int NS_ERROR_PRIVATE_VTDU_VTM_VTDUINFO_REQ_TMOUT = 2234; //��vtmȡ����ý����Ϣ����ʱ
        public const int NS_ERROR_PRIVATE_VTDU_PROXY_STARTSTREAM_REQ_TMOUT = 2235; //�����ȡ������ʱ
        public const int NS_ERROR_PRIVATE_VTDU_PROXY_KEEPALIVE_REQ_TMOUT = 2236; //��������ȡ������ʱ
        public const int NS_ERROR_PRIVATE_VTDU_STARTSTREAM_REQ_TMOUT = 2237; //��vtduȡ������ʱ
        public const int NS_ERROR_PRIVATE_VTDU_KEEPALIVE_REQ_TMOUT = 2238; //��vtdu����ȡ������ʱ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_402 = 2602; //�ط��ڲ���¼���ļ�
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_403 = 2603; //�������������Կ���豸��ƥ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_404 = 2604; //�豸������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_405 = 2605; //��ý�����豸���ͻ�������ʱ/cas��Ӧ��ʱ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_406 = 2606; //tokenʧЧ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_407 = 2607; //�ͻ��˵�URL��ʽ����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_409 = 2609; //Ԥ��������˽����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_410 = 2610; //�豸�ﵽ���������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_411 = 2611; //token��Ȩ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_412 = 2612; //session������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_413 = 2613; //��֤token�������쳣�������壩
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_415 = 2615; //�豸ͨ����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_451 = 2651; //�豸��֧�ֵ���������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_452 = 2652; //�豸����Ԥ����ý�������ʧ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_454 = 2654; //��Ƶ����ʱ���Ѿ�����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_491 = 2691; //��ͬ�������ڴ�����ܾ����δ���
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_500 = 2700; //��ý��������ڲ��������
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_503 = 2703; //vtm����vtdu������ʧ��
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_544 = 2744; //�豸��������ƵԴ
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_545 = 2745; //��Ƶ����ʱ���Ѿ�����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_546 = 2746; //VTDUȡ������2·����
        public const int INS_ERROR_PRIVATE_VTDU_STATUS_556 = 2756; //ticketУ��ʧ��
        public const int INS_ERROR_CASLIB_BASE = 3000;
        public const int INS_ERROR_CASLIB_MSG_UNKNOW_ERROR = 3001; ///<δ֪����
        public const int INS_ERROR_CASLIB_MSG_PARAMS_ERROR = 3002; ///<���Ĳ�������
        public const int INS_ERROR_CASLIB_MSG_PARSE_FAILED = 3003; ///<���Ľ�������
        public const int INS_ERROR_CASLIB_MSG_COMMAND_UNKNOW = 3006; ///<�Ƿ�����
        public const int INS_ERROR_CASLIB_MSG_COMMAND_NO_LONGER_SUPPORTED = 3007; ///<��ʱ����
        public const int INS_ERROR_CASLIB_MSG_COMMAND_NOT_SUITABLE = 3008; ///<��������
        public const int INS_ERROR_CASLIB_MSG_CHECKSUM_ERROR = 3011; ///<У�������
        public const int INS_ERROR_CASLIB_MSG_VERSION_UNKNOW = 3016; ///<Э��汾����
        public const int INS_ERROR_CASLIB_MSG_VERSION_NO_LONGER_SUPPORTED = 3017; ///<Э��汾����
        public const int INS_ERROR_CASLIB_MSG_VERSION_FORBIDDEN = 3018; ///<Э��汾����ֹ
        public const int INS_ERROR_CASLIB_MSG_SERIAL_NOT_FOR_CIVIL = 3021; ///<���кŽ���ʧ��
        public const int INS_ERROR_CASLIB_MSG_SERIAL_FORBIDDEN = 3022; ///<���кű���ֹ
        public const int INS_ERROR_CASLIB_MSG_SERIAL_DUPLICATE = 3023; ///<���к��ظ�
        public const int INS_ERROR_CASLIB_MSG_SERIAL_FLUSHED_IN_A_SECOND = 3024; ///<��ͬ���кŶ�ʱ���ڴ����ظ�����
        public const int INS_ERROR_CASLIB_MSG_SERIAL_NO_LONGER_SUPPORTED = 3025; ///<���кŲ���֧��
        public const int INS_ERROR_CASLIB_MSG_LOCAL_SERVER_BUSY = 3031; ///<�����޷���Ӧ
        public const int INS_ERROR_CASLIB_MSG_LOCAL_SERVER_REFUSED = 3032; ///<���������ܾ�
        public const int INS_ERROR_CASLIB_REG_CANNOT_AFFORD_PU = 3033; ///<�޷���������
        public const int INS_ERROR_CASLIB_REG_CRYPTO_UNMATCHED = 3034; ///<�豸�����㷨��ƥ��
        public const int INS_ERROR_CASLIB_MSG_DEV_TYPE_INVAILED = 3036; ///<�豸���ʹ���
        public const int INS_ERROR_CASLIB_MSG_DEV_TYPE_NO_LONGGER_SUPPORTED = 3037; ///<�豸���Ͳ���֧��
        public const int INS_ERROR_CASLIB_MSG_PU_BUSY = 3041; ///<�豸�޷���Ӧ
        public const int INS_ERROR_CASLIB_MSG_OPERATION_FAILED = 3042; ///<���������
        public const int INS_ERROR_CASLIB_PU_NO_CRYPTO_FOUND = 3043; ///<�豸��ƽ̨δ�ҵ���Ӧ�ļ����㷨
        public const int INS_ERROR_CASLIB_MSG_PU_REFUSED = 3044; ///<�ܾ�
        public const int INS_ERROR_CASLIB_MSG_PU_NO_RESOURCE = 3045; ///<û�п�����Դ     �豸��������
        public const int INS_ERROR_CASLIB_MSG_PU_CHANNEL_ERROR = 3046; ///<ͨ����
        public const int INS_ERROR_CASLIB_SYSTEM_COMMAND_PU_COMMAND_UNSUPPORTED = 3047; ///<��֧�ֵ�����
        public const int INS_ERROR_CASLIB_SYSTEM_COMMAND_PU_NO_RIGHTS_TO_DO_COMMAND = 3048; ///<û��Ȩ��
        public const int INS_ERROR_CASLIB_MSG_NO_SESSION_FOUND = 3049; ///<û���ҵ��Ự
        public const int INS_ERROR_CASLIB_PREVIEW_CHANNEL_BUSY = 3051; ///<��ͨ�����ڷ���
        public const int INS_ERROR_CASLIB_PREVIEW_CLIENT_BUSY = 3052; ///<ȡ����ַ�ظ�
        public const int INS_ERROR_CASLIB_PREVIEW_STREAM_UNSUPPORTED = 3053; ///<��֧�ֵ���������
        public const int INS_ERROR_CASLIB_PREVIEW_TRANSPORT_UNSUPPORTED = 3054; ///<��֧�ֵĴ��䷽ʽ
        public const int INS_ERROR_CASLIB_PREVIEW_CONNECT_SERVER_FAIL = 3055; ///<����Ԥ����ý�������ʧ�� +
        public const int INS_ERROR_CASLIB_PREVIEW_QUERY_WLAN_INFO_FAIL = 3056; ///<��ѯ�豸�������ڵ�ַʧ��
        public const int INS_ERROR_CASLIB_RECORD_SEARCH_START_TIME_ERROR = 3061; ///<����¼��ʼʱ���
        public const int INS_ERROR_CASLIB_RECORD_SEARCH_STOP_TIME_ERROR = 3062; ///<����¼�����ʱ���
        public const int INS_ERROR_CASLIB_RECORD_SEARCH_FAIL = 3063; ///<����¼��ʧ��    +
        public const int INS_ERROR_CASLIB_PLAYBACK_TYPE_UNSUPPORTED = 3066; ///<��֧�ֵĻط�����
        public const int INS_ERROR_CASLIB_PLAYBACK_NO_FILE_MATCHED = 3067; ///<û���ҵ��ļ�
        public const int INS_ERROR_CASLIB_PLAYBACK_START_TIME_ERROR = 3068; ///<��ʼʱ�����
        public const int INS_ERROR_CASLIB_PLAYBACK_STOP_TIME_ERROR = 3069; ///<����Ľ���ʱ��
        public const int INS_ERROR_CASLIB_PLAYBACK_NO_FILE_FOUND = 3070; ///<��ʱ�����û��¼��
        public const int INS_ERROR_CASLIB_PLAYBACK_CONNECT_SERVER_FAIL = 3071; ///<���ӻطŷ�������ʧ��
        public const int INS_ERROR_CASLIB_TALK_ENCODE_TYPE_UNSUPPORTED = 3076; ///<��֧�ֵ������������
        public const int INS_ERROR_CASLIB_TALK_CHANNEL_BUSY = 3077; ///<��ͨ�����ڶԽ�
        public const int INS_ERROR_CASLIB_TALK_CLIENT_BUSY = 3078; ///<��Ŀ�ĵ�ַ��������
        public const int INS_ERROR_CASLIB_TALK_UNSUPPORTED = 3079; ///<not support talk
        public const int INS_ERROR_CASLIB_TALK_CHANNO_ERROR = 3080; ///<ͨ���Ŵ���
        public const int INS_ERROR_CASLIB_TALK_CONNECT_SERVER_FAILED = 3081; ///<�������������ʧ��
        public const int INS_ERROR_CASLIB_TALK_CONNECT_REFUSED = 3082; ///<�豸�ܾ�
        public const int INS_ERROR_CASLIB_TALK_CONNECT_CAPACITY_LIMITED = 3083; ///<�豸��Դ����
        public const int INS_ERROR_CASLIB_FORMAT_NO_LOCAL_STORAGE = 3086; ///<û�б��ش洢
        public const int INS_ERROR_CASLIB_FORMAT_FORMATING = 3087; ///<���ڸ�ʽ����
        public const int INS_ERROR_CASLIB_FORMAT_FAILED = 3088; ///<���Ը�ʽ��ʧ��
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_REFUSED = 3091; ///<�������ܾ��豸��������
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_VERSION_NOT_FOUND = 3092; ///<û���ҵ�����汾
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_UNNEEDED = 3093; ///<����Ҫ����
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_NO_SERVER_ONLINE = 3094; ///<û����������������
        public const int INS_ERROR_CASLIB_UPGRADE_PU_REQUEST_ALL_SERVER_BUSY = 3095; ///<�����������������ﵽ�����
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRADING = 3101; ///<�����������
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRAD_FAILED = 3102; ///<����ʧ�ܣ�����δ֪����
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRAD_WRITE_FLASH_FAILED = 3103; ///<����дFlashʧ��
        public const int INS_ERROR_CASLIB_UPGRADE_PU_UPGRAD_LANGUAGE_DISMATCH = 3104; ///<�������Բ�ƥ��
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_NO_USER_MATHCED = 3106; ///<�������ʧ�ܣ�û�ж�Ӧ�û�
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_ORIGINAL_PASSWORD_ERROR = 3107; ///<�������ʧ�ܣ�ԭʼ�������
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_NEW_PASSWORD_DECRYPTE_FAILED = 3108; ///<�������ʧ�ܣ����������ʧ��
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_NEW_PASSWORD_CHECK_FAILED = 3109; ///<�������ʧ�ܣ������벻���Ϲ���
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_WRITE_FLASH_FAILED = 3110; ///<��������ʧ�ܣ�дflashʧ��
        public const int INS_ERROR_CASLIB_PU_PASSWORD_UPDATE_OTHER_FALIURE = 3111; ///<��������ʧ�ܣ�����ԭ��
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_NO_PU_FOUNDED = 3121; ///<������豸������
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_REFUSED_TO_PROTECT_PU = 3122; ///<Ϊ�˱����豸���ܾ�����
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_PU_LIMIT_REACHED = 3123; ///<�豸�ﵽ���ӵĿͻ�������
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_TEARDOWN_PU_CONNECTION = 3124; ///<Ҫ��ͻ��˶Ͽ����豸����
        public const int INS_ERROR_CASLIB_PU_REFUSE_CLIENT_CONNECTION = 3125; ///<�豸�ܾ�ƽ̨���͵Ŀͻ�����������
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_VERIFY_AUTH_ERROR = 3126; ///<CAS����֤������֤�û�Ȩ��ʧ��
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_REQUEST_PU_OPEN_PRIVACY = 3127; ///<�豸������˽����
        public const int INS_ERROR_CASLIB_PLATFORM_CLIENT_NO_SIGN_RELEATED = 3128; ///</<û�й���������
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_UNSUPPORTED = 3131; ///<��֧�ֵĲ���������
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_FAILED = 3132; ///</<������ʧ��
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_FORCE_FAILED = 3133; ///</<ǿ�Ʋ�����ʧ��
        public const int INS_ERROR_CASLIB_DEFENCE_TYPE_NEED_FORCE = 3134; ///</<��Ҫǿ�Ʋ�����
        public const int INS_ERROR_CASLIB_CLOUD_NOT_FOUND = 3141; ///<û���ҵ��ƴ洢������
        public const int INS_ERROR_CASLIB_CLOUD_NO_USER = 3142; ///<�û�δ��ͨ�ƴ洢
        public const int INS_ERROR_CASLIB_CLOUD_FILE_TAIL_REACHED = 3145; ///<�ļ��ѵ���β
        public const int INS_ERROR_CASLIB_CLOUD_INVALID_SESSION = 3146; ///<��Ч��session
        public const int INS_ERROR_CASLIB_CLOUD_INVALID_HANDLE = 3147; ///<��Ч���ļ�
        public const int INS_ERROR_CASLIB_CLOUD_UNKNOWN_CLOUD = 3148; ///<δ֪���ƴ洢����
        public const int INS_ERROR_CASLIB_CLOUD_UNSUPPORT_FILETYPE = 3149; ///<��֧�ֵ��ļ�����
        public const int INS_ERROR_CASLIB_CLOUD_INVALID_FILE = 3150; ///<��Ч���ļ�
        public const int INS_ERROR_CASLIB_CLOUD_QUOTA_IS_FULL = 3151; ///<�������
        public const int INS_ERROR_CASLIB_CLOUD_FILE_IS_FULL = 3152; ///<�ļ�����
        public const int INS_ERROR_CASLIB_CLIENT_BASE = 3200; ///<�ͻ��˴����
        public const int INS_ERROR_CASLIB_CLIENT_PARAMETER = 3201; ///<��������
        public const int INS_ERROR_CASLIB_CLIENT_ALLOC_RESOURCE = 3202; ///<������Դʧ��
        public const int INS_ERROR_CASLIB_CLIENT_SEND_FAILED = 3203; ///<���ʹ���
        public const int INS_ERROR_CASLIB_CLIENT_RECV_FAILED = 3204; ///<���մ���
        public const int INS_ERROR_CASLIB_CLIENT_PARSE_XML = 3205; ///<�������Ĵ���
        public const int INS_ERROR_CASLIB_CLIENT_CREATE_XML = 3206; ///<���ɱ��Ĵ���
        public const int INS_ERROR_CASLIB_CLIENT_INIT_SOCKET = 3207; ///<��ʼ��Socketʧ��
        public const int INS_ERROR_CASLIB_CLIENT_CREATE_SOCKET = 3208; ///<����socketʧ��
        public const int INS_ERROR_CASLIB_CLIENT_CONNECT_FAILED = 3209; ///<���ӷ�����ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_NO_INIT = 3210; ///<CASLIB.dll not init
        public const int INS_ERROR_CASLIB_CLIENT_OVER_MAX_SESSION = 3211; ///<����CASCLIENT��֧�ֵ������
        public const int INS_ERROR_CASLIB_CLIENT_SENDTIMEOUT = 3212; ///<����ͳ�ʱ
        public const int INS_ERROR_CASLIB_CLIENT_RECV_TIMEOUT = 3213; ///<������ճ�ʱ
        public const int INS_ERROR_CASLIB_CLIENT_CREATE_PACKET = 3214; ///<create data packet failed
        public const int INS_ERROR_CASLIB_CLIENT_PARSE_PACKET = 3215; ///<�������ݰ�����
        public const int INS_ERROR_CASLIB_CLIENT_FORCE_STOP = 3216; ///<�û���;ǿ���˳�
        public const int INS_ERROR_CASLIB_CLIENT_GETPORT_FAILED = 3217; ///<��ȡ���ض˿ڴ���
        public const int INS_ERROR_CASLIB_CLIENT_BASE64_ENCODE = 3218; ///<base64�������
        public const int INS_ERROR_CASLIB_CLIENT_BASE64_DECODE = 3219; ///<base64 decode failed
        public const int INS_ERROR_CASLIB_CLIENT_RECV_DATAERROR = 3220; ///<�������ݴ���
        public const int INS_ERROR_CASLIB_CLIENT_AES_ENCRYPT_FAILED = 3221; ///<AES���ܳ���
        public const int INS_ERROR_CASLIB_CLIENT_AES_DECRYPT_FAILED = 3222; ///<AES���ܳ���
        public const int INS_ERROR_CASLIB_CLIENT_OPERATION_UNSUPPORTED = 3223; ///<��֧�ֵĲ���
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_P2P_FAILED = 3224; ///<p2p��ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_SEND_KEEPLIVE_FAILED = 3225; ///<���ʹ򶴰�ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_INIT_SSL = 3228; ///<��ʼ��sslʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_CONNECT_SSL = 3229; ///<ssl����ʧ��
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_OTHER_ERROR = 3249; ///<��֤����������
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_DB_ERROR = 3250; ///<��֤�����ݿ����
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_PARAMS_ERROR = 3251; ///<��֤�Ĳ�������
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_EXEC_ERROR = 3252; ///<��֤��ִ���쳣
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_SESSION_ERROR = 3253; ///<��֤��session������
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_CACHE_ERROR = 3254; ///<��֤�Ļ����쳣
        public const int INS_ERROR_CASLIB_CLIENT_ERROR_VERIFY_AUTH_NONE = 3255; ///<��֤����Ȩ��
        public const int INS_ERROR_CASLIB_CLIENT_NO_VALID_PRELINK = 3290; ///< û�п��õ�P2PԤ���ӣ���֮ǰ�����������3045����������
        public const int INS_ERROR_CASLIB_CLIENT_NO_INNER_RESOURCE = 3291; ///< ��·ֱ��/P2P֮�󣬵���·ֱ���豸���صĴ�����
        public const int INS_ERROR_CASLIB_CLIENT_NO_P2P_RESOURCE = 3292; ///< û��P2Pȡ����Դ, ֱ����Դ����
        public const int OPEN_SDK_GENERAL_ERROR = -1; ///< 【-1】常规（通用）错误
        public const int OPEN_SDK_BASE = 10000; ///< 开放SDK基础错误
        public const int OPEN_SDK_NETWORK_SETUP_BASE = 100000; ///< 网络级别错误码，CURL反馈, 都是调开放平台接口报错（测试时首先保证登陆成功）
        public const int OPEN_SDK_USER_OPERATION_BASE = 200000; ///< 用户操作级别错误码
        public const int OPEN_SDK_OPENAPI_BASE = 300000; ///< OpenApi接口级别错误码, 详细参考https://open.ys7.com/doc/book/index/api-code.html
        public const int OPEN_SDK_SYSTEM_RESOURCE_BASE = 400000; ///< 系统资源级别错误
        public const int OPEN_SDK_NETSTREAM_BASE = 500000; ///< NetStream的错误码
        public const int OPEN_SDK_NOT_SUPPORT_BASE = 600000; ///< 不支持的错误码
        public const int OPEN_SDK_PUSH_BASE = 700000; ///< 推送级别错误
        public const int OPEN_SDK_UNDEFINE_BASE = 900000; ///< 未定义的错误码
        public const string OPEN_SDK_STREAM_ACCESSTOKEN_ERROR_OR_EXPIRE = "UE001"; ///< accesstoken失效，请重新获取accesstoken
        public const string OPEN_SDK_STREAM_PU_NO_RESOURCE = "UE101"; ///< 设备连接数过大, 升级设备固件版本,海康设备可咨询客服获取升级流程
        public const string OPEN_SDK_STREAM_TRANSF_DEVICE_OFFLINE = "UE102"; ///< 设备不在线，确认设备上线之后重试
        public const string OPEN_SDK_STREAM_INNER_TIMEOUT = "UE103"; ///< 播放失败，请求连接设备超时，检测设备网路连接是否正常
        public const string OPEN_SDK_STREAM_INNER_VERIFYCODE_ERROR = "UE104"; ///< 视频验证码错误，建议查看设备上标记的验证码
        public const string OPEN_SDK_STREAM_PLAY_FAIL = "UE105"; ///< 视频播放失败, 请查看消息回调中具体错误码iErrorCode
        public const string OPEN_SDK_STREAM_TRANSF_TERMINAL_BINDING = "UE106"; ///< 当前账号开启了终端绑定，只允许指定设备登录操作
        public const string OPEN_SDK_STREAM_VIDEO_RECORD_NOTEXIST = "UE108"; ///< 未查找到录像文件
        public const string OPEN_SDK_STREAM_VTDU_CONCURRENT_LIMIT = "UE109"; ///< 取流并发路数限制,请升级为企业版
        public const string OPEN_SDK_STREAM_UNSUPPORTED = "UE110"; ///< 设备不支持的清晰度类型, 请根据设备预览能力级选择.
        public const string OPEN_SDK_STREAM_DEVICE_RETURN_ON_VIDEO_SOURCE = "UE111"; ///< 设备返回无视频源, 请检测设备是否接触良好.
    }
}
