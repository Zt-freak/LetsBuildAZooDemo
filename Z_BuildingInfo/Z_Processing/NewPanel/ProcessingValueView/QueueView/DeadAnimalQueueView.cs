// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView.DeadAnimalQueueView
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView
{
  internal class DeadAnimalQueueView
  {
    public Vector2 location;
    private DeadAnimalsInQueue queue;
    private ProductsInBuilding products;
    private float BaseScale;
    private FactoryProductionLine reffactoryproduction;
    private int lastQueueCount;
    private Vector2 size;

    public DeadAnimalQueueView(
      FactoryProductionLine factoryProduction,
      float _BaseScale,
      float ForcedWidth,
      float ForcedHeight)
    {
      this.BaseScale = _BaseScale;
      this.reffactoryproduction = factoryProduction;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.size = Vector2.Zero;
      float ForcedHeight1 = Math.Min(uiScaleHelper.ScaleY(160f), (float) (((double) ForcedHeight - (double) defaultBuffer.Y) * 0.600000023841858));
      this.queue = new DeadAnimalsInQueue(factoryProduction, this.BaseScale, ForcedHeight1, ForcedWidth);
      this.queue.location.Y = this.size.Y;
      this.queue.location += this.queue.GetSize() * 0.5f;
      this.size.Y += this.queue.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      float ForcedHeight2 = ForcedHeight - defaultBuffer.Y - ForcedHeight1;
      this.products = new ProductsInBuilding(factoryProduction, this.BaseScale, ForcedHeight2, ForcedWidth);
      this.products.location.Y = this.size.Y;
      this.products.location += this.products.GetSize() * 0.5f;
      this.size.Y += this.products.GetSize().Y;
      Vector2 vector2 = -new Vector2(ForcedWidth, ForcedHeight) * 0.5f;
      this.queue.location.Y += vector2.Y;
      this.products.location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateDeadAnimalQueueView(Player player, Vector2 offset)
    {
      offset += this.location;
      this.queue.UpdateDeadAnimalsInQueue(player, offset);
      this.products.UpdateProductsInBuilding(player, offset);
    }

    public void DrawDeadAnimalQueueView(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.queue.DrawDeadAnimalsInQueue(offset, spriteBatch);
      this.products.DrawProductsInBuilding(offset, spriteBatch);
    }
  }
}
