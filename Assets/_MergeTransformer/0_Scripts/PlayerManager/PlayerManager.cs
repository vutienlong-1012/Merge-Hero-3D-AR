using DG.Tweening;
using System;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerMoveForward playerMoveForward;
        public PlayerMoveController playerMoveController;
        public RunnerCharacterGroup runnerCharactersGroup;

        public IRoadEnemy currentRoadEnemy;

        public void MoveRunnerGroupForward(Vector3 _pos, float _duration, Action _action = null)
        {
            runnerCharactersGroup.transform.DOMoveX(_pos.x, _duration).SetEase(Ease.Linear).OnComplete(() => _action?.Invoke());
            playerMoveForward.transform.DOMoveZ(_pos.z, _duration).SetEase(Ease.Linear);
        }

        //public void DecideLose(float _delayResetPos, out bool _isLose)
        //{
        //    _isLose = false;

        //    if (currentRoadEnemy != null)
        //    {
        //        if (runnerCharactersGroup.runnerList.Count == 0)
        //        {
        //            GameManager.instance.State = GameState.LoseRun;
        //            _isLose = true;

        //            try
        //            {
        //                currentRoadEnemy.EnemyVictory();
        //            }
        //            catch
        //            { }
        //            return;
        //        }
        //        if (currentRoadEnemy.IsFinishFight())
        //        {
        //            playerMoveController.availableToMove = true;
        //            playerMoveForward.isAllowToMoveForward = true;
        //            playerMoveForward.ResetSpeed();

        //            foreach (var _item in runnerCharactersGroup.runnerList)
        //            {
        //                _item.motionController.SetBoolRun(true);
        //                _item.motionController.SetBoolAtk(false);
        //            }
        //            _isLose = false;

        //            currentRoadEnemy = null;

        //            StartCoroutine(runnerCharactersGroup.ResetPos(0));
        //            playerMoveController.CalculateBound();

        //        }
        //    }
        //    else
        //    {
        //        if (runnerCharactersGroup.runnerList.Count == 0)
        //        {
        //            GameManager.instance.State = GameState.LoseRun;
        //            _isLose = true;
        //        }
        //        else
        //        {
        //            _isLose = false;
        //            StartCoroutine(runnerCharactersGroup.ResetPos(_delayResetPos));
        //            playerMoveController.CalculateBound();
        //        }
        //    }
        //}

        public void ResetPlayer()
        {
            currentRoadEnemy = null;
            playerMoveForward.transform.position = Vector3.zero;
            runnerCharactersGroup.transform.position = Vector3.zero;
            runnerCharactersGroup.ClearAllList();
        }
    }
}