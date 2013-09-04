using System;

namespace BehaviorLibrary.Components.Composites
{
	public class RandomSelector : BehaviorComponent
	{
		private readonly BehaviorComponent[] _behaviorComponents;

		//use current milliseconds to set random seed
		private Random r_Random = new Random(DateTime.Now.Millisecond);

		/// <summary>
		///     Randomly selects and performs one of the passed behaviors
		///     -Returns Success if selected behavior returns Success
		///     -Returns Failure if selected behavior returns Failure
		///     -Returns Running if selected behavior returns Running
		/// </summary>
		/// <param name="behaviors">one to many behavior components</param>
		public RandomSelector(params BehaviorComponent[] behaviors)
		{
			_behaviorComponents = behaviors;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			r_Random = new Random(DateTime.Now.Millisecond);

			try
			{
				switch (_behaviorComponents[r_Random.Next(0, _behaviorComponents.Length - 1)].Behave())
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
						ReturnCode = BehaviorReturnCode.Failure;
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