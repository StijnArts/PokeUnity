using Assets.Scripts.World;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CameraFacingSpriteSheet
{
    [HideInInspector]
    public Dictionary<Direction.Directions, Texture2D> Textures = new Dictionary<Direction.Directions, Texture2D>();
    [HideInInspector]
    public Dictionary<Direction.Directions, int> FrameCounts = new Dictionary<Direction.Directions, int>();
    public int SpriteWidth = 0;
    public bool FlipSpriteForWest = false;
    public bool FlipSpriteForEast = false;
    public CameraFacingSpriteSheet(ref int width)
    {
        SpriteWidth = width;
    }

    public void LoadTexture(string textureLocation)
    {
        Textures.Clear();
        FrameCounts.Clear();
        foreach(Direction.Directions direction in Enum.GetValues(typeof(Direction.Directions))){
            string directionName = Enum.GetName(typeof(Direction.Directions), direction).ToLower();
            var texture = Resources.Load<Texture2D>(textureLocation + "_" + directionName);
            if (directionName.Contains("west") && texture == null)
            {
                var newDirection = directionName.Replace("west", "east");
                texture = Resources.Load<Texture2D>(textureLocation + "_" + newDirection);
                FlipSpriteForWest = true;
            } else if (directionName.Contains("east") && texture == null)
            {
                var newDirection = directionName.Replace("east", "west");
                texture = Resources.Load<Texture2D>(textureLocation + "_" + newDirection);
                FlipSpriteForEast = true;
            }
            if (texture == null)
            {
                texture = Resources.Load<Texture2D>("Sprites/missingsprite");
                Debug.Log("Couldnt find Texture2D at location: " + textureLocation + "_" + directionName);
            }

            texture.filterMode = FilterMode.Point;
            var frameCount = texture.width / SpriteWidth;
            texture.wrapMode = TextureWrapMode.Clamp;
            Textures.Add(direction, texture);
            FrameCounts.Add(direction, frameCount);
        }
    }

    public Sprite GetFrame(int frameNumber, int pixelsPerUnit, Direction.Directions facing)
    {
        var sprite = Sprite.Create(Textures[facing],
            new Rect(
                new Vector2(frameNumber * SpriteWidth, 0),
                new Vector2(SpriteWidth, Textures[facing].height)),
            new Vector2(0.5f, 0),
            pixelsPerUnit, 0,
            SpriteMeshType.Tight,
            Vector4.zero);
        return sprite;
    }

    public Sprite GetFrame(int frameNumber, Direction.Directions facing)
    {

        return GetFrame(frameNumber, 16, facing);
    }
}
