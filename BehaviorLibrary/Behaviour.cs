using System;
using BehaviourLibrary.Components.Composites;

namespace BehaviourLibrary
{
	public enum BehaviourReturnCode
	{
		Failure,
		Success,
		Running
	}

	public class Behaviour
	{
		private readonly RootSelector _rootSelector;

		
		public Behaviour(RootSelector root)
		{
			_rootSelector = root;
		}

		public BehaviourReturnCode ReturnCode { get; set; }

		public BehaviourReturnCode Behave()
		{
			try
			{
				switch (_rootSelector.Behave())
				{
					case BehaviourReturnCode.Failure:
						ReturnCode = BehaviourReturnCode.Failure;
						return ReturnCode;
					case BehaviourReturnCode.Success:
						ReturnCode = BehaviourReturnCode.Success;
						return ReturnCode;
					case BehaviourReturnCode.Running:
						ReturnCode = BehaviourReturnCode.Running;
						return ReturnCode;
					default:
						ReturnCode = BehaviourReturnCode.Running;
						return ReturnCode;
				}
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.Message, e.StackTrace);
#endif
				ReturnCode = BehaviourReturnCode.Failure;
				return ReturnCode;
			}
		}
	}
}