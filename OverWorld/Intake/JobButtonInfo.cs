// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.JobButtonInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.OverWorld.Intake
{
  internal class JobButtonInfo
  {
    private GameObject TextStuff;
    private int Total;
    private GameObject LittleGuy;

    public JobButtonInfo(IntakeInfo intakeinfo)
    {
      this.TextStuff = new GameObject();
      this.TextStuff.SetAllColours(ColourData.Cyannz);
      this.Total = intakeinfo.People.Count;
      this.LittleGuy = new GameObject();
      this.LittleGuy.DrawRect = new Rectangle(96, 225, 5, 8);
      this.LittleGuy.SetDrawOriginToCentre();
      this.LittleGuy.scale = 10f;
      this.LittleGuy.fAlpha = 0.5f;
    }

    public void DrawJobButtonInfo(Vector2 Offset, SpriteBatch spritebatch)
    {
      float scale1 = 1.5f;
      float scale2 = 2.5f;
      if (GameFlags.MobileUIScale)
      {
        scale1 = 2f;
        scale2 = 3f * Sengine.UltraWideSreenUpwardsMultiplier;
      }
      if (!GameFlags.MobileUIScale || (double) Sengine.UltraWideSreenUpwardsMultiplier <= 1.0)
      {
        if (PlayerStats.language == Language.Japanese)
          scale1 = 2.5f;
        if (PlayerStats.language == Language.Chinese_Simplified || PlayerStats.language == Language.Chinese_Traditional)
          scale1 = 3f;
        if (PlayerStats.language == Language.Korean)
          scale1 = 2.5f;
        if (PlayerStats.language == Language.Russian)
          scale1 = 1.5f;
        if (PlayerStats.language == Language.French || PlayerStats.language == Language.Portuguese)
          scale1 = 1.7f;
        if (PlayerStats.language == Language.German)
          scale1 = 1.9f;
        TextFunctions.DrawTextWithDropShadow("PLACEHOLDER", scale1, this.TextStuff.vLocation + Offset + new Vector2(-30f, -20f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.TextStuff.GetColour(), this.TextStuff.fAlpha, AssetContainer.springFont, spritebatch, false);
      }
      this.LittleGuy.scale = 4f;
      TextFunctions.DrawTextWithDropShadow("x" + (object) this.Total, scale2, this.TextStuff.vLocation + Offset + new Vector2(30f * Sengine.UltraWideSreenDownardsMultiplier, -10f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.TextStuff.GetColour(), this.TextStuff.fAlpha, AssetContainer.springFont, spritebatch, false);
      this.LittleGuy.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + new Vector2(-50f, 0.0f));
    }
  }
}
