using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;
using System.IO;
using Newtonsoft.Json;

namespace MergeAR
{
    public class CharacterDataManager : Singleton<CharacterDataManager>
    {
        [BoxGroup("Data"), ShowInInspector, ReadOnly]
        public List<CharacterData> allCharacterDatas = new();
        [BoxGroup("Data"), ShowInInspector, ReadOnly]
        public List<CharacterData> eMCharacterDatas = new();
        [BoxGroup("Data"), ShowInInspector, ReadOnly]
        public List<CharacterData> fMCharacterDatas = new();
        [BoxGroup("Data"), ShowInInspector, ReadOnly]
        public List<CharacterData> eRCharacterDatas = new();
        [BoxGroup("Data"), ShowInInspector, ReadOnly]
        public List<CharacterData> fRCharacterDatas = new();

        [BoxGroup("Data/Prefab"), SerializeField]
        EnemyMeleeCharacter enemyMeleeCharacter;

        [BoxGroup("Data/Prefab"), SerializeField]
        FriendlyMeleeCharacter friendlyMeleeCharacter;

        [BoxGroup("Data/Prefab"), SerializeField]
        EnemyRangedCharacter enemyRangedCharacter;

        [BoxGroup("Data/Prefab"), SerializeField]
        FriendlyRangedCharacter friendlyRangedCharacter;

        [BoxGroup("Data"), SerializeField, FolderPath]
        string characterDataFolderPath;

        [BoxGroup("Data"), SerializeField]
        GridManager gridManager;

        [TabGroup("Enemy Grids"), SerializeField, FilePath]
        string enemyGridDataFileName;

        [TabGroup("Enemy Grids"), ShowInInspector, ReadOnly]
        List<CharacterID> CurrentEnemyGridIds
        {
            get
            {
                List<CharacterID> _currentEnemyGridIDs = new();
                foreach (var _item in gridManager.enemyGrids)
                {
                    if (_item.IsHaveCharacter)
                        _currentEnemyGridIDs.Add(_item.GetCurrentCharacter().data.iD);
                    else
                        _currentEnemyGridIDs.Add(CharacterID.None);
                }
                return _currentEnemyGridIDs;
            }
        }

