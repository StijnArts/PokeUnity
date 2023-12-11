using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class PartyEntryController
    {
        private VisualElement _partySlot;

        public PartyEntryController(VisualElement partySlot)
        {
            _partySlot = partySlot;
        }

        public void SetPokemonData(PokemonIndividualData pokemonIndividualData)
        {
            var sprite = _partySlot;
            if (pokemonIndividualData != null)
            {
                var spriteSheet = new CameraFacingSpriteSheet(pokemonIndividualData.GetSpriteWidth());
                spriteSheet.LoadTexture("Sprites/Pokemon/" + pokemonIndividualData.PokemonId + "/" + pokemonIndividualData.PokemonId + pokemonIndividualData.GetSpriteSuffix());
                sprite.style.backgroundImage = Background.FromSprite(spriteSheet.GetFrame(0, Assets.Scripts.World.Direction.Directions.South));
            }
            else sprite.style.display = DisplayStyle.None;
            
        }
    }
}
