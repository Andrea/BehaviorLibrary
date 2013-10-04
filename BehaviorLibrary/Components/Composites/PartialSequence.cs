using System;

namespace BehaviorLibrary.Components.Composites
{
    public class PartialSequence : BehaviorComponent
    {
	    private BehaviorComponent[] _behaviors;
        private short _sequence;
        private short _seqLength;
        
        /// <summary>
        /// Performs the given behavior components sequentially (one evaluation per Behave call)
        /// Performs an AND-Like behavior and will perform each successive component
        /// -Returns Success if all behavior components return Success
        /// -Returns Running if an individual behavior component returns Success or Running
        /// -Returns Failure if a behavior components returns Failure or an error is encountered
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public PartialSequence(params BehaviorComponent[] behaviors)
        {
            _behaviors = behaviors;
            _seqLength = (short) _behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave()
        {
            //while you can go through them, do so
            while (_sequence < _seqLength)
            {
                try
                {
                    switch (_behaviors[_sequence].Behave())
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
