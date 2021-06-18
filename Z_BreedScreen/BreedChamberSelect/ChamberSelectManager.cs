// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChamberSelect.ChamberSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Tutorials;
using TinyZoo.Z_BreedResult;

namespace TinyZoo.Z_BreedScreen.BreedChamberSelect
{
  internal class ChamberSelectManager
  {
    private BreedStatusButton[] breederbuttons;
    private LerpHandler_Float lerper;
    private int SelectedIndex = -1;
    private ScreenHeading screenheading;

    public ChamberSelectManager(Player player, bool WillLerpOn = false)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.Breeding)
        this.screenheading = new ScreenHeading("BREEDING HUTCH", 70f);
      this.lerper = new LerpHandler_Float();
      this.breederbuttons = new BreedStatusButton[3];
      for (int Index = 0; Index < 3; ++Index)
        this.breederbuttons[Index] = new BreedStatusButton(Index, player);
      if (!WillLerpOn)
        return;
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public int UpdateChamberSelectManager(Vector2 Offset, Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      int num = -1;
      Offset.X += this.lerper.Value * 1024f;
      if (this.SelectedIndex == -1)
      {
        for (int index = 0; index < this.breederbuttons.Length; ++index)
        {
          if (this.breederbuttons[index].UpdateBreedStatusButton(player, DeltaTime, Offset))
          {
            this.SelectedIndex = index;
            this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
          }
          if (BreedResultManager.newthingget != null)
          {
            if (TinyZoo.Game1.GetNextGameState() != GAMESTATE.NewBreedSetUp)
            {
              TinyZoo.Game1.SetNextGameState(GAMESTATE.NewBreedSetUp);
              TinyZoo.Game1.screenfade.BeginFade(true);
            }
            return -1;
          }
        }
      }
      else if ((double) this.lerper.TargetValue == 1.0)
        return this.SelectedIndex;
      return num;
    }

    public void DrawChamberSelectManager(Vector2 Offset)
    {
      if (this.screenheading != null)
        this.screenheading.DrawScreenHeading(Offset, AssetContainer.pointspritebatch03);
      Offset.X += this.lerper.Value * 1024f;
      for (int index = 0; index < this.breederbuttons.Length; ++index)
        this.breederbuttons[index].DrawBreedStatusButton(Offset);
    }
  }
}
