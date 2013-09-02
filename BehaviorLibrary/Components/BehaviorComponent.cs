namespace BehaviorLibrary.Components
{
	public abstract class BehaviorComponent
	{
		protected BehaviorReturnCode ReturnCode;

		public abstract BehaviorReturnCode Behave();
	}
}