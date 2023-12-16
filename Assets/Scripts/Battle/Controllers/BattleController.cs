using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Pokemon;
using Assets.Scripts.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.Battle
{
    public abstract class BattleController : BattleSide
    {
        public List<PokemonNpc> ActivePokemon;
        public bool FinishedTurn = false;
        public List<PokemonIndividualData> ParticipatingPokemon;
        public int NumberOfMaxActivePokemon = 1;
        public bool IsSelectingParticipatingPokemon = false;
        public bool HasActivePokemon = false;
        public PokemonIndividualData FaintedLastTurn;
        public PokemonIndividualData FaintedThisTurn;
        public int TotalFainted => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp <= 0).Count();
        public int PokemonLeft => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp >= 1).Count();
        public abstract void SelectMove(PokemonIndividualData pokemonToMove, ActiveMove move, List<Target> targets);

        public abstract bool SelectParticipatingPokemon(int numberAllowed = 0);

        //TODO make it create pokemonNpc's for the amount of necessary active pokemon in the battle, and set the individual data of those npcs;
        public abstract bool CreateActivePokemon(int minimumAmountOfActivePokemon);

        public bool RemoveSlotCondition(int? slotAsInt = null, PokemonIndividualData slotAsPokemon = null, string statusAsString = null, Effect statusAsEffect = null)
        {
            int? target = null;
            if (slotAsInt.HasValue) target = slotAsInt.Value;
            else if (slotAsPokemon != null) target = GetSlotNumber(slotAsPokemon);
            else throw new ArgumentNullException("linkedStatus cannot be null");
            Effect status = null;
            if (statusAsEffect != null) status = statusAsEffect;
            else if (statusAsString != null) status = ConditionRegistry.GetConditionById(statusAsString);
            else throw new ArgumentNullException("linkedStatus cannot be null");

            if (!SlotConditions.Keys.Contains(target.Value) && !SlotConditions[target.Value].ContainsKey(status.Id)) return false;
            var condition = SlotConditions[target.Value][status.Id];
            battle.SingleEvent("End", status, condition, GetActivePokemonData()[target.Value]);
            SlotConditions[target.Value].Remove(status.Id);

            return true;
        }

        public int? GetSlotNumber(PokemonIndividualData slotAsPokemon)
        {
            List<PokemonIndividualData> activePokemon = GetActivePokemonData();
            var indexOfPokemon = Enumerable.Range(0, NumberOfMaxActivePokemon).Where(iterator => activePokemon.Count - 1 <= iterator && activePokemon[iterator] == slotAsPokemon).ToList();
            return indexOfPokemon.Count > 0 ? indexOfPokemon[0] : null;
        }

        private List<PokemonIndividualData> GetActivePokemonData()
        {
            return ActivePokemon.Select(pokemon => pokemon.PokemonIndividualData).ToList();
        }
    }
}
