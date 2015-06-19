using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	public static class CollisionChecker
	{
		public static bool CheckForCollision(CollisionShape itemA, CollisionShape itemB)
		{
			if (itemA is Circle)
			{
				if (itemB is Circle)
				{
					return CheckForCircleCollision(itemA as Circle, itemB as Circle);
				}

				return CheckForCircleWithAABB(itemA as Circle, itemB as AABB);
			}

			if (itemB is Circle)
			{
				return CheckForCircleWithAABB(itemB as Circle, itemA as AABB);
			}

			return CheckForAABBCollision(itemA as AABB, itemB as AABB);
		}

		public static bool CheckForAABBCollision(AABB boxA, AABB boxB)
		{
			return boxA.Max.X >= boxB.Min.X && boxA.Min.X <= boxB.Max.X
				&& boxA.Max.Y >= boxB.Min.Y && boxA.Min.Y <= boxB.Max.Y;
		}

		public static bool CheckForCircleWithAABB(Circle circle, AABB box)
		{
			bool outsideX = circle.Position.X > box.Max.X || circle.Position.X < box.Min.X;
			bool outsideY = circle.Position.Y > box.Max.Y || circle.Position.Y < box.Min.Y;

			if (outsideX && outsideY)
			{
				return IsPointInsideCircle(box.Min, circle)
					|| IsPointInsideCircle(box.Max, circle)
					|| IsPointInsideCircle(new Vector2(box.Min.X, box.Max.Y), circle)
					|| IsPointInsideCircle(new Vector2(box.Max.X, box.Min.Y), circle);
			}

			return CheckForAABBCollision(new AABB(circle.Position, new Vector2(-circle.Radius), new Vector2(circle.Radius), circle.ObjectFlags), box);
		}

		public static bool CheckForCircleCollision(Circle circleA, Circle circleB)
		{
			return DistanceChecker.GetDistanceBetweenCircles(circleA, circleB) <= 0;
		}

		public static bool IsPointInsideAABB(Vector2 point, AABB box)
		{
			return point.X >= box.Min.X && point.X <= box.Max.X
				&& point.Y >= box.Min.Y && point.Y <= box.Max.Y;
		}

		public static bool IsPointInsideCircle(Vector2 point, Circle circle)
		{
			return DistanceChecker.GetDistanceBetweenPointAndCircle(point, circle) <= 0;
		}
	}
}
