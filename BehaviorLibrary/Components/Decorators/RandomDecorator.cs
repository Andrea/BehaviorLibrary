using System;

namespace BehaviourLibrary.Components.Decorators
{
	public class RandomDecorator : BehaviourComponent
	{
		private readonly BehaviourComponent _behaviourComponent;
		private readonly float _probability;
		private readonly Func<float> _rRandomFunction;

		/// <summary>
		///     randomly executes the behavior
		/// </summary>
		/// <param name="probability">probability of execution</param>
		/// <param name="randomFunction">function that determines probability to execute</param>
		/// <param name="behaviour">behavior to execute</param>
		public RandomDecorator(float probability, Func<float> randomFunction, BehaviourComponent behaviour)
		{
			_probability = probability;
			_rRandomFunction = randomFunction;
			_behaviourComponent = behaviour;
		}


		public override BehaviourReturnCode Behave()
		{
			try
			{
				if (_rRandomFunction.Invoke() <= _probability)
				{
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