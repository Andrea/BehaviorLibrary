using System;

namespace BehaviorLibrary.Components.Decorators
{
	public class RandomDecorator : BehaviorComponent
	{
		private readonly BehaviorComponent _rBehavior;
		private readonly float _rProbability;
		private readonly Func<float> _rRandomFunction;

		/// <summary>
		///     randomly executes the behavior
		/// </summary>
		/// <param name="probability">probability of execution</param>
		/// <param name="randomFunction">function that determines probability to execute</param>
		/// <param name="behavior">behavior to execute</param>
		public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
		{
			_rProbability = probability;
			_rRandomFunction = randomFunction;
			_rBehavior = behavior;
		}


		public override BehaviorReturnCode Behave()
		{
			try
			{
				if (_rRandomFunction.Invoke() <= _rProbability)
				{
					ReturnCode = _rBehavior.Behave();
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