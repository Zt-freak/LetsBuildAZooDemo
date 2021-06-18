// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.StrikeListEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People
{
  internal class StrikeListEntry : ScrollingListEntry
  {
    private ZGenericText employeetxt;
    private ZGenericText revenuetxt;

    public StrikeListEntry(string employeestr, int revenue, float basescale_)
      : base(basescale_)
    {
      this.employeetxt = new ZGenericText(employeestr, this.basescale, false);
      this.revenuetxt = new ZGenericText("$" + (object) revenue, this.basescale, false, true);
      float num = this.scalehelper.ScaleX(370f);
      this.framescale.Y = this.employeetxt.GetSize().Y;
      this.framescale.X = num;
      this.framescale = this.framescale + 1f * this.pad;
      this.frame = new CustomerFrame(this.framescale, this.darkerframe, this.basescale);
      Vector2 vector2 = -0.5f * this.framescale + 0.5f * this.pad;
      this.employeetxt.vLocation = vector2;
      vector2.X = 0.5f * this.framescale.X - this.pad.X;
      this.revenuetxt.vLocation = vector2;
    }

    public override bool UpdateScrollingListEntry(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public override void DrawScrollingListEntry(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.employeetxt.DrawZGenericText(offset, spritebatch);
      this.revenuetxt.DrawZGenericText(offset, spritebatch);
    }
  }
}
