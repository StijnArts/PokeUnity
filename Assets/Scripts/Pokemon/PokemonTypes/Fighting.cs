using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Fighting : PokemonType
    {
        public static string TypeName = "fighting";
        public Fighting() : base(TypeName,  //type name
                                        new List<string> { Dark.TypeName, Ice.TypeName, Normal.TypeName, Rock.TypeName, Steel.TypeName }, //strong against
                                        new List<string> { Bug.TypeName, Fairy.TypeName, Flying.TypeName, Poison.TypeName, Psychic.TypeName }, //resisted by
                                        new List<string> { Ghost.TypeName })
        {
        }
    }
}
