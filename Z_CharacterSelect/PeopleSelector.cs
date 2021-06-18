// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CharacterSelect.PeopleSelector
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_CharacterSelect
{
  internal class PeopleSelector
  {
    private SelectablePersonSet[] people;
    private bool Exiting;
    private int SelectedIndex_Controller;
    private ButtonRepeater repeater;
    private int selectedPerson = -1;
    private int selectedSet = -1;
    private LerpHandler_Float lerper;
    private BigBrownPanel BGPanel;
    public CustomerFrame custmomerframe;
    private SelectionFrame SelectionFrame_Controller;
    private float BaseScale;

    public PeopleSelector()
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      float num = 50f * baseScaleForUi;
      float Offset = 50f * baseScaleForUi;
      float y = 400f;
      this.Exiting = false;
      this.people = new SelectablePersonSet[2];
      List<AnimalType> ListOfPeople = new List<AnimalType>();
      ListOfPeople.Add(AnimalType.MaleZookeeper);
      ListOfPeople.Add(AnimalType.MaleAsianZookeeper);
      ListOfPeople.Add(AnimalType.MaleDarkZookeeper);
      this.people[0] = new SelectablePersonSet(true, ListOfPeople, new Vector2(512f - num, y), -Offset, baseScaleForUi);
      ListOfPeople.Clear();
      ListOfPeople.Add(AnimalType.FemaleZookeeper);
      ListOfPeople.Add(AnimalType.FemaleAsianZookeeper);
      ListOfPeople.Add(AnimalType.FemaleDarkZookeeper);
      this.people[1] = new SelectablePersonSet(false, ListOfPeople, new Vector2(512f + num, y), Offset, baseScaleForUi);
      this.repeater = new ButtonRepeater();
      this.SelectedIndex_Controller = -this.people[0].GetNumberOfPeople();
      this.SelectionFrame_Controller = new SelectionFrame(60, 100, 2f);
      this.SelectionFrame_Controller.Corners[0].SetAllColours(ColourData.Z_PaleBrown);
      this.selectedSet = 0;
      this.selectedPerson = this.people[0].GetNumberOfPeople() - 1;
      this.BGPanel = new BigBrownPanel(new Vector2(200f, 200f), addHeaderText: SEngine.Localization.Localization.GetText(752), _BaseScale: baseScaleForUi);
      this.custmomerframe = new CustomerFrame(new Vector2(400f, 120f * Sengine.ScreenRatioUpwardsMultiplier.Y) * baseScaleForUi);
      this.BGPanel.Finalize(this.custmomerframe.VSCale);
      this.BGPanel.location = new Vector2(512f, y + 10f * baseScaleForUi * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.lerper = new LerpHandler_Float();
    }

    public bool UpdatePeopleSelector(float DeltaTime, Player player)
    {
      bool flag1 = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      bool flag2 = false;
      for (int index = 0; index < this.people.Length; ++index)
      {
        int num = this.people[index].UpdateSelectablePersonSet(player, DeltaTime, this.Exiting);
        if (num != -1)
        {
          this.selectedPerson = num;
          this.selectedSet = index;
          flag2 = true;
        }
      }
      if (!this.Exiting)
      {
        DirectionPressed Direction;
        if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, false, false, player.inputmap.HeldButtons[18], player.inputmap.HeldButtons[19]))
        {
          switch (Direction)
          {
            case DirectionPressed.Right:
              if (this.SelectedIndex_Controller < this.people[1].GetNumberOfPeople())
              {
                ++this.SelectedIndex_Controller;
                if (this.SelectedIndex_Controller == 0)
                  ++this.SelectedIndex_Controller;
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
                break;
              }
              break;
            case DirectionPressed.Left:
              if (this.SelectedIndex_Controller > -this.people[0].GetNumberOfPeople())
              {
                --this.SelectedIndex_Controller;
                if (this.SelectedIndex_Controller == 0)
                  --this.SelectedIndex_Controller;
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
                break;
              }
              break;
          }
          if (this.SelectedIndex_Controller < 0)
          {
            this.selectedSet = 0;
            this.selectedPerson = Math.Abs(this.SelectedIndex_Controller) - 1;
          }
          else
          {
            this.selectedSet = 1;
            this.selectedPerson = Math.Abs(this.SelectedIndex_Controller) - 1;
          }
        }
        if (player.inputmap.PressedThisFrame[0])
          flag2 = true;
        if (flag2)
          this.lerper.SetLerp(false, 0.0f, 1f, 3f);
      }
      if (flag2 && !this.Exiting)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        player.Stats.HasPickedCharacter = true;
        player.Stats.ZooKeeperAvatarIndex = (int) this.people[this.selectedSet].GetAnimalType(this.selectedPerson);
        this.Exiting = true;
        flag1 = true;
        bool DelayHumanLerp = false;
        if (this.people[this.selectedSet].GetNumberOfPeople() % 2 == 0)
          DelayHumanLerp = true;
        else if (this.people[this.selectedSet].GetNumberOfPeople() / 2 != this.selectedPerson)
          DelayHumanLerp = true;
        for (int index = 0; index < this.people.Length; ++index)
        {
          if (this.selectedSet == index)
            this.people[index].Exit(this.selectedPerson, DelayHumanLerp);
          else
            this.people[index].Exit(DelayHumanLerp: DelayHumanLerp);
        }
        Console.WriteLine("You selected:" + (object) (AnimalType) player.Stats.ZooKeeperAvatarIndex);
      }
      return flag1;
    }

    public void DrawPeopleSelector()
    {
      Vector2 vector2 = new Vector2(0.0f, this.lerper.Value * -70f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.BGPanel.DrawBigBrownPanel(vector2, AssetContainer.pointspritebatch01);
      this.custmomerframe.DrawCustomerFrame(this.BGPanel.location + vector2, AssetContainer.pointspritebatch01);
      for (int index = 0; index < this.people.Length; ++index)
      {
        if (GameFlags.IsUsingController && this.selectedSet == index)
          this.people[index].ForceMouseOver(this.selectedPerson);
        this.people[index].DrawSelectablePersonSet(vector2);
        if (GameFlags.IsUsingController)
        {
          int selectedSet = this.selectedSet;
        }
      }
    }
  }
}
