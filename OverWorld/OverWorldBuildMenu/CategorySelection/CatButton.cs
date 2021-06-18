// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection.CatButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection
{
  internal class CatButton : GameObject
  {
    private float TIMER;
    private bool MouseOver;
    public GameObject Exclaaaim;
    private int EXCounter;
    public Rectangle BaseRect;
    public bool ForceSelectFromController;
    public CATEGORYTYPE category;
    private bool POPUPDONE;
    private PopLerper poplerp;
    private bool Disabled;
    private Rectangle MouseOverRect;
    private RedLight redlight;
    private CustomerFrameMouseOverBox mouseovertext;

    public CatButton(float _Scale = 1f)
    {
      this.DrawRect = new Rectangle(963, 352, 19, 19);
      this.SetDrawOriginToCentre();
      this.scale = 3f;
      if (DebugFlags.IsPCVersion)
        this.scale = _Scale;
      this.Exclaaaim = new GameObject();
      this.Exclaaaim.DrawRect = new Rectangle(227, 75, 13, 13);
      this.Exclaaaim.bActive = false;
      this.Exclaaaim.scale = 3f;
      this.Exclaaaim.SetAllColours(ColourData.FernGreen);
      this.Exclaaaim.SetDrawOriginToCentre();
      this.Exclaaaim.DrawOrigin.X -= 17f;
    }

    public void DoPop(float Index)
    {
      this.poplerp = new PopLerper();
      this.poplerp.SetDelay(Index * 0.1f);
    }

    public void Disable() => this.Disabled = true;

    public bool UpdateCatButton(Player player, Vector2 Offset, float DeltaTime)
    {
      this.Exclaaaim.bActive = player.livestats.CheckCellBlockReminderTutorial(player);
      if (this.Exclaaaim.bActive && this.category == CATEGORYTYPE.Enclosure)
      {
        this.Exclaaaim.bActive = false;
        if (!this.POPUPDONE)
          this.POPUPDONE = true;
      }
      if (this.poplerp != null)
      {
        int num1 = (int) this.poplerp.OnUpdate(DeltaTime);
      }
      float num2 = 1f;
      if (this.Exclaaaim.bActive)
      {
        this.TIMER += DeltaTime;
        if ((double) this.TIMER > 0.699999988079071)
          this.Exclaaaim.bActive = false;
        if ((double) this.TIMER > 1.0)
        {
          this.TIMER = 0.0f;
          ++this.EXCounter;
          if (this.EXCounter > 1)
            this.EXCounter = 0;
          int exCounter = this.EXCounter;
        }
      }
      if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale * num2, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
        this.MouseOver = true;
      else if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale * num2, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation))
        this.MouseOver = true;
      if (!this.Disabled && (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale * num2, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]) || this.ForceSelectFromController))
      {
        this.ForceSelectFromController = false;
        return true;
      }
      this.ForceSelectFromController = false;
      if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.BuildShop && this.category == CATEGORYTYPE.Shops)
      {
        Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
        return true;
      }
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.BuildArchtect || this.category != CATEGORYTYPE.Facilities)
        return false;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
      return true;
    }

    public void SetCategory(CATEGORYTYPE _category)
    {
      this.category = _category;
      string textToWrite = "";
      if (this.category == CATEGORYTYPE.Enclosure)
      {
        this.BaseRect = new Rectangle(824, 260, 38, 38);
        textToWrite = "Encolsures";
      }
      else if (this.category == CATEGORYTYPE.Floors)
      {
        this.BaseRect = new Rectangle(782, 221, 38, 38);
        textToWrite = "Floors";
      }
      else if (this.category == CATEGORYTYPE.Shops)
      {
        this.BaseRect = new Rectangle(824, 338, 38, 38);
        textToWrite = "Shops";
      }
      else if (this.category == CATEGORYTYPE.Nature)
      {
        textToWrite = "Nature";
        this.BaseRect = new Rectangle(824, 299, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Attractions)
      {
        textToWrite = "Attractions";
        this.BaseRect = new Rectangle(824, 377, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Signboards)
      {
        textToWrite = "Signs";
        this.BaseRect = new Rectangle(707, 377, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Amenities)
      {
        textToWrite = "Amenities";
        this.BaseRect = new Rectangle(707, 338, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Decorative)
      {
        textToWrite = "Decorations";
        this.BaseRect = new Rectangle(707, 299, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Facilities)
      {
        textToWrite = "Facilities";
        this.BaseRect = new Rectangle(707, 260, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Walls)
      {
        textToWrite = "Walls";
        this.BaseRect = new Rectangle(782, 182, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Light)
      {
        textToWrite = "Lights";
        this.BaseRect = new Rectangle(782, 143, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Benches)
      {
        textToWrite = "Benches";
        this.BaseRect = new Rectangle(816, 434, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Farm)
      {
        textToWrite = "Farms";
        this.BaseRect = new Rectangle(590, 136, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Factories)
      {
        textToWrite = "Factories";
        this.BaseRect = new Rectangle(590, 175, 38, 38);
      }
      else if (this.category == CATEGORYTYPE.Water)
      {
        textToWrite = "Water";
        this.BaseRect = new Rectangle(590, 303, 38, 38);
      }
      else
        this.mouseovertext = (CustomerFrameMouseOverBox) null;
      if (textToWrite != "")
        this.mouseovertext = new CustomerFrameMouseOverBox(Z_GameFlags.GetBaseScaleForUI(), textToWrite, 100f);
      if (this.mouseovertext != null)
        this.mouseovertext.location.Y -= (float) ((double) this.scale * (double) this.DrawRect.Height * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5);
      if (this.category == CATEGORYTYPE.Shops || this.category == CATEGORYTYPE.Facilities || (this.category == CATEGORYTYPE.Benches || this.category == CATEGORYTYPE.Amenities) || this.category == CATEGORYTYPE.Decorative)
      {
        this.redlight = new RedLight(true, BaseScale: this.scale);
        this.redlight.vLocation = new Vector2(13f * this.scale, -13f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      }
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.MouseOverRect = this.GetMouseOverRect();
    }

    private Rectangle GetMouseOverRect()
    {
      Rectangle rectangle = new Rectangle();
      if (this.category == CATEGORYTYPE.Enclosure)
        rectangle = new Rectangle(746, 260, 38, 38);
      else if (this.category == CATEGORYTYPE.Floors)
        rectangle = new Rectangle(704, 221, 38, 38);
      else if (this.category == CATEGORYTYPE.Shops)
        rectangle = new Rectangle(746, 338, 38, 38);
      else if (this.category == CATEGORYTYPE.Nature)
        rectangle = new Rectangle(746, 299, 38, 38);
      else if (this.category == CATEGORYTYPE.Attractions)
        rectangle = new Rectangle(746, 377, 38, 38);
      else if (this.category == CATEGORYTYPE.Signboards)
        rectangle = new Rectangle(629, 377, 38, 38);
      else if (this.category == CATEGORYTYPE.Amenities)
        rectangle = new Rectangle(629, 338, 38, 38);
      else if (this.category == CATEGORYTYPE.Decorative)
        rectangle = new Rectangle(629, 299, 38, 38);
      else if (this.category == CATEGORYTYPE.Facilities)
        rectangle = new Rectangle(629, 260, 38, 38);
      else if (this.category == CATEGORYTYPE.Walls)
        rectangle = new Rectangle(704, 182, 38, 38);
      else if (this.category == CATEGORYTYPE.Light)
        rectangle = new Rectangle(704, 143, 38, 38);
      else if (this.category == CATEGORYTYPE.Benches)
        rectangle = new Rectangle(738, 434, 38, 38);
      else if (this.category == CATEGORYTYPE.Farm)
        rectangle = new Rectangle(512, 136, 38, 38);
      else if (this.category == CATEGORYTYPE.Factories)
        rectangle = new Rectangle(512, 175, 38, 38);
      else if (this.category == CATEGORYTYPE.Nature)
        rectangle = new Rectangle(746, 299, 38, 38);
      else if (this.category == CATEGORYTYPE.Water)
        rectangle = new Rectangle(590, 342, 38, 38);
      return rectangle;
    }

    public bool CheckMouseOver(Player player, Vector2 Offset, float SCALEMULT = 1f) => MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale * SCALEMULT, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);

    public void DrawCatButton(Vector2 Offset, bool IsSelected)
    {
      float num1 = 1f;
      float num2 = 1f;
      if (this.poplerp != null)
        num2 = this.poplerp.CurrentValue;
      int num3 = IsSelected ? 1 : 0;
      this.DrawRect = this.BaseRect;
      if (IsSelected)
      {
        this.MouseOver = false;
        this.DrawRect = this.MouseOverRect;
      }
      if (this.MouseOver || this.Disabled)
      {
        this.DrawRect.X -= this.DrawRect.Width + 1;
        if (this.Disabled)
          num1 = 0.5f;
      }
      this.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.scale * num2, this.fAlpha * num1);
      if (this.redlight != null)
      {
        if (FeatureFlags.FlashBuildShop && this.category == CATEGORYTYPE.Shops && !IsSelected)
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
        else if ((FeatureFlags.FlashBuildFacility || FeatureFlags.FlashResearchFromTask || (FeatureFlags.FlashStoreRoomFromTask || FeatureFlags.FlashCRISPRFromTask)) && (this.category == CATEGORYTYPE.Facilities && !IsSelected))
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
        else if (FeatureFlags.FlashBuildPen && this.category == CATEGORYTYPE.Enclosure && !IsSelected)
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
        else if (FeatureFlags.FlashBuildBench && this.category == CATEGORYTYPE.Benches && !IsSelected)
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
        else if (FeatureFlags.FlashBuildDecoration && this.category == CATEGORYTYPE.Decorative && !IsSelected)
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
        if (FeatureFlags.FlashBuildBin && this.category == CATEGORYTYPE.Amenities && !IsSelected)
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
        if (FeatureFlags.FlashBuildToilet && this.category == CATEGORYTYPE.Amenities && !IsSelected)
          this.redlight.DrawRedLight(AssetContainer.pointspritebatch03, Offset + this.vLocation);
      }
      if (this.Exclaaaim.bActive && this.category == CATEGORYTYPE.Enclosure)
        this.Exclaaaim.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + this.vLocation, this.scale * num2, this.fAlpha);
      if (!this.MouseOver)
        return;
      this.MouseOver = false;
      if (this.mouseovertext == null)
        return;
      this.mouseovertext.Active = true;
      this.mouseovertext.DrawCustomerFrameMouseOverBoc(Offset + this.vLocation, AssetContainer.pointspritebatch03);
    }
  }
}
