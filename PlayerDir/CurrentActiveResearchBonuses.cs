// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CurrentActiveResearchBonuses
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_Research_.RData;

namespace TinyZoo.PlayerDir
{
  internal class CurrentActiveResearchBonuses
  {
    public int[] TypesOfUpgradeAndLevel;

    public CurrentActiveResearchBonuses() => this.TypesOfUpgradeAndLevel = new int[6];

    public void RecountSetsAndCreateUnlocks(Player player)
    {
      this.TypesOfUpgradeAndLevel = new int[6];
      for (int index1 = 0; index1 < 41; ++index1)
      {
        ResearchUpgradeInfoSet upgradeTiers = RGrid_Data.GetUpgradeTiers((UpgradeCategory) index1);
        int unlockedFromThisSet = RGrid_Data.GetNumberUnlockedFromThisSet((UpgradeCategory) index1, player, out int _);
        int num = 0;
        if (upgradeTiers != null)
        {
          for (int index2 = 0; index2 < upgradeTiers.researchinfo.Count && unlockedFromThisSet >= upgradeTiers.researchinfo[index2].TotalInSet; ++index2)
            num += upgradeTiers.researchinfo[index2].TotalInSet;
          if (upgradeTiers.upgradetype != ResearchUpgradeType.Count)
            this.TypesOfUpgradeAndLevel[(int) upgradeTiers.upgradetype] += num;
        }
      }
    }
  }
}
