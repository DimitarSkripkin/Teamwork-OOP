using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	static class DistanceChecker
	{
		public static float GetDistanceBetweenCollisionHulls(CollisionShape itemA, CollisionShape itemB)
		{
			if (itemA is Circle)
			{
				if (itemB is Circle)
				{
					return GetDistanceBetweenCircles(itemA as Circle, itemB as Circle);
				}

				return GetDistanceBetweenCircleAndAABB(itemA as Circle, itemB as AABB);
			}

			if (itemB is Circle)
			{
				return GetDistanceBetweenCircleAndAABB(itemB as Circle, itemA as AABB);
			}

			return GetDistanceBetweenAABB(itemA as AABB, itemB as AABB);
		}

		// if is negative then there is collision
		public static float GetDistanceBetweenCircles(Circle circleA, Circle circleB)
		{
			return GetDistanceBetweenPointAndCircle(circleA.Position, circleB) - circleA.Radius;
		}

		// if is negative then there is collision
		public static float GetDistanceBetweenAABB(AABB boxA, AABB boxB)
		{
			Vector2 boxACenter = boxA.Max - boxA.Min;
			float[] distances = new float[]{
				(boxA.Min-boxACenter).Length(),
				(boxA.Max-boxACenter).Length(),

				(new Vector2( boxA.Min.X, boxA.Max.Y) - boxACenter).Length(),
				(new Vector2( boxA.Max.X, boxA.Min.Y) - boxACenter).Length()
			};

			float minDistance = distances.Min();

			if (CollisionChecker.CheckForAABBCollision(boxA, boxB))
			{
				return -minDistance;
			}

			return minDistance;
		}

		// if is negative then there is collision
		public static float GetDistanceBetweenCircleAndAABB(Circle circle, AABB box)
		{
			float minDistance = float.MaxValue;

			Vector2[] AABBPoints = new Vector2[]{
				box.Min,
				box.Max,
				new Vector2( box.Min.X, box.Max.Y),
				new Vector2( box.Max.X, box.Min.Y)
			};

			foreach (var point in AABBPoints)
			{
				var currentDistance = GetDistanceBetweenPointAndCircle(point, circle);

				if (currentDistance < minDistance)
				{
					minDistance = currentDistance;
				}
			}

			if (CollisionChecker.CheckForCircleWithAABB(circle, box))
			{
				return -minDistance;
			}
			return minDistance;
		}

		public static float GetDistanceBetweenPointAndClosesedAABBCorner(Vector2 point, AABB box)
		{
			float minDistance = float.MaxValue;

			Vector2[] AABBPoints = new Vector2[]{
				box.Min,
				box.Max,
				new Vector2( box.Min.X, box.Max.Y),
				new Vector2( box.Max.X, box.Min.Y)
			};

			foreach (var p in AABBPoints)
			{
				var currentDistance = GetDistanceBetweenPoints(p, point);

				if (currentDistance < minDistance)
				{
					minDistance = currentDistance;
				}
			}

			return minDistance;
		}

		// if is negative then there is collision
		public static float GetDistanceBetweenPointAndCircle(Vector2 point, Circle circle)
		{
			return (float)Math.Sqrt(
				(circle.Position.X - point.X) * (circle.Position.X - point.X)
					+ (circle.Position.Y - point.Y) * (circle.Position.Y - point.Y))
				- circle.Radius;
		}

		public static float GetDistanceBetweenPoints(Vector2 pointA, Vector2 pointB)
		{
			return (float)Math.Sqrt((pointA.X - pointB.Y) + (pointA.X - pointB.Y));
		}
	}
}
