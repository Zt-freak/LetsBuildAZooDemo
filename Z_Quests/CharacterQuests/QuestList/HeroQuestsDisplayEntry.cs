// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.CharacterQuests.QuestList.HeroQuestsDisplayEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Quests.HeroQuests.QuestList;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Quests.CharacterQuests.QuestList
{
  internal class HeroQuestsDisplayEntry
  {
    public int Page;
    private CustomerFrame customerframe;
    public Vector2 Location;
    private AnimalInFrame animalinframe;
    private NameAndQuest nameandquest;
    private QuestProgressDisplay questprogress;
    private LittleSummaryButton inspect;
    public HeroQuestDescription quest;
    public AnimalType animal;
    public HeroCharacter herocharacter;
    public HeroProgressStatus RefHeroProgress;
    private bool IsComplete;
    private PressTheButtonText pressthebutton;
    private LerpHandler_Float NewTaskLerper;
    private float ValueLerp;
    public bool HasWillShortcutOnPanelOpen;

    public HeroQuestsDisplayEntry(
      float BaseScale,
      HeroProgressStatus heroprogress,
      float Width,
      Player player,
      HeroCharacter ThisHeroIsDoingANewTask,
      BigBrownPanel panel)
    {
      this.RefHeroProgress = heroprogress;
      this.quest = heroprogress.GetActiveQuest();
      if (this.quest != null)
      {
        this.IsComplete = this.quest.CheckIsComplete(player);
        if (this.IsComplete && this.quest.WillShortcutOnPanelOpenIfomplete)
          this.HasWillShortcutOnPanelOpen = true;
        else if (this.IsComplete)
          Z_GameFlags.HasCompleteQuestsToView = true;
      }
      this.animal = heroprogress.thishero <= HeroCharacter.Count ? HeroQuestData.GetHeroQuestData(heroprogress.thishero).ThisCharacterAnimal : HeroQuestData.GetHeroCharacterToAnimalType(heroprogress.thishero);
      this.herocharacter = heroprogress.thishero;
      this.customerframe = new CustomerFrame(new Vector2(Width * BaseScale, 80f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.animalinframe = new AnimalInFrame(this.animal, AnimalType.None, TargetSize: (50f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: (BaseScale * 2f));
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.animalinframe.Location.X = Width * -0.5f * BaseScale;
      this.animalinframe.Location.X += 25f * BaseScale;
      this.animalinframe.Location.X += 10f * BaseScale;
      this.nameandquest = new NameAndQuest(HeroQuestData.GetHeroCharacterToString(heroprogress.thishero) + "~" + heroprogress.GetQuestName(), BaseScale, true, 1f);
      this.nameandquest.vLocation.X += this.animalinframe.Location.X + 35f * BaseScale;
      this.nameandquest.vLocation.Y += defaultBuffer.Y;
      this.nameandquest.vLocation.Y += this.customerframe.VSCale.Y * -0.5f;
      this.questprogress = new QuestProgressDisplay(heroprogress, player, BaseScale);
      this.questprogress.nameandquest.vLocation = this.nameandquest.vLocation;
      this.questprogress.nameandquest.vLocation.Y += this.nameandquest.GetSize().Y;
      if (this.IsComplete)
      {
        this.inspect = new LittleSummaryButton(LittleSummaryButtonType.FinishQuest, _BaseScale: BaseScale);
        this.pressthebutton = new PressTheButtonText(BaseScale);
      }
      else
      {
        if (heroprogress.IsNew)
          this.pressthebutton = new PressTheButtonText(BaseScale, true);
        this.inspect = new LittleSummaryButton(LittleSummaryButtonType.Locate, _BaseScale: BaseScale);
      }
      this.inspect.vLocation.X += Width * 0.5f * BaseScale;
      this.inspect.vLocation.X -= (float) (this.inspect.DrawRect.Width / 2) * BaseScale;
      this.inspect.vLocation.X -= 10f * BaseScale;
      if (this.pressthebutton != null)
      {
        this.pressthebutton.vLocation = this.inspect.vLocation;
        this.pressthebutton.vLocation.X -= (float) (this.inspect.DrawRect.Width / 2) * BaseScale;
        this.pressthebutton.vLocation.X -= 5f * BaseScale;
      }
      this.ValueLerp = 1f;
      if (ThisHeroIsDoingANewTask == this.herocharacter)
      {
        this.NewTaskLerper = new LerpHandler_Float();
        this.NewTaskLerper.SetLerp(true, 0.0f, 1f, 2f);
        this.NewTaskLerper.SetDelay(0.3f);
        this.ValueLerp = -0.2f;
      }
      if (player.Stats.TutorialsComplete[28] || !this.IsComplete || (this.quest.UID != 0 || this.quest.herocharacter != HeroCharacter.Investor))
        return;
      panel.BlockCloseButton = true;
      FeatureFlags.BlockMouseOverOnBuildBar = true;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool UpdateHeroQuestsDisplayEntry(Vector2 Offset, float DeltaTime, Player player)
    {
      if (this.NewTaskLerper != null)
      {
        this.ValueLerp += DeltaTime * 2f;
        if ((double) this.ValueLerp > 1.0)
        {
          this.ValueLerp = 1f;
          this.NewTaskLerper = (LerpHandler_Float) null;
        }
        return false;
      }
      Offset += this.Location;
      return this.inspect.UpdateLittleSummaryButton(DeltaTime, player, Offset);
    }

    public void DrawHeroQuestsDisplayEntry(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      if ((double) this.ValueLerp < 1.0)
      {
        float valueLerp = this.ValueLerp;
        if ((double) this.ValueLerp <= 0.0)
          return;
        this.customerframe.frame.SetAlpha(Math.Min(valueLerp * 2f, 1f));
        this.customerframe.DrawCustomerFrame(Offset, spritebatch);
        if ((double) valueLerp > 0.400000005960464)
          this.animalinframe.DrawAnimalInFrame(Offset);
        if ((double) valueLerp > 0.600000023841858)
          this.nameandquest.DrawNameAndQuest(Offset, spritebatch);
        if ((double) valueLerp > 0.800000011920929)
          this.questprogress.DrawQuestProgressDisplay(Offset, spritebatch);
        if ((double) valueLerp > 0.600000023841858)
        {
          this.inspect.fAlpha = (float) ((double) valueLerp * 2.5 - 0.600000023841858);
          this.inspect.DrawLittleSummaryButton(Offset, spritebatch);
        }
        this.inspect.fAlpha = 1f;
        this.customerframe.frame.SetAlpha(1f);
      }
      else
      {
        this.customerframe.DrawCustomerFrame(Offset, spritebatch);
        this.animalinframe.DrawAnimalInFrame(Offset);
        this.nameandquest.DrawNameAndQuest(Offset, spritebatch);
        this.questprogress.DrawQuestProgressDisplay(Offset, spritebatch);
        if (this.pressthebutton != null)
          this.pressthebutton.DrawPressTheButtonText(Offset, spritebatch);
        this.inspect.DrawLittleSummaryButton(Offset, spritebatch);
      }
    }
  }
}
