// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralityActionUI.MoralityActionButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_MoralityActionUI
{
  internal class MoralityActionButton
  {
    private TextButton textbutton;
    private string text;
    public Vector2 location;
    private float basescale;
    private bool isGood;
    private float length;
    private UIScaleHelper uiScale;
    private bool close;
    private bool mouseover;

    public MoralityActionButton(bool isGood_, string text_, float length_, float basescale_)
    {
      this.basescale = basescale_;
      this.isGood = isGood_;
      this.text = text_;
      this.length = length_;
      this.uiScale = new UIScaleHelper(this.basescale);
      this.textbutton = new TextButton(this.basescale, this.text, this.length);
      if (this.isGood)
        this.textbutton.SetButtonColour(BTNColour.GoodYellow);
      else
        this.textbutton.SetButtonColour(BTNColour.EvilPurple);
    }

    public bool UpdateMoralityActionButton(Player player, Vector2 offset, float DeltaTime)
    {
      this.close = this.textbutton.UpdateTextButton(player, offset + this.location, DeltaTime);
      return this.close;
    }

    public void DrawMoralityActionButton(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.textbutton.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
