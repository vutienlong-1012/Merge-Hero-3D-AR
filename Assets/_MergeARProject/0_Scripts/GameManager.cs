using MergeAR.UI;
using Sirenix.OdinInspector;
using System.Collections;
using VTLTools;
using UnityEngine;

namespace MergeAR
{
    public class GameManager : Singleton<GameManager>
    {

        UIManager uIManager;

        private GameState _state;
        [ShowInInspector]
        public GameState State
        {
            get => _state;
            set
            {
                _state = value;
                EventDispatcher.Instance.Dispatch(EventName.OnBeforeFightStateChange, _state);
                Debug.Log(State);
                switch (_state)
                {
                    case GameState.ResetRound:
                        HandleResetRound();
                        break;
                    case GameState.Starting:
                        HandleStartingState();
                        break;
                    case GameState.WaitToPlaceEnvironment:
                        HandleWaitToPlaceEnvironment();
                        break;
                    case GameState.Idle:
                        HandleIdleState();
                        break;
                    case GameState.Lose:
                        HandleLose();
                        break;
                    case GameState.Win:
                        HandleWin();
                        break;
                    case GameState.Retry:
                        HandleRetry();
                        break;
                }
                EventDispatcher.Instance.Dispatch(EventName.OnAfterFightStateChange, _state);
            }
        }


        // Kick the game off with the first state
        void Start()
        {
            uIManager = UIManager.instance;
            Application.targetFrameRate = 60;
            State = GameState.Starting;
        }

        void HandleResetRound()
        {

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
            uIManager.ShowPopup(uIManager.homePopup);
            CursorControl.instance.isARRaycast = false;
            CursorControl.instance.cursor.SetScale(EnvironmentManager.instance.EnvironmentScale);
        }
        void HandleChoosingNodeState()
        {

        }
        void HandleRunState()
        {

        }
        void HandleFightState()
        {

        }

        //void HandleAttackCastle()
        //{

        //}

        void HandleLose()
        {

        }
        void HandleWin()
        {

        }

        void HandleRetry()
        {


        }
    }
}