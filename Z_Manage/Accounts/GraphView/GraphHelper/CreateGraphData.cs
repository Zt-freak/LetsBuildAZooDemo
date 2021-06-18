// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper.CreateGraphData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper
{
  internal class CreateGraphData
  {
    internal static List<ComaprerSet> GetAcountGraph(Player player)
    {
      GraphData Compareres = new GraphData("Visitors", new Vector3(0.2f, 0.2f, 0.8f));
      GraphData graphData1 = new GraphData("Audience", new Vector3(0.1f, 0.8f, 0.1f));
      GraphData graphData2 = new GraphData("Revenue Earned", new Vector3(0.2f, 0.8f, 0.2f));
      GraphData graphData3 = new GraphData("Revenue Spent", new Vector3(0.8f, 0.2f, 0.2f));
      GraphData graphData4 = new GraphData("Available Funds", new Vector3(0.8f, 0.5f, 0.2f));
      int num = 0;
      if (Player.financialrecords.daystats.Count > 50)
        num = Player.financialrecords.daystats.Count - 50;
      for (int index = num; index < Player.financialrecords.daystats.Count; ++index)
      {
        Compareres.graphentries.Add(new GraphEntry((float) Player.financialrecords.daystats[index].PeopleWhoCame));
        graphData1.graphentries.Add(new GraphEntry((float) Player.financialrecords.daystats[index].PeopleWhoWantedToCome));
        graphData2.graphentries.Add(new GraphEntry((float) Player.financialrecords.daystats[index].TotalMoneyEarnedThisDay));
        graphData3.graphentries.Add(new GraphEntry((float) Player.financialrecords.daystats[index].TotalMoneySpentThisDay));
        graphData4.graphentries.Add(new GraphEntry((float) Player.financialrecords.daystats[index].BankBalanceOnClosing));
      }
      graphData4.SetMax();
      Compareres.SetMax(graphData1);
      graphData1.Colours.Add(new Vector3(0.8f, 0.2f, 0.2f));
      graphData1.AddColourForLowerOrHigher(Compareres);
      graphData2.SetMax(graphData3);
      return new List<ComaprerSet>()
      {
        new ComaprerSet("Visitors", new GraphData[2]
        {
          Compareres,
          graphData1
        }),
        new ComaprerSet("Revenue", new GraphData[2]
        {
          graphData2,
          graphData3
        }),
        new ComaprerSet("Balance", new GraphData[1]{ graphData4 })
      };
    }
  }
}
