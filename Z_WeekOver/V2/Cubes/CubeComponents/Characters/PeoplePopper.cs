// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.Characters.PeoplePopper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine.Lerp;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.Characters
{
  internal class PeoplePopper
  {
    private AnimalInFrame animalpop;
    private PopLerper poplerper;
    private bool Complete;
    private float CompleteDelay;
    public Vector2 Location;
    public float BaseScale;

    public PeoplePopper(
      IntakePerson intakeperson,
      float _BaseScale,
      float ActiveTime = -1f,
      float AnimalSize = 80f)
    {
      this.BaseScale = _BaseScale;
      this.CompleteDelay = ActiveTime;
      this.Complete = false;
      this.poplerper = new PopLerper(_peakValue: 1.1f);
      this.Construct(intakeperson, this.BaseScale, AnimalSize);
    }

    public PeoplePopper(Employee employee, float BaseScale, float ActiveTime = -1f, float AnimalSize = 80f)
    {
      this.CompleteDelay = ActiveTime;
      this.Complete = false;
      this.poplerper = new PopLerper();
      if (employee.intakeperson != null)
        this.Construct(employee.intakeperson, BaseScale, AnimalSize);
      else
        this.animalpop = new AnimalInFrame(employee.quickemplyeedescription.thisemployee, AnimalType.None, TargetSize: (BaseScale * AnimalSize), BaseScale: BaseScale);
    }

    private void Construct(IntakePerson intakeperson, float BaseScale, float AnimalSize) => this.animalpop = new AnimalInFrame(intakeperson.animaltype, intakeperson.HeadType, intakeperson.CLIndex, BaseScale * AnimalSize, BaseScale: BaseScale, HeadVariant: intakeperson.HeadVariant);

    public bool LerpComplete() => this.poplerper.HasCompleted;

    public bool UpdatePeoplePopper(float DeltaTime, bool Active)
    {
      if (Active)
      {
        int num = (int) this.poplerper.OnUpdate(DeltaTime);
        if (!this.Complete)
        {
          if ((double) this.CompleteDelay > -1.0)
          {
            this.CompleteDelay -= DeltaTime;
            if ((double) this.CompleteDelay <= 0.0)
            {
              this.Complete = true;
              return true;
            }
          }
          else if ((double) this.poplerper.CurrentValue >= 1.0)
          {
            this.Complete = true;
            return true;
          }
        }
      }
      return false;
    }

    public void DrawPeoplePopper(Vector2 Offset, SpriteBatch spritebatch) => this.animalpop.JustDrawAnimal(Offset + this.Location, spritebatch, ScaleMultiplier: this.poplerper.CurrentValue);
  }
}
