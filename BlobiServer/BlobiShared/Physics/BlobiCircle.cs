using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BlobiShared.Physics
{
    public struct BlobiCircle
    {
        public Vector2 Center { get; private set; }
        public float Radius { get; private set; }
        public float SqrRadius { get; private set; }

        public BlobiCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
            SqrRadius = radius * radius;
        }

        public BlobiCircle(Vector2 center, float radius, float sqrRadius)
        {
            Center = center;
            Radius = radius;
            SqrRadius = sqrRadius;
        }

        public BlobiCircle Offset(Vector2 offset)
        {
            return new BlobiCircle(Center + offset, Radius, SqrRadius);
        }

        public BlobiCircle Resize(float radius)
        {
            return new BlobiCircle(Center, radius);
        }

        public bool Intersects(BlobiCircle other)
        {
            float sqrCenterDistance = Vector2.DistanceSquared(Center, other.Center);
            float sqrRadiusSum = SqrRadius + other.SqrRadius;
            return sqrCenterDistance < sqrRadiusSum;
        }

        public bool ContainsPoint(Vector2 point)
        {
            float sqrCenterDistance = Vector2.DistanceSquared(Center, point);
            return sqrCenterDistance < SqrRadius;
        }

        public bool FullyWithinAABB(BlobiAABB aabb)
        {
            return aabb.CircleFullyWithin(this);
        }

        public bool CheckNorthPoint(BlobiAABB aabb)
        {
            return Center.Y + Radius > aabb.Max.Y;
        }
        public bool CheckSouthPoint(BlobiAABB aabb)
        {
            return Center.Y - Radius < aabb.Min.Y;
        }
        public bool CheckWestPoint(BlobiAABB aabb)
        {
            return Center.X - Radius < aabb.Min.X;
        }
        public bool CheckEastPoint(BlobiAABB aabb)
        {
            return Center.X + Radius > aabb.Max.X;
        }
    }
}
