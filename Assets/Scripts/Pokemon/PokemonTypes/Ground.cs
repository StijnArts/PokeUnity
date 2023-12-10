using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Ground : PokemonType
    {
        public static string TypeName = "ground";
        public Ground() : base(TypeName,  //type name
                                        new List<string> { Electric.TypeName, Fire.TypeName, Poison.TypeName, Rock.TypeName, Steel.TypeName }, //strong against
                                        new List<string> { Bug.TypeName, Grass.TypeName }, //resisted by
                                        new List<string> { Flying.TypeName })
        {
        }
    }
}
