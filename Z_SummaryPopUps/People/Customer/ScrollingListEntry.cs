// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.ScrollingListEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class ScrollingListEntry
  {
    public Vector2 location;
    protected float basescale;
    protected UIScaleHelper scalehelper;
    protected CustomerFrame frame;
    protected Vector2 framescale;
    protected Vector2 pad;
    protected bool disableinput;
    protected bool darkerframe;

    public bool DarkerFrame
    {
      get => this.darkerframe;
      set
      {
        this.darkerframe = value;
        this.frame = new CustomerFrame(this.framescale, this.darkerframe, this.basescale);
      }
    }

    public bool DisableInput
    {
      get => this.disableinput;
      set => this.disableinput = value;
    }

    public ScrollingListEntry(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.framescale = new Vector2();
    }

    public Vector2 GetSize() => this.framescale;

    public virtual bool UpdateScrollingListEntry(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public virtual void DrawScrollingListEntry(SpriteBatch spritebatch, Vector2 offset)
    {
    }
  }
}
