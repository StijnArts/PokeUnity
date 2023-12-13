using System;

namespace Assets.Scripts.Battle.Events
{
    public abstract class MoveEvents : CommonBattleEventsHandlers
    {
        public Action basePowerCallback (this: Battle, pokemon: Pokemon, target: Pokemon, move: ActiveMove) => number | false | null;
        /** Return true to stop the move from being used */
        public Action beforeMoveCallback(this: Battle, pokemon: Pokemon, target: Pokemon | null, move: ActiveMove) => boolean | void;
        public Action beforeTurnCallback(this: Battle, pokemon: Pokemon, target: Pokemon) => void;
        public Action damageCallback(this: Battle, pokemon: Pokemon, target: Pokemon) => number | false;
        public Action priorityChargeCallback(this: Battle, pokemon: Pokemon) => void;

        public Action onDisableMove(this: Battle, pokemon: Pokemon) => void;

        public Action onAfterHit => VoidSourceMove;
        public Action onAfterSubDamage(this: Battle, damage: number, target: Pokemon, source: Pokemon, move: ActiveMove) => void;
        public Action onAfterMoveSecondarySelf CommonHandlers['VoidSourceMove'];
        public Action onAfterMoveSecondary CommonHandlers['VoidMove'];
        public Action onAfterMove CommonHandlers['VoidSourceMove'];
        public Action onDamagePrioritynumber;
        public Action onDamage(this: Battle, damage: number, target: Pokemon, source: Pokemon, effect: Effect) => number | boolean | null | void;

        /* Invoked by the global BasePower event (onEffect = true) */
        public Action onBasePowerCommonHandlers['ModifierSourceMove'];

        public Action onEffectiveness(this: Battle, typeMod: number, target: Pokemon | null, type: string, move: ActiveMove) => number | void;
	onHitCommonHandlers['ResultMove'];
	onHitFieldCommonHandlers['ResultMove'];
	onHitSide(this: Battle, side: Side, source: Pokemon, move: ActiveMove) => boolean | null | "" | void;
	onModifyMove(this: Battle, move: ActiveMove, pokemon: Pokemon, target: Pokemon | null) => void;
	onModifyPriorityCommonHandlers['ModifierSourceMove'];
	onMoveFailCommonHandlers['VoidMove'];
	onModifyType(this: Battle, move: ActiveMove, pokemon: Pokemon, target: Pokemon) => void;
	onModifyTarget(

        this: Battle, relayVar: {target: Pokemon}, pokemon: Pokemon, target: Pokemon, move: ActiveMove
	) => void;
	onPrepareHitCommonHandlers['ResultMove'];
	onTryCommonHandlers['ResultSourceMove'];
	onTryHitCommonHandlers['ExtResultSourceMove'];
	onTryHitFieldCommonHandlers['ResultMove'];
	onTryHitSide(this: Battle, side: Side, source: Pokemon, move: ActiveMove) => boolean |
	 null | "" | void;
	onTryImmunityCommonHandlers['ResultMove'];
	onTryMoveCommonHandlers['ResultSourceMove'];
	onUseMoveMessageCommonHandlers['VoidSourceMove'];
    }
}
