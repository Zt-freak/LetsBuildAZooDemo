// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.PenInfoPanel.Damage.DamageBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_ManagePen.PenInfoPanel.Damage
{
  internal class DamageBar
  {
    private SatisfactionBar bar;
    public Vector2 Location;
    private ActiveIcon exclaim;

    public DamageBar(float BaseScale, PrisonZone prisonzone)
    {
      this.bar = new SatisfactionBar(prisonzone.GateIntegrity / 100f, BaseScale * 2f, BarSIze.Thin);
      if ((double) prisonzone.GateIntegrity <= 25.0)
        this.bar.SetBarColours(new Vector3(1f, 0.0f, 0.0f));
      if ((double) prisonzone.GateIntegrity > 20.0)
        return;
      this.exclaim = new ActiveIcon(false, BaseScale, true);
      this.exclaim.vLocation = new Vector2((float) ((double) this.bar.DrawRect.Width * (double) this.bar.scale * -0.5), (float) this.bar.DrawRect.Height * -this.bar.scale);
      this.exclaim.WillFlash = true;
    }

    public void DrawDamageBar(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      this.bar.DrawSatisfactionBar(Offset, spritebatch);
      if (this.exclaim == null)
        return;
      this.exclaim.DrawActiveIcon(spritebatch, Offset);
    }
  }
}
