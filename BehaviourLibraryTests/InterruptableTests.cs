using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Conditionals;
using BehaviourLibrary.Components.Decorators;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	class InterruptableTests
	{
		[Test]
		public void When_interrupt_is_false_child_behaves_as_normal()
		{
			var successAction = new BehaviourAction(() => BehaviourReturnCode.Success);
			var failureAction = new BehaviourAction(() => BehaviourReturnCode.Failure);
			var runningAction = new BehaviourAction(() => BehaviourReturnCode.Running);

			var condition = new Conditional(() => false);

			var successInterruptable = new Interruptable(successAction, condition, BehaviourReturnCode.Failure);
			var failureInterruptable = new Interruptable(failureAction, condition, BehaviourReturnCode.Failure);
			var runningInterruptable = new Interruptable(runningAction, condition, BehaviourReturnCode.Failure);

			Assert.AreEqual(successInterruptable.Behave(), BehaviourReturnCode.Success);
			Assert.AreEqual(failureInterruptable.Behave(), BehaviourReturnCode.Failure);
			Assert.AreEqual(runningInterruptable.Behave(), BehaviourReturnCode.Running);
		}

		[Test]
		public void When_interrupt_is_true_returns_assigned_interrupt_return_value()
		{
			var action = new BehaviourAction(() => BehaviourReturnCode.Success);

			var condition = new Conditional(() => true);

			var successInterruptable = new Interruptable(action, condition, BehaviourReturnCode.Success);
			var failureInterruptable = new Interruptable(action, condition, BehaviourReturnCode.Failure);
			var runningInterruptable = new Interruptable(action, condition, BehaviourReturnCode.Running);

			Assert.AreEqual(successInterruptable.Behave(), BehaviourReturnCode.Success);
			Assert.AreEqual(failureInterruptable.Behave(), BehaviourReturnCode.Failure);
			Assert.AreEqual(runningInterruptable.Behave(), BehaviourReturnCode.Running);
		}
	}
}
