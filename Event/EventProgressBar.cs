// Decompiled with JetBrains decompiler
// Type: TinyZoo.Event.EventProgressBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.ProfitLadder.PeopleDisplay;

namespace TinyZoo.Event
{
  internal class EventProgressBar
  {
    private GameObjectNineSlice Frameees;
    public Vector2 Location;
    private SimpleTextHandler simpletext;
    private PeopleLine peopleline;
    private string TimeLeft;

    public EventProgressBar(EVProgress evprogress, Player player)
    {
      this.Frameees = new GameObjectNineSlice(new Rectangle(895, 372, 21, 21), 7);
      string str = "";
      if (evprogress.enemytype != AnimalType.None)
        str = EnemyData.GetEnemyTypeName(evprogress.enemytype) + " ";
      this.simpletext = new SimpleTextHandler(string.Format("Capture {0} new {1} prisoners in the time remaining to disover a new Alien!", (object) evprogress.TargetValue, (object) str), true, 0.8f);
      this.simpletext.AutoCompleteParagraph();
      this.Location = new Vector2(512f, 400f);
      this.peopleline = new PeopleLine(evprogress, player);
    }

    public void UpdateEventProgressBar(Player player)
    {
      this.peopleline.UpdatePeopleLine(player.player.touchinput);
      if (player.Stats.currentEvent.evprogress[0].endtime < DateTime.UtcNow)
        this.TimeLeft = "Error: Please check your device clock.";
      else
        this.TimeLeft = string.Format("Time Left: {0}", (object) TimeUtils.GetDifferenceInTimeAsString(player.Stats.currentEvent.evprogress[0].endtime, DateTime.UtcNow));
    }

    public void DrawEventProgressBar()
    {
      this.Frameees.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Location, new Vector2(850f, 140f * Sengine.UltraWideSreenUpwardsMultiplier));
      this.simpletext.DrawSimpleTextHandler(this.Location + new Vector2(0.0f, -30f * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText(this.TimeLeft, 4f, this.Location + new Vector2(0.0f, 30f * Sengine.UltraWideSreenUpwardsMultiplier), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      this.peopleline.DrawPeopleLine();
    }
  }
}
