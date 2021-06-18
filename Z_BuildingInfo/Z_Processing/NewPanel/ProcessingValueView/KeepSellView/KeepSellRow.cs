// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView.KeepSellRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView
{
  internal class KeepSellRow
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MeatWithNumber meat;
    private StoreroomKeepSelector keepSelector;

    public KeepSellRow(
      AnimalFoodType foodType,
      Player player,
      float BaseScale,
      float forceThisWidth)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      this.meat = new MeatWithNumber(foodType, BaseScale, AddNameTextOnRight: true, ForceConsistentSize: true);
      this.meat.SmartSetIfUndiscoveredOrUnavailable(player);
      this.keepSelector = new StoreroomKeepSelector(BaseScale);
      float num = uiScaleHelper.ScaleY(24f);
      Vector2 _VSCale = defaultBuffer;
      this.meat.location.X = _VSCale.Y;
      _VSCale.X += uiScaleHelper.ScaleX(200f);
      this.keepSelector.location.X = _VSCale.X;
      _VSCale.Y += num;
      _VSCale.Y += defaultBuffer.Y;
      _VSCale.X = forceThisWidth;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.meat.location.X += vector2.X;
      this.keepSelector.location.X += vector2.X;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateKeepSellRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.keepSelector.UpdateStoreroomKeepSelector(player, DeltaTime, offset);
    }

    public void DrawKeepSellRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.meat.DrawMeatWithNumber(offset, spriteBatch);
      this.keepSelector.DrawStoreroomKeepSelector(offset, spriteBatch);
    }
  }
}
