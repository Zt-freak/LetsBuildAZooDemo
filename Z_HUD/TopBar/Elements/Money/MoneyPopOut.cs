// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Money.MoneyPopOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;

namespace TinyZoo.Z_HUD.TopBar.Elements.Money
{
  internal class MoneyPopOut : GenericTopBarPopOutFrame
  {
    private List<MoneyInfoRow> infoRows;
    private MoneyBreakDownPopOut breakdownPopOut;
    private bool DrawPanelOnTop;
    private GenericTopBarPopOutFrame currentPopOut;

    public MoneyPopOut(float BaseScale, Player player)
      : base(BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.Y += defaultBuffer.Y;
      zero.X += defaultBuffer.X;
      this.infoRows = new List<MoneyInfoRow>();
      for (int index = 0; index < 2; ++index)
      {
        MoneyInfoRow moneyInfoRow = new MoneyInfoRow((MoneyInfoType) index, BaseScale, player);
        moneyInfoRow.location.Y = zero.Y;
        moneyInfoRow.location.Y += moneyInfoRow.GetSize().Y * 0.5f;
        zero.Y += moneyInfoRow.GetSize().Y;
        zero.Y += defaultBuffer.Y;
        this.infoRows.Add(moneyInfoRow);
      }
      zero.X += this.infoRows[0].GetSize().X;
      zero.X += defaultBuffer.X;
      this.FinalizeSize(zero);
      Vector2 vector2 = -zero * 0.5f;
      for (int index = 0; index < this.infoRows.Count; ++index)
        this.infoRows[index].location.Y += vector2.Y;
    }

    public override void ToggleLerp() => base.ToggleLerp();

    public override bool LerpIn(bool ForceLerp = false) => base.LerpIn(ForceLerp);

    public override bool LerpOff()
    {
      if (this.breakdownPopOut != null)
        this.breakdownPopOut.LerpOff();
      return base.LerpOff();
    }

    public override bool CheckMouseOver(Player player, Vector2 offset)
    {
      if (this.breakdownPopOut == null || !this.breakdownPopOut.CheckMouseOver(player, offset + this.location))
        return base.CheckMouseOver(player, offset);
      Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = true;
      return true;
    }

    public bool UpdateMoneyPopOut(Player player, float DeltaTime, Vector2 offset)
    {
      bool flag = this.UpdatePopOutFrame(player, DeltaTime, ref offset);
      if (this.breakdownPopOut != null && this.breakdownPopOut.UpdateMoneyBreakDownPopOut(player, DeltaTime, offset))
        this.breakdownPopOut.LerpOff();
      if (this.IsOffScreen() || !this.IsDoneLerping())
        return false;
      for (int index = 0; index < this.infoRows.Count; ++index)
      {
        if (this.infoRows[index].UpdateMoneyInfoRow(player, DeltaTime, offset))
          this.OnClickRow(this.infoRows[index].refRowType, player, offset);
      }
      return flag;
    }

    private void OnClickRow(MoneyInfoType infoType, Player player, Vector2 offset)
    {
      GenericTopBarPopOutFrame topBarPopOutFrame = (GenericTopBarPopOutFrame) null;
      if (infoType != MoneyInfoType.Loan && infoType == MoneyInfoType.Profit && (this.breakdownPopOut == null || this.breakdownPopOut.IsOffScreen()))
      {
        this.breakdownPopOut = new MoneyBreakDownPopOut(player, this.BaseScale);
        topBarPopOutFrame = (GenericTopBarPopOutFrame) this.breakdownPopOut;
      }
      if (topBarPopOutFrame == null)
        return;
      Vector2 size = topBarPopOutFrame.GetSize();
      if ((double) size.X + (double) offset.X + (double) this.GetSize().X * 0.5 > 1024.0)
      {
        topBarPopOutFrame.AddPreviousButton();
        topBarPopOutFrame.location.Y += size.Y * 0.5f;
        topBarPopOutFrame.location.Y -= this.GetSize().Y * 0.5f;
        this.DrawPanelOnTop = true;
      }
      else
      {
        this.DrawPanelOnTop = false;
        topBarPopOutFrame.location += size * 0.5f;
        topBarPopOutFrame.location.X += this.GetSize().X * 0.5f;
        topBarPopOutFrame.location.Y -= this.GetSize().Y * 0.5f;
      }
      if (this.currentPopOut == null || topBarPopOutFrame == this.currentPopOut)
        return;
      this.currentPopOut.LerpOff();
    }

    public void DrawMoneyPopOut(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      for (int index = 0; index < this.infoRows.Count; ++index)
        this.infoRows[index].DrawMoneyInfoRow(offset, spriteBatch);
      if (this.breakdownPopOut == null)
        return;
      this.breakdownPopOut.DrawMoneyBreakDownPopOut(offset, spriteBatch);
    }
  }
}
