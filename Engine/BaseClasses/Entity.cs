using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Interfaces;

	public abstract class Entity// : GameObject, IBaseStats//IMoveable
	{
		private int strength;
		private int dexterity;
		private int intelligence;
		private int vitality;
		
		//
		protected Entity(int strength, int dexterity, int intelligence, int vitality)
		{
			this.Strength = strength;
			this.Dexterity = dexterity;
			this.Intelligence = intelligence;
			this.Vitality = vitality;
		}

		public int Strength
		{
			get { return strength; }
			set { strength = value; }
		}

		public int Dexterity
		{
			get { return dexterity; }
			set { dexterity = value; }
		}

		public int Intelligence
		{
			get { return intelligence; }
			set { intelligence = value; }
		}

		public int Vitality
		{
			get { return vitality; }
			set { vitality = value; }
		}
	}
}
