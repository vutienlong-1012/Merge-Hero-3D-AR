using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using VTLTools;

namespace MergeAR
{
    public class LanguageSwitcherDropdown : MonoBehaviour
    {
        [SerializeField] Dropdown languageSwitcherDropdown;

        private void OnEnable()
        {
            Init();
            languageSwitcherDropdown.onValueChanged.AddListener(DropdownOnValueChange);
        }

        private void OnDisable()
        {
            languageSwitcherDropdown.onValueChanged.RemoveListener(DropdownOnValueChange);
        }

        void Init()
        {
            languageSwitcherDropdown.ClearOptions();
            var _languages = LanguageSystem.Instance.AllLanguages;
            for (int i = 0; i < _languages.Count; i++)
            {
                Dropdown.OptionData _optionData = new()
                {
                    text = LocalizationManager.GetTranslation(_languages[i], true, 0, true, false, null, _languages[i], true),
                };
                languageSwitcherDropdown.options.Add(_optionData);

                if (_languages[i] == StaticVariables.CurrentLanguage)
                {
                    languageSwitcherDropdown.value = i;
                    languageSwitcherDropdown.RefreshShownValue();
                }
            }
        }
        void DropdownOnValueChange(int _value)
        {
            LanguageSystem.Instance.ChangeLanguage(_value);
        }
    }
}