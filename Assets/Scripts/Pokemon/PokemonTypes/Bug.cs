using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Bug : PokemonType
    {
        public static string TypeName = "bug";
        public Bug() : base(TypeName,  //type name
                                        new List<string> { Dark.TypeName, Grass.TypeName, Psychic.TypeName }, //strong against
                                        new List<string> { Fairy.TypeName, Fighting.TypeName, Fire.TypeName, Flying.TypeName, Ghost.TypeName, Poison.TypeName, Steel.TypeName })
        {
        }
    }
}
