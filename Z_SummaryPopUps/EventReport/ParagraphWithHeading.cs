// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ParagraphWithHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ParagraphWithHeading
  {
    private ZGenericText heading;
    private SimpleTextHandler body;
    private GameObject line;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private float width;

    public ParagraphWithHeading(string headingstr, string bodystr, float width_, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.width = width_;
      this.heading = new ZGenericText(headingstr, this.basescale, false, _UseOnePointFiveFont: true);
      this.heading.SetAllColours(ColourData.Z_DarkTextGray);
      this.body = new SimpleTextHandler(bodystr, this.width, _Scale: this.basescale, _UseFontOnePointFive: true);
      this.body.SetAllColours(ColourData.Z_DarkTextGray);
      this.body.AutoCompleteParagraph();
      this.line = new GameObject();
      this.line.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.line.SetAllColours(ColourData.Z_DarkTextGray);
      this.framescale = new Vector2();
      this.framescale = this.body.GetSize();
      this.framescale.Y += this.heading.GetSize().Y + defaultBuffer.Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      Vector2 vector2_1 = -0.5f * this.framescale;
      this.heading.vLocation = vector2_1;
      vector2_1.Y += this.heading.GetSize().Y;
      this.line.vLocation = vector2_1;
      vector2_1.Y += defaultBuffer.Y;
      this.body.Location = vector2_1;
      Vector2 vector2_2 = vector2_1 + this.body.GetSize();
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateParagraphWithHeading(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawParagraphWithHeading(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.heading.DrawZGenericText(offset, spritebatch);
      this.line.Draw(spritebatch, AssetContainer.SpriteSheet, offset, new Vector2(this.width, 1f));
      this.body.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
