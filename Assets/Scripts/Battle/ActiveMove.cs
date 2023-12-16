using Assets.Scripts.Battle.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class ActiveMove : Effect
    {
        public Move Move;
        public string Weather = null;
        public string Status = null;
        public int hit;
        public MoveHitData MoveHitData;
        public Ability Ability;
        public List<PokemonIndividualData> Allies;
        public PokemonIndividualData AuraBooster;
        public bool? CausedCrashDamage;
        public string ForcedStatus;
        public bool? HasAuraBreak;
        public bool? HasBounced;
        public bool? HasSheerForce;
        public bool? IsExternal;
        public bool? LastHit;
        public int? Magnitude;
        public bool? NegateSecondary;
        public bool? PranksterBoosted;
        public bool? SelfDropped;
        public enum SelfSwitchStates { CopyVolatile, ShedTail, True, False };
        public SelfSwitchStates? SelfSwitch;
        public bool? SpreadHit;
        public int? STAB;
        public string StatusRoll;
        public int? TotalDamage;
        public Effect TypeChangerBoosted;
        public bool? WillChangeForme;
        public bool? Infiltrates;

        /**
	    * Has this move been boosted by a Z-crystal or used by a Dynamax Pokemon? Usually the same as
	    * `isZ` or `isMax`, but hacked moves will have this be `false` and `isZ` / `isMax` be truthy.
	    */
        public bool? IsZOrMaxPowered;
        public ActiveMove(Move move)
        {
            Move = move;
            EffectType = move.EffectType;
            Name = move.Name;
            Id = move.Id;
        }
    }
}
