using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.MyCardPopup
{

    public class HeroCard : MonoBehaviour
    {
        [SerializeField] Text levelText;
        [SerializeField] Image iconImage;
        [SerializeField] Text healthInforText;
        [SerializeField] Text damageInforText;
        [SerializeField] Image outline;

        [SerializeField] Color avatarShowColor;
        [SerializeField] Color avatarHideColor;

        [SerializeField] Color outlineShowColor;
        [SerializeField] Color outlineHideColor;

        public void Init(CharacterData _char)
        {
            if (_char.IsUnlocked)
            {
                levelText.text = (I2.Loc.ScriptLocalization.LEVEL + " " + _char.scaleLevel).ToUpper();
                iconImage.sprite = _char.avatar;
                iconImage.color = avatarShowColor;

                healthInforText.text = _char.startHealth.ToString();
                damageInforText.text = _char.damage.ToString();
            }
            else
            {
                levelText.text = (I2.Loc.ScriptLocalization.LOCKED).ToUpper();
                iconImage.sprite = _char.avatar;
                iconImage.color = avatarHideColor;

                healthInforText.text = "???";
                damageInforText.text = "???";
            }

            //if (_char.Type == VTLTools.CharacterType.FriendlyRanged)
                //iconImage.rectTransform.anchoredPosition += new Vector2(20, 0);
        }
    }
}