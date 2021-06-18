// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Quarantine.QuarantineBottomFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Collection.Quarantine
{
  internal class QuarantineBottomFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText selectedText;
    private string selectedBaseString;
    private List<LittleSummaryButton> actionButtons;
    private SimpleTextHandler helperDesc;
    private TextButton selectAllButton;
    private int refBuildingUID;

    public List<PrisonerInfo> animalsSelected { get; private set; }

    public QuarantineBottomFrame(float BaseScale, float ForcedWidth, int _buildingUID)
    {
      this.refBuildingUID = _buildingUID;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 vector2_1 = defaultBuffer;
      this.animalsSelected = new List<PrisonerInfo>();
      this.actionButtons = new List<LittleSummaryButton>();
      this.actionButtons.Add(new LittleSummaryButton(LittleSummaryButtonType.Remove, true, BaseScale));
      this.actionButtons.Add(new LittleSummaryButton(LittleSummaryButtonType.ReleaseToWild, true, BaseScale));
      this.actionButtons.Add(new LittleSummaryButton(LittleSummaryButtonType.ReturnToPen, true, BaseScale));
      this.selectedBaseString = "Selected: ";
      this.selectedText = new ZGenericText("X", BaseScale, false, _UseOnePointFiveFont: true);
      this.helperDesc = new SimpleTextHandler("Select animals that you would like to perform an action on.", ForcedWidth * 0.6f, _Scale: BaseScale, AutoComplete: true);
      this.helperDesc.SetAllColours(ColourData.Z_Cream);
      this.selectAllButton = new TextButton(BaseScale, "Select All", 60f);
      this.selectedText.vLocation = vector2_1;
      vector2_1.Y += this.selectedText.GetSize().Y;
      this.helperDesc.Location = vector2_1;
      vector2_1.Y += this.helperDesc.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y;
      this.selectAllButton.vLocation = vector2_1;
      TextButton selectAllButton1 = this.selectAllButton;
      selectAllButton1.vLocation = selectAllButton1.vLocation + this.selectAllButton.GetSize_True() * 0.5f;
      vector2_1.Y += this.selectAllButton.GetSize_True().Y;
      vector2_1.Y += defaultBuffer.Y;
      for (int index = 0; index < this.actionButtons.Count; ++index)
      {
        Vector2 vector2_2 = new Vector2(ForcedWidth, vector2_1.Y);
        this.actionButtons[index].vLocation = new Vector2(ForcedWidth, vector2_1.Y);
        LittleSummaryButton actionButton1 = this.actionButtons[index];
        actionButton1.vLocation = actionButton1.vLocation - this.actionButtons[index].GetSize() * 0.5f;
        LittleSummaryButton actionButton2 = this.actionButtons[index];
        actionButton2.vLocation = actionButton2.vLocation - defaultBuffer;
        this.actionButtons[index].vLocation.X -= this.actionButtons[index].GetSize().X * (float) index;
        this.actionButtons[index].vLocation.X -= defaultBuffer.X * (float) index;
      }
      this.customerFrame = new CustomerFrame(new Vector2(ForcedWidth, vector2_1.Y), CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2_3 = -this.customerFrame.VSCale * 0.5f;
      for (int index = 0; index < this.actionButtons.Count; ++index)
      {
        LittleSummaryButton actionButton = this.actionButtons[index];
        actionButton.vLocation = actionButton.vLocation + vector2_3;
      }
      ZGenericText selectedText = this.selectedText;
      selectedText.vLocation = selectedText.vLocation + vector2_3;
      this.helperDesc.Location += vector2_3;
      TextButton selectAllButton2 = this.selectAllButton;
      selectAllButton2.vLocation = selectAllButton2.vLocation + vector2_3;
      this.SetButtonAndTextState();
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateQuarantineBottomFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ForceClosePanel,
      out bool SelectAllClicked)
    {
      offset += this.location;
      ForceClosePanel = false;
      SelectAllClicked = false;
      for (int index = 0; index < this.actionButtons.Count; ++index)
      {
        if (this.actionButtons[index].UpdateLittleSummaryButton(DeltaTime, player, offset))
          this.OnButtonClicked(this.actionButtons[index].Buttontype, player, out ForceClosePanel);
      }
      if (!this.selectAllButton.UpdateTextButton(player, offset, DeltaTime))
        return;
      SelectAllClicked = true;
    }

    private void OnButtonClicked(
      LittleSummaryButtonType buttonClicked,
      Player player,
      out bool ForceClosePanel)
    {
      ForceClosePanel = false;
      if (buttonClicked != LittleSummaryButtonType.ReturnToPen)
        return;
      QuarantineBuilding quarantineBuilding = player.animalquarantine.GetThisQuarantineBuilding(this.refBuildingUID);
      List<int> animalUID = new List<int>();
      for (int index = 0; index < this.animalsSelected.Count; ++index)
        animalUID.Add(this.animalsSelected[index].intakeperson.UID);
      quarantineBuilding.TransferQuarantinedAnimalToPen(animalUID, player, true);
      ForceClosePanel = true;
    }

    private void SetButtonAndTextState()
    {
      this.selectedText.textToWrite = this.selectedBaseString + (object) this.animalsSelected.Count;
      for (int index = 0; index < this.actionButtons.Count; ++index)
        this.actionButtons[index].SetDisabled(this.animalsSelected.Count == 0);
    }

    public bool AddOrRemoveAnimalToList(PrisonerInfo animal, bool DoNotRemove = false)
    {
      bool flag = false;
      if (this.animalsSelected.Contains(animal))
      {
        if (!DoNotRemove)
          this.animalsSelected.Remove(animal);
      }
      else
      {
        this.animalsSelected.Add(animal);
        flag = true;
      }
      this.SetButtonAndTextState();
      return flag;
    }

    public void DrawQuarantineBottomFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.selectedText.DrawZGenericText(offset, spriteBatch);
      for (int index = 0; index < this.actionButtons.Count; ++index)
        this.actionButtons[index].DrawLittleSummaryButton(offset, spriteBatch);
      this.helperDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.selectAllButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
