using System;

namespace BehaviourLibrary.Components.Composites
{
	public class RandomSelector : BehaviourComponent
	{
		private readonly BehaviourComponent[] _behaviourComponents;

		//use current milliseconds to set random seed
		private Random r_Random = new Random(DateTime.Now.Millisecond);
		private int _index = -1;

		/// <summary>
		///     Randomly selects and performs one of the passed behaviors
		///     -Returns Success if selected behavior returns Success
		///     -Returns Failure if selected behavior returns Failure
		///     -Returns Running if selected behavior returns Running
		/// </summary>
		/// <param name="behaviours">one to many behavior components</param>
		public RandomSelector(params BehaviourComponent[] behaviours)
			: this("", behaviours)
		{
		}

		/// <summary>
		///     Randomly selects and performs one of the passed behaviors
		///     -Returns Success if selected behavior returns Success
		///     -Returns Failure if selected behavior returns Failure
		///     -Returns Running if selected behavior returns Running
		/// </summary>
		/// <param name="name">the name of the selector</param>
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
			try
			{

				if (_index == -1)
					_index = r_Random.Next(0, _behaviourComponents.Length - 1);
				switch (_behaviourComponents[_index].Behave())
				{
					case BehaviourReturnCode.Failure:
						ReturnCode = BehaviourReturnCode.Failure;
						_index = -1;
						return ReturnCode;
					case BehaviourReturnCode.Success:
						ReturnCode = BehaviourReturnCode.Success;
						_index = -1;
						return ReturnCode;
					case BehaviourReturnCode.Running:
						ReturnCode = BehaviourReturnCode.Running;
						return BehaviourReturnCode.Running;
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
				_index = -1;
				ReturnCode = BehaviourReturnCode.Failure;
				return ReturnCode;
			}

		}
	}
}