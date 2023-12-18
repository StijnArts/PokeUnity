using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleChoice
    {
        public string Error;
        public List<ChosenAction> Actions;
        public int forcedSwitchesLeft;
        public int forcedPassesLeft;
        public List<PokemonIndividualData> SwitchIns;
        public Dictionary<string, bool> GimmicksSelected = new();
    }
}
