// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.SeverenceOptionIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Employees.Emp_Summary
{
  internal class SeverenceOptionIcon
  {
    private CustomerFrame customerFrame;
    private CustomerFrame selectionHighlightFrame;
    private GameObject icon;
    public SeverenceOption option;
    private SimpleTextHandler text;
    public Vector2 location;
    private bool UseCreamFrame = true;
    private bool Selected;

    public SeverenceOptionIcon(SeverenceOption _option, float BaseScale, bool hasText = true)
    {
      this.option = _option;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.Y += defaultBuffer.Y;
      this.icon = new GameObject();
      this.icon.DrawRect = new Rectangle(413, 199, 18, 22);
      this.icon.scale = BaseScale * 2f;
      switch (this.option)
      {
        case SeverenceOption.PayNothing:
          this.icon.DrawRect = new Rectangle(413, 199, 18, 22);
          break;
        case SeverenceOption.PayWagesOnly:
          this.icon.DrawRect = new Rectangle(432, 199, 17, 22);
          break;
        case SeverenceOption.PayAll:
          this.icon.DrawRect = new Rectangle(450, 200, 19, 21);
          break;
      }
      this.icon.SetDrawOriginToCentre();
      Vector2 vector2_1 = new Vector2(19f, 22f) * this.icon.scale * Sengine.ScreenRatioUpwardsMultiplier;
      this.icon.vLocation.Y = zero.Y;
      this.icon.vLocation.Y += vector2_1.Y * 0.5f;
      Vector2 _VSCale = zero + vector2_1;
      _VSCale.Y += defaultBuffer.Y;
      if (hasText)
      {
        this.text = new SimpleTextHandler(FireEmployeePanel.GetSevernceOptionToString(_option), _VSCale.X, true, BaseScale, AutoComplete: true);
        this.text.AutoCompleteParagraph();
        this.text.SetAllColours(ColourData.Z_Cream);
        if (this.UseCreamFrame)
          this.text.SetAllColours(ColourData.Z_TextBrown);
        this.text.Location.Y = _VSCale.Y;
        this.text.Location.Y += this.text.GetHeightOfOneLine() * 0.5f;
        _VSCale.Y += this.text.GetHeightOfParagraph();
        _VSCale.Y += defaultBuffer.Y * 0.5f;
      }
      _VSCale.X += defaultBuffer.X * 2f;
      Vector3 color = ColourData.Z_FrameMidBrown;
      if (this.UseCreamFrame)
        color = new Vector3(192f, 181f, 156f) / (float) byte.MaxValue;
      this.customerFrame = new CustomerFrame(_VSCale, color, BaseScale);
      this.selectionHighlightFrame = new CustomerFrame(_VSCale, ColourData.Z_TextOrange, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      if (this.text != null)
        this.text.Location.Y += vector2_2.Y;
      this.icon.vLocation.Y += vector2_2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateSeverenceOptionIcon(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return this.customerFrame.UpdateForMouseOverAndClick(player, DeltaTime, offset, out this.Selected);
    }

    public void DrawSeverenceOptionIcon(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      if (this.Selected)
        this.selectionHighlightFrame.DrawCustomerFrame(offset, spriteBatch);
      else
        this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.icon.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      if (this.text == null)
        return;
      this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
