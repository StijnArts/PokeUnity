using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Ice : PokemonType
    {
        public static string TypeName = "ice";
        public Ice() : base(TypeName,  //type name
                                        new List<string> { Dragon.TypeName, Flying.TypeName, Grass.TypeName, Ground.TypeName }, //strong against
                                        new List<string> { Fire.TypeName, Ice.TypeName, Steel.TypeName, Water.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
