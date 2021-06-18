// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.ResearchHubNewsLayout
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
  internal class ResearchHubNewsLayout
  {
    public Vector2 location;
    private ZGenericText header;
    private SimpleTextHandler bodyParagraph;
    private PictureWithCaption buildingPicture;
    private PictureWithCaption buildingIllustration;
    private Vector2 size;

    public ResearchHubNewsLayout(float BaseScale, FeatureUnlockDisplayType unlockType, float width)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.header = new ZGenericText(FeatureUnlockDisplayData.GetTitleOfBodyTextForThisNews(unlockType), BaseScale, false, _UseOnePointFiveFont: true);
      this.header.SetAllColours(ColourData.Z_DarkTextGray);
      this.buildingPicture = new PictureWithCaption(BaseScale, PictureType.ResearchHub);
      this.buildingIllustration = new PictureWithCaption(BaseScale, PictureType.ResearchHubDrawings);
      float width_ = width - this.buildingPicture.GetSize().X - defaultBuffer.X;
      this.bodyParagraph = new SimpleTextHandler(FeatureUnlockDisplayData.GetBodyParagraphForNews(unlockType), width_, _Scale: BaseScale, AutoComplete: true);
      this.bodyParagraph.SetAllColours(ColourData.Z_DarkTextGray);
      this.size = Vector2.Zero;
      Vector2 size = this.size;
      this.buildingPicture.location.Y = size.Y;
      this.buildingPicture.location.X = width;
      this.buildingPicture.location.X -= this.buildingPicture.GetSize().X * 0.5f;
      this.header.vLocation = this.size;
      this.size.Y += this.header.GetSize().Y;
      this.bodyParagraph.Location = this.size;
      this.size.Y += this.bodyParagraph.GetHeightOfParagraph();
      this.size.Y += defaultBuffer.Y;
      this.buildingIllustration.location = this.size;
      this.buildingIllustration.location.X += this.buildingIllustration.GetSize().X * 0.5f;
      this.size.Y += this.buildingIllustration.GetSize().Y;
      this.size.Y = Math.Max(this.size.Y, size.Y);
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void DrawResearchHubNewsLayout(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.buildingPicture.DrawPictureWithCaption(offset, spriteBatch);
      this.buildingIllustration.DrawPictureWithCaption(offset, spriteBatch);
      this.bodyParagraph.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
