// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.BreedResultManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.Z_BreedResult
{
  internal class BreedResultManager
  {
    internal static newThingRenderer newthingget;

    public BreedResultManager() => BreedResultManager.newthingget.AddBlackOut();

    public void UpdateBreedResultManager(float DeltaTime, Player player)
    {
      if (!BreedResultManager.newthingget.UpdatenewThingRenderer(DeltaTime, player))
        return;
      Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
      Game1.screenfade.BeginFade(true);
      ParkGate.Reset();
    }

    public void DrawBreedResultManager() => BreedResultManager.newthingget.DrawnewThingRenderer(false);
  }
}
