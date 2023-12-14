using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleField : Target, EffectHolder
    {
        readonly public Battle Battle;
        readonly public string Id;
        public Dictionary<string, EffectState> PseudoWeathers = new();
        public string Weather;
        public string Terrain;
        public EffectState TerrainState;

        public bool SuppressingWeather()
        {
            foreach(var side in Battle.Participants){
                foreach(var activePokemon in side.ActivePokemon.Select(pokemonNpc=>pokemonNpc.PokemonIndividualData))
                {
                    if (activePokemon != null && !activePokemon.Fainted && activePokemon.IgnoringAbility() && activePokemon.GetAbility().SuppressWeather)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
