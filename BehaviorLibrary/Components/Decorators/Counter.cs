using System;

namespace BehaviorLibrary.Components.Decorators
{
	public class Counter : BehaviorComponent
	{
		private readonly BehaviorComponent _behavior;
		private readonly int _maxCount;
		private int _counter;

		/// <summary>
		///     executes the behavior based on a counter
		///     -each time Counter is called the counter increments by 1
		///     -Counter executes the behavior when it reaches the supplied maxCount
		/// </summary>
		/// <param name="maxCount">max number to count to</param>
		/// <param name="behavior">behavior to run</param>
		public Counter(int maxCount, BehaviorComponent behavior)
		{
			_maxCount = maxCount;
			_behavior = behavior;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			try
			{
				if (_counter < _maxCount)
				{
					_counter++;
					ReturnCode = BehaviorReturnCode.Running;
					return BehaviorReturnCode.Running;
				}
				_counter = 0;
				ReturnCode = _behavior.Behave();
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