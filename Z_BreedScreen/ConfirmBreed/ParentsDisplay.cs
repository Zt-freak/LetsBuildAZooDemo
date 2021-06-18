// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.ParentsDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class ParentsDisplay
  {
    public AnimalAndSex Mum;
    public AnimalAndSex Dad;
    public Vector2 Location;
    private ZGenericText parentsHeading;

    public ParentsDisplay(
      int MotherVariant,
      int FaterVariant,
      AnimalType animaltype,
      float BaseScale,
      bool DrawHeading = true,
      bool FatherDead = false,
      bool MotherIsDead = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      double defaultYbuffer = (double) uiScaleHelper.GetDefaultYBuffer();
      this.Mum = new AnimalAndSex(MotherVariant, animaltype, true, "Mother".ToUpper(), BaseScale, MotherIsDead);
      this.Dad = new AnimalAndSex(FaterVariant, animaltype, false, "Father".ToUpper(), BaseScale, FatherDead);
      this.Mum.Location.X = (float) (-(double) this.Mum.GetSize().X * 0.5 - (double) defaultXbuffer * 0.5);
      this.Dad.Location.X = (float) ((double) this.Dad.GetSize().X * 0.5 + (double) defaultXbuffer * 0.5);
      if (!DrawHeading)
        return;
      this.parentsHeading = new ZGenericText("Parents:".ToUpper(), BaseScale, _UseOnePointFiveFont: true);
      this.parentsHeading.vLocation.Y = (float) (-(double) this.Mum.GetSize().Y * 0.5 - (double) this.parentsHeading.GetSize().Y * 0.5);
    }

    public float GetHeight() => this.Mum.GetHeight() + this.GetTextOffset();

    public float GetTextOffset() => this.parentsHeading != null ? this.parentsHeading.GetSize().Y : 0.0f;

    public void DrawParentsDisplay(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      if (this.parentsHeading != null)
        this.parentsHeading.DrawZGenericText(Offset, spritebatch);
      this.Mum.DrawAnimalAndSex(Offset, spritebatch);
      this.Dad.DrawAnimalAndSex(Offset, spritebatch);
    }
  }
}
