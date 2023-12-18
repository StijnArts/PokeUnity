using Assets.Scripts.Battle.Conditions;
using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using Assets.Scripts.Registries;
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
        readonly public string Id = "";
        public Dictionary<string, EffectState> PseudoWeathers = new();
        public string Weather = "";
        public string Terrain = "";
        public EffectState TerrainState = new();
        public EffectState WeatherState = new();

        public BattleField(Battle battle)
        {
            Battle = battle;
        }

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

        public bool RemovePseudoWeather(string statusAsString = null, Effect statusAsEffect = null)
        {
            Effect status = null;
            if (statusAsEffect != null) status = statusAsEffect;
            else if (statusAsString != null) status = ConditionRegistry.GetConditionById(statusAsString);
            else throw new ArgumentNullException("pseudo weather status cannot be null");

            if (!PseudoWeathers.Keys.Contains(status.Id)) return false;
            var state = PseudoWeathers[status.Id];
            Battle.SingleEvent("FieldEnd", status, state, this);
            PseudoWeathers[status.Id].Remove(status.Id);

            return true;
        }

        public Condition GetWeather()
        {
            return ConditionRegistry.GetConditionById(Weather);
        }

        public Condition GetTerrain()
        {
            return ConditionRegistry.GetConditionById(Terrain);
        }

        public bool ClearWeather()
        {
            if(string.IsNullOrEmpty(Weather)) return false;
            var previousWeather = GetWeather();
            Battle.SingleEvent("FieldEnd", previousWeather, WeatherState, this);
            Weather = "";
            WeatherState = new EffectState();
            Battle.EachEvent("WeatherChange");
            return true;
        }

        public bool ClearTerrain()
        {
            if (string.IsNullOrEmpty(Terrain)) return false;
            var previousTerrain = GetWeather();
            Battle.SingleEvent("FieldEnd", previousTerrain, TerrainState, this);
            Terrain = "";
            TerrainState = new EffectState();
            Battle.EachEvent("TerrainChange");
            return true;
        }

        public Condition GetPseudoWeather(string conditionAsString = null, Effect conditionAsEffect = null)
        {
            Condition status;
            if (conditionAsEffect != null) conditionAsString = conditionAsEffect.Id;
            if (conditionAsString != null) status = ConditionRegistry.GetConditionById(conditionAsString);
            else throw new ArgumentNullException("pseudo weather status cannot be null");

            return PseudoWeathers.ContainsKey(status.Id) ? status : null;
        }
    }
}
