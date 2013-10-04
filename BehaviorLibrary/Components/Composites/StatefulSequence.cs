using System;
using BehaviorLibrary.Components;

namespace BehaviorLibrary
{
	public class StatefulSequence : BehaviorComponent
	{
		private BehaviorComponent[] s_Behaviors;

		private int s_LastBehavior = 0;

		/// <summary>
		/// attempts to run the behaviors all in one cycle (stateful on running)
		/// -Returns Success when all are successful
		/// -Returns Failure if one behavior fails or an error occurs
		/// -Does not Return Running
		/// </summary>
		/// <param name="behaviors"></param>
		public StatefulSequence (params BehaviorComponent[] behaviors){
			this.s_Behaviors = behaviors;
		}

		/// <summary>
		/// performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave(){

			//start from last remembered position
			for(; s_LastBehavior < s_Behaviors.Length;s_LastBehavior++){
				try{
					switch (s_Behaviors[s_LastBehavior].Behave()){
					case BehaviorReturnCode.Failure:
						s_LastBehavior = 0;
						ReturnCode = BehaviorReturnCode.Failure;
						return ReturnCode;
					case BehaviorReturnCode.Success:
						continue;
					case BehaviorReturnCode.Running:
						ReturnCode = BehaviorReturnCode.Running;
						return ReturnCode;
					default:
						s_LastBehavior = 0;
						ReturnCode = BehaviorReturnCode.Success;
						return ReturnCode;
					}
				}
				catch (Exception e){
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
					s_LastBehavior = 0;
					ReturnCode = BehaviorReturnCode.Failure;
					return ReturnCode;
				}
			}

			s_LastBehavior = 0;
			ReturnCode = BehaviorReturnCode.Success;
			return ReturnCode;
		}


	}
}

