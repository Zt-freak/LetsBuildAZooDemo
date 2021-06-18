// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.PenMover
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;

namespace TinyZoo.Z_BuldMenu.PenBuilder
{
  internal class PenMover
  {
    public int CELLUID;

    public PenMover(PerimeterBuilder perimeterBuilder, int _CELLUID) => this.CELLUID = _CELLUID;

    internal static void ConfirmMovePen(
      Player player,
      PenMoveCollision penmovercollision,
      PrisonZone prisonzone_ForMove,
      WallsAndFloorsManager wallsandfloors,
      AnimalsInPens peopleandbeams)
    {
      OverWorldManager.overworldenvironment.animalsinpens.MovePen_A_DeleteAllDynamicObjects(prisonzone_ForMove.Cell_UID, player, prisonzone_ForMove);
      prisonzone_ForMove.MoveWholeZone(PenMoveCollision.Offset);
      penmovercollision.ConfirmMove(player, wallsandfloors, prisonzone_ForMove.Cell_UID);
      peopleandbeams.DeletePeopleAfterSellingPrison(prisonzone_ForMove.Cell_UID);
      prisonzone_ForMove.prisonercontainer.ThisWasTehCellBlockThatChanged = true;
      peopleandbeams.AddCellBlockOnTheFly(player);
      OverWorldManager.overworldenvironment.animalsinpens.MovePen(prisonzone_ForMove.Cell_UID, PenMoveCollision.Offset, player, prisonzone_ForMove);
      if (Z_GameFlags.SelectedPrisonZoneisFarm)
        OverWorldManager.overworldenvironment.wallsandfloors.SetUpFarmSigns(player, prisonzone_ForMove.Cell_UID);
      else
        OverWorldManager.overworldenvironment.wallsandfloors.SetUpAnimalsOnOrder(player, prisonzone_ForMove.Cell_UID);
    }

    public void UpdatePenMover(float DeltaTime, Player player)
    {
    }

    public void DrawPenMover(PerimeterBuilder perimeterBuilder)
    {
    }
  }
}
