// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.SeveranceInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Employees.Emp_Summary
{
  internal class SeveranceInfoPanel
  {
    public CustomerFrame customerFrame;
    private SimpleTextHandler leftWagesText;
    private ZGenericText wagesText;
    private ZGenericText severanceText;
    private List<SeverenceOptionIcon> severenceIcons;
    public Vector2 location;
    private float textMargin = 10f;

    public SeveranceInfoPanel(int OwedUntilnow, int Severence, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 _VSCale = Vector2.Zero + defaultBuffer;
      this.leftWagesText = new SimpleTextHandler("Owed Wages:~Severence:", uiScaleHelper.ScaleX(500f), _Scale: BaseScale, AutoComplete: true);
      this.leftWagesText.SetAllColours(ColourData.Z_Cream);
      this.leftWagesText.Location = _VSCale;
      this.leftWagesText.AutoCompleteParagraph();
      this.wagesText = new ZGenericText("$" + (object) OwedUntilnow, BaseScale, false, true);
      this.severanceText = new ZGenericText("$" + (object) Severence, BaseScale, false, true);
      _VSCale.Y += this.leftWagesText.GetHeightOfParagraph();
      _VSCale.Y += defaultBuffer.Y;
      this.severenceIcons = new List<SeverenceOptionIcon>();
      for (int index = 0; index < 3; ++index)
      {
        SeverenceOptionIcon severenceOptionIcon = new SeverenceOptionIcon((SeverenceOption) index, BaseScale);
        severenceOptionIcon.location = _VSCale;
        severenceOptionIcon.location += severenceOptionIcon.GetSize() * 0.5f;
        _VSCale.X += severenceOptionIcon.GetSize().X;
        if (index != 2)
          _VSCale.X += defaultBuffer.X;
        this.severenceIcons.Add(severenceOptionIcon);
      }
      _VSCale.Y += this.severenceIcons[0].GetSize().Y;
      _VSCale.Y += defaultBuffer.Y;
      this.wagesText.vLocation.X = _VSCale.X;
      this.wagesText.vLocation.Y = this.leftWagesText.Location.Y;
      this.severanceText.vLocation.X = _VSCale.X;
      this.severanceText.vLocation.Y = this.wagesText.vLocation.Y + this.wagesText.GetSize().Y;
      _VSCale.X += defaultBuffer.X;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.leftWagesText.Location += vector2;
      ZGenericText wagesText = this.wagesText;
      wagesText.vLocation = wagesText.vLocation + vector2;
      ZGenericText severanceText = this.severanceText;
      severanceText.vLocation = severanceText.vLocation + vector2;
      for (int index = 0; index < this.severenceIcons.Count; ++index)
        this.severenceIcons[index].location += vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public SeverenceOption UpdateSeveranceInfoPanel(
      Player player,
      Vector2 offset,
      int OwedThisMuch,
      float DeltaTime)
    {
      offset += this.location;
      this.wagesText.textToWrite = "$" + (object) OwedThisMuch;
      for (int index = 0; index < this.severenceIcons.Count; ++index)
      {
        if (this.severenceIcons[index].UpdateSeverenceOptionIcon(player, offset, DeltaTime))
          return this.severenceIcons[index].option;
      }
      return SeverenceOption.Count;
    }

    public void DrawSeveranceInfoPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      for (int index = 0; index < this.severenceIcons.Count; ++index)
        this.severenceIcons[index].DrawSeverenceOptionIcon(spriteBatch, offset);
      this.leftWagesText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.wagesText.DrawZGenericText(offset, spriteBatch);
      this.severanceText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
