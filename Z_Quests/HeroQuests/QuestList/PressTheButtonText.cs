// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.HeroQuests.QuestList.PressTheButtonText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Quests.HeroQuests.QuestList
{
  internal class PressTheButtonText : GameObject
  {
    private CustomerFrame frame;
    private string TextA;
    private string TextB;

    public PressTheButtonText(float BaseScale, bool IsNew = false)
    {
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.frame = new CustomerFrame(new Vector2(93f * this.scale, 38f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y), true, this.scale);
      this.frame.frame.vLocation.X = -8f * this.scale;
      this.TextA = "FINISH";
      this.TextB = "TASK";
      if (!IsNew)
        return;
      this.TextA = "NEW";
    }

    public void DrawPressTheButtonText(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.frame.DrawCustomerFrame(Offset + this.vLocation, spritebatch);
      TextFunctions.DrawTextWithDropShadow(this.TextA, this.scale, this.vLocation + Offset + new Vector2(0.0f, -14f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false, true);
      TextFunctions.DrawTextWithDropShadow(this.TextB, this.scale, this.vLocation + Offset + new Vector2(0.0f, 0.0f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false, true);
    }
  }
}
