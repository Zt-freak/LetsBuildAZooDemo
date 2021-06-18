// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.SuppliesRemainingColumn
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff
{
  internal class SuppliesRemainingColumn
  {
    public Vector2 location;
    private ZGenericText SuppliesRemaining;
    private ZGenericText DaysLeftText;
    private int DaysLeft;
    private Vector2 size;

    public SuppliesRemainingColumn(TempAnimalInfo tempanimalinfo, Player player, float BaseScale)
    {
      this.DaysLeft = (int) tempanimalinfo.DaysOfFood;
      this.SuppliesRemaining = new ZGenericText("Supplies Remaining: ", BaseScale, false);
      this.DaysLeftText = new ZGenericText(this.DaysLeft.ToString() + " days", BaseScale, false, _UseOnePointFiveFont: true);
      this.size.Y += this.SuppliesRemaining.GetSize().Y;
      this.DaysLeftText.vLocation.Y = this.size.Y;
      this.size.Y += this.DaysLeftText.GetSize().Y;
      this.size.X = this.SuppliesRemaining.GetSize().X;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateSuppliesRemainingColumn()
    {
    }

    public void DrawSuppliesRemainingColumn(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.SuppliesRemaining.DrawZGenericText(offset, spriteBatch);
      this.DaysLeftText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
