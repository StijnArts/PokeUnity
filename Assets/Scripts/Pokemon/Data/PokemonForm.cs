﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            bool cannotDynamax = false) : base(originalPokemonId.SpeciesId, types, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, spriteWidth, spriteResolution, spriteAnimationSpeed, cannotDynamax, hasGenderDifferences)
        {
            FormName = formName;
            FormId = formId;
        }
    }
}
