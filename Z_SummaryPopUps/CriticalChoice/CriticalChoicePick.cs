// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.CriticalChoice.CriticalChoicePick
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.CriticalChoice
{
  internal class CriticalChoicePick
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    private TextButton button;
    private SimpleTextHandler body;
    public bool ControllerSelected;

    public CriticalChoicePick(
      StarColour morality,
      string label,
      string bodytext,
      float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.button = new TextButton(this.basescale, label, 40f);
      this.body = new SimpleTextHandler(bodytext, this.scalehelper.ScaleX(180f), true, this.basescale);
      this.body.SetAllColours(ColourData.Z_DarkTextGray);
      this.body.AutoCompleteParagraph();
      this.button.AddControllerButton(ControllerButton.XboxA);
      this.framescale = this.scalehelper.ScaleVector2(new Vector2(220f, 130f));
      switch (morality)
      {
        case StarColour.Good_Yellow:
          this.frame = new CustomerFrame(this.framescale, CustomerFrameColors.GoodYellowChoice, this.basescale);
          this.button.SetButtonColour(BTNColour.CriticalGoodYellow);
          break;
        case StarColour.Evil_Purple:
          this.frame = new CustomerFrame(this.framescale, CustomerFrameColors.EvilPurpleChoice, this.basescale);
          this.button.SetButtonColour(BTNColour.CriticalEvilPurple);
          break;
        case StarColour.Neutral:
          this.frame = new CustomerFrame(this.framescale, CustomerFrameColors.NeutralGrayChoice, this.basescale);
          this.button.SetButtonColour(BTNColour.CriticalNeutralBlue);
          break;
      }
      Vector2 vector2 = -0.5f * this.framescale + 2f * this.pad;
      vector2.X = 0.0f;
      this.body.Location = vector2;
      this.body.Location.Y += 0.5f * this.body.GetHeightOfOneLine();
      this.button.vLocation = new Vector2();
      this.button.vLocation.Y = (float) (0.5 * (double) this.framescale.Y - 0.5 * (double) this.button.GetSize_True().Y - 1.0 * (double) this.pad.Y);
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCriticalChoicePick(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return this.button.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawCriticalChoicePick(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if (GameFlags.IsUsingController && this.ControllerSelected)
        this.button.MouseOver = true;
      this.button.DrawTextButton(offset, 1f, spritebatch, GameFlags.IsUsingController && !this.ControllerSelected);
      this.body.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
