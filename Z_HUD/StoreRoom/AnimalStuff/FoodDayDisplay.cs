// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.FoodDayDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff
{
  internal class FoodDayDisplay
  {
    private AnimalFoodIcon foodicon;
    public Vector2 Location;
    private SuppliesRemainingColumn foodSuppliesColumn;
    private CriticalTextColumn crticalFoodColumn;
    private SatisfactionBar bar;

    public FoodDayDisplay(TempAnimalInfo tempanimalinfo, Player player, float BaseScale)
    {
      if (tempanimalinfo.CriticalFood == AnimalFoodType.Count)
        return;
      this.foodicon = new AnimalFoodIcon(tempanimalinfo.CriticalFood, 1f, BaseScale);
      this.bar = new SatisfactionBar((float) (int) tempanimalinfo.DaysOfFood / 30f, BaseScale * 2f, BarSIze.VerySmall);
      this.foodSuppliesColumn = new SuppliesRemainingColumn(tempanimalinfo, player, BaseScale);
      this.crticalFoodColumn = new CriticalTextColumn(tempanimalinfo, player, BaseScale);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      float num1 = 0.0f;
      this.foodSuppliesColumn.location.X = num1;
      this.foodSuppliesColumn.location.Y -= this.foodSuppliesColumn.GetSize().Y * 0.5f;
      float num2 = num1 + this.foodSuppliesColumn.GetSize().X + defaultBuffer.X * 1.5f;
      this.foodicon.vLocation.X = num2;
      this.foodicon.vLocation.X += this.foodicon.GetSize_IconOnly().X * 0.5f;
      float num3 = num2 + uiScaleHelper.ScaleX(40f);
      this.crticalFoodColumn.location.X = num3;
      this.crticalFoodColumn.location.Y -= this.crticalFoodColumn.GetSize().Y * 0.5f;
      this.bar.vLocation.X = num3 + uiScaleHelper.ScaleX(130f);
      this.bar.vLocation.X += this.bar.GetSize().X * 0.5f;
    }

    public void UpdateFoodDayDisplay()
    {
    }

    public void DrawFoodDayDisplay(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (this.foodicon == null)
        return;
      Offset += this.Location;
      this.foodicon.DrawAnimalFoodIcon(Offset, false, spritebatch);
      this.foodSuppliesColumn.DrawSuppliesRemainingColumn(Offset, spritebatch);
      this.crticalFoodColumn.DrawCriticalTextColumn(Offset, spritebatch);
      this.bar.DrawSatisfactionBar(Offset, spritebatch);
    }
  }
}
