// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VisitSummary.VetCoverageSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VisitSummary
{
  internal class VetCoverageSummary
  {
    private CustomerFrame frame;
    private MiniHeading miniheading;
    private SimpleTextHandler texthandler;
    public Vector2 Location;
    private SatisfactionBar satisfaction;

    public VetCoverageSummary(float BaseScale)
    {
      this.frame = new CustomerFrame(new Vector2(300f * BaseScale, 100f * BaseScale), true, BaseScale);
      this.miniheading = new MiniHeading(this.frame.VSCale, "Vet COverage 50%", 1f, BaseScale);
      this.satisfaction = new SatisfactionBar(0.5f, BaseScale);
      this.texthandler = new SimpleTextHandler("You have one vet, it takes him 4 days to visit his assigned anaimals", 200f * BaseScale, true, BaseScale, AutoComplete: true);
      this.texthandler.SetAllColours(ColourData.Z_Cream);
      this.satisfaction.vLocation.Y = 40f * BaseScale;
    }

    public Vector2 GetSize() => this.frame.VSCale;

    public void DrawVetCoverageSummary(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.frame.DrawCustomerFrame(Offset, spritebatch);
      this.miniheading.DrawMiniHeading(Offset + this.frame.location);
      this.texthandler.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.satisfaction.DrawSatisfactionBar(Offset, spritebatch);
    }
  }
}
