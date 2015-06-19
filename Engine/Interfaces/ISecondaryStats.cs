using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Interfaces
{
	interface ISecondaryStats
	{
		int AttackDamage { get; set; }
		int SpellDamage { get; set; }

		int Arrmor { get; set; }
		int MagicResistance { get; set; }
		// TODO: EnergyShield prop ?

		int AttackSpeed { get; set; }
		int SpellCastingSpeed { get; set; }
		int MovementSpeed { get; set; }

		int HealthPoints { get; set; }
		int ManaPoints { get; set; }

		int AttackRange { get; set; }

		int CriticalHit { get; set; }
		int CriticalDamage { get; set; }
	}
}
