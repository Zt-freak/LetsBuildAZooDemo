// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.Z_ScrollingBG.ScrollingBGFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI.Z_ScrollingBG
{
  internal class ScrollingBGFrame : CustomerFrame
  {
    private ScrollingItemContainer scrollingItems;
    private UIScaleHelper scaleHelper;
    private float BaseScale;

    public ScrollingBGFrame(Vector2 vScale, float _BaseScale)
      : base(vScale, ColourData.ACPaleYellow, _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.scrollingItems = new ScrollingItemContainer(this.BaseScale);
      this.scrollingItems.SetUpStripsAsStaggeredPairs(vScale, this.BaseScale);
      this.scrollingItems.location = new Vector2((float) (-(double) vScale.X * 0.5), vScale.Y * 0.5f);
    }

    public void UpdateScrollingBGFrame(float DeltaTime, Vector2 offset) => this.scrollingItems.UpdateScrollingItemContainer(DeltaTime, offset);

    public void DrawScrollingBGFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawCustomerFrame(offset, spriteBatch);
      this.scrollingItems.DrawScrollingItemContainer(offset, spriteBatch);
    }
  }
}
