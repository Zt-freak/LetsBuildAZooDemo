// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.Keyboard.DescriptionString
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Pause.Controls.Keyboard
{
  internal class DescriptionString
  {
    private KeyWithString keyandstring;
    private SimpleTextHandler Name;
    public Vector2 vLocation;
    private Vector2 size;

    public DescriptionString(Keys thiskey, string TextToDraw, float BaseScale, float maxWidth = -1f)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.keyandstring = new KeyWithString(thiskey, BaseScale);
      float width_ = 1024f;
      if ((double) maxWidth != -1.0)
        width_ = maxWidth;
      this.Name = new SimpleTextHandler(TextToDraw, width_, _Scale: BaseScale, _UseFontOnePointFive: true, AutoComplete: true);
      this.Name.SetAllColours(ColourData.Z_Cream);
      this.size = Vector2.Zero;
      this.keyandstring.vLocation.X += this.keyandstring.GetSize().X * 0.5f;
      this.size.X += this.keyandstring.GetSize().X;
      this.size.X += defaultBuffer.X;
      this.Name.Location.X = this.size.X;
      this.Name.Location.Y -= this.Name.GetHeightOfParagraph() * 0.5f;
      this.size.X += this.Name.GetSize().X;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateDescriptionString()
    {
    }

    public void DrawDescriptionString(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.vLocation;
      this.keyandstring.DrawKeyWithString(Offset, spriteBatch);
      this.Name.DrawSimpleTextHandler(Offset, 1f, spriteBatch);
    }
  }
}
