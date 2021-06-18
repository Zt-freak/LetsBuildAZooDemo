// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_DayNight.StartDayButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld;

namespace TinyZoo.Z_DayNight
{
  internal class StartDayButton : GameObject
  {
    private GameObject tcls;
    private bool MouseOver;
    private string TextForButton;
    private bool IsSkipDay;
    private LerpHandler_Float lerper;

    public StartDayButton(bool _IsSkipDay)
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.TextForButton = SEngine.Localization.Localization.GetText(885);
      if (_IsSkipDay)
        this.TextForButton = SEngine.Localization.Localization.GetText(963);
      this.IsSkipDay = _IsSkipDay;
      this.tcls = new GameObject();
      this.tcls.SetAllColours(ColourData.ACDarkerAutumn);
      this.DrawRect = new Rectangle(656, 438, 81, 51);
      if (this.IsSkipDay)
      {
        this.tcls.SetAllColours(0.1f, 0.1f, 0.4f);
        this.DrawRect = new Rectangle(932, 662, 81, 51);
      }
      this.SetDrawOriginToCentre();
      this.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public bool CheckMouseOver(Player player) => MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);

    public bool UpdateStartDayButton(Player player, float DeltaTime)
    {
      if (Z_GameFlags.HasStartedFirstDay || !Z_GameFlags.QuestToOpenZooStarted)
        return false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.MouseOver = MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      if (this.MouseOver)
        OverwoldMainButtons.MouseIsOverAButton = true;
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawStartDayButton(Vector2 Offset)
    {
      if (Z_GameFlags.HasStartedFirstDay || !Z_GameFlags.QuestToOpenZooStarted || DebugFlags.HideAllUI_DEBUG)
        return;
      if (this.MouseOver)
      {
        --this.DrawRect.Height;
        ++this.DrawRect.Y;
      }
      this.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawJustifiedText(this.TextForButton, this.scale * 0.5f, this.vLocation + Offset + new Vector2(0.0f, 17f * this.scale), this.tcls.GetColour(), this.fAlpha, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch01);
      if (!this.MouseOver)
        return;
      --this.DrawRect.Y;
      ++this.DrawRect.Height;
      this.MouseOver = false;
    }
  }
}
