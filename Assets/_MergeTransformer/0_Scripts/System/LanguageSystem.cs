using I2.Loc;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using VTLTools;

namespace MergeAR
{
    public class LanguageSystem : Singleton<LanguageSystem>
    {
        [ShowInInspector, ReadOnly]
        public List<string> AllLanguages
        {
            get => LocalizationManager.GetAllLanguages();
        }

        public void ChangeLanguage(int _newLangIndex)
        {
            StaticVariables.CurrentLanguage = AllLanguages[_newLangIndex];
        }
    }
}