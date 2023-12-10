using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Psychic : PokemonType
    {
        public static string TypeName = "psychic";
        public Psychic() : base(TypeName,  //type name
                                        new List<string> { Fighting.TypeName, Poison.TypeName }, //strong against
                                        new List<string> { Psychic.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
