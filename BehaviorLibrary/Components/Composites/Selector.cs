using System;

namespace BehaviorLibrary.Components.Composites
{
	public class Selector : BehaviorComponent
	{
		private readonly short _selLength;
		protected BehaviorComponent[] BehaviorComponents;

		private short _selections;

		/// <summary>
		///     Selects among the given behavior components
		///     Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure
		///     is certain
		///     -Returns Success if a behavior component returns Success
		///     -Returns Running if a behavior component returns Failure or Running
		///     -Returns Failure if all behavior components returned Failure or an error has occured
		/// </summary>
		/// <param name="behaviors">one to many behavior components</param>
		public Selector(params BehaviorComponent[] behaviors)
		{
			BehaviorComponents = behaviors;
			_selLength = (short) BehaviorComponents.Length;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			while (_selections < _selLength)
			{
				try
				{
					switch (BehaviorComponents[_selections].Behave())
					{
						case BehaviorReturnCode.Failure:
							_selections++;
							ReturnCode = BehaviorReturnCode.Running;
							return ReturnCode;
						case BehaviorReturnCode.Success:
							_selections = 0;
							ReturnCode = BehaviorReturnCode.Success;
							return ReturnCode;
						case BehaviorReturnCode.Running:
							ReturnCode = BehaviorReturnCode.Running;
							return ReturnCode;
						default:
							_selections++;
							ReturnCode = BehaviorReturnCode.Failure;
							return ReturnCode;
					}
				}
				catch (Exception e)
				{
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
					_selections++;
					ReturnCode = BehaviorReturnCode.Failure;
					return ReturnCode;
				}
			}

			_selections = 0;
			ReturnCode = BehaviorReturnCode.Failure;
			return ReturnCode;
		}
	}
}