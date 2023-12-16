using Assets.Scripts.Pokemon;
using System;

namespace Assets.Scripts.Battle.Events
{
    public interface CommonBattleEventsHandlers
    {
        //Not used anywhere in the showdown code. Perhaps should ask how this works?
        /*public Action ModifierEffect(Battle battle, double relayVar, PokemonIndividualData target, PokemonIndividualData source, effect Effect, out double? number)
        {
            number = number;
            return null;
        }*/
        //Generic event to modify a move before it's used
        //TODO declare the handlers as classes that extend the BattleCallBack Class with the necessary types;
        public BattleCallback<double?> ModifierMove(Battle battle = null, double? relayVar = null, PokemonIndividualData target = null, PokemonIndividualData source = null, ActiveMove move = null) => null;
        //Generic event to modify the result of a move
        public BattleCallback<bool?> ResultMove(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, ActiveMove move = null) => () => false;
        //Checks if a condition is true based on the properties of the other move
        public BattleCallback<Tuple<bool?,double?>> ExtResultMove(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, ActiveMove move = null) => null;
        public BattleCallback<double?> VoidEffect(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, Effect Effect = null) => null;
        public BattleCallback<double?> VoidMove(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, ActiveMove move = null) => null;
        public BattleCallback ModifierSourceEffect(Battle battle = null, double? relayVar = null, PokemonIndividualData source = null, PokemonIndividualData target = null, Effect effect = null) => number | void;
        public BattleCallback ModifierSourceMove(Battle battle = null, double? relayVar = null, PokemonIndividualData source = null, PokemonIndividualData target = null, ActiveMove move = null) => number | void;
        public BattleCallback ResultSourceMove(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, ActiveMove move = null) => boolean | null | "" | void);
	    public BattleCallback ExtResultSourceMove(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, ActiveMove move = null) => boolean | null | number | "" | void);
	    public BattleCallback VoidSourceEffect(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, Effect effect = null);
        public BattleCallback VoidSourceMove(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, ActiveMove move = null);
    }
}
