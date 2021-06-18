// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.NewsCaster.CasterAndCamera
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_Events.NewsCaster
{
  internal class CasterAndCamera
  {
    private WalkingPerson Lady;
    private WalkingPerson CameraGuy;
    private bool LadyIsTalking;
    private float TalkTime;
    private int talkFrame;
    private int TalkCycles;
    private Rectangle ladystartrect;

    public CasterAndCamera()
    {
      this.Lady = new WalkingPerson(0, AnimalType.NewsReporter);
      this.CameraGuy = new WalkingPerson(0, AnimalType.Cameraman);
      this.Lady.ForceRotationAndHold(DirectionPressed.Down, 0.0f);
      this.CameraGuy.ForceRotationAndHold(DirectionPressed.Left, 10f);
      this.Lady.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(39, 52));
      this.Lady.vLocation = TileMath.GetTileToWorldSpace(TileMath.GetGateLocationvector2Int() + new Vector2Int(7, 2));
      this.Lady.vLocation.Y -= 12f;
      this.Lady.vLocation.X -= 65f;
      this.CameraGuy.vLocation = this.Lady.vLocation;
      this.CameraGuy.vLocation.X += 20f;
      this.Lady.vLocation.Y -= 9f;
      this.StartTalking();
    }

    private void StartTalking()
    {
      this.TalkCycles = 4;
      this.LadyIsTalking = true;
      this.ladystartrect = this.Lady.DrawRect;
    }

    public void UpdateCasterAndCamera(float DeltaTime)
    {
      if (!this.LadyIsTalking)
        return;
      this.TalkTime -= DeltaTime;
      if ((double) this.TalkTime >= 0.0)
        return;
      ++this.talkFrame;
      if (this.talkFrame > 3)
      {
        --this.TalkCycles;
        this.talkFrame = 0;
      }
      this.Lady.DrawRect = new Rectangle(976 + 17 * this.talkFrame, 928, 16, 18);
      this.TalkTime = 0.1f;
      if (this.TalkCycles != 0)
        return;
      this.TalkTime = (float) TinyZoo.Game1.Rnd.Next(40, 100);
      this.TalkTime *= 0.01f;
      this.TalkCycles = TinyZoo.Game1.Rnd.Next(2, 6);
    }

    public void DrawCasterAndCamera()
    {
      this.CameraGuy.DrawWalkingPerson();
      this.Lady.DrawWalkingPerson();
    }
  }
}
