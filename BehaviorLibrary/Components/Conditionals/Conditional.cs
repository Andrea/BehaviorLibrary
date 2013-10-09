using System;

namespace BehaviourLibrary.Components.Conditionals
{
	public class Conditional : BehaviourComponent
	{
		private readonly Func<Boolean> _conditional;

		/// <summary>
		///     Returns a return code equivalent to the test
		///     -Returns Success if true
		///     -Returns Failure if false
		/// </summary>
		/// <param name="test">the value to be tested</param>
		public Conditional(Func<Boolean> test)
		{
			_conditional = test;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviourReturnCode Behave()
		{
			try
			{

				var conditionalResult = _conditional.Invoke();
#if DEBUG
				Behaviour.NodeInfo.AppendFormat("Conditional: {0} {1} \n", _conditional.Method.Name, conditionalResult);
#endif
				switch (conditionalResult)
				{
					case true:
						ReturnCode = BehaviourReturnCode.Success;
						return ReturnCode;
					case false:
						ReturnCode = BehaviourReturnCode.Failure;
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