// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.MaintenanceBar.MaintenanceBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_HUD.MaintenanceBar
{
  internal class MaintenanceBar
  {
    private CustomerFrame frame;
    private SatisfactionBar bar;
    public Vector2 location;
    private ActiveIcon exclaim;
    private float basescale;
    private static float scalemult = 2f;
    private UIScaleHelper uiscale;
    private ZGenericText barLabel;
    private Vector2 size;

    public MaintenanceBar(float percentage, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.barLabel = new ZGenericText("Decay", this.basescale, false, _UseOnePointFiveFont: true);
      this.bar = new SatisfactionBar(percentage, TinyZoo.Z_HUD.MaintenanceBar.MaintenanceBar.scalemult * this.basescale, BarSIze.Thin);
      this.size.Y += this.barLabel.GetSize().Y;
      this.size.Y += this.uiscale.DefaultBuffer.Y * 0.5f;
      this.bar.vLocation.Y = this.size.Y;
      this.bar.vLocation.Y += this.bar.GetSize().Y * 0.5f;
      this.barLabel.vLocation.X -= this.bar.GetSize().X * 0.5f;
      this.size += this.bar.GetSize();
      if ((double) percentage <= 25.0)
        this.bar.SetBarColours(new Vector3(1f, 0.0f, 0.0f));
      if ((double) percentage <= 20.0)
      {
        this.exclaim = new ActiveIcon(false, this.basescale, true);
        this.exclaim.vLocation = this.uiscale.ScaleVector2(TinyZoo.Z_HUD.MaintenanceBar.MaintenanceBar.scalemult * new Vector2(-0.5f * (float) this.bar.DrawRect.Width, -1f * (float) this.bar.DrawRect.Height));
        this.exclaim.WillFlash = true;
      }
      this.frame = new CustomerFrame(this.GetSize(), BaseScale: this.basescale);
    }

    public Vector2 GetSize() => this.size;

    public float GetYOffsetToCenterOfBar() => this.bar.vLocation.Y;

    public void DrawMaintenanceBar(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.barLabel.DrawZGenericText(offset, spritebatch);
      this.bar.DrawSatisfactionBar(offset, spritebatch);
      if (this.exclaim == null)
        return;
      this.exclaim.DrawActiveIcon(spritebatch, offset);
    }
  }
}
