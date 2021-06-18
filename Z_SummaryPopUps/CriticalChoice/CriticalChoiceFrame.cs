// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.CriticalChoice.CriticalChoiceFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.CriticalChoice
{
  internal class CriticalChoiceFrame
  {
    public Vector2 location;
    protected float basescale;
    protected UIScaleHelper scalehelper;
    protected CustomerFrame frame;
    protected Vector2 framescale;
    protected Vector2 pad;

    public CriticalChoiceFrame(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.framescale = new Vector2();
    }

    public virtual Vector2 GetSize() => this.framescale;

    public virtual bool UpdateCriticalChoiceFrame(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out int choice)
    {
      offset += this.location;
      choice = 0;
      return false;
    }

    public virtual void DrawCriticalChoiceFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
    }
  }
}
