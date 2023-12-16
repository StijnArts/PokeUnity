using Assets.Scripts.Pokemon.Data;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Registries
{
    public class MoveRegistry
    {
        //TODO create abilities
        public static Dictionary<string, Move> MovesDictionary = new Dictionary<string, Move>();

        public static void RegisterAbility(string id, Move ability)
        {
            MovesDictionary.Add(id, ability);
        }

        public static List<string> GetMoveIds() => MovesDictionary.Values.Select(move => move.Id).ToList();

        public static Move GetMove(string id)
        {
            Move value;
            MovesDictionary.TryGetValue(id, out value);
            if (value == null)
            {
                return MovesDictionary["none"];
            }
            return value;
        }

        public static void RegisterMoves()
        {
            var moves = SubTypeReflector<Move>.FindSubTypeClasses();
            foreach (Move move in moves)
            {
                MovesDictionary.Add(move.Id, move);
            }
        }

        public static void RegisterMoveSets()
        {
            var moveSets = SubTypeReflector<MoveSet>.FindSubTypeClasses();
            Dictionary<PokemonIdentifier, List<MoveSet>> moveSetsPerIdentifier = new Dictionary<PokemonIdentifier, List<MoveSet>>();
            foreach(var moveset in moveSets)
            {
                AddByIdentifier(moveset, moveSetsPerIdentifier);
            }
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

        private static bool AddByIdentifier(MoveSet moveset, Dictionary<PokemonIdentifier, List<MoveSet>> moveSetsPerIdentifier)
        {
            if (!moveSetsPerIdentifier.ContainsKey(moveset.PokemonIdentifier))
            {
                moveSetsPerIdentifier.Add(moveset.PokemonIdentifier, new List<MoveSet>() { moveset });
            }
            else
            {
                moveSetsPerIdentifier[moveset.PokemonIdentifier].Add(moveset);
            }
            return true;
        }
    }
}
