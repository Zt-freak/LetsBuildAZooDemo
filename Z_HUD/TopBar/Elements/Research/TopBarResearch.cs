// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Research.TopBarResearch
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.RatingPopUp.Row.Columns;

namespace TinyZoo.Z_HUD.TopBar.Elements.Research
{
  internal class TopBarResearch
  {
    public Vector2 location;
    private TopBarHeaderBase headerBase;
    private Z_ResearchIcon icon;
    private SatisfactionBarWithBigNumber bar;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private LerpHandler_Float lerper;
    private ResearchInfoPopOut popOut;
    private bool IsForResearchMenu_NotTopBar;
    private bool CropWhenAboveCertainHeight;
    private float CropHeight;
    private int lastResearchPointValue;
    private bool ForceSpendPointsReminder;

    public TopBarResearch(
      float _BaseScale,
      float frameHeight,
      Player player,
      bool _IsForResearchMenu_NotTopBar = false)
    {
      this.BaseScale = _BaseScale;
      this.IsForResearchMenu_NotTopBar = _IsForResearchMenu_NotTopBar;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      this.lerper = new LerpHandler_Float();
      this.icon = new Z_ResearchIcon(this.BaseScale, IconType.Research);
      this.bar = !this.IsForResearchMenu_NotTopBar ? new SatisfactionBarWithBigNumber(this.BaseScale) : new SatisfactionBarWithBigNumber(this.BaseScale, 0.0f);
      Vector2 zero = Vector2.Zero;
      zero.X += defaultBuffer.X;
      this.icon.location.X = zero.X;
      this.icon.location.X += this.icon.GetSize().X * 0.5f;
      zero.X += this.icon.GetSize().X;
      zero.X += defaultBuffer.X;
      this.bar.location.X = zero.X;
      zero.X += this.bar.GetSize().X;
      zero.X += defaultBuffer.X;
      this.headerBase = !this.IsForResearchMenu_NotTopBar ? new TopBarHeaderBase(this.BaseScale, frameHeight, zero.X, true) : new TopBarHeaderBase(this.BaseScale, frameHeight, zero.X, ColourData.Z_FrameBluePale, true, 1f);
      Vector2 vector2 = -this.headerBase.GetSize() * 0.5f;
      this.icon.location.X += vector2.X;
      this.bar.location.X += vector2.X;
      this.SetData(player, true);
    }

    public Vector2 GetSize() => this.headerBase.GetSize();

    public void LerpIn()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return;
      this.lerper.SetLerp(false, -1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if ((double) this.lerper.TargetValue == -1.0)
        return;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      if (!this.IsForResearchMenu_NotTopBar)
        offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      return this.popOut != null && this.popOut.CheckMouseOver(player, offset);
    }

    public void DoRedFlash() => this.headerBase.DoFlash(false);

    public void DoFlashForChange() => this.headerBase.DoFlash(true);

    public void UpdateTopBarResearch(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.IsForResearchMenu_NotTopBar)
      {
        offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
        if (FeatureFlags.BlockAllUI)
          this.LerpOff();
        else
          this.LerpIn();
      }
      if ((this.headerBase.UpdateTopBarHeaderBase(player, DeltaTime, offset) || this.ForceSpendPointsReminder) && (double) this.lerper.Value == 0.0)
      {
        if (this.popOut == null || this.popOut.IsOffScreen() || this.ForceSpendPointsReminder)
        {
          this.popOut = new ResearchInfoPopOut(this.BaseScale, player, this.IsForResearchMenu_NotTopBar, this.ForceSpendPointsReminder);
          this.popOut.location.Y += TopBarManager.GetMiddleOfBar();
          this.popOut.location.Y += this.popOut.GetSize().Y * 0.5f;
          if (this.IsForResearchMenu_NotTopBar)
            this.popOut.location.Y += this.scaleHelper.ScaleY(8f);
          float num1 = (float) ((double) this.popOut.GetSize().X * 0.5 - (double) this.GetSize().X * 0.5);
          if ((double) this.popOut.location.X + (double) offset.X + (double) num1 + (double) this.popOut.GetSize().X * 0.5 < 1024.0)
            this.popOut.location.X += num1;
          else
            this.popOut.location.X -= num1;
          int num2 = this.CropWhenAboveCertainHeight ? 1 : 0;
          this.ForceSpendPointsReminder = false;
        }
        else
          this.popOut.ToggleLerp();
      }
      else if (this.popOut != null)
      {
        if (!this.popOut.UpdateResearchInfoPopOut(player, DeltaTime, offset) && !this.IsForResearchMenu_NotTopBar && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
          this.popOut.LerpOff();
        if (!this.IsForResearchMenu_NotTopBar && FeatureFlags.BlockAllUI)
          this.popOut.LerpOff();
      }
      this.SetData(player);
      this.lerper.UpdateLerpHandler(DeltaTime);
    }

    private void SetData(Player player, bool IsInit = false)
    {
      if (this.IsForResearchMenu_NotTopBar)
      {
        this.bar.SetBarValues(player.z_research.GetResearchProgressForHUDAbstract(), DontDisplayValue: true);
      }
      else
      {
        int researchPoints = player.unlocks.ResearchPoints;
        if (!IsInit && researchPoints > this.lastResearchPointValue && this.CheckIfPlayerCanUnlockSomething(player))
          this.ForceSpendPointsReminder = true;
        this.lastResearchPointValue = researchPoints;
        this.bar.SetBarValues(player.z_research.GetResearchProgressForHUDAbstract(), researchPoints.ToString());
      }
    }

    private bool CheckIfPlayerCanUnlockSomething(Player player) => true;

    public void SetToCropWhenAboveThis(float cropHeight)
    {
      this.CropWhenAboveCertainHeight = true;
      this.CropHeight = cropHeight;
    }

    public void PreDrawTopBarRating(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (!this.IsForResearchMenu_NotTopBar)
        offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (this.popOut == null)
        return;
      this.popOut.DrawResearchInfoPopOut(offset, spriteBatch);
    }

    public void DrawTopBarResearch(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (!this.IsForResearchMenu_NotTopBar)
        offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.headerBase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.icon.DrawResearchIcon(offset, spriteBatch);
      this.bar.DrawSatisfactionBarWithBigNumber(offset, spriteBatch);
      this.headerBase.PostDrawTopBarHeaderBase(offset, spriteBatch);
    }
  }
}
