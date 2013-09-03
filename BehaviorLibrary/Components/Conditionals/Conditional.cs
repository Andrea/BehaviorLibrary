using System;

namespace BehaviorLibrary.Components.Conditionals
{
	public class Conditional : BehaviorComponent
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
		public override BehaviorReturnCode Behave()
		{
			try
			{
				switch (_conditional.Invoke())
				{
					case true:
						ReturnCode = BehaviorReturnCode.Success;
						return ReturnCode;
					case false:
						ReturnCode = BehaviorReturnCode.Failure;
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