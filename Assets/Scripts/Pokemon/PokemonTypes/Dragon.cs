using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Dragon : PokemonType
    {
        public static string TypeName = "dragon";
        public Dragon() : base(TypeName,  //type name
                                        new List<string> { Dragon.TypeName, Psychic.TypeName }, //strong against
                                        new List<string> { Steel.TypeName }, //resisted by
                                        new List<string> { Fairy.TypeName })
        {
        }
    }
}
