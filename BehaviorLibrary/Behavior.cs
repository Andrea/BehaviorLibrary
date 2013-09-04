using System;
using BehaviorLibrary.Components.Composites;

namespace BehaviorLibrary
{
	public enum BehaviorReturnCode
	{
		Failure,
		Success,
		Running
	}

	public delegate BehaviorReturnCode BehaviorReturn();

	public class Behavior
	{
		private readonly RootSelector _rootSelector;

		
		public Behavior(RootSelector root)
		{
			_rootSelector = root;
		}

		public BehaviorReturnCode ReturnCode { get; set; }

		public BehaviorReturnCode Behave()
		{
			try
			{
				switch (_rootSelector.Behave())
				{
					case BehaviorReturnCode.Failure:
						ReturnCode = BehaviorReturnCode.Failure;
						return ReturnCode;
					case BehaviorReturnCode.Success:
						ReturnCode = BehaviorReturnCode.Success;
						return ReturnCode;
					case BehaviorReturnCode.Running:
						ReturnCode = BehaviorReturnCode.Running;
						return ReturnCode;
					default:
						ReturnCode = BehaviorReturnCode.Running;
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