// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.TrayOpenClose
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_HUD.ControlHint;

namespace TinyZoo.Z_AnimalNotification
{
  internal class TrayOpenClose
  {
    private MicroOpenClose openclose;
    public Vector2 location;
    private bool open;
    private Vector2 framescale;
    private float basescale;

    public TrayOpenClose(float basescale_)
    {
      this.basescale = basescale_;
      this.openclose = new MicroOpenClose(this.basescale, false);
      this.openclose.SetAsArrow();
      this.framescale = this.openclose.GetSize();
    }

    public void Toggle()
    {
      this.open = !this.open;
      if (!this.open)
        this.openclose.SetAsArrow();
      else
        this.openclose.SetAsClose();
    }

    public bool UpdateTrayOpenClose(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      bool flag = false;
      if (this.openclose.UpdateMicroOpenClose(player, DeltaTime, offset))
        flag = (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
      return flag;
    }

    public void DrawTrayOpenClose(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.openclose.DrawMicroOpenClose(offset, spritebatch);
    }
  }
}
