// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy.Mate
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy
{
  internal class Mate
  {
    private AnimalInFrame animalinframe;
    public Vector2 Location;
    private ZGenericText MateText;
    private Vector2 size;

    public Mate(PrisonerInfo animal, Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.MateText = new ZGenericText(nameof (Mate), BaseScale);
      ActiveBreed thisBreed = player.breeds.GetThisBreed(animal.intakeperson.UID);
      if (thisBreed == null)
        throw new Exception("NO BREEDER");
      float TargetSize = 30f * BaseScale;
      PrisonerInfo thisAnimal = player.prisonlayout.GetThisAnimal(thisBreed.FatherUID, out int _);
      if (thisAnimal == null)
      {
        this.animalinframe = new AnimalInFrame(thisBreed.animalType, AnimalType.None, TargetSize: TargetSize, FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
        this.animalinframe.SetDead(thisBreed.animalType, thisBreed.MaleParentVARIANT);
      }
      else
        this.animalinframe = new AnimalInFrame(thisAnimal.intakeperson.animaltype, thisAnimal.intakeperson.HeadType, thisAnimal.intakeperson.CLIndex, TargetSize, 6f * BaseScale, BaseScale, thisAnimal.intakeperson.HeadVariant);
      this.MateText.vLocation.Y += this.MateText.GetSize().Y * 0.5f;
      this.size.Y += this.MateText.GetSize().Y;
      this.animalinframe.Location = this.size;
      this.animalinframe.Location.Y += this.animalinframe.GetSize().Y * 0.5f;
      this.size.Y += this.animalinframe.GetSize().Y;
      this.size.X = Math.Max(this.MateText.GetSize().X, this.animalinframe.GetSize().X);
    }

    public Vector2 GetSize() => this.size;

    public void UpdateMate()
    {
    }

    public void DrawMate(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.MateText.DrawZGenericText(Offset, spriteBatch);
      this.animalinframe.DrawAnimalInFrame(Offset, spriteBatch);
    }
  }
}
