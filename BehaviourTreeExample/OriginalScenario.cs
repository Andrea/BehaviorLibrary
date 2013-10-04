using System;
using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using BehaviourLibrary.Components.Conditionals;
using BehaviourLibrary.Components.Decorators;

namespace BehaviourTreeExample
{
	/// <summary>
	/// To run simply setup and call Behave
	/// </summary>
	public class OriginalScenario
	{
		private Behaviour _behaviour;

		private void Setup()
		{
			var tooClose = new Conditional(isTooClose);
			var targetMoved = new Conditional(hasTargetMoved);
			var pathFound = new Conditional(hasPathBeenFound);
			var reachedCell = new Conditional(hasReachedCell);
			var reachedTarget = new Conditional(hasReachedTarget);
			var isNewPath = new Conditional(hasNewPath);

			//setup all actions and their delegate functions
			BehaviourAction moveToCell = new BehaviourAction(moveTowardsCell);
			BehaviourAction calcPath = new BehaviourAction(calculatePath);
			BehaviourAction initPathfinder = new BehaviourAction(initializePathfinder);
			BehaviourAction getNextCell = new BehaviourAction(getNextPathCell);
			BehaviourAction setPath = new BehaviourAction(setNewPath);
			BehaviourAction getPath = new BehaviourAction(getCurrentPath);
			BehaviourAction updatePosition = new BehaviourAction(updateTargetPosision);
			BehaviourAction reset = new BehaviourAction(resetPathfinder);
			BehaviourAction animate = new BehaviourAction(updateAnimation);

			//setup an initilization branch
			var initialize = new Sequence(initPathfinder, calcPath);

			//if the target has moved, reset and calculate a new path
			var ifMovedCreateNewPath = new Selector(new Inverter(targetMoved), new Inverter(reset), calcPath);
			var ifPathFoundGetPath = new Selector(new Inverter(pathFound), getPath);
			var ifPathNewUseIt = new Selector(new Inverter(isNewPath), setPath);
			var ifReachedCellGetNext = new Selector(new Inverter(reachedCell), getNextCell);
			var ifNotReachedTargetMoveTowardsCell = new Selector(reachedTarget, moveToCell);

			var follow = new Selector(new Inverter(tooClose), updatePosition, ifMovedCreateNewPath, ifPathFoundGetPath,
				ifPathNewUseIt, ifReachedCellGetNext, ifNotReachedTargetMoveTowardsCell, animate);

			var root = new RootSelector(SwitchBehaviours, initialize, follow);

			//set a reference to the root
			_behaviour = new Behaviour(root);
		}

		public void Behave()
		{
			//to execute the behavior
			_behaviour.Behave();
			Console.ReadLine();
		}

		private BehaviourReturnCode updateAnimation()
		{

			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode resetPathfinder()
		{
			Console.WriteLine("Action - Reset path finder");
			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode updateTargetPosision()
		{
			Console.WriteLine("Action - Update Target position ");
			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode getCurrentPath()
		{
			Console.WriteLine("Action - Get current path");
			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode setNewPath()
		{
			Console.WriteLine("Action - set new path ");
			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode getNextPathCell()
		{
			Console.WriteLine("Action - Get next path cell");
			return BehaviourReturnCode.Success;
		}

		private bool hasNewPath()
		{
			Console.WriteLine("Conditional - Has new path");
			return true;
		}

		private bool hasReachedTarget()
		{
			Console.WriteLine("Conditional - Has reached target");
			return true;
		}

		private bool hasReachedCell()
		{
			Console.WriteLine("Conditional - Has reached Cell");
			return true;
		}

		private bool hasPathBeenFound()
		{
			Console.WriteLine("Conditional - Has path been found");
			return true;
		}

		private BehaviourReturnCode initializePathfinder()
		{
			Console.WriteLine("Action - init path finder");
			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode calculatePath()
		{
			Console.WriteLine("Action - CAlculate Path");
			return BehaviourReturnCode.Success;
		}

		private BehaviourReturnCode moveTowardsCell()
		{
			Console.WriteLine("Action - Move towards cell");
			return BehaviourReturnCode.Success;
		}

		private bool hasTargetMoved()
		{
			Console.WriteLine("Conditional - Has target moved");
			return true;
		}

		private bool isTooClose()
		{
			return Helper.LogAndReturn(false);
		}

		private int SwitchBehaviours()
		{
			return Helper.LogAndReturn(0);
		}
	}
}