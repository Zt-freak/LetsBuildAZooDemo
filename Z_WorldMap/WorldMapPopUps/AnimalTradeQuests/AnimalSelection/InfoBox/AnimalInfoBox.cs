// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.InfoBox.AnimalInfoBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine.Text;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.InfoBox
{
  internal class AnimalInfoBox
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private BreedMeIcon animalInFrame;
    private PrisonerInfo refAnimal;
    private ZGenericText name;
    private StringScroller nameScroller;
    private SimpleTextHandler detailsPara;
    private TextButton selectForTradeButton;
    private LongevityBar longevityBar;
    private ZGenericText PriceText_BlackMarket;
    private SimpleTextHandler nothingSelectedText;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private float Xbuffer;
    private float Ybuffer;
    private bool DisableButton;

    public AnimalInfoBox(float _BaseScale, Vector2 forcedSize, AnimalSelectionUIType UIType)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.Xbuffer = this.scaleHelper.GetDefaultXBuffer();
      this.Ybuffer = this.scaleHelper.GetDefaultYBuffer();
      this.customerFrame = new CustomerFrame(forcedSize, CustomerFrameColors.DarkBrown, this.BaseScale);
      string TextToWrite = "Select an animal to view its information and trade.";
      if (UIType == AnimalSelectionUIType.Donation)
        TextToWrite = "Select an animal to view its information and donate.";
      this.nothingSelectedText = new SimpleTextHandler(TextToWrite, true, (float) ((double) forcedSize.X / 1024.0 * 0.899999976158142), this.BaseScale, AutoComplete: true);
      this.nothingSelectedText.SetAllColours(ColourData.Z_Cream);
    }

    public void SetInfoForThisAnimal(
      PrisonerInfo animal,
      bool IsAlreadyInTradeList = false,
      bool HaveEnoughForTrade_BlockSelectingMore = false,
      int SellPrice_BlackMarket = -1)
    {
      this.refAnimal = animal;
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      float x = vector2.X + this.Xbuffer;
      float num = vector2.Y + this.Ybuffer;
      this.animalInFrame = new BreedMeIcon(new AnimalRenderDescriptor(animal.GetAnimalPainted(), animal.intakeperson.CLIndex, animal.intakeperson.HeadType, animal.intakeperson.HeadVariant, _IsFemale: animal.intakeperson.IsAGirl), this.BaseScale, true);
      Vector2 size = this.animalInFrame.GetSize();
      this.animalInFrame.Location = new Vector2(x + this.animalInFrame.GetOffsetToDrawFromLeft(), num + size.Y * 0.5f);
      float y1 = num + (size.Y + this.Ybuffer);
      this.name = new ZGenericText(animal.intakeperson.Name, this.BaseScale, false, _UseOnePointFiveFont: true);
      this.name.vLocation = new Vector2(this.animalInFrame.Location.X + (size.X - this.animalInFrame.GetOffsetToDrawFromLeft()) + this.Xbuffer, this.animalInFrame.Location.Y);
      this.name.vLocation.Y -= this.name.GetSize().Y * 0.5f;
      this.nameScroller = new StringScroller((this.customerFrame.VSCale.X - (this.name.vLocation.X - vector2.X) - this.Xbuffer) / this.BaseScale, animal.intakeperson.Name, AssetContainer.SpringFontX1AndHalf);
      string TextToWrite = "Age: " + (object) animal.Age + "~Happiness: " + (object) Math.Round((double) animal.animalhappyness.GetHappiness(), 1) + "~Health: " + (object) animal.GetHealthWellBeing();
      if (animal.intakeperson.IsAGirl)
      {
        string str1 = TextToWrite + "~Fertile: ";
        string str2 = (animal.IsFertile ? str1 + "Yes" : str1 + "No") + "~Pregnant: ";
        TextToWrite = !animal.IsPregnant ? str2 + "No" : str2 + "Yes";
      }
      this.detailsPara = new SimpleTextHandler(TextToWrite, false, (float) ((double) this.customerFrame.VSCale.X / 1024.0 * 0.899999976158142), this.BaseScale, false, true);
      this.detailsPara.SetAllColours(ColourData.Z_Cream);
      this.detailsPara.Location = new Vector2(x, y1);
      float y2 = y1 + (this.detailsPara.GetHeightOfParagraph() + this.Ybuffer);
      this.longevityBar = new LongevityBar(animal, 1f, this.BaseScale);
      this.longevityBar.Location = new Vector2(0.0f, y2);
      this.longevityBar.Location.X -= this.longevityBar.GetSize().X * 0.5f;
      this.selectForTradeButton = new TextButton(this.BaseScale, "BLAH", 50f);
      this.selectForTradeButton.vLocation.Y = (float) (-(double) vector2.Y - (double) this.selectForTradeButton.GetSize_True().Y * 0.5) - this.Ybuffer;
      if (SellPrice_BlackMarket != -1)
      {
        this.PriceText_BlackMarket = new ZGenericText("$" + (object) SellPrice_BlackMarket, this.BaseScale, _UseOnePointFiveFont: true);
        this.PriceText_BlackMarket.vLocation.Y = this.selectForTradeButton.vLocation.Y - this.selectForTradeButton.GetSize_True().Y * 0.5f - this.Ybuffer;
        this.PriceText_BlackMarket.vLocation.Y -= this.PriceText_BlackMarket.GetSize().Y * 0.5f;
      }
      this.SetSelection(IsAlreadyInTradeList, HaveEnoughForTrade_BlockSelectingMore);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public PrisonerInfo UpdateAnimalInfoBox(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      if (this.name != null)
      {
        this.nameScroller.UpdateStringScroller(DeltaTime);
        this.name.textToWrite = this.nameScroller.GetString();
      }
      return this.refAnimal != null && !this.DisableButton && this.selectForTradeButton.UpdateTextButton(player, offset, DeltaTime) ? this.refAnimal : (PrisonerInfo) null;
    }

    public void SetSelection(bool selectThis, bool IsFull_BlockSelectingMore)
    {
      if (!selectThis)
      {
        this.selectForTradeButton.SetButtonColour(BTNColour.Green);
        this.selectForTradeButton.SetText("Select");
      }
      else if (selectThis)
      {
        this.selectForTradeButton.SetButtonColour(BTNColour.Blue);
        this.selectForTradeButton.SetText("Selected");
      }
      this.DisableButton = IsFull_BlockSelectingMore && !selectThis;
      if (!this.DisableButton)
        return;
      this.selectForTradeButton.SetButtonColour(BTNColour.Grey);
      this.selectForTradeButton.SetText("Select");
    }

    public void DrawAnimalInfoBox(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.refAnimal != null)
      {
        this.animalInFrame.DrawBreedMeIcon(offset, spriteBatch);
        this.name.DrawZGenericText(offset, spriteBatch);
        this.detailsPara.DrawSimpleTextHandler(offset, 1f, spriteBatch);
        this.selectForTradeButton.DrawTextButton(offset, 1f, spriteBatch);
        this.longevityBar.DrawLongevityBar(offset, spriteBatch);
        if (this.PriceText_BlackMarket == null)
          return;
        this.PriceText_BlackMarket.DrawZGenericText(offset, spriteBatch);
      }
      else
        this.nothingSelectedText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
