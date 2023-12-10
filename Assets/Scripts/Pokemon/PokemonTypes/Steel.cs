using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Steel : PokemonType
    {
        public static string TypeName = "steel";
        public Steel() : base(TypeName,  //type name
                                        new List<string> { Fairy.TypeName, Ice.TypeName, Rock.TypeName }, //strong against
                                        new List<string> { Electric.TypeName, Fire.TypeName, Steel.TypeName, Water.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
