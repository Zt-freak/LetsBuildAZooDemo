// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.MoralityElements.MoralityLockArrowWithImage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.Specific.MoralityElements
{
  internal class MoralityLockArrowWithImage
  {
    public Vector2 location;
    private MoralityLockArrow moralityLock;
    private PictureWithCaption moralityGoodPreview;
    private PictureWithCaption moralityBadPreview;
    private Vector2 size;

    public MoralityLockArrowWithImage(float BaseScale)
    {
      this.moralityLock = new MoralityLockArrow(BaseScale * 3f);
      this.moralityGoodPreview = new PictureWithCaption(BaseScale, PictureType.MoralityPreview_Good, false);
      this.moralityBadPreview = new PictureWithCaption(BaseScale, PictureType.MoralityPreview_Bad, false);
      this.size = Vector2.Zero;
      this.size += this.moralityLock.GetSize();
      this.moralityGoodPreview.location = this.moralityLock.GetOffsetToCenterOfMoralityIcons(true);
      this.moralityBadPreview.location = this.moralityLock.GetOffsetToCenterOfMoralityIcons(false);
      this.size.X += this.moralityGoodPreview.GetSize().X * 0.5f;
      this.size.X += this.moralityBadPreview.GetSize().X * 0.5f;
      this.size.Y += this.moralityBadPreview.GetSize().Y - this.moralityLock.GetSizeOfMoralityIconOnly().Y * 0.5f;
    }

    public Vector2 GetSize() => this.size;

    public void DrawMoralityLockArrowWithImage(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.moralityGoodPreview.DrawPictureWithCaption(offset, spriteBatch);
      this.moralityBadPreview.DrawPictureWithCaption(offset, spriteBatch);
      this.moralityLock.DrawMoralityLockArrow(offset, spriteBatch);
    }
  }
}
