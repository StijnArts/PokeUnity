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
        public bool Ready = false;
        public PokemonIndividualData FaintedLastTurn;
        public PokemonIndividualData FaintedThisTurn;
        public int TotalFainted => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp <= 0).Count();
        public int PokemonLeft => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp >= 1).Count();
        public abstract void SelectMove(PokemonIndividualData pokemonToMove, ActiveMove move, List<Target> targets);
        public BattleChoice Choice;
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
            Battle.SingleEvent("End", status, condition, GetActivePokemonData()[target.Value]);
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

        public void SetBattle(Battle Battle)
        {
            base.Battle = Battle;
        }

        public void SetBattleData(Battle battle, int slotNumber)
        {
            base.Battle = battle;
            base.SlotNumber = slotNumber;
        }

        public void SetAlliesAndFoes(List<BattleController> foes, BattleController ally = null)
        {
            base.Foes = foes;
            base.AllySide = ally;
            Ready = true;
        }

        public List<PokemonIndividualData> GetFoePokemon()
        {
            return Foes.SelectMany(side => side.ActivePokemon).Select(pokemon => pokemon.PokemonIndividualData).ToList();
        }

        public bool CanDynamaxNow()
        {
            var positions = new int[] { 1, 1, 0, 0 };
            if (Battle.BattleSettings.GameType == BattleType.Multi && Battle.Turn % 2 != positions[this.SlotNumber]) return false;
            return !GimmickUsed.ContainsKey("Dynamax") || GimmickUsed["Dynamax"] == false;
        }

        public void ClearChoice()
        {
            var forcedSwitches = 0;
            var forcedPasses = 0;
            if(Battle.RequestState == BattleRequestState.Switch)
            {
                var canSwitchOut = ActivePokemon.Where(pokemon => !string.IsNullOrEmpty(pokemon.PokemonIndividualData.BattleData.SwitchFlag)).Count();
                var canSwitchIn =  ParticipatingPokemon.Where(pokemon => !GetActivePokemonData().Contains(pokemon)).Where(pokemon => pokemon != null && !pokemon.Fainted).Count();
                forcedSwitches = Math.Min(canSwitchOut, canSwitchIn);
                forcedPasses = canSwitchOut - forcedSwitches;
            }
            Choice = new BattleChoice()
            {
                Error = null,
                Actions = new List<ChosenAction>(),
                forcedSwitchesLeft = forcedSwitches,
                forcedPassesLeft = forcedPasses,
                SwitchIns = new List<PokemonIndividualData>()
            };
        }
    }
}
