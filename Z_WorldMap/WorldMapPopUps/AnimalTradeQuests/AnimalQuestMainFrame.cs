// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalQuestMainFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Quests;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_WorldMap.Quests;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalsForTrade;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests
{
  internal class AnimalQuestMainFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private CharacterTextBox characterTextBox;
    private Z_MiniMap miniMap;
    private AnimalsForTradeFrame animalsForTradeFrame;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private float Xbuffer;
    private float Ybuffer;
    private QuestPack refQuest;
    private CityName city;

    public bool isShowingisAfterCompletionView { get; private set; }

    public AnimalQuestMainFrame(CityName _city, Player player, float _BaseScale)
    {
      this.city = _city;
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.Ybuffer = this.scaleHelper.GetDefaultYBuffer();
      this.Xbuffer = this.scaleHelper.GetDefaultXBuffer();
      this.refQuest = player.zquests.GetActiveQuestFromThisCity(this.city);
      QuestPack refQuest = this.refQuest;
      this.Construct(player);
    }

    public void Construct(Player player, bool isAfterCompletionView = false)
    {
      this.isShowingisAfterCompletionView = isAfterCompletionView;
      float ybuffer = this.Ybuffer;
      AnimalType TalkingHead;
      this.characterTextBox = new CharacterTextBox(QuestStories.GetStory(this.refQuest, this.city, player, out string _, out TalkingHead, isAfterCompletionView), TalkingHead, this.BaseScale, OverrideWidth_Scaled: this.scaleHelper.ScaleX(464f), AutoCompleteParagraph: (!isAfterCompletionView));
      this.characterTextBox.Location.Y = ybuffer;
      this.characterTextBox.Location.Y += this.characterTextBox.GetSize().Y * 0.5f;
      float num1 = ybuffer + (this.characterTextBox.GetSize().Y + this.Ybuffer);
      this.miniMap = new Z_MiniMap(this.refQuest, this.city, this.BaseScale);
      if (isAfterCompletionView)
        this.miniMap.SetClaim();
      this.miniMap.location.Y = num1 + this.miniMap.GetSize().Y * 0.5f;
      float num2 = num1 + (this.miniMap.GetSize().Y + this.Ybuffer);
      float x = this.miniMap.GetSize().X;
      this.animalsForTradeFrame = new AnimalsForTradeFrame(this.refQuest, player, this.BaseScale, x, isAfterCompletionView);
      this.animalsForTradeFrame.location.Y = num2 + this.animalsForTradeFrame.GetSize().Y * 0.5f;
      float y = num2 + (this.animalsForTradeFrame.GetSize().Y + this.Ybuffer);
      this.customerFrame = new CustomerFrame(new Vector2(x + this.Xbuffer * 2f, y), CustomerFrameColors.Brown, this.BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.characterTextBox.Location.Y += vector2.Y;
      this.miniMap.location.Y += vector2.Y;
      this.animalsForTradeFrame.location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateAnimalQuestMainFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out QuestPack ExitToViewThisTrade,
      out bool ExitToOverworld)
    {
      ExitToOverworld = false;
      ExitToViewThisTrade = (QuestPack) null;
      offset += this.location;
      this.characterTextBox.UpdateCharacterTextBox(DeltaTime);
      this.miniMap.UpdateZ_MiniMap(DeltaTime);
      if (!this.animalsForTradeFrame.UpdateAnimalsForTradeFrame(player, DeltaTime, offset))
        return false;
      if (this.isShowingisAfterCompletionView)
      {
        ExitToOverworld = true;
        return true;
      }
      ExitToViewThisTrade = this.refQuest;
      return true;
    }

    public List<AnimalRenderDescriptor> GetListOfAnimalsNeededForRendering() => this.animalsForTradeFrame.GetListOfAnimalsNeededForRendering();

    public void DrawAnimalQuestMainFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.characterTextBox.DrawCharacterTextBox(offset, spriteBatch);
      this.miniMap.DrawZ_MiniMap(offset, spriteBatch);
      this.animalsForTradeFrame.DrawAnimalsForTradeFrame(offset, spriteBatch);
    }
  }
}
