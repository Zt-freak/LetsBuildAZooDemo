// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.Shelter.ShelterMainFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Shelter;
using TinyZoo.Z_Shelter.SheltderedAnimals;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.Shelter
{
  internal class ShelterMainFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ShelterSummary summaryDesc;
    private ShelteredAnimalManager shelterAnimalRows;

    public ShelterMainFrame(Player player, float BaseScale, BlackMarketTraderInfo blackMarket)
    {
      this.SetUp(player, BaseScale, true);
      this.shelterAnimalRows.PopulateAnimals(blackMarket, player);
    }

    public ShelterMainFrame(Player player, float BaseScale)
    {
      this.SetUp(player, BaseScale);
      this.shelterAnimalRows.PopulateAnimals(player);
    }

    private void SetUp(Player player, float BaseScale, bool isBlackMarket = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = defaultYbuffer;
      this.shelterAnimalRows = new ShelteredAnimalManager(player, BaseScale, true, isBlackMarket);
      Vector2 size = this.shelterAnimalRows.GetSize();
      this.summaryDesc = new ShelterSummary(BaseScale, (float) ((double) size.X / 1024.0 * 0.899999976158142), player, isBlackMarket);
      this.summaryDesc.Location.Y = num1;
      float num2 = num1 + (this.summaryDesc.GetHeight() + defaultYbuffer);
      this.shelterAnimalRows.location.Y = num2;
      float y = num2 + (size.Y + defaultYbuffer);
      this.customerFrame = new CustomerFrame(new Vector2(size.X + defaultXbuffer * 2f, y), CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.summaryDesc.Location.Y += vector2.Y;
      this.shelterAnimalRows.location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateShelterMainFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.shelterAnimalRows.UpdateShelteredAnimalManager(player, DeltaTime, offset);
    }

    public void DrawShelterMainFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.summaryDesc.DrawShelterSummary(offset, spriteBatch);
      this.shelterAnimalRows.DrawShelteredAnimalManager(offset, spriteBatch);
    }
  }
}
