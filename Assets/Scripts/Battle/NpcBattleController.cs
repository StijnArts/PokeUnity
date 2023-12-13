using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class NpcBattleController : BattleController
    {

        public override bool SelectParticipatingPokemon(int numberAllowed = 0)
        {
            if (numberAllowed == 0)
            {
                numberAllowed = Settings.MaxPartySize;
            }

        }
    }
}
