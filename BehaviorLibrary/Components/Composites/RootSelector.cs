using System;

namespace BehaviourLibrary.Components.Composites
{
    public class RootSelector : PartialSelector
    {

        private BehaviourComponent[] _rsBehaviours;

        private Func<int> rs_Index;

        /// <summary>
        /// The selector for the root node of the behavior tree
        /// </summary>
        /// <param name="index">an index representing which of the behavior branches to perform</param>
        /// <param name="behaviours">the behavior branches to be selected from</param>
        public RootSelector(Func<int> index, params BehaviourComponent[] behaviours)
        {
            rs_Index = index;
            _rsBehaviours = behaviours;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviourReturnCode Behave()
        {
            try
            {
                switch (_rsBehaviours[rs_Index.Invoke()].Behave())
                {
                    case BehaviourReturnCode.Failure:
                        ReturnCode = BehaviourReturnCode.Failure;
                        return ReturnCode;
                    case BehaviourReturnCode.Success:
                        ReturnCode = BehaviourReturnCode.Success;
                        return ReturnCode;
                    case BehaviourReturnCode.Running:
                        ReturnCode = BehaviourReturnCode.Running;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviourReturnCode.Running;
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
