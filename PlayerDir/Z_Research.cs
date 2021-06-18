// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Z_Research
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Architcture;
using TinyZoo.Z_Architcture.RData;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir
{
  internal class Z_Research
  {
    public int DaysLeft;
    public TILETYPE ResearchingThis = TILETYPE.Count;
    internal static List<Employee> researchers;

    public bool IsResearching() => this.ResearchingThis != TILETYPE.Count;

    internal static void ResetResearchGuys() => Z_Research.researchers = new List<Employee>();

    internal static void AddResearchGuy(Employee employee, bool IsAdd)
    {
      if (Z_Research.researchers == null)
        Z_Research.researchers = new List<Employee>();
      if (IsAdd)
      {
        Z_Research.researchers.Add(employee);
      }
      else
      {
        if (!Z_Research.researchers.Contains(employee))
          throw new Exception("NO WAAAAY REsearcher not here");
        Z_Research.researchers.Remove(employee);
      }
    }

    public float GetResearchProgressForHUDAbstract()
    {
      int num = 0;
      for (int index = 0; index < Z_Research.researchers.Count; ++index)
      {
        if (Z_Research.researchers[index].ResearchProgress > num)
          num = Z_Research.researchers[index].ResearchProgress;
      }
      return (float) num * 0.01f;
    }

    public void StartNewDay(Player player)
    {
      if (this.DaysLeft <= 0)
        return;
      --this.DaysLeft;
      if (this.DaysLeft > 0)
        return;
      ResearchSet researchSet = ArchitectResearchData.GetResearchSet(CATEGORYTYPE.Enclosure);
      bool flag1 = false;
      for (int index = 0; index < researchSet.entries.Count; ++index)
      {
        if (researchSet.entries[index].ThingToDiscover == this.ResearchingThis)
          flag1 = true;
      }
      bool flag2 = false;
      if (flag1 && !player.Stats.research.CellBlocksReseacrhed.Contains(this.ResearchingThis))
      {
        flag2 = true;
        player.Stats.research.CellBlocksReseacrhed.Add(this.ResearchingThis);
      }
      else if (!flag1 && !player.Stats.research.BuildingsResearched.Contains(this.ResearchingThis))
      {
        flag2 = true;
        player.Stats.research.BuildingsResearched.Add(this.ResearchingThis);
      }
      if (!flag2)
        return;
      Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.F_ResearchComplete, this.ResearchingThis), player);
      this.ResearchingThis = TILETYPE.Count;
    }

    public void StartResearch(TILETYPE researchthis, int _Days)
    {
      this.DaysLeft = _Days;
      this.ResearchingThis = researchthis;
    }
  }
}
