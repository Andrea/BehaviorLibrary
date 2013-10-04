using System;

namespace BehaviorLibrary.Components.Composites
{
	public class StatefulSequence : BehaviorComponent
	{
		private BehaviorComponent[] _behaviors;

		private int _lastBehavior;

		/// <summary>
		/// attempts to run the behaviors all in one cycle (stateful on running)
		/// -Returns Success when all are successful
		/// -Returns Failure if one behavior fails or an error occurs
		/// -Does not Return Running
		/// </summary>
		/// <param name="behaviors"></param>
		public StatefulSequence (params BehaviorComponent[] behaviors){
			_behaviors = behaviors;
		}

		/// <summary>
		/// performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave(){

			//start from last remembered position
			for(; _lastBehavior < _behaviors.Length;_lastBehavior++){
				try{
					switch (_behaviors[_lastBehavior].Behave()){
					case BehaviorReturnCode.Failure:
						_lastBehavior = 0;
						ReturnCode = BehaviorReturnCode.Failure;
						return ReturnCode;
					case BehaviorReturnCode.Success:
						continue;
					case BehaviorReturnCode.Running:
						ReturnCode = BehaviorReturnCode.Running;
						return ReturnCode;
					default:
						_lastBehavior = 0;
						ReturnCode = BehaviorReturnCode.Success;
						return ReturnCode;
					}
				}
				catch (Exception e){
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
					_lastBehavior = 0;
					ReturnCode = BehaviorReturnCode.Failure;
					return ReturnCode;
				}
			}
			_lastBehavior = 0;
			ReturnCode = BehaviorReturnCode.Success;
			return ReturnCode;
		}
	}
}

