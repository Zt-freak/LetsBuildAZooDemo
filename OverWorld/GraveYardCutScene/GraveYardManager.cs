// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.GraveYardCutScene.GraveYardManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.OverWorld.GraveYardCutScene
{
  internal class GraveYardManager
  {
    private GraveYardCreator graveyardcreator;
    private GraveYardCutScenePlayer graveyardCS;

    public GraveYardManager(Player player)
    {
      FeatureFlags.BlockStats = true;
      FeatureFlags.BlockTimer = true;
      FeatureFlags.BlockCash = true;
      if (player.livestats.NewlyDeads.Count > player.prisonlayout.HasThisMuchSpaceInGraveYard())
      {
        GameFlags.GraveYardMaxSize = 48;
        GameFlags.GraveYardMinimumSize = player.livestats.NewlyDeads.Count + player.livestats.NewlyDeads.Count * 2 + 2;
        if (GameFlags.GraveYardMinimumSize < 16)
          GameFlags.GraveYardMinimumSize = 16;
        if (GameFlags.GraveYardMinimumSize >= GameFlags.GraveYardMaxSize - 5)
          GameFlags.GraveYardMaxSize = GameFlags.GraveYardMinimumSize + 5;
        this.graveyardcreator = new GraveYardCreator(player, player.livestats.NewlyDeads.Count);
      }
      else
        this.graveyardCS = new GraveYardCutScenePlayer(player);
    }

    public bool UpdateGraveYardManager(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      if (this.graveyardcreator != null && this.graveyardcreator.UpdateGraveYardCreator(DeltaTime, player, wallsandfloors))
      {
        this.graveyardCS = new GraveYardCutScenePlayer(player);
        this.graveyardcreator = (GraveYardCreator) null;
      }
      if (this.graveyardCS == null || !this.graveyardCS.UpdateGraveYardCutScenePlayer(DeltaTime, player))
        return false;
      FeatureFlags.BlockStats = false;
      FeatureFlags.BlockTimer = false;
      FeatureFlags.BlockCash = false;
      FeatureFlags.GraveYrardCreatorActive = false;
      return true;
    }

    public void DrawGraveYardManager(TileRenderer[,] tilesasarray)
    {
      if (this.graveyardcreator != null)
        this.graveyardcreator.DrawGraveYardCreator(tilesasarray);
      if (this.graveyardCS == null)
        return;
      this.graveyardCS.DrawGraveYardCutScenePlayer();
    }
  }
}
