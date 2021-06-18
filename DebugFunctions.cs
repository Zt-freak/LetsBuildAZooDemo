// Decompiled with JetBrains decompiler
// Type: TinyZoo.DebugFunctions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople;

namespace TinyZoo
{
  internal class DebugFunctions
  {
    internal static bool SpeedUpActive;
    internal static bool pauseActive;
    internal static bool SlowDownActive;
    internal static bool DebugAutoFastScrollForLookingAtArt;
    internal static int AutoSpwnPeople;
    private static float PeopleSpawnDelay;
    private static GameObject Block;

    internal static PrisonerInfo GetRandomAnimal() => new PrisonerInfo(new IntakePerson((AnimalType) Game1.Rnd.Next(0, 55), _IsAGirl: (Game1.Rnd.Next(0, 2) > 0), Variant: Game1.Rnd.Next(0, 10)), false, Vector2.Zero, CellBlockType.Arctic)
    {
      causeofdeath = (CauseOfDeath) Game1.Rnd.Next(0, 8),
      Age = Game1.Rnd.Next(0, 999),
      Hunger = 0.3f
    };

    internal static void UpdateDebugFunctions(
      InputMap inputmap,
      Player player,
      GraphicsDeviceManager graphics,
      float DeltaTime)
    {
    }

    internal static void QuickDrawMarker(Vector2Int Location)
    {
      if (DebugFunctions.Block == null)
      {
        DebugFunctions.Block = new GameObject();
        DebugFunctions.Block.DrawRect = Game1.WhitePixelRect;
        DebugFunctions.Block.SetDrawOriginToCentre();
        DebugFunctions.Block.scale = 15f;
        DebugFunctions.Block.fAlpha = 0.4f;
      }
      DebugFunctions.Block.SetAllColours(1f, 0.0f, 1f);
      DebugFunctions.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(Location.X, Location.Y));
      DebugFunctions.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
    }
  }
}
