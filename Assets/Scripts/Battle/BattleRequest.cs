using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleRequest
    {
        public List<string> ForceSwitch = new();
        public int MaxChosenTeamSize;
        public BattleRequestData Side;
        public BattleRequestData Ally;
        public bool? Wait;
        public bool? NoCancel;
    }

    public class BattleRequestData
    {
        public string Name;
        public string Id;
        public List<BattleRequestEntry> Pokemon;
    }

    public interface BattleRequestEntry
    {

    }

    public class SwitchRequestData : BattleRequestEntry
    {
        public string Identity;
        public string Details;
        public int Condition;
        public bool Active;
        public PokemonStats Stats;
        public List<string> Moves;
        public string BaseAbility;
        public string Item;
        public string Pokeball;
        public bool? Commanding;
        public bool? Reviving;
        public string TeraType;
        public string Terastallized;

        public object Ability { get; internal set; }
    }

    public class MoveRequestData : BattleRequestEntry
    {
        public List<MoveRequestDataEntry> Moves;
        public bool? MaybeDisabled;
        public bool? Trapped;
        public bool? MaybeTrapped;
        public bool? CanMegaEvolve;
        public bool? CanUltraBurst;
        public ZMoveOptions CanZMove;
        public bool? CanDynamax;
        public DynamaxOptions MaxMoves;
        public string canTerastallize;
    }

    public class MoveRequestDataEntry
    {
        public string Move;
        public string Id;
        public string Target;
        public string Disabled;
    }

    public class DynamaxOptions
    {
        public string Gigantamax;
        public List<MaxMoveRequestData> MaxMoves;
    }

    public class MaxMoveRequestData
    {
        public string Move;
        public Target.TargettingType Target;
        public bool? Disabled;
    }

    public class ZMoveOptions
    {
        public string Move;
        public Target.TargettingType Target;
    }
}
