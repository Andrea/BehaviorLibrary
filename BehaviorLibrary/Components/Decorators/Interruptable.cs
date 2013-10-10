using System;
using BehaviourLibrary.Components.Conditionals;

namespace BehaviourLibrary.Components.Decorators
{
	public class Interruptable : BehaviourComponent
	{
		private readonly BehaviourComponent _behaviourComponent;
		private readonly Conditional _interruptCondition;
		private readonly BehaviourReturnCode _onInterruptReturn;

		public Interruptable(BehaviourComponent behaviour, Conditional interruptCondition,
			BehaviourReturnCode onInterruptReturn)
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
					return _onInterruptReturn;
				}

				return _behaviourComponent.Behave();
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
