// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.BreakOut.BreakOutManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Events.NewsCaster;
using TinyZoo.Z_Events.PoliceSniper;

namespace TinyZoo.Z_Events.BreakOut
{
  internal class BreakOutManager
  {
    private int TotalDeaths;
    private int TotalAnimals;
    private int AnimalsKilled;
    private List<AnimalType> animals;
    private string Body;
    private bool SetTextAgain;
    private List<SimPerson> DeadPeople;
    private float TimeUntilPolice;
    private static int NewAnimalsDead;
    internal static float MaxTimeForPoliceToCome = 2f;
    internal static List<int> JustFixedAGate;
    private static List<BreakoutByPen> breakoutsbypen;
    private int AnimalsSaved;

    public BreakOutManager(ref NewsCasterManager newscastermanager, PrisonZone prisonzone)
    {
      BreakOutManager.breakoutsbypen = new List<BreakoutByPen>();
      BreakOutManager.breakoutsbypen.Add(new BreakoutByPen(prisonzone));
      this.DeadPeople = new List<SimPerson>();
      this.animals = prisonzone.prisonercontainer.GetAllTypesOfAnimal();
      this.TotalAnimals = 0;
      for (int index = 0; index < prisonzone.prisonercontainer.prisoners.Count; ++index)
      {
        if (!prisonzone.prisonercontainer.prisoners[index].IsDead)
          ++this.TotalAnimals;
      }
      this.SetString();
      newscastermanager = new NewsCasterManager("ANIMAL BREAK OUT", this.Body);
      this.TimeUntilPolice = BreakOutManager.MaxTimeForPoliceToCome;
    }

    public void AddExtraBreakOut(ref NewsCasterManager newscastermanager, PrisonZone prisonzone) => BreakOutManager.breakoutsbypen.Add(new BreakoutByPen(prisonzone));

    internal static void ScrubDeadAnimals()
    {
      for (int index = 0; index < BreakOutManager.breakoutsbypen.Count; ++index)
        BreakOutManager.breakoutsbypen[index].Scrubnimals(ref BreakOutManager.NewAnimalsDead);
    }

    internal static bool HasActiveBreakOut() => BreakOutManager.breakoutsbypen != null;

    internal static void AddEscapedAnimal(AnimalRenderMan animalendermanager, int PenUID)
    {
      List<BreakoutByPen> breakoutsbypen = BreakOutManager.breakoutsbypen;
      for (int index = 0; index < BreakOutManager.breakoutsbypen.Count; ++index)
      {
        if (BreakOutManager.breakoutsbypen[index].REF_prisonzone.Cell_UID == PenUID)
          BreakOutManager.breakoutsbypen[index].AddEscapedAnimal(animalendermanager);
      }
    }

    internal static AnimalRenderMan GetAnimalToHunt() => BreakOutManager.breakoutsbypen.Count > 0 ? BreakOutManager.breakoutsbypen[0].GetAnimalToHunt() : (AnimalRenderMan) null;

    private void SetString()
    {
      this.Body = "";
      EnemyData.GetEnemyTypeName(this.animals[0]);
      if (this.TotalDeaths == 0)
        this.Body = "There is trouble here at the zoo, we are getting reports that a badly maintained gate broke open, resulting in some animals excaping their enclosure, we will have more on this stry as it develops.";
      else if (this.TotalDeaths == 1)
        this.Body = string.Format("There have been shocking developments here at the zoo, the excaped animals have claimed a human victim, {0} thought they were just coming for a nice day out at the zoo, until tragedy struck! Our hearts go out to their friends and family...Stay tuned for more updates on this story.", (object) this.DeadPeople[0].GetName());
      else
        this.Body = string.Format("The death toll just keeps climbing here, there are now {0} fatalities. How will this animal rampage be stopped. We have reports that the police are sending several officers to deal with the issue.", (object) this.DeadPeople[0].GetName());
    }

    public bool UpdateBreakOutManager(
      float SimulatonTime,
      ref PoliceSnipermanger policesniper,
      Player player)
    {
      if (BreakOutManager.JustFixedAGate != null)
      {
        for (int index1 = 0; index1 < BreakOutManager.JustFixedAGate.Count; ++index1)
        {
          for (int index2 = BreakOutManager.breakoutsbypen.Count - 1; index2 > -1; --index2)
          {
            if (BreakOutManager.breakoutsbypen[index2].REF_prisonzone.Cell_UID == BreakOutManager.JustFixedAGate[index1] && BreakOutManager.breakoutsbypen[index2].EscapedAnimals.Count > 0)
            {
              for (int index3 = BreakOutManager.breakoutsbypen[index2].EscapedAnimals.Count - 1; index3 > -1; --index3)
              {
                Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(BreakOutManager.breakoutsbypen[index2].EscapedAnimals[index3].enemyrenderere.vLocation);
                if (BreakOutManager.breakoutsbypen[index2].REF_prisonzone.ThisIsInThisEnclosure(worldSpaceToTile.X, worldSpaceToTile.Y))
                {
                  ++this.AnimalsSaved;
                  BreakOutManager.breakoutsbypen[index2].EscapedAnimals[index3].EndBreakOutReturnToPen();
                  BreakOutManager.breakoutsbypen[index2].EscapedAnimals.RemoveAt(index3);
                  if (BreakOutManager.breakoutsbypen[index2].EscapedAnimals.Count == 0)
                    BreakOutManager.breakoutsbypen.RemoveAt(index2);
                }
              }
            }
          }
        }
      }
      if (this.AnimalsSaved + this.AnimalsKilled == this.TotalAnimals)
      {
        if (!OverWorldManager.zoopopupHolder.IsNull())
          OverWorldManager.zoopopupHolder.SetbreakOutData(this.TotalDeaths, this.TotalAnimals - this.AnimalsKilled, this.TotalAnimals);
        return true;
      }
      if (BreakOutManager.NewAnimalsDead > 0)
      {
        this.AnimalsKilled += BreakOutManager.NewAnimalsDead;
        BreakOutManager.NewAnimalsDead = 0;
        this.SetString();
        if (!OverWorldManager.zoopopupHolder.IsNull())
          OverWorldManager.zoopopupHolder.SetbreakOutData(this.TotalDeaths, this.TotalAnimals - this.AnimalsKilled, this.TotalAnimals);
      }
      this.TimeUntilPolice -= SimulatonTime;
      if ((double) this.TimeUntilPolice <= 0.0 && policesniper == null)
        policesniper = new PoliceSnipermanger(player);
      return false;
    }

    public void HumanKilled(SimPerson simperson, NewsCasterManager newscastermanager)
    {
      this.SetTextAgain = true;
      this.SetString();
      newscastermanager.SetNewBodyText(this.Body);
      this.DeadPeople.Add(simperson);
      ++this.TotalDeaths;
      if (OverWorldManager.zoopopupHolder.IsNull())
        return;
      OverWorldManager.zoopopupHolder.SetbreakOutData(this.TotalDeaths, this.AnimalsKilled, this.TotalAnimals);
    }
  }
}
