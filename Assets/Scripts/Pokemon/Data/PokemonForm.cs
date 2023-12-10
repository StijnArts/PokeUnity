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
            List<string> abilities, 
            string hiddenAbility, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, primaryType, abilities, hiddenAbility, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }

        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
            int nationalPokedexNumber, 
            string primaryType, 
            List<string> abilities, 
            string hiddenAbility, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, nationalPokedexNumber, primaryType, abilities, hiddenAbility, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }

        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
            string primaryType, 
            string secondaryType, 
            List<string> abilities, 
            string hiddenAbility, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, primaryType, secondaryType, abilities, hiddenAbility, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }

        public PokemonForm(
            string pokemonName, 
            string pokemonId, 
            int nationalPokedexNumber, 
            string primaryType, 
            string secondaryType, 
            List<string> abilities, 
            string hiddenAbility, 
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
            bool cannotDynamax = false) : base(pokemonName, pokemonId, nationalPokedexNumber, primaryType, secondaryType, abilities, hiddenAbility, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, hasGenderDifferences, cannotDynamax)
        {
        }
    }
}
