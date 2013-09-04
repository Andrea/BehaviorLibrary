using System;

namespace BehaviorLibrary.Components.Decorators
{
	public class Timer : BehaviorComponent
	{
		private readonly BehaviorComponent _behaviorComponent;
		private readonly Func<int> _elapsedTimeFunction;

		private readonly int _waitTime;
		private int _timeElapsed;

		/// <summary>
		///     executes the behavior after a given amount of time in miliseconds has passed
		/// </summary>
		/// <param name="elapsedTimeFunction">function that returns elapsed time</param>
		/// <param name="timeToWait">maximum time to wait before executing behavior</param>
		/// <param name="behavior">behavior to run</param>
		public Timer(Func<int> elapsedTimeFunction, int timeToWait, BehaviorComponent behavior)
		{
			_elapsedTimeFunction = elapsedTimeFunction;
			_behaviorComponent = behavior;
			_waitTime = timeToWait;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			try
			{
				_timeElapsed += _elapsedTimeFunction.Invoke();

				if (_timeElapsed >= _waitTime)
				{
					_timeElapsed = 0;
					ReturnCode = _behaviorComponent.Behave();
					return ReturnCode;
				}
				ReturnCode = BehaviorReturnCode.Running;
				return BehaviorReturnCode.Running;
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