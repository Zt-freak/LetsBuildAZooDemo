// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.HeatMap.HeatMapTopBarElements
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HeatMaps;

namespace TinyZoo.Z_HUD.TopBar.Elements.HeatMap
{
  internal class HeatMapTopBarElements
  {
    public Vector2 location;
    private HeatMapHeading heading;
    private HeatMapDescription desc;
    private BackButton closeButton;
    private List<OWCategoryButton> categoryButton;
    private LerpHandler_Float lerper;

    public HeatMapTopBarElements(float BaseScale, float BarHeight, float frameHeight)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 vector2_1 = new Vector2(defaultBuffer.X, BarHeight * 0.5f);
      this.heading = new HeatMapHeading(BaseScale, frameHeight);
      this.categoryButton = new List<OWCategoryButton>();
      this.lerper = new LerpHandler_Float();
      this.closeButton = new BackButton(true, BaseScale: BaseScale);
      List<OverworldButtons> overworldButtonsList = new List<OverworldButtons>()
      {
        OverworldButtons.HeatMap_Utility,
        OverworldButtons.HeatMap_Profit,
        OverworldButtons.HeatMap_Hygiene,
        OverworldButtons.HeatMap_Congestion,
        OverworldButtons.HeatMap_Privacy,
        OverworldButtons.HeatMap_Deco
      };
      if (Z_DebugFlags.IsBetaVersion)
        overworldButtonsList = new List<OverworldButtons>()
        {
          OverworldButtons.HeatMap_Utility,
          OverworldButtons.HeatMap_Deco
        };
      this.desc = new HeatMapDescription(BaseScale);
      this.heading.location = vector2_1;
      this.heading.location.X += this.heading.GetSize().X * 0.5f;
      vector2_1.X += this.heading.GetSize().X;
      vector2_1.X += defaultBuffer.X;
      this.desc.location = vector2_1;
      this.desc.location.Y -= this.heading.GetSize().Y * 0.5f;
      vector2_1.X += this.desc.GetWidth();
      Vector2 vector2_2 = new Vector2(1024f - defaultBuffer.X, BarHeight * 0.5f);
      this.closeButton.vLocation = vector2_2;
      this.closeButton.vLocation.X -= this.closeButton.GetSize().X * 0.5f;
      vector2_2.X -= this.closeButton.GetSize().X;
      vector2_2.X -= defaultBuffer.X * 1.2f;
      for (int index = overworldButtonsList.Count - 1; index > -1; --index)
      {
        OWCategoryButton owCategoryButton = new OWCategoryButton(overworldButtonsList[index], BaseScale);
        owCategoryButton.Location = vector2_2;
        owCategoryButton.Location.X -= owCategoryButton.GetSize().X * 0.5f;
        vector2_2.X -= owCategoryButton.GetSize().X + defaultBuffer.X;
        this.categoryButton.Add(owCategoryButton);
      }
    }

    public void LerpIn()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return;
      this.lerper.SetLerp(false, -1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if ((double) this.lerper.TargetValue == -1.0)
        return;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f);
    }

    public void UpdateHeatMapTopBarElements(Player player, float DeltaTime, Vector2 offset)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.heading.UpdateHeatMapHeading();
      HeatMapType heatmap = HeatMapType.None;
      for (int index = 0; index < this.categoryButton.Count; ++index)
      {
        if (this.categoryButton[index].UpdateOWCategoryButton(DeltaTime, player, offset))
        {
          switch (this.categoryButton[index].buttontype)
          {
            case OverworldButtons.HeatMap_Privacy:
              heatmap = HeatMapType.AnimalPrivacy;
              break;
            case OverworldButtons.HeatMap_Utility:
              heatmap = HeatMapType.Water;
              break;
            case OverworldButtons.HeatMap_Hygiene:
              heatmap = HeatMapType.Hygiene;
              Z_GameFlags.MustRebuildHygieneMap = true;
              break;
            case OverworldButtons.HeatMap_Profit:
              heatmap = HeatMapType.Spending;
              break;
            case OverworldButtons.HeatMap_Congestion:
              heatmap = HeatMapType.Congestion;
              break;
            case OverworldButtons.HeatMap_Deco:
              heatmap = HeatMapType.Deco;
              HeatMapManager.decomap = new DecoMap(player);
              break;
          }
          Z_GameFlags.SetHeatMapType(heatmap);
        }
      }
      this.desc.UpdateHeatMapDescription();
      if (Z_GameFlags.DRAW_heatmaptype == HeatMapType.None || !this.closeButton.UpdateBackButton(player, DeltaTime, offset))
        return;
      Z_GameFlags.SetHeatMapType(HeatMapType.None);
    }

    public void DrawHeatMapTopBarElements(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.heading.DrawHeatMapHeading(offset, spriteBatch);
      this.desc.DrawHeatMapDescription(offset, spriteBatch);
      for (int index = 0; index < this.categoryButton.Count; ++index)
        this.categoryButton[index].DrawOWCategoryButton(offset, spriteBatch);
      this.closeButton.DrawBackButton(offset, spriteBatch);
    }
  }
}
