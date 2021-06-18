// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.Cubes.LoanRepayment
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.PlayerDir;
using TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents;

namespace TinyZoo.Z_WeekOver.V2.Cubes.Cubes
{
  internal class LoanRepayment : BaseCube
  {
    private Cube_Paragraph cubepara;
    private FramedBigNumber LoanRepaymentValue;
    private int PayBack;
    private int CanPayThisMuch;
    private bool HasPaid;

    public LoanRepayment(float _BaseScale, Player player)
      : base(_BaseScale, true, new Vector3(0.7529412f, 0.4941176f, 0.4588235f))
    {
      this.PayBack = player.Stats.GetCurrentLoan();
      this.CanPayThisMuch = this.PayBack;
      if (this.PayBack > 0)
      {
        if (this.PayBack > 1000)
          this.PayBack = (int) Math.Ceiling((double) this.PayBack * 0.100000001490116);
        this.CanPayThisMuch = this.PayBack;
        if (this.PayBack > player.Stats.GetCashHeld())
          this.CanPayThisMuch = player.Stats.GetCashHeld();
      }
      this.AddMiniHeading("Loan Repayment", new Vector3(0.9568627f, 0.9411765f, 0.8784314f));
      string Text = "Loans are repayed at 10% of your current savings or $100 which ever is greater. You are due to pay $" + (object) this.PayBack + " today.";
      if (this.PayBack == 0)
        Text = "There are no repayments due today";
      else if (this.CanPayThisMuch != this.PayBack)
        Text = "Loans are repayed at 10% of your current savings or $100 which ever is greater. You are due to pay $" + (object) this.PayBack + " today, but can only afford $" + (object) this.CanPayThisMuch + ".";
      this.cubepara = new Cube_Paragraph(Text, _BaseScale);
      this.cubepara.Location.Y = -60f * _BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.LoanRepaymentValue = new FramedBigNumber(_BaseScale, "Minimum Repayment", new Vector3(0.6392157f, 0.3803922f, 0.345098f), this.PayBack, "$", true);
      this.LoanRepaymentValue.Position.Y = 45f * this.BaseScale;
      this.AlsoWaitForCashBeforeMovingOn = true;
    }

    public override void UpdateBaseCube(float DeltaTime, Player player, Vector2 Offset)
    {
      if (this.lerperBaseCube.IsComplete())
      {
        this.LoanRepaymentValue.UpdateFramedBigNumber(DeltaTime);
        if (!this.HasPaid && this.LoanRepaymentValue.fadelerper.IsComplete())
        {
          this.HasPaid = true;
          player.Stats.SpendCash(this.CanPayThisMuch, SpendingCashOnThis.PayingOffLoan, player, true);
        }
      }
      base.UpdateBaseCube(DeltaTime, player, Offset);
    }

    public override bool LerpComplete(CurrentFinances currentfinances) => base.LerpComplete(currentfinances) && this.LoanRepaymentValue.fadelerper.IsComplete();

    public override void DrawBaseCube(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      base.DrawBaseCube(Offset, spritebatch);
      if ((double) this.lerperBaseCube.Value != 1.0)
        return;
      this.cubepara.DrawCube_Paragraph(Offset, spritebatch);
      this.LoanRepaymentValue.DrawFramedBigNumber(Offset, spritebatch);
    }
  }
}
