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
        public static PlayerBattleController PlayerBattleController;
        private Battle currentBattle;
        public void StartBattle(BattleController thisSide, List<BattleController> opposingSides)
        {
            currentBattle = gameObject.AddComponent<Battle>();
            currentBattle.PrepareBattle();
            GameStateManager.SetState(GameStateManager.GameStates.BATTLE);
            _battleUiManager.StartBattle(playerSidePokemon[0], opposingSides[0][0]);
        }

        public BattleSide BattleSideFromParty(List<PokemonIndividualData> party)
        {
            throw new NotImplementedException();
        }
    }
}
