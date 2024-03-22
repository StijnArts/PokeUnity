using Assets.Scripts.Battle.Controllers;
using Assets.Scripts.Pokemon;
using Assets.Scripts.Pokemon.Data.Moves;
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
        private BattleEngine currentBattle;
        public void StartBattle(BattleController thisSide, List<BattleController> opposingSides)
        {
            var participants = new List<BattleController>
            {
                thisSide
            };
            participants.AddRange(opposingSides);
            currentBattle = new BattleEngine(null, participants);
            //currentBattle.PrepareBattle();
            GameStateManager.SetState(GameStateManager.GameStates.BATTLE);
            _battleUiManager.StartBattle(null, null);//playerSidePokemon[0], opposingSides[0][0]);
        }

        internal void SelectMove(MoveSlot move)
        {
            Console.WriteLine(move.Move);
        }
    }
}
