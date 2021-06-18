// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies.SpeciesSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies
{
  internal class SpeciesSelectManager
  {
    private AnimalsForBreedInfo[] AvailableBreeds;
    public List<BreedMeIcon> breedicons;
    public Vector2 Location;
    private MainBreedSpeciesPanel mianbreedpanel;

    public SpeciesSelectManager(Player player, float BaseScale)
    {
      this.AvailableBreeds = new AnimalsForBreedInfo[56];
      this.mianbreedpanel = new MainBreedSpeciesPanel(player, BaseScale);
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.HeadType == AnimalType.None && !player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].GetIsABaby() && player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].IsFertile)
          {
            if (this.AvailableBreeds[(int) player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype] == null)
              this.AvailableBreeds[(int) player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype] = new AnimalsForBreedInfo();
            this.AvailableBreeds[(int) player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype].AddThis(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2], index1);
          }
        }
      }
      this.breedicons = new List<BreedMeIcon>();
      for (int index = 0; index < this.AvailableBreeds.Length; ++index)
      {
        if (this.AvailableBreeds[index] != null && this.AvailableBreeds[index].HasBreedingPair())
        {
          bool IsNew = this.AvailableBreeds[index].HasNewOpportunity(player) > 0;
          this.mianbreedpanel.SetUpThis(this.AvailableBreeds[index], IsNew);
        }
      }
    }

    public void LerpIn() => this.mianbreedpanel.LerpIn();

    public void LerpOff() => this.mianbreedpanel.LerpOff();

    public void ResetSelection() => this.mianbreedpanel.ResetSelection();

    public Vector2 GetSize() => this.mianbreedpanel.GetSize();

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return this.mianbreedpanel.CheckMouseOver(player, offset);
    }

    public AnimalsForBreedInfo UpdateSpeciesSelectManager(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool Cancel)
    {
      offset += this.Location;
      return this.mianbreedpanel.UpdateMainBreedSpeciesPanel(DeltaTime, player, offset, out Cancel);
    }

    public void DrawSpeciesSelectManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      this.mianbreedpanel.DrawMainBreedSpeciesPanel(offset, spriteBatch);
    }
  }
}
