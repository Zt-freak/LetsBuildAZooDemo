// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Money.MoneyInfoRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_BalanceSystems.Finances;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.Elements.Money
{
  internal class MoneyInfoRow
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText leftHandText;
    private ZGenericText rightHandText;

    public MoneyInfoType refRowType { get; private set; }

    public MoneyInfoRow(MoneyInfoType rowType, float BaseScale, Player player)
    {
      this.refRowType = rowType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.X += defaultBuffer.X;
      this.leftHandText = new ZGenericText(this.GetStringForLeftHand(rowType), BaseScale, false, _UseOnePointFiveFont: true);
      this.leftHandText.vLocation.X = zero.X;
      this.leftHandText.vLocation.Y += this.leftHandText.GetSize().Y * 0.5f;
      zero.X += uiScaleHelper.ScaleX(160f);
      this.rightHandText = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
      this.rightHandText.vLocation.X = zero.X;
      zero.X += uiScaleHelper.ScaleX(35f);
      this.SetValues(player);
      zero.Y = uiScaleHelper.ScaleY(25f);
      this.customerFrame = new CustomerFrame(zero, CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      ZGenericText leftHandText = this.leftHandText;
      leftHandText.vLocation = leftHandText.vLocation + vector2;
      this.rightHandText.vLocation.X += vector2.X;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void SetValues(Player player)
    {
      int num = -1;
      switch (this.refRowType)
      {
        case MoneyInfoType.Loan:
          num = player.Stats.GetCurrentLoan();
          break;
        case MoneyInfoType.Profit:
          num = GetProfits.GetProfitsTodayRealTime_Est();
          break;
      }
      if (num < 0)
        this.rightHandText.textToWrite = "-$" + (object) Math.Abs(num);
      else
        this.rightHandText.textToWrite = "$" + (object) Math.Abs(num);
    }

    public bool UpdateMoneyInfoRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.SetValues(player);
      return this.customerFrame.UpdateForMouseOverAndClick(player, DeltaTime, offset, out bool _);
    }

    private string GetStringForLeftHand(MoneyInfoType rowType)
    {
      if (rowType == MoneyInfoType.Loan)
        return "Loan";
      return rowType == MoneyInfoType.Profit ? "Profit Today" : "I AM AN INFOROWTYPE";
    }

    public void DrawMoneyInfoRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.leftHandText.DrawZGenericText(offset, spriteBatch);
      this.rightHandText.DrawZGenericText(offset, spriteBatch);
      this.customerFrame.PostDrawMouseoverOverlay(offset, spriteBatch);
    }
  }
}
