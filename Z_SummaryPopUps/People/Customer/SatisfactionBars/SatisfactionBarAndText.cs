// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars.SatisfactionBarAndText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Employees.QuickPick;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars
{
  internal class SatisfactionBarAndText : GameObject
  {
    private SatisfactionBar satisfactionbar;
    private string TEXT = "";
    public Vector2 Location;
    private float TextOffsetGap = 5f;
    private CustomerFrameMouseOverBox mouseovertext;
    private SatisfactionType refSatisfactionType;
    private StoreEmployeeStat refEmployeeStat;
    private AnimalSatisfactionType refAnimalSatisfactionType;
    private SimPerson refSimPerson;
    private QuickEmployeeDescription refEmployeeDesc;
    private PrisonerInfo refPrisonerInfo;

    public float Value
    {
      get => this.satisfactionbar.Value;
      set => this.satisfactionbar.Value = value;
    }

    public SatisfactionBarAndText(
      SimPerson simperson,
      SatisfactionType satisfactiontype,
      float BaseScale = 1f)
    {
      this.refSatisfactionType = satisfactiontype;
      this.refSimPerson = simperson;
      this.TEXT = SatisfactionBarAndText.GetSatisfactionTypeToString(satisfactiontype);
      this.satisfactionbar = new SatisfactionBar(simperson.memberofthepublic.customerneeds.CurrentWantValues[(int) satisfactiontype], BaseScale);
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.vLocation.X = this.satisfactionbar.GetVScale().X * -0.5f;
      this.TextOffsetGap *= BaseScale;
      this.vLocation.X -= this.TextOffsetGap;
      this.SetValues();
    }

    public SatisfactionBarAndText(
      SimPerson simperson,
      EmployeeSatisfactionType satisfactiontype,
      float BaseScale = 1f)
    {
      this.TEXT = SatisfactionBarAndText.GetSatisfactionTypeToString(satisfactiontype);
      this.satisfactionbar = new SatisfactionBar(0.5f, BaseScale);
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.vLocation.X = this.satisfactionbar.GetVScale().X * -0.5f;
      this.TextOffsetGap *= BaseScale;
      this.vLocation.X -= this.TextOffsetGap;
      this.SetValues();
    }

    public SatisfactionBarAndText(string CustomHeading, float Fullness, float BaseScale = 1f)
    {
      this.TEXT = CustomHeading;
      this.satisfactionbar = new SatisfactionBar(Fullness, BaseScale);
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.vLocation.X = this.satisfactionbar.GetVScale().X * -0.5f;
      this.TextOffsetGap *= BaseScale;
      this.vLocation.X -= this.TextOffsetGap;
      this.SetAllColours(ColourData.Z_Cream);
      this.SetValues();
    }

    public void SetMouseOver(string TEXT, float BaseScale) => this.mouseovertext = new CustomerFrameMouseOverBox(BaseScale, TEXT, 350f);

    internal static SatisfactionType GetQuestToSatisfaction(
      CustomerQuest customerquest)
    {
      switch (customerquest)
      {
        case CustomerQuest.SeekingBathroom:
          return SatisfactionType.Bathroom;
        case CustomerQuest.SeekingDrink:
          return SatisfactionType.Thirst;
        case CustomerQuest.SeekingFood:
          return SatisfactionType.Hunger;
        case CustomerQuest.SeekingSouvenier:
          return SatisfactionType.Souvenirs;
        case CustomerQuest.SeekingIceCream:
          return SatisfactionType.IceCream;
        case CustomerQuest.SeekingBench:
          return SatisfactionType.Energy;
        case CustomerQuest.SeekingATM:
          return SatisfactionType.GetCash;
        case CustomerQuest.SeekingBuyingSouvenirsBeforeLeavingPark:
          return SatisfactionType.Souvenirs;
        default:
          throw new Exception("ihsdf");
      }
    }

    public SatisfactionBarAndText(float Value, string _Text, float BaseScale = 1f)
    {
      this.TEXT = _Text;
      this.satisfactionbar = new SatisfactionBar(Value, BaseScale);
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.vLocation.X = this.satisfactionbar.GetVScale().X * -0.5f;
      this.TextOffsetGap *= BaseScale;
      this.vLocation.X -= this.TextOffsetGap;
      this.SetValues();
    }

    public SatisfactionBarAndText(
      PrisonerInfo animal,
      AnimalSatisfactionType satisfactiontype,
      float BaseScale = 1f)
    {
      this.refPrisonerInfo = animal;
      this.refAnimalSatisfactionType = satisfactiontype;
      this.TEXT = SatisfactionBarAndText.GetSatisfactionTypeToString(satisfactiontype);
      this.satisfactionbar = new SatisfactionBar(this.GetValueForThisBar(satisfactiontype, animal), BaseScale);
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.vLocation.X = this.satisfactionbar.GetVScale().X * -0.5f;
      this.TextOffsetGap *= BaseScale;
      this.vLocation.X -= this.TextOffsetGap;
      this.SetValues();
    }

    private float GetValueForThisBar(AnimalSatisfactionType satisfactiontype, PrisonerInfo animal)
    {
      switch (satisfactiontype)
      {
        case AnimalSatisfactionType.Happiness:
          return MathHelper.Clamp(animal.animalhappyness.GetHappiness(), 0.0f, 1f);
        case AnimalSatisfactionType.Satiation:
          return Math.Max(1f - animal.Hunger, 0.0f);
        case AnimalSatisfactionType.Hydration:
          return animal.GetHydration();
        case AnimalSatisfactionType.Hygiene:
          return animal.GetHygiene();
        case AnimalSatisfactionType.Health:
          return animal.animalhappyness.GetHappiness();
        case AnimalSatisfactionType.Habitat:
          return animal.TEMP_HabitatMultiplers;
        case AnimalSatisfactionType.Enrichment:
          return animal.GetEnrichment();
        case AnimalSatisfactionType.Nutrition:
          return animal.GetNutrition();
        default:
          return 0.0f;
      }
    }

    public SatisfactionBarAndText(
      QuickEmployeeDescription desc,
      StoreEmployeeStat stat,
      float BaseScale)
    {
      this.refEmployeeStat = stat;
      this.refEmployeeDesc = desc;
      this.TEXT = QuickEmployeeDescription.GetStoreEmployeeStatToString(stat);
      if (stat == StoreEmployeeStat.Experience)
        this.TEXT = this.TEXT + " Lvl:" + (object) desc.Level;
      desc.SetStatValues();
      this.satisfactionbar = new SatisfactionBar(desc.StoreEmployeeStatValues[(int) stat], BaseScale);
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale;
      this.TextOffsetGap *= BaseScale;
      this.vLocation.X = this.satisfactionbar.GetVScale().X * -0.5f;
      this.vLocation.X -= this.TextOffsetGap;
      this.SetValues();
    }

    public void PreviewChange(float newvalue, Vector3 positiveColour, Vector3 negativeColour) => this.satisfactionbar.PreviewChange(newvalue, positiveColour, negativeColour);

    public void ApplyChange() => this.satisfactionbar.ApplyChange();

    public float GetFullness(int layer = 0) => this.satisfactionbar.GetFullness(layer);

    public void SetBarColour(Vector3 colour, int layer = 0) => this.satisfactionbar.SetBarColours(colour, layer);

    public Vector2 GetSize(bool GetFullSize = false) => GetFullSize ? new Vector2(this.GetTextWidth() + this.TextOffsetGap + this.GetBarWidth(), this.GetHeight()) : this.satisfactionbar.GetSize();

    public float GetHeight() => this.satisfactionbar.GetVScale().Y;

    public float GetBarWidth() => this.satisfactionbar.GetVScale().X;

    public float GetTextWidth() => SpringFontUtil.MeasureString(this.TEXT, AssetContainer.springFont).X * this.scale + this.TextOffsetGap;

    private static string GetSatisfactionTypeToString(EmployeeSatisfactionType satisfaction)
    {
      switch (satisfaction)
      {
        case EmployeeSatisfactionType.Efficiency:
          return "Efficiency";
        case EmployeeSatisfactionType.Happyness:
          return "Happyness";
        case EmployeeSatisfactionType.Polliteness:
          return "Politeness";
        default:
          return "NO STRING";
      }
    }

    public static string GetSatisfactionTypeToString(SatisfactionType satisfaction)
    {
      switch (satisfaction)
      {
        case SatisfactionType.Energy:
          return "Energy";
        case SatisfactionType.Hunger:
          return "Satiation";
        case SatisfactionType.Thirst:
          return "Hydration";
        case SatisfactionType.Bathroom:
          return "Bathroom";
        case SatisfactionType.Animals:
          return "Animals";
        case SatisfactionType.Health:
          return "Health";
        default:
          return "Attractions";
      }
    }

    private static string GetSatisfactionTypeToString(AnimalSatisfactionType satisfaction)
    {
      switch (satisfaction)
      {
        case AnimalSatisfactionType.Happiness:
          return "Happiness";
        case AnimalSatisfactionType.Satiation:
          return "Satiation";
        case AnimalSatisfactionType.Hydration:
          return "Hydration";
        case AnimalSatisfactionType.Hygiene:
          return "Hygiene";
        case AnimalSatisfactionType.Health:
          return "Health";
        case AnimalSatisfactionType.Habitat:
          return "Habitat";
        case AnimalSatisfactionType.Enrichment:
          return "Enrichment";
        default:
          return "Nutrition";
      }
    }

    public void UpdateSatisfactionBarAndText(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      this.mouseovertext.Active = this.satisfactionbar.CheckMouseOver(player, Offset);
      this.SetValues();
    }

    private void SetValues()
    {
      if (this.refPrisonerInfo != null)
        this.satisfactionbar.SetFullness(this.GetValueForThisBar(this.refAnimalSatisfactionType, this.refPrisonerInfo), DoSetColorBasedOnValue: true);
      else if (this.refEmployeeDesc != null)
      {
        this.satisfactionbar.SetFullness(this.refEmployeeDesc.StoreEmployeeStatValues[(int) this.refEmployeeStat], DoSetColorBasedOnValue: true);
      }
      else
      {
        if (this.refSimPerson == null)
          return;
        float _Fullness = this.refSimPerson.memberofthepublic.customerneeds.CurrentWantValues[(int) this.refSatisfactionType];
        if (this.refSatisfactionType == SatisfactionType.Hunger || this.refSatisfactionType == SatisfactionType.Bathroom || this.refSatisfactionType == SatisfactionType.Thirst)
          _Fullness = 1f - _Fullness;
        this.satisfactionbar.SetFullness(_Fullness, DoSetColorBasedOnValue: true);
      }
    }

    public void DrawSatisfactionBarAndText(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      TextFunctions.DrawTextWithDropShadow(this.TEXT.ToUpper(), this.scale, Offset + this.vLocation, this.GetColour(), this.fAlpha, AssetContainer.springFont, spritebatch, false, true, false, 0.0f, 1);
      this.satisfactionbar.DrawSatisfactionBar(Offset, spritebatch);
      if (this.mouseovertext == null)
        return;
      this.mouseovertext.DrawCustomerFrameMouseOverBoc(Offset + this.vLocation, spritebatch);
    }

    public void DrawSatisfactionBarAndText_InverseOrder(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      TextFunctions.DrawTextWithDropShadow(this.TEXT.ToUpper(), this.scale, Offset + this.vLocation, this.GetColour(), this.fAlpha, AssetContainer.springFont, spritebatch, false, true, false, 0.0f, 1);
      this.satisfactionbar.DrawSatisfactionBar_InverseOrder(Offset, spritebatch);
      if (this.mouseovertext == null)
        return;
      this.mouseovertext.DrawCustomerFrameMouseOverBoc(Offset + this.vLocation, spritebatch);
    }
  }
}
