// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.EventData.ReportActionsGetter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_AnimalsAndPeople.Sim_Person;

namespace TinyZoo.Z_SummaryPopUps.EventReport.EventData
{
  internal class ReportActionsGetter
  {
    internal static ReportActionSet GetDataSet(CustomerType customertype)
    {
      ReportActionSet reportActionSet = new ReportActionSet();
      if (customertype != CustomerType.AnimalWelfareOfficer)
      {
        if (customertype == CustomerType.Influencer)
          ;
      }
      else
      {
        ReportAction reportAction1 = new ReportAction(ActionType.SavingsAtRescueShelter, ReportResultRank.A, 15, 30);
        reportActionSet.actions.Add(reportAction1);
        ReportAction reportAction2 = new ReportAction(ActionType.ZooTradesWillGiveAnExtraAnimal, ReportResultRank.A, 1, 20);
        reportActionSet.actions.Add(reportAction2);
        ReportAction reportAction3 = new ReportAction(ActionType.DonationsToMascottsBoost, ReportResultRank.B, 100, 20);
        reportActionSet.actions.Add(reportAction3);
        ReportAction reportAction4 = new ReportAction(ActionType.NoAction, ReportResultRank.C, 100, 20);
        reportActionSet.actions.Add(reportAction4);
        ReportAction reportAction5 = new ReportAction(ActionType.Bad_PayFine, ReportResultRank.F, 20, 0);
        reportActionSet.actions.Add(reportAction5);
        ReportAction reportAction6 = new ReportAction(ActionType.Bad_ShelterCostsIncrease, ReportResultRank.F, 300, 30);
        reportActionSet.actions.Add(reportAction6);
      }
      return reportActionSet;
    }
  }
}
