// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.PoliceSniper.PoliceSquad
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_Events.PoliceSniper
{
  internal class PoliceSquad
  {
    private WalkingPerson Sniper;
    private WalkingPerson PoliceMan2;
    private WalkingPerson PoliceMan3;
    private WalkingPerson PoliceMan;

    public PoliceSquad()
    {
      this.Sniper = new WalkingPerson(0, AnimalType.PoliceWithGun);
      this.PoliceMan = new WalkingPerson(0, AnimalType.PoliceWhite);
      this.PoliceMan2 = new WalkingPerson(0, AnimalType.PoliceBlack);
      this.PoliceMan3 = new WalkingPerson(0, AnimalType.PoliceAsian2);
      this.Sniper.ForceRotationAndHold(DirectionPressed.Left, 0.0f);
      this.PoliceMan.ForceRotationAndHold(DirectionPressed.Left, 10f);
      this.PoliceMan2.ForceRotationAndHold(DirectionPressed.Up, 10f);
      this.PoliceMan3.ForceRotationAndHold(DirectionPressed.Up, 10f);
    }

    public void UpdateCasterAndCamera()
    {
    }

    public void DrawCasterAndCamera()
    {
      this.Sniper.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(41, 44));
      ++this.Sniper.vLocation.Y;
      this.Sniper.vLocation.X -= 50f;
      this.PoliceMan.vLocation = this.Sniper.vLocation;
      this.PoliceMan.vLocation.X += 20f;
      this.Sniper.vLocation.Y -= 9f;
      this.Sniper.DrawWalkingPerson();
      this.PoliceMan2.DrawRect.X = 495;
      this.PoliceMan2.vLocation = this.Sniper.vLocation;
      this.PoliceMan2.vLocation.X -= 23f;
      this.PoliceMan2.vLocation.Y += 69f;
      this.PoliceMan2.DrawWalkingPerson();
      this.PoliceMan3.vLocation = this.Sniper.vLocation;
      this.PoliceMan3.vLocation.X += 20f;
      this.PoliceMan3.vLocation.Y += 20f;
      this.PoliceMan3.DrawWalkingPerson();
    }
  }
}
