using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class MergeableFX : MonoBehaviour
    {
        [SerializeField] GameObject meleeIdleEffect;
        [SerializeField] GameObject rangedIdleEffect;
        [SerializeField] GameObject meleeSelectEffect;
        [SerializeField] GameObject rangedSelectEffect;

        public void SetActive(bool _isActive, CharacterType _type = CharacterType.None, bool _isSelect = false)
        {
            if (!_isActive)
            {
                _SetActive(false, false, false, false);
            }
            else
            {
                if (_type == CharacterType.FriendlyMelee)
                {
                    if (_isSelect)
                        _SetActive(_meleeIdle: false, _rangedIdle: false, _meleeSelect: true, _rangedSelect: false);
                    else
                        _SetActive(_meleeIdle: true, _rangedIdle: false, _meleeSelect: false, _rangedSelect: false);
                }
                else
                {
                    if (_isSelect)
                        _SetActive(_meleeIdle: false, _rangedIdle: false, _meleeSelect: false, _rangedSelect: true);
                    else
                        _SetActive(_meleeIdle: false, _rangedIdle: true, _meleeSelect: false, _rangedSelect: false);
                }
            }

            void _SetActive(bool _meleeIdle, bool _rangedIdle, bool _meleeSelect, bool _rangedSelect)
            {
                meleeIdleEffect.SetActive(_meleeIdle);
                rangedIdleEffect.SetActive(_rangedIdle);
                meleeSelectEffect.SetActive(_meleeSelect);
                rangedSelectEffect.SetActive(_rangedSelect);
            }
        }
    }
}