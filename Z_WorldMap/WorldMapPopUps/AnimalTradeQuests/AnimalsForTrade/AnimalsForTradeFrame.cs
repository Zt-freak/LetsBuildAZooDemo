// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalsForTrade.AnimalsForTradeFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Quests;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalsForTrade
{
  internal class AnimalsForTradeFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalTradeEquation trade;
    private TextButton viewTradeButton;
    private ActiveIcon tickIcon;
    private ZGenericText completedText;
    private List<AnimalRenderDescriptor> refAnimalsNeeded;
    private bool AlQuestsDone;

    public AnimalsForTradeFrame(
      QuestPack quest,
      Player player,
      float BaseScale,
      float forcedWidth = 250f,
      bool isCompleteView = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float y = defaultYbuffer;
      float x = defaultXbuffer;
      if (quest == null)
      {
        this.AlQuestsDone = true;
      }
      else
      {
        AnimalType getThisAnimal = quest.GetThisAnimal;
        bool _IsUnlocked = player.Stats.GetTotalOfThisVariantFound(quest.trades_ListOnlyOne[0].animal, quest.trades_ListOnlyOne[0].VariantIndex) > 0;
        List<PrisonerInfo> beIncludedInTrade = AnimalSelectionManager.GetListOfPlayersAnimalsThatCanBeIncludedInTrade(quest, player);
        int length = 0;
        if (!isCompleteView)
          length = quest.trades_ListOnlyOne[0].Total.SmartGetValue(true);
        AnimalRenderDescriptor[] renderDescriptorArray = new AnimalRenderDescriptor[length];
        for (int index = 0; index < length; ++index)
        {
          bool _IsAvailable = beIncludedInTrade.Count > index;
          AnimalRenderDescriptor renderDescriptor = new AnimalRenderDescriptor(quest.trades_ListOnlyOne[0].animal, quest.trades_ListOnlyOne[0].VariantIndex, _IsUnlocked: _IsUnlocked, _IsAvailable: _IsAvailable);
          renderDescriptorArray[index] = renderDescriptor;
        }
        int num1 = beIncludedInTrade.Count == length ? 1 : 0;
        this.refAnimalsNeeded = ((IEnumerable<AnimalRenderDescriptor>) renderDescriptorArray).ToList<AnimalRenderDescriptor>();
        this.trade = new AnimalTradeEquation(BaseScale, getThisAnimal, renderDescriptorArray);
        this.trade.location = new Vector2(x, y);
        this.trade.location.X -= this.trade.GetSize().X * 0.5f;
        float num2 = y + (this.trade.GetSize().Y + defaultYbuffer);
        if (isCompleteView)
        {
          this.completedText = new ZGenericText(SEngine.Localization.Localization.GetText(959), BaseScale, _UseOnePointFiveFont: true);
          this.completedText.vLocation.Y = num2 + this.completedText.GetSize().Y * 0.5f;
          num2 += this.completedText.GetSize().Y + defaultYbuffer;
        }
        string text = SEngine.Localization.Localization.GetText(960);
        float Length = 60f;
        if (isCompleteView)
        {
          text = SEngine.Localization.Localization.GetText(961);
          Length = 70f;
        }
        else if (quest.trades_ListOnlyOne[0].Total.GetUnvallidatedValue() == 0)
        {
          text = SEngine.Localization.Localization.GetText(953);
          Length = 40f;
        }
        this.viewTradeButton = new TextButton(BaseScale, text, Length);
        this.viewTradeButton.vLocation.Y = num2 + this.viewTradeButton.GetSize_True().Y * 0.5f;
        float num3 = num2 + (this.viewTradeButton.GetSize_True().Y + defaultYbuffer);
        if (num1 != 0 && !isCompleteView)
        {
          this.tickIcon = new ActiveIcon(true, BaseScale);
          this.tickIcon.vLocation.X = this.viewTradeButton.GetSize_True().X * 0.5f + defaultXbuffer;
          this.tickIcon.vLocation.Y = this.viewTradeButton.vLocation.Y;
          this.tickIcon.vLocation.X += this.tickIcon.GetSize().X * 0.5f;
        }
        this.customerFrame = new CustomerFrame(new Vector2(forcedWidth, uiScaleHelper.ScaleY(140f)), CustomerFrameColors.DarkBrown, BaseScale);
        Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
        float num4 = this.customerFrame.VSCale.Y - this.trade.GetSize().Y;
        vector2.Y += num4 * 0.25f;
        float num5 = num4 * 0.05f;
        this.trade.location.Y += vector2.Y - num5;
        if (this.viewTradeButton != null)
          this.viewTradeButton.vLocation.Y += vector2.Y + num5;
        if (this.tickIcon != null)
          this.tickIcon.vLocation.Y += vector2.Y + num5;
        if (this.completedText == null)
          return;
        this.completedText.vLocation.Y += vector2.Y + num5;
      }
    }

    public Vector2 GetSize() => this.AlQuestsDone ? Vector2.Zero : this.customerFrame.VSCale;

    public List<AnimalRenderDescriptor> GetListOfAnimalsNeededForRendering() => this.refAnimalsNeeded;

    public bool UpdateAnimalsForTradeFrame(Player player, float DeltaTime, Vector2 offset)
    {
      if (this.AlQuestsDone)
        return false;
      offset += this.location;
      this.trade.UpdateAnimalTradeEquation(DeltaTime, offset);
      return this.viewTradeButton != null && this.viewTradeButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawAnimalsForTradeFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      if (this.AlQuestsDone)
        return;
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.trade.DrawAnimalTradeEquation(offset, spriteBatch);
      if (this.viewTradeButton != null)
        this.viewTradeButton.DrawTextButton(offset, 1f, spriteBatch);
      if (this.tickIcon != null)
        this.tickIcon.DrawActiveIcon(spriteBatch, offset);
      if (this.completedText == null)
        return;
      this.completedText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
