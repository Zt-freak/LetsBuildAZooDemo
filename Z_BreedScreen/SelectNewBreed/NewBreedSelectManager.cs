// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectNewBreed.NewBreedSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;

namespace TinyZoo.Z_BreedScreen.SelectNewBreed
{
  internal class NewBreedSelectManager
  {
    private SpeciesSelectManager speciesselect;
    public Vector2 location;

    public NewBreedSelectManager(Player player, float BaseScale) => this.speciesselect = new SpeciesSelectManager(player, BaseScale);

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.speciesselect.CheckMouseOver(player, offset);
    }

    public AnimalsForBreedInfo UpdateNewBreedSelectManager(
      Player player,
      float DeltaTime,
      out bool Cancel)
    {
      return this.speciesselect.UpdateSpeciesSelectManager(player, DeltaTime, this.location, out Cancel);
    }

    public void LerpIn() => this.speciesselect.LerpIn();

    public void LerpOff() => this.speciesselect.LerpOff();

    public void ResetSelection() => this.speciesselect.ResetSelection();

    public Vector2 GetSize() => this.speciesselect.GetSize();

    public void DrawNewBreedSelectManager(SpriteBatch spriteBatch) => this.speciesselect.DrawSpeciesSelectManager(this.location, spriteBatch);
  }
}
