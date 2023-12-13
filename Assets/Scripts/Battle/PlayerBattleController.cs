using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class PlayerBattleController : BattleController
    {
        public PlayerController PlayerController;

        public PlayerBattleController(PlayerController playerController)
        {
            PlayerController = playerController;
        }

        public override bool CreateActivePokemon()
        {
            throw new NotImplementedException();
        }

        public override bool SelectParticipatingPokemon(int numberAllowed = 0)
        {
            if(numberAllowed == 0)
            {
                numberAllowed = Settings.MaxPartySize;
            }

        }
    }
}
