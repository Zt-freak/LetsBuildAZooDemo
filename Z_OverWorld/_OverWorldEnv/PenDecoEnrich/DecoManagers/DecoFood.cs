// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.DecoManagers.DecoFood
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_FoodData;
using TinyZoo.Z_Particles;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.DecoManagers
{
  internal class DecoFood : GameObject
  {
    private AFoodIconInfo foodinfo;
    private float Pause;
    private AnimalFoodType animalfoodtype;
    private List<Z_Particle> Z_ParticleList;
    private int Size;
    private GameObject OverFader;

    public DecoFood(AnimalFoodType _animalfoodtype)
    {
      this.Size = 2;
      this.animalfoodtype = _animalfoodtype;
      this.foodinfo = AnimalFoodIconData.GetAnimalFoodIconData(this.animalfoodtype);
      this.DrawRect = this.foodinfo.rectangles[this.Size];
      this.SetDrawOriginToCentre();
      this.Pause = MathStuff.getRandomFloat(4f, 8f);
      this.bActive = true;
      this.SetAlpha(false, 0.2f, 0.0f, 1f);
    }

    public bool UpdateDecoFood(float DeltaTime, float AnimalsOnThisTileEating)
    {
      if (this.bActive)
      {
        this.UpdateColours(DeltaTime);
        if ((double) AnimalsOnThisTileEating > 4.0)
          AnimalsOnThisTileEating = 4f;
        this.Pause -= AnimalsOnThisTileEating * DeltaTime;
        if ((double) this.Pause < 0.0)
        {
          this.OverFader = new GameObject((GameObject) this);
          this.OverFader.SetAlpha(false, 1f, 1f, 0.0f);
          this.Pause = MathStuff.getRandomFloat(4f, 8f);
          float num = (float) this.DrawRect.Height - this.DrawOrigin.Y;
          this.Z_ParticleList = OverWorldManager.particlemanager.SpawnParticle(this.vLocation, this.vLocation.Y + num * 0.1f, this.vLocation.Y + num * 1.2f, 10, ParticleType.Food);
          for (int index = 0; index < this.Z_ParticleList.Count; ++index)
            this.Z_ParticleList[index].SetAllColours(AnimalFoodIconData.GetAnimalFoodColour(this.animalfoodtype));
          --this.Size;
          if (this.Size > -1)
            this.DrawRect = this.foodinfo.rectangles[this.Size];
          else
            this.bActive = false;
        }
      }
      if (this.OverFader != null)
      {
        this.OverFader.UpdateColours(DeltaTime);
        if (!this.bActive && (double) this.OverFader.fAlpha == 0.0)
        {
          for (int index = 0; index < this.Z_ParticleList.Count; ++index)
          {
            if (this.Z_ParticleList[index].bActive)
              return false;
          }
          return true;
        }
      }
      return false;
    }

    public void DrawDeco()
    {
      if (this.bActive)
      {
        this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
        this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.AnimalSheet);
      }
      if (this.OverFader != null)
        this.OverFader.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.AnimalSheet);
      if (this.Z_ParticleList == null)
        return;
      for (int index = 0; index < this.Z_ParticleList.Count; ++index)
        this.Z_ParticleList[index].DrawParticle(AssetContainer.pointspritebatch03);
    }
  }
}
