using Assets.Scripts.Pokemon;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private BattleUiManager _battleUiManager => new();
        private PlayerController _player => GameObject.Find("Player").GetComponentInChildren<PlayerController>();

        public void StartBattle(List<PokemonIndividualData> playerSidePokemon, List<List<PokemonIndividualData>> opposingSides)
        {
            GameStateManager.SetState(GameStateManager.GameStates.BATTLE);
            _battleUiManager.StartBattle(playerSidePokemon[0], opposingSides[0][0]);
        }

        internal void SelectMove(PokemonMove move)
        {
            Debug.Log(_player.Party.GetSelectedPokemon().GetName() + " used " + move.Move.MoveName + "!");
        }
    }
}
