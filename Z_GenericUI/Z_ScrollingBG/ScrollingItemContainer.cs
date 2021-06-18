// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.Z_ScrollingBG.ScrollingItemContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TinyZoo.Z_GenericUI.Z_ScrollingBG
{
  internal class ScrollingItemContainer
  {
    public Vector2 location;
    private List<ScrollingItem> items;
    private UIScaleHelper scaleHelper;

    public ScrollingItemContainer(float BaseScale) => this.scaleHelper = new UIScaleHelper(BaseScale);

    public void SetUpStripsAsStaggeredPairs(Vector2 vScale, float BaseScale)
    {
      int minValue = 10;
      int maxValue = 25;
      int num1 = 4;
      float num2 = 5f;
      float num3 = 10f;
      int num4 = 3;
      this.items = new List<ScrollingItem>();
      float num5 = vScale.X / (float) num1;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        float x = (float) ((double) num5 * (double) index1 + (double) num5 * 0.5);
        float y = (float) -Game1.Rnd.Next(0, (int) vScale.Y);
        float _scrollSpeed_scaled = this.scaleHelper.ScaleY((float) Game1.Rnd.Next(minValue, maxValue));
        for (int index2 = 0; index2 < num4 * 2; ++index2)
        {
          ScrollingItem scrollingItem = new ScrollingItem(BaseScale, ColourData.ACDarkerYellow, vScale.Y, _scrollSpeed_scaled);
          scrollingItem.vLocation = new Vector2(x, y);
          scrollingItem.vLocation.Y += (float) ((double) scrollingItem.GetSize().Y * (double) index2 * 0.5 + (double) this.scaleHelper.ScaleY(num3 * 0.5f) * (double) index2);
          if (index2 % 2 == 0)
            scrollingItem.vLocation.X -= (float) (((double) scrollingItem.GetSize().X + (double) num2) * 0.5);
          else
            scrollingItem.vLocation.X += (float) (((double) scrollingItem.GetSize().X + (double) num2) * 0.5);
          this.items.Add(scrollingItem);
        }
      }
    }

    public void UpdateScrollingItemContainer(float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.items.Count; ++index)
        this.items[index].UpdateScrollingItem(DeltaTime, offset);
    }

    public void DrawScrollingItemContainer(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.items.Count; ++index)
        this.items[index].DrawScrollingItem(offset, spriteBatch);
    }
  }
}
