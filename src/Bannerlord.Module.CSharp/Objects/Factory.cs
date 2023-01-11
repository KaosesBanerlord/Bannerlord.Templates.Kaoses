using KaosesCommon.Utils;
using BLNamespace.Settings;
using System.Reflection;

namespace BLNamespace
{
    /// <summary>
    /// BLNamespace Factory Object
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Variable to hold the MCM settings object
        /// </summary>
        private static Config? _settings = null;

        /// <summary>
        /// Bool indicates if MCM is a loaded mod
        /// </summary>
        public static bool MCMModuleLoaded { get; set; } = false;

        /// <summary>
        /// MCM Settings Object Instance
        /// </summary>
        public static Config Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Config.Instance;
                    if (_settings is null)
                    {
                        IM.ShowMessageBox("$(ProjectModuleName Failed to load MCM config provider", "$(ProjectModuleName MCM Error");
                    }
                }
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        /// <summary>
        /// Mod version
        /// </summary>
        public static string ModVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Unused mod config file path
        /// </summary>
        private static string ConfigFilePath
        {

            get
            {
                return @"..\\..\\Modules\\" + SubModule.ModuleId + "\\config.json";
            }
            //set {  = value; }

        }

    }
}
