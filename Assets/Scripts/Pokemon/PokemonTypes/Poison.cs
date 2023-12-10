using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Poison : PokemonType
    {
        public static string TypeName = "poison";
        public Poison() : base(TypeName,  //type name
                                        new List<string> { Fairy.TypeName, Grass.TypeName }, //strong against
                                        new List<string> { Poison.TypeName, Ground.TypeName, Rock.TypeName, Ghost.TypeName }, //resisted by
                                        new List<string> { Steel.TypeName })
        {
        }
    }
}
