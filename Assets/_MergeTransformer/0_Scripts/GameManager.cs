using MergeAR.UI;
using Sirenix.OdinInspector;
using VTLTools;
using UnityEngine;
using DG.Tweening;

namespace MergeAR
{
    public class GameManager : Singleton<GameManager>
    {
        SoundSystem soundSystem;
        MusicSystem musicSystem;

        UIManager uIManager;
        GridManager gridManager;
        CharacterDataManager characterDataManager;
        PlayerManager playerManager;
        //CameraFollow cameraFollow;
        Battlefield battlefield;
        CursorControl cursorControl;
        ControlManager controlManager;
        Reinforcement reinforcement;
        TutorialManager tutorialManager;
        //PushRateTracking pushRateTracking;
        //EnvironmentManager.EnvironmentManager environmentManager;
        TimeManager timeManager;


        private GameState _state;
        [ShowInInspector]
        public GameState State
        {
            get => _state;
            set
            {
                _state = value;
                EventDispatcher.Instance.Dispatch(EventName.OnGameStateChanged, _state);
                Debug.Log("<color=yellow>Current game state:</color> " + State);
                switch (_state)
                {
                    case GameState.ResetRound:
                        HandleResetRound();
                        break;
                    case GameState.WaitToPlaceEnvironment:
                        HandleWaitToPlaceEnvironment();
                        break;
                    case GameState.Starting:
                        HandleStartingState();
                        break;
                    case GameState.Idle:
                        HandleIdleState();
                        break;
                    case GameState.Merging:
                        HandleMergingState();
                        break;
                    case GameState.Fight:
                        HandleFightState();
                        break;
                    case GameState.DefeatBattle:
                        HandleLoseBattle();
                        break;
                    case GameState.VictoryBattle:
                        HandleWinBattle();
                        break;
                }
                //WARNING: NO CODE BELOW THIS LINE!!!
            }
        }

        [SerializeField] AudioClip fightStateBackgroundAudioClip;
        [SerializeField] AudioClip runStateBackgroundAudioClip;
        [SerializeField] AudioClip victoryAnnounceAudioClip;
        [SerializeField] AudioClip loseAudioClip;
        [SerializeField] AudioClip danceAudioClip;

        [SerializeField] Camera canvasCamera;
        // Kick the game off with the first state
        void Start()
        {
            soundSystem = SoundSystem.instance;
            musicSystem = MusicSystem.instance;

            uIManager = UIManager.Instance;
            gridManager = GridManager.Instance;
            characterDataManager = CharacterDataManager.Instance;
            playerManager = PlayerManager.instance;
            //cameraFollow = CameraFollow.instance;
            battlefield = Battlefield.instance;
            cursorControl = CursorControl.instance;
            controlManager = ControlManager.Instance;
            reinforcement = Reinforcement.instance;
            tutorialManager = TutorialManager.Instance;
            //pushRateTracking = PushRateTracking.instance;
            //environmentManager = EnvironmentManager.EnvironmentManager.instance;
            timeManager = TimeManager.Instance;

            State = GameState.Starting;
            //AppOpenAdManager.Instance.SetCameraCanvasNativeAd(canvasCamera);
        }

        void HandleStartingState()
        {
            uIManager.ShowPopup(
                uIManager.setupMapPopup,
                null,
                0,
                () => State = GameState.WaitToPlaceEnvironment,
                null,
                null,
                () => State = GameState.Idle);
        }

