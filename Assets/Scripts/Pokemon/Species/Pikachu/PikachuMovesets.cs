using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Species.Pikachu
{
    public class GenerationOneMoveset : MoveSet
    {
        public GenerationOneMoveset() :
            base(
                Pikachu.Identifier,
                new Dictionary<int, string>()
                {
                    {1,  "tackle"}
                })
        {
        }
    }
}
