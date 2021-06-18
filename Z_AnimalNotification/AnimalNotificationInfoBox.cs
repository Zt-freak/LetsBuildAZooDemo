// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalNotificationInfoBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalNotification.SubElements;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalNotificationInfoBox
  {
    private static Color cream = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private static float descriptionScreenWidthFraction = 0.17f;
    public Vector2 location;
    private float basescale;
    private AnimalNotificationType notificationType;
    private CustomerFrame frame;
    private Vector2 framescale;
    private BasicAnimalInfoBox basicbox;
    private CRISPRAnimalInfoBox crisprInfoBox;
    private AnimalVersusIcons animalVersus;
    private AnimalIconsScalingDisplay scalingIcons;
    private StatsListerBox statsLister;
    private LabelledBar bar;
    private float barValue = -1f;
    private string barLabel;
    private TextButton textbutton;
    private Vector2 textSize;
    private ZGenericText plusmore;
    private SimpleTextHandler description;
    private string descriptionStr = "";
    private Vector2 currLoc = Vector2.Zero;
    public bool ConstructionFailed;

    public AnimalNotificationInfoBox(
      AnimalNotificationType notificationType_,
      float basescale_,
      PrisonerInfo info,
      Player player,
      float customHeight = -1f)
    {
      this.Init(notificationType_, basescale_, new List<PrisonerInfo>()
      {
        info
      }, player, customHeight);
    }

    public AnimalNotificationInfoBox(
      AnimalNotificationType notificationType_,
      float basescale_,
      List<PrisonerInfo> infolist,
      Player player,
      float customHeight = -1f,
      List<AnimalRenderDescriptor> hybridAnimals = null)
    {
      this.Init(notificationType_, basescale_, infolist, player, customHeight, hybridAnimals);
    }

    public void Init(
      AnimalNotificationType notificationType_,
      float basescale_,
      List<PrisonerInfo> infolist,
      Player player,
      float customHeight = -1f,
      List<AnimalRenderDescriptor> hybridAnimals = null)
    {
      this.notificationType = notificationType_;
      PrisonerInfo prisonerInfo = (PrisonerInfo) null;
      PrisonerInfo animal1 = (PrisonerInfo) null;
      if (infolist != null && infolist.Count > 0)
      {
        prisonerInfo = infolist[0];
        animal1 = (PrisonerInfo) null;
        if (infolist.Count > 1)
          animal1 = infolist[1];
      }
      this.basescale = basescale_;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(basescale_);
      float num1 = uiScaleHelper.ScaleY(10f);
      double num2 = (double) uiScaleHelper.ScaleX(10f);
      this.textSize = uiScaleHelper.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString("stringggg"));
      string str1 = string.Empty;
      string str2 = string.Empty;
      string empty = string.Empty;
      string str3;
      if (this.notificationType == AnimalNotificationType.CRIPSRBirth)
      {
        if (hybridAnimals == null || hybridAnimals.Count == 0 || hybridAnimals[0] == null)
        {
          this.ConstructionFailed = true;
          return;
        }
        if (hybridAnimals.Count > 0)
        {
          int bodyAnimalType = (int) hybridAnimals[0].bodyAnimalType;
          int variant = hybridAnimals[0].variant;
          AnimalType headAnimalType = hybridAnimals[0].headAnimalType;
          int headVariant = hybridAnimals[0].headVariant;
          int num3 = (int) headAnimalType;
          str3 = HybridNames.GetAnimalCombinedName((AnimalType) bodyAnimalType, (AnimalType) num3);
        }
        else
          str3 = "NA Animal Bug";
      }
      else
      {
        if (prisonerInfo == null)
        {
          this.ConstructionFailed = true;
          return;
        }
        int animaltype = (int) prisonerInfo.intakeperson.animaltype;
        int clIndex = prisonerInfo.intakeperson.CLIndex;
        str1 = prisonerInfo.intakeperson.Name;
        str2 = prisonerInfo.intakeperson.IsAGirl ? "She" : "He";
        str3 = EnemyData.GetEnemyTypeName((AnimalType) animaltype);
      }
      float num4 = 3f * this.textSize.Y;
      switch (this.notificationType)
      {
        case AnimalNotificationType.Birth:
          this.descriptionStr = "An animal has been born.";
          this.descriptionStr = this.descriptionStr + " Say hello to " + str1 + "!";
          break;
        case AnimalNotificationType.Death:
          this.descriptionStr = str1 + " " + prisonerInfo.causeofdeath.GetDescription() + ". ";
          this.descriptionStr = this.descriptionStr + str2 + " was " + (object) prisonerInfo.Age + " days old.";
          break;
        case AnimalNotificationType.Hunger:
          this.descriptionStr = "Your " + str3.ToLower() + " is hungry!";
          this.descriptionStr = this.descriptionStr + " " + str1 + " will need some food soon or " + str2.ToLower() + " will starve.";
          this.barValue = prisonerInfo.Hunger;
          this.barLabel = "Satiation";
          break;
        case AnimalNotificationType.Fight:
          this.descriptionStr = "Oh no! A fight has broken out between " + str1 + " and " + animal1.intakeperson.Name + "!";
          break;
        case AnimalNotificationType.Breakout:
          this.descriptionStr = "Some animals have broken out of their enclosures!";
          break;
        case AnimalNotificationType.CRIPSRBirth:
          this.descriptionStr = "A new animal is ready to be collected from a CRIPSR Splicer!";
          break;
      }
      this.description = new SimpleTextHandler(this.descriptionStr, false, AnimalNotificationInfoBox.descriptionScreenWidthFraction * this.basescale, this.basescale, false, false);
      this.description.SetAllColours(ColourData.Z_Cream);
      this.description.AutoCompleteParagraph();
      Vector2 vector2 = new Vector2();
      switch (this.notificationType)
      {
        case AnimalNotificationType.Birth:
        case AnimalNotificationType.Death:
          if (infolist == null)
            return;
          this.basicbox = new BasicAnimalInfoBox(prisonerInfo, this.basescale);
          vector2 = this.basicbox.GetSize();
          this.textbutton = new TextButton(this.basescale, "Track", 40f);
          this.plusmore = new ZGenericText("1 of " + (object) infolist.Count + " total animals", this.basescale);
          break;
        case AnimalNotificationType.Hunger:
          if (infolist == null)
            return;
          this.basicbox = new BasicAnimalInfoBox(prisonerInfo, this.basescale);
          vector2 = this.basicbox.GetSize();
          this.bar = new LabelledBar(this.barValue, ColourData.Z_BarBabyGreen, this.barLabel, basescale_);
          this.bar.SetNewValues(MathHelper.Clamp(1f - prisonerInfo.Hunger, 0.0f, 1f));
          this.textbutton = new TextButton(this.basescale, "Track", 40f);
          this.plusmore = new ZGenericText("1 of " + (object) infolist.Count + " total animals", this.basescale);
          break;
        case AnimalNotificationType.Fight:
          this.animalVersus = new AnimalVersusIcons(prisonerInfo, animal1, this.basescale);
          vector2 = this.animalVersus.GetSize();
          this.textbutton = new TextButton(this.basescale, "View");
          break;
        case AnimalNotificationType.Breakout:
          if (infolist == null)
            return;
          this.scalingIcons = new AnimalIconsScalingDisplay(infolist, this.basescale);
          vector2 = this.scalingIcons.GetSize();
          this.statsLister = new StatsListerBox(this.basescale, false);
          break;
        case AnimalNotificationType.CRIPSRBirth:
          this.crisprInfoBox = new CRISPRAnimalInfoBox(this.basescale, hybridAnimals[0], player);
          vector2 = this.crisprInfoBox.GetSize();
          this.textbutton = new TextButton(this.basescale, "Track", 40f);
          this.plusmore = new ZGenericText("1 of " + (object) hybridAnimals.Count + " total animals", this.basescale);
          break;
      }
      this.framescale.X = uiScaleHelper.ScaleX(190f);
      this.framescale.Y += (float) ((double) vector2.Y + (double) num4 + (double) num1 + 2.0 * (double) num1);
      if (this.statsLister != null)
      {
        this.statsLister.AddOrUpdate("Animals loose", 0.ToString());
        this.statsLister.AddOrUpdate("Animals lost", 0.ToString());
        this.statsLister.AddOrUpdate("Human casualties", 0.ToString());
        this.framescale.Y += this.statsLister.GetSize().Y;
        this.framescale.Y += num1;
      }
      if (this.plusmore != null)
        this.framescale.Y += this.plusmore.GetSize().Y;
      if (this.bar != null)
        this.framescale.Y += this.bar.GetSize().Y + num1;
      if (this.textbutton != null)
        this.framescale.Y += this.textbutton.GetSize_True().Y + num1;
      if ((double) customHeight > (double) this.framescale.Y)
        this.framescale.Y = customHeight;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.currLoc.Y = -0.5f * this.framescale.Y + uiScaleHelper.ScaleY(10f);
      switch (this.notificationType)
      {
        case AnimalNotificationType.Birth:
        case AnimalNotificationType.Death:
        case AnimalNotificationType.Hunger:
          this.plusmore.vLocation = this.currLoc;
          this.currLoc.Y += this.plusmore.GetSize().Y;
          this.basicbox.location = this.currLoc;
          this.basicbox.location.Y += 0.5f * this.basicbox.GetSize().Y;
          break;
        case AnimalNotificationType.Fight:
          this.animalVersus.location.Y = this.currLoc.Y + 0.5f * this.animalVersus.GetSize().Y;
          break;
        case AnimalNotificationType.Breakout:
          this.scalingIcons.location.Y = this.currLoc.Y + 0.5f * this.scalingIcons.GetSize().Y;
          break;
        case AnimalNotificationType.CRIPSRBirth:
          this.plusmore.vLocation = this.currLoc;
          this.currLoc.Y += this.plusmore.GetSize().Y;
          this.crisprInfoBox.location.Y = this.currLoc.Y;
          break;
      }
      this.currLoc.X = -0.5f * this.framescale.X + uiScaleHelper.ScaleX(10f);
      this.currLoc.Y += vector2.Y + num1;
      this.description.Location = this.currLoc;
      this.currLoc.Y += num4 + num1;
      if (this.statsLister != null)
      {
        this.statsLister.location = this.currLoc;
        this.statsLister.location.X = 0.0f;
        this.statsLister.location.Y += 0.5f * this.statsLister.GetSize().Y;
        this.currLoc.Y += this.statsLister.GetSize().Y + num1;
      }
      if (this.bar != null)
      {
        this.bar.location = this.currLoc;
        this.bar.location.Y += 0.5f * this.bar.GetSize().Y;
        this.bar.location.X += this.bar.GetLabelSize().X;
        this.currLoc.Y += this.bar.GetSize().Y + num1;
      }
      if (this.textbutton != null)
      {
        this.textbutton.vLocation = this.currLoc;
        this.textbutton.vLocation.X = 0.0f;
        this.textbutton.vLocation.Y += 0.5f * this.textbutton.GetSize_True().Y;
        this.currLoc.Y += this.textbutton.GetSize().Y + num1;
      }
      if (this.crisprInfoBox == null)
        return;
      this.crisprInfoBox.location.X = this.currLoc.X;
    }

    public void AddOrUpdateStatsLister(string name, int value)
    {
      if (this.statsLister == null)
        return;
      this.statsLister.AddOrUpdate(name, value.ToString());
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateAnimalNotificationInfoBox(Player player, Vector2 offset, float DeltaTime)
    {
      bool flag = false;
      if (this.textbutton != null)
        flag |= this.textbutton.UpdateTextButton(player, offset, DeltaTime);
      return flag;
    }

    public void DrawAnimalNotificationInfoBox(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if ((double) this.barValue > 0.0)
        this.bar.DrawLabelledBar(offset, spritebatch);
      this.description.DrawSimpleTextHandler(offset, 1f, spritebatch);
      switch (this.notificationType)
      {
        case AnimalNotificationType.Birth:
        case AnimalNotificationType.Death:
          this.basicbox.DrawBasicAnimalInfoBox(offset, spritebatch);
          this.plusmore.DrawZGenericText(offset, spritebatch);
          this.textbutton.DrawTextButton(offset, 1f, spritebatch);
          break;
        case AnimalNotificationType.Hunger:
          this.textbutton.DrawTextButton(offset, 1f, spritebatch);
          this.basicbox.DrawBasicAnimalInfoBox(offset, spritebatch);
          this.plusmore.DrawZGenericText(offset, spritebatch);
          break;
        case AnimalNotificationType.Fight:
          this.animalVersus.DrawAnimalVersusIcons(offset, spritebatch);
          this.textbutton.DrawTextButton(offset, 1f, spritebatch);
          break;
        case AnimalNotificationType.Breakout:
          this.scalingIcons.DrawAnimalIconsScalingDisplay(offset, spritebatch);
          this.statsLister.DrawStatsListerBox(offset, spritebatch);
          break;
        case AnimalNotificationType.CRIPSRBirth:
          this.crisprInfoBox.DrawCRISPRAnimalInfoBox(offset, spritebatch);
          this.textbutton.DrawTextButton(offset, 1f, spritebatch);
          this.plusmore.DrawZGenericText(offset, spritebatch);
          break;
      }
    }
  }
}
