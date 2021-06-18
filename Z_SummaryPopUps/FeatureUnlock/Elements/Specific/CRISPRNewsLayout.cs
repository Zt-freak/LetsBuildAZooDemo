// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.CRISPRNewsLayout
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific
{
  internal class CRISPRNewsLayout
  {
    public Vector2 location;
    private ZGenericText header;
    private SimpleTextHandler bodyParagraph;
    private PictureWithCaption buildingPicture;
    private PictureWithCaption hybridPicture;
    private PictureWithCaption hybridIllustration;
    private Vector2 size;

    public CRISPRNewsLayout(float BaseScale, FeatureUnlockDisplayType unlockType, float width)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.header = new ZGenericText(FeatureUnlockDisplayData.GetTitleOfBodyTextForThisNews(unlockType), BaseScale, false, _UseOnePointFiveFont: true);
      this.header.SetAllColours(ColourData.Z_DarkTextGray);
      this.buildingPicture = new PictureWithCaption(BaseScale, PictureType.DNABuilding);
      this.hybridPicture = new PictureWithCaption(BaseScale, PictureType.HybridAnimal);
      this.hybridIllustration = new PictureWithCaption(BaseScale, PictureType.HyrbidAnimalDrawings);
      float width_ = width - this.buildingPicture.GetSize().X - defaultBuffer.X;
      this.bodyParagraph = new SimpleTextHandler(FeatureUnlockDisplayData.GetBodyParagraphForNews(unlockType), width_, _Scale: BaseScale, AutoComplete: true);
      this.bodyParagraph.SetAllColours(ColourData.Z_DarkTextGray);
      this.size = Vector2.Zero;
      Vector2 size = this.size;
      this.buildingPicture.location.Y = size.Y;
      this.buildingPicture.location.X = width;
      this.buildingPicture.location.X -= this.buildingPicture.GetSize().X * 0.5f;
      size.Y += this.buildingPicture.GetSize().Y;
      size.Y += defaultBuffer.Y;
      this.hybridPicture.location.Y = size.Y;
      this.hybridPicture.location.X = this.buildingPicture.location.X;
      this.header.vLocation = this.size;
      this.size.Y += this.header.GetSize().Y;
      this.bodyParagraph.Location = this.size;
      this.size.Y += this.bodyParagraph.GetHeightOfParagraph();
      this.hybridIllustration.location.Y = this.size.Y;
      this.hybridIllustration.location.X = this.bodyParagraph.Location.X + this.bodyParagraph.GetSize(true).X * 0.5f;
      this.size.Y += this.hybridIllustration.GetSize().Y;
      this.size.Y = Math.Max(this.size.Y, size.Y);
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void DrawCRISPRNewsLayout(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.buildingPicture.DrawPictureWithCaption(offset, spriteBatch);
      this.hybridPicture.DrawPictureWithCaption(offset, spriteBatch);
      this.hybridIllustration.DrawPictureWithCaption(offset, spriteBatch);
      this.bodyParagraph.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
