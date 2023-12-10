using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Rock : PokemonType
    {
        public static string TypeName = "rock";
        public Rock() : base(TypeName,  //type name
                                        new List<string> { Bug.TypeName, Fire.TypeName, Flying.TypeName, Ice.TypeName }, //strong against
                                        new List<string> { Fighting.TypeName, Ground.TypeName, Steel.TypeName }, //resisted by
                                        new List<string> { })
        {
        }
    }
}
