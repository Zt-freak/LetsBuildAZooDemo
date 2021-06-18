// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.UXPanels.TimeOfDayPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GenericUI.UXPanels
{
  internal class TimeOfDayPanel
  {
    private int CASHLastFrame;
    public Vector2 Locaion;
    private GameObject Clock;
    private string DaysText;
    private string Time;
    private GameObject textthing;
    private UXFrame uxframe;

    public TimeOfDayPanel(Player player)
    {
      this.uxframe = new UXFrame(RenderMath.GetPixelSizeBestMatch(2f));
      this.Clock = new GameObject();
      this.Clock.DrawRect = new Rectangle(123, 0, 7, 7);
      this.Clock.SetDrawOriginToCentre();
      this.Clock.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.textthing = new GameObject();
      this.textthing.SetAllColours(ColourData.TanGoldText);
      this.textthing.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.Clock.SetAllColours(new Vector3(240f, 190f, 64f) / (float) byte.MaxValue);
    }

    public void UpdateDollarPanel(float DeltaTime, Player player)
    {
      this.DaysText = string.Format(SEngine.Localization.Localization.GetText(12), (object) Player.financialrecords.GetDaysPassed());
      this.Time = player.Stats.GetTimeOfDay() ?? "";
    }

    public void DrawDollarPanel(Vector2 Offset)
    {
      this.Locaion = new Vector2(550f, (float) ((double) this.uxframe.DrawRect.Height * (double) this.uxframe.scale * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.uxframe.DrawUXFrame(Offset + this.Locaion);
      this.Clock.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.Locaion + new Vector2(30f, 0.0f));
      this.textthing.vLocation = new Vector2(-40f * this.uxframe.scale, -4f * this.uxframe.scale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      TextFunctions.DrawTextWithDropShadow(this.DaysText, this.textthing.scale, Offset + this.Locaion + this.textthing.vLocation - new Vector2(18f, 0.0f), this.textthing.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      this.textthing.vLocation.X = 10f * this.uxframe.scale;
      TextFunctions.DrawTextWithDropShadow(this.Time, this.textthing.scale, Offset + this.Locaion + this.textthing.vLocation + new Vector2(30f, 0.0f), this.textthing.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
    }
  }
}
