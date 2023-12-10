using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Registries
{
    public class MoveRegistry

    {
        //TODO create abilities
        public static Dictionary<string, Move> Moves = RegisterMoves();

        public static void RegisterAbility(string id, Move ability)
        {
            Moves.Add(id, ability);
        }

        public static List<string> GetAbilityIds() => Moves.Values.Select(move => move.MoveId).ToList();

        public static Move GetAbility(string id)
        {
            Move value;
            Moves.TryGetValue(id, out value);
            if (value == null)
            {
                return Moves["none"];
            }
            return value;
        }

        public static Dictionary<string, Move> RegisterMoves()
        {
            var registry = new Dictionary<string, Move>();
            var moves = SubTypeReflector<Move>.FindSubTypeClasses();
            foreach (Move move in moves)
            {
                registry.Add(move.MoveId, move);
            }
            return registry;
        }
        
        public static void RegisterMoveSets()
        {
            var moveSets = SubTypeReflector<MoveSet>.FindSubTypeClasses();
            Dictionary<PokemonIdentifier, List<MoveSet>> moveSetsPerIdentifier = new Dictionary<PokemonIdentifier, List<MoveSet>>();
                moveSets.Select(moveset => {
                    if(moveSetsPerIdentifier[moveset.PokemonIdentifier] == null)
                    {
                        moveSetsPerIdentifier.Add(moveset.PokemonIdentifier, new List<MoveSet>() { moveset });
                    } else
                    {
                        moveSetsPerIdentifier[moveset.PokemonIdentifier].Add(moveset);
                    }
                    return true;
                });
            foreach (PokemonIdentifier identifier in moveSetsPerIdentifier.Keys)
            {
                var targetPokemonSpecies = PokemonRegistry.GetPokemonSpecies(identifier.SpeciesId);
                if (!string.IsNullOrEmpty(identifier.FormId) && targetPokemonSpecies.Forms.ContainsKey(identifier.FormId))
                {
                    var form = targetPokemonSpecies.Forms[identifier.FormId];
                    foreach (var moveSet in moveSetsPerIdentifier[identifier])
                    {
                        form.MoveSets.Add(moveSet);
                    }
                }
                else
                {
                    foreach (var moveSet in moveSetsPerIdentifier[identifier])
                    {
                        targetPokemonSpecies.MoveSets.Add(moveSet);
                    }
                }
                
            }
        }
    }
}
