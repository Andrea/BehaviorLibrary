using System;
using BehaviourLibrary.Components.Conditionals;

namespace BehaviourLibrary.Components.Decorators
{
	public class Interruptible : BehaviourComponent
	{
		private readonly BehaviourComponent _behaviourComponent;
		private readonly Conditional _interruptCondition;
		private readonly BehaviourReturnCode _onInterruptReturn;

		/// <summary>
		/// Checks an interrupt condition, executing the given behaviour only if the condition returns False
		/// -Returns Success if the given behaviour returns Success and the condition returns False
		/// -Returns Running if the given behaviour returns Running and the condition returns False
		/// -Returns Failure if the given behaviour returns Failure or the condition returns True
		/// 
		/// Possibly not a good solution for interrupt style behaviour in the long run as it is very difficult to write
		/// conditions for interrupting without adding lots of state elsewhere to track when interrupts occur
		/// </summary>
		/// <param name="behaviour"></param>
		/// <param name="interruptCondition"></param>
		/// <param name="onInterruptReturn"></param>
		public Interruptible(BehaviourComponent behaviour, Conditional interruptCondition, BehaviourReturnCode onInterruptReturn)
		{
			_behaviourComponent = behaviour;
			_interruptCondition = interruptCondition;
			_onInterruptReturn = onInterruptReturn;
		}

		public override BehaviourReturnCode Behave()
		{
			try
			{
				if (_interruptCondition.Behave() == BehaviourReturnCode.Success)
				{
					ReturnCode = _onInterruptReturn;
					return _onInterruptReturn;
				}

				ReturnCode = _behaviourComponent.Behave();
				return ReturnCode;
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.ToString());
#endif
				ReturnCode = BehaviourReturnCode.Failure;
				return ReturnCode;
			}
		}
	}
}
