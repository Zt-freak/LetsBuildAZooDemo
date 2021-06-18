// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.BuildSystemManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildThisInfo;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem
{
  internal class BuildSystemManager
  {
    public ThingToBuildManager thingtobuild;
    private BuildingInformationMessage infotext;
    private BuildUI buildconfirmUI;
    private BackButton BackButton;

    public BuildSystemManager(TILETYPE tletobuild, Player player)
    {
      this.thingtobuild = new ThingToBuildManager(tletobuild, player);
      this.infotext = new BuildingInformationMessage(BuildMessageType.None);
      this.buildconfirmUI = new BuildUI(tletobuild, player);
      this.BackButton = new BackButton();
    }

    public bool UpdateBuildSystemManager(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors,
      out bool Built,
      CATEGORYTYPE cetegory)
    {
      Built = false;
      if (!FeatureFlags.BlockCloseBuildMenu && this.BackButton.UpdateBackButton(player, DeltaTime))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        return true;
      }
      if (player.inputmap.PressedBackOnController() && !FeatureFlags.BlockCloseBuildMenu)
        return true;
      if (player.inputmap.PressedThisFrame[0] && this.buildconfirmUI.HasEnoughCash && this.buildconfirmUI.GetCanBuild())
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
        Built = true;
        return false;
      }
      if (this.thingtobuild.UpdateThingToBuildManager(player, DeltaTime, wallsandfloors, this.buildconfirmUI))
      {
        BlockInfo block = wallsandfloors.CanBuildThisHere(ThingToBuildManager.LastLocation, this.thingtobuild.tilerenderer);
        this.infotext.SetMessage(block.GetMessageType());
        this.buildconfirmUI.SetUp(block.GetMessageType(), player);
        this.thingtobuild.SetBlocks(block);
      }
      if (ThingToBuildManager.placetype == PlaceType.PlacingBuildings && this.buildconfirmUI.UpdateBuildUI(DeltaTime, player))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
        player.player.touchinput.ReleaseTapArray[0].X = -1000f;
        Built = true;
        return false;
      }
      this.infotext.UpdateInformationMessage(DeltaTime);
      return false;
    }

    public void DrawBuildSystemManager(TileRenderer[,] tilesasarray)
    {
      this.thingtobuild.DrawThingToBuildManager(tilesasarray);
      if (!FeatureFlags.BlockCloseBuildMenu)
        this.BackButton.DrawBackButton(Vector2.Zero);
      this.infotext.DrawInformationMessage();
      this.buildconfirmUI.DrawBuildUI();
    }
  }
}
