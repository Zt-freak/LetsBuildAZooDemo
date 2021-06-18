// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.Z_EventsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Events.BreakOut;
using TinyZoo.Z_Events.NewsCaster;
using TinyZoo.Z_Events.PoliceSniper;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_Events
{
  internal class Z_EventsManager
  {
    private NewsCasterManager newscastermanager;
    private PoliceSnipermanger policesniper;
    private BreakOutManager breakoutmanager;
    internal static bool AGateWasFixed;

    public Z_EventsManager(Player player)
    {
    }

    public bool HasBreakOut() => this.breakoutmanager != null;

    public void StartBreakOutEvent(Player player, PrisonZone prisonzone)
    {
      if (this.breakoutmanager == null)
        this.breakoutmanager = new BreakOutManager(ref this.newscastermanager, prisonzone);
      else
        this.breakoutmanager.AddExtraBreakOut(ref this.newscastermanager, prisonzone);
    }

    public void StartPoliceEvent(Player player) => this.policesniper = new PoliceSnipermanger(player);

    public void HumanKilled(SimPerson simperson)
    {
      if (this.breakoutmanager == null)
        return;
      this.breakoutmanager.HumanKilled(simperson, this.newscastermanager);
    }

    public void UpdateZ_EventsManager(float DeltaTime, float SimulatonTime, Player player)
    {
      if (this.breakoutmanager != null && this.breakoutmanager.UpdateBreakOutManager(SimulatonTime, ref this.policesniper, player))
      {
        this.breakoutmanager = (BreakOutManager) null;
        if (this.newscastermanager.RemoveThisEvent(Z_NotificationType.F_GateBroke))
          this.newscastermanager = (NewsCasterManager) null;
      }
      if (this.policesniper != null)
      {
        if (this.policesniper.UpdatePoliceSnipermanger(DeltaTime, Z_EventsManager.AGateWasFixed))
          this.policesniper = (PoliceSnipermanger) null;
        Z_EventsManager.AGateWasFixed = false;
      }
      if (this.newscastermanager == null)
        return;
      this.newscastermanager.UpdateNewsCasterManager(DeltaTime);
    }

    public void DrawZ_EventsManager()
    {
      if (this.newscastermanager != null)
        this.newscastermanager.DrawNewsCasterManager();
      if (this.policesniper == null)
        return;
      this.policesniper.DrawPoliceSnipermanger();
    }
  }
}
