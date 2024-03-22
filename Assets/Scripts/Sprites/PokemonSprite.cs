using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sprites
{
    public class PokemonSprite : CameraFacingSprite
    {
        void Start()
        {
        }

        public void InitializeSprite(string spriteGroup, string spriteSuffix, int spriteWidth, int spriteResolution, int millisecondsPerFrame)
        {
            SpriteWidth = spriteWidth;
            PixelsPerUnit = spriteResolution;
            MillisecondPerFrame = millisecondsPerFrame;
            SpriteGroup = spriteGroup;
            SpriteSuffix = spriteSuffix;
            base.InitializeSprite();
        }
    }
}
