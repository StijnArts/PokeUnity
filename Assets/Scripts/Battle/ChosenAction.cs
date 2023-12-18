using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class ChosenAction
    {
        public BattleActionTypes Choice;
        public PokemonIndividualData Pokemon;
        public int? TargetLocation;
        public string MoveId;
        public ActiveMove Move;
        public PokemonIndividualData Target;
        public int? Index;
        public BattleController Side;
        public Dictionary<string, object> GimmickChoice = new();
        public int? Priority;
    }
}
