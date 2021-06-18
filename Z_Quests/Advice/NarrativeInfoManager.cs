// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.Advice.NarrativeInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD;
using TinyZoo.Z_Quests.HeroQuests.QuestList;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Quests.Advice
{
  internal class NarrativeInfoManager
  {
    private CustomerFrame customerframe;
    private float BaseScale;
    private Vector2 Location;
    private ZGenericText miniheader;
    private AnimalInFrame animalinframe;
    private QuestProgressDisplay questprogress;
    private SimpleTextHandler headerparagraph;
    private SimpleTextHandler paragraph;
    private SimpleTextHandler HEROPROGRESS;
    private TextButton Confirm;
    public HeroCharacter herocharacter;
    public HeroProgressStatus RefHeroProgress;
    public HeroQuestDescription quest;
    public bool IsComplete;
    private CheckBoxInFrame checkbox;
    private TinyZoo.Z_Quests.HeroQuests.QuestAction.QuestAction questaction;

    public NarrativeInfoManager(
      HeroQuestDescription _quest,
      Vector2 VScale,
      float _BaseScale,
      HeroCharacter hero,
      HeroProgressStatus _RefHeroProgress,
      Player player,
      bool IsForcedPopUpFromNewTask = false)
    {
      _RefHeroProgress.IsNew = false;
      this.quest = _quest;
      this.herocharacter = hero;
      this.RefHeroProgress = _RefHeroProgress;
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      Vector2 _VSCale = defaultBuffer;
      this.animalinframe = new AnimalInFrame(_RefHeroProgress.RefHeroQuestPack.herocharacter <= HeroCharacter.Count ? HeroQuestData.GetHeroQuestData(hero).ThisCharacterAnimal : HeroQuestData.GetHeroCharacterToAnimalType(_RefHeroProgress.RefHeroQuestPack.herocharacter), AnimalType.None, TargetSize: (50f * this.BaseScale), FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: (this.BaseScale * 2f));
      this.animalinframe.Location = _VSCale;
      this.animalinframe.Location += this.animalinframe.GetSize() * 0.5f;
      Vector2 vector2_1 = _VSCale;
      vector2_1.X += defaultBuffer.X + this.animalinframe.GetSize().X;
      this.miniheader = new ZGenericText(HeroQuestData.GetHeroCharacterToString(hero), this.BaseScale, false, _UseOnePointFiveFont: true);
      this.miniheader.vLocation = vector2_1;
      vector2_1.Y += this.miniheader.GetSize().Y;
      this.questprogress = new QuestProgressDisplay(this.RefHeroProgress, player, this.BaseScale, true);
      this.questprogress.Location = vector2_1;
      vector2_1.Y += this.questprogress.GetSize().Y;
      vector2_1.X += Math.Max(this.miniheader.GetSize().X, this.questprogress.GetSize().X);
      float num = Math.Max(uiScaleHelper.ScaleX(350f), vector2_1.X);
      string str1 = SEngine.Localization.Localization.GetText(761) + (object) this.RefHeroProgress.GetCompletetedQuests() + "/" + (object) this.RefHeroProgress.RefHeroQuestPack.heroquests.Count;
      _VSCale.Y += this.animalinframe.GetSize().Y;
      _VSCale.Y = Math.Max(_VSCale.Y, vector2_1.Y);
      _VSCale.Y += defaultBuffer.Y;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      bool _CentreJustify = false;
      string TextToWrite1;
      string TextToWrite2;
      if (IsForcedPopUpFromNewTask)
      {
        TextToWrite1 = HeroQuestData.GetHeroCharacterToString(hero) + "~~" + SEngine.Localization.Localization.GetText(762) + "~" + this.quest.GetQuestHeading();
        TextToWrite2 = this.quest.GetQuestDescription();
        _CentreJustify = true;
      }
      else
      {
        TextToWrite1 = SEngine.Localization.Localization.GetText(765);
        string str2 = SEngine.Localization.Localization.GetText(763);
        if (this.quest != null)
        {
          TextToWrite1 = SEngine.Localization.Localization.GetText(764) + this.quest.GetQuestHeading();
          str2 = this.quest.GetQuestDescription();
        }
        else if (_RefHeroProgress.GetCompletetedQuests() == _RefHeroProgress.RefHeroQuestPack.heroquests.Count)
          str2 = HeroQuestData.GetHeroCompleteText(hero);
        TextToWrite2 = str2 + "~~" + str1;
      }
      this.headerparagraph = new SimpleTextHandler(TextToWrite1, num, _CentreJustify, this.BaseScale, true, true);
      this.paragraph = new SimpleTextHandler(TextToWrite2, num, _CentreJustify, this.BaseScale, AutoComplete: true);
      this.headerparagraph.SetAllColours(ColourData.Z_Cream);
      this.paragraph.SetAllColours(ColourData.Z_Cream);
      this.headerparagraph.Location = _VSCale;
      _VSCale.Y += this.headerparagraph.GetHeightOfParagraph();
      if (!string.IsNullOrEmpty(TextToWrite1))
        _VSCale.Y += defaultBuffer.Y;
      this.paragraph.Location = _VSCale;
      _VSCale.Y += this.paragraph.GetHeightOfParagraph();
      if (!string.IsNullOrEmpty(TextToWrite2))
        _VSCale.Y += defaultBuffer.Y;
      if (_CentreJustify)
      {
        this.headerparagraph.Location.Y += this.headerparagraph.GetHeightOfOneLine() * 0.5f;
        this.paragraph.Location.Y += this.paragraph.GetHeightOfOneLine() * 0.5f;
      }
      _VSCale.X += num;
      _VSCale.X += defaultBuffer.X;
      if (this.quest != null && this.quest.CheckIsComplete(player))
      {
        this.IsComplete = true;
        this.Confirm = new TextButton(this.BaseScale, SEngine.Localization.Localization.GetText(766), 70f);
        this.Confirm.vLocation.Y = _VSCale.Y;
        this.Confirm.vLocation.Y += this.Confirm.GetSize_True().Y * 0.5f;
        _VSCale.Y += this.Confirm.GetSize_True().Y;
        _VSCale.Y += defaultBuffer.Y;
      }
      if (this.quest != null && !this.IsComplete && Z_GameFlags.HasStartedFirstDay)
      {
        this.checkbox = new CheckBoxInFrame(this.BaseScale, ZHudManager.zquestpins.GetIsPinned(this.quest) | _quest.WillAutoPin);
        this.checkbox.location = _VSCale;
        this.checkbox.location.Y += this.checkbox.GetSize().Y * 0.5f;
        this.checkbox.location.X -= this.checkbox.GetSize().X * 0.5f + defaultBuffer.X;
        _VSCale.Y += this.checkbox.GetSize().Y + defaultBuffer.Y;
      }
      if (this.quest != null && !this.IsComplete && this.quest.heroquesttype == HEROQUESTTYPE.DonateMoney)
      {
        this.questaction = new TinyZoo.Z_Quests.HeroQuests.QuestAction.QuestAction(this.quest, this.BaseScale, num);
        this.questaction.location = _VSCale;
        this.questaction.location.Y += 0.5f * this.questaction.GetSize().Y;
        this.questaction.location.X -= 0.5f * this.questaction.GetSize().X + defaultBuffer.X;
        _VSCale.Y += this.questaction.GetSize().Y + defaultBuffer.Y;
      }
      if (IsForcedPopUpFromNewTask)
      {
        this.miniheader = (ZGenericText) null;
        this.animalinframe.Location.X = _VSCale.X * 0.5f;
        this.HEROPROGRESS = (SimpleTextHandler) null;
        this.questprogress = (QuestProgressDisplay) null;
        this.headerparagraph.Location.X = _VSCale.X * 0.5f;
        this.paragraph.Location.X = _VSCale.X * 0.5f;
      }
      this.customerframe = new CustomerFrame(_VSCale, BaseScale: this.BaseScale);
      Vector2 vector2_2 = -this.customerframe.VSCale * 0.5f;
      if (this.checkbox != null)
        this.checkbox.location += vector2_2;
      this.headerparagraph.Location += vector2_2;
      this.paragraph.Location += vector2_2;
      if (this.HEROPROGRESS != null)
        this.HEROPROGRESS.Location += vector2_2;
      if (this.questprogress != null)
        this.questprogress.Location += vector2_2;
      if (this.miniheader != null)
      {
        ZGenericText miniheader = this.miniheader;
        miniheader.vLocation = miniheader.vLocation + vector2_2;
      }
      this.animalinframe.Location += vector2_2;
      if (this.Confirm != null)
        this.Confirm.vLocation.Y += vector2_2.Y;
      if (this.questaction == null)
        return;
      this.questaction.location += vector2_2;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool UpdateNarrativeInfoManager(Vector2 Offset, Player player, float DeltaTime)
    {
      if (this.checkbox != null && this.checkbox.UpdateCheckBoxInFrame(player, DeltaTime, Offset) && this.quest != null)
      {
        if (this.checkbox.GetIsTicked())
          ZHudManager.zquestpins.PinQuest(this.quest, player);
        else
          ZHudManager.zquestpins.UnPinQuest(this.quest, player);
      }
      if (this.questaction != null)
        this.questaction.UpdateQuestAction(player, Offset, DeltaTime);
      return this.Confirm != null && this.Confirm.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawNarrativeInfoManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.animalinframe.DrawAnimalInFrame(Offset, spritebatch);
      if (this.miniheader != null)
        this.miniheader.DrawZGenericText(Offset, spritebatch);
      if (this.questprogress != null)
        this.questprogress.DrawQuestProgressDisplay(Offset, spritebatch);
      if (this.HEROPROGRESS != null)
        this.HEROPROGRESS.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.headerparagraph.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.paragraph.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      if (this.Confirm != null)
        this.Confirm.DrawTextButton(Offset, 1f, spritebatch);
      if (this.checkbox != null)
        this.checkbox.DrawCheckBoxInFrame(Offset, spritebatch);
      if (this.questaction == null)
        return;
      this.questaction.DrawQuestAction(spritebatch, Offset);
    }
  }
}
