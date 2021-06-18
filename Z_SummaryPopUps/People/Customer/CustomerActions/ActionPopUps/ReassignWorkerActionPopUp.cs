// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.ReassignWorkerActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class ReassignWorkerActionPopUp : CustomerActionPopUp
  {
    private HealthInspectorInfo healthInspectorInfo;
    private SimpleTextHandler para;

    public ReassignWorkerActionPopUp(WalkingPerson walkingPerson, float BaseScale)
      : base(BaseScale)
    {
      this.hidemainpanel = true;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      CustomerType customertype = walkingPerson.simperson.customertype;
      string empty = string.Empty;
      if (customertype == CustomerType.HealthInspector)
      {
        this.healthInspectorInfo = new HealthInspectorInfo(walkingPerson, this.basescale);
        this.healthInspectorInfo.location.Y = this.healthInspectorInfo.GetSize().Y * 0.5f;
        zero += this.healthInspectorInfo.GetSize();
      }
      if (!string.IsNullOrEmpty(empty))
      {
        this.para = new SimpleTextHandler(empty, zero.X, _Scale: this.basescale, AutoComplete: true);
        this.para.SetAllColours(ColourData.Z_Cream);
        this.para.Location.Y = zero.Y;
        zero.Y += this.para.GetHeightOfParagraph();
      }
      this.framescale = zero;
      this.SizeFrame();
      Vector2 vector2 = -this.framescale * 0.5f;
      this.healthInspectorInfo.location.Y += vector2.Y;
      if (this.para != null)
        this.para.Location += vector2;
      this.SetUpHeatMap();
    }

    private void SetUpHeatMap() => Z_GameFlags.SetHeatMapType(HeatMapType.Hygiene);

    public override void OnPanelClosed() => Z_GameFlags.SetHeatMapType(HeatMapType.None);

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime) => false;

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      if (this.healthInspectorInfo != null)
        this.healthInspectorInfo.DrawHealthInspectorInfo(offset, spritebatch);
      if (this.para == null)
        return;
      this.para.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
