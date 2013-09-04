using System;

namespace BehaviorLibrary.Components.Composites
{
	public class Sequence : BehaviorComponent
	{
		private readonly short _sequenceLength;
		private readonly BehaviorComponent[] _behaviorComponents;
		private short _sequence;

		/// <summary>
		///     Performs the given behavior components sequentially
		///     Performs an AND-Like behavior and will perform each successive component
		///     -Returns Success if all behavior components return Success
		///     -Returns Running if an individual behavior component returns Success or Running
		///     -Returns Failure if a behavior components returns Failure or an error is encountered
		/// </summary>
		/// <param name="behaviors">one to many behavior components</param>
		public Sequence(params BehaviorComponent[] behaviors)
		{
			_behaviorComponents = behaviors;
			_sequenceLength = (short) _behaviorComponents.Length;
		}

		/// <summary>
		///     performs the given behavior
		/// </summary>
		/// <returns>the behaviors return code</returns>
		public override BehaviorReturnCode Behave()
		{
			//while you can go through them, do so
			while (_sequence < _sequenceLength)
			{
				try
				{
					switch (_behaviorComponents[_sequence].Behave())
					{
						case BehaviorReturnCode.Failure:
							_sequence = 0;
							ReturnCode = BehaviorReturnCode.Failure;
							return ReturnCode;
						case BehaviorReturnCode.Success:
							_sequence++;
							ReturnCode = BehaviorReturnCode.Running;
							return ReturnCode;
						case BehaviorReturnCode.Running:
							ReturnCode = BehaviorReturnCode.Running;
							return ReturnCode;
					}
				}
				catch (Exception e)
				{
#if DEBUG
					Console.Error.WriteLine(e.ToString());
#endif
					_sequence = 0;
					ReturnCode = BehaviorReturnCode.Failure;
					return ReturnCode;
				}
			}

			_sequence = 0;
			ReturnCode = BehaviorReturnCode.Success;
			return ReturnCode;
		}
	}
}