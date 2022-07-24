using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BlobiShared.Physics
{
    public class BlobiEntity
    {
        private readonly BlobiWorld world;

        public uint Id { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; } = Vector2.Zero;

        public BlobiCircle Circle { get; private set; }

        public uint CurrentCellIndex { get; private set; }

        public bool HasBounds { get; private set; } = false;
        public BlobiAABB Bounds { get; private set; }
        public float BoundedCollisionElasticity { get; private set; }

        public bool HasVelocityEpsilon { get; private set; } = false;
        public float VelocityEpsilon { get; private set; }

        public bool HasDrag { get; private set; } = false;
        public float DragForce { get; private set; }

        public bool IsSleeping { get; private set; } = false;

        public BlobiEntity(BlobiWorld world, uint id, Vector2 position, float circleRadius)
        {
            this.world = world;

            Id = id;
            Position = position;
            Circle = new BlobiCircle(Vector2.Zero, circleRadius);

            CurrentCellIndex = world.GetIndexAtPosition(position);
        }

        #region Builder Methods
        public BlobiEntity WithBounds(bool hasBounds, BlobiAABB bounds, float boundedCollisionElasticity)
        {
            HasBounds = hasBounds;
            Bounds = bounds;
            BoundedCollisionElasticity = boundedCollisionElasticity;
            return this;
        }
        public BlobiEntity WithVelocityEpsilon(bool hasVelocityEpsilon, float velocityEpsilon)
        {
            HasVelocityEpsilon = hasVelocityEpsilon;
            VelocityEpsilon = velocityEpsilon;
            return this;
        }
        public BlobiEntity WithDrag(bool hasDrag, float dragForce)
        {
            HasDrag = hasDrag;
            DragForce = dragForce;
            return this;
        }
        #endregion Builder Methods

        public void SetSleeping(bool state)
        {
            IsSleeping = state;
        }
        public void AddForce(Vector2 force, float deltaTime = 1)
        {
            Velocity += force * new Vector2(deltaTime);
        }
        public bool Tick(float deltaTime, out uint lastCellIndex, out uint nextCellIndex)
        {
            if (!IsSleeping)
            {
                ApplyVelocityEpsilon();

                if (Velocity != Vector2.Zero && Move(deltaTime))
                {
                    ApplyDrag(deltaTime);

                    nextCellIndex = world.GetIndexAtPosition(Position);
                    if (CurrentCellIndex != nextCellIndex)
                    {
                        lastCellIndex = CurrentCellIndex;
                        CurrentCellIndex = nextCellIndex;
                        return true;
                    }
                }
            }

            lastCellIndex = default;
            nextCellIndex = default;
            return false;
        }

        private void ApplyVelocityEpsilon()
        {
            if (!HasVelocityEpsilon)
            {
                return;
            }

            if (Velocity.X != 0 && Math.Abs(Velocity.X) < VelocityEpsilon)
            {
                Velocity = new Vector2(0, Velocity.Y);
            }
            if (Velocity.Y != 0 && Math.Abs(Velocity.Y) < VelocityEpsilon)
            {
                Velocity = new Vector2(Velocity.X, 0);
            }
        }
        private void ApplyDrag(float deltaTime)
        {
            if (!HasDrag)
            {
                return;
            }

            Velocity *= 1f - (DragForce * deltaTime);
            ApplyVelocityEpsilon();
        }

        private bool Move(float deltaTime)
        {
            var nominalPos = Position + (Velocity * new Vector2(deltaTime));

            if (HasBounds)
            {
                var nominalCircle = Circle.Offset(nominalPos);

                if (Velocity.X != 0)
                {
                    if ((nominalCircle.CheckWestPoint(Bounds) && Velocity.X < 0) ||
                        (nominalCircle.CheckEastPoint(Bounds) && Velocity.X > 0))
                    {
                        Velocity = new Vector2(Velocity.X * -BoundedCollisionElasticity, Velocity.Y);
                    }
                }

                if (Velocity.Y != 0)
                {
                    if ((nominalCircle.CheckNorthPoint(Bounds) && Velocity.Y > 0) ||
                        (nominalCircle.CheckSouthPoint(Bounds) && Velocity.Y < 0))
                    {
                        Velocity = new Vector2(Velocity.X, Velocity.Y * -BoundedCollisionElasticity);
                    }
                }

                Position += Velocity * new Vector2(deltaTime);
                return Velocity != Vector2.Zero;
            }
            else
            {
                Position = nominalPos;
                return true;
            }
        }
    }
}
