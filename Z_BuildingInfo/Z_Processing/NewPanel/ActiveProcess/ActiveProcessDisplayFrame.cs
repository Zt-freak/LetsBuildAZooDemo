// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess.ActiveProcessDisplayFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ActiveProcess
{
  internal class ActiveProcessDisplayFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private WaitingQueueSummary waitingQueueSummary;
    private CurrentlyMaking currentlyMaking;
    private EndProduct endProduct;
    private float BaseScale;
    private Vector2 buffer;
    private FactoryProductionLine factoryproduction;

    public ActiveProcessDisplayFrame(ShopEntry shopEntry, Player player, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = uiScaleHelper.DefaultBuffer;
      this.factoryproduction = shopEntry.factoryproduction;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, this.BaseScale);
      this.customerFrame.AddMiniHeading("Production Progress");
      float forcedHeight = uiScaleHelper.ScaleY(120f);
      this.waitingQueueSummary = new WaitingQueueSummary(this.BaseScale, forcedHeight, shopEntry.tiletype);
      this.currentlyMaking = new CurrentlyMaking(this.BaseScale, forcedHeight, shopEntry.tiletype, this.factoryproduction);
      this.endProduct = new EndProduct(this.BaseScale, forcedHeight, shopEntry.tiletype, this.factoryproduction, player);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.Y += this.buffer.Y;
      zero.X += this.buffer.X;
      this.waitingQueueSummary.location = zero;
      this.waitingQueueSummary.location.X += this.waitingQueueSummary.GetSize().X * 0.5f;
      zero.X += this.waitingQueueSummary.GetSize().X;
      zero.X += this.buffer.X;
      this.currentlyMaking.location = zero;
      this.currentlyMaking.location.X += this.currentlyMaking.GetSize().X * 0.5f;
      zero.X += this.currentlyMaking.GetSize().X;
      zero.X += this.buffer.X;
      this.endProduct.location = zero;
      this.endProduct.location.X += this.endProduct.GetSize().X * 0.5f;
      zero.X += this.endProduct.GetSize().X;
      zero.X += this.buffer.X;
      this.waitingQueueSummary.location.Y += forcedHeight * 0.5f;
      this.currentlyMaking.location.Y += forcedHeight * 0.5f;
      this.endProduct.location.Y += forcedHeight * 0.5f;
      zero.Y += forcedHeight;
      zero.Y += this.buffer.Y;
      this.customerFrame.Resize(zero);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.waitingQueueSummary.location += vector2;
      this.currentlyMaking.location += vector2;
      this.endProduct.location += vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateActiveProcessDisplayFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.waitingQueueSummary.UpdateWaitingQueueSummary(DeltaTime);
      this.currentlyMaking.UpdateCurrentlyMaking();
      this.endProduct.UpdateEndProduct(player, DeltaTime, offset);
      this.waitingQueueSummary.SetQueueCount(this.factoryproduction.GetStockToDisplay(), true, this.factoryproduction.GetDeadAnimalQueue());
    }

    public void DrawActiveProcessDisplayFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.waitingQueueSummary.DrawWaitingQueueSummary(offset, spriteBatch);
      this.currentlyMaking.DrawCurrentlyMaking(offset, spriteBatch);
      this.endProduct.DrawEndProduct(offset, spriteBatch);
    }
  }
}
