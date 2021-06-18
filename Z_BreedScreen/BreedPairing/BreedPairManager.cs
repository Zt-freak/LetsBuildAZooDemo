// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedPairing.BreedPairManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Breeding;

namespace TinyZoo.Z_BreedScreen.BreedPairing
{
  internal class BreedPairManager
  {
    private LerpHandler_Float lerper;
    private SexAnimalRow Boys;
    private SexAnimalRow Girls;
    private TextButton BreedNow;
    private AnimalRow TopBabies;
    private AnimalRow BottomBabies;
    private AnimalType animal;
    private ScreenHeading HEAD;
    private SimpleTextBox Lowertextbox;
    private SimpleTextBox SelectTwoParentsParents;
    public GameObjectNineSlice FrameForWHoleThing;
    private int BreedSlot;
    public int ChildIndex = -1;
    private bool BothParentsUnlocked = true;

    public BreedPairManager(Player player, AnimalType _animal, int _BreedSlot)
    {
      this.BreedSlot = _BreedSlot;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.animal = _animal;
      this.SelectTwoParentsParents = new SimpleTextBox("Select two available parents to see potential offspring", 600f, false);
      this.SelectTwoParentsParents.text.AutoCompleteParagraph();
      this.SelectTwoParentsParents.Location = new Vector2(512f, 250f);
      this.HEAD = new ScreenHeading("Select Parents", 150f);
      this.Lowertextbox = new SimpleTextBox("Possible Children", 500f, false);
      this.FrameForWHoleThing = new GameObjectNineSlice(new Rectangle(302, 128, 21, 21), 7);
      this.FrameForWHoleThing.scale = 2f;
      this.Boys = new SexAnimalRow(this.animal, false, player);
      this.Girls = new SexAnimalRow(this.animal, true, player);
      this.Girls.Location.Y = 150f;
      this.Boys.Location.Y = 260f;
      this.Girls.Location.X = 30f;
      this.Boys.Location.X = 30f;
      this.Reconstruct(player);
    }

    public bool UpdateBreedPairManager(Player player, Vector2 Offset, float DeltaTime)
    {
      this.BottomBabies.UpdateAnimalRow(player, DeltaTime, Offset);
      this.TopBabies.UpdateAnimalRow(player, DeltaTime, Offset);
      bool flag = this.Girls.UpdateSexAnimalRow(player, Offset, DeltaTime);
      if (this.Boys.UpdateSexAnimalRow(player, Offset, DeltaTime) | flag)
        this.Reconstruct(player);
      if (this.BreedNow == null || !this.BreedNow.UpdateTextButton(player, Offset, DeltaTime) || this.ChildIndex <= -1)
        return false;
      player.breeds.StartBreed(this.BreedSlot, BreedData.breedinfo[(int) this.animal].breedentries[this.ChildIndex].PercentChanceOfThisChild, this.animal, this.ChildIndex, player, this.Boys.animalrow.SelectedINdex, this.Girls.animalrow.SelectedINdex);
      return true;
    }

