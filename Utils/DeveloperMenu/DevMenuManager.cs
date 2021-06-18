// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.DeveloperMenu.DevMenuManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Data;

namespace TinyZoo.Utils.DeveloperMenu
{
  internal class DevMenuManager
  {
    private DevMenuSetting[] Settings;
    private PeopleAdder peopleadder;
    private Vector2 Offset;
    private AnimalAdder animaladder;
    private bool HasUnlockedAllLand;
    private bool OverworldManagerIsnull;

    public DevMenuManager()
    {
      this.Settings = new DevMenuSetting[16];
      int num1 = 0;
      float num2 = 100f;
      for (int index = 0; index < this.Settings.Length; ++index)
      {
        this.Settings[index] = new DevMenuSetting(Z_DebugFlags.developerOverrides[index], (DeveloperOverrides) index);
        this.Settings[index].Location.Y = (float) (80 * num1) * Sengine.ScreenRationReductionMultiplier.Y;
        this.Settings[index].Location.X = num2;
        ++num1;
        if (num1 > 9)
        {
          num1 = 0;
          num2 += 300f;
        }
      }
      this.Offset = new Vector2(0.0f, 100f);
    }

    public void UpdateDevMenuManager(Player player, float DeltaTime)
    {
      this.OverworldManagerIsnull = false;
      if (OverWorldManager.overworldenvironment == null)
        this.OverworldManagerIsnull = true;
      else if (this.peopleadder != null)
      {
        if (!this.peopleadder.UpdatePeopleAdder(player, DeltaTime))
          return;
        this.peopleadder = (PeopleAdder) null;
      }
      else if (this.animaladder != null)
      {
        if (!this.animaladder.UpdateAnimalAdder(player, DeltaTime))
          return;
        this.animaladder = (AnimalAdder) null;
      }
      else
      {
        for (int index1 = 0; index1 < this.Settings.Length; ++index1)
        {
          if (this.Settings[index1].UpdateDevMenuSetting(player, this.Offset, DeltaTime))
          {
            switch (index1)
            {
              case 2:
                player.Stats.research.DebugUnlockAllResearch();
                continue;
              case 5:
                DebugFlags.HasEndlessMoney = (uint) this.Settings[index1].ThisSetting > 0U;
                continue;
              case 7:
                this.animaladder = new AnimalAdder(player);
                continue;
              case 8:
                this.peopleadder = new PeopleAdder();
                continue;
              case 9:
                if (!this.HasUnlockedAllLand)
                {
                  this.HasUnlockedAllLand = true;
                  for (int _X = 1; _X < PlayerStats.unblockedSectors.GetLength(0) - 1; ++_X)
                  {
                    for (int _Y = 1; _Y < PlayerStats.unblockedSectors.GetLength(1); ++_Y)
                    {
                      if (!PlayerStats.unblockedSectors[_X, _Y])
                        ZMapSetUp.UnlockThisSector(player, new Vector2Int(_X, _Y), OverWorldManager.overworldenvironment, false);
                    }
                  }
                  OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
                  Z_DebugFlags.SimulationIsVerbose = false;
                  continue;
                }
                continue;
              case 10:
                for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index2)
                  player.prisonlayout.cellblockcontainer.prisonzones[index2].prisonercontainer.ClearTheDead();
                continue;
              case 11:
                DebugFlags.SaveGame = true;
                player.SaveThisPlayer(DelayUntilNextFrame: false, IsEndOfDay: true);
                continue;
              case 14:
                DebugFlags.HideAllUI_DEBUG = (uint) this.Settings[index1].ThisSetting > 0U;
                continue;
              case 15:
                Z_DebugFlags.DisableMoralityBlocks = Z_DebugFlags.developerOverrides[index1] == 1;
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    public void DrawDevMenuManager()
    {
      if (this.OverworldManagerIsnull)
        TextFunctions.DrawJustifiedText("Wait until game has started to use debug menu", 1f, new Vector2(512f, 600f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch07Final);
      else if (this.peopleadder != null)
        this.peopleadder.DrawPeopleAdder();
      else if (this.animaladder != null)
      {
        this.animaladder.DrawAnimalAdder();
      }
      else
      {
        for (int index = 0; index < this.Settings.Length; ++index)
          this.Settings[index].DrawDevMenuSetting(this.Offset);
      }
    }
  }
}
