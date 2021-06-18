// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Customers.CustomerPopOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;

namespace TinyZoo.Z_HUD.TopBar.Elements.Customers
{
  internal class CustomerPopOut : GenericTopBarPopOutFrame
  {
    private string baseCustomerString;
    private string baseVIPString;
    private string baseEmployeeString;
    private string baseAnimalString;
    private ZGenericText customerCount;
    private ZGenericText VIPCount;
    private ZGenericText employeeCount;
    private ZGenericText animalCount;

    public CustomerPopOut(float BaseScale, Player player)
      : base(BaseScale)
    {
      this.baseCustomerString = "Visitors: ";
      this.baseVIPString = "VIPs: ";
      this.baseEmployeeString = "Employees: ";
      this.baseAnimalString = "Animals: ";
      this.customerCount = new ZGenericText(this.baseCustomerString + "000", BaseScale, false);
      this.VIPCount = new ZGenericText(this.baseVIPString + "000", BaseScale, false);
      this.employeeCount = new ZGenericText(this.baseEmployeeString + "000", BaseScale, false);
      this.animalCount = new ZGenericText(this.baseAnimalString + "000", BaseScale, false);
      Vector2 vector2_1 = Vector2.Zero + this.scaleHelper.DefaultBuffer;
      this.customerCount.vLocation = vector2_1;
      vector2_1.Y += this.customerCount.GetSize().Y;
      this.VIPCount.vLocation = vector2_1;
      vector2_1.Y += this.VIPCount.GetSize().Y;
      this.employeeCount.vLocation = vector2_1;
      vector2_1.Y += this.employeeCount.GetSize().Y;
      this.animalCount.vLocation = vector2_1;
      vector2_1.Y += this.animalCount.GetSize().Y;
      Vector2 _frameSize = vector2_1 + this.scaleHelper.DefaultBuffer;
      _frameSize.X += Math.Max(this.customerCount.GetSize().X, this.VIPCount.GetSize().X);
      _frameSize.X += this.scaleHelper.DefaultBuffer.X;
      this.FinalizeSize(_frameSize);
      Vector2 vector2_2 = -_frameSize * 0.5f;
      ZGenericText customerCount = this.customerCount;
      customerCount.vLocation = customerCount.vLocation + vector2_2;
      ZGenericText vipCount = this.VIPCount;
      vipCount.vLocation = vipCount.vLocation + vector2_2;
      ZGenericText employeeCount = this.employeeCount;
      employeeCount.vLocation = employeeCount.vLocation + vector2_2;
      ZGenericText animalCount = this.animalCount;
      animalCount.vLocation = animalCount.vLocation + vector2_2;
      this.SetValues(player);
    }

    private void SetValues(Player player)
    {
      this.customerCount.textToWrite = this.baseCustomerString + (object) Math.Max(CustomerManager.CustomersInPark_NotWaitingForBus - CustomerManager.VIP_BlackMarketEtc, 0);
      this.VIPCount.textToWrite = this.baseVIPString + (object) CustomerManager.VIP_BlackMarketEtc;
      this.employeeCount.textToWrite = this.baseEmployeeString + (object) player.employees.employees.Count;
      this.animalCount.textToWrite = this.baseAnimalString + (object) Z_GameFlags.TotalLivingAnimalsInZoo;
    }

    public bool UpdateCustomerPopOut(Player player, float DeltaTime, Vector2 offset)
    {
      if (!this.IsOffScreen())
        this.SetValues(player);
      return this.UpdatePopOutFrame(player, DeltaTime, ref offset);
    }

    public void DrawCustomerPopOut(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      this.customerCount.DrawZGenericText(offset, spriteBatch);
      this.VIPCount.DrawZGenericText(offset, spriteBatch);
      this.employeeCount.DrawZGenericText(offset, spriteBatch);
      this.animalCount.DrawZGenericText(offset, spriteBatch);
    }
  }
}
