using BehaviourLibrary;
using BehaviourLibrary.Components;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class RandomSelectorTest
	{
		[Test]
		public void When_running_then_complete_until_sucess_or_failure()
		{
			var s = new RandomSelector(new Helper().RunningTwiceThenSuccess(), new Helper().RunningTwiceThenSuccess(), new Helper().RunningTwiceThenSuccess());

			s.Behave();
			s.Behave();
			s.Behave();

			Assert.AreEqual(BehaviourReturnCode.Success, s.ReturnCode);
		}

		[Test]
		public void When_completed_then_new_task_starts()
		{
			var s = new RandomSelector(new Helper().RunningTwiceThenSuccess(), new Helper().RunningTwiceThenSuccess(), new Helper().RunningTwiceThenSuccess());

			s.Behave();
			s.Behave();
			s.Behave();
			s.Behave();

			Assert.AreEqual(BehaviourReturnCode.Running, s.ReturnCode);
		}

		private class Helper
		{
			private int _i;

			public BehaviourComponent RunningTwiceThenSuccess()
			{
				return new BehaviourAction(() =>
						{
							_i++;
							if (_i < 3)
								return BehaviourReturnCode.Running;
							_i = 0;
							return BehaviourReturnCode.Success;
						});
			}
		}
	}
}