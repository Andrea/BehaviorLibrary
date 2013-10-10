using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class ExclusiveSelectorTests
	{
		private int _calledAndFailedTimes;

		[SetUp]
		public void SetUp()
		{
			_calledAndFailedTimes = 0;
		}

		[Test]
		public void When_Failed_then_execute_until_success()
		{
			var returnCode = new ExclusiveSelector(
				CreateFailiedAction(),
				CreateFailiedAction(),
				CreateSuccessAction())
				.Behave();
			
			Assert.AreEqual(BehaviourReturnCode.Success, returnCode);
		}

		[Test]
		public void When_Running_then_execute_until_success()
		{
			var returnCode = new ExclusiveSelector(
				CreateRunningAction(),
				CreateRunningAction(),
				CreateSuccessAction())
				.Behave();

			Assert.AreEqual(BehaviourReturnCode.Success, returnCode);
		}

		[Test]
		public void When_failed_then_Running_Then_keeps_going_until_sucess()
		{
			var returnCode = new StatefulSelector(
							CreateFailiedAction(),
							CreateRunningAction(),
							CreateSuccessAction()).Behave();
			
			Assert.AreEqual(BehaviourReturnCode.Running, returnCode);
		}

		[Test]
		public void When_running_and_completed_sequence_then_Running()
		{
			var returnCode = new StatefulSelector(CreateRunningAction(), CreateRunningAction()).Behave();

			Assert.AreEqual(BehaviourReturnCode.Running, returnCode);
		}

		private static BehaviourAction CreateRunningAction()
		{
			return new BehaviourAction(() => BehaviourReturnCode.Running);
		}

		private static BehaviourAction CreateFailiedAction()
		{
			return new BehaviourAction(() => BehaviourReturnCode.Failure);
		}

		private BehaviourAction CreateSuccessAction()
		{
			return new BehaviourAction(()=> BehaviourReturnCode.Success);
		}

		//
//		[Test]
//		public void When_running_Then_complete_with_many_behave_calls()
//		{
//			var sequence = new StatefulSelector(
//								new BehaviourAction(CalledAndFailed),
//								new BehaviourAction(new TestsHelper().RunningTwiceThenSuccess),
//								new BehaviourAction(() => BehaviourReturnCode.Success)
//						);
//			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
//			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
//			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
//			Assert.AreEqual(BehaviourReturnCode.Success, sequence.Behave());
//			Assert.AreEqual(1, _calledAndFailedTimes);
//		}

	}
}