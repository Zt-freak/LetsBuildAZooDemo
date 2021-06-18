// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CurrentFinances
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents;

namespace TinyZoo.Z_WeekOver.V2.Cubes
{
  internal class CurrentFinances : BaseCube
  {
    private BigNumberAndHeading Current;
    private int LastCash;
    private FramedBigNumber CurrentMoney;
    private FramedBigNumber CurrentLoan;

    public CurrentFinances(float _BaseScale, Player player)
      : base(_BaseScale, true, new Vector3(0.2588235f, 0.4235294f, 0.3803922f))
    {
      this.LastCash = player.Stats.GetCashHeld();
      this.Current = new BigNumberAndHeading("", player.Stats.GetCashHeld(), _BaseScale, BigTextType.SmallText_BigNumber);
      this.Current.SetLocation(false, false);
      this.AddMiniHeading("Finances", new Vector3(0.9568627f, 0.9411765f, 0.8784314f));
      this.CurrentMoney = new FramedBigNumber(_BaseScale, "Current Held Money", new Vector3(0.8352941f, 0.5294118f, 0.3333333f), player.Stats.GetCashHeld(), "$", true);
      this.CurrentLoan = new FramedBigNumber(_BaseScale, "Current Loan", new Vector3(0.7529412f, 0.4941176f, 0.4588235f), player.Stats.GetCurrentLoan(), "$", true);
      this.CurrentMoney.Position.Y = (float) (45.0 * -(double) this.BaseScale) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CurrentLoan.Position.Y = 45f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CurrentLoan.Position.Y += 5f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CurrentMoney.Position.Y += 10f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CurrentMoney.SetColourBehaviour(ColourBehavor.Money_Blue_Black);
      this.CurrentLoan.SetColourBehaviour(ColourBehavor.Loan_Black_Red);
    }

    public override bool LerpComplete(CurrentFinances currentfinances) => base.LerpComplete(currentfinances);

    public bool MoneyIsLerping() => !this.CurrentMoney.NumberLerpComplete();

    public override void UpdateBaseCube(float DeltaTime, Player player, Vector2 Offset)
    {
      if ((double) this.lerperBaseCube.Value == 1.0)
      {
        this.CurrentMoney.CheckValue(player.Stats.GetCashHeld());
        this.CurrentMoney.UpdateFramedBigNumber(DeltaTime);
        this.CurrentLoan.CheckValue(player.Stats.GetCurrentLoan());
        this.CurrentLoan.UpdateFramedBigNumber(DeltaTime);
      }
      base.UpdateBaseCube(DeltaTime, player, Offset);
    }

    public override void DrawBaseCube(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      base.DrawBaseCube(Offset, spritebatch);
      if ((double) this.lerperBaseCube.Value != 1.0)
        return;
      this.CurrentMoney.DrawFramedBigNumber(Offset, spritebatch);
      this.CurrentLoan.DrawFramedBigNumber(Offset, spritebatch);
    }
  }
}
