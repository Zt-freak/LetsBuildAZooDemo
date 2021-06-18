// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class CustomerActionPopUp
  {
    public Vector2 location;
    protected Vector2 framescale;
    protected float basescale;
    protected UIScaleHelper uiscale;
    protected Vector2 pad;
    protected CustomerFrame frame;
    public bool ForceCloseEverythingOnClose;
    public bool HasPreviousButton;
    public bool hidemainpanel;

    protected CustomerActionPopUp(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.framescale = new Vector2();
    }

    protected void SizeFrame() => this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));

    public virtual Vector2 GetSize() => this.framescale;

    public virtual void OnPreviousButtonClicked()
    {
    }

    public virtual void OnPanelClosed()
    {
    }

    public virtual bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime) => false;

    public virtual void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
    }
  }
}
