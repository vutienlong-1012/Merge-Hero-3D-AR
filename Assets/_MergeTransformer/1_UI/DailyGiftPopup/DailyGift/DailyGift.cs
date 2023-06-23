using BreakInfinity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.DailyGiftPopup
{
    public class DailyGift : MonoBehaviour
    {
        [SerializeField] Text dayText;
        [SerializeField] Image filterImage;
        [SerializeField] Image checkMarkImage;
        [SerializeField] Image glowEffectImage;
        [SerializeField] Text valueText;

        bool isClaimed;
        [ShowInInspector]
        public bool IsClaimed
        {
            get => isClaimed;
            set
            {
                isClaimed = value;
                checkMarkImage.gameObject.SetActive(value);
                filterImage.gameObject.SetActive(value);
            }
        }

        bool isTodayGift;
        [ShowInInspector]
        public bool IsTodayGift
        {
            get => isTodayGift;
            set
            {
                isTodayGift = value;
                glowEffectImage.gameObject.SetActive(value);
            }
        }

        public void Init(DailyGiftData _data)
        {
            try
            {
                dayText.text = (I2.Loc.ScriptLocalization.DAY + " " + _data.day).ToUpper();
            }
            catch
            {
                dayText.text = I2.Loc.ScriptLocalization.DAY + " " + _data.day;
            }

            valueText.text = BigDouble.ToText(_data.value);

            IsClaimed = _data.day < StaticVariables.CurrentDailyGiftDay;
            IsTodayGift = _data.day == StaticVariables.CurrentDailyGiftDay;
        } 
    }
}