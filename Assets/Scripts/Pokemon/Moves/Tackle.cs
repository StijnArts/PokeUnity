using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Battle;
using Assets.Scripts.Pokemon.Data.Moves;
using Assets.Scripts.Pokemon.PokemonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Moves
{
    public class Tackle : Move
    {
        public static string MoveIdentifier = "tackle";

        public Tackle() : base(MoveIdentifier, "Tackle", Normal.TypeName, MoveCategories.Physical, 40, Target.TargettingType.Normal, 100)
        {
            BasePower = 40;
            
        }
    }
}
