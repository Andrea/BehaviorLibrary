﻿using System;

namespace BehaviorLibrary.Components.Composites
{
	public class ParallelSequence : BehaviorComponent
	{
		private readonly BehaviorComponent[] _behaviorComponents;

		/// <summary>
		///     attempts to run the behaviors all in one cycle
		///     -Returns Success when all are successful
		///     -Returns Failure if one behavior fails or an error occurs
		///     -Does not Return Running
		/// </summary>
		/// <param name="behaviors"></param>
		public ParallelSequence(params BehaviorComponent[] behaviors)
		{
			_behaviorComponents = behaviors;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			for (int i = 0; i < _behaviorComponents.Length; i++)
			{
				try
				{
					switch (_behaviorComponents[i].Behave())
					{
						case BehaviorReturnCode.Failure:
							ReturnCode = BehaviorReturnCode.Failure;
							return ReturnCode;
						case BehaviorReturnCode.Success:
							continue;
						case BehaviorReturnCode.Running:
							continue;
						default:
							ReturnCode = BehaviorReturnCode.Success;
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

			ReturnCode = BehaviorReturnCode.Success;
			return ReturnCode;
		}
	}
}