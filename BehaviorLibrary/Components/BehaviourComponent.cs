using System.Text;

namespace BehaviourLibrary.Components
{
	public abstract class BehaviourComponent
	{
		private BehaviourReturnCode _returnCode;

		public string Name { get; set; }

		public BehaviourReturnCode ReturnCode
		{
			get { return _returnCode; }
			set
			{
				_returnCode = value;
				UpdatedThisFrame = true;
			}
		}

		public bool UpdatedThisFrame { get; set; }

		public abstract BehaviourReturnCode Behave();
	}
}