using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public class PokemonIdentifier
    {
        public string SpeciesId;
        public string FormId;

        public PokemonIdentifier(string speciesId, string formId)
        {
            SpeciesId = speciesId;
            FormId = formId;
        }

        public PokemonIdentifier(string speciesId)
        {
            SpeciesId = speciesId;
        }
    }
}
