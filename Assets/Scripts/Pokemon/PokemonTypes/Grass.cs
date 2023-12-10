using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Grass : PokemonType
    {
        public static string TypeName = "grass";
        public Grass() : base(TypeName,  //type name
                                        new List<string> { Ground.TypeName, Rock.TypeName, Water.TypeName }, //strong against
                                        new List<string> { Bug.TypeName, Dragon.TypeName, Fire.TypeName, Flying.TypeName, Grass.TypeName, Poison.TypeName, Steel.TypeName }) //resisted by
        {
        }
    }
}
