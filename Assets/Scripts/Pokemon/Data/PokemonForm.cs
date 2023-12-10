using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public abstract class PokemonForm : PokemonSpecies
    {
        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }

        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
            int nationalPokedexNumber, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, nationalPokedexNumber, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }

        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, primaryType, secondaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }

        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
            int nationalPokedexNumber, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, nationalPokedexNumber, primaryType, secondaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }
    }
}
