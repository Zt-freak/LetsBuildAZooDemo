// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Customers.TopBarCustomer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.Elements.Research;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;

namespace TinyZoo.Z_HUD.TopBar.Elements.Customers
{
  internal class TopBarCustomer
  {
    public Vector2 location;
    private TopBarHeaderBase headerBase;
    private Z_ResearchIcon icon;
    private ZGenericText customerCount;
    private LerpHandler_Float lerper;

    public TopBarCustomer(float BaseScale, float BarHeight)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.customerCount = new ZGenericText("000", BaseScale, _UseOnePointFiveFont: true);
      this.icon = new Z_ResearchIcon(BaseScale, IconType.Customer);
      Vector2 zero = Vector2.Zero;
      zero.X = uiScaleHelper.DefaultBuffer.X;
      this.icon.location.X = zero.X;
      this.icon.location.X += this.icon.GetSize().X * 0.5f;
      zero.X += this.icon.GetSize().X;
      zero.X += uiScaleHelper.DefaultBuffer.X;
      this.customerCount.vLocation.X = zero.X;
      this.customerCount.vLocation.X += this.customerCount.GetSize().X * 0.5f;
      zero.X += this.customerCount.GetSize().X;
      zero.X += uiScaleHelper.DefaultBuffer.X;
      this.headerBase = new TopBarHeaderBase(BaseScale, BarHeight, zero.X, true);
      Vector2 vector2 = -this.headerBase.GetSize() * 0.5f;
      this.customerCount.vLocation.X += vector2.X;
      this.icon.location.X += vector2.X;
      this.headerBase.SetPopOutFrame(TopBarPopOutType.Customer);
      this.SetValues();
      this.lerper = new LerpHandler_Float();
    }

    public Vector2 GetSize() => this.headerBase.GetSize();

    public bool CheckMouseOver(Player player, Vector2 offset) => this.headerBase.CheckMouseOver(player, offset);

    private void SetValues() => this.customerCount.textToWrite = CustomerManager.CustomersInPark_NotWaitingForBus.ToString();

    public void LerpIn()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return;
      this.lerper.SetLerp(false, -1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if ((double) this.lerper.TargetValue == -1.0)
        return;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f);
    }

    public void UpdateTopBarCustomer(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (FeatureFlags.BlockAllUI)
        this.LerpOff();
      else
        this.LerpIn();
      if ((double) this.lerper.Value == -1.0)
        return;
      this.SetValues();
      this.headerBase.UpdateTopBarHeaderBase(player, DeltaTime, offset);
    }

    public void PreDrawTopBarCustomer(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.headerBase.PreDrawTopBarHeaderBase(offset, spriteBatch);
    }

    public void DrawTopBarCustomer(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.headerBase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.icon.DrawResearchIcon(offset, spriteBatch);
      this.customerCount.DrawZGenericText(offset, spriteBatch);
      this.headerBase.PostDrawTopBarHeaderBase(offset, spriteBatch);
    }
  }
}
