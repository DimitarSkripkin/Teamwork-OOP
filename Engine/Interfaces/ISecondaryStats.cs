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

		int AttackSpeed { get; set; }
		int MovementSpeed { get; set; }
	}
}
