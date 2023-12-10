using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public abstract class PokemonForm : BaseSpecies
    {
        public string FormName;
        public string FormId;

        public PokemonForm(
            string formName,
            string formId, 
            PokemonIdentifier originalPokemonId, 
            string primaryType,
            BaseStats baseStats, 
            int catchRate, 
            double maleRatio, 
            int baseExperienceYield, 
            string experienceGroup, 
            int eggCycles, 
            List<string> eggGroups, 
            int baseFriendship, 
            EvYield evYield, 
            double heightInCm, 
            double weightInGrams, 
            bool hasGenderDifferences = false, 
            bool cannotDynamax = false) : base(originalPokemonId.SpeciesId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
            FormName = formName;
            FormId = formId;
        }

        public PokemonForm(
            string formName,
            string formId,
            PokemonIdentifier originalPokemonId,
            string primaryType, 
            string secondaryType, 
            BaseStats baseStats, 
            int catchRate, 
            double maleRatio, 
            int baseExperienceYield, 
            string experienceGroup, 
            int eggCycles, 
            List<string> eggGroups, 
            int baseFriendship, 
            EvYield evYield, 
            double heightInCm, 
            double weightInGrams, 
            bool hasGenderDifferences = false, 
            bool cannotDynamax = false) : base(originalPokemonId.SpeciesId, primaryType, secondaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
            FormName = formName;
            FormId = formId;
        }
    }
}
