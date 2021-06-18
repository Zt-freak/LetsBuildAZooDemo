// Decompiled with JetBrains decompiler
// Type: TinyZoo.DebugViewer.DebugAnimalViewer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using System.Collections.Generic;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.NewDiscoveryScreen;

namespace TinyZoo.DebugViewer
{
  internal class DebugAnimalViewer
  {
    private bool ShowingCollection;
    private newThingRenderer newthingget;
    private CollectionScreenManager collection;
    private List<AnimalRenderer> animal;
    private AnimalType selectedanmal;
    private int HeadIndex;
    private int ZOOM = 8;
    private int BodyVariant;
    private int AnimalHead;
    private int Page;
    private bool UsingBodyToHeads = true;

    public DebugAnimalViewer(Player player)
    {
      this.Page = 1;
      TinyZoo.Game1.ClsCLR.SetAllColours(0.0f, 0.0f, 0.0f);
      this.ShowingCollection = true;
      this.collection = new CollectionScreenManager(player);
    }

    public void UpdateDebugAnimalViewer(float DeltaTime, Player player)
    {
      if (this.ShowingCollection)
      {
        bool ExitDone;
        AnimalType animalType = this.collection.UpdateCollectionScreenManager(Vector2.Zero, DeltaTime, player, out ExitDone, out bool _);
        if (ExitDone && TinyZoo.Game1.GetNextGameState() != GAMESTATE.OverWorld)
        {
          TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
          TinyZoo.Game1.screenfade.BeginFade(true);
        }
        if (animalType != AnimalType.None && this.selectedanmal != animalType)
        {
          this.selectedanmal = animalType;
          this.ShowingCollection = false;
          this.MakeThese();
          player.player.touchinput.ReleaseTapArray[0].X = -1000f;
        }
      }
      if (this.ShowingCollection)
        return;
      if (PC_KeyState.Plus_PressedThisFrame)
      {
        ++this.BodyVariant;
        if (this.BodyVariant > 9)
          this.BodyVariant = 0;
        this.MakeThese();
      }
      if (PC_KeyState.Minus_PressedThisFrame)
      {
        --this.BodyVariant;
        if (this.BodyVariant < 0)
          this.BodyVariant = 9;
        this.MakeThese();
      }
      if (PC_KeyState.OpenBrackets_PressedThisFrame)
      {
        --this.AnimalHead;
        if (this.AnimalHead < 0)
          this.AnimalHead = 56;
        this.MakeThese();
      }
      if (PC_KeyState.CloseBrackets_PressedThisFrame)
      {
        ++this.AnimalHead;
        if (this.AnimalHead > 56)
          this.AnimalHead = 0;
        this.MakeThese();
      }
      if (PC_KeyState.Up_Pressed)
      {
        if (this.ZOOM > 1)
        {
          --this.ZOOM;
          this.MakeThese();
        }
      }
      else if (PC_KeyState.Down_Pressed)
      {
        ++this.ZOOM;
        this.MakeThese();
      }
      if (PC_KeyState.Right_Pressed)
      {
        if (this.UsingBodyToHeads)
        {
          if (this.Page < 2)
          {
            ++this.Page;
            this.MakeThese();
          }
        }
        else if (this.HeadIndex < 56)
        {
          if (this.HeadIndex < 0)
            ++this.HeadIndex;
          this.MakeThese();
        }
      }
      else if (PC_KeyState.Left_Pressed)
      {
        if (this.UsingBodyToHeads)
        {
          if (this.Page > 0)
          {
            --this.Page;
            this.MakeThese();
          }
        }
        else if (this.HeadIndex > 0 && this.HeadIndex != 10)
        {
          if (this.HeadIndex > 0)
            this.HeadIndex -= 21;
          if (this.HeadIndex < 0)
            this.HeadIndex = 0;
          else
            ++this.HeadIndex;
        }
        else
          this.HeadIndex = -2;
        this.MakeThese();
      }
      for (int index = 0; index < this.animal.Count; ++index)
        this.animal[index].UpdateAnimal(DeltaTime);
      if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0)
        return;
      this.ShowingCollection = true;
    }

    private void MakeThese()
    {
      this.animal = new List<AnimalRenderer>();
      int num1 = 0;
      int num2 = 0;
      int HeadVariant = 0;
      for (int index = 0; index < 10; ++index)
      {
        if (this.Page == 1)
          this.animal.Add(new AnimalRenderer(this.selectedanmal, index));
        else if (this.Page == 0)
          this.animal.Add(new AnimalRenderer(this.selectedanmal, index, this.selectedanmal, index));
        else if (this.UsingBodyToHeads)
        {
          this.animal.Add(new AnimalRenderer(this.selectedanmal, this.BodyVariant, (AnimalType) this.AnimalHead, HeadVariant));
          ++HeadVariant;
        }
        else if (this.HeadIndex < 56)
        {
          this.animal.Add(new AnimalRenderer(this.selectedanmal, this.BodyVariant, (AnimalType) this.AnimalHead, this.AnimalHead));
          ++this.HeadIndex;
        }
        if (index < this.animal.Count)
        {
          this.animal[index].enemy.vLocation = new Vector2((float) (100 + num1 * 200), (float) (100 + num2 * 200));
          ++num1;
          if (num1 > 4)
          {
            num1 = 0;
            ++num2;
          }
          this.animal[index].enemy.scale = (float) this.ZOOM;
        }
      }
    }

    public void DrawDebugAnimalViewer()
    {
      if (this.ShowingCollection)
      {
        this.collection.DrawCollectionScreenManager(Vector2.Zero);
        TextFunctions.DrawJustifiedText("Click on an animal to see the variants", 2f, new Vector2(512f, 700f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
      }
      else
      {
        string stringToDraw = "Showing Default Variants";
        float x = 512f;
        if (this.Page == 0)
          stringToDraw = "Showing reference Variants";
        else if (this.Page == 2)
          stringToDraw = "Showing Hybrid - Current Body Variant: " + (object) this.BodyVariant + " Current Head Type: " + (object) (AnimalType) this.AnimalHead;
        TextFunctions.DrawJustifiedText(stringToDraw, Z_GameFlags.GetBaseScaleForUI(), new Vector2(x, 20f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03);
        for (int index = 0; index < this.animal.Count; ++index)
        {
          if (index < 5)
            this.animal[index].enemy.vLocation.Y = 300f;
          else
            this.animal[index].enemy.vLocation.Y = 600f;
          this.animal[index].DrawAnimal();
        }
        TextFunctions.DrawJustifiedText("Use the arrow keys to cycle through different options", Z_GameFlags.GetBaseScaleForUI(), new Vector2(512f, 650f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03);
        TextFunctions.DrawJustifiedText("Use + & - to cycle Body Variant, and [ & ] to cycle head animal", Z_GameFlags.GetBaseScaleForUI(), new Vector2(512f, 710f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03);
      }
    }
  }
}
