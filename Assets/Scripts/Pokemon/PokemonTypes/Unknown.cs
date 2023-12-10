using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Unknown : PokemonType
    {
        public static string TypeName = "???";
        public Unknown() : base(TypeName,  //type name
                                        new List<string> { }, //strong against
                                        new List<string> { }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
