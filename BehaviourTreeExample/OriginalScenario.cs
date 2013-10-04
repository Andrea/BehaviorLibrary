using System;
using BehaviorLibrary;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Conditionals;
using BehaviorLibrary.Components.Decorators;

namespace BehaviourTreeExample
{
	/// <summary>
	/// To run simply setup and call Behave
	/// </summary>
	public class OriginalScenario
	{
		private Behavior _behavior;

		private void Setup()
		{
			var tooClose = new Conditional(isTooClose);
			var targetMoved = new Conditional(hasTargetMoved);
			var pathFound = new Conditional(hasPathBeenFound);
			var reachedCell = new Conditional(hasReachedCell);
			var reachedTarget = new Conditional(hasReachedTarget);
			var isNewPath = new Conditional(hasNewPath);

			//setup all actions and their delegate functions
			BehaviorAction moveToCell = new BehaviorAction(moveTowardsCell);
			BehaviorAction calcPath = new BehaviorAction(calculatePath);
			BehaviorAction initPathfinder = new BehaviorAction(initializePathfinder);
			BehaviorAction getNextCell = new BehaviorAction(getNextPathCell);
			BehaviorAction setPath = new BehaviorAction(setNewPath);
			BehaviorAction getPath = new BehaviorAction(getCurrentPath);
			BehaviorAction updatePosition = new BehaviorAction(updateTargetPosision);
			BehaviorAction reset = new BehaviorAction(resetPathfinder);
			BehaviorAction animate = new BehaviorAction(updateAnimation);

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
			_behavior = new Behavior(root);
		}

		public void Behave()
		{
			//to execute the behavior
			_behavior.Behave();
			Console.ReadLine();
		}

		private BehaviorReturnCode updateAnimation()
		{

			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode resetPathfinder()
		{
			Console.WriteLine("Action - Reset path finder");
			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode updateTargetPosision()
		{
			Console.WriteLine("Action - Update Target position ");
			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode getCurrentPath()
		{
			Console.WriteLine("Action - Get current path");
			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode setNewPath()
		{
			Console.WriteLine("Action - set new path ");
			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode getNextPathCell()
		{
			Console.WriteLine("Action - Get next path cell");
			return BehaviorReturnCode.Success;
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

		private BehaviorReturnCode initializePathfinder()
		{
			Console.WriteLine("Action - init path finder");
			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode calculatePath()
		{
			Console.WriteLine("Action - CAlculate Path");
			return BehaviorReturnCode.Success;
		}

		private BehaviorReturnCode moveTowardsCell()
		{
			Console.WriteLine("Action - Move towards cell");
			return BehaviorReturnCode.Success;
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