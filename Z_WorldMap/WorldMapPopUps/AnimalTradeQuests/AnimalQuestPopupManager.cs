// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalQuestPopupManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Quests;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection;
using TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests
{
  internal class AnimalQuestPopupManager
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private AnimalQuestMainFrame mainFrame;
    private AnimalSelectionManager animalSelectionManager;
    private NewThingPanel newThingPanel;
    private BlackOut blackOut;
    private CityName city;
    private float BaseScale;

    public AnimalQuestPopupManager(CityName _city, Player player)
    {
      this.city = _city;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, string.Format("{0} Zoo", (object) QuestData.GetCityName(this.city)), this.BaseScale, true);
      this.mainFrame = new AnimalQuestMainFrame(this.city, player, this.BaseScale);
      this.mainFrame.location.Y -= this.mainFrame.GetSize().Y * 0.5f;
      this.bigBrownPanel.Finalize(this.mainFrame.GetSize());
      this.mainFrame.location.Y -= this.bigBrownPanel.GetFrameOffsetFromTop().Y;
      this.bigBrownPanel.HasPreviousButton = false;
      this.blackOut = new BlackOut();
      this.blackOut.SetAlpha(0.0f);
    }

    public Vector2 GetSize() => this.bigBrownPanel.vScale;

    public Vector2 GetPositionOffset() => this.bigBrownPanel.InternalOffset;

    private void FadeInOrOutBlackOut(bool FadeIn)
    {
      float blendTime = 0.1f;
      if (FadeIn)
        this.blackOut.SetAlpha(true, blendTime, this.blackOut.fAlpha, 0.8f);
      else
        this.blackOut.SetAlpha(true, blendTime, this.blackOut.fAlpha, 0.0f);
    }

    public bool CheckMouseOver(Player player)
    {
      Vector2 location = this.location;
      return this.newThingPanel != null && this.newThingPanel.CheckMouseOver(player, Vector2.Zero) || this.bigBrownPanel.CheckMouseOver(player, location);
    }

    public bool AllowExternalClosing() => this.newThingPanel == null && !this.mainFrame.isShowingisAfterCompletionView;

    public bool UpdateAnimalQuestPopupManager(
      Player player,
      float DeltaTime,
      out bool exitToOverworld)
    {
      exitToOverworld = false;
      this.blackOut.UpdateColours(DeltaTime);
      Vector2 location = this.location;
      if (this.newThingPanel != null)
      {
        if (this.newThingPanel.UpdateNewThingPanel(player, DeltaTime, Vector2.Zero))
        {
          this.newThingPanel = (NewThingPanel) null;
          this.animalSelectionManager = (AnimalSelectionManager) null;
          this.bigBrownPanel.HasPreviousButton = false;
          this.mainFrame.Construct(player, true);
          this.bigBrownPanel.BlockCloseButton = true;
          this.FadeInOrOutBlackOut(false);
        }
      }
      else
      {
        if (this.animalSelectionManager != null)
        {
          bool flag = false;
          List<PrisonerInfo> animalsSelected;
          if (this.animalSelectionManager.UpdateAnimalSelectionManager(player, DeltaTime, location, out animalsSelected))
            this.DoTradeRightNow(animalsSelected, player, this.animalSelectionManager.refQuest);
          if (this.bigBrownPanel.UpdatePanelpreviousButton(player, DeltaTime, location))
            flag = true;
          if (flag)
          {
            this.animalSelectionManager = (AnimalSelectionManager) null;
            this.bigBrownPanel.HasPreviousButton = false;
          }
        }
        else
        {
          QuestPack ExitToViewThisTrade;
          if (this.mainFrame.UpdateAnimalQuestMainFrame(player, DeltaTime, location, out ExitToViewThisTrade, out exitToOverworld))
          {
            if (ExitToViewThisTrade != null)
            {
              if (ExitToViewThisTrade.trades_ListOnlyOne[0].Total.GetUnvallidatedValue() == 0)
              {
                this.DoTradeRightNow((List<PrisonerInfo>) null, player, ExitToViewThisTrade);
              }
              else
              {
                this.animalSelectionManager = new AnimalSelectionManager(ExitToViewThisTrade, this.mainFrame.GetListOfAnimalsNeededForRendering(), player, this.BaseScale, this.mainFrame.GetSize());
                this.bigBrownPanel.HasPreviousButton = true;
              }
            }
            else if (exitToOverworld)
              return true;
          }
        }
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
          return true;
      }
      return false;
    }

    public void DoTradeRightNow(
      List<PrisonerInfo> animals_toTradeAway,
      Player player,
      QuestPack quest)
    {
      player.zquests.DoTradeAndCompleteQuest(animals_toTradeAway, player, quest);
      this.newThingPanel = new NewThingPanel(new List<AnimalRenderDescriptor>()
      {
        new AnimalRenderDescriptor(quest.GetThisAnimal)
        {
          IsFemale = false
        },
        new AnimalRenderDescriptor(quest.GetThisAnimal)
        {
          IsFemale = true
        }
      });
      this.newThingPanel.location = new Vector2(512f, 384f);
      this.newThingPanel.LerpIn();
      this.FadeInOrOutBlackOut(true);
    }

    public void DrawAnimalQuestPopupManager()
    {
      SpriteBatch pointspritebatch03 = AssetContainer.pointspritebatch03;
      Vector2 location = this.location;
      this.bigBrownPanel.DrawBigBrownPanel(location, pointspritebatch03);
      if (this.animalSelectionManager != null)
        this.animalSelectionManager.DrawAnimalSelectionManager(location, pointspritebatch03);
      else
        this.mainFrame.DrawAnimalQuestMainFrame(location, pointspritebatch03);
      if (this.blackOut != null)
        this.blackOut.DrawBlackOut(Vector2.Zero, pointspritebatch03);
      if (this.newThingPanel == null)
        return;
      this.newThingPanel.DrawNewThingPanel(pointspritebatch03, Vector2.Zero);
    }
  }
}
