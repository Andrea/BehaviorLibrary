using System;

namespace BehaviourLibrary.Components.Composites
{
	public class StatefulSelector : BehaviourComponent
	{
		private BehaviourComponent[] _behaviours;

		private int _lastBehavior;

		/// <summary>
		/// Selects among the given behavior components (stateful on running) 
		/// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
		/// -Returns Success if a behavior component returns Success
		/// -Returns Running if a behavior component returns Running
		/// -Returns Failure if all behavior components returned Failure
		/// </summary>
		/// <param name="behaviours">one to many behavior components</param>
		public StatefulSelector(params BehaviourComponent[] behaviours)
		{
			_behaviours = behaviours;
		}

		/// <summary>
		/// performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			for (; _lastBehavior < _behaviours.Length; _lastBehavior++)
			{
				try
				{
					switch (_behaviours[_lastBehavior].Behave())
					{
						case BehaviourReturnCode.Failure:
							continue;
						case BehaviourReturnCode.Success:
							_lastBehavior = 0;
							ReturnCode = BehaviourReturnCode.Success;
							return ReturnCode;
						case BehaviourReturnCode.Running:
							ReturnCode = BehaviourReturnCode.Running;
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
			ReturnCode = BehaviourReturnCode.Failure;
			return ReturnCode;
		}
	}
}
