// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet.CurrentDietDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet
{
  internal class CurrentDietDisplay
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText daysLeft;
    private AnimalDietIcons foodIcons;
    private WeightDisplay weightDisplay;
    private LittleSummaryButton infoButton;
    private SimpleTextHandler infoDesc;
    private bool IsViewingInfo;

    public CurrentDietDisplay(
      PrisonerInfo prisonerInfo,
      Player player,
      float BaseScale,
      float width)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(prisonerInfo.intakeperson.animaltype);
      int Cell_UID;
      player.prisonlayout.cellblockcontainer.GetThisAnimal(prisonerInfo.intakeperson.UID, out Cell_UID);
      FoodSet thisSet = player.prisonlayout.GetThisCellBlock(Cell_UID).prisonercontainer.FoodForAnimals.GetThisSet(prisonerInfo.intakeperson.animaltype);
      player.prisonlayout.SetUpAllStock(player);
      int num = -1;
      for (int index = 0; index < foodCollection.animalfoodentry.Count; ++index)
      {
        if ((double) thisSet.FoodUnitsPerDay[index] > 0.0)
        {
          int days = player.livestats.stocktimes.GetDays(foodCollection.animalfoodentry[index].foodtype);
          if (num == -1 || num < days)
            num = days;
        }
      }
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Diet");
      this.infoButton = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
      this.foodIcons = new AnimalDietIcons(prisonerInfo.intakeperson.animaltype, BaseScale, player, false, prisonerInfo);
      this.daysLeft = new ZGenericText("Days of Stock Left: " + (object) num, BaseScale, false);
      this.weightDisplay = new WeightDisplay(prisonerInfo, BaseScale);
      this.infoDesc = new SimpleTextHandler("You can change your animal's diet by going to the store room.", width - defaultBuffer.X, _Scale: BaseScale, AutoComplete: true);
      this.infoDesc.SetAllColours(ColourData.Z_Cream);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      Vector2 _vScale = zero + defaultBuffer;
      this.infoDesc.Location = _vScale;
      this.foodIcons.location = _vScale;
      this.foodIcons.location += this.foodIcons.GetSize() * 0.5f;
      this.foodIcons.location.X -= defaultBuffer.X * 0.5f;
      _vScale.Y += this.foodIcons.GetSize().Y;
      _vScale.Y += defaultBuffer.Y * 0.5f;
      this.daysLeft.vLocation = _vScale;
      _vScale.Y += this.daysLeft.GetSize().Y;
      _vScale.Y += defaultBuffer.Y * 0.5f;
      this.weightDisplay.location = _vScale;
      _vScale.Y += this.weightDisplay.GetSize().Y;
      this.infoButton.vLocation.Y = this.customerFrame.GetMiniHeadingHeight(false) * 0.5f;
      this.infoButton.vLocation.X = this.customerFrame.GetMiniHeadingSize().X + defaultBuffer.X * 0.5f;
      this.infoButton.vLocation.Y += defaultBuffer.Y;
      this.infoButton.vLocation.X += this.infoButton.GetSize().X * 0.5f;
      _vScale.X = width;
      this.customerFrame.Resize(_vScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      LittleSummaryButton infoButton = this.infoButton;
      infoButton.vLocation = infoButton.vLocation + vector2;
      this.foodIcons.location += vector2;
      this.weightDisplay.location += vector2;
      this.infoDesc.Location += vector2;
      ZGenericText daysLeft = this.daysLeft;
      daysLeft.vLocation = daysLeft.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateCurrentDietDisplay(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.infoButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return;
      this.IsViewingInfo = !this.IsViewingInfo;
    }

    public void DrawCurrentDietDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.infoButton.DrawLittleSummaryButton(offset, spriteBatch);
      if (this.IsViewingInfo)
      {
        this.infoDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      }
      else
      {
        this.daysLeft.DrawZGenericText(offset, spriteBatch);
        this.foodIcons.DrawAnimalDietIcons(offset, spriteBatch);
        this.weightDisplay.DrawWeightDisplay(offset, spriteBatch);
      }
    }
  }
}
