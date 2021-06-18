// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_BusStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using TinyZoo.Z_Bus;

namespace TinyZoo.Z_HUD
{
  internal class Z_BusStatus : AnimatedGameObject
  {
    private Vector2 ChopperLocation;
    private Vector2Int position;
    private GameObject BuIcon;
    private GameObject ChopperIcon;

    public Z_BusStatus()
    {
      this.DrawRect = new Rectangle(475, 840, 23, 21);
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.SetUpSimpleAnimation(7, 0.1f);
      this.DrawOrigin.X -= 20f;
      this.position = new Vector2Int(1, TileMath.GetOverWorldMapSize_YSize() - 3);
      this.SetAlpha(0.0f);
      this.DrawOrigin.Y = 18f;
      this.SetAllColours(0.8f, 0.5f, 0.0f);
      this.BuIcon = new GameObject();
      this.BuIcon.DrawRect = new Rectangle(81, 780, 18, 13);
      this.BuIcon.SetDrawOriginToCentre();
      this.ChopperIcon = new GameObject();
      this.ChopperIcon.DrawRect = new Rectangle(942, 372, 18, 14);
      this.ChopperIcon.SetDrawOriginToCentre();
      this.BuIcon.vLocation.X = (float) this.BuIcon.DrawRect.Width * -0.5f * this.BuIcon.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.ChopperIcon.vLocation.X = (float) this.ChopperIcon.DrawRect.Width * -0.5f * this.ChopperIcon.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.BuIcon.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
      this.ChopperIcon.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
    }

    public void UpdateZ_BusStatusAsChopper(float DeltaTime, Vector2 _ChopperLocation, bool DrawIt)
    {
      this.BuIcon.bActive = false;
      this.ChopperIcon.bActive = true;
      this.UpdateAnimation(DeltaTime);
      this.ChopperLocation = _ChopperLocation;
      if ((double) RenderMath.TranslateWorldSpaceToScreenSpace(this.ChopperLocation).X > 30.0)
        DrawIt = false;
      if ((double) this.fTargetAlpha < 1.0 & DrawIt)
        this.SetAlpha(true, 0.2f, 0.0f, 1f);
      else if (!DrawIt && (double) this.fTargetAlpha > 0.0)
        this.SetAlpha(true, 0.2f, 1f, 0.0f);
      this.ColourCycle(0.7f, 0.8f, 0.5f, 0.0f, 0.9f, 0.75f, 0.4f);
      this.UpdateColours(DeltaTime);
    }

    public void UpdateZ_BusStatus(float DeltaTime)
    {
      this.BuIcon.bActive = true;
      this.ChopperIcon.bActive = false;
      bool flag = false;
      if (Z_BusManager.busses.Count > 0 && (Z_BusManager.busses[0].drivestate == DriveState.DrivingInToDropOff || Z_BusManager.busses[0].drivestate == DriveState.DrivingIn) && (double) RenderMath.TranslateWorldSpaceToScreenSpace(Z_BusManager.busses[0].vLocation).X < 0.0)
        flag = true;
      if ((double) this.fTargetAlpha < 1.0 & flag)
        this.SetAlpha(true, 0.2f, 0.0f, 1f);
      else if (!flag && (double) this.fTargetAlpha > 0.0)
        this.SetAlpha(true, 0.2f, 1f, 0.0f);
      this.UpdateColours(DeltaTime);
    }

    public void DrawZ_BusStatusForChopper()
    {
      if ((double) this.fAlpha <= 0.0)
        return;
      this.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.ChopperLocation);
      this.vLocation.X = 0.0f;
      this.scale = Sengine.WorldOriginandScale.Z;
      this.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
    }

    public void DrawZ_BusStatus()
    {
      if ((double) this.fAlpha <= 0.0)
        return;
      this.vLocation = TileMath.GetTileToWorldSpace(this.position);
      this.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.vLocation);
      this.vLocation.X = 0.0f;
      this.scale = Sengine.WorldOriginandScale.Z;
      this.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
      if (this.BuIcon.bActive)
      {
        this.BuIcon.scale = this.scale;
        this.BuIcon.vLocation.X = 2f * this.scale;
        this.BuIcon.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, this.vLocation + new Vector2(0.0f, FlashingAlpha.MediumSin * 3f));
      }
      else
      {
        if (!this.ChopperIcon.bActive)
          return;
        this.ChopperIcon.scale = 2f * this.scale;
        this.ChopperIcon.vLocation.X = 3f;
        this.ChopperIcon.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, this.vLocation + new Vector2(0.0f, FlashingAlpha.MediumSin * 3f));
      }
    }
  }
}
