// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.StaffPayment.EmplyeeAndStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_WeekOver.StaffPayment
{
  internal class EmplyeeAndStats : AnimatedGameObject
  {
    private int Salary;
    private GameObject Salaryobj;
    private bool DrawPersonOnly;

    public EmplyeeAndStats(
      AnimalType thispersontype,
      int _Salary,
      EMOTION emotion,
      Vector3 SalaryColour)
    {
      this.DrawPersonOnly = false;
      this.Salary = _Salary;
      this.DrawRect = EnemyData.GetEnemyRectangle(thispersontype).WalkDown;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.SetUpSimpleAnimation(4, 0.2f);
      this.Salaryobj = new GameObject();
      this.Salaryobj.SetAllColours(SalaryColour);
      this.scale = 2f;
    }

    public EmplyeeAndStats(AnimalType thispersontype)
    {
      this.DrawPersonOnly = true;
      this.DrawRect = EnemyData.GetEnemyRectangle(thispersontype).WalkDown;
      this.SetDrawOriginToPoint(DrawOriginPosition.Centre);
      this.SetUpSimpleAnimation(4, 0.2f);
    }

    public void UpdateEmplyeeAndStats(float DeltaTime) => this.UpdateAnimation(DeltaTime);

    public void DrawEmplyeeAndStats(Vector2 Offset)
    {
      this.Draw(AssetContainer.pointspritebatch03, AssetContainer.AnimalSheet, Offset);
      if (this.DrawPersonOnly)
        return;
      TextFunctions.DrawJustifiedText("$" + (object) this.Salary, RenderMath.GetPixelSizeBestMatch(1f) * 0.5f, new Vector2(0.0f, 10f) + Offset + this.vLocation, this.Salaryobj.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
    }
  }
}
