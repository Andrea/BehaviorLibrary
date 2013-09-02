using System;

namespace BehaviorLibrary.Components.Decorators
{
	public class RandomDecorator : BehaviorComponent
	{
		private readonly BehaviorComponent r_Behavior;
		private readonly float r_Probability;

		private readonly Func<float> r_RandomFunction;

		/// <summary>
		///     randomly executes the behavior
		/// </summary>
		/// <param name="probability">probability of execution</param>
		/// <param name="randomFunction">function that determines probability to execute</param>
		/// <param name="behavior">behavior to execute</param>
		public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior)
		{
			r_Probability = probability;
			r_RandomFunction = randomFunction;
			r_Behavior = behavior;
		}


		public override BehaviorReturnCode Behave()
		{
			try
			{
				if (r_RandomFunction.Invoke() <= r_Probability)
				{
					ReturnCode = r_Behavior.Behave();
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