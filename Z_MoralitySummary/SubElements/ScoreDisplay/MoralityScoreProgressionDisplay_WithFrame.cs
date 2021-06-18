// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.SubElements.ScoreDisplay.MoralityScoreProgressionDisplay_WithFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_MoralitySummary.SubElements.ScoreDisplay
{
  internal class MoralityScoreProgressionDisplay_WithFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MoralityScoreProgressionDisplay moralityDisplay;
    private float BaseScale;
    private Vector2 buffer;
    private UIScaleHelper scaleHelper;

    public MoralityScoreProgressionDisplay_WithFrame(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.moralityDisplay = new MoralityScoreProgressionDisplay(this.BaseScale);
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Yellow, this.BaseScale);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetScore(
      float currentScore,
      float maxScore,
      bool IsGoodNotEvil_Icon,
      bool RoundToInt = true)
    {
      this.moralityDisplay.SetScore(currentScore, maxScore, IsGoodNotEvil_Icon, RoundToInt);
      this.SetCustomerFrame(IsGoodNotEvil_Icon);
    }

    public void SmartSetScoreForBuilding(TILETYPE tileType, Player player) => this.SetCustomerFrame(this.moralityDisplay.SmartSetScoreForBuilding(tileType, player));

    private void SetCustomerFrame(bool _IsGood)
    {
      CustomerFrameColors color = CustomerFrameColors.Yellow;
      if (!_IsGood)
        color = CustomerFrameColors.Purple;
      this.customerFrame = new CustomerFrame(new Vector2(this.scaleHelper.ScaleX(120f), this.moralityDisplay.GetSize().Y + this.buffer.Y), color, this.BaseScale);
      this.moralityDisplay.location.X -= this.moralityDisplay.GetSize().X * 0.5f;
    }

    public void DrawMoralityScoreProgressionDisplay_WithFrame(
      Vector2 offset,
      SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.moralityDisplay.DrawMoralityScoreProgressionDisplay(offset, spriteBatch);
    }
  }
}
