// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.SeedPicker.SeedPackCollection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_Collection.Animals;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Farms.CropSum.SeedPicker
{
  internal class SeedPackCollection
  {
    private CustomerFrame customerframe;
    private List<SeedAndStats> seedandstats;
    private AnimalCollectionPage animalcollectionpage;
    private Vector2 VSCALE_NOTRENDERED;

    public SeedPackCollection(float BaseScale, Player player)
    {
      this.VSCALE_NOTRENDERED = new UIScaleHelper(BaseScale).ScaleVector2(new Vector2(550f, 400f));
      this.animalcollectionpage = new AnimalCollectionPage(CollectionType.Seeds, player, BaseScale, this.VSCALE_NOTRENDERED, 4);
    }

    public Vector2 GetSize() => this.VSCALE_NOTRENDERED;

    public void UpdateSeedPackCollection(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      out bool ForceClosePanel)
    {
      this.animalcollectionpage.UpdateAnimalCollectionPage(player, DeltaTime, Offset, out ForceClosePanel);
    }

    public void DrawSeedPackCollection(Vector2 Offset) => this.animalcollectionpage.DrawAnimalCollectionPage(Offset, AssetContainer.pointspritebatchTop05);
  }
}
