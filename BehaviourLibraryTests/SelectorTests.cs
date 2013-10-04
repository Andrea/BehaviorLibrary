using BehaviorLibrary;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Composites;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class SelectorTests
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
			var returnCode = new Selector(
				new BehaviorAction(CalledAndFailed), 
				new BehaviorAction(CalledAndFailed), 
				new BehaviorAction(()=> BehaviorReturnCode.Success)
				).Behave();
			Assert.AreEqual(2, _calledAndFailedTimes);
			Assert.AreEqual(BehaviorReturnCode.Success, returnCode);
		}

		[Test]
		public void When_behave_Running_then_completes_until_running_behaviour()
		{
			_calledAndFailedTimes = 0;
			var returnCode = new Selector(
									new BehaviorAction(CalledAndFailed), 
									new BehaviorAction(CalledAndRunning),
									new BehaviorAction(() => BehaviorReturnCode.Success)
								).Behave();
			Assert.AreEqual(1, _calledAndFailedTimes);
			Assert.AreEqual(BehaviorReturnCode.Running, returnCode);
		}

		[Test]
		public void When_running_Then_complete_with_many_behave_calls()
		{
			var sequence = new Selector(
								new BehaviorAction(CalledAndFailed),
								new BehaviorAction(new RunningTimes().Run), 
								new BehaviorAction(() => BehaviorReturnCode.Success)
						);
			Assert.AreEqual(BehaviorReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviorReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviorReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviorReturnCode.Success, sequence.Behave());
			Assert.AreEqual(1, _calledAndFailedTimes);
		}


		private BehaviorReturnCode CalledAndRunning()
		{
			return BehaviorReturnCode.Running;
		}


		private BehaviorReturnCode CalledAndFailed()
		{
			_calledAndFailedTimes++;
			return BehaviorReturnCode.Failure;
		}
	}
}