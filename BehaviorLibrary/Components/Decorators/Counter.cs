using System;

namespace BehaviourLibrary.Components.Decorators
{
	public class Counter : BehaviourComponent
	{
		private readonly BehaviourComponent _behaviour;
		private readonly int _maxCount;
		private int _counter;

		/// <summary>
		///     executes the behavior based on a counter
		///     -each time Counter is called the counter increments by 1
		///     -Counter executes the behavior when it reaches the supplied maxCount
		/// </summary>
		/// <param name="maxCount">max number to count to</param>
		/// <param name="behaviour">behavior to run</param>
		public Counter(int maxCount, BehaviourComponent behaviour) : this("", maxCount, behaviour)
		{
		}

		/// <summary>
		///     executes the behavior based on a counter
		///     -each time Counter is called the counter increments by 1
		///     -Counter executes the behavior when it reaches the supplied maxCount
		/// </summary>
		/// <param name="name">the name of the counter</param>
		/// <param name="maxCount">max number to count to</param>
		/// <param name="behaviour">behavior to run</param>
		public Counter(string name, int maxCount, BehaviourComponent behaviour)
		{
			Name = name;
			_maxCount = maxCount;
			_behaviour = behaviour;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			try
			{
				if (_counter < _maxCount)
				{
					_counter++;
					ReturnCode = BehaviourReturnCode.Running;
					return BehaviourReturnCode.Running;
				}
				_counter = 0;
				ReturnCode = _behaviour.Behave();
				return ReturnCode;
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