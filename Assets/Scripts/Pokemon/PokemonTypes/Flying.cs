using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Flying : PokemonType
    {
        public static string TypeName = "flying";
        public Flying() : base(TypeName,  //type name
                                        new List<string> { Bug.TypeName, Fighting.TypeName, Grass.TypeName }, //strong against
                                        new List<string> { Electric.TypeName, Rock.TypeName, Steel.TypeName })
        {
        }
    }
}
