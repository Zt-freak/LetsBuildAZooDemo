// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.GraveYardCutScene.GraveYardCreator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildThisInfo;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.GraveYardCutScene
{
  internal class GraveYardCreator
  {
    private SmartCharacterBox charactertextbox;
    private DragZone dragzone;
    private BuildUI buildUI;
    private DragZoneCamera dragzonecamera;
    private int Volume;

    public GraveYardCreator(Player player, int Peopletohouse)
    {
      FeatureFlags.GraveYrardCreatorActive = true;
      FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = true;
      this.charactertextbox = new SmartCharacterBox(player.prisonlayout.GetNumberOfGraveYards() != 0 ? string.Format("Your cemeteries are full! You must build another one! You need space for at least {0} inmates.", (object) Peopletohouse) : "Prisoners can be lost, but fear not, once you have a place to store them, you can always bring them back again!~Time to build your first cemetery.", AnimalType.Administrator);
      this.dragzonecamera = new DragZoneCamera();
      this.dragzone = new DragZone(player);
      this.buildUI = new BuildUI(TILETYPE.GraveYard, player);
    }

    public bool UpdateGraveYardCreator(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      if (this.buildUI != null && this.buildUI.UpdateBuildUI(DeltaTime, player, true))
      {
        if (!this.dragzone.GetIsBlocked(wallsandfloors.tilesasarray))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
          player.prisonlayout.AddNewCellBlock(this.dragzone.GetTopLeft().X, this.dragzone.GetTopLeft().Y, this.dragzone.GetWidth(), this.dragzone.GetHeight(), wallsandfloors, TileData.GetTileTypeToCellBlockType(TILETYPE.GraveYard), player, true);
          player.OldSaveThisPlayer();
        }
        return true;
      }
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
      this.dragzonecamera.UpdateDragZoneCamera(player, DeltaTime);
      this.dragzone.UpdateDragZone(DeltaTime, player);
      LiveStats.DraggingDragZone = this.dragzone.IsDraggingging;
      int volume = this.dragzone.GetVolume();
      if (volume != this.Volume)
      {
        this.Volume = volume;
        int width = this.dragzone.GetWidth();
        int height = this.dragzone.GetHeight();
        this.buildUI.SetUp(TILETYPE.GraveYard, player, this.Volume, width > 2 && height > 2, true, volume);
        if (this.dragzone.GetIsBlocked(wallsandfloors.tilesasarray))
          this.buildUI.SetUp(BuildMessageType.Overlapping, player, true);
        else if (!this.dragzone.GetThisIsnextToSomething(wallsandfloors.tilesasarray))
          this.buildUI.SetUp(BuildMessageType.PlaceNextToExistingWall, player, true);
        else if (width < 3 || height < 3)
        {
          if (width < 3 && height < 3)
            this.buildUI.SetUp(BuildMessageType.TooSmall, player, true);
          else if (width < 3)
            this.buildUI.SetUp(BuildMessageType.MakeWider, player, true);
          else if (height < 3)
            this.buildUI.SetUp(BuildMessageType.MakeTaller, player, true);
        }
        else if (width * height < GameFlags.GraveYardMinimumSize)
          this.buildUI.SetUp(BuildMessageType.TooSmall, player, true);
        else if (width * height > GameFlags.GraveYardMaxSize)
          this.buildUI.SetUp(BuildMessageType.TooBig, player, true);
        else
          this.buildUI.SetUp(BuildMessageType.CanBuild, player, true);
      }
      return false;
    }

    public void DrawGraveYardCreator(TileRenderer[,] tilesasarray)
    {
      this.dragzone.DrawDragZone(tilesasarray);
      this.charactertextbox.DrawSmartCharacterBox();
      if (this.buildUI == null)
        return;
      this.buildUI.DrawBuildUI();
    }
  }
}
