using BreakInfinity;
using Sirenix.OdinInspector;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class CurrencyManager : MonoBehaviour
    {
        //[ShowInInspector]
        //public BigDouble Coin
        //{
        //    get
        //    {
        //        return StaticVariables.CurrentCoin;
        //    }

        //    set
        //    {
        //        StaticVariables.CurrentCoin = value;
        //        EventDispatcher.Instance.Dispatch(EventName.OnCoinValueChange, value);
        //    }
        //}

        [Button]
        public BigDouble CostBuySoldierCalculate(int _time)
        {
            _time = Mathf.Clamp(_time, 1, _time);
            if (_time == 1)
                return 200;
            else
            {
                int _result = 200;
                for (int i = 1; i < _time; i++)
                {
                    _result += 605;
                }
                return _result;
            }
        }

        [Button]
        public BigDouble WinLevelRewardCalculate(int _level)
        {
            _level = Mathf.Clamp(_level, 1, _level);
            if (_level == 1)
                return 600;
            else
            {
                int _result = 600;
                for (int i = 1; i < _level; i++)
                {
                    _result += 400;
                }
                return _result;
            }
        }

        [Button]
        public BigDouble LoseLevelRewardCalculate(int _level, float _percentHealthLost)
        {
            _level = Mathf.Clamp(_level, 1, _level);
            if (_level == 1)
                return 600 * (_percentHealthLost / 100);
            else
            {
                int _result = 600;
                for (int i = 1; i < _level; i++)
                {
                    _result += 400;
                }
                return _result * (_percentHealthLost / 100);
            }
        }

        [Button]
        public BigDouble UnlockNewHeroRewardCalculate(int _level)
        {
            return WinLevelRewardCalculate(_level) * 5;
        }
    }
} 