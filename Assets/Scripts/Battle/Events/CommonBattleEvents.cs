using Assets.Scripts.Pokemon;
using System;

namespace Assets.Scripts.Battle.Events
{
    public abstract class CommonBattleEventsHandlers
    {
        //Not used anywhere in the showdown code. Perhaps should ask how this works?
        /*public Action ModifierEffect(Battle battle, decimal relayVar, PokemonIndividualData target, PokemonIndividualData source, effect Effect, out decimal? number)
        {
            number = number;
            return null;
        }*/
        //Generic event to modify a move before it's used
        public Action<decimal?> ModifierMove(Battle battle = null, decimal? relayVar = null, PokemonIndividualData target = null, PokemonIndividualData source = null, PokemonMove move = null) => null;
        //Generic event to modify the result of a move
        public Func<bool?> ResultMove(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, PokemonMove move = null) => () => false;
        //Checks if a condition is true based on the properties of the other move
        public Func<Tuple<bool?,double?>> ExtResultMove(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, PokemonMove move = null) => null;
        public Action VoidEffect(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, Effect Effect = null) => null;
        public Action VoidMove(Battle battle = null, PokemonIndividualData target = null, PokemonIndividualData source = null, PokemonMove move = null) => null;
        public Action ModifierSourceEffect(Battle battle = null, decimal? relayVar = null, PokemonIndividualData source = null, PokemonIndividualData target = null, effect Effect = null) => number | void;
        public Action ModifierSourceMove(Battle battle = null, decimal? relayVar = null, PokemonIndividualData source = null, PokemonIndividualData target = null, PokemonMove move = null) => number | void;
        public Action ResultSourceMove(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, PokemonMove move = null) => boolean | null | "" | void);
	    public Action ExtResultSourceMove(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, PokemonMove move = null) => boolean | null | number | "" | void);
	    public Action VoidSourceEffect(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, effect Effect = null);
        public Action VoidSourceMove(Battle battle = null, PokemonIndividualData source = null, PokemonIndividualData target = null, PokemonMove move = null);
    }
}