    private void Reconstruct(Player player)
    {
      this.BothParentsUnlocked = true;
      List<int> intList1 = new List<int>();
      List<int> intList2 = new List<int>();
      int index1 = -1;
      if (!player.Stats.research.HasThisAnimalBeenResearched(this.animal, this.Girls.animalrow.SelectedINdex, true))
        this.BothParentsUnlocked = false;
      if (!player.Stats.research.HasThisAnimalBeenResearched(this.animal, this.Boys.animalrow.SelectedINdex))
        this.BothParentsUnlocked = false;
      if (this.BothParentsUnlocked)
      {
        for (int index2 = 0; index2 < BreedData.breedinfo[(int) this.animal].breedentries.Count; ++index2)
        {
          if (this.Girls.animalrow.SelectedINdex > -1 && BreedData.breedinfo[(int) this.animal].breedentries[index2].Parent1_girl == this.Girls.animalrow.SelectedINdex && (this.Boys.animalrow.SelectedINdex > -1 && BreedData.breedinfo[(int) this.animal].breedentries[index2].Parent2 == this.Boys.animalrow.SelectedINdex))
            index1 = index2;
          if (this.Girls.animalrow.SelectedINdex > -1 && BreedData.breedinfo[(int) this.animal].breedentries[index2].Parent1_girl == this.Girls.animalrow.SelectedINdex)
            intList1.Add(BreedData.breedinfo[(int) this.animal].breedentries[index2].Parent2);
          if (this.Boys.animalrow.SelectedINdex > -1 && BreedData.breedinfo[(int) this.animal].breedentries[index2].Parent2 == this.Boys.animalrow.SelectedINdex)
            intList2.Add(BreedData.breedinfo[(int) this.animal].breedentries[index2].Parent1_girl);
        }
      }
      this.ChildIndex = index1;
      List<int> Variants1 = new List<int>();
      for (int index2 = 0; index2 < 10; ++index2)
        Variants1.Add(0);
      if (index1 > -1)
      {
        List<int> Variants2 = new List<int>();
        int num = BreedData.breedinfo[(int) this.animal].breedentries[index1].PercentChanceOfThisChild / 5;
        for (int index2 = 9; index2 >= 0; --index2)
        {
          if ((double) index2 >= Math.Floor((double) num * 0.5))
            Variants2.Add(0);
          else
            Variants2.Add(index1);
        }
        this.TopBabies = new AnimalRow(this.animal, false, player, true, Variants2);
        List<int> Variants3 = new List<int>();
        for (int index2 = 9; index2 >= 0; --index2)
        {
          if ((double) index2 >= Math.Ceiling((double) num * 0.5))
            Variants3.Add(0);
          else
            Variants3.Add(index1);
        }
        this.BottomBabies = new AnimalRow(this.animal, false, player, true, Variants3);
        this.BreedNow = new TextButton("Breed", 60f);
        this.BreedNow.vLocation = new Vector2(512f, 600f);
      }
      else
      {
        this.TopBabies = new AnimalRow(AnimalType.QuestionMark, false, player, true, Variants1);
        this.BottomBabies = new AnimalRow(AnimalType.QuestionMark, false, player, true, Variants1);
        this.BreedNow = (TextButton) null;
      }
      this.TopBabies.Location.Y = 200f;
      this.BottomBabies.Location.Y = 300f;
      for (int index2 = 0; index2 < 10; ++index2)
      {
        this.Girls.animalrow.animal[index2].BreedPartner = false;
        this.Boys.animalrow.animal[index2].BreedPartner = false;
        if (intList2.Contains(index2) && this.Boys.animalrow.animal[index2].IsUnlocked)
          this.Girls.animalrow.animal[index2].BreedPartner = true;
        if (intList1.Contains(index2) && this.Girls.animalrow.animal[index2].IsUnlocked)
          this.Boys.animalrow.animal[index2].BreedPartner = true;
      }
    }

    public void DrawBreedPairManager(Vector2 Offset)
    {
      this.TopBabies.Location.Y = 425f;
      this.BottomBabies.Location.Y = 500f;
      this.Lowertextbox.DrawSimpleTextBox(Offset + new Vector2(512f, 350f), 5f);
      Vector2 Offset1 = Offset + this.TopBabies.Location + new Vector2(337.5f, 35f);
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset1, new Vector2(800f, 160f));
      Offset1.Y += this.FrameForWHoleThing.vLocation.Y;
      if (this.BothParentsUnlocked && this.Boys.animalrow.SelectedINdex > -1 && this.Girls.animalrow.SelectedINdex > -1)
      {
        this.BottomBabies.DrawAnimalRow(Offset, AssetContainer.pointspritebatch03);
        this.TopBabies.DrawAnimalRow(Offset, AssetContainer.pointspritebatch03);
      }
      else
      {
        this.SelectTwoParentsParents.Location.Y = Offset1.Y;
        this.SelectTwoParentsParents.DrawSimpleTextBox(Offset, 3f, AssetContainer.pointspritebatch03);
      }
      this.HEAD.DrawScreenHeading(Offset + new Vector2(512f, 80f), AssetContainer.pointspritebatch03);
      this.Girls.DrawSexAnimalRow(Offset, AssetContainer.pointspritebatch03);
      this.Boys.DrawSexAnimalRow(Offset, AssetContainer.pointspritebatch03);
      if (this.BreedNow == null)
        return;
      this.BreedNow.DrawTextButton(Offset);
    }
  }
}
