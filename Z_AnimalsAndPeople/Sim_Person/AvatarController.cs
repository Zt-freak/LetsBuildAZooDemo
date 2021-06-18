// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.AvatarController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_Avatar;
using TinyZoo.Z_OverWorld.AvatarUI;
using TinyZoo.Z_Trailer;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class AvatarController
  {
    private CharacterDirectController directcontroller;
    private Z_AvatarUI avatarui;
    private WalkingPerson Person;

    public AvatarController(WalkingPerson _refPerson)
    {
      this.Person = _refPerson;
      this.directcontroller = new CharacterDirectController();
      this.avatarui = new Z_AvatarUI();
    }

    public void UpdateAvatarController(
      PathNavigator pathnavigator,
      Player player,
      float DeltaTime,
      Employee Ref_Employee,
      ref bool IsWalking,
      WalkingPerson _refPerson)
    {
      bool IsActive = OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar || Z_GameFlags.IsWaitingToReturnToControllerWalk;
      this.avatarui.UpdateZ_AvatarUI(DeltaTime, IsActive);
      if (IsActive)
      {
        this.Person.AnimationFrameRate = 0.1f;
        if (Z_GameFlags.IsWaitingToReturnToControllerWalk)
        {
          this.directcontroller.Velocity = Vector2.Zero;
          return;
        }
        if (this.directcontroller.UpdateCharacterDirectController(player, DeltaTime, ref IsWalking, this.Person, pathnavigator))
          AvatarUIManager.SetNewCharacterLocation(this.Person, pathnavigator, player, true);
        else
          AvatarUIManager.SetNewCharacterLocation(this.Person, pathnavigator, player, false);
        if (player.inputmap.PressedBackOnController())
          OverWorldManager.overworldstate = OverWOrldState.MainMenu;
      }
      else
        _refPerson.AnimationFrameRate = 0.2f;
      if (!TrailerDemoFlags.HasTrailerFlag)
        return;
      SpawnBlockArray.SetAvatarPostion(this.Person.vLocation);
    }

    public void DrawAvatarController() => this.avatarui.DrawZ_AvatarUI(this.Person.vLocation);
  }
}
