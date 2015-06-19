using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Interfaces
{
	interface IBaseStats
	{
		int Strength { get; set; }

		int Dexterity { get; set; }

		int Intelligence { get; set; }

		int Vitality { get; set; }
	}
}
