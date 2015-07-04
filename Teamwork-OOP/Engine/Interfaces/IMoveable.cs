using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Interfaces
{
	interface IMoveable
	{
		Vector2 Position { get; }
		void GoToPosition(Vector2 position);//, int speed );

		void Move(Vector2 direction);
		void Jump();
	}
}
