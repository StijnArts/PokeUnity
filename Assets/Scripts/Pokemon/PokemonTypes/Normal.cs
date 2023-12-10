using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.PokemonTypes
{
    public class Normal : PokemonType
    {
        public static string TypeName = "normal";
        public Normal() : base(TypeName, 
            new List<string> { }, 
            new List<string> { Rock.TypeName, Steel.TypeName }, 
            new List<string> { Ghost.TypeName })
        {
        }
    }
}
