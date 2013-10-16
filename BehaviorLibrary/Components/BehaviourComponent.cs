namespace BehaviourLibrary.Components
{
	public abstract class BehaviourComponent
	{
		private BehaviourReturnCode _returnCode;

		public string Name { get; set; }

		public BehaviourReturnCode ReturnCode
		{
			get { return _returnCode; }
			internal set
			{
				_returnCode = value;
				UpdatedThisFrame = true;
			}
		}

		/// <summary>
		/// Used for debugging. This is set when a return code is set and must be reset from the calling library every frame.
		/// </summary>
		public bool UpdatedThisFrame { get; set; }

		public abstract BehaviourReturnCode Behave();
	}
}