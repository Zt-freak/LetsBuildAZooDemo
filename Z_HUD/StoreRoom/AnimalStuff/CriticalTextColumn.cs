// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.CriticalTextColumn
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff
{
  internal class CriticalTextColumn
  {
    public Vector2 location;
    private ZGenericText CriticalHeader;
    private ZGenericText CriticalIngredient;
    private ZGenericText EatingPerDay;
    private Vector2 size;

    public CriticalTextColumn(TempAnimalInfo tempanimalinfo, Player player, float BaseScale)
    {
      this.CriticalHeader = new ZGenericText("Critical Food Item: ", BaseScale, false);
      this.CriticalIngredient = new ZGenericText(AnimalFoodData.GetAnimalFoodTypeToString(tempanimalinfo.CriticalFood), BaseScale, false, _UseOnePointFiveFont: true);
      this.EatingPerDay = new ZGenericText(string.Format("Eating {0}/day", (object) Math.Round((double) FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[(int) tempanimalinfo.CriticalFood], 2)), BaseScale, false);
      this.size = Vector2.Zero;
      this.size.Y += this.CriticalHeader.GetSize().Y;
      this.CriticalIngredient.vLocation.Y = this.size.Y;
      this.size.Y += this.CriticalIngredient.GetSize().Y;
      this.EatingPerDay.vLocation.Y = this.size.Y;
      this.size.Y += this.EatingPerDay.GetSize().Y;
      this.size.X = Math.Max(this.CriticalHeader.GetSize().X, this.EatingPerDay.GetSize().X);
      this.size.X = Math.Max(this.size.X, this.CriticalIngredient.GetSize().X);
    }

    public Vector2 GetSize() => this.size;

    public void UpdateCriticalTextColumn()
    {
    }

    public void DrawCriticalTextColumn(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.CriticalHeader.DrawZGenericText(offset, spriteBatch);
      this.CriticalIngredient.DrawZGenericText(offset, spriteBatch);
      this.EatingPerDay.DrawZGenericText(offset, spriteBatch);
    }
  }
}
