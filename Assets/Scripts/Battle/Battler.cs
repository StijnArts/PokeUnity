namespace Assets.Scripts.Battle
{
    public class Battler
    {
        public PokemonIndividualData PokemonIndividualData;
        public bool hasMoved = false;
        public Battler(PokemonIndividualData pokemonIndividualData)
        {
            PokemonIndividualData = pokemonIndividualData;
        }
    }
}
