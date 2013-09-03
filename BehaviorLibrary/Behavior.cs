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
		private readonly RootSelector b_Root;

		
		public Behavior(RootSelector root)
		{
			b_Root = root;
		}

		public BehaviorReturnCode ReturnCode { get; set; }

		public BehaviorReturnCode Behave()
		{
			try
			{
				switch (b_Root.Behave())
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