// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.SafetyInspectorInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.SubElements;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo
{
  internal class SafetyInspectorInfo : VIPInfo
  {
    private CustomerFrame frame;
    private ZGenericText buildingRowHeader;
    private BuildingRow buildingRow;

    public SafetyInspectorInfo(WalkingPerson person, float BaseScale, float forceThisWidth)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      List<TILETYPE> buildingsTypes = new List<TILETYPE>();
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      this.frame = new CustomerFrame(Vector2.Zero, ColourData.Z_FrameMidBrown, BaseScale);
      this.frame.AddMiniHeading("Info");
      this.buildingRowHeader = new ZGenericText("Places Visited:", BaseScale, false);
      this.buildingRow = new BuildingRow(BaseScale, buildingsTypes);
      zero.X += defaultBuffer.X;
      zero.Y += this.frame.GetMiniHeadingHeight();
      zero.Y += defaultBuffer.Y;
      this.buildingRowHeader.vLocation = zero;
      zero.Y += this.buildingRowHeader.GetSize().Y;
      this.buildingRow.location = zero;
      this.buildingRow.location.Y += this.buildingRow.GetSize().Y * 0.5f;
      zero.Y += this.buildingRow.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.frame.Resize(new Vector2(forceThisWidth, zero.Y));
      Vector2 vector2 = -this.frame.VSCale * 0.5f;
      this.buildingRow.location += vector2;
      ZGenericText buildingRowHeader = this.buildingRowHeader;
      buildingRowHeader.vLocation = buildingRowHeader.vLocation + vector2;
    }

    public override Vector2 GetSize() => this.frame.VSCale;

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.buildingRowHeader.DrawZGenericText(offset, spritebatch);
      this.buildingRow.DrawBuildingRow(offset, spritebatch);
    }
  }
}
