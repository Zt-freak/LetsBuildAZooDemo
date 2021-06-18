// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Shelter.SheltderedAnimals.ShelteredAnimalManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_BalanceSystems.Animals.SellCosts;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Shelter.SheltderedAnimals
{
  internal class ShelteredAnimalManager
  {
    public Vector2 location;
    private List<ShelteredAnimalRow> shelteredanimals;
    private int rowsPerPage;

    public ShelteredAnimalManager(
      Player player,
      float _BaseScale = -1f,
      bool PositionFromZero = false,
      bool isBlackMarket = false)
    {
      float defaultYbuffer = new UIScaleHelper(_BaseScale).GetDefaultYBuffer();
      this.rowsPerPage = 5;
      if (isBlackMarket)
        this.rowsPerPage = 3;
      if ((double) _BaseScale == -1.0)
        _BaseScale = Z_GameFlags.GetBaseScaleForUI();
      float _BaseScale1 = _BaseScale;
      Vector2 vector2 = Vector2.Zero;
      if (!PositionFromZero)
        vector2 = new Vector2(512f, 200f);
      this.shelteredanimals = new List<ShelteredAnimalRow>();
      for (int index = 0; index < this.rowsPerPage; ++index)
      {
        this.shelteredanimals.Add(new ShelteredAnimalRow(player, _BaseScale1));
        Vector2 size = this.shelteredanimals[index].GetSize();
        this.shelteredanimals[index].Location = vector2;
        this.shelteredanimals[index].Location.Y += size.Y * 0.5f;
        this.shelteredanimals[index].Location.Y += (float) index * (defaultYbuffer + size.Y);
      }
    }

    public void PopulateAnimals(BlackMarketTraderInfo blackMarketTrader, Player player)
    {
      AnimalRenderDescriptor _animal = new AnimalRenderDescriptor(blackMarketTrader.Body, blackMarketTrader.BodyVariant, blackMarketTrader.Head, blackMarketTrader.HeadVariant, _IsFemale: (Game1.Rnd.Next(0, 2) == 0));
      int costfromBlackMarket = AnimalSellCostCalculator.GetBuyCostfromBlackMarket(blackMarketTrader.Body, blackMarketTrader.BodyVariant, blackMarketTrader.Head, blackMarketTrader.HeadVariant);
      bool purchased = blackMarketTrader.Purchased;
      this.shelteredanimals[0].PopulateAnimalRow(player, _animal, true, costfromBlackMarket, purchased);
    }

    public void PopulateAnimals(Player player)
    {
      for (int index = 0; index < player.shelterstocks.shelteredanimal.Count; ++index)
        this.shelteredanimals[index].PopulateAnimalRow(player, player.shelterstocks.shelteredanimal[index]);
    }

    public Vector2 GetSize()
    {
      Vector2 size = this.shelteredanimals[0].GetSize();
      return new Vector2(size.X, size.Y + this.shelteredanimals[this.shelteredanimals.Count - 1].Location.Y - this.shelteredanimals[0].Location.Y);
    }

    public bool UpdateShelteredAnimalManager(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.shelteredanimals.Count; ++index)
      {
        if (this.shelteredanimals[index].UpdateShelteredAnimalRow(offset, player, DeltaTime))
          return true;
      }
      return false;
    }

    public void DrawShelteredAnimalManager(Vector2 offset) => this.DrawShelteredAnimalManager(offset, AssetContainer.pointspritebatch03);

    public void DrawShelteredAnimalManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.shelteredanimals.Count; ++index)
        this.shelteredanimals[index].DrawShelteredAnimalRow(offset, spriteBatch);
    }
  }
}
