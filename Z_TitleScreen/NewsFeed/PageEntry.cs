// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.PageEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_TitleScreen.NewsFeed.DataManage;

namespace TinyZoo.Z_TitleScreen.NewsFeed
{
  internal class PageEntry
  {
    private GameObject MainImage;
    private SimpleTextHandler simpletext;
    private SimpleTextHandler simpletextBody;
    private float FullHeight;
    private NewsEntry REF_newsentry;
    public Vector2 TopLeft;

    public PageEntry(NewsEntry newsentry, float BaseScale, float Width)
    {
      this.REF_newsentry = newsentry;
      this.MainImage = new GameObject();
      this.MainImage.DrawRect = new Rectangle(0, 0, newsentry.image.Width, newsentry.image.Height);
      this.MainImage.scale = (Width - 50f * BaseScale) / (float) this.MainImage.DrawRect.Width;
      this.MainImage.vLocation.X = 25f * BaseScale;
      float num = 25f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.simpletext = new SimpleTextHandler(newsentry.Heading, Width, _Scale: BaseScale, _UseFontOnePointFive: true, AutoComplete: true);
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.FullHeight = this.MainImage.scale * (float) this.MainImage.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.FullHeight += num;
      this.simpletext.Location = new Vector2(25f * BaseScale, this.FullHeight);
      this.FullHeight += this.simpletext.GetSize().Y;
      this.FullHeight += num;
      this.simpletextBody = new SimpleTextHandler(newsentry.TextBody, Width, _Scale: BaseScale, AutoComplete: true);
      this.simpletextBody.SetAllColours(ColourData.Z_Cream);
      this.simpletextBody.Location = new Vector2(25f * BaseScale, this.FullHeight);
      this.FullHeight += this.simpletextBody.GetSize().Y;
      this.FullHeight += num;
    }

    public float GetHeight() => this.FullHeight;

    public void UpdatePageEntry()
    {
    }

    public void DrawPageEntry(Vector2 Offset, float ALPHA)
    {
      Offset += this.TopLeft;
      this.MainImage.Draw(AssetContainer.pointspritebatch03, this.REF_newsentry.image, Offset, ALPHA);
      this.simpletext.DrawSimpleTextHandler(Offset, ALPHA, AssetContainer.pointspritebatch03);
      this.simpletextBody.DrawSimpleTextHandler(Offset, ALPHA, AssetContainer.pointspritebatch03);
    }
  }
}
