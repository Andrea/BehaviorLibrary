using System;

namespace BehaviorLibrary.Components.Decorators
{
	public class Counter : BehaviorComponent
	{
		private readonly BehaviorComponent c_Behavior;
		private readonly int c_MaxCount;
		private int c_Counter;

		/// <summary>
		///     executes the behavior based on a counter
		///     -each time Counter is called the counter increments by 1
		///     -Counter executes the behavior when it reaches the supplied maxCount
		/// </summary>
		/// <param name="maxCount">max number to count to</param>
		/// <param name="behavior">behavior to run</param>
		public Counter(int maxCount, BehaviorComponent behavior)
		{
			c_MaxCount = maxCount;
			c_Behavior = behavior;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			try
			{
				if (c_Counter < c_MaxCount)
				{
					c_Counter++;
					ReturnCode = BehaviorReturnCode.Running;
					return BehaviorReturnCode.Running;
				}
				c_Counter = 0;
				ReturnCode = c_Behavior.Behave();
				return ReturnCode;
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.ToString());
#endif
				ReturnCode = BehaviorReturnCode.Failure;
				return BehaviorReturnCode.Failure;
			}
		}
	}
}