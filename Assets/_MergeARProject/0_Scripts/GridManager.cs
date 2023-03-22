using Sirenix.OdinInspector;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VTLTools;
using System;
using Newtonsoft.Json;
using UnityEngine.UIElements;

namespace MergeAR
{
    [Serializable]
    public class GridManager : SerializedMonoBehaviour
    {
        [SerializeField] int rows = 5;
        [SerializeField] int columns = 4;
        [SerializeField] float spacing = 1.0f;
        [SerializeField] Character characterPrefab;

        [TabGroup("Friendly Grids"), SerializeField] Transform friendlyGridsParent;
        [TabGroup("Friendly Grids"), SerializeField] FriendlyGrid friendlyGridPrefab;
        [TabGroup("Friendly Grids"), ReadOnly] public List<FriendlyGrid> friendlyGrids;
        [ShowInInspector, ReadOnly, TabGroup("Friendly Grids")]
        string friendlyGridData
        {
            get
            {
                return PlayerPrefs.GetString(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA, "{}");
            }
            set
            {
                PlayerPrefs.SetString(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA, value);
            }
        }


        [TabGroup("Enemy Grids"), SerializeField] Transform enemyGridsParent;
        [TabGroup("Enemy Grids"), SerializeField] EnemyGrid enemyGridPrefab;
        [TabGroup("Enemy Grids"), ReadOnly] public List<EnemyGrid> enemyGrids;
        [TabGroup("Enemy Grids"), ShowInInspector]
        string enemyGridData;


        [Button]
        private void SaveFriendlyGrids()
        {
            List<CharacterID> _friendlyGridIDs = new();
            foreach (var _item in friendlyGrids)
            {
                if (_item.currentCharacter != null)
                    _friendlyGridIDs.Add(_item.currentCharacter.data.iD);
                else
                    _friendlyGridIDs.Add(CharacterID.None);
            }

            string _jsonStr = JsonConvert.SerializeObject(_friendlyGridIDs);

            PlayerPrefs.SetString(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA, _jsonStr);
        }

        [Button]
        private void LoadFriendlyGrids()
        {
            if (PlayerPrefs.HasKey(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA))
            {
                List<CharacterID> _friendlyGridIDs = new();
                _friendlyGridIDs = JsonConvert.DeserializeObject<List<CharacterID>>(PlayerPrefs.GetString(StringsSafeAccess.PREF_KEY_FRIENDLY_GRIDS_DATA, "{}"));
                for (int _i = 0; _i < _friendlyGridIDs.Count; _i++)
                {
                    CharacterData _charData = DataManager.Instance.GetCharacterDataByID(_friendlyGridIDs[_i]);
                    if (_charData != null)
                    {
                        SpawnCharacterInGrid(friendlyGrids[_i], _charData);
                    }
                }
                Debug.Log("List loaded!");
            }
            else
                Debug.LogError("File not found!");
        }

        void SpawnCharacterInGrid(Grid _grid, CharacterData _charData)
        {
            _grid.currentCharacter = Instantiate(characterPrefab);
            _grid.currentCharacter.transform.SetParent(_grid.transform);
            _grid.currentCharacter.data = _charData;
            _grid.currentCharacter.transform.localPosition = Vector3.zero;
        }

        #region Editor function
        [Button]
        public void SpawnAllGrid()
        {
            //Clear List
            friendlyGrids.Clear();
            enemyGrids.Clear();

            // calculate the total size of the grid based on the number of rows, columns, and spacing
            float _width = columns * spacing;
            float _height = rows * spacing;

            // calculate the starting position for the grid so it is centered in the scene
            float _startX = -_width / 2 + spacing / 2;
            float _startZ = -_height / 2 + spacing / 2;


            Helpers.DestroyAllChilds(friendlyGridsParent.gameObject);
            Helpers.DestroyAllChilds(enemyGridsParent.gameObject);

            // loop through each row and column, and create a cube at the appropriate position
            for (int _row = 0; _row < rows; _row++)
            {
                for (int _col = 0; _col < columns; _col++)
                {
                    // calculate the position of the current cube based on its row and column
                    float _x = _startX + _col * spacing;
                    float _z = _startZ + _row * spacing;

                    // instantiate a new cube at the calculated position
                    Grid _friendlyGrid = SpawnSingleGrid(friendlyGridPrefab, new Vector3(_x, 0, _z), friendlyGridsParent, _col, _row);
                    friendlyGrids.Add(_friendlyGrid as FriendlyGrid);

                    Grid _enemyGrid = SpawnSingleGrid(enemyGridPrefab, new Vector3(_x, 0, _z), enemyGridsParent, _col, _row);
                    enemyGrids.Add(_enemyGrid as EnemyGrid);
                }
            }
        }

        Grid SpawnSingleGrid(Grid _grid, Vector3 _localPosition, Transform _parent, int _col, int _row)
        {
            // instantiate a new cube at the calculated position
            _grid = (Grid)PrefabUtility.InstantiatePrefab(_grid);

            // set the cube as a child of this game object (for organization purposes)
            _grid.transform.parent = _parent;
            _grid.transform.localPosition = _localPosition;
            _grid.transform.rotation = Quaternion.identity;
            _grid.name = _grid.name + _col + _row;
            return _grid;
        }
        #endregion
    }
}