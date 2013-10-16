using System;

namespace BehaviourLibrary.Components.Decorators
{
	public class Inverter : BehaviourComponent
	{
		private readonly BehaviourComponent _behaviourComponent;

		/// <summary>
		///     inverts the given behavior
		///     -Returns Success on Failure or Error
		///     -Returns Failure on Success
		///     -Returns Running on Running
		/// </summary>
		/// <param name="behaviour"></param>
		public Inverter(BehaviourComponent behaviour) : this("", behaviour)
		{
		}

		/// <summary>
		///     inverts the given behavior
		///     -Returns Success on Failure or Error
		///     -Returns Failure on Success
		///     -Returns Running on Running
		/// </summary>
		/// <param name="name">the name of the inverter</param>
		/// <param name="behaviour"></param>
		public Inverter(string name, BehaviourComponent behaviour)
		{
			Name = name;
			_behaviourComponent = behaviour;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			try
			{
				switch (_behaviourComponent.Behave())
				{
					case BehaviourReturnCode.Failure:
						ReturnCode = BehaviourReturnCode.Success;
						return ReturnCode;
					case BehaviourReturnCode.Success:
						ReturnCode = BehaviourReturnCode.Failure;
						return ReturnCode;
					case BehaviourReturnCode.Running:
						ReturnCode = BehaviourReturnCode.Running;
						return ReturnCode;
				}
			}
			catch (Exception e)
			{
#if DEBUG
				Console.Error.WriteLine(e.ToString());
#endif
				ReturnCode = BehaviourReturnCode.Success;
				return ReturnCode;
			}

			ReturnCode = BehaviourReturnCode.Success;
			return ReturnCode;
		}
	}
}