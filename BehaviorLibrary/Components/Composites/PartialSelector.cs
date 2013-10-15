using System;

namespace BehaviourLibrary.Components.Composites
{
    public class PartialSelector : BehaviourComponent
    {
	    private BehaviourComponent[] _behaviours;
        private short _selections;
        private short _selLength;

        /// <summary>
		/// Selects among the given behavior components (one evaluation per Behave call)
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Failure or Running
        /// -Returns Failure if all behavior components returned Failure or an error has occured
        /// </summary>
        /// <param name="behaviours">one to many behavior components</param>
		public PartialSelector(string name, params BehaviourComponent[] behaviours)
        {
	        Name = name;
	        _behaviours = behaviours;
            _selLength = (short)_behaviours.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviourReturnCode Behave()
        {
            while (_selections < _selLength)
            {
                try
                {
                    switch (_behaviours[_selections].Behave())
                    {
                        case BehaviourReturnCode.Failure:
                            _selections++;
                            ReturnCode = BehaviourReturnCode.Running;
                            return ReturnCode;
                        case BehaviourReturnCode.Success:
                            _selections = 0;
                            ReturnCode = BehaviourReturnCode.Success;
                            return ReturnCode;
                        case BehaviourReturnCode.Running:
                            ReturnCode = BehaviourReturnCode.Running;
                            return ReturnCode;
                        default:
                            _selections++;
                            ReturnCode = BehaviourReturnCode.Failure;
                            return ReturnCode;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                    _selections++;
                    ReturnCode = BehaviourReturnCode.Failure;
                    return ReturnCode;
                }
            }

            _selections = 0;
            ReturnCode = BehaviourReturnCode.Failure;
            return ReturnCode;
        }
    }
}
