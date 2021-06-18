// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.HeatMap.HeatMapDescription
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_HeatMaps;

namespace TinyZoo.Z_HUD.TopBar.Elements.HeatMap
{
  internal class HeatMapDescription
  {
    public Vector2 location;
    private SimpleTextHandler desc;
    private HeatMapType lastHeatMapType;
    private float BaseScale;
    private float width;

    public HeatMapDescription(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.lastHeatMapType = HeatMapType.None;
      this.width = 500f * this.BaseScale;
    }

    private void SetUpNewHeatMapTypeDesc(HeatMapType heatMapType)
    {
      string TextToWrite = string.Empty;
      switch (heatMapType)
      {
        case HeatMapType.Water:
          TextToWrite = "Utility Map - Shows the ground covered by the water pump. Water troughs in enclosures need to be within the water zones to have a water supply.";
          break;
        case HeatMapType.Power:
          TextToWrite = "Power Map - Unavailable for Beta.";
          break;
        case HeatMapType.Spending:
          TextToWrite = "Spending Map - Unavailable for Beta.";
          break;
        case HeatMapType.Congestion:
          TextToWrite = "Congestion Map - Unavailable for Beta.";
          break;
        case HeatMapType.AnimalPrivacy:
          TextToWrite = "Privacy Map - Your animals require enough privacy to be comfortable. Build them shelters or things to hide behind when they need to rest.";
          break;
        case HeatMapType.Deco:
          TextToWrite = "Deco Map - Shows Decoration Score.";
          break;
        case HeatMapType.Hygiene:
          TextToWrite = "Hygiene Map - Shows the Hygiene rating of your establishments.";
          break;
      }
      this.desc = new SimpleTextHandler(TextToWrite, this.width, _Scale: this.BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.lastHeatMapType = heatMapType;
    }

    public float GetWidth() => this.width;

    public void UpdateHeatMapDescription()
    {
      if (Z_GameFlags.DRAW_heatmaptype == this.lastHeatMapType)
        return;
      this.SetUpNewHeatMapTypeDesc(Z_GameFlags.DRAW_heatmaptype);
    }

    public void DrawHeatMapDescription(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.desc == null)
        return;
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
