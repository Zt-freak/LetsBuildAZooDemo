// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Fights.FightManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_HUD.PointAtThings;

namespace TinyZoo.Z_Fights
{
  internal class FightManager
  {
    private AnimalRenderMan attacker;
    private AnimalRenderMan defender;
    private AnimalStat Attackerstats;
    private AnimalStat DefenderSTats;
    public bool IsDone;
    public AnimalAttackHandler animalattackhandler_Attaccker;
    public AnimalAttackHandler animalattackhandler_Defender;
    private bool HasUpdated;

    public FightManager(PrisonerInfo ATTACKER_prisoner, PrisonerInfo DEFENDER_prisoner)
    {
      this.IsDone = false;
      ATTACKER_prisoner.IsCurrentlyFighting = true;
      DEFENDER_prisoner.IsCurrentlyFighting = true;
      this.Attackerstats = AnimalData.GetAnimalStat(ATTACKER_prisoner.intakeperson.animaltype);
      this.DefenderSTats = AnimalData.GetAnimalStat(DEFENDER_prisoner.intakeperson.animaltype);
      int PenID;
      this.attacker = OverWorldManager.overworldenvironment.animalsinpens.GetAnimalRendererByUID(ATTACKER_prisoner.intakeperson.UID, -1, out PenID);
      this.defender = OverWorldManager.overworldenvironment.animalsinpens.GetAnimalRendererByUID(DEFENDER_prisoner.intakeperson.UID, PenID, out PenID);
      PointOffScreenManager.AddPointer(this.attacker, this.defender, SpecialEventType.AnimalFight, this);
      this.attacker.AddFightController(this);
      this.defender.AddFightController(this);
      this.animalattackhandler_Attaccker = new AnimalAttackHandler(this.Attackerstats, this.attacker);
      this.animalattackhandler_Defender = new AnimalAttackHandler(this.DefenderSTats, this.defender);
      this.animalattackhandler_Attaccker.opponent = this.animalattackhandler_Defender;
      this.animalattackhandler_Attaccker.OpponentRenderer = this.defender;
      this.animalattackhandler_Defender.opponent = this.animalattackhandler_Attaccker;
      this.animalattackhandler_Defender.OpponentRenderer = this.attacker;
    }

    public bool UpdateFightManager(float DeltaTime, PrisonerInfo thisanimal_CallingUpdate)
    {
      if (thisanimal_CallingUpdate == this.attacker.REF_prisonerinfo)
      {
        this.animalattackhandler_Attaccker.UpdateAnimalAttackHandler(DeltaTime, ref this.IsDone);
        return true;
      }
      this.animalattackhandler_Defender.UpdateAnimalAttackHandler(DeltaTime, ref this.IsDone);
      return false;
    }

    public bool DrawFightManager(Vector2 Location, PrisonerInfo thisanimal_CallingUpdate) => thisanimal_CallingUpdate == this.attacker.REF_prisonerinfo ? this.animalattackhandler_Attaccker.DrawAnimalAttackHandler(Location) : this.animalattackhandler_Defender.DrawAnimalAttackHandler(Location);
  }
}
