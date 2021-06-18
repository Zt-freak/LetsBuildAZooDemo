// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.LandExpansionNewsLayout
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
  internal class LandExpansionNewsLayout
  {
    public Vector2 location;
    private ZGenericText header;
    private SimpleTextHandler bodyParagraph;
    private PictureWithCaption picture;
    private Vector2 size;

    public LandExpansionNewsLayout(
      float BaseScale,
      FeatureUnlockDisplayType unlockType,
      float width)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.picture = new PictureWithCaption(BaseScale, PictureType.LandForSale);
      float width_ = width - this.picture.GetSize().X - defaultBuffer.X;
      this.header = new ZGenericText(FeatureUnlockDisplayData.GetTitleOfBodyTextForThisNews(unlockType), BaseScale, false, _UseOnePointFiveFont: true);
      this.header.SetAllColours(ColourData.Z_DarkTextGray);
      this.bodyParagraph = new SimpleTextHandler(FeatureUnlockDisplayData.GetBodyParagraphForNews(unlockType), width_, _Scale: BaseScale, AutoComplete: true);
      this.bodyParagraph.SetAllColours(ColourData.Z_DarkTextGray);
      this.size = Vector2.Zero;
      Vector2 size = this.size;
      this.picture.location.Y = size.Y;
      this.picture.location.X = width;
      this.picture.location.X -= this.picture.GetSize().X * 0.5f;
      this.header.vLocation = this.size;
      this.size.Y += this.header.GetSize().Y;
      this.bodyParagraph.Location = this.size;
      this.size.Y += this.bodyParagraph.GetHeightOfParagraph();
      this.size.Y += defaultBuffer.Y;
      this.size.Y = Math.Max(this.size.Y, size.Y);
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void DrawLandExpansionNewsLayout(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.bodyParagraph.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.picture.DrawPictureWithCaption(offset, spriteBatch);
    }
  }
}
