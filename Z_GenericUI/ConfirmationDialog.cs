// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ConfirmationDialog
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI
{
  internal class ConfirmationDialog
  {
    private static float rawTextX = 180f;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private BigBrownPanel panel;
    private CustomerFrame frame;
    private Vector2 framescale;
    private string textstr;
    private SimpleTextHandler text;
    private TextButton yesbutton;
    private TextButton nobutton;
    private Vector2 textsize;
    private bool disabled;

    public ConfirmationDialog(
      string heading,
      string bodytext,
      float basescale_,
      bool disabled_ = false,
      bool IncludeBrownPanel = true,
      float customTextX_raw = -1f)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      float num = this.uiscale.ScaleX(ConfirmationDialog.rawTextX);
      if ((double) customTextX_raw != -1.0)
        num = this.uiscale.ScaleX(customTextX_raw);
      this.textstr = bodytext;
      this.text = new SimpleTextHandler(this.textstr, false, num / Sengine.ReferenceScreenRes.X, this.basescale, false, false);
      this.text.AutoCompleteParagraph();
      this.text.SetAllColours(ColourData.Z_Cream);
      this.yesbutton = new TextButton(this.basescale, "Yes");
      this.nobutton = new TextButton(this.basescale, "No");
      this.framescale = 2f * defaultBuffer;
      this.framescale.X += num;
      this.framescale.Y += this.text.GetHeightOfParagraph() + 1f * defaultBuffer.Y;
      this.framescale.Y += this.yesbutton.GetSize_True().Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      if (IncludeBrownPanel)
      {
        this.panel = new BigBrownPanel(this.framescale, addHeaderText: heading, _BaseScale: this.basescale);
        this.panel.Finalize(this.framescale);
      }
      this.disabled = disabled_;
      if (disabled_)
      {
        this.yesbutton.SetButtonColour(BTNColour.Grey);
        this.frame.SetDullAlertRed();
      }
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      this.text.Location = vector2;
      vector2.Y += this.text.GetHeightOfParagraph() + defaultBuffer.Y;
      this.yesbutton.vLocation.X = (float) (-0.5 * (double) this.yesbutton.GetSize_True().X - 1.5 * (double) defaultBuffer.X);
      this.yesbutton.vLocation.Y = vector2.Y + 0.5f * this.yesbutton.GetSize_True().Y;
      this.nobutton.vLocation.X = (float) (0.5 * (double) this.yesbutton.GetSize_True().X + 1.5 * (double) defaultBuffer.X);
      this.nobutton.vLocation.Y = vector2.Y + 0.5f * this.nobutton.GetSize_True().Y;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.panel != null && this.panel.CheckMouseOver(player, offset) || this.frame != null && this.frame.CheckMouseOver(player, offset);
    }

    public Vector2 GetSizeOfContentsFrame() => this.frame.VSCale;

    public void SetDisabled(bool disabled_)
    {
      this.disabled = disabled_;
      if (!this.disabled)
      {
        this.yesbutton.SetButtonColour(BTNColour.Green);
        this.frame.ResetColor();
      }
      else
      {
        this.yesbutton.SetButtonColour(BTNColour.Grey);
        this.frame.SetDullAlertRed();
      }
    }

    public bool UpdateConfirmationDialog(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool confirmed)
    {
      bool flag1 = false;
      if (this.panel != null)
      {
        this.panel.UpdatePanelCloseButton(player, DeltaTime, offset + this.location);
        this.panel.UpdateDragger(player, ref this.location, DeltaTime, offset);
      }
      if (!this.disabled)
        flag1 = this.yesbutton.UpdateTextButton(player, offset + this.location, DeltaTime);
      bool flag2 = this.nobutton.UpdateTextButton(player, offset + this.location, DeltaTime);
      confirmed = flag1;
      return flag1 | flag2;
    }

    public void DrawConfirmationDialog(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      if (this.panel != null)
        this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.yesbutton.DrawTextButton(offset, 1f, spritebatch);
      this.nobutton.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
