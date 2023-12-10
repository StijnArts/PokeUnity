using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Dark : PokemonType
    {
        public static string TypeName = "dark";
        public Dark() : base(TypeName,  //type name
                                        new List<string> { Ghost.TypeName, Psychic.TypeName }, //strong against
                                        new List<string> { Dark.TypeName, Fairy.TypeName, Fighting.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
