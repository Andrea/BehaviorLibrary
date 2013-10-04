using System;
using BehaviorLibrary;
using BehaviorLibrary.Components.Actions;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Conditionals;
using BehaviorLibrary.Components.Decorators;

namespace BehaviourTreeExample
{
	/// <summary>
	/// Based on example here http://www.altdevblogaday.com/2011/02/24/introduction-to-behavior-trees/
	/*
	0. Root priority selector
    1. Guard treasure concurrent selector
        1.1. Is thief near treasure detectable? condition
        1.2. Make thief flee (or eat him/her) behavior
    2. Rob more treasures sequence selector
        2.1. Choose a castle to fly to! action
        2.2. Fly to castle! action
        2.3. Fight (and eat) guards
        2.4. Is still strong enough to carry treasure home? condition
        2.5. Take gold! action
        2.6. Fly home! action
        2.7. Put newly robbed treasure to possessed treasure! action    
	 */
	/// </summary>
	public class DraggonScenario
	{
		private Behavior _behavior;
		private int _i = 0;
		private int _nodeState;

		public void Setup()
		{
			var isThiefNearTreasureConditional = new Conditional(IsThiefNearTreasure);
			var makethiefFleeAction = new BehaviorAction(MakeThiefFlee);
			var sequence = new Sequence(new Inverter(isThiefNearTreasureConditional), makethiefFleeAction);

			var chooseCastleAction = new BehaviorAction(ChooseACastleToFlyTo);
			var flytoCastleAction = new BehaviorAction(FlyToCastle);
			var fightAction = new BehaviorAction(FightGuards);
			var strongEnoughConditional = new Conditional(StrongEnough);
			var takeGold = new BehaviorAction(TakeGold);
			var flytoHomeAction = new BehaviorAction(FlyToHome);
			var storeRobingsAction = new BehaviorAction(StoreGold);
			var secondSequence = new Sequence(chooseCastleAction, flytoCastleAction, fightAction, strongEnoughConditional, takeGold, 
									flytoHomeAction, storeRobingsAction);
			var rootSelector = new RootSelector(SwitchNodes, sequence, secondSequence);
			_behavior = new Behavior(rootSelector);
		}

		private int SwitchNodes()
		{
			return _nodeState;
		}

		private BehaviorReturnCode StoreGold()
		{
			_nodeState++;
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		private BehaviorReturnCode FlyToHome()
		{
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		private BehaviorReturnCode TakeGold()
		{
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		private bool StrongEnough()
		{
			return Helper.LogAndReturn(true);
		}

		private BehaviorReturnCode FightGuards()
		{
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		private BehaviorReturnCode FlyToCastle()
		{
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		private BehaviorReturnCode ChooseACastleToFlyTo()
		{
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		public void Behave()
		{
			_behavior.Behave();
			Console.ReadLine();
		}


		private BehaviorReturnCode MakeThiefFlee()
		{
			if (_i < 3)
			{
				_i++;
				return Helper.LogAndReturn(BehaviorReturnCode.Running);
			}
			
			_nodeState++;
			return Helper.LogAndReturn(BehaviorReturnCode.Success);
		}

		private bool IsThiefNearTreasure()
		{
			return Helper.LogAndReturn(false);
		}

		
	}
}