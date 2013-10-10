using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;

namespace BehaviourLibraryTests
{
	internal  class TestsHelper
	{
		private int _i;

		public BehaviourReturnCode RunningTwiceThenSuccess()
		{
			var rc=  _i < 3 ? BehaviourReturnCode.Running : BehaviourReturnCode.Success;
			_i++;
			return rc;
		}


		public static BehaviourAction CreateRunningAction()
		{
			return new BehaviourAction(() => BehaviourReturnCode.Running);
		}

		public static BehaviourAction CreateFailiedAction()
		{
			return new BehaviourAction(() => BehaviourReturnCode.Failure);
		}

		public static BehaviourAction CreateSuccessAction()
		{
			return new BehaviourAction(() => BehaviourReturnCode.Success);
		}
	}

}
