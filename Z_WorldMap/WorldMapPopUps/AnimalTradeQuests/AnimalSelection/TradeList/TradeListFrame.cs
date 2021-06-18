// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.TradeList.TradeListFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals.SellCosts;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.TradeList
{
  internal class TradeListFrame
  {
    public Vector2 location;
    private ZGenericText headerText;
    private CustomerFrame customerFrame;
    private AnimalInFrameGrid animalsInFrameGrid;
    private TradeStackWithNumber TradeStackWithNumber;
    private TextButton tradeButton;
    private List<AnimalRenderDescriptor> RefanimalsNeeded_forQuest;
    private AnimalSelectionUIType UItype;
    private float BaseScale;
    private Vector2 temp_GridLocation;
    private float Xbuffer;
    private float Ybuffer;
    private int numberPerRow;
    private int maxAnimalsBeforeStacking;
    private float raw_FrameSize;

    public List<PrisonerInfo> tradeList { get; private set; }

    public bool ReadyForTrade { get; private set; }

    public TradeListFrame(
      List<AnimalRenderDescriptor> animalsNeeded_forQuest,
      float _BaseScale,
      Vector2 frameSize,
      AnimalSelectionUIType _UItype)
    {
      this.RefanimalsNeeded_forQuest = animalsNeeded_forQuest;
      this.UItype = _UItype;
      this.BaseScale = _BaseScale;
      this.tradeList = new List<PrisonerInfo>();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.Xbuffer = uiScaleHelper.GetDefaultXBuffer();
      this.Ybuffer = uiScaleHelper.GetDefaultYBuffer();
      this.customerFrame = new CustomerFrame(frameSize, CustomerFrameColors.DarkBrown, this.BaseScale);
      Vector2 vector2_1 = -this.customerFrame.VSCale * 0.5f;
      float xbuffer = this.Xbuffer;
      float ybuffer = this.Ybuffer;
      this.headerText = new ZGenericText(this.BaseScale, false, _UseOnePointFiveFont: true);
      this.SetHeaderText();
      this.headerText.vLocation = new Vector2(xbuffer, ybuffer);
      float y = ybuffer + this.headerText.GetSize().Y + this.Ybuffer;
      this.raw_FrameSize = 25f;
      Vector2 vector2_2 = uiScaleHelper.ScaleVector2(this.raw_FrameSize * Vector2.One);
      this.numberPerRow = (int) Math.Floor(((double) frameSize.X - (double) xbuffer) / ((double) vector2_2.X + (double) this.Xbuffer));
      this.maxAnimalsBeforeStacking = this.numberPerRow * (int) Math.Floor(((double) frameSize.Y - (double) y) / ((double) vector2_2.Y + (double) this.Ybuffer)) - 3;
      this.temp_GridLocation = new Vector2(xbuffer, y);
      this.temp_GridLocation += vector2_1;
      if (this.UItype == AnimalSelectionUIType.TradeQuest)
        this.CreateAnimalGrid(animalsNeeded_forQuest);
      string TextToDraw = "Trade";
      if (this.UItype == AnimalSelectionUIType.BlackMarket)
        TextToDraw = "Sell";
      else if (this.UItype == AnimalSelectionUIType.Donation)
        TextToDraw = "Donate";
      this.tradeButton = new TextButton(this.BaseScale, TextToDraw, 40f);
      this.tradeButton.vLocation.X = frameSize.X - this.tradeButton.GetSize_True().X * 0.5f - this.Xbuffer;
      this.tradeButton.vLocation.Y = frameSize.Y - this.tradeButton.GetSize_True().Y * 0.5f - this.Ybuffer;
      this.RefreshButton();
      ZGenericText headerText = this.headerText;
      headerText.vLocation = headerText.vLocation + vector2_1;
      TextButton tradeButton = this.tradeButton;
      tradeButton.vLocation = tradeButton.vLocation + vector2_1;
    }

    private void CreateAnimalGrid(List<AnimalRenderDescriptor> animalsToDisplay)
    {
      if (animalsToDisplay.Count > this.maxAnimalsBeforeStacking)
      {
        if (this.UItype == AnimalSelectionUIType.BlackMarket || this.UItype == AnimalSelectionUIType.Donation)
        {
          this.animalsInFrameGrid = new AnimalInFrameGrid(this.BaseScale, this.numberPerRow, this.Xbuffer, this.Ybuffer, animalsToDisplay.ToList<AnimalRenderDescriptor>(), this.maxAnimalsBeforeStacking, UseNumberFrameWhenMaxFrames_NotButton: true, rawFrameSize: this.raw_FrameSize);
          this.animalsInFrameGrid.location += this.temp_GridLocation;
        }
        else
        {
          this.TradeStackWithNumber = new TradeStackWithNumber(this.BaseScale, animalsToDisplay);
          this.TradeStackWithNumber.location += this.temp_GridLocation;
        }
      }
      else
      {
        this.animalsInFrameGrid = new AnimalInFrameGrid(this.BaseScale, this.numberPerRow, this.Xbuffer, this.Ybuffer, animalsToDisplay.ToList<AnimalRenderDescriptor>(), rawFrameSize: this.raw_FrameSize);
        this.animalsInFrameGrid.location += this.temp_GridLocation;
      }
    }

    private void SetHeaderText()
    {
      if (this.UItype == AnimalSelectionUIType.BlackMarket)
        this.headerText.textToWrite = string.Format("Offered For Sale: {0}, Total: ${1}", (object) this.tradeList.Count, (object) this.GetRecalculatedTotalCost());
      else if (this.UItype == AnimalSelectionUIType.Donation)
        this.headerText.textToWrite = string.Format("Offered for Donation: {0}", (object) this.tradeList.Count);
      else
        this.headerText.textToWrite = string.Format("Required For Trade: {0}/{1}", (object) this.tradeList.Count, (object) this.RefanimalsNeeded_forQuest.Count);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void RefreshButton()
    {
      if (!this.ReadyForTrade)
        this.tradeButton.SetButtonColour(BTNColour.Grey);
      else
        this.tradeButton.SetButtonColour(BTNColour.Green);
    }

    public bool AddOrRemoveThisFromTradeList(PrisonerInfo animal)
    {
      bool flag;
      if (this.tradeList.Contains(animal))
      {
        this.tradeList.Remove(animal);
        flag = false;
      }
      else
      {
        this.tradeList.Add(animal);
        flag = true;
      }
      if (this.UItype == AnimalSelectionUIType.BlackMarket)
      {
        this.ReadyForTrade = this.tradeList.Count > 0;
        List<AnimalRenderDescriptor> animalsToDisplay = new List<AnimalRenderDescriptor>();
        foreach (PrisonerInfo trade in this.tradeList)
          animalsToDisplay.Add(new AnimalRenderDescriptor(trade.intakeperson.animaltype, trade.intakeperson.CLIndex, trade.intakeperson.HeadType, trade.intakeperson.HeadVariant, _IsFemale: trade.intakeperson.IsAGirl));
        this.CreateAnimalGrid(animalsToDisplay);
      }
      else if (this.UItype == AnimalSelectionUIType.Donation)
      {
        this.ReadyForTrade = this.tradeList.Count > 0;
        List<AnimalRenderDescriptor> animalsToDisplay = new List<AnimalRenderDescriptor>();
        foreach (PrisonerInfo trade in this.tradeList)
          animalsToDisplay.Add(new AnimalRenderDescriptor(trade.intakeperson.animaltype, trade.intakeperson.CLIndex, trade.intakeperson.HeadType, trade.intakeperson.HeadVariant, _IsFemale: trade.intakeperson.IsAGirl));
        this.CreateAnimalGrid(animalsToDisplay);
      }
      else if (this.UItype == AnimalSelectionUIType.TradeQuest)
        this.ReadyForTrade = this.tradeList.Count == this.RefanimalsNeeded_forQuest.Count;
      this.SetHeaderText();
      this.RefreshButton();
      return flag;
    }

    private int GetRecalculatedTotalCost()
    {
      int num = 0;
      for (int index = 0; index < this.tradeList.Count; ++index)
        num += AnimalSellCostCalculator.GetSellCostOfPlayerAnimal(this.tradeList[index]);
      return num;
    }

    public bool UpdateTradeListFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.ReadyForTrade && this.tradeButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawTradeListFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.animalsInFrameGrid != null)
        this.animalsInFrameGrid.DrawAnimalInFrameGrid(offset, spriteBatch);
      if (this.TradeStackWithNumber != null)
        this.TradeStackWithNumber.DrawTradeStackWithNumber(offset, spriteBatch);
      this.headerText.DrawZGenericText(offset, spriteBatch);
      this.tradeButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
