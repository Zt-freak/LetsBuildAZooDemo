// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet.WeightBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet
{
  internal class WeightBar
  {
    public Vector2 location;
    private SatisfactionBar bar;
    private PointerAndText pointerAndText;
    private float extraPointerHeightOnTopOfBar;

    public WeightBar(PrisonerInfo prisonerInfo, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float _Fullness1 = 0.2f;
      float _Fullness2 = 0.7f;
      float num1 = MathHelper.Clamp(prisonerInfo.GetWeightValueForBar(), 0.0f, 1f);
      this.bar = new SatisfactionBar(0.0f, BaseScale);
      this.bar.SetFullness(1f);
      this.bar.SetFullness(_Fullness2, 1);
      this.bar.SetFullness(_Fullness1, 2);
      this.bar.SetBarColours(ColourData.Z_BarYellow, 2);
      this.bar.SetBarColours(ColourData.Z_BarBabyGreen, 1);
      this.bar.SetBarColours(ColourData.Z_BarRed);
      string Text = "Healthy Weight";
      if ((double) num1 < (double) _Fullness1)
        Text = "Underweight";
      else if ((double) num1 > (double) _Fullness2)
        Text = "Overweight";
      this.pointerAndText = new PointerAndText(Text, 1f, BaseScale, (uiScaleHelper.ScaleY(9f) + this.bar.GetSize().Y) / BaseScale);
      this.pointerAndText.vLocation.X += num1 * (this.bar.GetSize().X - uiScaleHelper.DefaultBuffer.X);
      this.pointerAndText.vLocation.X -= this.bar.GetSize().X * 0.5f;
      this.extraPointerHeightOnTopOfBar = (float) (((double) this.pointerAndText.GetLineVScale().Y - (double) this.bar.GetSize().Y) * 0.5);
      float num2 = this.extraPointerHeightOnTopOfBar + this.bar.GetSize().Y * 0.5f;
      this.bar.vLocation.Y += num2;
      this.pointerAndText.vLocation.Y += num2;
    }

    public Vector2 GetSize() => new Vector2(this.bar.GetSize().X, this.pointerAndText.GetLineAndTextHeight());

    public float GetHeightToCenterOfBar() => this.extraPointerHeightOnTopOfBar + this.bar.GetSize().Y * 0.5f;

    public void DrawWeightBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bar.DrawSatisfactionBar(offset, spriteBatch);
      this.pointerAndText.DrawPointerAndText(offset, spriteBatch);
    }
  }
}
