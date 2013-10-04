using System;
using BehaviorLibrary;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Composites;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class SequenceTests
	{
		private int _calledAndSuccessTimes;

		[SetUp]
		public void Setup()
		{
			_calledAndSuccessTimes = 0;
		}

		[Test]
		public void When_behave_successfull_then_goes_through_all_sequence_elements()
		{
			_calledAndSuccessTimes = 0;
			var returnCode = new Sequence(new BehaviorAction(CalledAndSuccess), new BehaviorAction(CalledAndSuccess), new BehaviorAction(CalledAndSuccess)).Behave();
			Assert.AreEqual(3, _calledAndSuccessTimes);
			Assert.AreEqual(BehaviorReturnCode.Success,returnCode);
		}

		[Test]
		public void When_behave_Running_then_completes_until_running_behaviour()
		{
			_calledAndSuccessTimes = 0;
			var seq = new Sequence(new BehaviorAction(CalledAndSuccess), new BehaviorAction(CalledAndRunning), new BehaviorAction(CalledAndSuccess));
			
			var returnCode = seq.Behave();
			
			Assert.AreEqual(1, _calledAndSuccessTimes);
			Assert.AreEqual(BehaviorReturnCode.Running, returnCode);
		}

		[Test]
		public void When_running_Then_complete_with_many_behave_calls()
		{
			var sequence = new Sequence(new BehaviorAction(CalledAndSuccess), new BehaviorAction(new RunningTimes().Run), new BehaviorAction(CalledAndSuccess));
			Assert.AreEqual(BehaviorReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviorReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviorReturnCode.Running, sequence.Behave());
			Assert.AreEqual(BehaviorReturnCode.Success, sequence.Behave());
			Assert.AreEqual(2, _calledAndSuccessTimes);
		}


		private BehaviorReturnCode CalledAndRunning()
		{
			return BehaviorReturnCode.Running;
		}

		private BehaviorReturnCode CalledAndSuccess()
		{
			_calledAndSuccessTimes++;
			return BehaviorReturnCode.Success;
		}
	}

	internal class RunningTimes
	{
		private int _i;
		public BehaviorReturnCode Run()
		{
			var rc=  _i < 3 ? BehaviorReturnCode.Running : BehaviorReturnCode.Success;
			_i++;
			return rc;
		}
	}

}
