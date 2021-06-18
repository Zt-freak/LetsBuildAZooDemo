// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Variants.VariantsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Animals.DetailFrame;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Variants
{
  internal class VariantsManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private AnimalVariantsAndDNA variants;

    public VariantsManager(PrisonerInfo animal, float width, float BaseScale, Player player)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading(EnemyData.GetEnemyTypeName(animal.intakeperson.animaltype));
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      Vector2 _vScale = zero + defaultBuffer;
      this.variants = new AnimalVariantsAndDNA(animal.intakeperson.animaltype, player, BaseScale);
      this.variants.location = _vScale;
      _vScale.Y += this.variants.GetSize().Y;
      _vScale.Y += defaultBuffer.Y;
      _vScale.X = width;
      this.customerframe.Resize(_vScale);
      this.variants.location += -this.customerframe.VSCale * 0.5f;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateVariantsManager(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.variants.UpdateAnimalVariantsAndDNA(player, DeltaTime, offset, true);
    }

    public void DrawVariantsManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.variants.DrawAnimalVariantsAndDNA(offset, spriteBatch);
    }
  }
}
