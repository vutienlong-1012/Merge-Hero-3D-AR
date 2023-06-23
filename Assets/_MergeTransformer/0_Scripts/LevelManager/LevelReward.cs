using BreakInfinity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class LevelReward : Singleton<LevelReward>
    {

        [ShowInInspector]
        public BigDouble GetCurrentLevelReward()
        {
            CharacterDataManager _characterDataManager = CharacterDataManager.Instance;
            float _totalEnemyHealthLostPercent = (_characterDataManager.startTotalEnemyHealth - _characterDataManager.CurrentTotalEnemyHealth) / (_characterDataManager.startTotalEnemyHealth / 100);
            if (StaticVariables.CurrentLevel == 1)
            {
                return 600 * _totalEnemyHealthLostPercent / 100;
            }
            else
            {
                return (600 + (StaticVariables.CurrentLevel - 1) * 400) * _totalEnemyHealthLostPercent / 100;
            }
        }

        [ShowInInspector]
        public BigDouble GetCurrentLevelWinReward()
        {
            return GetRewardByLevel(StaticVariables.CurrentLevel);
        }

        public BigDouble GetRewardByLevel(int _level)
        {
            _level = Mathf.Clamp(_level, 1, _level);
            if (_level == 1)
            {
                return 600;
            }
            else
            {
                return 600 + (_level - 1) * 400;
            }
        }
    }
}