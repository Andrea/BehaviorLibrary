using System;
using System.Diagnostics;

namespace BehaviourTreeExample
{
	public class Program
	{
	

		static void Main(string[] args)
		{
			var draggonScenario = new DraggonScenario();
			draggonScenario.Setup();
			while (true)
			{
				draggonScenario.Behave();
			}
		}
	}

	public static class Helper
	{
		public static T LogAndReturn<T>(T code)
		{
			Console.Write(code);
			Console.Write("  --  Called from {0}", new StackTrace().GetFrame(1).GetMethod().Name);
			Console.WriteLine();
			return code;
		}
	}
}
