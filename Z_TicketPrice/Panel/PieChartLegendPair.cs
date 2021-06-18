// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Panel.PieChartLegendPair
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_TicketPrice.Panel
{
  internal class PieChartLegendPair
  {
    public Vector2 location;
    private GameObject box;
    private float buffer;
    private ZGenericText textObj;

    public PieChartLegendPair(string Text, Vector3 color, float BaseScale)
    {
      this.buffer = 10f * BaseScale;
      this.textObj = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
      this.textObj.textToWrite = Text;
      this.box = new GameObject();
      this.box.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.box.SetDrawOriginToCentre();
      this.box.scale = BaseScale * 7f;
      this.box.vLocation.X -= this.buffer;
      this.box.vLocation.X -= this.textObj.GetSize().X * 0.5f;
      this.box.SetAllColours(color);
    }

    public float GetHeight() => this.textObj.GetSize().Y;

    public void DrawPieChartLegendPair(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      this.textObj.DrawZGenericText(offset, spriteBatch);
      this.box.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
