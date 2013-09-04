using System;

namespace BehaviorLibrary.Components.Decorators
{
	public class RandomDecorator : BehaviorComponent
	{
		private readonly BehaviorComponent _behaviorComponent;
		private readonly float _probability;
		private readonly Func<float> _rRandomFunction;

		/// <summary>
		///     randomly executes the behavior
		/// </summary>
		/// <param name="probability">probability of execution</param>
		/// <param name="randomFunction">function that determines probability to execute</param>
		/// <param name="behavior">behavior to execute</param>
		public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
		{
			_probability = probability;
			_rRandomFunction = randomFunction;
			_behaviorComponent = behavior;
		}


		public override BehaviorReturnCode Behave()
		{
			try
			{
				if (_rRandomFunction.Invoke() <= _probability)
				{
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