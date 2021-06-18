// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.JanitorController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class JanitorController
  {
    private Vector2Int TargetTrashLoc;
    private int TrashHeld;

    public void CheckPathOnSetNewTarget(PathNavigator pathnavigator)
    {
      if (pathnavigator.GetCurrentPath() == null)
        return;
      for (int index = 0; index < pathnavigator.GetCurrentPath().Count; ++index)
      {
        if (OverWorldManager.trashmanager.IsTrashHere(pathnavigator.GetCurrentPath()[index].Location))
        {
          this.TargetTrashLoc = new Vector2Int(pathnavigator.GetCurrentPath()[index].Location);
          pathnavigator.TrimPath(pathnavigator.GetCurrentPath().Count - (index + 1));
        }
      }
    }

    public void ReachedTargetLocation(
      Vector2Int CurrentLocation,
      ref Vector2Int ForceGoHere,
      Employee Ref_Employee,
      Player player)
    {
      if (this.TargetTrashLoc != null && this.TargetTrashLoc.CompareMatches(CurrentLocation) && OverWorldManager.trashmanager.TryToPickUpTrash(this.TargetTrashLoc))
      {
        --Ref_Employee.ActionsLeftToday;
        Ref_Employee.ehistory.CompletedAction();
        this.TargetTrashLoc = (Vector2Int) null;
        ++this.TrashHeld;
        if (this.TrashHeld > 3)
        {
          Player.garbage.AddGarbageToBigGreenBins((float) this.TrashHeld);
          this.TrashHeld = 0;
        }
      }
      if (Ref_Employee.ActionsLeftToday <= 0 || !GameFlags.IsDay || TinyZoo.Game1.Rnd.Next(0, 100) >= Ref_Employee.Determination)
        return;
      this.TargetTrashLoc = OverWorldManager.trashmanager.GetTrashLocation(Ref_Employee.workzoneinfo);
      if (this.TargetTrashLoc == null)
        return;
      ForceGoHere = new Vector2Int(this.TargetTrashLoc);
      Ref_Employee.ehistory.TriedToStartAction();
    }
  }
}
