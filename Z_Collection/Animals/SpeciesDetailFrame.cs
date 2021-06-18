// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.SpeciesDetailFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Collection.Animals.DetailFrame;
using TinyZoo.Z_Collection.EmployeeSpecific;
using TinyZoo.Z_Collection.Quarantine;
using TinyZoo.Z_Farms.CropSum.Crop_Manager;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Collection.Animals
{
  internal class SpeciesDetailFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalDetailFrame animalDetailFrame;
    private EmployeeDetailFrame employeeDetailFrame;
    private QuarantinedAnimalDetailFrame quarantinedAnimalDetailFrame;
    public SeedDetailFrame seeddetailframe;
    private SimpleTextHandler nothingSelectedText;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private Vector2 frameOffset;
    private AnimalType refAnimalTypeSelected;
    private EmployeeType refEmployeeTypeSelected;
    private PrisonerInfo refPrisonerInfo;
    private CollectionType collectionType;

    public SpeciesDetailFrame(CollectionType _collectionType, float _BaseScale, float ForcedWidth)
    {
      this.BaseScale = _BaseScale;
      this.collectionType = _collectionType;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.refAnimalTypeSelected = AnimalType.None;
      Vector2 _VSCale = new Vector2(ForcedWidth, this.scaleHelper.ScaleY(100f));
      if (_collectionType == CollectionType.Seeds)
        _VSCale.Y = this.scaleHelper.ScaleY(120f);
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.DarkBrown, this.BaseScale);
      this.frameOffset = -this.customerFrame.VSCale * 0.5f;
      string TextToWrite = "Select an animal species to view more information.";
      if (this.collectionType == CollectionType.EmployeesJobs)
        TextToWrite = "Select a job to view more information.";
      else if (this.collectionType == CollectionType.QuarantineAnimals)
        TextToWrite = "Select a quarantined animal to view more information.";
      this.nothingSelectedText = new SimpleTextHandler(TextToWrite, true, (float) ((double) ForcedWidth / 1024.0 * 0.899999976158142), this.BaseScale, AutoComplete: true);
      this.nothingSelectedText.SetAllColours(ColourData.Z_Cream);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetToNoneSelected()
    {
      this.refAnimalTypeSelected = AnimalType.None;
      this.refEmployeeTypeSelected = EmployeeType.None;
      this.refPrisonerInfo = (PrisonerInfo) null;
    }

    public void SetAnimal(AnimalType animalType, Player player, bool IsBaseTypeUnlocked)
    {
      this.refAnimalTypeSelected = animalType;
      this.animalDetailFrame = new AnimalDetailFrame(animalType, player, this.BaseScale, IsBaseTypeUnlocked, this.customerFrame.VSCale);
      this.animalDetailFrame.location += -this.customerFrame.VSCale * 0.5f;
    }

    public void SetSeedToApplyToField(Player player, bool IsBaseTypeUnlocked, CROPTYPE croptype)
    {
      this.seeddetailframe = new SeedDetailFrame(player, IsBaseTypeUnlocked, croptype, this.BaseScale, this.customerFrame.VSCale);
      this.seeddetailframe.location += -this.customerFrame.VSCale * 0.5f;
    }

    public void SetEmployeeTypeToView(
      EmployeeType employeeType,
      AnimalType animalType,
      TILETYPE tileType,
      Player player,
      bool IsBaseTypeUnlocked)
    {
      this.refEmployeeTypeSelected = employeeType;
      this.refAnimalTypeSelected = animalType;
      this.employeeDetailFrame = new EmployeeDetailFrame(employeeType, animalType, tileType, this.BaseScale);
      this.employeeDetailFrame.location += -this.customerFrame.VSCale * 0.5f;
    }

    public void SetQuarantinedAnimalToView(
      PrisonerInfo prisonerInfo,
      Player player,
      int BuildingUID)
    {
      this.refPrisonerInfo = prisonerInfo;
      this.quarantinedAnimalDetailFrame = new QuarantinedAnimalDetailFrame(prisonerInfo, this.BaseScale, this.customerFrame.VSCale, BuildingUID, player);
      this.quarantinedAnimalDetailFrame.location += -this.customerFrame.VSCale * 0.5f;
    }

    public void UpdateSpeciesDetailFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.animalDetailFrame == null)
        return;
      this.animalDetailFrame.UpdateAnimalDetailFrame(player, DeltaTime, offset);
    }

    public bool UpdateSeedDetailFrame(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      out bool ForceClosePanel,
      out bool RefreshGridList)
    {
      Offset += this.location;
      RefreshGridList = false;
      ForceClosePanel = false;
      if (this.seeddetailframe.UpdateSeedDetailFrame(Offset, player, DeltaTime))
      {
        player.farms.SetSeedType(this.seeddetailframe.croptype, player);
        ForceClosePanel = true;
      }
      return ForceClosePanel;
    }

    public bool UpdateQuarantinedAnimalsDetailFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ForceClosePanel,
      out bool RefreshGridList)
    {
      offset += this.location;
      ForceClosePanel = false;
      RefreshGridList = false;
      return this.quarantinedAnimalDetailFrame != null && this.quarantinedAnimalDetailFrame.UpdateQuarantinedAnimalDetailFrame(player, DeltaTime, offset, out ForceClosePanel, out RefreshGridList);
    }

    public void SetTextButtonState(bool isSelected)
    {
      if (this.quarantinedAnimalDetailFrame == null)
        return;
      this.quarantinedAnimalDetailFrame.SetSelectedButtonState(isSelected);
    }

    public void DrawSpeciesDetailFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.refEmployeeTypeSelected != EmployeeType.None)
        this.employeeDetailFrame.DrawEmployeeDetailFrame(offset, spriteBatch);
      else if (this.refPrisonerInfo != null)
        this.quarantinedAnimalDetailFrame.DrawQuarantinedAnimalDetailFrame(offset, spriteBatch);
      else if (this.seeddetailframe != null)
        this.seeddetailframe.DrawSeedDetailFrame(offset, spriteBatch);
      else if (this.refAnimalTypeSelected != AnimalType.None)
        this.animalDetailFrame.DrawAnimalDetailFrame(offset, spriteBatch);
      else
        this.nothingSelectedText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
