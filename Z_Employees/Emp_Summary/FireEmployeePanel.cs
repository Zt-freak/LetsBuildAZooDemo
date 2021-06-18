// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.FireEmployeePanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Employees.Emp_Summary
{
  internal class FireEmployeePanel
  {
    private BigBrownPanel brownPanel;
    private EmployeeSummaryPanel employeePanel;
    private SimpleTextHandler text;
    public Vector2 location;
    private SeveranceInfoPanel severencePanel;

    public FireEmployeePanel(
      QuickEmployeeDescription newemployee,
      bool HasCancelOption,
      int Severence,
      int OwedUntilnow,
      float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.brownPanel = new BigBrownPanel(Vector2.Zero, HasCancelOption, "Fire", BaseScale);
      Vector2 zero = Vector2.Zero;
      this.employeePanel = new EmployeeSummaryPanel(newemployee, false, false, BaseScale);
      this.severencePanel = new SeveranceInfoPanel(OwedUntilnow, Severence, BaseScale);
      zero.X = Math.Max(this.employeePanel.GetSize().X, this.severencePanel.GetSize().X);
      this.text = new SimpleTextHandler("This employee will be fired.~Select how you would like to pay this employee.", zero.X, true, BaseScale, AutoComplete: true);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.employeePanel.location.Y = zero.Y;
      this.employeePanel.location.Y += this.employeePanel.GetSize().Y * 0.5f;
      zero.Y += this.employeePanel.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.text.Location.Y = zero.Y;
      this.text.Location.Y += this.text.GetHeightOfOneLine() * 0.5f;
      zero.Y += this.text.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y;
      this.severencePanel.location.Y = zero.Y;
      this.severencePanel.location.Y += this.severencePanel.GetSize().Y * 0.5f;
      zero.Y += this.severencePanel.GetSize().Y;
      this.brownPanel.Finalize(zero);
      Vector2 frameOffsetFromTop = this.brownPanel.GetFrameOffsetFromTop();
      this.employeePanel.location.Y += frameOffsetFromTop.Y;
      this.text.Location.Y += frameOffsetFromTop.Y;
      this.severencePanel.location.Y += frameOffsetFromTop.Y;
    }

    public SeverenceOption UpdateFireEmployeePanel(
      float DeltaTime,
      Player player,
      Vector2 offset,
      int OwedThisMuch)
    {
      offset += this.location;
      this.employeePanel.UpdateEmployeeSummary(DeltaTime, player, offset);
      if (this.brownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return SeverenceOption.Cancel;
      SeverenceOption severenceOption = this.severencePanel.UpdateSeveranceInfoPanel(player, offset, OwedThisMuch, DeltaTime);
      return severenceOption != SeverenceOption.Count ? severenceOption : SeverenceOption.Count;
    }

    public static string GetSevernceOptionToString(SeverenceOption option)
    {
      switch (option)
      {
        case SeverenceOption.PayNothing:
          return "Pay~None";
        case SeverenceOption.PayWagesOnly:
          return "Pay~Owed";
        case SeverenceOption.PayAll:
          return "Pay~All";
        default:
          return "NA";
      }
    }

    public void DrawFireEmployeePanel(Vector2 offset)
    {
      offset += this.location;
      this.brownPanel.DrawBigBrownPanel(offset);
      this.employeePanel.DrawEmployeeSummary(offset, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(offset, 1f, AssetContainer.pointspritebatchTop05);
      this.severencePanel.DrawSeveranceInfoPanel(offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
