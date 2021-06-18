// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Instructional.InstructionalPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManagePen.Instructional
{
  internal class InstructionalPanel
  {
    private SimpleTextHandler texthandler;
    private BackButton close;
    private PageArrowButton pagearrowbutton;

    public InstructionalPanel(
      string TextToWrite,
      float BaseScale,
      float WidthOfPanel,
      float HeightOfPanel)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.pagearrowbutton = new PageArrowButton(false, BaseScale, true);
      this.pagearrowbutton.vLocation.X = WidthOfPanel * 0.5f;
      this.pagearrowbutton.vLocation.Y = HeightOfPanel * -0.5f;
      this.pagearrowbutton.vLocation.X -= defaultBuffer.X;
      this.pagearrowbutton.vLocation.Y += defaultBuffer.Y;
      this.pagearrowbutton.vLocation.X -= this.pagearrowbutton.GetSize().X * 0.5f;
      this.pagearrowbutton.vLocation.Y += this.pagearrowbutton.GetSize().Y * 0.5f;
      this.texthandler = new SimpleTextHandler(TextToWrite, (float) ((double) WidthOfPanel - (double) this.pagearrowbutton.GetSize().X - (double) defaultBuffer.X * 2.0), _Scale: BaseScale);
      this.texthandler.AutoCompleteParagraph();
      this.texthandler.SetAllColours(ColourData.Z_Cream);
      this.texthandler.Location.X = WidthOfPanel * -0.5f;
      this.texthandler.Location.X += defaultBuffer.X;
      this.texthandler.Location.Y = this.texthandler.GetHeightOfParagraph() * -0.5f;
    }

    public bool UpdateInstructionalPanel(float DeltaTime, Player player, Vector2 Offset) => this.pagearrowbutton.UpdatePageArrowButton(DeltaTime, player, Offset);

    public void DrawInstructionalPanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.pagearrowbutton.DrawPageArrowButton(Offset, spritebatch);
      this.texthandler.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}
