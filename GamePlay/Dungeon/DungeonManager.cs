// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Dungeon.DungeonManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Ground;
using TinyZoo.PlayerDir;

namespace TinyZoo.GamePlay.Dungeon
{
  internal class DungeonManager
  {
    private DungoenTileRenderer floor;
    private GroundManager groundmanager;
    private BlackOut blackout;

    public DungeonManager(Player player)
    {
      this.floor = new DungoenTileRenderer(player);
      if (GameFlags.IsBreakOut)
      {
        this.groundmanager = new GroundManager();
      }
      else
      {
        if (!GameFlags.BountyMode)
          return;
        this.blackout = new BlackOut();
        this.blackout.DrawRect = Bounty.GetBlakOut();
        this.blackout.SetAllColours(Bounty.OuterCLR);
      }
    }

    public void UpdateDungeonManager(float DeltaTime) => this.floor.UpdateFloor(DeltaTime);

    public void DrawDungeonManager()
    {
      if (GameFlags.IsBreakOut)
        this.groundmanager.DrawGroundManager();
      else if (GameFlags.BountyMode)
        this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch0, AssetContainer.EnvironmentSheet);
      throw new Exception("NEXT LINE IS NOT IN GAME RIGHT - ITS PRISON PLANET RIGHT");
    }
  }
}
