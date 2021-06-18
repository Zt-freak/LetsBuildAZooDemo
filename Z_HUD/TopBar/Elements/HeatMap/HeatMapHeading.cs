// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.HeatMap.HeatMapHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HeatMaps;

namespace TinyZoo.Z_HUD.TopBar.Elements.HeatMap
{
  internal class HeatMapHeading
  {
    public Vector2 location;
    private ZGenericText header;
    private TopBarHeaderBase headerbase;
    private ZGenericText smallHeader;

    public HeatMapHeading(float BaseScale, float BarHeight)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = new Vector2(uiScaleHelper.DefaultBuffer.X, uiScaleHelper.ScaleY(4f));
      this.header = new ZGenericText("X", BaseScale, false, _UseOnePointFiveFont: true);
      this.smallHeader = new ZGenericText("Heat Map View", BaseScale, false);
      this.smallHeader.vLocation = vector2_1;
      vector2_1.Y += this.smallHeader.GetSize().Y;
      this.header.vLocation = vector2_1;
      this.headerbase = new TopBarHeaderBase(BaseScale, BarHeight, uiScaleHelper.ScaleX(100f));
      Vector2 vector2_2 = -this.headerbase.GetSize() * 0.5f;
      ZGenericText smallHeader = this.smallHeader;
      smallHeader.vLocation = smallHeader.vLocation + vector2_2;
      ZGenericText header = this.header;
      header.vLocation = header.vLocation + vector2_2;
    }

    public Vector2 GetSize() => this.headerbase.GetSize();

    public void UpdateHeatMapHeading()
    {
      switch (Z_GameFlags.DRAW_heatmaptype)
      {
        case HeatMapType.Water:
        case HeatMapType.Power:
          this.header.textToWrite = "Utility";
          break;
        case HeatMapType.Spending:
          this.header.textToWrite = "Profit";
          break;
        case HeatMapType.Congestion:
          this.header.textToWrite = "Congestion";
          break;
        case HeatMapType.AnimalPrivacy:
          this.header.textToWrite = "Privacy";
          break;
        case HeatMapType.Deco:
          this.header.textToWrite = "Decoration";
          break;
        case HeatMapType.Hygiene:
          this.header.textToWrite = "Hygiene";
          break;
      }
    }

    public void DrawHeatMapHeading(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerbase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.header.DrawZGenericText(offset, spriteBatch);
      this.smallHeader.DrawZGenericText(offset, spriteBatch);
    }
  }
}
