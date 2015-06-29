using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.BaseClasses
{
	// DO WE NEED THIS ?????
	// maybe add Draw interface and remove id ???
	public abstract class GameObject
	{
		private int id;

		protected GameObject(int id)
		{
			this.ID = id;
		}		

		public int ID
		{
			get { return id; }
			set { id = value; }
		}
	}
}
