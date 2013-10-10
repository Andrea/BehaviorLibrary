using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using BehaviourLibrary.Components.Conditionals;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class ExclusiveSequenceTest
	{
		private int _calledAndSuccessTimes;

		[SetUp]
		public void Setup()
		{
			_calledAndSuccessTimes = 0;
		}

		[Test]
		public void When_behave_running_takes_up_re_evaluating_prev_conditions()
		{
			var called = 0;
			var exclusive = new ExclusiveSequence(new Conditional(() =>
									{
										called++;
										return true;
									}),
									TestsHelper.CreateRunningAction(),
									TestsHelper.CreateRunningAction(),
									TestsHelper.CreateSuccessAction()
									);
			exclusive.Behave();
			exclusive.Behave();
			Assert.AreEqual(2, called);
			
		}

		[Test]
		public void When_behave_Running_then_completes_until_running_behaviour()
		{
			_calledAndSuccessTimes = 0;
			var seq = new ExclusiveSequence(
				new BehaviourAction(CalledAndSuccess), 
				new BehaviourAction(CalledAndRunning), 
				new BehaviourAction(CalledAndSuccess));
			
			var returnCode = seq.Behave();
			
			Assert.AreEqual(1, _calledAndSuccessTimes);
			Assert.AreEqual(BehaviourReturnCode.Running, returnCode);
		}

		[Test]
		public void When_running_Then_re_evaluate_and_complete_with_many_behave_calls()
		{
			var sequence = new ExclusiveSequence(
				new BehaviourAction(CalledAndSuccess), 
				new BehaviourAction(new TestsHelper().RunningTwiceThenSuccess)
				);

			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Success, sequence.Behave());
			
			Assert.AreEqual(4, _calledAndSuccessTimes);
		}


		private BehaviourReturnCode CalledAndRunning()
		{
			return BehaviourReturnCode.Running;
		}

		private BehaviourReturnCode CalledAndSuccess()
		{
			_calledAndSuccessTimes++;
			return BehaviourReturnCode.Success;
		}
	}
}