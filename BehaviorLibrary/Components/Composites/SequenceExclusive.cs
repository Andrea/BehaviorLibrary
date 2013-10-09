using System;

namespace BehaviourLibrary.Components.Composites
{
	//TODO Create SelectorExclusive
	public class SequenceExclusive : BehaviourComponent
	{
		private BehaviourComponent[] _behaviours;

		/// <summary>
		/// attempts to run the behaviors all in one cycle
		/// -Returns Success when all are successful
		/// -Returns Failure if one behavior fails or an error occurs
		/// -Returns Running if one behavior returns Running
		/// </summary>
		/// <param name="behaviours"></param>
		public SequenceExclusive(params BehaviourComponent[] behaviours)
		{
			_behaviours = behaviours;
		}

		/// <summary>
		/// performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			for (int i = 0; i < _behaviours.Length; i++)
			{
				try
				{
					switch (_behaviours[i].Behave())
					{
						case BehaviourReturnCode.Failure:
							ReturnCode = BehaviourReturnCode.Failure;
							return ReturnCode;
						case BehaviourReturnCode.Success:
							continue;
						case BehaviourReturnCode.Running:
							ReturnCode = BehaviourReturnCode.Running;
							return ReturnCode;
						default:
							ReturnCode = BehaviourReturnCode.Success;
							return ReturnCode;
					}
				}
				catch (Exception e)
				{
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
					ReturnCode = BehaviourReturnCode.Failure;
					return ReturnCode;
				}
			}
			ReturnCode = BehaviourReturnCode.Success;
			return ReturnCode;
		}


	}
}