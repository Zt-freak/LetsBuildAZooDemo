// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.CubeLayoutController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_WeekOver.V2.Cubes;
using TinyZoo.Z_WeekOver.V2.Cubes.Cubes;
using TinyZoo.Z_WeekOver.V2.Cubes.Cubes.EndOfWeek;

namespace TinyZoo.Z_WeekOver.V2
{
  internal class CubeLayoutController
  {
    private List<BaseCube> cubes;
    private Vector2 TopLeftTileCenter;
    private CurrentFinances currentfinances;

    public CubeLayoutController(float BaseScale, Player player)
    {
      this.cubes = new List<BaseCube>();
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.currentfinances = new CurrentFinances(baseScaleForUi, player);
      this.cubes.Add((BaseCube) this.currentfinances);
      this.cubes[0].SetToLocation(new Vector2(0.0f, 0.5f));
      this.cubes.Add((BaseCube) new PayTheStaff(baseScaleForUi, player));
      this.cubes[this.cubes.Count - 1].SetToLocation(new Vector2(1f, 0.5f));
      this.cubes.Add((BaseCube) new LoanRepayment(baseScaleForUi, player));
      this.cubes[this.cubes.Count - 1].SetToLocation(new Vector2(2f, 0.5f));
      this.cubes.Add((BaseCube) new IncomeCube(baseScaleForUi, player, IncomeCubeType.Income));
      this.cubes[this.cubes.Count - 1].SetToLocation(new Vector2(3f, 0.5f));
      this.cubes.Add((BaseCube) new IncomeCube(baseScaleForUi, player, IncomeCubeType.Profit));
      this.cubes[this.cubes.Count - 1].SetToLocation(new Vector2(4f, 0.0f));
      this.cubes.Add((BaseCube) new IncomeCube(baseScaleForUi, player, IncomeCubeType.Expanditure));
      this.cubes[this.cubes.Count - 1].SetToLocation(new Vector2(4f, 1f));
      this.TopLeftTileCenter.X = baseScaleForUi * -2f * EndPOfWeekSummaryManager.SIZE;
      this.TopLeftTileCenter.Y = baseScaleForUi * -0.5f * EndPOfWeekSummaryManager.SIZE * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void UpdateCubeLayoutController(
      Player player,
      float DeltaTime,
      Vector2 BrownPanelCentre,
      out bool BlockClose)
    {
      BlockClose = true;
      if ((double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
        return;
      BrownPanelCentre += this.TopLeftTileCenter;
      BlockClose = false;
      for (int index = 0; index < this.cubes.Count; ++index)
      {
        if (index == 0 && !this.cubes[0].LerpComplete(this.currentfinances))
          BlockClose = true;
        if (index > 0 && !this.cubes[index - 1].LerpComplete(this.currentfinances))
          BlockClose = true;
        else
          this.cubes[index].UpdateBaseCube(DeltaTime, player, BrownPanelCentre);
      }
    }

    public void DrawCubeLayoutController(Vector2 BrownPanelCentre)
    {
      BrownPanelCentre += this.TopLeftTileCenter;
      for (int index = 0; index < this.cubes.Count; ++index)
        this.cubes[index].DrawBaseCube(BrownPanelCentre, AssetContainer.pointspritebatchTop05);
    }
  }
}
