﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public class BaseSpecies
    {
        public string PokemonName;
        public string PokemonId;
        public string PrimaryType;
        public string SecondaryType;
        public List<Ability> Abilities;
        public Ability HiddenAbility;
        public BaseStats BaseStats;
        public int CatchRate;
        public double MaleRatio;
        public int BaseExperienceYield;
        public string ExperienceGroup;
        public int EggCycles;
        public List<string> EggGroups;
        public List<MoveSet> MoveSets;
        public string PreEvolution;
        public List<Evolution> Evolutions;
        public int BaseFriendship;
        public EvYield EvYield;
        public double HeightInCm;
        public double WeightInKilos;
        public bool CannotDynamax;
        public bool HasGenderDifferences;

        public BaseSpecies(
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
        bool cannotDynamax = false)
        {
            PokemonName = pokemonName;
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
        }

        public BaseSpecies(
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
            bool cannotDynamax = false) :
                this(pokemonName, pokemonId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, cannotDynamax, hasGenderDifferences)
        {
            SecondaryType = secondaryType;
        }
    }
}
