using BreakInfinity;
using I2.Loc;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools
{
    public class StaticVariables : MonoBehaviour
    {
        #region Setting
        [ShowInInspector, BoxGroup("Setting")]
        public static bool IsSoundOn
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_SOUND_ON, true);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_SOUND_ON, value);
        }

        [ShowInInspector, BoxGroup("Setting")]
        public static bool IsMusicOn
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_MUSIC_ON, true);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_MUSIC_ON, value);
        }

        [ShowInInspector, BoxGroup("Setting")]
        public static bool IsVibrationOn
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_VIBRATION_ON, true);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_VIBRATION_ON, value);
        }

        [ShowInInspector, BoxGroup("Setting")]
        public static string CurrentLanguage
        {
            get => LocalizationManager.CurrentLanguage;
            set => LocalizationManager.CurrentLanguage = value;
        }

        [ShowInInspector, BoxGroup("Setting")]
        public static GraphicPreset SavedGraphicPreset
        {

            get
            {
#if UNITY_ANDROID
                if (SystemInfo.systemMemorySize < 3000)
                    return VTLPlayerPrefs.GetEnumValue(StringsSafeAccess.PREF_KEY_GRAPHIC_PRESET, GraphicPreset.Low);
#endif
#if UNITY_IOS
                if (SystemInfo.systemMemorySize < 2000)
                    return VTLPlayerPrefs.GetEnumValue(StringsSafeAccess.PREF_KEY_GRAPHIC_PRESET, GraphicPreset.Low);
#endif
                else
                    return VTLPlayerPrefs.GetEnumValue(StringsSafeAccess.PREF_KEY_GRAPHIC_PRESET, GraphicPreset.Performant);
            }
            set
            {
                VTLPlayerPrefs.SetEnumValue(StringsSafeAccess.PREF_KEY_GRAPHIC_PRESET, value);
            }
        }
        #endregion

        #region Data
        [ShowInInspector, BoxGroup("Data")]
        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_CURRENT_LEVEL, 1);
            set
            {
                PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_CURRENT_LEVEL, value);
                EventDispatcher.Instance.Dispatch(EventName.OnLevelChange, value);
            }
        }

        [ShowInInspector, BoxGroup("Data")]
        public static BigDouble CurrentCoin
        {
            get => BigDouble.Parse(PlayerPrefs.GetString(StringsSafeAccess.PREF_KEY_CURRENT_COIN, "200"));

            set
            {
                PlayerPrefs.SetString(StringsSafeAccess.PREF_KEY_CURRENT_COIN, value.ToString());
                EventDispatcher.Instance.Dispatch(EventName.OnCoinValueChange, value);
            }
        }

        [ShowInInspector, BoxGroup("Data")]
        public static int ArchivedMeleeCharacterNumber
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_ARCHIVED_MELEE, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_ARCHIVED_MELEE, value);
        }

        [ShowInInspector, BoxGroup("Data")]
        public static int ArchivedRangedCharacterNumber
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_ARCHIVED_RANGED, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_ARCHIVED_RANGED, value);
        }

        [ShowInInspector, BoxGroup("Data")]
        public static int TotalNumberOfPurchasedMelee
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_TOTAL_PURCHASED_MELEE, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_TOTAL_PURCHASED_MELEE, value);
        }

        [ShowInInspector, BoxGroup("Data")]
        public static int TotalNumberOfPurchasedRanged
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_TOTAL_PURCHASED_RANGED, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_TOTAL_PURCHASED_RANGED, value);
        }

        [ShowInInspector, BoxGroup("Data")]
        public static int loseStreak = 0;
        #endregion

        #region Tutorial
        [ShowInInspector, BoxGroup("Tutorial")]
        public static bool IsUnlockedAtLeastOneHero
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_UNLOCKED_ATLEAST_ONE_HERO, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_UNLOCKED_ATLEAST_ONE_HERO, value);
        }

        [ShowInInspector, BoxGroup("Tutorial")]
        public static TutorialPhase CurrentTutorialPhase
        {

            get
            {
                return VTLPlayerPrefs.GetEnumValue(StringsSafeAccess.PREF_KEY_CURRENT_TUTORIAL_PHASE, TutorialPhase.FirstBuyCharacter);
            }
            set
            {
                VTLPlayerPrefs.SetEnumValue(StringsSafeAccess.PREF_KEY_CURRENT_TUTORIAL_PHASE, value);
            }
        }

        [ShowInInspector, BoxGroup("Tutorial")]
        public static bool IsAlreadyShowDailyGiftTutorial
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_ALREADY_SHOW_DAILYGIFT, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_ALREADY_SHOW_DAILYGIFT, value);
        }
        #endregion

        #region Time

        [ShowInInspector, BoxGroup("Time")]
        public static string LastOnlineTime
        {
            get => PlayerPrefs.GetString(StringsSafeAccess.PREF_KEY_LAST_TIME_ONLINE, DateTime.MinValue.ToString()).ToString();
            set => PlayerPrefs.SetString(StringsSafeAccess.PREF_KEY_LAST_TIME_ONLINE, value.ToString());
        }

        [ShowInInspector, BoxGroup("Time")]
        public static int CurrentDailyGiftDay
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_CURRENT_DAILYGIFT_DAY, 1);
            set
            {
                if (value == 8) //Over number of daily gift
                    PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_CURRENT_DAILYGIFT_DAY, 1);
                else
                    PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_CURRENT_DAILYGIFT_DAY, value);

            }
        }

        [ShowInInspector, BoxGroup("Time")]
        public static bool IsAlreadyGetTodayDailyGift
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_ALREADY_GET_TODAY_DAILYGIFT, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_ALREADY_GET_TODAY_DAILYGIFT, value);
        }


        [ShowInInspector, BoxGroup("Time")]
        public static float RemainTimeCountDownOnlineReward
        {
            get => PlayerPrefs.GetFloat(StringsSafeAccess.PREF_KEY_REMAINTIME_COUNTDOWN_ONLINEREWARD, 60); //first online reward time, mean 1 minute
            set => PlayerPrefs.SetFloat(StringsSafeAccess.PREF_KEY_REMAINTIME_COUNTDOWN_ONLINEREWARD, value);
        }

        [ShowInInspector, BoxGroup("Time")]
        public static int OnlineRewardClaimedCount
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_ONLINEREWARD_CLAIMED_COUNT, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_ONLINEREWARD_CLAIMED_COUNT, value);
        }

        #endregion

        #region IAP, AD
        [ShowInInspector, BoxGroup("IAP, AD")]
        public static bool IsRemovedAd
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_REMOVED_AD, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_REMOVED_AD, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static bool IsShowMediation
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_SHOW_MEDIATION, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_SHOW_MEDIATION, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static bool isHackNoAd = false;

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static int CurrentIndexShowPush
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_CURRENT_INDEX_SHOW_PUSH, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_CURRENT_INDEX_SHOW_PUSH, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static bool IsShowPushAtLeastOne
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_SHOW_PUSH_AT_LEAST_ONE, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_SHOW_PUSH_AT_LEAST_ONE, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static int CurrentIndexShowRate
        {
            get => PlayerPrefs.GetInt(StringsSafeAccess.PREF_KEY_CURRENT_INDEX_SHOW_RATE, 0);
            set => PlayerPrefs.SetInt(StringsSafeAccess.PREF_KEY_CURRENT_INDEX_SHOW_RATE, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static bool IsShowRateAtLeastOne
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_SHOW_RATE_AT_LEAST_ONE, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_SHOW_RATE_AT_LEAST_ONE, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static bool IsRated
        {
            get => VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_IS_RATED, false);
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_IS_RATED, value);
        }

        [ShowInInspector, BoxGroup("IAP, AD")]
        public static List<string> ListGamesDownloadedByPush
        {
            get
            {
                string joinedString = PlayerPrefs.GetString(StringsSafeAccess.PREF_KEY_LIST_GAME_DOWNLOADED_BY_PUSH, "");

                if (string.IsNullOrEmpty(joinedString))
                {
                    return new List<string>();
                }

                string[] splitString = joinedString.Split('|');
                List<string> stringList = new(splitString);

                return stringList;
            }
            set
            {
                string joinedString = string.Join("|", value.ToArray());
                PlayerPrefs.SetString(StringsSafeAccess.PREF_KEY_LIST_GAME_DOWNLOADED_BY_PUSH, joinedString);
            }
        }
        #endregion
    }
}