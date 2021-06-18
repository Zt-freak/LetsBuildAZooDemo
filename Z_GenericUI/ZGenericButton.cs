// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ZGenericButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class ZGenericButton
  {
    private Rectangle drawRect;
    private Rectangle mouseoverRect;
    private GameObject drawobj;
    public Vector2 location;
    protected float basescale;
    public bool mouseover;
    public UIScaleHelper uiscale;
    private MouseoverHandler mouseoverhandler;
    private Vector2 size;
    protected bool greyedOut;

    public ZGenericButton(float basescale_, Rectangle drawRect_, bool greyedOut_ = false) => this.Init(basescale_, drawRect_, drawRect_, greyedOut_);

    public ZGenericButton(
      float basescale_,
      Rectangle drawRect_,
      Rectangle mouseoverRect_,
      bool greyedOut_ = false)
    {
      this.Init(basescale_, drawRect_, mouseoverRect_, greyedOut_);
    }

    public void Init(
      float basescale_,
      Rectangle drawRect_,
      Rectangle mouseoverRect_,
      bool greyedOut_ = false)
    {
      this.greyedOut = greyedOut_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(basescale_);
      this.drawRect = drawRect_;
      this.mouseoverRect = mouseoverRect_;
      this.drawobj = new GameObject();
      this.drawobj.DrawRect = drawRect_;
      this.drawobj.SetDrawOriginToCentre();
      this.drawobj.scale = this.basescale;
      this.size = this.uiscale.ScaleVector2(new Vector2((float) drawRect_.Width, (float) drawRect_.Height));
      this.mouseoverhandler = new MouseoverHandler(this.size.X, this.size.Y, this.basescale);
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateZGenericButton(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.mouseover = this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      bool flag = false;
      if (this.mouseover)
      {
        flag = (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
        this.drawobj.DrawRect = this.mouseoverRect;
      }
      else
        this.drawobj.DrawRect = this.drawRect;
      return flag;
    }

    public virtual void DrawZGenericButton(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.drawobj.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
    }

    public void DrawButtonHighlight(SpriteBatch spritebatch, Vector2 offset)
    {
      if (this.greyedOut)
        return;
      offset += this.location;
      this.mouseoverhandler.DrawMouseOverHandler(spritebatch, offset);
    }
  }
}
