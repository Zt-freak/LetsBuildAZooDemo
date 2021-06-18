// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Customers.NewCustomers.BusPeopleAndRoutes
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData;

namespace TinyZoo.Z_BalanceSystems.Customers.NewCustomers
{
  internal class BusPeopleAndRoutes
  {
    private int BasePeopleValue_PerHour;
    private BUSROUTE ThisRoute;
    private float LocalMultiplier;
    private float LastTimeOfBusVisit;
    private int PeopleWaiting;
    private int TotalPeopleToday_Unmodified;
    private int PeopleLeftToGetOnBusToday;
    private bool HadABusOnThisRouteToday;
    private int CollectionsToday;
    public bool WasFootballTeam;

    public BusPeopleAndRoutes(int _BasePeopleValue, BUSROUTE _ThisRoute, bool HasABusOnThisRoute)
    {
      this.CollectionsToday = 0;
      this.HadABusOnThisRouteToday = HasABusOnThisRoute;
      this.LocalMultiplier = BusTimes.GetPopulationMultiplier(_ThisRoute);
      this.BasePeopleValue_PerHour = _BasePeopleValue;
      this.ThisRoute = _ThisRoute;
      this.BasePeopleValue_PerHour = (int) ((double) this.BasePeopleValue_PerHour * (double) BusTimes.GetPopulationMultiplier(_ThisRoute) * 3.0);
      this.PeopleWaiting = Game1.Rnd.Next(this.BasePeopleValue_PerHour, this.BasePeopleValue_PerHour + Math.Min(this.BasePeopleValue_PerHour / 3, Game1.Rnd.Next(13, 17)));
      if (this.PeopleWaiting < 1)
        this.PeopleWaiting = 1;
      this.LastTimeOfBusVisit = -1f;
      float num = 2f;
      this.TotalPeopleToday_Unmodified = this.PeopleWaiting + (int) ((double) this.BasePeopleValue_PerHour * (((double) Z_GameFlags.ZooOpenTime_InSeconds - (double) BusTimes.GetRouteTime(_ThisRoute) * (double) num) / ((double) Z_GameFlags.SecondsInDay / 24.0)));
      if (!HasABusOnThisRoute)
        this.PeopleWaiting = 1;
      this.PeopleLeftToGetOnBusToday = this.TotalPeopleToday_Unmodified;
    }

    public void FinalizePropleWaitingRecordsAtEndOfDay()
    {
      if (!this.HadABusOnThisRouteToday || this.TotalPeopleToday_Unmodified <= 0)
        return;
      Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1].DayEndedPeopleLeftAtStop(this.ThisRoute, this.PeopleWaiting);
    }

    public int GetPeopleForThisBus(int MaximumPeopleOnThisBus, bool IsLastBus)
    {
      ++this.CollectionsToday;
      if (Player.financialrecords.GetDaysPassed() == 0L && this.CollectionsToday == 2)
        BusVIPs.AddVIPonTheFly(new VIP_Entry(AnimalType.SpecialEvent_Scientist, CustomerType.ResearchGrantGuy, CriticalChoiceAction.GetCriticalChoiceCharacterToString(CriticalChoiceCharacter.Scientist)));
      if (TrailerDemoFlags.HasTrailerFlag || Z_DebugFlags.TempBlockMorePeopleSpawning)
        return 0;
      if (Z_DebugFlags.ForcedPeoplePerDay > -1)
      {
        Z_DebugFlags.TempBlockMorePeopleSpawning = true;
        return Z_DebugFlags.ForcedPeoplePerDay;
      }
      if (this.ThisRoute == BUSROUTE.Route01 && BusVIPs.HasFootBallTeam && this.CollectionsToday == 2)
      {
        this.WasFootballTeam = true;
        Player.financialrecords.BusDidPickUp(this.ThisRoute, 11);
        return 11;
      }
      this.HadABusOnThisRouteToday = true;
      if (this.PeopleLeftToGetOnBusToday <= 0)
        return 0;
      if ((double) this.LastTimeOfBusVisit < 0.0)
      {
        this.LastTimeOfBusVisit = Z_GameFlags.DayTimer;
        this.PeopleWaiting += Math.Min(this.BasePeopleValue_PerHour / 3, 15);
        if (this.PeopleWaiting < 0)
          this.PeopleWaiting = 0;
        if (this.PeopleWaiting > this.PeopleLeftToGetOnBusToday)
          this.PeopleWaiting = this.PeopleLeftToGetOnBusToday;
        int peopleWaiting = this.PeopleWaiting;
      }
      else
      {
        int num1 = IsLastBus ? 1 : 0;
        int num2 = (int) ((double) this.BasePeopleValue_PerHour * (((double) Z_GameFlags.DayTimer - (double) this.LastTimeOfBusVisit) / ((double) Z_GameFlags.SecondsInDay / 24.0))) + Math.Min(this.BasePeopleValue_PerHour / 3, 15);
        if (num2 < 0)
          num2 = 0;
        this.PeopleWaiting += num2;
        if (this.PeopleWaiting > this.PeopleLeftToGetOnBusToday)
          this.PeopleWaiting = this.PeopleLeftToGetOnBusToday;
        this.LastTimeOfBusVisit = Z_GameFlags.DayTimer;
      }
      int PeopleWhoGotOnBus = Math.Min(MaximumPeopleOnThisBus, this.PeopleWaiting);
      this.PeopleLeftToGetOnBusToday -= PeopleWhoGotOnBus;
      this.PeopleWaiting -= PeopleWhoGotOnBus;
      Player.financialrecords.BusDidPickUp(this.ThisRoute, PeopleWhoGotOnBus);
      return PeopleWhoGotOnBus;
    }
  }
}
