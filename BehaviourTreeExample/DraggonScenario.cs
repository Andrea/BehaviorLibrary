using System;
using BehaviourLibrary;
using BehaviourLibrary.Components.Actions;
using BehaviourLibrary.Components.Composites;
using BehaviourLibrary.Components.Conditionals;
using BehaviourLibrary.Components.Decorators;

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
		private Behaviour _behaviour;
		private int _i;
		private int _nodeState;

		public void Setup()
		{
			var isThiefNearTreasureConditional = new Conditional(IsThiefNearTreasure);
			var makethiefFleeAction = new BehaviourAction(MakeThiefFlee);
			var sequence = new Sequence(new Inverter(isThiefNearTreasureConditional), makethiefFleeAction);

			var chooseCastleAction = new BehaviourAction(ChooseACastleToFlyTo);
			var flytoCastleAction = new BehaviourAction(FlyToCastle);
			var fightAction = new BehaviourAction(FightGuards);
			var strongEnoughConditional = new Conditional(StrongEnough);
			var takeGold = new BehaviourAction(TakeGold);
			var flytoHomeAction = new BehaviourAction(FlyToHome);
			var storeRobingsAction = new BehaviourAction(StoreGold);
			var secondSequence = new Sequence(chooseCastleAction, flytoCastleAction, fightAction, strongEnoughConditional, takeGold, 
									flytoHomeAction, storeRobingsAction);
			var rootSelector = new RootSelector(SwitchNodes, sequence, secondSequence);
			_behaviour = new Behaviour(rootSelector);
		}

		private int SwitchNodes()
		{
			return _nodeState;
		}

		private BehaviourReturnCode StoreGold()
		{
			_nodeState++;
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		private BehaviourReturnCode FlyToHome()
		{
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		private BehaviourReturnCode TakeGold()
		{
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		private bool StrongEnough()
		{
			return Helper.LogAndReturn(true);
		}

		private BehaviourReturnCode FightGuards()
		{
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		private BehaviourReturnCode FlyToCastle()
		{
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		private BehaviourReturnCode ChooseACastleToFlyTo()
		{
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		public void Behave()
		{
			_behaviour.Behave();
			Console.ReadLine();
		}


		private BehaviourReturnCode MakeThiefFlee()
		{
			if (_i < 3)
			{
				_i++;
				return Helper.LogAndReturn(BehaviourReturnCode.Running);
			}
			
			_nodeState++;
			return Helper.LogAndReturn(BehaviourReturnCode.Success);
		}

		private bool IsThiefNearTreasure()
		{
			return Helper.LogAndReturn(false);
		}

		
	}
}