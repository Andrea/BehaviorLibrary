using System;

namespace BehaviourLibrary.Components.Composites
{
	public class RandomSelector : BehaviourComponent
	{
		private readonly BehaviourComponent[] _behaviourComponents;

		//use current milliseconds to set random seed
		private Random r_Random = new Random(DateTime.Now.Millisecond);

		/// <summary>
		///     Randomly selects and performs one of the passed behaviors
		///     -Returns Success if selected behavior returns Success
		///     -Returns Failure if selected behavior returns Failure
		///     -Returns Running if selected behavior returns Running
		/// </summary>
		/// <param name="behaviours">one to many behavior components</param>
		public RandomSelector(string name, params BehaviourComponent[] behaviours)
		{
			Name = name;
			_behaviourComponents = behaviours;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			r_Random = new Random(DateTime.Now.Millisecond);

			try
			{
				switch (_behaviourComponents[r_Random.Next(0, _behaviourComponents.Length - 1)].Behave())
				{
					case BehaviourReturnCode.Failure:
						ReturnCode = BehaviourReturnCode.Failure;
						return ReturnCode;
					case BehaviourReturnCode.Success:
						ReturnCode = BehaviourReturnCode.Success;
						return ReturnCode;
					case BehaviourReturnCode.Running:
						ReturnCode = BehaviourReturnCode.Running;
						return ReturnCode;
					default:
						ReturnCode = BehaviourReturnCode.Failure;
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
	}
}