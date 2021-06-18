// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.R_Icon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;
using TinyZoo.Z_Morality;
using TinyZoo.Z_Research_.RData;

namespace TinyZoo.Z_Research_.IconGrid
{
  internal class R_Icon : GameObject
  {
    public int ArrayX;
    public int ArrayY;
    private GameObjectNineSlice mouseSelectNineSlice;
    private GameObject overlay;
    private GameObject PreviewOverlay;
    public bool MouseOver;
    public REntry rentry;
    public bool IsHeldOn;
    public UnlockState unlockstate;
    public int MyRing;
    private PopLerper spawnlerper;
    internal static float IconSpacing = 70f;
    private RStar star;
    public bool BlockMouseDown;

    public R_Icon(
      ref int SprialIndex,
      ref int Ring,
      ref int NextRingValue,
      ref int SideSize,
      REntry _rentry,
      ref int MaxForArray)
    {
      this.unlockstate = UnlockState.Locked;
      this.MyRing = Ring;
      this.rentry = _rentry;
      this.mouseSelectNineSlice = new GameObjectNineSlice(new Rectangle(961, 350, 21, 21), 7);
      int num1 = SprialIndex / (SideSize + 1);
      this.ArrayX = SprialIndex - num1 * (SideSize + 1);
      switch (num1)
      {
        case 1:
        case 3:
          this.ArrayY = this.ArrayX;
          this.ArrayX = 0;
          if (num1 == 3)
          {
            this.ArrayY = SideSize + 1 - this.ArrayY;
            break;
          }
          this.ArrayX = SideSize + 1;
          break;
        case 2:
          this.ArrayY = SideSize + 1;
          this.ArrayX = SideSize + 1 - this.ArrayX;
          break;
      }
      if (this.ArrayX > MaxForArray)
        MaxForArray = this.ArrayX;
      int num2 = -(SideSize / 2);
      this.ArrayX += num2;
      this.ArrayY += num2;
      this.vLocation = new Vector2((float) this.ArrayX * R_Icon.IconSpacing, (float) this.ArrayY * R_Icon.IconSpacing * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.vLocation = this.vLocation - new Vector2(R_Icon.IconSpacing * 0.5f, R_Icon.IconSpacing * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.scale = 2f;
      this.DrawRect = _rentry.IconRect;
      this.SetDrawOriginToCentre();
      ++SprialIndex;
      if (SprialIndex >= SideSize * 4 + 4)
      {
        ++NextRingValue;
        SideSize += 2;
        ++Ring;
        SprialIndex = 0;
      }
      this.overlay = new GameObject();
      this.overlay.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.overlay.SetDrawOriginToCentre();
      this.overlay.scale = this.scale;
      this.MouseOver = false;
      this.overlay.SetAlpha(0.3f);
      this.overlay.vLocation = this.vLocation;
      this.PreviewOverlay = new GameObject(this.overlay);
      this.PreviewOverlay.SetAllColours(0.0f, 0.0f, 0.0f);
      this.PreviewOverlay.SetAlpha(0.5f);
    }

    public void UpdateR_Icon(Player player, float DeltaTime, Vector2 PointerLocationInWorldSpace)
    {
      if (this.spawnlerper != null)
      {
        int num = (int) this.spawnlerper.OnUpdate(DeltaTime);
      }
      this.IsHeldOn = false;
      if (this.unlockstate == UnlockState.Locked || !MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, PointerLocationInWorldSpace))
        return;
      this.MouseOver = true;
      if (this.BlockMouseDown)
      {
        if (player.inputmap.LeftMouseHeld)
          return;
        this.BlockMouseDown = false;
      }
      else
      {
        if (!player.inputmap.LeftMouseHeld)
          return;
        this.IsHeldOn = true;
      }
    }

    public void Unlock(ref int NewOnesFound, bool isForPreview = false, bool LerpInNew = false)
    {
      if (isForPreview)
      {
        if (LerpInNew)
        {
          this.spawnlerper = new PopLerper();
          this.spawnlerper.SetDelay((float) ((double) (NewOnesFound + 1) * 0.200000002980232 + 0.300000011920929));
          ++NewOnesFound;
        }
        this.unlockstate = UnlockState.Preview;
      }
      else
      {
        this.SetAlpha(1f);
        this.unlockstate = UnlockState.Unlocked;
      }
    }

    public void CheckAndAddStar()
    {
      if (!RGrid_Data.IsThisAStarBuildingUnlock(this.rentry.unlocktype))
        return;
      StarColour _starColour = StarColour.Neutral;
      for (int index = 0; index < this.rentry.WillUnlockThese.Count; ++index)
      {
        if (MoralityUnlocksData.IsAMoralityBuilding(this.rentry.WillUnlockThese[index]))
          _starColour = MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(this.rentry.WillUnlockThese[index]) < 0 ? StarColour.Evil_Purple : StarColour.Good_Yellow;
      }
      this.star = new RStar(_starColour, this.scale, this.unlockstate == UnlockState.Locked);
      this.star.vLocation = this.vLocation;
      if (this.unlockstate == UnlockState.Locked)
        return;
      RStar star1 = this.star;
      star1.vLocation = star1.vLocation - new Vector2((float) this.DrawRect.Width * this.scale, (float) this.DrawRect.Height * this.scale) * 0.5f * Sengine.ScreenRatioUpwardsMultiplier;
      RStar star2 = this.star;
      star2.vLocation = star2.vLocation + new Vector2(1f, 1f) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public Vector2 GetMouseOverVscale() => new Vector2(75f, 75f * Sengine.ScreenRatioUpwardsMultiplier.Y);

    public void DrawR_Icon()
    {
      if (this.unlockstate != UnlockState.Locked)
      {
        if (this.spawnlerper != null)
          this.scale = 2f * this.spawnlerper.CurrentValue;
        if (this.MouseOver)
        {
          this.mouseSelectNineSlice.vLocation = Vector2.Zero;
          this.mouseSelectNineSlice.scale = Sengine.WorldOriginandScale.Z;
          this.mouseSelectNineSlice.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, RenderMath.TranslateWorldSpaceToScreenSpace(this.vLocation) + CameraShake.CameraShakeOffset * Sengine.WorldOriginandScale.Z, this.GetMouseOverVscale() * Sengine.WorldOriginandScale.Z);
        }
        this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
        if (this.unlockstate == UnlockState.Preview)
        {
          this.PreviewOverlay.scale = 60f;
          this.PreviewOverlay.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
        }
        if (this.MouseOver)
        {
          this.overlay.scale = 60f;
          this.overlay.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
          this.MouseOver = false;
        }
      }
      if (this.star == null)
        return;
      this.star.DrawRStar_WorldOffsetDraw(AssetContainer.pointspritebatch03);
    }

    public void UNLOCK_DrawR_Icon(float Percentage, float ScrollerUnlockValue)
    {
      this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      for (int index = 0; index < 10; ++index)
      {
        this.overlay.SetAlpha(0.1f);
        this.overlay.scale = 60f * ScrollerUnlockValue * (float) index;
        this.overlay.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
        this.MouseOver = false;
      }
      this.overlay.SetAlpha(0.3f);
    }
  }
}
