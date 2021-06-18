// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.Zoning.ZoningSelPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyZoo.Z_SummaryPopUps.People.Employee.Zoning
{
  internal class ZoningSelPanel
  {
    private TextButton ZoningButton;
    public Vector2 Location;

    public ZoningSelPanel(float BaseScale) => this.ZoningButton = new TextButton(BaseScale, "Zoning", 40f);

    public bool UpdateZoningSelPanel(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      return this.ZoningButton.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawZoningSelPanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      this.ZoningButton.DrawTextButton(Offset, 1f, spritebatch);
    }
  }
}
