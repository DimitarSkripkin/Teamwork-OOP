﻿using System;

namespace Teamwork_OOP
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class MainEntry
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			using (var game = new Game1())
			{
				game.Run();
			}
        }
    }
#endif
}
