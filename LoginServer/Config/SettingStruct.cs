namespace LoginServer.Config
{
    public struct SettingStruct
    {
        public bool Debug;

        public bool AutoAccount;

        public bool GMOnly;

        public SettingStruct(bool debug, bool auto, bool gm)
        {
            Debug = debug;
            AutoAccount = auto;
            GMOnly = gm;
        }
    }
}
