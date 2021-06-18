// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.MoralityChoicesLayout
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.MoralityElements;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific
{
  internal class MoralityChoicesLayout
  {
    public Vector2 location;
    private Vector2 size;
    private MoralityLockArrowWithImage diagram;
    private PictureWithCaption simplePicture;
    private ZGenericText header;
    private SimpleTextHandler para;

    public MoralityChoicesLayout(float BaseScale, FeatureUnlockDisplayType unlockType)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      float num = 0.0f;
      PictureType pictureType = PictureType.Count;
      switch (unlockType)
      {
        case FeatureUnlockDisplayType.MoralityUsed:
          this.diagram = new MoralityLockArrowWithImage(BaseScale);
          num = this.diagram.GetSize().X;
          break;
        case FeatureUnlockDisplayType.VIPIntro:
          pictureType = PictureType.VIPThreeInOne;
          break;
      }
      if (pictureType != PictureType.Count)
      {
        this.simplePicture = new PictureWithCaption(BaseScale, pictureType, false);
        num = this.simplePicture.GetSize().X;
      }
      this.header = new ZGenericText(FeatureUnlockDisplayData.GetNewsHeaderForThis(unlockType), BaseScale, _UseRoundaboutHugeFont: true);
      this.header.SetAllColours(ColourData.Z_DarkTextGray);
      float width_ = num - defaultBuffer.X * 2f;
      this.para = new SimpleTextHandler(FeatureUnlockDisplayData.GetBodyParagraphForNews(unlockType), width_, true, BaseScale, AutoComplete: true);
      this.para.SetAllColours(ColourData.Z_DarkTextGray);
      if (this.diagram != null)
        this.size = this.diagram.GetSize();
      else if (this.simplePicture != null)
        this.size = this.simplePicture.GetSize();
      this.size.Y += defaultBuffer.Y * 3f;
      this.header.vLocation.Y = this.size.Y;
      this.header.vLocation.Y += this.header.GetSize().Y * 0.5f;
      this.size.Y += this.header.GetSize().Y;
      this.para.Location.Y = this.size.Y;
      this.para.Location.Y += this.para.GetHeightOfOneLine() * 0.5f;
      this.size.Y += this.para.GetHeightOfParagraph();
    }

    public Vector2 GetSize() => this.size;

    public void DrawMorality_PaintedAnimalLayout(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.diagram != null)
        this.diagram.DrawMoralityLockArrowWithImage(offset, spriteBatch);
      else if (this.simplePicture != null)
        this.simplePicture.DrawPictureWithCaption(offset, spriteBatch);
      this.header.DrawZGenericText(offset, spriteBatch);
      this.para.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
