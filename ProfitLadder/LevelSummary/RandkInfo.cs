// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.LevelSummary.RandkInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.Research;
using TinyZoo.PlayerDir;

namespace TinyZoo.ProfitLadder.LevelSummary
{
  internal class RandkInfo
  {
    public int IncomeMin;
    public StringID stringID;
    public int ResearchAlienLimit;
    public Rectangle DrawRectForIcon;

    public RandkInfo(int _ResearchAlienLimit, Rectangle _DrawRectForIcon, StringID _stringID)
    {
      this.stringID = _stringID;
      this.DrawRectForIcon = _DrawRectForIcon;
      this.ResearchAlienLimit = _ResearchAlienLimit;
    }

    public RandkInfo(int _ResearchAlienLimit) => this.ResearchAlienLimit = _ResearchAlienLimit;

    public void CalculateRank(int RankIndex)
    {
      List<AnimalType> reseachedInOrder = ResearchData.GetAliensReseachedInOrder();
      this.IncomeMin = 0;
      for (int index = 0; index < this.ResearchAlienLimit; ++index)
      {
        if (index == 0)
          this.IncomeMin += LiveStats.reqforpeople.wantsbyperson[0].VPM.GetUnvallidatedValue() * 3;
        int num = 3;
        if (index == this.ResearchAlienLimit - 1)
          num = 1;
        this.IncomeMin += LiveStats.reqforpeople.wantsbyperson[(int) reseachedInOrder[index]].VPM.GetUnvallidatedValue() * num;
      }
      switch (RankIndex)
      {
        case 1:
          this.IncomeMin = 150;
          break;
        case 2:
          this.IncomeMin = 500;
          break;
        case 3:
          this.IncomeMin = 1250;
          break;
        case 4:
          this.IncomeMin = 2500;
          break;
        case 5:
          this.IncomeMin = 4500;
          break;
      }
    }
  }
}
