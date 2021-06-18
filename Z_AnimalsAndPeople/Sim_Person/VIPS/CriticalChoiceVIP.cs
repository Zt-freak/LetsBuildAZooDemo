// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS.CriticalChoiceVIP
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS.CriticalChoice;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS
{
  internal class CriticalChoiceVIP
  {
    public int SelectedChoice = -1;
    private bool HasPoopedUp;
    private CustomerType customerType;
    private CC_ArtistController artistController;

    public CriticalChoiceVIP(CustomerType _customerType)
    {
      this.customerType = _customerType;
      if (this.customerType != CustomerType.AnimalArtist)
        return;
      this.artistController = new CC_ArtistController();
    }

    public void UpdateCriticalChoiceVIP(
      WalkingPerson walkingperson,
      Player player,
      float DeltaTime,
      ref bool IsPlayingWalkAnimation,
      ref bool BlockAutoWalk)
    {
      if (!this.HasPoopedUp && OverWorldManager.zoopopupHolder.TopLayerIsNull())
      {
        this.HasPoopedUp = true;
        OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.CriticalChoice, walkingperson);
      }
      if (this.customerType != CustomerType.AnimalArtist)
        return;
      this.artistController.UpdateCC_ArtistController(walkingperson, this.HasPoopedUp, DeltaTime, walkingperson, ref IsPlayingWalkAnimation, ref BlockAutoWalk, player);
    }

    public void ReachedLocation(
      Vector2Int CurrentLocation,
      out bool SetNewPath,
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      ref bool IsWalking,
      ref bool BlockAutoWalk,
      MemberOfThePublic member)
    {
      SetNewPath = false;
      if (this.customerType != CustomerType.AnimalArtist)
        return;
      this.artistController.ReachedLocation(CurrentLocation, out SetNewPath, player, parent, ref ForceGoHere, ref IsWalking, ref BlockAutoWalk, member);
    }
  }
}
