using System;

namespace BehaviorLibrary.Components.Composites
{
	public class StatefulSelector : BehaviorComponent
	{
		private BehaviorComponent[] _behaviors;

		private int _lastBehavior;

		/// <summary>
		/// Selects among the given behavior components (stateful on running) 
		/// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
		/// -Returns Success if a behavior component returns Success
		/// -Returns Running if a behavior component returns Running
		/// -Returns Failure if all behavior components returned Failure
		/// </summary>
		/// <param name="behaviors">one to many behavior components</param>
		public StatefulSelector(params BehaviorComponent[] behaviors)
		{
			_behaviors = behaviors;
		}

		/// <summary>
		/// performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			for (; _lastBehavior < _behaviors.Length; _lastBehavior++)
			{
				try
				{
					switch (_behaviors[_lastBehavior].Behave())
					{
						case BehaviorReturnCode.Failure:
							continue;
						case BehaviorReturnCode.Success:
							_lastBehavior = 0;
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
			_lastBehavior = 0;
			ReturnCode = BehaviorReturnCode.Failure;
			return ReturnCode;
		}
	}
}
