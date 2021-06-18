// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay.AnimalFrame_AndTimer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.Intake.InmateSummary.Psners;

namespace TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay
{
  internal class AnimalFrame_AndTimer
  {
    private GameObjectNineSlice FrameForWHoleThing;
    private PrisonerSprite alienentry;
    private ActiveBreed activebreenref;
    private GameObject QUESTION;
    private string TIMER;
    private bool MustHaveServer;
    private int _INDEX;

    public AnimalFrame_AndTimer(Player player, int Index) => throw new Exception("DepricatedBreeds XX");

    public bool Consume(
      out AnimalType animal,
      out int ChildSkin,
      Player player,
      out bool ABOY,
      out bool WasNew)
    {
      throw new Exception("DepricatedBreeds 128");
    }

    public void UpdateAnimalFrame_AndTimer(
      Player player,
      out bool IsComplete,
      out bool ServerError)
    {
      if (DebugFlags.IsPCVersion)
      {
        ServerError = false;
        this.TIMER = "Days Left: " + (object) this.activebreenref.DaysLeft;
        IsComplete = this.activebreenref.DaysLeft <= 0;
      }
      else
      {
        double dateTimePercent = (double) this.activebreenref.researchtimeleft.GetDateTimePercent(player.Stats.datetimemanager, true);
        this.TIMER = this.activebreenref.researchtimeleft.TimeLeftString;
        IsComplete = this.activebreenref.researchtimeleft.IsComplete(player.Stats.datetimemanager, out ServerError, this.MustHaveServer);
      }
    }

    public void DrawAnimalFrame_AndTimer(Vector2 Offset)
    {
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, new Vector2(130f, 130f));
      this.alienentry.SetDrawOriginToCentre();
      this.alienentry.DrawPrisonerSprite(Offset, AssetContainer.pointspritebatch03);
      TextFunctions.DrawJustifiedText(this.TIMER, 0.8f, Offset + new Vector2(0.0f, 85f), Color.RosyBrown, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
    }
  }
}
