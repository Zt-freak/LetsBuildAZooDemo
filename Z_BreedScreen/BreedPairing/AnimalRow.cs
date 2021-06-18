// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedPairing.AnimalRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BreedScreen.BreedPairing
{
  internal class AnimalRow
  {
    public AlienEntry[] animal;
    public Vector2 Location;
    private bool IsLockSelect;
    public int SelectedINdex;
    private AlienEntry lockselectframe;
    private AlienEntry PartnerFrame;
    private bool BlockMouseOver;
    private string HeldString;

    public AnimalRow(
      AnimalType animaltype,
      bool IsParentRow,
      Player player,
      bool _IsLockSelect,
      List<int> Variants = null,
      List<int> Partners = null,
      bool IsAGirl = false,
      bool IsForTradeScreen = false)
    {
      this.BlockMouseOver = !IsParentRow;
      this.lockselectframe = new AlienEntry(AnimalType.None, false, true, SCALEs: 2.3f);
      this.PartnerFrame = new AlienEntry(AnimalType.None, false, true, SCALEs: 2.2f);
      this.SelectedINdex = -1;
      this.IsLockSelect = _IsLockSelect;
      this.animal = new AlienEntry[10];
      if (Variants != null)
      {
        for (int index = 0; index < this.animal.Length; ++index)
        {
          bool flag = true;
          int variant = Variants[index];
          if (!player.Stats.research.HasThisAnimalBeenResearched(animaltype, Variants[index]))
            flag = false;
          this.animal[index] = new AlienEntry(animaltype, flag, flag, Variants[index], 2f);
          this.animal[index].vLocation.X = (float) (index * 75);
          int num = this.animal[index].BreedPartner ? 1 : 0;
        }
      }
      else
      {
        for (int index = 0; index < this.animal.Length; ++index)
        {
          bool flag = true;
          if (IsParentRow && !player.Stats.research.HasThisAnimalBeenResearched(animaltype, index, IsAGirl))
            flag = false;
          this.animal[index] = new AlienEntry(animaltype, flag, flag, index, 2f);
          if (IsForTradeScreen)
          {
            int totalOfThisAlien = player.prisonlayout.cellblockcontainer.GetTotalOfThisALien(animaltype, index, IsAGirl);
            if (totalOfThisAlien == 0)
              this.animal[index].AddStringBelow(string.Concat((object) totalOfThisAlien), Color.Gray);
            else
              this.animal[index].AddStringBelow(string.Concat((object) totalOfThisAlien), new Color(220, (int) byte.MaxValue, 220));
          }
          this.animal[index].vLocation.X = (float) (index * 75);
        }
      }
      this.Location.X = 174.5f;
    }

    public bool UpdateAnimalRow(Player player, float DeltaTime, Vector2 Offset)
    {
      int selectedIndex = this.SelectedINdex;
      for (int index = 0; index < this.animal.Length; ++index)
      {
        if (this.animal[index].UpdateAlienEntry(this.Location + Offset, DeltaTime, player))
          this.SelectedINdex = index;
      }
      if (this.PartnerFrame != null)
        this.PartnerFrame.UpdateAlienEntry(Vector2.Zero, DeltaTime, player);
      if (!this.IsLockSelect && this.SelectedINdex > -1)
        this.lockselectframe.UpdateAlienEntry(this.animal[this.SelectedINdex].vLocation, DeltaTime, player);
      return selectedIndex != this.SelectedINdex;
    }

    public void DrawAnimalRow(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      for (int index = 0; index < this.animal.Length; ++index)
      {
        if (this.BlockMouseOver)
        {
          this.animal[index].baseframe.MouseOverWhite = false;
          this.SelectedINdex = -1;
          this.animal[index].baseframe.MouseOver = false;
        }
        if (index == this.SelectedINdex)
        {
          this.lockselectframe.baseframe.SetAllColours(0.2f, 0.7f, 0.2f);
          this.lockselectframe.DrawAlienEntry(this.Location + this.animal[index].vLocation + Offset, DrawWithThis);
          this.animal[index].baseframe.MouseOverWhite = true;
        }
        else if (this.animal[index].BreedPartner)
        {
          this.PartnerFrame.baseframe.SetAllColours(1f, 0.3f, 0.8f);
          this.PartnerFrame.DrawAlienEntry(this.Location + this.animal[index].vLocation + Offset, DrawWithThis);
          this.animal[index].baseframe.MouseOverWhite = true;
        }
        else
          this.animal[index].baseframe.MouseOverWhite = false;
        if ((this.SelectedINdex != -1 || !this.BlockMouseOver) && index != this.SelectedINdex)
        {
          this.animal[index].baseframe.SetAllColours(0.6f, 0.6f, 0.6f);
          this.animal[index].DrawAlienEntry(this.Location + Offset, DrawWithThis);
        }
        else
        {
          this.animal[index].baseframe.SetAllColours(1f, 1f, 1f);
          this.animal[index].DrawAlienEntry(this.Location + Offset, DrawWithThis);
        }
      }
    }
  }
}
