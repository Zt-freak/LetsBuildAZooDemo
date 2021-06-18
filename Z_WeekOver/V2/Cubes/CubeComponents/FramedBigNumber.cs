// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.FramedBigNumber
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class FramedBigNumber
  {
    private CustomerFrame customerframe;
    private MiniHeading miniheading;
    private GameObject LowerTextObject;
    public Vector2 Position;
    private SpringFont LowerFont;
    private NumberLerp numberlerper;
    private string NumberPrefix;
    private ColourBehavor colourbehaviour;
    private bool WillfadeIn;
    public LerpHandler_Float fadelerper;
    private Vector3 TopColour;
    private Vector3 BottomColour;

    public FramedBigNumber(
      float BaseScale,
      string Heading,
      Vector3 ColourForFrame,
      int Number,
      string PREFIX,
      bool _WillfadeIn)
    {
      this.WillfadeIn = _WillfadeIn;
      if (this.WillfadeIn)
      {
        this.fadelerper = new LerpHandler_Float();
        this.fadelerper.SetLerp(true, 0.0f, 1f, 3f);
      }
      this.LowerTextObject = new GameObject();
      this.LowerTextObject.SetAllColours(ColourData.Z_Cream);
      this.LowerFont = AssetContainer.roundaboutFont;
      this.LowerTextObject.scale = BaseScale * 2f;
      this.LowerTextObject.vLocation.X = (float) (85.0 * -(double) BaseScale);
      this.LowerTextObject.vLocation.Y = (float) (8.0 * -(double) BaseScale);
      this.customerframe = new CustomerFrame(new Vector2(180f * BaseScale, 80f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y), ColourForFrame, BaseScale);
      this.miniheading = new MiniHeading(this.customerframe.VSCale, Heading, BaseScale, BaseScale);
      this.numberlerper = new NumberLerp();
      this.numberlerper.SetBasicNumberAndTarget(Number, 0.5f);
      this.NumberPrefix = PREFIX;
    }

    public void SetColourBehaviour(ColourBehavor clrbehaviour)
    {
      this.colourbehaviour = clrbehaviour;
      switch (this.colourbehaviour)
      {
        case ColourBehavor.Money_Blue_Black:
          this.TopColour = new Vector3(0.4235294f, 0.5490196f, 0.4235294f);
          this.BottomColour = new Vector3(0.8352941f, 0.5294118f, 0.3333333f);
          if ((double) this.numberlerper.CurrentTransitioningValue <= 0.0)
          {
            this.customerframe.frame.SetColours(false, 0.0f, this.BottomColour, this.BottomColour);
            break;
          }
          this.customerframe.frame.SetColours(false, 0.0f, this.TopColour, this.TopColour);
          break;
        case ColourBehavor.Loan_Black_Red:
          this.TopColour = new Vector3(0.5294118f, 0.6117647f, 0.5294118f);
          this.BottomColour = new Vector3(0.7529412f, 0.4941176f, 0.4588235f);
          if ((double) this.numberlerper.CurrentTransitioningValue < 0.0)
          {
            this.customerframe.frame.SetColours(false, 0.0f, this.BottomColour, this.BottomColour);
            break;
          }
          this.customerframe.frame.SetColours(false, 0.0f, this.TopColour, this.TopColour);
          break;
      }
    }

    public void CheckValue(int NewValue)
    {
      this.CheckColours();
      if (NewValue == this.numberlerper.CurrentTargetValue_DoNotDraw)
        return;
      this.numberlerper.AddOrRemoveValue(NewValue - this.numberlerper.CurrentTargetValue_DoNotDraw);
    }

    private void CheckColours()
    {
      float blendTime = 0.8f;
      switch (this.colourbehaviour)
      {
        case ColourBehavor.Money_Blue_Black:
          if ((double) this.customerframe.frame.fTargetBlue > 0.400000005960464)
          {
            if ((double) this.numberlerper.CurrentTransitioningValue > 0.0)
              break;
            this.customerframe.frame.SetColours(true, blendTime, Vector3.One, this.BottomColour);
            break;
          }
          if ((double) this.numberlerper.CurrentTransitioningValue <= 0.0)
            break;
          this.customerframe.frame.SetColours(true, blendTime, Vector3.One, this.TopColour);
          break;
        case ColourBehavor.Loan_Black_Red:
          if ((double) this.customerframe.frame.fTargetRed < 0.400000005960464)
          {
            if ((double) this.numberlerper.CurrentTransitioningValue <= 0.0)
              break;
            this.customerframe.frame.SetColours(true, blendTime, Vector3.One, this.BottomColour);
            break;
          }
          if ((double) this.numberlerper.CurrentTransitioningValue > 0.0)
            break;
          this.customerframe.frame.SetColours(true, blendTime, Vector3.One, this.TopColour);
          break;
      }
    }

    public void UpdateFramedBigNumber(float DeltaTime)
    {
      if (this.fadelerper != null)
        this.fadelerper.UpdateLerpHandler(DeltaTime);
      this.customerframe.frame.UpdateColours(DeltaTime);
      this.numberlerper.UpdateNumberLerp(DeltaTime);
    }

    public bool NumberLerpComplete() => this.numberlerper.LerpComplete();

    public void DrawFramedBigNumber(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Position;
      float Alpha = 1f;
      if (this.fadelerper != null)
      {
        Alpha = this.fadelerper.Value;
        this.customerframe.frame.SetAlpha(Alpha);
        this.miniheading.SetAlpha(Alpha);
      }
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.miniheading.DrawMiniHeading(Offset, spritebatch);
      if ((this.NumberPrefix + (object) (int) this.numberlerper.CurrentTransitioningValue).Length >= 5)
        TextFunctions.DrawTextWithDropShadow(this.NumberPrefix + (object) (int) this.numberlerper.CurrentTransitioningValue, this.LowerTextObject.scale * 0.5f, Offset + this.LowerTextObject.vLocation, this.LowerTextObject.GetColour(), this.LowerTextObject.fAlpha * Alpha, this.LowerFont, spritebatch, false);
      else
        TextFunctions.DrawTextWithDropShadow(this.NumberPrefix + (object) (int) this.numberlerper.CurrentTransitioningValue, this.LowerTextObject.scale, Offset + this.LowerTextObject.vLocation, this.LowerTextObject.GetColour(), this.LowerTextObject.fAlpha * Alpha, this.LowerFont, spritebatch, false);
    }
  }
}
