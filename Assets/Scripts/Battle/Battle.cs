using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Pokemon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class Battle : MonoBehaviour, Target
    {
        public BattleSettings _battleSettings;
        public bool BattleIsReady;
        public bool PreparingBattle;
        public List<BattleController> Participants;
        public Dictionary<BattleController, List<PokemonNpc>> ActivePokemon;
        public List<PseudoWeather> PseudoWeathers = new List<PseudoWeather>();
        public BattleWeather Weathers;
        public Terrain Terrain;
        public BattleManager BattleManager => ServiceLocator.Instance.BattleManager;
        public bool StartOfPokemonTurn = true;
        private void Start()
        {
            enabled = false;
        }

        public void PrepareBattle(List<BattleController> participants)
        {
            Participants = participants;
            enabled = true;
        }

        private void Update()
        {
            
            if (!BattleIsReady)
            {
                StartCoroutine(PrepareBattle());
            } else
            {
                if (ParticipantsAreReady())
                {
                    if(AllPokemonHaveMoved())
                    {
                        ResetParticipantTurnState();
                    } else
                    {
                        PlayTurn();
                    }
                } else
                {

                }
                
            }
        }

        IEnumerator PrepareBattle()
        {
            yield return new WaitUntil(BattleIsPrepared);
            BattleIsReady = true;
        }

        public void PlayTurn()
        {
            var executingPokemon = GetActivePokemonIndividualData().Where(pokemon => !pokemon.BattleData.HasMovedThisTurn).First();
            if (StartOfPokemonTurn)
            {
                DecideMoveOrder();
                StartOfPokemonTurn = false;
            }
            executingPokemon.BattleData.PlayTurn();
        }

        private void DecideMoveOrder()
        {
            GetActivePokemonWithBattleControllers().ForEach(activePokemon => 
                activePokemon.Item1.BattleData.CalculateTurnSpeed(activePokemon.Item1.Stats.speed, this, activePokemon.Item2));
            GetActivePokemonIndividualData().Sort((PokemonIndividualData a, PokemonIndividualData b) => 
            { 
                if(a.BattleData.Priority > b.BattleData.Priority)
                {
                    return 1;
                }
                else if (a.BattleData.TurnSpeed > b.BattleData.TurnSpeed)
                {
                    return 1;
                }
                else if (a.BattleData.TurnSpeed == b.BattleData.TurnSpeed)
                {
                    var values = new int[] { -1, 1 };
                    var random = new Unity.Mathematics.Random();
                    return values[random.NextInt(0, 1)];
                }

                return -1;
            });
        }

        private bool AllPokemonHaveMoved()
        {
            return GetActivePokemonIndividualData().All(pokemon => pokemon.BattleData.HasMovedThisTurn == true);
        }

        public List<PokemonIndividualData> GetActivePokemonIndividualData()
        {
            return ActivePokemon.SelectMany(value => value.Value).Select(pokemonNpc => pokemonNpc.PokemonIndividualData).ToList();
        }

        public List<Tuple<PokemonIndividualData, BattleController>> GetActivePokemonWithBattleControllers()
        {
            var activePokemonWithControllers = new List<Tuple<PokemonIndividualData, BattleController>>();
            foreach (var activePokemon in ActivePokemon)
            {
                var controller = activePokemon.Key;
                foreach(var pokemon in activePokemon.Value)
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

        private void ResetParticipantTurnState()
        {
            foreach (var participant in Participants) 
            { 
                participant.FinishedTurn = false;
            }
        }

        private bool BattleIsPrepared()
        {
            var participantsHaveSelectedPokemon = !Participants
                .Where(participant => participant.ParticipatingPokemon == null && !participant.IsSelectingParticipatingPokemon)
                .Select(participant => participant.SelectParticipatingPokemon())
                .Any();
            var participantsHaveActivePokemon = false;
            if (participantsHaveSelectedPokemon && !participantsHaveActivePokemon)
            {
                participantsHaveActivePokemon = !Participants
                .Where(participant => participant.ActivePokemon == null && !participant.HasActivePokemon)
                .Select(participant => participant.CreateActivePokemon(_battleSettings.MinimumAmountOfActivePokemon))
                .Any();
            }

        
            return participantsHaveSelectedPokemon && participantsHaveActivePokemon;
        }


    }
}
