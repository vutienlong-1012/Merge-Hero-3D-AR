using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class GridManager : SerializedMonoBehaviour
    {
        [SerializeField] int rows = 5;
        [SerializeField] int columns = 4;
        [SerializeField] float spacing = 1.0f;

        [SerializeField] Transform friendlyGridsParent;
        [SerializeField] Transform enemyGridsParent;
        [SerializeField] FriendlyGrid friendlyGridPrefab;
        [SerializeField] EnemyGrid enemyGridPrefab;

        [ReadOnly] public List<FriendlyGrid> friendlyGrids;
        [ReadOnly] public List<EnemyGrid> enemyGrids;

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
            _grid = Instantiate(_grid);

            // set the cube as a child of this game object (for organization purposes)
            _grid.transform.parent = _parent;
            _grid.transform.localPosition = _localPosition;
            _grid.transform.rotation = Quaternion.identity;
            _grid.name = _grid.name + _col + _row;
            return _grid;
        }
    }
}