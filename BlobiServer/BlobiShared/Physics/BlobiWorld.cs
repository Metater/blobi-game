using BlobiShared.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BlobiShared.Physics
{
    public class BlobiWorld
    {
        public int CellSize { get; private set; }
        public int GridSize { get; private set;}
        public int HalfGridSize { get; private set; }

        private readonly IdGen entityIdGen = new IdGen();

        private readonly Dictionary<uint, BlobiEntity> entities = new Dictionary<uint, BlobiEntity>();
        private readonly Dictionary<uint, HashSet<uint>> cells = new Dictionary<uint, HashSet<uint>>();


        public BlobiWorld(int cellSize, int gridSize)
        {
            CellSize = cellSize;
            GridSize = gridSize;
            HalfGridSize = GridSize / 2;
        }

        public void Tick(float deltaTime)
        {
            foreach (var entity in entities)
            {

            }
        }

        public Vector2 GetCellPos(uint index)
        {
            float x = (((float)index % GridSize) - (GridSize / 2f))
        }
    }
}
