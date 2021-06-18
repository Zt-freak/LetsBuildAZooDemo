// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.PeopleDisplay.PeopleLine
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.DragHandlers;
using SEngine.Input;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;

namespace TinyZoo.ProfitLadder.PeopleDisplay
{
  internal class PeopleLine
  {
    private List<EnemyRenderer> enemyrenderers;
    public float Height;
    private string DrawThis;
    private GameObject TextObject;
    private SpringDrag_ZoneManager springdrag;

    public PeopleLine(Player player, int _Hieght, bool IsHoldingCell = false, bool IsGraveyard = false)
    {
      this.Height = (float) _Hieght;
      this.TextObject = new GameObject();
      this.TextObject.SetAllColours(ColourData.FernLemon);
      this.TextObject.scale = 3f * Sengine.UltraWideSreenDownardsMultiplier;
      this.DrawThis = "Inmates in Cell Blocks";
      this.enemyrenderers = new List<EnemyRenderer>();
      if (IsHoldingCell)
      {
        this.DrawThis = "Inmates in Holding Cells";
        for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.holdingcells.Count; ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.holdingcells[index1].prisonercontainer.prisoners.Count; ++index2)
            this.enemyrenderers.Add(new EnemyRenderer(player.prisonlayout.cellblockcontainer.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype, player.prisonlayout.cellblockcontainer.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson.CLIndex));
        }
      }
      else if (IsGraveyard)
      {
        this.DrawThis = "Inmates in Graveyards";
        for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.graveblocks.Count; ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.graveblocks[index1].deadpeople.deadpeople.Count; ++index2)
            this.enemyrenderers.Add(new EnemyRenderer(player.prisonlayout.cellblockcontainer.graveblocks[index1].deadpeople.deadpeople[index2].intakeperson.animaltype, player.prisonlayout.cellblockcontainer.graveblocks[index1].deadpeople.deadpeople[index2].intakeperson.CLIndex));
        }
      }
      else
      {
        for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
            this.enemyrenderers.Add(new EnemyRenderer(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype, player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.CLIndex));
        }
      }
      for (int index = 0; index < this.enemyrenderers.Count; ++index)
      {
        this.enemyrenderers[index].scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
        this.enemyrenderers[index].vLocation = new Vector2((float) (index * 48) * Sengine.ScreenRationReductionMultiplier.Y, 0.0f);
      }
      if (this.enemyrenderers.Count <= 0)
        return;
      if ((double) this.enemyrenderers[this.enemyrenderers.Count - 1].vLocation.X > 850.0)
        this.springdrag = new SpringDrag_ZoneManager((float) (((double) this.enemyrenderers[this.enemyrenderers.Count - 1].vLocation.X - 850.0) * -1.0), new Vector2(0.0f, this.Height - 60f), new Vector2(1024f, 90f), false);
      this.DrawThis = this.DrawThis + " " + (object) this.enemyrenderers.Count;
    }

    public PeopleLine(EVProgress evprogress, Player player)
    {
      this.Height = 650f;
      this.TextObject = new GameObject();
      this.TextObject.SetAllColours(ColourData.FernLemon);
      this.TextObject.scale = 3f * Sengine.UltraWideSreenDownardsMultiplier;
      this.DrawThis = SEngine.Localization.Localization.GetText(string.Format("Progress: {0}/{1}", (object) evprogress.enemiescaught.Count, (object) evprogress.TargetValue));
      if (player.Stats.currentEvent.IsComplete_AndClaimed(player))
        this.DrawThis = this.DrawThis + " (" + SEngine.Localization.Localization.GetText(71) + ")";
      this.enemyrenderers = new List<EnemyRenderer>();
      for (int index = 0; index < evprogress.TargetValue; ++index)
      {
        if (evprogress.enemiescaught.Count > index)
        {
          this.enemyrenderers.Add(new EnemyRenderer(evprogress.enemiescaught[index], -1));
        }
        else
        {
          EnemyRenderer enemyRenderer = new EnemyRenderer(AnimalType.Rabbit, -1);
          enemyRenderer.SetAllColours(0.0f, 0.0f, 0.0f);
          this.enemyrenderers.Add(enemyRenderer);
        }
      }
      for (int index = 0; index < this.enemyrenderers.Count; ++index)
      {
        this.enemyrenderers[index].scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
        this.enemyrenderers[index].vLocation = new Vector2((float) (index * 48) * Sengine.ScreenRationReductionMultiplier.Y, 0.0f);
      }
      if (this.enemyrenderers.Count <= 0 || (double) this.enemyrenderers[this.enemyrenderers.Count - 1].vLocation.X <= 850.0)
        return;
      this.springdrag = new SpringDrag_ZoneManager((float) (((double) this.enemyrenderers[this.enemyrenderers.Count - 1].vLocation.X - 850.0) * -1.0), new Vector2(0.0f, this.Height - 60f), new Vector2(1024f, 90f), false);
    }

    public void UpdatePeopleLine(TouchInput touchinput)
    {
      if (this.springdrag == null)
        return;
      this.springdrag.UpdateSpringDrag_ZoneManager(touchinput, 100f);
    }

    public void DrawPeopleLine()
    {
      Vector2 vector2 = Vector2.Zero;
      if (this.springdrag != null)
        vector2 = this.springdrag.CurrentOffset;
      this.TextObject.vLocation.Y = -60f;
      TextFunctions.DrawTextWithDropShadow(this.DrawThis, this.TextObject.scale, this.TextObject.vLocation + new Vector2(80f, this.Height), this.TextObject.GetColour(), this.TextObject.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      for (int index = 0; index < this.enemyrenderers.Count; ++index)
        this.enemyrenderers[index].ScreenSpaceDrawEnemyRenderer(new Vector2(100f, this.Height) + vector2);
    }
  }
}
