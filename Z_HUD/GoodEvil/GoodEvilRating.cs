// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.GoodEvil.GoodEvilRating
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_HUD.TopBar;
using TinyZoo.Z_HUD.TopBar.Elements.Morality;

namespace TinyZoo.Z_HUD.GoodEvil
{
  internal class GoodEvilRating
  {
    private TextButton rating;
    private LerpHandler_Float lerper;
    public Vector2 location;
    private TopBarMorality topBarMorality_newUI;
    private bool UseNewUI;

    public GoodEvilRating(Player player)
    {
      this.rating = new TextButton(SEngine.Localization.Localization.GetText(985), 120f, OverAllMultiplier: 0.66666f);
      this.rating.vLocation = new Vector2(625f, 15f);
      this.SetUp(player);
    }

    public GoodEvilRating(Player player, float BaseScale, float frameHeight)
    {
      this.UseNewUI = true;
      this.topBarMorality_newUI = new TopBarMorality(BaseScale, frameHeight);
      this.SetUp(player);
    }

    private void SetUp(Player player)
    {
      this.lerper = new LerpHandler_Float();
      this.SetString(player);
    }

    public Vector2 GetSize()
    {
      if (this.UseNewUI)
        return this.topBarMorality_newUI.GetSize();
      throw new Exception("not coded");
    }

    private void SetString(Player player)
    {
      float moralityScore = player.livestats.MoralityScore;
      if (this.UseNewUI)
        this.topBarMorality_newUI.SetMoralityValue(moralityScore);
      else if ((double) moralityScore < 0.0)
      {
        this.rating.SetText(SEngine.Localization.Localization.GetText(985) + SEngine.Localization.Localization.GetText(989).ToUpper() + ": " + (object) (float) -(double) moralityScore);
        this.rating.SetButtonColour(BTNColour.Red);
      }
      else if ((double) moralityScore > 0.0)
      {
        this.rating.SetText(SEngine.Localization.Localization.GetText(985) + SEngine.Localization.Localization.GetText(990).ToUpper() + ": " + (object) moralityScore);
        this.rating.SetButtonColour(BTNColour.Green);
      }
      else
      {
        this.rating.SetText(SEngine.Localization.Localization.GetText(985) + ": " + SEngine.Localization.Localization.GetText(991));
        this.rating.SetButtonColour(BTNColour.Blue);
      }
    }

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
      return this.topBarMorality_newUI.CheckMouseOver(player, offset);
    }

    public void UpdateGoodEvilRating(Player player, float DeltaTime) => this.UpdateGoodEvilRating(player, DeltaTime, Vector2.Zero);

    public void UpdateGoodEvilRating(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (FeatureFlags.BlockAllUI)
        this.LerpOff();
      else
        this.LerpIn();
      if (Z_GameFlags.RecalculatedMorality)
      {
        Z_GameFlags.RecalculatedMorality = false;
        this.SetString(player);
      }
      if ((double) DeltaTime == 0.0)
        this.lerper.UpdateLerpHandler(GameFlags.RefDeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0)
        return;
      bool flag = false;
      if (this.UseNewUI)
      {
        if (!this.topBarMorality_newUI.UpdateTopBarMorality(player, DeltaTime, offset))
          ;
      }
      else if (this.rating.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        flag = true;
      if (!flag)
        return;
      TinyZoo.Game1.screenfade.BeginFade(true);
      TinyZoo.Game1.SetNextGameState(GAMESTATE.MoralitySummarySetUp);
    }

    public void DrawGoodEvilRating() => this.DrawGoodEvilRating(Vector2.Zero, AssetContainer.pointspritebatch03);

    public void PreDrawGoodEvilRating(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (!this.UseNewUI)
        return;
      this.topBarMorality_newUI.PreDrawTopBarMorality(offset, spriteBatch);
    }

    public void DrawGoodEvilRating(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (this.UseNewUI)
        this.topBarMorality_newUI.DrawTopBarMorality(offset, spriteBatch);
      else
        this.rating.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
