using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Fairy : PokemonType
    {
        public static string TypeName = "fairy";
        public Fairy() : base(TypeName,  //type name
                                        new List<string> { Dark.TypeName, Dragon.TypeName, Fighting.TypeName }, //strong against
                                        new List<string> { Fire.TypeName, Poison.TypeName, Steel.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
