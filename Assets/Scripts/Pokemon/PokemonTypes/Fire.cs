using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Fire : PokemonType
    {
        public static string TypeName = "fire";
        public Fire() : base(TypeName,  //type name
                                        new List<string> { Bug.TypeName, Grass.TypeName, Ice.TypeName, Steel.TypeName }, //strong against
                                        new List<string> { Dragon.TypeName, Fire.TypeName, Rock.TypeName, Water.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