        void HandleWaitToPlaceEnvironment()
        {
            EnvironmentManager.instance.SetActiveEnvironment(false);
            CursorControl.instance.isARRaycast = true;
            CursorControl.instance.cursor.SetScale(Vector3.one);
        }
        void HandleIdleState()
        {
            if (StaticVariables.CurrentLevel == 3 && !StaticVariables.IsAlreadyShowDailyGiftTutorial)
            {
                uIManager.ShowPopup(uIManager.dailyGiftPopup);
                StaticVariables.IsAlreadyShowDailyGiftTutorial = true;
            }
            else
                uIManager.ShowPopup(uIManager.mergingPopup, null, 0, null, TutorialManager.instance.CheckTutorialFirstBuy);

            CursorControl.instance.isARRaycast = false;
            CursorControl.instance.cursor.SetScale(EnvironmentManager.instance.EnvironmentScale);

            musicSystem.FadeMusicVolume(0.5f, 0.3f);
            musicSystem.PlayMusic(runStateBackgroundAudioClip);

            if (!uIManager.coinInforPopup.IsShow)
                uIManager.ShowPopup(uIManager.coinInforPopup);

            characterDataManager.LoadFriendlyGrids();
            characterDataManager.LoadEnemyGrids(StaticVariables.CurrentLevel);

            gridManager.CheckFriendlyGridFull();
            controlManager.isAllowToMerge = true;

            reinforcement.Init();
        }
        void HandleResetRound()
        {
            Helpers.RecycleAllChilds(ObjectPool.instance.spawnedPool.gameObject);

            //levelManager.ClearOldLevel();
            gridManager.ClearGridReference();
            characterDataManager.ClearCurrentCharacterData();
            //playerManager.ResetPlayer();
            //playerManager.playerMoveForward.ResetSpeed();
            //cameraFollow.ResetCamera();
            battlefield.ResetBattlefield();

            State = GameState.Starting;
        }



        void HandleMergingState()
        {


            musicSystem.FadeMusicVolume(0.3f, 0.3f);
            characterDataManager.SaveFriendlyGrids();


        }

        void HandleFightState()
        {
            musicSystem.FadeMusicVolume(1, 0.3f);
            musicSystem.PlayMusic(fightStateBackgroundAudioClip);
            controlManager.isAllowToMerge = false;
            uIManager.ShowPopup(uIManager.fightPopup);
            uIManager.fightPopup.Init();

            uIManager.HidePopup(uIManager.mergingPopup);

            //cameraFollow.MoveCamera(battlefield.FightCameraPoint, 1);

            reinforcement.IsTouchable = false;

            tutorialManager.HideGuildHandTutorial();
        }

        void HandleLoseBattle()
        {
            Time.timeScale = 1;
            musicSystem.FadeMusicVolume(0.3f, 0.3f);
            soundSystem.PlaySoundOneShot(soundSystem.uIAudioSource, loseAudioClip);

            uIManager.HidePopup(uIManager.fightPopup);

            StaticVariables.loseStreak++;
            timeManager.StopCountLevelPlayingTime();
            //LogManager.LogLevel(StaticVariables.CurrentLevel, LevelDifficulty.Easy, (int)timeManager.GetLevelPlayingTime(), PassLevelStatus.Fail, "RunMode");

            //CC_Interface.current.ShowInterAds(1.8f, "Inter_LoseBattle", StaticVariables.CurrentLevel);



            //pushRateTracking.CheckShowPushRate(2, () =>
            //{
            //    uIManager.ShowPopup(uIManager.battleResultPopup, levelManager.levelReward.GetCurrentLevelReward());
            //});
        }
        void HandleWinBattle()
        {
            Time.timeScale = 1;
            musicSystem.FadeMusicVolume(0.2f, 0.3f);
            soundSystem.PlaySoundOneShot(soundSystem.uIAudioSource, danceAudioClip);
            uIManager.HidePopup(uIManager.fightPopup);
            //cameraFollow.MoveCamera(battlefield.VictoryCameraPoint, 1);

            StaticVariables.loseStreak = 0;

            timeManager.StopCountLevelPlayingTime();
            //LogManager.LogLevel(StaticVariables.CurrentLevel, LevelDifficulty.Easy, (int)timeManager.GetLevelPlayingTime(), PassLevelStatus.Pass, "RunMode");
            Debug.Log((int)timeManager.GetLevelPlayingTime());

            //CC_Interface.current.ShowInterAds(3.8f, "Inter_WinBattle", StaticVariables.CurrentLevel);


            //pushRateTracking.CheckShowPushRate(3.8f, () =>
            //{
            //    uIManager.ShowPopup(uIManager.battleResultPopup, levelManager.levelReward.GetCurrentLevelReward(), 0,
            //    () =>
            //    {
            //        soundSystem.PlaySoundOneShot(soundSystem.uIAudioSource, victoryAnnounceAudioClip);
            //    },
            //    null,
            //    () =>
            //    {
            //        StaticVariables.CurrentLevel++;
            //    });
            //});
        }
    }
}