using MCM.Abstractions;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;
using System;
using System.Collections.Generic;
using TaleWorlds.Localization;
//using MCM.Abstractions.Settings.Base.PerSave;


namespace BLNamespace.Settings
{
    //public class Settings : AttributePerSaveSettings<Settings>, ISettingsProviderInterface
    public class Config : AttributeGlobalSettings<Config>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Config()
        {
            PropertyChanged += MCMSettings_PropertyChanged;
        }


        #region ModSettingsStandard
        public override string Id => SubModule.ModuleId;
        public override string FolderName => SubModule.ModuleId;
        public string ModName => "$(ProjectModuleName)";
        public override string FormatType => "json";
        #region Translatable DisplayName 
        // Build mod display name with name and version form the project properties version
        #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the null ability of reference types.
        TextObject versionTextObj = new TextObject(typeof(Config).Assembly.GetName().Version?.ToString(3) ?? "");
        public override string DisplayName => new TextObject("{=BLNamespaceDisplayName}" + ModName + " " + versionTextObj.ToString())!.ToString();
        #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the null ability of reference types.
        #endregion
        public string ModDisplayName { get { return DisplayName; } }
        #endregion

        [SettingPropertyBool("{=debug}Debug", RequireRestart = false, HintText = "{=debug_desc}Displays mod developer debug information and logs them to the file")]
        [SettingPropertyGroup("Debug", GroupOrder = 1)]
        public bool Debug { get; set; } = true;

        [SettingPropertyBool("{=debuglog}Log to file", RequireRestart = false, HintText = "{=debuglog_desc}Log information messages to the log file as well as errors and debug")]
        [SettingPropertyGroup("Debug", GroupOrder = 2)]
        public bool LogToFile { get; set; } = true;

        ///~ Mod Specific settings 
        #region Mod Specific settings
        [SettingPropertyBool("{=Name_bool}Name_bool", Order = 0, RequireRestart = false, HintText = "{=Bool_explanation}Bool_explanation.")]
        [SettingPropertyGroup("Main_Group/Nested_Group/Second_Nested")]
        public bool boolVariable { get; set; } = true;

        // Value is displayed as "X Denars"
        [SettingPropertyInteger("Name_Int", 0, 100, "0 Denars", Order = 1, RequireRestart = false, HintText = "Int_explanation.")]
        [SettingPropertyGroup("Main_Group/Nested_Group/Second_Nested")]
        public int IntVariable { get; set; } = 25;

        // Value is displayed as a percentage
        [SettingPropertyFloatingInteger("Name_Float", 0.0f, 1.0f, "#0%", Order = 2, RequireRestart = false, HintText = "Float_explanation.")]
        [SettingPropertyGroup("Main_Group/Nested_Group/Second_Nested")]
        public float FloatVariable { get; set; } = 0.75f;

        // Value is displayed as a string
        [SettingPropertyText("Name_String", Order = 3, RequireRestart = false, HintText = "String_Explanation")]
        [SettingPropertyGroup("Main_Group/Nested_Group/Second_Nested")]
        public string StringVariable { get; set; } = "The_textbox_data_here";


        [SettingPropertyDropdown("{=DropDown}DropDownName" + "*", Order = 3, RequireRestart = true,
            HintText = "{=DropDown_Desc}Description for Dropdown."),
            SettingPropertyGroup("{=Category}Category" + "/" + "{=Category2}Category2")]
        public Dropdown<string> DropDownName { get; } = new(new string[]
        {
            "No Override",
            "Override"
        }, 0);
        #endregion

        //~ Presets
        #region Presets
        public override IEnumerable<ISettingsPreset> GetBuiltInPresets()
        {
            foreach (var preset in base.GetBuiltInPresets())
            {
                yield return preset;
            }

            yield return new MemorySettingsPreset(Id, "native all off", "Native All Off", () => new Config
            {

            });


            yield return new MemorySettingsPreset(Id, "native all on", "Native All On", () => new Config
            {
                //TownFoodBonus = 4.0f,
                //SettlementFoodBonusEnabled = true,
                //SettlementProsperityFoodMalusDivisor = 100
            });
        }
        #endregion


        private void MCMSettings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Debug))
            {
                Debug = false;
                LogToFile = false;
            }
        }


    }
}
