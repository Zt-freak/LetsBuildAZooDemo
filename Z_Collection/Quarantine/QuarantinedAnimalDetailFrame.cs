// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Quarantine.QuarantinedAnimalDetailFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Quarantine
{
  internal class QuarantinedAnimalDetailFrame
  {
    public Vector2 location;
    private Vector2 size;
    private ZGenericText Name;
    private ZGenericText Species;
    private ZGenericText Disease;
    private ZGenericText TimeInQuaratine;
    private LabelledBar TimeBar;
    private TextButton selectButton;
    private int refBuildingUID;
    private PrisonerInfo refPrisonerInfo;

    public QuarantinedAnimalDetailFrame(
      PrisonerInfo info,
      float BaseScale,
      Vector2 frameSize,
      int BuildingUID,
      Player player)
    {
      this.refBuildingUID = BuildingUID;
      this.refPrisonerInfo = info;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.size = defaultBuffer;
      QuarantinedAnimal quarantinedAnimal = player.animalquarantine.GetThisQuarantineBuilding(BuildingUID).GetQuarantinedAnimal(info.intakeperson.UID);
      this.Name = new ZGenericText(info.intakeperson.Name, BaseScale, false, _UseOnePointFiveFont: true);
      string _textToWrite1 = EnemyData.GetEnemyTypeName(info.intakeperson.animaltype);
      if (info.intakeperson.HeadType != AnimalType.None)
        _textToWrite1 = HybridNames.GetAnimalCombinedName(info.intakeperson.animaltype, info.intakeperson.HeadType);
      this.Species = new ZGenericText(_textToWrite1, BaseScale, false);
      this.Disease = new ZGenericText("Disease: ???", BaseScale, false);
      float daysInQuarantine = (float) quarantinedAnimal.DaysInQuarantine;
      float quarantinePeriodDays = (float) quarantinedAnimal.QuarantinePeriod_Days;
      string _textToWrite2 = "Days Quaratined: " + (object) daysInQuarantine;
      if ((double) quarantinePeriodDays > 0.0)
      {
        this.TimeBar = new LabelledBar(daysInQuarantine / quarantinePeriodDays, ColourData.Z_BarBabyGreen, "Quarantine Progress", BaseScale, false);
        _textToWrite2 = _textToWrite2 + "/" + (object) quarantinePeriodDays;
      }
      this.TimeInQuaratine = new ZGenericText(_textToWrite2, BaseScale, false);
      this.selectButton = new TextButton(BaseScale, "", 50f);
      this.Name.vLocation = this.size;
      this.size.Y += this.Name.GetSize().Y;
      this.Species.vLocation = this.size;
      this.size.Y += this.Species.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.Disease.vLocation = this.size;
      this.size.Y += this.Disease.GetSize().Y;
      this.TimeInQuaratine.vLocation = this.size;
      this.size.Y += this.TimeInQuaratine.GetSize().Y;
      if (this.TimeBar != null)
      {
        this.TimeBar.location = this.size;
        this.size.Y += this.TimeBar.GetSize().Y;
        this.size.Y += defaultBuffer.Y * 0.5f;
      }
      this.selectButton.vLocation = frameSize - this.selectButton.GetSize() * 0.5f - defaultBuffer;
      this.SetSelectedButtonState(false);
    }

    public Vector2 GetSize() => this.size;

    public void SetSelectedButtonState(bool isSelected)
    {
      if (isSelected)
      {
        this.selectButton.SetText("Selected");
        this.selectButton.SetButtonColour(BTNColour.Blue);
      }
      else
      {
        this.selectButton.SetText("Select");
        this.selectButton.SetButtonColour(BTNColour.Green);
      }
    }

    public bool UpdateQuarantinedAnimalDetailFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ForceClosePanel,
      out bool RefreshGridList)
    {
      ForceClosePanel = false;
      RefreshGridList = false;
      offset += this.location;
      return this.selectButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawQuarantinedAnimalDetailFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.Name.DrawZGenericText(offset, spriteBatch);
      this.Species.DrawZGenericText(offset, spriteBatch);
      this.Disease.DrawZGenericText(offset, spriteBatch);
      this.TimeInQuaratine.DrawZGenericText(offset, spriteBatch);
      if (this.TimeBar != null)
        this.TimeBar.DrawLabelledBar(offset, spriteBatch);
      this.selectButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
