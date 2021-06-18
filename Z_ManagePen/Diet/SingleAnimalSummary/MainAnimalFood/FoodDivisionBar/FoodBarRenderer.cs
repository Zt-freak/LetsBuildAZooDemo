// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar.FoodBarRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar
{
  internal class FoodBarRenderer : GameObject
  {
    private List<GameObject> frames;
    private List<FoodPointer> pointers;
    private LerpHandler_Float lerper;
    private List<LerpHandler_Float> lerpers;
    private List<int> On;
    private FoodPointer IDEAL;

    public FoodBarRenderer(float BaseScale)
    {
      this.pointers = new List<FoodPointer>();
      this.scale = BaseScale;
      this.DrawRect = new Rectangle(596, 635, 288, 10);
      this.frames = new List<GameObject>();
      this.vLocation.X = (float) ((double) this.scale * (double) this.DrawRect.Width * 0.5 * -1.0);
      this.lerpers = new List<LerpHandler_Float>();
      this.On = new List<int>();
      this.IDEAL = new FoodPointer((AnimalFoodEntry) null);
      this.IDEAL.vLocation.X = this.scale * ((float) this.DrawRect.Width * 0.6666667f);
    }

    public void AddSegment(
      float FULLPercentEnd,
      bool IsMeat,
      AnimalFoodEntry animalfoodentry,
      bool IsOn)
    {
      if (IsOn)
        this.On.Add(2);
      else
        this.On.Add(0);
      GameObject gameObject = new GameObject((GameObject) this);
      if (IsMeat)
      {
        gameObject.DrawRect.X = 596;
        gameObject.DrawRect.Y = 624;
      }
      else
      {
        gameObject.DrawRect.X = 596;
        gameObject.DrawRect.Y = 613;
      }
      gameObject.DrawRect.Width = (int) ((double) (gameObject.DrawRect.Width - 4) * (double) FULLPercentEnd) + 2;
      this.frames.Insert(0, gameObject);
      FoodPointer foodPointer = new FoodPointer(animalfoodentry);
      foodPointer.vLocation.X = (float) gameObject.DrawRect.Width * gameObject.scale;
      this.pointers.Add(foodPointer);
      this.lerpers.Add(new LerpHandler_Float());
      this.lerpers[this.lerpers.Count - 1].SetLerp(true, (float) gameObject.DrawRect.Width, (float) gameObject.DrawRect.Width, 3f);
    }

    public void SetValue(
      float FULLPercentEnd,
      bool IsOn,
      int Index,
      bool HasStock,
      bool LowStock)
    {
      if (LowStock)
        this.frames[Index].DrawRect = new Rectangle(596, 646, 288, 10);
      else if (HasStock)
      {
        this.frames[Index].DrawRect.X = 596;
        this.frames[Index].DrawRect.Y = 613;
      }
      else
      {
        this.frames[Index].DrawRect.X = 596;
        this.frames[Index].DrawRect.Y = 624;
      }
      if ((double) FULLPercentEnd > 1.0)
        FULLPercentEnd = 1f;
      if (IsOn)
      {
        this.On[Index] = 2;
        this.pointers[Index].animalfoodicon.SetAlpha(1f);
      }
      else if (this.On[Index] != 0)
      {
        this.On[Index] = 1;
        if ((double) this.pointers[Index].animalfoodicon.fTargetAlpha != 0.0)
        {
          this.pointers[Index].SetColourDelay(0.1f);
          this.pointers[Index].animalfoodicon.SetAlpha(true, 0.15f, 1f, 0.0f);
        }
      }
      this.lerpers[Index].SetLerp(false, 0.0f, (float) ((int) ((double) (this.DrawRect.Width - 4) * (double) FULLPercentEnd) + 2), 3f, true);
    }

    public void UpdateFoodBarRenderer(float DeltaTime)
    {
      for (int index = 0; index < this.frames.Count; ++index)
      {
        this.lerpers[index].UpdateLerpHandler(DeltaTime);
        if (this.On[index] == 1 && this.lerpers[index].IsComplete())
          this.On[index] = 0;
        this.pointers[index].animalfoodicon.UpdateColours(DeltaTime);
      }
    }

    public void DrawFoodBarRenderer(Vector2 Offset)
    {
      this.IDEAL.DrawFoodPointer(Offset + this.vLocation);
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      for (int index = this.frames.Count - 1; index > -1; --index)
      {
        if (this.On[index] > 0)
        {
          this.frames[index].DrawRect.Width = (int) Math.Ceiling((double) this.lerpers[index].Value);
          this.frames[index].Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
        }
      }
      Offset += this.vLocation;
      for (int index = 0; index < this.pointers.Count; ++index)
      {
        if (this.On[index] > 0)
        {
          this.pointers[index].vLocation.X = this.lerpers[index].Value * this.frames[index].scale;
          this.pointers[index].DrawFoodPointer(Offset);
        }
      }
    }
  }
}
