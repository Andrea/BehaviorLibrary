using BehaviorLibrary;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Conditionals;
using BehaviorLibrary.Components.Decorators;

namespace BehaviourTreeExample
{
	public class Program
	{
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
				isNewPath, ifReachedCellGetNext, ifNotReachedTargetMoveTowardsCell, animate);

			RootSelector root = new RootSelector(switchBehaviors, initialize, follow);

			//set a reference to the root
			var behavior = new Behavior(root);

			//to execute the behavior
			behavior.Behave();
		}
	}
}
