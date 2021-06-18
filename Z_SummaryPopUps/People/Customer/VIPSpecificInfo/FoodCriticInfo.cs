// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.FoodCriticInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.SubElements;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo
{
  internal class FoodCriticInfo : VIPInfo
  {
    private CustomerFrame customerFrame;
    private ZGenericText buildingRowHeader;
    private BuildingRow buildingRow;

    public FoodCriticInfo(float BaseScale, float forceThisWidth = -1f)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Info");
      List<TILETYPE> buildingsTypes = new List<TILETYPE>();
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      buildingsTypes.Add(TILETYPE.None);
      this.buildingRowHeader = new ZGenericText("Places Visited: ", BaseScale, false);
      this.buildingRow = new BuildingRow(BaseScale, buildingsTypes);
      Vector2 zero = Vector2.Zero;
      zero.X = defaultBuffer.X;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.Y += defaultBuffer.Y * 0.5f;
      this.buildingRowHeader.vLocation = zero;
      zero.Y += this.buildingRowHeader.GetSize().Y;
      this.buildingRow.location += zero;
      this.buildingRow.location.Y += this.buildingRow.GetSize().Y * 0.5f;
      zero.Y += this.buildingRow.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      zero.X += this.buildingRow.GetSize().X;
      zero.X += defaultBuffer.X;
      Vector2 _vScale = zero;
      if ((double) forceThisWidth != -1.0)
        _vScale.X = forceThisWidth;
      this.customerFrame.Resize(_vScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.buildingRow.location += vector2;
      ZGenericText buildingRowHeader = this.buildingRowHeader;
      buildingRowHeader.vLocation = buildingRowHeader.vLocation + vector2;
    }

    public override Vector2 GetSize() => this.customerFrame.VSCale;

    public override bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime)
    {
      this.UpdateHealthInspectorInfo(player, DeltaTime, offset);
      return false;
    }

    public void UpdateHealthInspectorInfo(Player player, float DeltaTime, Vector2 offset) => offset += this.location;

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset) => this.DrawHealthInspectorInfo(offset, spritebatch);

    public void DrawHealthInspectorInfo(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.buildingRowHeader.DrawZGenericText(offset, spriteBatch);
      this.buildingRow.DrawBuildingRow(offset, spriteBatch);
    }
  }
}
