using System;

namespace BehaviorLibrary.Components.Composites
{
    public class PartialSelector : BehaviorComponent
    {
        protected BehaviorComponent[] _behaviors;
        private short _selections;
        private short _selLength;

        /// <summary>
		/// Selects among the given behavior components (one evaluation per Behave call)
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Failure or Running
        /// -Returns Failure if all behavior components returned Failure or an error has occured
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public PartialSelector(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
            _selLength = (short)_behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            while (_selections < _selLength)
            {
                try
                {
                    switch (_behaviors[_selections].Behave())
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
