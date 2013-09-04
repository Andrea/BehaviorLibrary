using System;

namespace BehaviorLibrary.Components.Actions
{
	public class BehaviorAction : BehaviorComponent
	{
		private readonly Func<BehaviorReturnCode> _action;

		public BehaviorAction()
		{
		}

		public BehaviorAction(Func<BehaviorReturnCode> action)
		{
			_action = action;
		}

		public override BehaviorReturnCode Behave()
		{
			try
			{
				switch (_action.Invoke())
				{
					case BehaviorReturnCode.Success:
						ReturnCode = BehaviorReturnCode.Success;
						return ReturnCode;
					case BehaviorReturnCode.Failure:
						ReturnCode = BehaviorReturnCode.Failure;
						return ReturnCode;
					case BehaviorReturnCode.Running:
						ReturnCode = BehaviorReturnCode.Running;
						return ReturnCode;
					default:
						ReturnCode = BehaviorReturnCode.Failure;
						return ReturnCode;
				}
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.ToString());
#endif
				ReturnCode = BehaviorReturnCode.Failure;
				return ReturnCode;
			}
		}
	}
}