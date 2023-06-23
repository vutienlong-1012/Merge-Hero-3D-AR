using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VTLTools;
using System;
using DG.Tweening;

namespace MergeAR
{
    [Serializable]
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] int rows = 5;
        [SerializeField] int columns = 4;
        [SerializeField] float spacing = 1.0f;

        [TabGroup("Enemy Grids"), SerializeField] Transform enemyGridsParent;
        [TabGroup("Enemy Grids"), SerializeField] EnemyGrid enemyGridPrefab;
        [TabGroup("Enemy Grids"), ReadOnly] public List<EnemyGrid> enemyGrids;
        //[TabGroup("Enemy Grids"), SerializeField] Material enemyGridMaterial;

        [TabGroup("Friendly Grids"), SerializeField] Transform friendlyGridsParent;
        [TabGroup("Friendly Grids"), SerializeField] FriendlyGrid friendlyGridPrefab;
        [TabGroup("Friendly Grids"), ReadOnly] public List<FriendlyGrid> friendlyGrids;
        //[TabGroup("Friendly Grids"), SerializeField] Material friendlyGridMaterial;

        //Color defaultEnemyGridColor;
        //Color defaultFriendGridColor;

        private void Start()
        {
            //friendlyGridMaterial.color = Color.white;
            //friendlyGridMaterial.DOFade(0, 0);

            //    defaultEnemyGridColor = enemyGridMaterial.color;
            //    defaultFriendGridColor = friendlyGridMaterial.color;
        }


        #region Common Function     
        [Button]
        public Grid GetEmptyFriendlyGrid()
        {

            foreach (Grid _item in friendlyGrids)
            {
                if (!_item.IsHaveCharacter)
                    return _item;

            }
            return null; // Return a default value when no empty grid is found
        }

        public void CheckFriendlyGridFull()
        {
            int _emptyCount = 0;
            foreach (Grid _item in friendlyGrids)
            {
                if (!_item.IsHaveCharacter)
                    _emptyCount++;
            }
            EventDispatcher.Instance.Dispatch(EventName.OnFriendlyGridFull, _emptyCount == 0);
        }

        public bool IsFriendlyGridFull()
        {
            int _emptyCount = 0;
            foreach (Grid _item in friendlyGrids)
            {
                if (!_item.IsHaveCharacter)
                    _emptyCount++;
            }
            return _emptyCount == 0;
        }
        #endregion

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


            Helpers.DestroyAllChilds(friendlyGridsParent);
            Helpers.DestroyAllChilds(enemyGridsParent);

            // loop through each row and column, and create a cube at the appropriate position
            for (int _row = 0; _row < rows; _row++)
            {
                for (int _col = columns - 1; _col >= 0; _col--)
                {
                    // calculate the position of the current cube based on its row and column
                    float _x;
                    if (_row % 2 == 0)
                        _x = _startX + _col * spacing + spacing / 4;
                    else
                        _x = _startX + _col * spacing - spacing / 4;

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
#if UNITY_EDITOR
            // instantiate a new cube at the calculated position
            _grid = (Grid)PrefabUtility.InstantiatePrefab(_grid);
#else
            _grid = (Grid)Instantiate(_grid);
#endif
            _grid.Init(_parent, _localPosition, Quaternion.identity, _col, _row);
            return _grid;
        }

        public void ClearGridReference()
        {
            foreach (var item in enemyGrids)
            {
                item.ClearGrid();
            }

            foreach (var item in friendlyGrids)
            {
                item.ClearGrid();
            }
        }

        private void OnApplicationQuit()
        {
            //enemyGridMaterial.color = defaultEnemyGridColor;
            //friendlyGridMaterial.color = defaultFriendGridColor;
        }
        #endregion
    }
}