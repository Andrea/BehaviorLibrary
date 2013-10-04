namespace BehaviourLibrary.Components
{
	public abstract class BehaviourComponent
	{
		protected BehaviourReturnCode ReturnCode;

		public abstract BehaviourReturnCode Behave();
	}
}