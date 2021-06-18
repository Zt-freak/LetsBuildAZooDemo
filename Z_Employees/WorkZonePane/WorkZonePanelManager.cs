// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.WorkZonePane.WorkZonePanelManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Employees.WorkZonePane
{
  internal class WorkZonePanelManager
  {
    private MiniHeading_V2 miniheading;
    public Vector2 Location;
    private CustomerFrame InnerBrownHole;
    private SimpleTextHandler text;
    private TextButton textbutton;
    private CheckBoxWithString checkboxwithstring;
    public WorkZoneInfo workzoneinfo;

    public WorkZonePanelManager(
      float BaseScale,
      WorkZoneInfo _workzoneinfo,
      float EdgeBuffer,
      TILETYPE buildingtype = TILETYPE.Count,
      float SubWidth = 280f)
    {
      this.workzoneinfo = _workzoneinfo;
      this.miniheading = new MiniHeading_V2("BAL:LLLLZ", BaseScale * 2f);
      Vector2 _VSCale = new Vector2(SubWidth, 140f * Sengine.ScreenRatioUpwardsMultiplier.Y) * BaseScale;
      this.textbutton = new TextButton(BaseScale, "Select Zone", 90f);
      this.InnerBrownHole = new CustomerFrame(_VSCale, BaseScale: BaseScale);
      this.textbutton.vLocation.Y = (float) ((double) this.textbutton.GetSize().Y * -0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y + (double) this.InnerBrownHole.VSCale.Y * 0.5);
      this.textbutton.vLocation.Y -= EdgeBuffer * BaseScale;
      string TextToWrite = "Assign a work area for this employee, this focuses them more, and allows you to better manage what happens in your zoo";
      if (this.workzoneinfo.workzonetype == WorkZoneType.SingleZone)
        TextToWrite = "This employee has an assigned work zone";
      else if (this.workzoneinfo.workzonetype == WorkZoneType.Painted)
      {
        TextToWrite = "Paint the paths and areas you wish this employee to work in";
        if (this.workzoneinfo.Paintzones.Count > 1)
          TextToWrite = string.Format("There are {0} enclosures assigned to this employee", (object) this.workzoneinfo.workzones.Count);
      }
      else if (this.workzoneinfo.workzonetype == WorkZoneType.Pens)
      {
        TextToWrite = "There is an enclosure assigned to this employee";
        if (this.workzoneinfo.workzones.Count > 1)
          TextToWrite = string.Format("There are {0} enclosures assigned to this employee", (object) this.workzoneinfo.workzones.Count);
        if (buildingtype == TILETYPE.MeatProcessor)
        {
          TextToWrite = "There are no pens assigned to this processing plant, as a result dead animals will be collected at random.";
          if (this.workzoneinfo.PenUIDs.Count > 1)
            TextToWrite = string.Format("there are {0} Collection areas for this meat processing plant", (object) this.workzoneinfo.PenUIDs.Count);
        }
      }
      this.checkboxwithstring = new CheckBoxWithString("Enabled", true, BaseScale, true);
      this.checkboxwithstring.Location.Y = 0.0f;
      this.text = new SimpleTextHandler(TextToWrite, true, 0.25f * BaseScale, BaseScale);
      this.text.AutoCompleteParagraph();
      this.text.Location.Y = this.InnerBrownHole.VSCale.Y * -0.5f;
      this.text.Location.Y += EdgeBuffer * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public Vector2 GetSize() => this.InnerBrownHole.VSCale;

    public void UpdateWorkZonePanelManager(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      out bool EditZone)
    {
      EditZone = false;
      this.checkboxwithstring.UpdateCheckBoxWithString(player, Offset);
      if (!this.textbutton.UpdateTextButton(player, Offset, DeltaTime))
        return;
      EditZone = true;
    }

    public void DrawWorkZonePanelManager(Vector2 Offset)
    {
      Offset += this.Location;
      this.InnerBrownHole.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      this.textbutton.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.checkboxwithstring.DrawCheckBoxWithString(Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
