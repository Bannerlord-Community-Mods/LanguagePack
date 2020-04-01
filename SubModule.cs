using System;
using System.Reflection;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace LanguagePack
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            // add the change language button
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Reload_Languages", new TextObject("Reload Languages", null), 9999999, () => Reset_gameTextDictionary(), false));
        }

        public static void Reset_gameTextDictionary()
        {
            // clear the translation data from the game using reflection
            Type type = typeof(LocalizedTextManager);
            FieldInfo info = type.GetField("_gameTextDictionary", BindingFlags.NonPublic | BindingFlags.Static);
            object value = info.GetValue(null);

            value.GetType().GetMethod("Clear").Invoke(value, null);

            // reload all localization xmls
            LocalizedTextManager.LoadLocalizationXmls();
        }
    }
}