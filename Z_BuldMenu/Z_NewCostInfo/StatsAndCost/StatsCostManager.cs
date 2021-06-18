// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.StatsCostManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;
using TinyZoo.Z_OverWorld;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class StatsCostManager
  {
    private BuildingCost buildingcost;
    private EmployeesAndCost employeecost;
    private SaleItemDetails saleitems;
    private BuildingMoralityRequirement moralityRequirement;
    private NotUnlockedPanel notUnlockedPanel;
    private HeadingBlock headingblock;
    private float FullHeight;
    private float BAseWidth;

    public StatsCostManager(
      TILETYPE buildingtype,
      float BaseScale,
      Player player,
      bool CanPlayerBuildThis)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.BAseWidth = 220f * BaseScale;
      this.headingblock = CanPlayerBuildThis ? new HeadingBlock(this.BAseWidth, TileData.GetTileStats(buildingtype).Name, CategoryData.GetCategoryDescription(buildingtype), BaseScale) : new HeadingBlock(this.BAseWidth, "Unknown", "More information about this will be available once it has been unlocked.", BaseScale);
      this.FullHeight = this.headingblock.GetSize().Y;
      this.headingblock.Location.Y += this.FullHeight * 0.5f;
      this.FullHeight += defaultBuffer.Y;
      if (CanPlayerBuildThis)
      {
        this.buildingcost = new BuildingCost(buildingtype, player, BaseScale, this.BAseWidth + BaseScale * 20f);
        float y1 = this.buildingcost.GetSize().Y;
        this.buildingcost.Location.Y = this.FullHeight;
        this.buildingcost.Location.Y += y1 * 0.5f;
        this.FullHeight += y1;
        int tileTypeToCetagory = (int) CategoryData.GetTileTypeToCetagory(buildingtype, out POINT_OF_INTEREST _);
        if (EmployeeData.GetBuildingWithEmployees().Contains(buildingtype))
          this.employeecost = new EmployeesAndCost(buildingtype, BaseScale, false);
        else if (buildingtype == TILETYPE.DrinksVendingMachine || buildingtype == TILETYPE.SnacksVendingMachine || buildingtype == TILETYPE.ChocolateVendingMachine)
          this.employeecost = new EmployeesAndCost(buildingtype, BaseScale, true);
        if (this.employeecost != null)
        {
          this.FullHeight += defaultBuffer.Y;
          this.employeecost.Location.Y = this.FullHeight;
          float y2 = this.employeecost.GetSize().Y;
          this.employeecost.Location.Y += y2 * 0.5f;
          this.FullHeight += y2;
        }
        if (tileTypeToCetagory == 1)
        {
          this.FullHeight += defaultBuffer.Y;
          this.saleitems = new SaleItemDetails(buildingtype, player, BaseScale, this.BAseWidth);
          this.saleitems.Location.Y = this.FullHeight;
          this.saleitems.Location.Y += this.saleitems.getSize().Y * 0.5f;
          this.FullHeight += this.saleitems.getSize().Y;
        }
        if (MoralityUnlocksData.IsAMoralityBuilding(buildingtype))
        {
          this.FullHeight += defaultBuffer.Y;
          this.moralityRequirement = new BuildingMoralityRequirement(buildingtype, player, BaseScale, this.BAseWidth + defaultBuffer.X * 2f);
          this.moralityRequirement.location.Y = this.FullHeight;
          this.moralityRequirement.location.Y += this.moralityRequirement.GetSize().Y * 0.5f;
          this.FullHeight += this.moralityRequirement.GetSize().Y;
        }
      }
      else
      {
        this.notUnlockedPanel = new NotUnlockedPanel(BaseScale, this.BAseWidth + defaultBuffer.X * 2f, player, buildingtype);
        this.notUnlockedPanel.location.Y = this.FullHeight;
        this.notUnlockedPanel.location.Y += this.notUnlockedPanel.GetSize().Y * 0.5f;
        this.FullHeight += this.notUnlockedPanel.GetSize().Y;
      }
      if (this.buildingcost != null)
        this.buildingcost.Location.Y -= this.FullHeight * 0.5f;
      if (this.headingblock != null)
        this.headingblock.Location.Y -= this.FullHeight * 0.5f;
      if (this.employeecost != null)
        this.employeecost.Location.Y -= this.FullHeight * 0.5f;
      if (this.saleitems != null)
        this.saleitems.Location.Y -= this.FullHeight * 0.5f;
      if (this.moralityRequirement != null)
        this.moralityRequirement.location.Y -= this.FullHeight * 0.5f;
      if (this.notUnlockedPanel != null)
        this.notUnlockedPanel.location.Y -= this.FullHeight * 0.5f;
      this.BAseWidth += BaseScale * 20f;
    }

    public bool GetCanAfford() => this.buildingcost != null && this.buildingcost.costmanager.moneybox.CanAfford;

    public Vector2 GetSize() => new Vector2(this.BAseWidth, this.FullHeight);

    public void UpdateStatsCostManager(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      int TileCount,
      Z_PenBuilder z_penbuilder)
    {
      if (this.buildingcost != null)
        this.buildingcost.UpdateBuildingCost(player, TileCount, z_penbuilder, DeltaTime);
      if (this.notUnlockedPanel == null)
        return;
      this.notUnlockedPanel.UpdateNotUnlockedPanel(player, DeltaTime, Offset);
    }

    public void FlashRedCantAfford()
    {
      if (this.buildingcost == null)
        return;
      this.buildingcost.FlashRedCantAfford();
    }

    public void DrawStatsCostManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.headingblock.DrawHeadingBlock(Offset, spritebatch);
      if (this.buildingcost != null)
        this.buildingcost.DrawBuildingCost(Offset, spritebatch);
      if (this.employeecost != null)
        this.employeecost.DrawStatsAndCostPanel(Offset, spritebatch);
      if (this.saleitems != null)
        this.saleitems.DrawSaleItems(Offset, spritebatch);
      if (this.moralityRequirement != null)
        this.moralityRequirement.DrawBuildingMoralityRequirement(Offset, spritebatch);
      if (this.notUnlockedPanel == null)
        return;
      this.notUnlockedPanel.DrawNotUnlockedPanel(Offset, spritebatch);
    }
  }
}
