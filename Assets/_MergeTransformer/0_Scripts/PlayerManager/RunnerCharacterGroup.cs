using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class RunnerCharacterGroup : MonoBehaviour
    {
        [ReadOnly] public List<Character> runnerList;
        [ReadOnly] public List<Character> runnerMeleeList;
        [ReadOnly] public List<Character> runnerRangedList;
        public float destinyPopulation = 1;
        [SerializeField, ReadOnly] bool isWaitingToReOrder = false;

        public void SpawnDefaultCharacter()
        {
            Character _char = CharacterDataManager.Instance.SpawnCharacterWhileRun(this.transform, CharacterID.FM0);
            _char.tag = StringsSafeAccess.TAG_FRIENDLY_CHARACTER;
            runnerList.Add(_char);
        }


        [Button]
        public void PlusCharacter(int _number)
        {
            int _temp = 0;
            for (int i = 0; i < _number; i++)
            {
                Character _char;
                if (_temp % 2 == 0)
                {
                    _char = CharacterDataManager.Instance.SpawnCharacterWhileRun(this.transform, VTLTools.CharacterID.FM1);
                    runnerMeleeList.Add(_char);
                }
                else
                {
                    _char = CharacterDataManager.Instance.SpawnCharacterWhileRun(this.transform, VTLTools.CharacterID.FR1);
                    runnerRangedList.Add(_char);
                }
                _char.tag = StringsSafeAccess.TAG_FRIENDLY_CHARACTER;
                //_char.Init();
                runnerList.Add(_char);

                _char.transform.localPosition = NewCharacterPosition(this.transform.childCount - 1);

                _temp++;
                if (_temp > 1)
                    _temp = 0;

                PlayerManager.Instance.playerMoveController.CalculateBound();
            }
        }

        [Button]
        public void PlusCharacter(int _number, CharacterType _type)
        {
            int _temp = 0;
            for (int i = 0; i < _number; i++)
            {
                Character _char;
                if (_type == VTLTools.CharacterType.FriendlyMelee)
                {
                    _char = CharacterDataManager.Instance.SpawnCharacterWhileRun(this.transform, VTLTools.CharacterID.FM1);
                    runnerMeleeList.Add(_char);
                }
                else
                {
                    _char = CharacterDataManager.Instance.SpawnCharacterWhileRun(this.transform, VTLTools.CharacterID.FR1);
                    runnerRangedList.Add(_char);
                }
                //_char.Init();
                runnerList.Add(_char);

                _char.transform.localPosition = NewCharacterPosition(this.transform.childCount - 1);

                _temp++;
                if (_temp > 1)
                    _temp = 0;

                PlayerManager.Instance.playerMoveController.CalculateBound();
            }
        }

        [Button]
        public void MinusCharacter(int _number)
        {
            for (int i = 0; i < _number; i++)
            {
                if (this.transform.childCount > 0)
                {
                    runnerList[^1].motionController.SetTriggerDead();

                    Destroy(this.transform.GetChild(this.transform.childCount - 1).gameObject, 4);
                    this.transform.GetChild(this.transform.childCount - 1).SetParent(ObjectPool.instance.spawnedPool);

                    if (runnerList[^1].data.Type == CharacterType.FriendlyMelee)
                        runnerMeleeList.Remove(runnerList[^1]);
                    else
                        runnerRangedList.Remove(runnerList[^1]);

                    runnerList.RemoveAt(runnerList.Count - 1);
                }
            }
            //StartCoroutine(ResetPos());
            StartCoroutine(PlayerManager.Instance.runnerCharactersGroup.ResetPos(0.3f));
            //  CheckLose();
        }

        [Button]
        public void MinusCharacter(int number, CharacterType _type)
        {

            for (int i = 0; i < number; i++)
            {
                if (_type == CharacterType.FriendlyMelee)
                {
                    if (runnerMeleeList.Count > 0)
                    {
                        _RemoveLastCharacter(runnerMeleeList);
                    }
                }
                else
                {
                    if (runnerRangedList.Count > 0)
                    {
                        _RemoveLastCharacter(runnerRangedList);
                    }
                }
            }

            void _RemoveLastCharacter(List<Character> characters)
            {
                characters[^1].motionController.SetTriggerDead();


                Destroy(characters[^1].gameObject, 4);
                characters[^1].transform.SetParent(ObjectPool.instance.spawnedPool);

                runnerList.Remove(characters[^1]);
                characters.Remove(characters[^1]);
            }

            StartCoroutine(PlayerManager.Instance.runnerCharactersGroup.ResetPos(0.3f));

            // StartCoroutine(ResetPos());
            //CheckLose();
        }

        [Button]
        public void MultiCharacter(int _number)
        {

            int _index = this.transform.childCount * _number - this.transform.childCount;
            PlusCharacter(_index);
        }

        [Button]
        public void MultiCharacter(int _number, CharacterType _type)
        {

            int _index;
            if (_type == CharacterType.FriendlyMelee)
            {
                _index = runnerMeleeList.Count * _number - runnerMeleeList.Count;
                PlusCharacter(_index, _type);
            }
            else
            {
                _index = runnerRangedList.Count * _number - runnerRangedList.Count;
                PlusCharacter(_index, _type);
            }
        }

        [Button]
        public void DivideCharacter(int _number)
        {

            int _index = this.transform.childCount - this.transform.childCount / _number;
            MinusCharacter(_index);
            //CheckLose();
        }

        [Button]
        public void DivideCharacter(int _number, CharacterType _type)
        {

            int _index;
            if (_type == CharacterType.FriendlyMelee)
            {
                _index = runnerMeleeList.Count - runnerMeleeList.Count / _number;
                MinusCharacter(_index, _type);
            }
            else
            {
                _index = runnerRangedList.Count - runnerRangedList.Count / _number;
                MinusCharacter(_index, _type);
            }
            //CheckLose();
        }


        Vector3 NewCharacterPosition(int _posNumber)
        {
            Vector3 _pos;
            int _xPos = (_posNumber % 10 + 1) / 2;
            int _zPos = -_posNumber / 10;
            if (_posNumber % 2 == 0)
            {
                _pos = new Vector3(_xPos + destinyPopulation / 2, 0, _zPos) * destinyPopulation;
            }
            else
            {
                if (_posNumber > 10)
                    _pos = new Vector3((_xPos) * -1 + destinyPopulation / 2, 0, _zPos) * destinyPopulation;
                else
                    _pos = new Vector3((_xPos) * -1, 0, _zPos) * destinyPopulation;

            }
            return (_pos);
        }

        public void AddCharacterOnRunWay(Character _char)
        {
            if (_char.data.Type == CharacterType.FriendlyMelee)
            {
                runnerMeleeList.Add(_char);
            }

            if (_char.data.Type == CharacterType.FriendlyRanged)
            {
                runnerRangedList.Add(_char);
            }
            runnerList.Add(_char);
            _char.transform.SetParent(this.transform);

            GameObject _tempGO = new();
            _tempGO.transform.SetParent(this.transform);
            _tempGO.transform.localPosition = NewCharacterPosition(runnerList.Count - 1);


            _char.transform.SetParent(_tempGO.transform);
            _char.transform.DOLocalMove(Vector3.zero, 0.2f).OnComplete(() =>
            {
                _char.transform.SetParent(this.transform);
                Destroy(_tempGO);
            });




            PlayerManager.Instance.playerMoveController.CalculateBound();
        }

        Vector3 _Offset => new(destinyPopulation / 2, 0, 0);

        public bool isHavingOffset = false;
        public void CenterParentPosition()
        {
            if (this.runnerList.Count <= 10)
            {
                if (this.runnerList.Count % 2 == 0)
                {
                    if (isHavingOffset)
                        return;
                    foreach (Transform _child in this.transform)
                    {
                        _child.position += _Offset;
                    }
                    transform.position -= _Offset;
                    isHavingOffset = true;
                }
                else
                {
                    if (!isHavingOffset)
                        return;
                    foreach (Transform child in this.transform)
                    {
                        child.position -= _Offset;
                    }
                    transform.position += _Offset;
                    isHavingOffset = false;
                }
            }
        }

        public IEnumerator ResetPos(float _delayTime)
        {
            if (isWaitingToReOrder)
                yield break;

            isWaitingToReOrder = true;
            yield return new WaitForSeconds(_delayTime);
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).DOLocalMove(_ResetCharacterPosition(i), 0.2f);
            }
            yield return new WaitForSeconds(0.22f);
            PlayerManager.Instance.playerMoveController.CalculateBound();
            isWaitingToReOrder = false;
            Vector3 _ResetCharacterPosition(int _posNumber)
            {
                Vector3 _pos;
                int _xPos = (_posNumber % 10 + 1) / 2;
                int _zPos = -_posNumber / 10;
                if (this.transform.childCount > 10)
                {
                    if (_posNumber % 2 == 0)
                    {
                        _pos = new Vector3(_xPos + destinyPopulation / 2, 0, _zPos) * destinyPopulation;
                    }
                    else
                    {
                        _pos = new Vector3((_xPos) * -1 + destinyPopulation / 2, 0, _zPos) * destinyPopulation;
                    }
                }
                else
                {
                    if (this.transform.childCount % 2 == 0)
                    {
                        if (_posNumber % 2 == 0)
                        {
                            _pos = new Vector3(_xPos + destinyPopulation / 2, 0, _zPos) * destinyPopulation;
                        }
                        else
                        {
                            _pos = new Vector3((_xPos) * -1 + destinyPopulation / 2, 0, _zPos) * destinyPopulation;
                        }
                    }
                    else
                    {
                        if (_posNumber % 2 == 0)
                            _pos = new Vector3((_xPos), 0, _zPos) * destinyPopulation;
                        else
                            _pos = new Vector3((_xPos) * -1, 0, _zPos) * destinyPopulation;
                    }
                }
                return (_pos);
            }
        }

        public void MakeAllCharRunToBattlefield()
        {
            foreach (var _item in runnerList)
            {
                _item.RunToBattlefield();
            }
        }

        public void MakeAllCharGetInEmptyGrid()
        {
            foreach (var _char in runnerList)
            {
                Grid _grid = GridManager.Instance.GetEmptyFriendlyGrid();

                if (_grid == null)
                {
                    if (_char.data.Type == CharacterType.FriendlyMelee)
                    {
                        StaticVariables.ArchivedMeleeCharacterNumber++;
                    }
                    else
                    {
                        StaticVariables.ArchivedRangedCharacterNumber++;
                    }
                    Destroy(_char.gameObject);
                }
                else
                {
                    ControlManager.Instance.SetNewParentCharacter(_char.transform, _grid.transform, Vector3.zero, () => _char.motionController.SetBoolRun(false));
                    _grid.SetCurrentCharacter(_char);
                    CharacterDataManager.Instance.friendlyCharacters.Add(_char);
                }
                GridManager.Instance.CheckFriendlyGridFull(); //After all character get in position, check full grid or not
            }
        }

        public void KillOneRandomRunner()
        {
            Character _char = runnerList[Random.Range(0, runnerList.Count)];
            if (_char.State == CharacterState.Dead)
                return;
            runnerList.Remove(_char);
            if (_char.data.Type == CharacterType.FriendlyMelee)
                runnerMeleeList.Remove(_char);
            else
                runnerRangedList.Remove(_char);
            _char.State = CharacterState.Dead;
            _char.transform.SetParent(ObjectPool.instance.spawnedPool);
            Destroy(_char.gameObject, 10);
        }

        public void KillOneRunner(Character _char)
        {
            if (_char.State == CharacterState.Dead)
                return;
            runnerList.Remove(_char);
            if (_char.data.Type == CharacterType.FriendlyMelee)
                runnerMeleeList.Remove(_char);
            else
                runnerRangedList.Remove(_char);
            _char.State = CharacterState.Dead;
            _char.transform.SetParent(ObjectPool.instance.spawnedPool);
            Destroy(_char.gameObject, 10);
        }

        public void DrownOneRunner(Character _char)
        {
            if (_char.State == CharacterState.Fall)
                return;
            runnerList.Remove(_char);
            if (_char.data.Type == CharacterType.FriendlyMelee)
                runnerMeleeList.Remove(_char);
            else
                runnerRangedList.Remove(_char);
            _char.State = CharacterState.Fall;
            _char.transform.SetParent(EnvironmentManager.Instance.transform);
            Destroy(_char.gameObject, 10);
        }

        public void ClearAllList()
        {
            runnerList.Clear();
            runnerMeleeList.Clear();
            runnerRangedList.Clear();
            isHavingOffset = false;
        }

        public void ChangeSpeedAllRunner(float _speed)
        {
            foreach (var item in runnerList)
            {
                item.motionController.animator.speed = _speed;
            }
        }
    }
}