// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.GeneralEmployees.NotHere.NotHereCollection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Employees.GeneralEmployees.NotHere
{
  internal class NotHereCollection
  {
    public Vector2 location;
    private LocationSummary locationsummary;
    private NotHereManager notheremanager;
    private Vector2 size;

    public NotHereCollection(
      Player player,
      EmployeeType employeetype,
      float BaseScale,
      float UnmultipliedWidth,
      AnimalInFrame animalinframe)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      List<TILETYPE> emplyeeTypeToBuilding = EmployeeData.GetEmplyeeTypeToBuilding(employeetype);
      int num = 0;
      if (emplyeeTypeToBuilding.Contains(TILETYPE.StoreRoom))
      {
        if (player.storerooms.HasBuiltStoreRoom)
          num = 1;
      }
      else
      {
        for (int index = 0; index < emplyeeTypeToBuilding.Count; ++index)
          num += player.shopstatus.GetTotalOfThese(emplyeeTypeToBuilding[index]);
      }
      if (!Z_DebugFlags.IsBetaVersion)
        this.locationsummary = new LocationSummary(employeetype, player, UnmultipliedWidth, BaseScale, num);
      this.notheremanager = new NotHereManager(player, employeetype, BaseScale, UnmultipliedWidth, animalinframe, num);
      this.size = Vector2.Zero;
      this.notheremanager.Location.Y += this.notheremanager.GetSize().Y * 0.5f;
      this.size.Y += this.notheremanager.GetSize().Y;
      this.size.Y += uiScaleHelper.DefaultBuffer.Y;
      if (this.locationsummary == null)
        return;
      this.locationsummary.Location.Y = this.size.Y;
      this.locationsummary.Location.Y += this.locationsummary.GetSize().Y * 0.5f;
      this.size.Y += this.locationsummary.GetSize().Y;
      this.size.X += this.locationsummary.GetSize().X;
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateNotHereCollection(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.location;
      if (this.locationsummary != null)
        this.locationsummary.UpdateLocationSummary(Offset, player, DeltaTime);
      return this.notheremanager.UpdateNotHereManager(player, DeltaTime, Offset);
    }

    public void DrawNotHereCollection(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      this.notheremanager.DrawNotHereManager(Offset, spritebatch);
      if (this.locationsummary == null)
        return;
      this.locationsummary.DrawLocationSummary(Offset, spritebatch);
    }
  }
}
