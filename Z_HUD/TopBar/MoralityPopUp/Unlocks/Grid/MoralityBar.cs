// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid.MoralityBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralityActionUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid
{
  internal class MoralityBar
  {
    public Vector2 location;
    private SatisfactionBar bar;
    private CustomerFrame customerFrame;
    private MoralityScoreRequired scoreDisplay;
    private Vector2 totalSize;

    public MoralityBar(
      bool IsGoodNotEvil,
      float BaseScale,
      bool AddFrame = false,
      bool AddScoreOnRight = false,
      BarSIze barSize = BarSIze.Normal)
    {
      Vector2 vector2_1 = new UIScaleHelper(BaseScale).ScaleVector2(new Vector2(5f, 5f));
      float num1 = 0.0f;
      this.bar = new SatisfactionBar(0.0f, BaseScale, barSize);
      this.bar.SetBarColours(IsGoodNotEvil ? ColourData.GoodYellow : ColourData.EvilPurple);
      this.bar.vLocation.X += this.bar.GetSize().X * 0.5f;
      float x = num1 + this.bar.GetSize().X;
      if (AddScoreOnRight)
      {
        float num2 = x + vector2_1.X;
        this.scoreDisplay = new MoralityScoreRequired(IsGoodNotEvil, -100f, BaseScale);
        this.scoreDisplay.location.X = num2;
        this.scoreDisplay.location.X += this.scoreDisplay.GetSize().X * 0.5f;
        x = num2 + this.scoreDisplay.GetSize().X;
      }
      this.totalSize = new Vector2(x, this.bar.GetSize().Y);
      if (AddFrame)
      {
        this.totalSize += vector2_1 * 2f;
        this.customerFrame = new CustomerFrame(this.totalSize, IsGoodNotEvil ? CustomerFrameColors.GoodYellowFrame : CustomerFrameColors.EvilPurpleFrame, BaseScale);
        this.bar.vLocation.X += vector2_1.X;
        if (this.scoreDisplay != null)
          this.scoreDisplay.location.X += vector2_1.X;
      }
      Vector2 vector2_2 = -this.totalSize * 0.5f;
      this.bar.vLocation.X += vector2_2.X;
      if (this.scoreDisplay == null)
        return;
      this.scoreDisplay.location.X += vector2_2.X;
    }

    public void SetScoreValue(float fullness, float scoreValue = -1f)
    {
      this.bar.SetFullness(fullness);
      if (this.scoreDisplay == null)
        return;
      this.scoreDisplay.SetNewValue(scoreValue);
    }

    public Vector2 GetSize() => this.totalSize;

    public void UpdateMoralityBar()
    {
    }

    public void DrawMoralityBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.customerFrame != null)
        this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.bar.DrawSatisfactionBar(offset, spriteBatch);
      if (this.scoreDisplay == null)
        return;
      this.scoreDisplay.DrawMoralityScoreRequired(offset, spriteBatch);
    }
  }
}
