// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Office.OfficeEnvironment
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.employees;

namespace TinyZoo.Z_Manage.Hiring.Interview.Office
{
  internal class OfficeEnvironment
  {
    private WalkingPerson Me;
    private WalkingPerson Employee;
    private int Frame;
    private GameObject Pointer;
    private GameObject World;
    private float FrameTimer;

    public OfficeEnvironment(PotentialHire potentialhire, Player player)
    {
      this.Me = new WalkingPerson(0, (AnimalType) player.Stats.ZooKeeperAvatarIndex);
      this.Employee = new WalkingPerson(0, potentialhire.intakeperson.animaltype, IsRenderOnly: true);
      this.Pointer = new GameObject();
      this.World = new GameObject();
      this.Pointer.DrawRect = new Rectangle(97, 9, 13, 12);
      this.World.DrawRect = new Rectangle(658, 35, 140, 63);
      this.Pointer.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.Pointer.DrawOrigin.Y += 26f;
      this.World.SetDrawOriginToCentre();
    }

    public void UpdateOfficeEnvironment(float DeltaTime)
    {
      this.FrameTimer += DeltaTime;
      if ((double) this.FrameTimer <= 0.200000002980232)
        return;
      ++this.Frame;
      if (this.Frame > 3)
        this.Frame = 0;
      this.FrameTimer = 0.0f;
    }

    public void DrawOfficeEnvironment(
      Vector2 Offset,
      bool IsAskingQuestions,
      bool IsAnswering,
      float WalkInOffset)
    {
      if ((double) WalkInOffset == 0.0)
        this.Frame = 0;
      float num1 = 3f;
      this.Employee.scale = num1;
      this.Employee.DrawOrigin.Y = (float) (this.Employee.DrawRect.Height + 9);
      this.Me.scale = num1;
      this.Me.DrawOrigin.Y = (float) (this.Me.DrawRect.Height + 9);
      this.Employee.vLocation = Vector2.Zero;
      this.Me.vLocation = Vector2.Zero;
      int frame1 = this.Frame;
      this.Employee.fAlpha = 1f;
      float y = 680f;
      this.Pointer.scale = num1;
      this.World.scale = num1;
      this.World.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.EnvironmentSheet, Offset + new Vector2(512f, y));
      this.World.DrawOrigin.Y = 51f;
      float num2 = 25f;
      if (!IsAnswering & IsAskingQuestions)
        this.Pointer.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2((float) (512.0 - ((double) num2 * (double) num1 + (double) WalkInOffset)), y) + this.Me.vLocation);
      this.Employee.ScreenSpaceDrawWalkingPerson(Offset + new Vector2((float) (512.0 + (double) num2 * (double) num1) + WalkInOffset, y), AssetContainer.pointspritebatchTop05, DirectionPressed.Left, this.Frame);
      int frame2 = this.Frame;
      this.Me.fAlpha = 1f;
      if (IsAnswering && !IsAskingQuestions)
        this.Pointer.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2((float) (512.0 + ((double) num2 * (double) num1 + (double) WalkInOffset)), y) + this.Employee.vLocation);
      this.Me.ScreenSpaceDrawWalkingPerson(Offset + new Vector2((float) (512.0 - ((double) num2 * (double) num1 + (double) WalkInOffset)), y), AssetContainer.pointspritebatchTop05, DirectionPressed.Right, this.Frame);
    }
  }
}