        [TabGroup("Enemy Grids"), ShowInInspector]
        public List<List<CharacterID>> AllEnemyGridIds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<List<CharacterID>>>(EnemyGridData);
            }
            set
            {
                EnemyGridData = JsonConvert.SerializeObject(value);
            }
        }

        [TabGroup("Enemy Grids"), ShowInInspector, ReadOnly]
        string EnemyGridData
        {
            get
            {
                return Helpers.LoadFileToString(enemyGridDataFileName);
            }
            set
            {
                Helpers.SaveStringToFile(enemyGridDataFileName, value);
            }
        }

        [TabGroup("Enemy Grids"), ReadOnly]
        public List<Character> enemyCharacters;

        [TabGroup("Enemy Grids"), ShowInInspector]
        public float CurrentTotalEnemyHealth
        {
            get
            {
                return GetTotalEnemyHealth();
            }
        }

        [TabGroup("Enemy Grids"), SerializeField, ReadOnly]
        public float startTotalEnemyHealth;

        [TabGroup("Friendly Grids"), ShowInInspector, ReadOnly]
        string FriendlyGridData
        {
            get
            {
                return PlayerPrefs.GetString(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA, "");
            }
            set
            {
                PlayerPrefs.SetString(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA, value);
            }
        }

        [TabGroup("Friendly Grids"), ReadOnly]
        public List<Character> friendlyCharacters;

        [TabGroup("Friendly Grids"), ShowInInspector]
        public float CurrentTotalFriendlyHealth
        {
            get
            {
                float _total = 0;
                foreach (var _character in friendlyCharacters)
                {
                    _total += _character.CurrentHealth;
                }
                return _total;
            }
        }

        #region Friendly Function
        [TabGroup("Friendly Grids")]
        public void SaveFriendlyGrids()
        {
            List<CharacterID> _friendlyGridIDs = new();
            foreach (var _item in gridManager.friendlyGrids)
            {
                if (_item.IsHaveCharacter && _item.GetCurrentCharacter().data.iD != CharacterID.HR && _item.GetCurrentCharacter().data.iD != CharacterID.HM)
                    _friendlyGridIDs.Add(_item.GetCurrentCharacter().data.iD);
                else
                    _friendlyGridIDs.Add(CharacterID.None);
            }

            FriendlyGridData = JsonConvert.SerializeObject(_friendlyGridIDs);
        }

        [TabGroup("Friendly Grids")]
        public void LoadFriendlyGrids()
        {
            List<CharacterID> _friendlyGridIDs; ;
            _friendlyGridIDs = JsonConvert.DeserializeObject<List<CharacterID>>(FriendlyGridData);
            if (_friendlyGridIDs == null)
                return;
            for (int _i = 0; _i < _friendlyGridIDs.Count; _i++)
            {
                if (_friendlyGridIDs[_i] == CharacterID.FM0)
                    continue;
                SpawnCharacterInGrid(gridManager.friendlyGrids[_i], _friendlyGridIDs[_i]);
            }
        }
        #endregion

        #region Enemy Function   
        [TabGroup("Enemy Grids")]
        public void AddEnemyGridData()
        {
            List<List<CharacterID>> _tempAllEnemyGridIds = AllEnemyGridIds;
            List<CharacterID> _tempCurrentEnemyGridIds = CurrentEnemyGridIds;
            _tempAllEnemyGridIds.Add(_tempCurrentEnemyGridIds);
            AllEnemyGridIds = _tempAllEnemyGridIds;

            Debug.Log("Added level: " + AllEnemyGridIds.Count);
        }

        [TabGroup("Enemy Grids")]
        public void ModifyEnemyGridData(int _level)
        {
            List<List<CharacterID>> _temp = new();

            foreach (var item in AllEnemyGridIds)
            {
                _temp.Add(item);
            }

            _temp[_level] = CurrentEnemyGridIds;
            AllEnemyGridIds = _temp;
        }

        [TabGroup("Enemy Grids")]
        public void DeleteEnemyGridData(int _level)
        {
            List<List<CharacterID>> _temp = AllEnemyGridIds;
            _temp.Remove(_temp[_level]);
            AllEnemyGridIds = _temp;
        }

        [TabGroup("Enemy Grids")]
        public void LoadEnemyGrids(int _level)
        {
            List<CharacterID> _enemyGridIDs; ;
            _enemyGridIDs = AllEnemyGridIds[Mathf.Clamp(_level - 1, 0, AllEnemyGridIds.Count - 1)];
            if (_enemyGridIDs == null)
                return;
            for (int _i = 0; _i < _enemyGridIDs.Count; _i++)
            {
                SpawnCharacterInGrid(gridManager.enemyGrids[_i], _enemyGridIDs[_i]);
            }
            startTotalEnemyHealth = GetTotalEnemyHealth();
        }

        float GetTotalEnemyHealth()
        {
            float _total = 0;
            foreach (var _character in enemyCharacters)
            {
                _total += _character.CurrentHealth;
            }
            return _total;
        }

        [Button]
        public CharacterData GetHighestEnemyLevelMinusOne()
        {
            int _tempHighestLevel = 0;
            foreach (var _item in gridManager.enemyGrids)
            {
                if (!_item.IsHaveCharacter)
                    continue;

                if (_item.GetCurrentCharacter().data.scaleLevel > _tempHighestLevel)
                    _tempHighestLevel = _item.GetCurrentCharacter().data.scaleLevel;
            }

            if (Helpers.RandomByWeight(new float[] { 50, 50 }) == 0)
            {
                int _level = Mathf.Clamp(_tempHighestLevel - 2, 0, fRCharacterDatas.Count - 1);
                return fRCharacterDatas[_level];
            }
            else
            {
                int _level = Mathf.Clamp(_tempHighestLevel - 1, 1, fMCharacterDatas.Count - 1);
                return fMCharacterDatas[_level];
            }
        }
        #endregion

        public void ClearCharacter()
        {
            EventDispatcher.Instance.Dispatch(EventName.OnClearGrid, null);

            enemyCharacters.Clear();
            friendlyCharacters.Clear();
        }

        public void SpawnCharacterInGrid(Grid _grid, CharacterID _charId)
        {
            if (_grid == null)
                return;

            CharacterData _data = GetCharacterDataByID(_charId);
            if (_data == null)
                return;

            Character _char = null;
            switch (_data.Type)
            {
                case CharacterType.EnemyMelee:
                    _char = Instantiate(enemyMeleeCharacter);
                    break;
                case CharacterType.FriendlyMelee:
                    _char = Instantiate(friendlyMeleeCharacter);
                    break;
                case CharacterType.EnemyRanged:
                    _char = Instantiate(enemyRangedCharacter);
                    break;
                case CharacterType.FriendlyRanged:
                    _char = Instantiate(friendlyRangedCharacter);
                    break;
            }

            _grid.SetCurrentCharacter(_char);

            if (_data.Faction == CharacterFaction.Friendly)
            {
                _char.Init(_data, _grid.transform, Vector3.zero, Quaternion.identity);
                friendlyCharacters.Add(_char);
            }
            else
            {
                _char.Init(_data, _grid.transform, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, 0)));
                enemyCharacters.Add(_char);
            }

        }

        public void SpawnHelpCharacter(CharacterData _charData)
        {
            Grid _grid = GridManager.Instance.GetEmptyFriendlyGrid();
            if (_grid == null)
                return;

            Character _char = null;
            switch (_charData.Type)
            {
                case CharacterType.FriendlyMelee:
                    _char = Instantiate(friendlyMeleeCharacter);
                    _char.Init(GetCharacterDataByID(CharacterID.HM), _grid.transform, Vector3.zero, Quaternion.identity);
                    break;
                case CharacterType.FriendlyRanged:
                    _char = Instantiate(friendlyRangedCharacter);
                    _char.Init(GetCharacterDataByID(CharacterID.HR), _grid.transform, Vector3.zero, Quaternion.identity);
                    break;
            }

            _grid.SetCurrentCharacter(_char);
            friendlyCharacters.Add(_char);


            _char.SetHelperStat(_charData);
        }

        public Character SpawnCharacterWhileRun(Transform _parent, CharacterID _charId)
        {
            CharacterData _data = GetCharacterDataByID(_charId);
            if (_data == null)
                return null;

            Character _char = null;

            switch (_data.Type)
            {
                case CharacterType.EnemyMelee:
                    _char = Instantiate(enemyMeleeCharacter);
                    break;
                case CharacterType.FriendlyMelee:
                    _char = Instantiate(friendlyMeleeCharacter);
                    break;
                case CharacterType.EnemyRanged:
                    _char = Instantiate(enemyRangedCharacter);
                    break;
                case CharacterType.FriendlyRanged:
                    _char = Instantiate(friendlyRangedCharacter);
                    break;
            }
            _char.Init(_data, _parent);
            _char.tag = StringsSafeAccess.TAG_FRIENDLY_CHARACTER;
            return _char;
        }

        public CharacterData GetCharacterDataByID(CharacterID _id)
        {
            CharacterData _char = null;
            foreach (var _item in allCharacterDatas)
            {
                if (_id == _item.iD)
                {
                    _char = _item;
                    break;
                }
            }
            return _char;
        }

        public void CharacterDeadCheck(Character _char)
        {
            if (_char.data.Faction == CharacterFaction.Enemy)
                enemyCharacters.Remove(_char);
            else
                friendlyCharacters.Remove(_char);

            if (GameManager.Instance.State != GameState.VictoryBattle && GameManager.Instance.State != GameState.DefeatBattle)
                if (enemyCharacters.Count == 0)
                    GameManager.Instance.State = GameState.VictoryBattle;

            if (GameManager.Instance.State != GameState.VictoryBattle && GameManager.Instance.State != GameState.DefeatBattle)
                if (friendlyCharacters.Count == 0)
                    GameManager.Instance.State = GameState.DefeatBattle;
        }

        public void ClearCurrentCharacterData()
        {
            enemyCharacters.Clear();
            friendlyCharacters.Clear();
        }

        #region Editor
        [BoxGroup("Data"), Button]
        void LoadAllScriptTableObject()
        {
#if UNITY_EDITOR
            List<CharacterData> _tempAllDatas = new List<CharacterData>();
            List<CharacterData> _tempEmDatas = new List<CharacterData>();
            List<CharacterData> _tempFmDatas = new List<CharacterData>();
            List<CharacterData> _tempErDatas = new List<CharacterData>();
            List<CharacterData> _tempFrDatas = new List<CharacterData>();
            string[] assetPaths = Directory.GetFiles(characterDataFolderPath, "*.asset", SearchOption.AllDirectories);
            foreach (string assetPath in assetPaths)
            {
                CharacterData characterData = UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterData>(assetPath);
                if (characterData != null)
                {
                    _tempAllDatas.Add(characterData);

                    switch (characterData.Type)
                    {
                        case CharacterType.EnemyMelee:
                            _tempEmDatas.Add(characterData);
                            break;
                        case CharacterType.FriendlyMelee:
                            _tempFmDatas.Add(characterData);
                            break;
                        case CharacterType.EnemyRanged:
                            _tempErDatas.Add(characterData);
                            break;
                        case CharacterType.FriendlyRanged:
                            _tempFrDatas.Add(characterData);
                            break;
                    }
                }

                allCharacterDatas = _tempAllDatas;
                eMCharacterDatas = _tempEmDatas;
                fMCharacterDatas = _tempFmDatas;
                eRCharacterDatas = _tempErDatas;
                fRCharacterDatas = _tempFrDatas;
            }
#endif
        }
        #endregion
    }
}