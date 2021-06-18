// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.MoneyBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_HUD
{
  internal class MoneyBox
  {
    public Vector2 vLocation;
    private StringInBox CurrentCost;
    public bool CanAfford;
    public int COST;

    public MoneyBox(float BaseScale, int _COST)
    {
      this.COST = _COST;
      this.CurrentCost = new StringInBox(BaseScale, "$" + (object) this.COST, 100f * BaseScale, true, true, _UseOnePointFiveFont: true);
      this.CurrentCost.SetWhite();
    }

    public Vector2 GetSize() => this.CurrentCost.VScale;

    public void SetCost(int COSTNOW, Player player)
    {
      this.CurrentCost.SetText("$" + (object) COSTNOW);
      this.COST = COSTNOW;
      if (player.Stats.GetCashHeld() >= COSTNOW || COSTNOW == 0)
      {
        if (COSTNOW == 0)
          this.CurrentCost.SetWhite();
        else
          this.CurrentCost.SetGreen();
        this.CanAfford = true;
      }
      else
      {
        this.CurrentCost.SetRed();
        this.CanAfford = false;
      }
    }

    public void UpdateMoneyBox(Player player)
    {
      if (!this.CanAfford)
      {
        if (player.Stats.GetCashHeld() < this.COST)
          return;
        this.CurrentCost.SetGreen();
        this.CanAfford = true;
      }
      else
      {
        if (player.Stats.GetCashHeld() >= this.COST)
          return;
        this.CurrentCost.SetRed();
        this.CanAfford = false;
      }
    }

    public void SetRed()
    {
      this.CanAfford = false;
      this.CurrentCost.SetRed();
    }

    public void DrawMoneyBox(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.vLocation;
      this.CurrentCost.DrawStringInBox(Offset, spritebatch);
    }
  }
}
