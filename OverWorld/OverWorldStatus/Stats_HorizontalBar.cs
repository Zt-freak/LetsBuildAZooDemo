// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldStatus.Stats_HorizontalBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldStatus.StatsRend;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.OverWorldStatus
{
  internal class Stats_HorizontalBar
  {
    private StatBar[] teststatbars;
    private Vector2 Location;
    public float LengthOfBar;
    private int LastSelected;

    public Stats_HorizontalBar(Player player)
    {
      this.teststatbars = new StatBar[player.Stats.GetNumberOfAvailableNeeds()];
      int index1 = 0;
      for (int index2 = 0; index2 < 10; ++index2)
      {
        if (player.Stats.AvailableNeedsCategories[index2])
        {
          this.teststatbars[index1] = new StatBar((ProductionType) index2);
          this.teststatbars[index1].Location.X += 30f;
          this.teststatbars[index1].Location.X += (float) (63 * index1);
          ++index1;
        }
      }
      this.LengthOfBar = this.teststatbars[this.teststatbars.Length - 1].Location.X + 30f;
      this.LastSelected = 0;
    }

    public void UpdateStats_HorizontalBar(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      bool BarIsOut)
    {
      if (OverwoldMainButtons.SelectedNeed != this.LastSelected)
      {
        if (BarIsOut)
        {
          this.LastSelected = OverwoldMainButtons.SelectedNeed;
          if (OverwoldMainButtons.SelectedNeed == 0)
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          else
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.7f, 1f);
        }
        else
        {
          OverwoldMainButtons.SelectedNeed = 0;
          this.LastSelected = OverwoldMainButtons.SelectedNeed;
        }
      }
      for (int ArrayIndex = 0; ArrayIndex < this.teststatbars.Length; ++ArrayIndex)
      {
        if (this.teststatbars[ArrayIndex].UpdateStatBar(player, this.Location + Offset, DeltaTime, ArrayIndex) && TutorialManager.currenttutorial == TUTORIALTYPE.None)
          GameStateManager.tutorialmanager.PopUpStatExplanaition(this.teststatbars[ArrayIndex].productiontype, player);
      }
    }

    public void DrawStats_HorizontalBar(Vector2 Offset)
    {
      this.Location.X = 60f;
      if (OverwoldMainButtons.SelectedNeed > this.teststatbars.Length)
        OverwoldMainButtons.SelectedNeed = this.teststatbars.Length;
      for (int ArrayIndex = 0; ArrayIndex < this.teststatbars.Length; ++ArrayIndex)
        this.teststatbars[ArrayIndex].DrawStatBar(this.Location + Offset, ArrayIndex);
    }
  }
}
