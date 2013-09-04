using System;

namespace BehaviorLibrary.Components.Composites
{
	public class RootSelector : Selector
	{
		private readonly BehaviorComponent[] _behaviorComponents;

		private readonly Func<int> _index;

		/// <summary>
		///     The selector for the root node of the behavior tree
		/// </summary>
		/// <param name="index">an index representing which of the behavior branches to perform</param>
		/// <param name="behaviors">the behavior branches to be selected from</param>
		public RootSelector(Func<int> index, params BehaviorComponent[] behaviors)
		{
			_index = index;
			_behaviorComponents = behaviors;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			try
			{
				switch (_behaviorComponents[_index.Invoke()].Behave())
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