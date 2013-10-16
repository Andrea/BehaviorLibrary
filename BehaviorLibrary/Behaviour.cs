using System;
using System.Text;
using BehaviourLibrary.Components.Composites;

namespace BehaviourLibrary
{
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
			ClearNodeInfos();
			return BehaveInternal();
		}

		private BehaviourReturnCode BehaveInternal()
		{
			
			try
			{
				switch (RootSelector.Behave())
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


		public static StringBuilder NodeInfo = new StringBuilder();

		public string NodeInfos { get { return NodeInfo.ToString(); }}

		public RootSelector RootSelector
		{
			get { return _rootSelector; }
		}

		public void ClearNodeInfos()
		{
			NodeInfo.Clear();
		}

	}
}