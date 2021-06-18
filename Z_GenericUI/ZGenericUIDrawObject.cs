// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ZGenericUIDrawObject
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class ZGenericUIDrawObject
  {
    public GameObject obj;
    private Texture2D spritesheet;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;

    public float Alpha
    {
      get => this.obj.fAlpha;
      set => this.obj.SetAlpha(value);
    }

    public float Rotation
    {
      get => this.obj.Rotation;
      set => this.obj.Rotation = value;
    }

    public ZGenericUIDrawObject(Rectangle drawrect, float basescale_, Texture2D spritesheet_)
    {
      this.spritesheet = spritesheet_;
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.obj = new GameObject();
      this.obj.scale = this.basescale;
      this.obj.DrawRect = drawrect;
      this.obj.SetDrawOriginToCentre();
      this.framescale = this.scalehelper.ScaleVector2(new Vector2((float) drawrect.Width, (float) drawrect.Height));
    }

    public void SetColour(Vector3 colour) => this.obj.SetAllColours(colour);

    public Vector2 GetSize() => this.framescale;

    internal void SetDrawRect(Rectangle drawrect) => this.obj.DrawRect = drawrect;

    public void DrawZGenericUIDrawObject(SpriteBatch spritebatch, Vector2 offset, float AlphaMult = 1f)
    {
      offset += this.location;
      this.obj.Draw(spritebatch, this.spritesheet, offset, this.obj.scale, this.obj.Rotation, true, this.obj.DrawRect, this.obj.fAlpha * AlphaMult, this.obj.GetColour());
    }
  }
}
