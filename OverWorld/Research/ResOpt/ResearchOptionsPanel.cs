// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Research.ResOpt.ResearchOptionsPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.Research.ResOpt
{
  internal class ResearchOptionsPanel
  {
    private List<ResearchOption> researchoptions;
    private ButtonRepeater repeater;
    private int SelectedWithController;

    public ResearchOptionsPanel(Player player)
    {
      this.researchoptions = new List<ResearchOption>();
      int TargetRank;
      bool researchNextAien = Researcher.IsHighEnoughRankToResearchNextAien(player, out TargetRank);
      this.researchoptions.Add(new ResearchOption(ResearchType.Alien, player.Stats.research.IsComplete(ResearchType.Alien), player.Stats.research.GetGetNextResearchTimeDisplay(ResearchType.Alien, player.prisonlayout.GetTotalResearch()), player, researchNextAien, TargetRank));
      this.researchoptions.Add(new ResearchOption(ResearchType.CellType, player.Stats.research.IsComplete(ResearchType.CellType), player.Stats.research.GetGetNextResearchTimeDisplay(ResearchType.CellType, player.prisonlayout.GetTotalResearch()), player));
      this.researchoptions.Add(new ResearchOption(ResearchType.Building, player.Stats.research.IsComplete(ResearchType.Building), player.Stats.research.GetGetNextResearchTimeDisplay(ResearchType.Building, player.prisonlayout.GetTotalResearch()), player));
      if (player.Stats.research.IsCellBlockResearchBlocked())
        this.researchoptions[1].SetPartialComplete();
      for (int index = 0; index < this.researchoptions.Count; ++index)
        this.researchoptions[index].location = new Vector2(512f, (float) (150.0 + 150.0 * (double) Sengine.UltraWideSreenUpwardsMultiplier + (double) (index * 120) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * (double) Sengine.UltraWideSreenDownardsMultiplier));
      this.repeater = new ButtonRepeater();
      this.SelectedWithController = 0;
    }

    public bool UpdateResearchOptionsPanel(Player player, float DeltaTime, Vector2 Offset)
    {
      DirectionPressed Direction;
      if (GameFlags.IsUsingController && this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
      {
        switch (Direction)
        {
          case DirectionPressed.Up:
            if (this.SelectedWithController > 0)
            {
              --this.SelectedWithController;
              break;
            }
            break;
          case DirectionPressed.Down:
            if (this.SelectedWithController < 2)
            {
              ++this.SelectedWithController;
              break;
            }
            break;
        }
      }
      bool ExitComplete = true;
      for (int index1 = 0; index1 < this.researchoptions.Count; ++index1)
      {
        if (this.researchoptions[index1].UpdateResearchOption(player, DeltaTime, Offset, ref ExitComplete, GameFlags.IsUsingController && this.SelectedWithController == index1))
        {
          player.Stats.research.beginResearch(this.researchoptions[index1].reearchtype, player);
          for (int index2 = 0; index2 < this.researchoptions.Count; ++index2)
            this.researchoptions[index2].Exit();
        }
      }
      return ExitComplete && this.researchoptions.Count > 0;
    }

    public void DrawResearchOptionsPanel(Vector2 Offset)
    {
      for (int index = 0; index < this.researchoptions.Count; ++index)
        this.researchoptions[index].DrawResearchOption(Offset, GameFlags.IsUsingController && this.SelectedWithController == index);
    }
  }
}
