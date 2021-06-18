// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Viewer.AssignedBus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus.BussInfo.Viewer
{
  internal class AssignedBus
  {
    private GameObject Buss;
    private GameObject BusWheels;
    private CustomerFrame customerframe;
    public Vector2 Location;
    private GameObject TotalText;
    private int Total;
    private TextButton action;
    private bool IsBussesInGarage;
    private string BackUpString;
    private bool IsBuyBus;
    private bool EnabledButton;
    private int BusCost;
    private bool IsLockedAsNotResearched;

    public AssignedBus(
      Player player,
      BUSTYPE bus,
      int _Total,
      float BaseScale,
      bool _IsBussesInGarage,
      bool _IsBuyBus = false,
      bool IsBuyScreen = false,
      bool _IsSellBus = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.IsBuyBus = _IsBuyBus;
      this.IsBussesInGarage = _IsBussesInGarage;
      this.BackUpString = !this.IsBussesInGarage ? "No bus in garage" : "No buses in service";
      if (_IsBuyBus)
        this.BackUpString = "Capacity " + (object) BusData.GetBusCapacity(bus);
      else if (_IsSellBus)
        this.BackUpString = "Scrap Bus $0";
      this.IsBussesInGarage = _IsBussesInGarage;
      this.Total = _Total;
      this.Buss = new GameObject();
      Rectangle Wheels;
      this.Buss.DrawRect = BusData.GetBusRectangle(bus, out Wheels);
      this.Buss.scale = BaseScale;
      this.Buss.SetDrawOriginToCentre();
      this.Buss.vLocation.Y -= uiScaleHelper.ScaleY(25f);
      if (bus != BUSTYPE.DoubleDeckerBus_04)
        this.Buss.vLocation.Y += uiScaleHelper.ScaleY(8f);
      this.BusWheels = new GameObject(this.Buss);
      this.BusWheels.DrawRect = Wheels;
      this.customerframe = new CustomerFrame(uiScaleHelper.ScaleVector2(new Vector2(113f, 150f)), BaseScale: BaseScale);
      if (!Researcher.BusTypesReseacred[(int) bus])
      {
        this.IsLockedAsNotResearched = true;
        this.Buss.SetAllColours(0.0f, 0.0f, 0.0f);
        this.BusWheels.SetAllColours(0.0f, 0.0f, 0.0f);
        this.Buss.SetAlpha(0.3f);
        this.BusWheels.SetAlpha(0.3f);
      }
      this.TotalText = new GameObject();
      this.TotalText.scale = BaseScale;
      this.TotalText.SetAllColours(ColourData.Z_Cream);
      this.TotalText.vLocation = new Vector2(0.0f, uiScaleHelper.ScaleY(30f));
      string TextToDraw = "Remove";
      if (this.IsBussesInGarage)
        TextToDraw = "Use";
      if (_IsBuyBus)
        TextToDraw = "$" + (object) BusData.GetBusCost(bus);
      else if (_IsSellBus)
        TextToDraw = "Destroy";
      this.action = new TextButton(BaseScale, TextToDraw, 40f);
      this.action.vLocation = new Vector2(0.0f, uiScaleHelper.ScaleY(55f));
      this.EnabledButton = true;
      if (this.IsBuyBus)
      {
        this.BusCost = BusData.GetBusCost(bus);
        if (player.Stats.GetCashHeld() < this.BusCost)
        {
          this.EnabledButton = false;
          this.action.SetButtonColour(BTNColour.Grey);
        }
      }
      else if (_IsSellBus)
      {
        if (player.busroutes.GetBussesNotInUse()[(int) bus] == 0)
        {
          this.EnabledButton = false;
          this.action.SetButtonColour(BTNColour.Grey);
        }
      }
      else if (this.Total == 0)
      {
        this.EnabledButton = false;
        this.action.SetButtonColour(BTNColour.Grey);
      }
      if (this.IsBuyBus && this.IsLockedAsNotResearched)
      {
        this.action.SetButtonColour(BTNColour.Grey);
        this.action.SetText("Locked");
        this.action.DarkenAndDisable();
        this.EnabledButton = false;
      }
      else
      {
        if (this.IsBuyBus || !this.IsLockedAsNotResearched)
          return;
        this.EnabledButton = false;
        this.action.SetText("Locked");
        this.action.DarkenAndDisable();
      }
    }

    public void Disable()
    {
      if (this.action == null)
        return;
      this.action.DarkenAndDisable();
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool UpdateAssignedBus(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      if (this.IsBuyBus)
      {
        if (this.IsLockedAsNotResearched)
        {
          if (this.EnabledButton)
          {
            this.action.SetButtonColour(BTNColour.Grey);
            this.action.SetText("Locked");
            this.action.DarkenAndDisable();
            this.EnabledButton = false;
          }
        }
        else if (player.Stats.GetCashHeld() < this.BusCost)
        {
          if (this.EnabledButton)
          {
            this.EnabledButton = false;
            this.action.SetButtonColour(BTNColour.Grey);
          }
        }
        else if (!this.EnabledButton)
        {
          this.EnabledButton = true;
          this.action.SetButtonColour(BTNColour.Green);
        }
      }
      return this.EnabledButton && this.action.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawAssignedBus(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.BusWheels.Draw(spritebatch, AssetContainer.AnimalSheet, Offset);
      this.Buss.Draw(spritebatch, AssetContainer.AnimalSheet, Offset);
      if (this.Total == 0)
        TextFunctions.DrawJustifiedText(this.BackUpString, this.TotalText.scale, this.TotalText.vLocation + Offset, this.TotalText.GetColour(), this.TotalText.fAlpha, AssetContainer.springFont, spritebatch);
      else
        TextFunctions.DrawJustifiedText("X" + (object) this.Total, this.TotalText.scale, this.TotalText.vLocation + Offset, this.TotalText.GetColour(), this.TotalText.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
      this.action.DrawTextButton(Offset, 1f, spritebatch);
    }
  }
}
