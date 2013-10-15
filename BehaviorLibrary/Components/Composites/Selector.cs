using System;

namespace BehaviourLibrary.Components.Composites
{
    public class Selector : BehaviourComponent
    {
	    private BehaviourComponent[] _behaviours;


        /// <summary>
        /// Selects among the given behavior components
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviours">one to many behavior components</param>
		public Selector(string name, params BehaviourComponent[] behaviours)
        {
	        Name = name;
	        _behaviours = behaviours;
        }

	    /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviourReturnCode Behave()
        {
            
            for (int i = 0; i < _behaviours.Length; i++)
            {
                try
                {
                    switch (_behaviours[i].Behave())
                    {
                        case BehaviourReturnCode.Failure:
                            continue;
                        case BehaviourReturnCode.Success:
                            ReturnCode = BehaviourReturnCode.Success;
                            return ReturnCode;
                        case BehaviourReturnCode.Running:
                            ReturnCode = BehaviourReturnCode.Running;
                            return ReturnCode;
                        default:
                            continue;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                }
            }

            ReturnCode = BehaviourReturnCode.Failure;
            return ReturnCode;
        }
    }
}
