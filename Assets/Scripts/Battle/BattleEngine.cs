using Assets.Scripts.Battle.Controllers;
using Assets.Scripts.BattleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class BattleEngine
    {
        public BattleSettings BattleSettings;
        public BattleQueue Queue;
        public int Turn = 1;
        public bool MidTurn = false;
        public bool Started = false;
        public bool Ended = false;
        public string Winner = null;
        public List<BattleController> Participants;
        public Dictionary<BattleController, List<PokemonNpc>> ActivePokemonDictionary;
        public BattleManager BattleManager => ServiceLocator.Instance.BattleManager;
        public DialogManager DialogManager => ServiceLocator.Instance.DialogManager;
        public int AbilityOrder = 0;
        public int ActivePerHalf;

        public string LastSuccesfulMoveThisTurn;
        public bool QuickClawRoll = false;
        public bool StartOfPokemonTurn = true;
        public int HitSubstitute = 0;
        public bool IsFourPlayer => BattleSettings.BattleType == (BattleType.Multi | BattleType.FreeForAll);

        public bool SupportCancel = false;

        public BattleEngine(BattleSettings battleSettings, List<BattleController> participants)
        {
            BattleSettings = battleSettings;
            if (participants.Count < 4 && IsFourPlayer) throw new ArgumentException("There must be at least 4 players present for a Multi or Free For All Battle");
            participants.ForEach(participant =>
            {
                participant.NumberOfMaxActivePokemon = (
                participant is NpcBattleController && BattleSettings.BattleType == BattleType.Wild ? (
                    BattleSettings.BattleType == BattleType.Triples ? 3 : BattleSettings.BattleType == BattleType.Doubles || IsFourPlayer ? 2 : 1)
                : participant.ParticipatingPokemon.Count);

                //participant.ParticipatingPokemon.ForEach(pokemon => pokemon.SetBattleData(participant, this));
            });
            ActivePerHalf = BattleSettings.BattleType == BattleType.Triples ? 3 : BattleSettings.BattleType == BattleType.Doubles || IsFourPlayer ? 2 : 1;
            Queue = new BattleQueue(this);

            Start();
        }

        private bool AllPokemonHaveMoved()
        {
            return GetActivePokemonIndividualData().All(pokemon => pokemon.BattleData.HasMovedThisTurn == true);
        }

        public List<PokemonIndividualData> GetActivePokemonIndividualData()
        {
            return ActivePokemonDictionary.SelectMany(value => value.Value).Select(pokemonNpc => pokemonNpc.PokemonIndividualData).ToList();
        }

        public List<Tuple<PokemonIndividualData, BattleController>> GetActivePokemonWithBattleControllers()
        {
            var activePokemonWithControllers = new List<Tuple<PokemonIndividualData, BattleController>>();
            foreach (var activePokemon in ActivePokemonDictionary)
            {
                var controller = activePokemon.Key;
                foreach (var pokemon in activePokemon.Value)
                {
                    activePokemonWithControllers.Add(new Tuple<PokemonIndividualData, BattleController>(pokemon.PokemonIndividualData, controller));
                }
            }
            return activePokemonWithControllers;
        }

        private bool ParticipantsAreReady()
        {
            return Participants.All(participant => participant.FinishedTurn);
        }

        public void Start()
        {
            //if(Deserialized) return;

            if (Started) throw new Exception("Battle has already started");

            if (Participants.Any(participant => participant.ParticipatingPokemon.Count < 1)) throw new Exception("Battle not started, a participant has no participating pokemon");

        }
    }
}
