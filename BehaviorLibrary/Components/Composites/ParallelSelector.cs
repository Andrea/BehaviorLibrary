using System;

namespace BehaviorLibrary.Components.Composites
{
	public class ParallelSelector : BehaviorComponent
	{
		private readonly short _selLength;

		private readonly BehaviorComponent[] _behaviorComponents;
		
		/// <summary>
		///     Selects among the given behavior components
		///     Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure
		///     is certain
		///     -Returns Success if a behavior component returns Success
		///     -Returns Running if a behavior component returns Running
		///     -Returns Failure if all behavior components returned Failure
		/// </summary>
		/// <param name="behaviors">one to many behavior components</param>
		public ParallelSelector(params BehaviorComponent[] behaviors)
		{
			_behaviorComponents = behaviors;
			_selLength = (short) _behaviorComponents.Length;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			for (int i = 0; i < _selLength; i++)
			{
				try
				{
					switch (_behaviorComponents[i].Behave())
					{
						case BehaviorReturnCode.Failure:
							continue;
						case BehaviorReturnCode.Success:
							ReturnCode = BehaviorReturnCode.Success;
							return ReturnCode;
						case BehaviorReturnCode.Running:
							ReturnCode = BehaviorReturnCode.Running;
							return ReturnCode;
						default:
							continue;
					}
				}
				catch (Exception e)
				{
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
				}
			}
			ReturnCode = BehaviorReturnCode.Failure;
			return ReturnCode;
		}
	}
}