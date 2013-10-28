using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using BehaviourLibrary.Components.Conditionals;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class StatefulSequenceTest
	{
		private int _calledAndSuccessTimes;

		[SetUp]
		public void Setup()
		{
			_calledAndSuccessTimes = 0;
		}

		[Test]
		public void When_behave_running_takes_up_from_where_it_left_without_re_evaluating_prev_conditions()
		{
			var called = 0;
			var exclusive = new StatefulSequence(new Conditional(() =>
										{
											called++;
											return true;
										}),
									TestsHelper.CreateRunningAction(),
									TestsHelper.CreateSuccessAction()
									);

			exclusive.Behave();
			exclusive.Behave();

			Assert.AreEqual(1, called);
		}

		[Test]
		public void When_behave_successfull_then_goes_through_all_sequence_elements()
		{
			_calledAndSuccessTimes = 0;
			var returnCode = new StatefulSequence(new BehaviourAction(CalledAndSuccess), new BehaviourAction(CalledAndSuccess), new BehaviourAction(CalledAndSuccess)).Behave();
			Assert.AreEqual(3, _calledAndSuccessTimes);
			Assert.AreEqual(BehaviourReturnCode.Success, returnCode);
		}

		[Test]
		public void When_behave_Running_then_completes_until_running_behaviour()
		{
			_calledAndSuccessTimes = 0;
			var seq = new StatefulSequence(new BehaviourAction(CalledAndSuccess), new BehaviourAction(CalledAndRunning), new BehaviourAction(CalledAndSuccess));

			var returnCode = seq.Behave();

			Assert.AreEqual(1, _calledAndSuccessTimes);
			Assert.AreEqual(BehaviourReturnCode.Running, returnCode);
		}

		[Test]
		public void When_running_Then_complete_with_many_behave_calls()
		{
			var sequence = new StatefulSequence(new BehaviourAction(CalledAndSuccess), new BehaviourAction(new TestsHelper().RunningTwiceThenSuccess), new BehaviourAction(CalledAndSuccess));
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviourReturnCode.Success, sequence.Behave());
			Assert.AreEqual(2, _calledAndSuccessTimes);
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