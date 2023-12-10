using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Water : PokemonType
    {
        public static string TypeName = "water";
        public Water() : base(TypeName,  //type name
                                        new List<string> { Fire.TypeName, Ground.TypeName, Rock.TypeName }, //strong against
                                        new List<string> { Dragon.TypeName, Grass.TypeName, Water.TypeName }) //resisted by
        {
        }
    }
}
