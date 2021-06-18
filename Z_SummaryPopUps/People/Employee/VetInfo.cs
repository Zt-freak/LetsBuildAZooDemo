// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.VetInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class VetInfo : EmployeeInfo
  {
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private ZGenericText seentext;
    private ZGenericText curedtext;
    private ZGenericText diseasestext;
    private ZGenericText injuriestext;

    public VetInfo(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.seentext = new ZGenericText("Animals Seen: ???", this.basescale, false);
      this.curedtext = new ZGenericText("Animals Cured: ???", this.basescale, false);
      this.diseasestext = new ZGenericText("Diseases Discovered: ???", this.basescale, false);
      this.injuriestext = new ZGenericText("Injuries Cured: ???", this.basescale, false);
      this.frame = new CustomerFrame(Vector2.Zero, BaseScale: this.basescale);
      this.frame.AddMiniHeading("Vet Info");
      this.framescale = defaultBuffer;
      this.framescale.X += this.scalehelper.ScaleX(190f);
      this.framescale.Y += this.frame.GetMiniHeadingHeight();
      this.framescale.Y += 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.seentext.GetSize().Y;
      this.framescale.Y += this.curedtext.GetSize().Y;
      this.framescale.Y += this.diseasestext.GetSize().Y;
      this.framescale.Y += this.injuriestext.GetSize().Y;
      this.frame.Resize(this.framescale);
      Vector2 vector2_1 = new Vector2();
      Vector2 vector2_2 = -0.5f * this.framescale + defaultBuffer;
      vector2_2.Y += this.frame.GetMiniHeadingHeight(false);
      vector2_2.Y += 0.5f * defaultBuffer.Y;
      this.seentext.vLocation = vector2_2;
      vector2_2.Y += this.seentext.GetSize().Y;
      this.curedtext.vLocation = vector2_2;
      vector2_2.Y += this.curedtext.GetSize().Y;
      this.injuriestext.vLocation = vector2_2;
      vector2_2.Y += this.injuriestext.GetSize().Y;
      this.diseasestext.vLocation = vector2_2;
      vector2_2.Y += this.diseasestext.GetSize().Y;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateEmployeeInfo(Player player, Vector2 offset, float DeltaTime) => false;

    public override void DrawEmployeeInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.seentext.DrawZGenericText(offset, spritebatch);
      this.curedtext.DrawZGenericText(offset, spritebatch);
      this.diseasestext.DrawZGenericText(offset, spritebatch);
      this.injuriestext.DrawZGenericText(offset, spritebatch);
    }
  }
}
