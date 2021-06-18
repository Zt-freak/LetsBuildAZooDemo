// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.WarehouseMarketView.WarehouseMarketViewFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.AnimalView;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.WarehouseMarketView
{
  internal class WarehouseMarketViewFrame
  {
    public Vector2 location;
    private ProcessingAnimalInfoView grid;

    public WarehouseMarketViewFrame(Player player, float BaseScale) => this.grid = new ProcessingAnimalInfoView(player, BaseScale, false, true);

    public Vector2 GetSize() => this.grid.GetSize();

    public void AddScroll(float maxHeight) => this.grid.AddScroll(maxHeight);

    public void UpdateWarehouseMarketViewFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.grid.UpdateProcessingAnimalInfoView(player, DeltaTime, offset);
    }

    public void DrawWarehouseMarketViewFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.grid.DrawProcessingAnimalInfoView(offset, spriteBatch);
    }

    public void PostDrawWarehouseMarketViewFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.grid.PostDrawProcessingAnimalInfoView(offset, spriteBatch);
    }
  }
}
