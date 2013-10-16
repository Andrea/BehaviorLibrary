using BehaviourLibrary;
using BehaviourLibrary.Components.Composites;
using NUnit.Framework;

namespace BehaviourLibraryTests
{
	[TestFixture]
	public class RootSelectorTests
	{
		[Test]
		public void When_child_fails_Then_returns_fail()
		{
			var returnCode = new RootSelector(() => 0,
				TestHelper.CreateFailiedAction()).Behave();

			Assert.AreEqual(BehaviourReturnCode.Failure, returnCode);
		}

		[Test]
		public void When_child_succeeds_Then_returns_success()
		{
			var returnCode = new RootSelector(() => 0,
				TestHelper.CreateSuccessAction()).Behave();

			Assert.AreEqual(BehaviourReturnCode.Success, returnCode);
		}

		[Test]
		public void When_child_is_running_Then_returns_running()
		{
			var returnCode = new RootSelector(() => 0,
				TestHelper.CreateRunningAction()).Behave();

			Assert.AreEqual(BehaviourReturnCode.Running, returnCode);
		}
	}
}