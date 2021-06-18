// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._05Info.InfoTabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Description;
using TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Diet;
using TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Ideal;
using TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Variants;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._05Info
{
  internal class InfoTabManager
  {
    public Vector2 location;
    private VariantsManager variantsmanager;
    private IdealHabitatManager idealhabitatmanager;
    private DescripManager descriptionmanager;
    private DietInfoFrame dietinfo;
    private Vector2 size;

    public InfoTabManager(PrisonerInfo animal, Player player, float width, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.size = Vector2.Zero;
      this.variantsmanager = new VariantsManager(animal, width, BaseScale, player);
      this.variantsmanager.location.Y = this.size.Y;
      this.variantsmanager.location.Y += this.variantsmanager.GetSize().Y * 0.5f;
      this.size.Y += this.variantsmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.descriptionmanager = new DescripManager(animal.intakeperson.animaltype, width, BaseScale);
      this.descriptionmanager.location.Y = this.size.Y;
      this.descriptionmanager.location.Y += this.descriptionmanager.GetSize().Y * 0.5f;
      this.size.Y += this.descriptionmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.idealhabitatmanager = new IdealHabitatManager(animal, width, BaseScale);
      this.idealhabitatmanager.location.Y = this.size.Y;
      this.idealhabitatmanager.location.Y += this.idealhabitatmanager.GetSize().Y * 0.5f;
      this.size.Y += this.idealhabitatmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.dietinfo = new DietInfoFrame(animal.intakeperson.animaltype, player, width, BaseScale);
      this.dietinfo.location.Y = this.size.Y;
      this.dietinfo.location.Y += this.dietinfo.GetSize().Y * 0.5f;
      this.size.Y += this.dietinfo.GetSize().Y;
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateInfoTabManager(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.variantsmanager.UpdateVariantsManager(player, DeltaTime, offset);
    }

    public void DrawInfoTabManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.descriptionmanager.DrawDescripManager(offset, spriteBatch);
      this.idealhabitatmanager.DrawIdealHabitatManager(offset, spriteBatch);
      this.variantsmanager.DrawVariantsManager(offset, spriteBatch);
      this.dietinfo.DrawDietInfoFrame(offset, spriteBatch);
    }
  }
}
