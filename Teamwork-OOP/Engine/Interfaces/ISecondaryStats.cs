using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Interfaces
{
	interface ISecondaryStats
	{
		// private setter ??

		int AttackDamage { get; set; }
		int SpellDamage { get; set; }

		int Armor { get; set; }  //Damage reduction from armor shouldnt exceed 0.00 - 0.25.
		int MagicResistance { get; set; }//Same as armor.
		// TODO: EnergyShield prop ?

		float AttackSpeed { get; set; }
		float SpellCastingSpeed { get; set; }
		float MovementSpeed { get; set; }

		int HealthPoints { get; set; }
		int ManaPoints { get; set; }

		float AttackRange { get; set; }

		float CriticalHit { get; set; }
		float CriticalDamage { get; set; }
	}
}