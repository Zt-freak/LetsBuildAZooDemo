// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ZGenericChecklistItem
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI
{
  internal class ZGenericChecklistItem
  {
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private Vector2 pad;
    private CustomerFrame frame;
    public Vector2 location;
    private ActiveIcon icon;
    private SimpleTextHandler texthandler;
    private bool iscomplete;
    private string text;
    private ZGenericText number;
    private bool useNumbers;
    private bool useSpringfont1point5;

    public bool IsComplete => this.iscomplete;

    public void SetTextColour(Vector3 colour)
    {
      this.texthandler.SetAllColours(colour);
      this.number.SetAllColours(colour);
    }

    public ZGenericChecklistItem(
      string text_,
      bool iscomplete_,
      float basescale_,
      bool useSpringfont1point5_ = false,
      int useThisNum = -1)
    {
      this.iscomplete = iscomplete_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.framescale = new Vector2();
      this.useSpringfont1point5 = useSpringfont1point5_;
      this.useNumbers = (double) useThisNum >= 0.0;
      this.text = text_;
      this.icon = new ActiveIcon(this.iscomplete, 0.4f * this.basescale);
      this.number = new ZGenericText(useThisNum.ToString() + ".", this.basescale, _UseOnePointFiveFont: this.useSpringfont1point5);
      this.texthandler = new SimpleTextHandler(text_, this.uiscale.ScaleX(240f), _Scale: this.basescale, _UseFontOnePointFive: this.useSpringfont1point5);
      this.texthandler.SetAllColours(ColourData.Z_Cream);
      this.texthandler.AutoCompleteParagraph();
      this.framescale = this.texthandler.GetSize();
      this.framescale.X += this.icon.GetSize().X + 0.5f * this.pad.X;
      this.framescale.Y = Math.Max(this.framescale.Y, this.icon.GetSize().Y);
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2 = -0.5f * this.framescale;
      this.icon.vLocation = vector2 + 0.5f * this.icon.GetSize();
      vector2.X += this.icon.GetSize().X;
      this.number.vLocation = this.icon.vLocation;
      vector2.X += 0.5f * this.pad.X;
      this.texthandler.Location = vector2;
      this.texthandler.Location.Y += (float) (0.5 * (double) this.icon.GetSize().Y - 0.5 * (double) this.texthandler.GetHeightOfOneLine());
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateZGenericChecklistItem(float DeltaTime) => false;

    public void DrawZGenericChecklistItem(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      if (this.useNumbers)
        this.number.DrawZGenericText(offset, spritebatch);
      else
        this.icon.DrawActiveIcon(spritebatch, offset);
      this.texthandler.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
