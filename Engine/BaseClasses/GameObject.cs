using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.BaseClasses
{
	public abstract class GameObject
	{
		private Vector2 position;
		private int id;

		public GameObject( Vector2 position, int id )
		{
			this.Position = position;
			this.ID = id;
		}
		
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public int ID
		{
			get { return id; }
			set { id = value; }
		}
	}
}
