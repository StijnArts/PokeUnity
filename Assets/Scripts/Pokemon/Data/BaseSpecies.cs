using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public class BaseSpecies
    {
        public string PokemonId;
        public string PrimaryType;
        public string SecondaryType;
        public List<Ability> Abilities = new List<Ability>();
        public Ability HiddenAbility;
        public BaseStats BaseStats;
        public int CatchRate;
        public double MaleRatio;
        public int BaseExperienceYield;
        public string ExperienceGroup;
        public int EggCycles;
        public List<string> EggGroups;
        public List<MoveSet> MoveSets;
        public PokemonIdentifier PreEvolution;
        public List<Evolution> Evolutions = new List<Evolution>();
        public int BaseFriendship;
        public EvYield EvYield;
        public double HeightInCm;
        public double WeightInKilos;
        public bool CannotDynamax;
        public bool HasGenderDifferences;
        public int SpriteWidth;
        public int SpriteResolution;
        public int SpriteAnimationSpeed;

        public BaseSpecies(
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
        int spriteWidth,
        int spriteResolution,
        int spriteAnimationSpeed,
        bool hasGenderDifferences = false,
        bool cannotDynamax = false)
        {
            PokemonId = pokemonId;
            PrimaryType = primaryType;
            BaseStats = baseStats;
            CatchRate = catchRate;
            MaleRatio = maleRatio;
            BaseExperienceYield = baseExperienceYield;
            ExperienceGroup = experienceGroup;
            EggCycles = eggCycles;
            EggGroups = eggGroups;
            BaseFriendship = baseFriendship;
            EvYield = evYield;
            HeightInCm = heightInCm;
            WeightInKilos = weightInGrams;
            CannotDynamax = cannotDynamax;
            HasGenderDifferences = hasGenderDifferences;
            SpriteWidth = spriteWidth;
            SpriteResolution = spriteResolution;
            SpriteAnimationSpeed = spriteAnimationSpeed;
        }

        public BaseSpecies(
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
            int spriteWidth,
            int spriteResolution,
            int spriteAnimationSpeed,
            bool hasGenderDifferences = false,
            bool cannotDynamax = false) :
                this(pokemonId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, spriteWidth, spriteResolution, spriteAnimationSpeed, cannotDynamax, hasGenderDifferences)
        {
            SecondaryType = secondaryType;
        }
    }
}
