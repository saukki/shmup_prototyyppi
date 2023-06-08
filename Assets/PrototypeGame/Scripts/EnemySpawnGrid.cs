using System;
using PrototypeGame.Scripts.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace PrototypeGame.Scripts
{
    [RequireComponent(typeof(Grid), typeof(EnemySpawnController))]
    public class EnemySpawnGrid : MonoBehaviour
    {
        private static readonly Vector3Int[] _locations =
        {
            new(0, 0, 0),
            new(1, 0, 0),
            new(2, 0, 0),
            new(3, 0, 0),
            new(4, 0, 0),
        };

        public bool IsVisibleInCamera;
        [SerializeField] private Grid _grid;

        private void Awake() => InitGrid();

        private void OnValidate() => transform.position =
            IsVisibleInCamera is false ? new Vector3(-10, 0, 25) : new Vector3(-10, 0, 20);

        private void InitGrid()
        {
            _grid = GetComponent<Grid>();
            Assert.IsNotNull(_grid);

            _grid.cellSize = new Vector3(3, 0, 1);
            _grid.cellGap = new Vector3(1, 0, 0);
            _grid.cellLayout = GridLayout.CellLayout.Rectangle;
            _grid.cellSwizzle = GridLayout.CellSwizzle.XYZ;
        }

        public Vector3 EnemySpawnPosition(EnemySpawn spawnPosition)
        {
            return spawnPosition switch
            {
                EnemySpawn.LeftLeft => LocationLeftLeft(),
                EnemySpawn.Left => LocationLeft(),
                EnemySpawn.Center => LocationCenter(),
                EnemySpawn.Right => LocationRight(),
                EnemySpawn.RightRight => LocationRightRight(),
                _ => throw new ArgumentOutOfRangeException(nameof(spawnPosition), spawnPosition, null)
            };
        }

        private Vector3 LocationLeftLeft() => _grid.GetCellCenterWorld(_locations[(int)EnemySpawn.LeftLeft]);
        private Vector3 LocationLeft() => _grid.GetCellCenterWorld(_locations[(int)EnemySpawn.Left]);
        private Vector3 LocationCenter() => _grid.GetCellCenterWorld(_locations[(int)EnemySpawn.Center]);
        private Vector3 LocationRight() => _grid.GetCellCenterWorld(_locations[(int)EnemySpawn.Right]);
        private Vector3 LocationRightRight() => _grid.GetCellCenterWorld(_locations[(int)EnemySpawn.RightRight]);
    }
}