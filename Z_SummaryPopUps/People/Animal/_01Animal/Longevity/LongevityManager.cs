// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity.LongevityManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity
{
  internal class LongevityManager
  {
    private CustomerFrame customerframe;
    private AnimalInFrame animalinframe;
    private NameSexBreed namesexbreed;
    private LongevityHeaderAndBar headerandbar;
    public Vector2 Location;

    public LongevityManager(PrisonerInfo animal, float width, float BaseScale, bool IsDead = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 vector2_1 = uiScaleHelper.ScaleVector2(Vector2.One * 80f);
      this.animalinframe = new AnimalInFrame(animal.GetAnimalPainted(), animal.intakeperson.HeadType, animal.intakeperson.CLIndex, vector2_1.X, 6f * BaseScale, BaseScale, animal.intakeperson.HeadVariant);
      this.namesexbreed = new NameSexBreed(animal, Vector2.Zero, BaseScale, IsDead);
      this.headerandbar = new LongevityHeaderAndBar(animal, Vector2.Zero, BaseScale);
      Vector2 _VSCale = defaultBuffer;
      this.animalinframe.Location = _VSCale;
      this.animalinframe.Location += this.animalinframe.GetSize() * 0.5f;
      _VSCale.X += this.animalinframe.GetSize().X;
      _VSCale.X += defaultBuffer.X;
      this.namesexbreed.Location = _VSCale;
      _VSCale.Y += this.namesexbreed.GetHeight();
      _VSCale.Y += defaultBuffer.Y;
      this.headerandbar.Location = _VSCale;
      _VSCale.Y += this.headerandbar.GetSize().Y;
      _VSCale.Y += defaultBuffer.Y;
      _VSCale.X = width;
      this.customerframe = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2_2 = -this.customerframe.VSCale * 0.5f;
      this.animalinframe.Location += vector2_2;
      this.namesexbreed.Location += vector2_2;
      this.headerandbar.Location += vector2_2;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateLongevityManager()
    {
    }

    public void DrawLongevityManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.animalinframe.DrawAnimalInFrame(offset, spriteBatch);
      this.namesexbreed.DrawNameSexBreed(offset);
      this.headerandbar.DrawLongevityHeaderAndBar(offset, spriteBatch);
    }
  }
}
