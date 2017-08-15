namespace TouchTableServer.Model
{
    public static class Opcodes
    {
        public enum ClientOpcodes
        {
            CMSG_CONNECTION_READY = 1000,
            CMSG_GAME_INITIALIZED = 1001,
            CMSG_GAME_USER_CHANGED_SHEET = 1002,
            CMSG_GAME_REPORT_STATE = 1003,
            CMSG_GAME_TRIGGER_INTERRUPT = 1004,
            CMSG_GAME_END = 1005,
            CMSG_USER_READY = 1006
        }

        public enum ServerOpcodes
        {
            SMSG_INIT_GAME = 2000,
            SMSG_START_GAME = 2001,
            SMSG_STOP_GAME = 2002,
            SMSG_ERR_NO_SESSION_FOR_GROUPID = 2003,
            SMSG_ERR_CONNECTION_BLOCKED = 2004,
            SMSG_ERR_UNKNOWN_OPCODE = 2005,
            SMSG_ERR_INVALID_PACKET = 2006,
            SMSG_GAME_UPDATE = 2007,
            SMSG_HANDSHAKE_CLIENT = 2008,
            SMSG_ERR_INVALID_CLIENTID = 2009,
            SMSG_PAUSE_GAME = 2010,
            SMSG_ERR_CLIENT_NOT_INITIALIZED = 2011,
            SMSG_REPORT_GAME_STATE = 2012,
            SMSG_ERR_CLIENT_NOT_SUPPORTED = 2013,
            SMSG_WRAPPER_LOAD_GAMES = 2014,
            SMSG_WRAPPER_SET_PIPE = 2015,
            SMSG_WRAPPER_INIT_PIPES = 2016,
            SMSG_WRAPPER_START_INTRO = 2017,
            SMSG_WRAPPER_SHOW_FEEDBACK = 2018,
            SMSG_CONTINUE_SESSION = 2019,
            SMSG_WRAPPER_SHOW_TEAMFEEDBACK = 2020
        }
    }
}
