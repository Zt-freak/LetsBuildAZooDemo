// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Unlocks
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_BalanceSystems.Publicity;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Research_.RData;

namespace TinyZoo.PlayerDir
{
  internal class Unlocks
  {
    public int[] UnlockedThings;
    private int _ResearchPoints;
    public HybridCombinations[] hybridAnimalsDiscovered;

    public Unlocks() => this.Create();

    public int ResearchPoints
    {
      get => Z_DebugFlags.developerOverrides[1] > 0 ? 99 : this._ResearchPoints;
      set => this._ResearchPoints = value;
    }

    public void UnlockThis(REntry REF_selection, Player player)
    {
      int publicity1 = PublicityCalculator.CalculatePublicity(player);
      this.UnlockedThings[(int) REF_selection.unlocktype] = (int) Player.financialrecords.GetDaysPassed();
      this.UnlockBuildingsForThisThing(REF_selection, player);
      PublicityCalculator.RecalculatePublicity = true;
      ParkRating.NeedsRecalculating = true;
      int publicity2 = PublicityCalculator.CalculatePublicity(player);
      NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
      Player.currentActiveResearchBonuses.RecountSetsAndCreateUnlocks(player);
    }

    public void UnlockBuildingsForThisThing(REntry REF_selection, Player player)
    {
      for (int index = 0; index < REF_selection.WillUnlockThese.Count; ++index)
      {
        TILETYPE tiletype = REF_selection.WillUnlockThese[index];
        if (TileData.IsThisACellBlock(tiletype))
        {
          if (!player.Stats.research.CellBlocksReseacrhed.Contains(tiletype) || Z_DebugFlags.UnlockAllBuildingsHack)
            player.Stats.research.CellBlocksReseacrhed.Add(tiletype);
        }
        else if (!player.Stats.research.BuildingsResearched.Contains(tiletype) || Z_DebugFlags.UnlockAllBuildingsHack)
          player.Stats.research.BuildingsResearched.Add(tiletype);
      }
    }

    public bool TryUnlockNewHybrid(
      AnimalType body,
      AnimalType head,
      int BodyVariant,
      int HeadVariant)
    {
      bool flag = false;
      HybridAnimal hybridAnimal = new HybridAnimal(head, BodyVariant, HeadVariant, (int) Player.financialrecords.GetDaysPassed());
      if (this.hybridAnimalsDiscovered[(int) body] == null)
      {
        this.hybridAnimalsDiscovered[(int) body] = new HybridCombinations();
      }
      else
      {
        foreach (HybridAnimal combination in this.hybridAnimalsDiscovered[(int) body].combinations)
        {
          if (combination.HeadAnimalType == hybridAnimal.HeadAnimalType && combination.BodyVariant == hybridAnimal.BodyVariant && combination.HeadVariant == hybridAnimal.HeadVariant)
          {
            flag = true;
            break;
          }
        }
      }
      if (flag)
        return false;
      this.hybridAnimalsDiscovered[(int) body].combinations.Add(hybridAnimal);
      return true;
    }

    public bool GetIsHybridDiscovered(
      AnimalType body,
      AnimalType head,
      int BodyVariant,
      int HeadVariant)
    {
      if (this.hybridAnimalsDiscovered[(int) body] != null)
      {
        foreach (HybridAnimal combination in this.hybridAnimalsDiscovered[(int) body].combinations)
        {
          if (combination.HeadAnimalType == head && combination.BodyVariant == BodyVariant && combination.HeadVariant == HeadVariant)
            return true;
        }
      }
      return false;
    }

    public List<AnimalRenderDescriptor> GetAllHybridsDiscoveredForThisAnimalType(
      AnimalType bodyAnimalType)
    {
      List<AnimalRenderDescriptor> renderDescriptorList = new List<AnimalRenderDescriptor>();
      if (this.hybridAnimalsDiscovered[(int) bodyAnimalType] != null)
      {
        foreach (HybridAnimal combination in this.hybridAnimalsDiscovered[(int) bodyAnimalType].combinations)
          renderDescriptorList.Add(new AnimalRenderDescriptor(bodyAnimalType, combination.BodyVariant, combination.HeadAnimalType, combination.HeadVariant));
      }
      return renderDescriptorList;
    }

    private void Create()
    {
      this.hybridAnimalsDiscovered = new HybridCombinations[56];
      for (int index = 0; index < this.hybridAnimalsDiscovered.Length; ++index)
        this.hybridAnimalsDiscovered[index] = new HybridCombinations();
      this.UnlockedThings = new int[249];
      for (int index = 0; index < 249; ++index)
        this.UnlockedThings[index] = -1;
    }

    public int GetTotalResearchUnlocked()
    {
      int num = 0;
      for (int index = 0; index < this.UnlockedThings.Length; ++index)
      {
        if (this.UnlockedThings[index] > 1)
          ++num;
      }
      return num;
    }

    public Unlocks(Reader reader)
    {
      int num1 = (int) reader.ReadInt("r", ref this._ResearchPoints);
      int _out = 0;
      this.Create();
      int num2 = (int) reader.ReadInt("r", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        int num3 = (int) reader.ReadInt("r", ref this.UnlockedThings[index]);
      }
      int num4 = (int) reader.ReadInt("r", ref _out);
      for (int index = 0; index < _out; ++index)
        this.hybridAnimalsDiscovered[index] = new HybridCombinations(reader);
    }

    public void SaveUnlocks(Writer writer)
    {
      writer.WriteInt("r", this._ResearchPoints);
      writer.WriteInt("r", this.UnlockedThings.Length);
      for (int index = 0; index < this.UnlockedThings.Length; ++index)
        writer.WriteInt("r", this.UnlockedThings[index]);
      writer.WriteInt("r", this.hybridAnimalsDiscovered.Length);
      for (int index = 0; index < this.hybridAnimalsDiscovered.Length; ++index)
        this.hybridAnimalsDiscovered[index].SaveHybridCombinations(writer);
    }
  }
}
