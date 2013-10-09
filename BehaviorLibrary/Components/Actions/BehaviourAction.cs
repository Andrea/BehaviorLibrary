using System;

namespace BehaviourLibrary.Components.Actions
{
	public class BehaviourAction : BehaviourComponent
	{
		private readonly Func<BehaviourReturnCode> _action;

		public BehaviourAction()
		{
		}

		public BehaviourAction(Func<BehaviourReturnCode> action)
		{
			_action = action;
		}

		public override BehaviourReturnCode Behave()
		{
			try
			{
				var returnCode = _action.Invoke();
#if DEBUG
				Behaviour.NodeInfo.AppendFormat("Action: {0} code: {1} \n", _action.Method.Name, returnCode);
#endif
				switch (returnCode)
				{
					case BehaviourReturnCode.Success:
						ReturnCode = BehaviourReturnCode.Success;
						return ReturnCode;
					case BehaviourReturnCode.Failure:
						ReturnCode = BehaviourReturnCode.Failure;
						return ReturnCode;
					case BehaviourReturnCode.Running:
						ReturnCode = BehaviourReturnCode.Running;
						return ReturnCode;
					default:
						ReturnCode = BehaviourReturnCode.Failure;
						return ReturnCode;
				}
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.ToString());
#endif
				ReturnCode = BehaviourReturnCode.Failure;
				return ReturnCode;
			}
		}
	}
}