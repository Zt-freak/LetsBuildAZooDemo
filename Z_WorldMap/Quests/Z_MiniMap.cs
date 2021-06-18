// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.Z_MiniMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Quests;

namespace TinyZoo.Z_WorldMap.Quests
{
  internal class Z_MiniMap
  {
    private GameObject ZooStripItem;
    private List<EnemyRenderer> ZooDude;
    private bool Claimed;
    public Vector2 location;

    public Z_MiniMap(QuestPack ref_questpack, CityName city, float BaseScale = -1f)
    {
      this.ZooStripItem = new GameObject();
      switch (city)
      {
        case CityName.Sydney:
          this.ZooStripItem.DrawRect = new Rectangle(0, 258, 464, 122);
          break;
        case CityName.London:
          this.ZooStripItem.DrawRect = new Rectangle(0, 381, 464, 122);
          break;
        case CityName.Africa:
          this.ZooStripItem.DrawRect = new Rectangle(2567, 413, 464, 122);
          break;
        case CityName.Bangladesh:
          this.ZooStripItem.DrawRect = new Rectangle(2567, 659, 464, 122);
          break;
        case CityName.Singapore:
          this.ZooStripItem.DrawRect = new Rectangle(2102, 290, 464, 122);
          break;
        case CityName.Tokyo:
          this.ZooStripItem.DrawRect = new Rectangle(0, 504, 464, 122);
          break;
        case CityName.Toronto:
          this.ZooStripItem.DrawRect = new Rectangle(0, 996, 464, 122);
          break;
        case CityName.Oakland:
          this.ZooStripItem.DrawRect = new Rectangle(0, 750, 464, 122);
          break;
        case CityName.Utah:
          this.ZooStripItem.DrawRect = new Rectangle(0, 627, 464, 122);
          break;
        case CityName.Moscow:
          this.ZooStripItem.DrawRect = new Rectangle(0, 873, 464, 122);
          break;
        case CityName.Surabaya:
          this.ZooStripItem.DrawRect = new Rectangle(2567, 290, 464, 122);
          break;
        case CityName.Berlin:
          this.ZooStripItem.DrawRect = new Rectangle(2567, 536, 464, 122);
          break;
        case CityName.Cuba:
          this.ZooStripItem.DrawRect = new Rectangle(2102, 659, 464, 122);
          break;
        case CityName.Beijing:
          this.ZooStripItem.DrawRect = new Rectangle(2102, 536, 464, 122);
          break;
        case CityName.Brazil:
          this.ZooStripItem.DrawRect = new Rectangle(2102, 413, 464, 122);
          break;
        default:
          this.ZooStripItem.DrawRect = new Rectangle(0, 258, 464, 122);
          break;
      }
      this.ZooStripItem.SetDrawOriginToCentre();
      this.ZooDude = new List<EnemyRenderer>();
      this.ZooDude.Add(new EnemyRenderer(EnemyData.GetZooKeeper(city), 0));
      if (ref_questpack != null)
      {
        this.ZooDude.Add(new EnemyRenderer(ref_questpack.GetThisAnimal, 0));
        this.ZooDude.Add(new EnemyRenderer(ref_questpack.GetThisAnimal, 0));
      }
      this.ZooDude[0].vLocation.X = -400f;
      if (this.ZooDude.Count > 1)
      {
        this.ZooDude[1].vLocation.X = -70f;
        this.ZooDude[1].vLocation.Y = 30f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.ZooDude[2].vLocation.X = -100f;
      }
      for (int index = 0; index < this.ZooDude.Count; ++index)
      {
        if ((double) BaseScale != -1.0)
        {
          this.ZooDude[index].scale = BaseScale;
          this.ZooDude[index].vLocation.X *= BaseScale * 0.5f;
          this.ZooDude[index].vLocation.Y *= (float) ((double) BaseScale * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5);
        }
        else
          this.ZooDude[index].scale = 2f;
      }
      if ((double) BaseScale != -1.0)
      {
        this.ZooStripItem.scale = BaseScale;
      }
      else
      {
        this.ZooStripItem.scale = 2f;
        this.ZooStripItem.vLocation = new Vector2(512f, 390f);
      }
    }

    public void UpdateZ_MiniMap(float DeltaTime)
    {
      for (int index = 0; index < this.ZooDude.Count; ++index)
        this.ZooDude[index].UpdateAnimalRenderer(DeltaTime);
    }

    public void SetClaim() => this.Claimed = true;

    public Vector2 GetSize() => new Vector2((float) this.ZooStripItem.DrawRect.Width, (float) this.ZooStripItem.DrawRect.Height) * this.ZooStripItem.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawZ_MiniMap(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.ZooStripItem.Draw(spriteBatch, AssetContainer.EnvironmentSheet, offset);
      for (int index = 0; index < this.ZooDude.Count; ++index)
      {
        if (!this.Claimed || index != 1 && index != 2)
          this.ZooDude[index].ScreenSpaceDrawEnemyRenderer(offset, spriteBatch);
      }
    }

    public void DrawZ_MiniMap(Vector2 Offset, bool Claimed)
    {
      Offset += this.location;
      this.ZooStripItem.scale = 2f;
      if (Claimed)
        this.ZooStripItem.vLocation = new Vector2(512f, 500f);
      else
        this.ZooStripItem.vLocation = new Vector2(512f, 390f);
      this.ZooStripItem.Draw(AssetContainer.pointspritebatch03, AssetContainer.EnvironmentSheet);
      for (int index = 0; index < this.ZooDude.Count; ++index)
      {
        if (!Claimed || index != 1 && index != 2)
          this.ZooDude[index].ScreenSpaceDrawEnemyRenderer(this.ZooStripItem.vLocation);
      }
    }
  }
}
