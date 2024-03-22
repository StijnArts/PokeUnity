using Assets.Scripts.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public class BaseSpecies
    {
        public string Id;
        public string[] Types = new string[Settings.MaxTypes];
        public List<string> Abilities = new List<string>();
        public string HiddenAbility;
        public BaseStats BaseStats;
        public int CatchRate;
        public double MaleRatio;
        public int BaseExperienceYield;
        public string ExperienceGroup;
        public int EggCycles;
        public List<string> EggGroups;
        public List<MoveSet> MoveSets = new List<MoveSet>();
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
        string[] types,
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
            Id = pokemonId;
            Types = types;
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
    }
}
