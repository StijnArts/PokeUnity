using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Electric    : PokemonType
    {
        public static string TypeName = "electric";
        public Electric() : base(TypeName,  //type name
                                        new List<string> { Flying.TypeName, Water.TypeName }, //strong against
                                        new List<string> { Dragon.TypeName, Electric.TypeName, Grass.TypeName }, //resisted by
                                        new List<string> { Ground.TypeName })
        {
        }
    }
}
