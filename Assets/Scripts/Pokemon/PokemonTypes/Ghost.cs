using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Ghost : PokemonType
    {
        public static string TypeName = "ghost";
        public Ghost() : base(TypeName,  //type name
                                        new List<string> { Ghost.TypeName, Psychic.TypeName }, //strong against
                                        new List<string> { Dark.TypeName }, //resisted by
                                        new List<string> { Normal.TypeName })
        {
        }
    }
}
