// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Store_Local.Entries.IConHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.Store_Local.Entries
{
  internal class IConHeading : GameObject
  {
    private string TXT;

    public IConHeading(StoreEntryType storeicontype)
    {
      if (GameFlags.MobileUIScale)
        this.scale = 3f;
      else
        this.scale = 2f;
      this.SetAllColours(ColourData.YellowHighlight);
      this.vLocation = new Vector2(10f, -5f);
      switch (storeicontype)
      {
        case StoreEntryType.BasicBeam:
          this.TXT = "More Lasers";
          break;
        case StoreEntryType.BeamSpeed:
          this.TXT = "Up Speed";
          break;
        case StoreEntryType.BeamL2:
          this.TXT = "Laser V2";
          break;
        case StoreEntryType.InstaBeam:
          this.TXT = "Instant Field";
          break;
        case StoreEntryType.BeamSpeedL2:
          this.TXT = "L2 Beam Speed";
          break;
      }
    }

    public void DrawIConHeading(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      float num = 1f;
      if (PlayerStats.language == Language.Russian || PlayerStats.language == Language.German || PlayerStats.language == Language.Spanish)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TXT, "XXXXXXXXXXXXXX", AssetContainer.springFont, true);
      this.vLocation = new Vector2(-90f, -10f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      TextFunctions.DrawTextWithDropShadow(this.TXT, this.scale * num, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, DrawWithThis, false);
    }
  }
}
