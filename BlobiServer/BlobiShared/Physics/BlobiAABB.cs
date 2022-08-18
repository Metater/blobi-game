using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BlobiShared.Physics
{
    public struct BlobiAABB
    {
        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }

        public BlobiAABB(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public BlobiAABB Offset(Vector2 offset)
        {
            return new BlobiAABB(Min + offset, Max + offset);
        }

        public bool Intersects(BlobiAABB other)
        {
            if (Max.X < other.Min.X || Min.X > other.Max.X)
            {
                return false;
            }
            if (Max.Y < other.Min.Y || Min.Y > other.Max.Y)
            {
                return false;
            }
            return true;
        }

        public bool ContainsPoint(Vector2 point)
        {
            return Min.X <= point.X && Max.X >= point.X && Min.Y <= point.Y && Max.Y >= point.Y;
        }

        public bool CircleFullyWithin(BlobiCircle circle)
        {
            return ContainsPoint(circle.Center + new Vector2(0, circle.Radius)) &&
                ContainsPoint(circle.Center + new Vector2(0, -circle.Radius)) &&
                ContainsPoint(circle.Center + new Vector2(-circle.Radius, 0)) &&
                ContainsPoint(circle.Center + new Vector2(circle.Radius, 0));
        }

        public Vector2 RandomPointInside(Random random)
        {
            float xSample = random.Sample();
            float ySample = random.Sample();
            
            float xDelta = Max.X - Min.X;
            float yDelta = Max.Y - Max.Y;

            float xRand = Min.X + (xDelta * xSample);
            float yRand = Min.Y + (yDelta * ySample);

            return new Vector2(xRand, yRand);
        }
    }
}
