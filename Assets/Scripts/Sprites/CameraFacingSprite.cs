using Assets.Scripts.World;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class CameraFacingSprite : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer SpriteRenderer;
    [HideInInspector]
    public CameraFacingSpriteSheet SpriteSheet;
    public enum SpriteType {Pokemon, Npc, Player, Item}
    public string SpriteSuffix;
    public string SpriteGroup;
    public int MillisecondPerFrame = 50;
    public float yOffset = 0;
    public int SpriteWidth = 16;
    public int PixelsPerUnit = 16;
    [HideInInspector]
    public int CurrentFrame = 0;
    protected float LastFrameUpdateTimeStamp = 0;
    public Direction.Directions Facing = Direction.Directions.South;
    private Direction.Directions _lastFacing = (Direction.Directions)1000;
    public SpriteType TypeOfSprite;
    [HideInInspector]
    public string TextureLocation => GetTextureLocation();

    private string GetTextureLocation()
    {
        var textureLocation = "Sprites/" + Enum.GetName(typeof(SpriteType), TypeOfSprite) + "/" + SpriteGroup + "/" + SpriteGroup;
        if (!string.IsNullOrWhiteSpace(SpriteSuffix))
        {
            textureLocation += SpriteSuffix;
        }
        return textureLocation;
    }

    void Awake()
    {
        InitializeSprite();
    }

    protected void InitializeSprite()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 0.00001f);
        SpriteSheet = new CameraFacingSpriteSheet(ref SpriteWidth);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DetermineTextureLocation();
        if (SpriteRenderer == null)
        {
            SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        SpriteRenderer.sprite = SpriteSheet.GetFrame(0, PixelsPerUnit, Facing);
        SpriteRenderer.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        SpriteRenderer.transform.localPosition = new Vector3(0, 0 + yOffset, 0);
        LastFrameUpdateTimeStamp = 0;
        SpriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        SpriteRenderer.receiveShadows = true;

        var material = Resources.Load<Material>("Materials/ShadowCastingSpriteMaterial");
        SpriteRenderer.material = material;
        this.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LastFrameUpdateTimeStamp += Time.deltaTime * 1000;
        
        if(SpriteSheet.FrameCounts[Facing] > 1)
        {
            Animate();
            if (LastFrameUpdateTimeStamp >= MillisecondPerFrame)
            {
                CurrentFrame++;
                LastFrameUpdateTimeStamp = 0;
            }
        } else
        {
            UpdateDirection();
        }
    }

    private void UpdateDirection()
    {
        if((int)_lastFacing == 1000 || _lastFacing != Facing)
        {
            _lastFacing = Facing;
            Animate();
        }
    }

    protected void DetermineTextureLocation()
    {
        SetNewSpriteLocation(SpriteGroup);
    }

    public void SetNewSpriteLocation(string spriteGroup, string spriteSuffix = "")
    {
        SpriteGroup = spriteGroup;
        SpriteSuffix = spriteSuffix;
        CurrentFrame = 0;
        if (SpriteSheet == null)
        {
            InitializeSprite();
        }
        SpriteSheet.LoadTexture(TextureLocation);
    }

    internal void ApplyRotation(float angle)
    {
        Facing = Direction.ApproximateDirection(angle);
        //Debug.Log(Facing);
    }

    private void Animate()
    {
        if (SpriteSheet.FrameCounts[Facing]-1 < CurrentFrame)
        {
            CurrentFrame = 0;
        }

        SpriteRenderer.sprite = SpriteSheet.GetFrame(CurrentFrame, PixelsPerUnit, Facing);
        if(SpriteSheet.FlipSpriteForWest && Enum.GetName(typeof(Direction.Directions), Facing).ToLower().Contains("west"))
        {
            SpriteRenderer.flipX = true;
        } else if(SpriteSheet.FlipSpriteForEast && Enum.GetName(typeof(Direction.Directions), Facing).ToLower().Contains("east"))
        {
            SpriteRenderer.flipX = true;
        } else
        {
            SpriteRenderer.flipX = false;
        }
    }
}
