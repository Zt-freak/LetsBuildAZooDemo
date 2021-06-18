// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.OveWorldUI.OWCategoryButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld;
using TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers;

namespace TinyZoo.GenericUI.OveWorldUI
{
  internal class OWCategoryButton
  {
    private GenericBox box;
    public GameObject Icon;
    private LerpHandler_Float ActiveLerper;
    public Vector2 Location;
    public bool MouseOver;
    public OverworldButtons buttontype;
    private Rectangle MouseOverRect;
    internal static float SizeBTN = 80f;
    private RedLight redlight;
    private float LerpPosMlt = 1f;
    private Rectangle BaseRect;
    private float BaseScale;
    public Vector2 LastDrawLocation;

    public OWCategoryButton(OverworldButtons overworldBTN, float _BaseScale = -1f)
    {
      this.BaseScale = _BaseScale;
      if (DebugFlags.IsPCVersion)
        OWCategoryButton.SizeBTN = 50f * Sengine.ScreenRationReductionMultiplier.Y;
      this.buttontype = overworldBTN;
      this.ActiveLerper = new LerpHandler_Float();
      this.ActiveLerper.SetLerp(true, 1f, 1f, 3f);
      this.box = new GenericBox(new Vector2(OWCategoryButton.SizeBTN, OWCategoryButton.SizeBTN * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.Icon = new GameObject();
      this.box.SetDarkestBlue();
      switch (overworldBTN)
      {
        case OverworldButtons.Settings:
          if (DebugFlags.IsPCVersion)
          {
            this.Icon.DrawRect = new Rectangle(409, 53, 24, 26);
            this.MouseOverRect = new Rectangle(409, 80, 24, 26);
            break;
          }
          this.Icon.DrawRect = new Rectangle(921, 0, 30, 31);
          this.MouseOverRect = this.Icon.DrawRect;
          ++this.MouseOverRect.Y;
          break;
        case OverworldButtons.Intake:
          this.redlight = new RedLight(true);
          this.redlight.scale = 0.5f;
          this.redlight.bActive = false;
          this.Icon.DrawRect = new Rectangle(378, 36, 30, 31);
          this.MouseOverRect = new Rectangle(378, 68, 30, 31);
          break;
        case OverworldButtons.Breeding:
          this.redlight = new RedLight(true);
          this.redlight.scale = 0.5f;
          this.redlight.bActive = false;
          this.Icon.DrawRect = new Rectangle(983, 0, 30, 31);
          this.MouseOverRect = new Rectangle(983, 32, 30, 31);
          break;
        case OverworldButtons.Build:
          this.redlight = new RedLight(true);
          this.redlight.scale = 0.5f;
          this.redlight.bActive = false;
          this.Icon.DrawRect = new Rectangle(378, 100, 30, 31);
          this.MouseOverRect = new Rectangle(378, 132, 30, 31);
          break;
        case OverworldButtons.Manage:
          this.Icon.DrawRect = new Rectangle(952, 0, 30, 31);
          this.MouseOverRect = new Rectangle(952, 32, 30, 31);
          break;
        case OverworldButtons.Store:
          this.Icon.DrawRect = new Rectangle(983, 372, 30, 30);
          this.MouseOverRect = this.Icon.DrawRect;
          ++this.MouseOverRect.Y;
          break;
        case OverworldButtons.AlertBuilding:
          this.Icon.DrawRect = new Rectangle(434, 53, 26, 28);
          this.MouseOverRect = new Rectangle(542, 53, 26, 28);
          this.LerpPosMlt = -1f;
          this.MouseOverRect = new Rectangle(542, 53, 26, 28);
          break;
        case OverworldButtons.AlertStaff:
          this.Icon.DrawRect = new Rectangle(461, 53, 26, 28);
          this.LerpPosMlt = -1f;
          this.MouseOverRect = new Rectangle(569, 53, 26, 28);
          break;
        case OverworldButtons.AlertAnimals:
          this.Icon.DrawRect = new Rectangle(515, 53, 26, 28);
          this.LerpPosMlt = -1f;
          this.MouseOverRect = new Rectangle(623, 53, 26, 28);
          break;
        case OverworldButtons.AlertEmergency:
          this.Icon.DrawRect = new Rectangle(488, 53, 26, 28);
          this.LerpPosMlt = -1f;
          this.MouseOverRect = new Rectangle(596, 53, 26, 28);
          break;
        case OverworldButtons.HeatMapView:
          this.Icon.DrawRect = new Rectangle(790, 961, 30, 31);
          this.MouseOverRect = new Rectangle(790, 993, 30, 31);
          break;
        case OverworldButtons.AlertQuest:
          this.Icon.DrawRect = new Rectangle(230, 761, 26, 28);
          this.LerpPosMlt = -1f;
          this.MouseOverRect = new Rectangle(257, 768, 26, 28);
          break;
        case OverworldButtons.HeatMap_Privacy:
          this.Icon.DrawRect = new Rectangle(635, 961, 30, 31);
          this.MouseOverRect = new Rectangle(635, 993, 30, 31);
          break;
        case OverworldButtons.HeatMap_Utility:
          this.Icon.DrawRect = new Rectangle(759, 961, 30, 31);
          this.MouseOverRect = new Rectangle(759, 993, 30, 31);
          break;
        case OverworldButtons.HeatMap_Hygiene:
          this.Icon.DrawRect = new Rectangle(697, 961, 30, 31);
          this.MouseOverRect = new Rectangle(697, 993, 30, 31);
          break;
        case OverworldButtons.HeatMap_Profit:
          this.Icon.DrawRect = new Rectangle(728, 961, 30, 31);
          this.MouseOverRect = new Rectangle(728, 993, 30, 31);
          break;
        case OverworldButtons.HeatMap_Congestion:
          this.Icon.DrawRect = new Rectangle(666, 961, 30, 31);
          this.MouseOverRect = new Rectangle(666, 993, 30, 31);
          break;
        case OverworldButtons.HeatMap_Deco:
          this.Icon.DrawRect = new Rectangle(796, 920, 30, 31);
          this.MouseOverRect = new Rectangle(827, 920, 30, 31);
          break;
      }
      this.BaseRect = this.Icon.DrawRect;
      this.Icon.SetDrawOriginToCentre();
      if ((double) this.BaseScale != -1.0)
      {
        this.Icon.scale = this.BaseScale;
      }
      else
      {
        this.Icon.scale = 2f;
        if (DebugFlags.IsPCVersion)
          this.Icon.scale = 1f;
      }
      if (this.redlight == null)
        return;
      this.redlight.vLocation = new Vector2((float) ((double) -this.Icon.DrawRect.Width * (double) this.Icon.scale * 0.5), (float) -this.Icon.DrawRect.Height * 0.5f);
      RedLight redlight = this.redlight;
      redlight.vLocation = redlight.vLocation + new Vector2(2f, 2f) * this.Icon.scale * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public Vector2 GetSize() => new Vector2((float) this.Icon.DrawRect.Width, (float) this.Icon.DrawRect.Height) * this.Icon.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool CanPress() => (double) this.ActiveLerper.Value == 0.0;

    public bool CheckMouseOver(Player player) => MathStuff.CheckPointCollision(true, this.Location, this.Icon.scale, (float) this.Icon.DrawRect.Width, (float) this.Icon.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);

    public bool UpdateOWCategoryButton(float DeltaTime, Player player) => this.UpdateOWCategoryButton(DeltaTime, player, Vector2.Zero);

    public bool UpdateOWCategoryButton(float DeltaTime, Player player, Vector2 offset)
    {
      offset += this.Location;
      if (FeatureFlags.GetIsThisSubIconBlocked(this.buttontype))
      {
        if ((double) this.ActiveLerper.TargetValue != 1.0)
          this.ActiveLerper.SetLerp(false, 1f, 1f, 3f, true);
        if ((double) DeltaTime == 0.0)
          this.ActiveLerper.UpdateLerpHandler(GameFlags.RefDeltaTime);
      }
      else if ((double) this.ActiveLerper.TargetValue != 0.0)
        this.ActiveLerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      this.ActiveLerper.UpdateLerpHandler(DeltaTime);
      bool flag1 = false;
      if ((double) this.ActiveLerper.Value == 0.0)
      {
        bool flag2 = false;
        if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.None)
        {
          if (this.buttontype == OverworldButtons.Build && Z_GameFlags.ForceToNewScreen == ForceToNewScreen.BuildShop)
            flag2 = true;
          if (this.buttontype == OverworldButtons.Build && Z_GameFlags.ForceToNewScreen == ForceToNewScreen.BuildArchtect)
            flag2 = true;
          if (this.buttontype == OverworldButtons.Manage && Z_GameFlags.ForceToNewScreen == ForceToNewScreen.ResearchView)
            flag2 = true;
        }
        if (!GameFlags.IsUsingController || !player.inputmap.PressedThisFrame[15])
        {
          flag1 = MathStuff.CheckPointCollision(true, offset, this.Icon.scale, (float) this.Icon.DrawRect.Width, (float) this.Icon.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
          if (flag1 | flag2)
          {
            flag1 = true;
            player.inputmap.ClearAllInput(player);
          }
        }
        this.MouseOver = MathStuff.CheckPointCollision(true, offset, this.Icon.scale, (float) this.Icon.DrawRect.Width, (float) this.Icon.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      }
      if (this.buttontype == OverworldButtons.Breeding)
      {
        this.redlight.bActive = false;
        if (player.breeds.QueickCheckSomethingFinished(player))
          this.redlight.bActive = true;
      }
      if (this.buttontype == OverworldButtons.Build)
      {
        this.redlight.bActive = false;
        if (FeatureFlags.FlashBuildFromNotificationTrack || FeatureFlags.FlashBuildFromTask)
          this.redlight.bActive = true;
      }
      if (this.buttontype == OverworldButtons.Intake)
      {
        this.redlight.bActive = false;
        if (FeatureFlags.FlashTrade)
          this.redlight.bActive = true;
      }
      return flag1;
    }

    public void DrawOWCategoryButton(Vector2 Offset) => this.DrawOWCategoryButton(Offset, AssetContainer.pointspritebatch03);

    public void DrawOWCategoryButton(Vector2 Offset, SpriteBatch spriteBatch)
    {
      this.LastDrawLocation = Offset + this.Location;
      this.LastDrawLocation += new Vector2(this.ActiveLerper.Value * (200f * this.LerpPosMlt), 0.0f);
      if (this.MouseOver && !FeatureFlags.BlockMouseOverOnBuildBar)
      {
        this.Icon.DrawRect = this.MouseOverRect;
        this.Icon.Draw(spriteBatch, AssetContainer.SpriteSheet, this.LastDrawLocation);
        this.Icon.DrawRect = this.BaseRect;
      }
      else
        this.Icon.Draw(spriteBatch, AssetContainer.SpriteSheet, this.LastDrawLocation);
      if (this.redlight != null && this.redlight.bActive)
      {
        float y = 0.0f;
        if (this.MouseOver)
          y = this.BaseScale * 2f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.redlight.DrawRedLight(spriteBatch, this.LastDrawLocation + new Vector2(0.0f, y));
      }
      this.MouseOver = false;
    }

    public Vector2 GetOffset() => new Vector2(this.ActiveLerper.Value * 200f, 0.0f);
  }
}
