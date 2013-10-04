//
//  Program.cs
//
//  Author:
//       Thomas H. Jonell <@Net_Gnome>
//
//  Copyright (c) 2013 Thomas H. Jonell
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using CSTester;
using CSLogging;

namespace Tests
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				Tester tester = new Tester();
				tester.registerTestCase(new TestCases());
				tester.initialize();
				Console.Write(tester.getResults());
			}
			catch (Exception e)
			{
				Console.Write(e.ToString());
			}
		}

	}
}
