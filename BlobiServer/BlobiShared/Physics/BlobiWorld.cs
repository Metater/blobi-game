using BlobiShared.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BlobiShared.Physics
{
    public class BlobiWorld
    {
        public uint CellSize { get; private set; }
        public uint HalfCellSize { get; private set; }
        public uint GridSize { get; private set;}
        public uint HalfGridSize { get; private set; }

        private readonly IdGen entityIdGen = new IdGen();

        private readonly Dictionary<uint, BlobiEntity> entities = new Dictionary<uint, BlobiEntity>();
        private readonly Dictionary<uint, HashSet<uint>> cells = new Dictionary<uint, HashSet<uint>>();


        public BlobiWorld(uint cellSize, uint gridSize)
        {
            CellSize = cellSize;
            HalfCellSize = cellSize / 2;
            GridSize = gridSize;
            HalfGridSize = gridSize / 2;
        }

        #region Entity Management
        public BlobiEntity SpawnEntity(Vector2 position, float circleRadius)
        {
            uint id = entityIdGen.GetNext();
            BlobiEntity entity = new BlobiEntity(this, id, position, circleRadius);
            uint cellIndex = entity.CurrentCellIndex;

            MoveEntityIntoCell(id, cellIndex);

            entities.Add(cellIndex, entity);
            return entity;
        }
        public void DespawnEntity(BlobiEntity entity)
        {
            if (entities.Remove(entity.Id))
            {
                if (cells.TryGetValue(entity.CurrentCellIndex, out var cell))
                {
                    cell.Remove(entity.Id);
                }
            }
        }
        #endregion Entity Management

        public void Tick(float deltaTime)
        {
            foreach (var entity in entities.Values)
            {
                if (entity.Tick(deltaTime, out uint lastCellIndex, out uint nextCellIndex))
                {
                    if (cells.TryGetValue(lastCellIndex, out var lastCell))
                    {
                        lastCell.Remove(entity.Id);
                    }
                    MoveEntityIntoCell(entity.Id, nextCellIndex);
                }
            }
        }

        #region Cell Math
        public uint GetIndexAtPosition(Vector2 position)
        {
            return GetIndexAtIntCoords(GetIntCoordsAtPosition(position));
        }
        public uint GetIndexAtIntCoords((uint X, uint Y) coords)
        {
            return (GridSize * coords.Y) + coords.X;
        }
        public (uint X, uint Y) GetIntCoordsAtIndex(uint index)
        {
            return (index % GridSize, index / GridSize);
        }
        public (uint X, uint Y) GetIntCoordsAtPosition(Vector2 position)
        {
            return (GetIntCoord(position.X), GetIntCoord(position.Y));
        }
        public uint GetIntCoord(float dimension)
        {
            return ((uint)dimension / CellSize) + HalfGridSize;
        }
        public Vector2 GetCellCenterPosition(uint index)
        {
            Vector2 cellPos = GetCellPosition(index);
            return new Vector2(cellPos.X + HalfCellSize, cellPos.Y + HalfCellSize);
        }
        public Vector2 GetCellPosition(uint index)
        {
            float x = (((float)index % GridSize) - HalfGridSize) * CellSize;
            float y = (((float)index / GridSize) - HalfGridSize) * CellSize;
            return new Vector2(x, y);
        }
        #endregion Cell Math

        private void MoveEntityIntoCell(uint entityId, uint cellIndex)
        {
            if (cells.TryGetValue(cellIndex, out var cell))
            {
                cell.Add(entityId);
            }
            else
            {
                cell = new HashSet<uint> { entityId };
                cells.Add(cellIndex, cell);
            }
        }
    }
}
