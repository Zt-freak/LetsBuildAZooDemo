// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Shared.Grid.AnimalGridEntryFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Text;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Elements;
using TinyZoo.Z_Collection.Animals;
using TinyZoo.Z_Farms;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Collection.Shared.Grid
{
  internal class AnimalGridEntryFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalInFrame animalInFrame;
    private StringScroller nameScroller;
    private ZGenericText animalName;
    private ZGenericText numberCollected;
    private MouseoverHandler mouseOverFrame;
    private CustomerFrame selectedFrame;
    private StarBarStar star;
    private DiseaseIcon diseaseIcon;
    private ActiveIcon tick;
    private float refBaseScale;
    private UIScaleHelper scaleHelper;
    private Vector2 OriginalFrameSize;
    private bool IsCurrentlySelected;
    private bool PointerIsOutOfZone;

    public AnimalType refAnimalType { get; private set; }

    public EmployeeType refEmployeeType { get; private set; }

    public TILETYPE refTileType { get; private set; }

    public PrisonerInfo refPrisonerInfo { get; private set; }

    public CROPTYPE refCropType { get; private set; }

    public bool IsUnlocked { get; private set; }

    public AnimalGridEntryFrame(
      CollectionType collectionType,
      Player player,
      float BaseScale,
      AnimalType animalType,
      EmployeeType employeeType = EmployeeType.None,
      TILETYPE tileType = TILETYPE.None,
      CROPTYPE croptype = CROPTYPE.Count)
    {
      this.SetUp(collectionType, player, BaseScale, animalType, employeeType, tileType, croptype: croptype);
    }

    public AnimalGridEntryFrame(
      CollectionType collectionType,
      Player player,
      float BaseScale,
      PrisonerInfo playerAnimal)
    {
      this.SetUp(collectionType, player, BaseScale, AnimalType.None, playerAnimal: playerAnimal);
    }

    private void SetUp(
      CollectionType collectionType,
      Player player,
      float BaseScale,
      AnimalType animalType,
      EmployeeType employeeType = EmployeeType.None,
      TILETYPE tileType = TILETYPE.None,
      PrisonerInfo playerAnimal = null,
      CROPTYPE croptype = CROPTYPE.None)
    {
      this.refBaseScale = BaseScale;
      this.refAnimalType = animalType;
      this.refEmployeeType = employeeType;
      this.refTileType = tileType;
      this.refPrisonerInfo = playerAnimal;
      this.refCropType = croptype;
      this.IsUnlocked = true;
      this.scaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = this.scaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      float num1 = 0.0f;
      float num2 = 0.0f;
      float y1 = num1 + defaultYbuffer;
      float x1 = num2 + defaultXbuffer;
      float x2 = this.scaleHelper.ScaleX(125f);
      this.animalInFrame = playerAnimal == null ? (croptype == CROPTYPE.Count ? new AnimalInFrame(animalType, AnimalType.None, TargetSize: (25f * BaseScale), BaseScale: BaseScale, HeadVariant: 0) : new AnimalInFrame(AnimalType.None, AnimalType.None, TargetSize: (25f * BaseScale), BaseScale: BaseScale, HeadVariant: 0, croptype: croptype)) : new AnimalInFrame(playerAnimal.intakeperson.animaltype, playerAnimal.intakeperson.HeadType, playerAnimal.intakeperson.CLIndex, 25f * BaseScale, BaseScale: BaseScale, HeadVariant: playerAnimal.intakeperson.HeadVariant);
      Vector2 size1 = this.animalInFrame.GetSize();
      this.animalInFrame.Location += size1 * 0.5f + new Vector2(x1, y1);
      float x3 = x1 + this.animalInFrame.GetSize().X + defaultXbuffer;
      string str1 = string.Empty;
      string str2 = string.Empty;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      switch (collectionType)
      {
        case CollectionType.Animals:
          str1 = "Unknown";
          int totalVaiantsFound = player.Stats.GetTotalVaiantsFound(animalType);
          int num3 = 10;
          str2 = "Found: " + (object) totalVaiantsFound + "/" + (object) num3;
          if (totalVaiantsFound == 0)
            this.SetAsLocked();
          else if (totalVaiantsFound == num3)
            flag1 = true;
          if (totalVaiantsFound > 0)
          {
            str1 = EnemyData.GetEnemyTypeName(animalType);
            break;
          }
          break;
        case CollectionType.EmployeesJobs:
          str1 = "Unknown";
          str2 = "Found: ???";
          this.SetAsLocked();
          break;
        case CollectionType.QuarantineAnimals:
          str1 = playerAnimal.intakeperson.Name;
          str2 = "Status: ???";
          flag2 = playerAnimal.GetIsSick();
          flag3 = playerAnimal.IsDead;
          break;
        case CollectionType.Seeds:
          str1 = CropData.GetCropTypeToString(croptype);
          str2 = "";
          break;
      }
      this.nameScroller = new StringScroller((float) ((double) x2 - (double) x3 - (double) defaultXbuffer * 0.5) / BaseScale, str1, AssetContainer.SpringFontX1AndHalf);
      this.animalName = new ZGenericText(str1, BaseScale, false, _UseOnePointFiveFont: true);
      Vector2 size2 = this.animalName.GetSize();
      this.animalName.vLocation = new Vector2(x3, y1);
      float y2 = y1 + size2.Y + defaultYbuffer * 0.5f;
      this.numberCollected = new ZGenericText(BaseScale, false);
      this.numberCollected.vLocation = new Vector2(x3, y2);
      this.numberCollected.textToWrite = str2;
      Vector2 size3 = this.numberCollected.GetSize();
      float val2 = y2 + size3.Y + defaultYbuffer;
      float y3 = Math.Max(size1.Y + defaultYbuffer * 2f, val2);
      Vector3 zFrameDarkBrown = ColourData.Z_FrameDarkBrown;
      this.customerFrame = new CustomerFrame(new Vector2(x2, y3), zFrameDarkBrown, BaseScale);
      if (flag3)
        this.customerFrame.SetAlertRed();
      this.OriginalFrameSize = this.customerFrame.VSCale;
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.animalInFrame.Location += vector2;
      ZGenericText animalName = this.animalName;
      animalName.vLocation = animalName.vLocation + vector2;
      ZGenericText numberCollected = this.numberCollected;
      numberCollected.vLocation = numberCollected.vLocation + vector2;
      this.mouseOverFrame = new MouseoverHandler(this.customerFrame.VSCale.X, this.customerFrame.VSCale.Y, BaseScale);
      if (flag1)
        this.AddStar();
      if (flag2)
        this.AddDiseaseIcon();
      this.selectedFrame = new CustomerFrame(this.customerFrame.VSCale, CustomerFrameColors.Cream, BaseScale);
    }

    public Vector2 GetSize() => this.selectedFrame != null ? this.selectedFrame.VSCale : this.customerFrame.VSCale;

    private void SetAsLocked()
    {
      this.IsUnlocked = false;
      this.animalInFrame.SetAnimalGreyedOut(true);
    }

    public void SetSelected(bool isSelected)
    {
      this.IsCurrentlySelected = isSelected;
      if (this.IsCurrentlySelected)
        this.customerFrame.VSCale = this.OriginalFrameSize - this.scaleHelper.ScaleVector2(new Vector2(2f, 2f));
      else
        this.customerFrame.VSCale = this.OriginalFrameSize;
    }

    private void AddStar()
    {
      this.star = new StarBarStar(1f, this.refBaseScale);
      this.star.GetSize();
      this.star.vLocation = this.animalInFrame.Location;
      StarBarStar star = this.star;
      star.vLocation = star.vLocation - this.animalInFrame.GetSize() * 0.5f;
    }

    private void AddDiseaseIcon()
    {
      this.diseaseIcon = new DiseaseIcon(this.refBaseScale);
      this.diseaseIcon.vLocation = this.animalInFrame.Location;
      DiseaseIcon diseaseIcon = this.diseaseIcon;
      diseaseIcon.vLocation = diseaseIcon.vLocation - this.animalInFrame.GetSize() * 0.5f;
    }

    public void SetTick(bool Remove)
    {
      if (Remove)
      {
        this.tick = (ActiveIcon) null;
      }
      else
      {
        this.tick = new ActiveIcon(true, this.refBaseScale, true);
        this.tick.vLocation = new Vector2(this.customerFrame.VSCale.X * 0.5f, (float) (-(double) this.customerFrame.VSCale.Y * 0.5));
        ActiveIcon tick = this.tick;
        tick.vLocation = tick.vLocation + new Vector2((float) (-(double) this.tick.GetSize().X * 0.5), this.tick.GetSize().Y * 0.5f);
      }
    }

    public bool UpdateAnimalGridEntryFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool _PointerIsOutOfZone = false)
    {
      offset += this.location;
      bool flag = false;
      this.PointerIsOutOfZone = _PointerIsOutOfZone;
      this.nameScroller.UpdateStringScroller(DeltaTime);
      this.animalName.textToWrite = this.nameScroller.GetString();
      if (!this.PointerIsOutOfZone)
      {
        this.mouseOverFrame.UpdateMouseoverHandler(player, offset, DeltaTime);
        flag = MathStuff.CheckPointCollision(true, offset, 1f, this.customerFrame.VSCale.X, this.customerFrame.VSCale.Y, player.player.touchinput.ReleaseTapArray[0]);
      }
      return flag;
    }

    public void DrawAnimalGridEntryFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.IsCurrentlySelected)
        this.selectedFrame.DrawCustomerFrame(offset, spriteBatch);
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
      this.animalName.DrawZGenericText(offset, spriteBatch);
      this.numberCollected.DrawZGenericText(offset, spriteBatch);
      if (this.star != null)
        this.star.DrawStar(spriteBatch, offset);
      if (!this.PointerIsOutOfZone)
        this.mouseOverFrame.DrawMouseOverHandler(spriteBatch, offset);
      if (this.diseaseIcon != null)
        this.diseaseIcon.DrawDiseaseIcon(offset, spriteBatch);
      if (this.tick == null)
        return;
      this.tick.DrawActiveIcon(spriteBatch, offset);
    }
  }
}
