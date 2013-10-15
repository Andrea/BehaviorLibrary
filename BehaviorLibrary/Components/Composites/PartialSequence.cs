using System;

namespace BehaviourLibrary.Components.Composites
{
    public class PartialSequence : BehaviourComponent
    {
	    private BehaviourComponent[] _behaviours;
        private short _sequence;
        private short _seqLength;
        
        /// <summary>
        /// Performs the given behavior components sequentially (one evaluation per Behave call)
        /// Performs an AND-Like behavior and will perform each successive component
        /// -Returns Success if all behavior components return Success
        /// -Returns Running if an individual behavior component returns Success or Running
        /// -Returns Failure if a behavior components returns Failure or an error is encountered
        /// </summary>
        /// <param name="behaviours">one to many behavior components</param>
		public PartialSequence(string name, params BehaviourComponent[] behaviours)
        {
	        Name = name;
	        _behaviours = behaviours;
            _seqLength = (short) _behaviours.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviourReturnCode Behave()
        {
            //while you can go through them, do so
            while (_sequence < _seqLength)
            {
                try
                {
                    switch (_behaviours[_sequence].Behave())
                    {
                        case BehaviourReturnCode.Failure:
                            _sequence = 0;
                            ReturnCode = BehaviourReturnCode.Failure;
                            return ReturnCode;
                        case BehaviourReturnCode.Success:
                            _sequence++;
                            ReturnCode = BehaviourReturnCode.Running;
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
                    _sequence = 0;
                    ReturnCode = BehaviourReturnCode.Failure;
                    return ReturnCode;
                }

            }

            _sequence = 0;
            ReturnCode = BehaviourReturnCode.Success;
            return ReturnCode;

        }

    }
}
