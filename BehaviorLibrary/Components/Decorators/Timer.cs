using System;

namespace BehaviourLibrary.Components.Decorators
{
	public class Timer : BehaviourComponent
	{
		private readonly BehaviourComponent _behaviourComponent;
		private readonly Func<int> _elapsedTimeFunction;

		private readonly int _waitTime;
		private int _timeElapsed;

		/// <summary>
		///     executes the behavior after a given amount of time in miliseconds has passed
		/// </summary>
		/// <param name="elapsedTimeFunction">function that returns elapsed time</param>
		/// <param name="timeToWait">maximum time to wait before executing behavior</param>
		/// <param name="behaviour">behavior to run</param>
		public Timer(string name, Func<int> elapsedTimeFunction, int timeToWait, BehaviourComponent behaviour)
		{
			Name = name;
			_elapsedTimeFunction = elapsedTimeFunction;
			_behaviourComponent = behaviour;
			_waitTime = timeToWait;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			try
			{
				_timeElapsed += _elapsedTimeFunction.Invoke();

				if (_timeElapsed >= _waitTime)
				{
					_timeElapsed = 0;
					ReturnCode = _behaviourComponent.Behave();
					return ReturnCode;
				}
				ReturnCode = BehaviourReturnCode.Running;
				return BehaviourReturnCode.Running;
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.ToString());
#endif
				ReturnCode = BehaviourReturnCode.Failure;
				return BehaviourReturnCode.Failure;
			}
		}
	}
}