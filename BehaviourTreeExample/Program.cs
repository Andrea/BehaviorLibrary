using System;

using BehaviorLibrary;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Conditionals;
using BehaviorLibrary.Components.Decorators;

namespace BehaviourTreeExample
{
	public class Program
	{
		private static int _behaviour;

		static void Main(string[] args)
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
			ParallelSequence initialize = new ParallelSequence(initPathfinder, calcPath);

			//if the target has moved, reset and calculate a new path
			ParallelSelector ifMovedCreateNewPath = new ParallelSelector(new Inverter(targetMoved), new Inverter(reset), calcPath);
			ParallelSelector ifPathFoundGetPath = new ParallelSelector(new Inverter(pathFound), getPath);
			ParallelSelector ifPathNewUseIt = new ParallelSelector(new Inverter(isNewPath), setPath);
			ParallelSelector ifReachedCellGetNext = new ParallelSelector(new Inverter(reachedCell), getNextCell);
			ParallelSelector ifNotReachedTargetMoveTowardsCell = new ParallelSelector(reachedTarget, moveToCell);

			ParallelSequence follow = new ParallelSequence(new Inverter(tooClose), updatePosition, ifMovedCreateNewPath, ifPathFoundGetPath,
				ifPathNewUseIt, ifReachedCellGetNext, ifNotReachedTargetMoveTowardsCell, animate);
			
			RootSelector root = new RootSelector(SwitchBehaviours, initialize, follow);

			//set a reference to the root
			var behavior = new Behavior(root);

			//to execute the behavior
			behavior.Behave();
			Console.ReadLine();
		}

		private static BehaviorReturnCode updateAnimation()
		{
			Console.WriteLine("Action - Update Animation");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode resetPathfinder()
		{
			Console.WriteLine("Action - Reset path finder");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode updateTargetPosision()
		{
			Console.WriteLine("Action - Update Target position ");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode getCurrentPath()
		{
			Console.WriteLine("Action - Get current path");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode setNewPath()
		{
			Console.WriteLine("Action - set new path ");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode getNextPathCell()
		{
			Console.WriteLine("Action - Get next path cell");
			return BehaviorReturnCode.Success;
		}

		private static bool hasNewPath()
		{
			Console.WriteLine("Conditional - Has new path");
			return true;
		}

		private static bool hasReachedTarget()
		{
			Console.WriteLine("Conditional - Has reached target");
			return true;
		}

		private static bool hasReachedCell()
		{
			Console.WriteLine("Conditional - Has reached Cell");
			return true;
		}

		private static bool hasPathBeenFound()
		{
			Console.WriteLine("Conditional - Has path been found");
			return true;
		}

		private static BehaviorReturnCode initializePathfinder()
		{
			Console.WriteLine("Action - init path finder");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode calculatePath()
		{
			Console.WriteLine("Action - CAlculate Path");
			return BehaviorReturnCode.Success;
		}

		private static BehaviorReturnCode moveTowardsCell()
		{
			Console.WriteLine("Action - Move towards cell");
			return BehaviorReturnCode.Success;
		}

		private static bool hasTargetMoved()
		{
			Console.WriteLine("Conditional - Has target moved");
			return true;
		}

		private static bool isTooClose()
		{
			Console.WriteLine("Conditional - Is too close");
			return false;

		}

		private static int SwitchBehaviours()
		{
			Console.WriteLine("Switch behaviours {0}", _behaviour++);
			return _behaviour;

		}
	}
}
