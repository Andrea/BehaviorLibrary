using System;

namespace BehaviourLibrary.Components.Composites
{
	public class StatefulSequence : BehaviourComponent
	{
		private BehaviourComponent[] _behaviours;

		private int _lastBehavior;

		/// <summary>
		/// attempts to run the behaviors all in one cycle (stateful on running)
		/// -Returns Success when all are successful
		/// -Returns Failure if one behavior fails or an error occurs
		/// -Does not Return Running
		/// </summary>
		/// <param name="behaviours"></param>
		public StatefulSequence(params BehaviourComponent[] behaviours) : this("", behaviours)
		{
		}

		/// <summary>
		/// attempts to run the behaviors all in one cycle (stateful on running)
		/// -Returns Success when all are successful
		/// -Returns Failure if one behavior fails or an error occurs
		/// -Does not Return Running
		/// </summary>
		/// <param name="name">the name of the sequence</param>
		/// <param name="behaviours"></param>
		public StatefulSequence(string name, params BehaviourComponent[] behaviours)
		{
			Name = name;
			_behaviours = behaviours;
		}

		/// <summary>
		/// performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{

			//start from last remembered position
			for (; _lastBehavior < _behaviours.Length; _lastBehavior++)
			{
				try
				{
					switch (_behaviours[_lastBehavior].Behave())
					{
						case BehaviourReturnCode.Failure:
							_lastBehavior = 0;
							ReturnCode = BehaviourReturnCode.Failure;
							return ReturnCode;
						case BehaviourReturnCode.Success:
							continue;
						case BehaviourReturnCode.Running:
							ReturnCode = BehaviourReturnCode.Running;
							return ReturnCode;
						default:
							_lastBehavior = 0;
							ReturnCode = BehaviourReturnCode.Success;
							return ReturnCode;
					}
				}
				catch (Exception e)
				{
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
					_lastBehavior = 0;
					ReturnCode = BehaviourReturnCode.Failure;
					return ReturnCode;
				}
			}
			_lastBehavior = 0;
			ReturnCode = BehaviourReturnCode.Success;
			return ReturnCode;
		}
	}
}

