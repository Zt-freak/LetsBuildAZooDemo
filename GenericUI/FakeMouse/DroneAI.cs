// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.FakeMouse.DroneAI
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv;

namespace TinyZoo.GenericUI.FakeMouse
{
  internal class DroneAI
  {
    internal static Vector2 DroneLocation;
    private Vector2 DroneTargetLocation;
    private Enemy enemy;
    private float CurrentSpeed;
    private float TopSpeed;
    private float HangAroundTimer;
    private bool OnTheWay;
    private bool WasAccelerating;

    public void UpdateDroneAI(float DeltaTime, OverWorldEnvironmentManager overworldmanager)
    {
      if (this.enemy == null)
      {
        if ((double) this.HangAroundTimer > 0.0)
          this.HangAroundTimer = -1f;
        if ((double) this.HangAroundTimer <= 0.0)
        {
          this.enemy = overworldmanager.GetRandomPerson();
          this.OnTheWay = true;
          this.HangAroundTimer = (float) Game1.Rnd.Next(2, 12);
        }
        this.CurrentSpeed = 0.0f;
      }
      else
      {
        Vector2 vector2 = this.enemy.enemyrenderere.vLocation + new Vector2(0.0f, -60f) - DroneAI.DroneLocation;
        if ((double) vector2.Length() < 100.0)
        {
          float num = vector2.Length() / 100f;
          if ((double) num < (double) this.CurrentSpeed || !this.WasAccelerating)
          {
            this.WasAccelerating = false;
            this.CurrentSpeed = num;
          }
          if ((double) vector2.Length() < 15.0 || (double) this.CurrentSpeed == 0.0)
            this.OnTheWay = false;
        }
        else
        {
          this.WasAccelerating = true;
          if ((double) this.CurrentSpeed < 1.0)
            this.CurrentSpeed += DeltaTime * 2f;
        }
        vector2.Normalize();
        DroneAI.DroneLocation += vector2 * DeltaTime * 300f * this.CurrentSpeed;
        if (this.OnTheWay)
          return;
        this.HangAroundTimer -= DeltaTime;
        if ((double) this.HangAroundTimer > 0.0)
          return;
        this.enemy = (Enemy) null;
      }
    }
  }
}
