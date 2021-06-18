// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Processing.Summary.ProcessingSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Processing;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Processing.Summary
{
  internal class ProcessingSummary
  {
    public Vector2 Location;
    private CustomerFrame Cframe;
    private SimpleTextHandler textbox;
    private MiniHeading miniHeading;

    public ProcessingSummary(
      float TargetWidth,
      float BaseScale,
      int BuildingDisplayIndex,
      ProcessingBuilding processingbuilding)
    {
      this.Cframe = new CustomerFrame(new Vector2(TargetWidth * BaseScale, 80f * BaseScale), BaseScale: BaseScale);
      string str = processingbuilding.storeddeadthings.Count != 0 ? "This facility is currently storing " + (object) processingbuilding.storeddeadthings.Count + " deceased animals for processing." : "This facility is currently storing no deceased animals for processing.";
      this.textbox = new SimpleTextHandler(processingbuilding.ActiveProcesses.Count != 0 ? str + "~~This facility is currently using 1/3 processing" : str + "~~This facility can process 2 animals simultaniously", true, 0.4f * BaseScale, BaseScale);
      this.textbox.AutoCompleteParagraph();
      this.miniHeading = new MiniHeading(this.Cframe.VSCale, "Summary for Processing Plant: " + (object) BuildingDisplayIndex, 1f, BaseScale);
    }

    public void UpdateProcessingSummary()
    {
    }

    public void DrawProcessingSummary(Vector2 Offset)
    {
      Offset += this.Location;
      this.Cframe.DrawCustomerFrame(Offset, AssetContainer.pointspritebatch03);
      this.textbox.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatch03);
      this.miniHeading.DrawMiniHeading(Offset);
    }
  }
}
