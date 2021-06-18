// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.Column4_Stock
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class Column4_Stock
  {
    private TopTextMini toptextmini;
    private TopTextMini MidText;
    public Vector2 Location;
    private AnimalFoodType animalfood;

    public Column4_Stock(
      float HeightForText,
      float HeightForMiddleText,
      AnimalFoodType _animalfood,
      Player player,
      float BaseScale)
    {
      this.animalfood = _animalfood;
      this.toptextmini = new TopTextMini("Stock", BaseScale, HeightForText);
      this.MidText = new TopTextMini("96 Days", BaseScale, HeightForMiddleText);
      this.MidText.SetAsSplit();
    }

    public void SetUpStock(Player player, bool DoFullSetUp)
    {
      if (DoFullSetUp)
        player.prisonlayout.SetUpAllStock(player);
      this.MidText.SetNewText(player.livestats.stocktimes.GetDays(this.animalfood).ToString() + " days");
      this.MidText.SetAsSplit();
    }

    public void DrawColumn4_Stock(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.MidText.DrawTopTextMini(Offset);
    }
  }
}
