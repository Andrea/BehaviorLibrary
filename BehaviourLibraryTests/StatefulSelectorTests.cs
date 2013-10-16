using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class StatefulSelectorTests
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
			var returnCode = new StatefulSelector(
				new BehaviourAction(CalledAndFailed), 
				new BehaviourAction(CalledAndFailed), 
				new BehaviourAction(()=> BehaviourReturnCode.Success)
				).Behave();
			Assert.AreEqual(2, _calledAndFailedTimes);
			Assert.AreEqual(BehaviourReturnCode.Success, returnCode);
		}

		[Test]
		public void When_behave_Running_then_completes_until_running_behaviour()
		{
			_calledAndFailedTimes = 0;
			var returnCode = new StatefulSelector(
				new BehaviourAction(CalledAndFailed), 
				new BehaviourAction(CalledAndRunning),
				new BehaviourAction(() => BehaviourReturnCode.Success)
				).Behave();
			Assert.AreEqual(1, _calledAndFailedTimes);
			Assert.AreEqual(BehaviourReturnCode.Running, returnCode);
		}

		[Test]
		public void When_running_Then_complete_with_many_behave_calls()
		{
			var sequence = new StatefulSelector(
				new BehaviourAction(CalledAndFailed),
				new BehaviourAction(new TestHelper().RunningTwiceThenSuccess), 
				new BehaviourAction(() => BehaviourReturnCode.Success)
				);
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Success, sequence.Behave());
			Assert.AreEqual(1, _calledAndFailedTimes);
		}


		private BehaviourReturnCode CalledAndRunning()
		{
			return BehaviourReturnCode.Running;
		}


		private BehaviourReturnCode CalledAndFailed()
		{
			_calledAndFailedTimes++;
			return BehaviourReturnCode.Failure;
		}
	}
}