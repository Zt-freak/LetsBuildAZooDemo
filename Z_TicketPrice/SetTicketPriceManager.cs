// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.SetTicketPriceManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Tutorials;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_TicketPrice
{
  internal class SetTicketPriceManager
  {
    private StoreBGManager storeBG;
    private BackButton close;
    private LerpHandler_Float lerper;
    private PriceAdjusterSet priceadjuster;
    private ScreenHeading heading;
    private bool ChangedPrice;
    private int StartCost;
    private TextButton AutoButton;
    private ScreenHeading screenhead;
    private int IdealCost;
    private SimpleTextBox simpletextbox;
    private SmartCharacterBox charactertextbox;
    private int LastSet = -1;
    private bool Exiting;
    private GameObject XecondaryRtingCL;
    public Vector2 Location;
    private string ExpenseString;

    public SetTicketPriceManager(Player player)
    {
      this.Exiting = false;
      this.ExpenseString = "";
      this.XecondaryRtingCL = new GameObject();
      this.heading = new ScreenHeading("TICKET PRICE", 90f);
      this.storeBG = new StoreBGManager(IsBlue: true);
      this.IdealCost = (int) AnimalTicketValue.GetIdealParkTicketCost(player);
      this.close = new BackButton();
      this.close.LerpOn();
      this.lerper = new LerpHandler_Float();
      if (TinyZoo.Game1.gamestate != GAMESTATE.TicketPrice)
        this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.priceadjuster = new PriceAdjusterSet(1, this.IdealCost >= 125 ? (this.IdealCost >= 250 ? (this.IdealCost >= 500 ? (this.IdealCost >= 1250 ? (this.IdealCost >= 2500 ? 10000 : 5000) : 2500) : 1000) : 500) : 1000, player.Stats.GetTicketCost());
      this.ChangedPrice = false;
      this.StartCost = player.Stats.GetTicketCost();
      this.priceadjuster.priceaduster.Location.Y = 700f;
      this.SetSTring(player);
    }

    private void SetSTring(Player player)
    {
      int idealCost = this.IdealCost;
      Expensiveness ticketExpensivenss = AnimalTicketValue.GetTicketExpensivenss(player, this.StartCost);
      if (this.IdealCost == 0)
      {
        if (this.LastSet == 0)
          return;
        this.LastSet = 0;
        string str = "Your recomended ticket price is $0! Get some animals or attractions to increase the value.";
        this.simpletextbox = new SimpleTextBox(str, 600f, false);
        this.simpletextbox.SetTextBoxToALternateColour(BTNColour.Cream);
        this.simpletextbox.text.AutoCompleteParagraph();
        this.ExpenseString = "Get Animals!";
        this.screenhead = new ScreenHeading("Very High");
        this.XecondaryRtingCL.SetAllColours(ColourData.Z_TextRed);
        this.screenhead.header.SetAsButtonFrame(BTNColour.Red);
        this.screenhead.header.vLocation = new Vector2(512f, 200f);
        this.charactertextbox = new SmartCharacterBox(str, AnimalType.Administrator, ShortenForCloseButton: true);
        this.charactertextbox.ForceEndLerp();
      }
      else
      {
        BTNColour btnclr = BTNColour.Red;
        this.ExpenseString = "Very Low";
        string ThisHeading = "Very Low";
        int num = 1;
        this.XecondaryRtingCL.SetAllColours(ColourData.Z_TextRed);
        string str;
        switch (ticketExpensivenss)
        {
          case Expensiveness.TooCheap:
            str = "Your Ticket price is very low,  more people will come to your zoo, but you could be making more money!~Consider increasing the cost.";
            break;
          case Expensiveness.SlightlyTooCheap:
            this.ExpenseString = "Slightly Low";
            this.XecondaryRtingCL.SetAllColours(ColourData.Z_TextOrange);
            ThisHeading = "Slightly Low";
            btnclr = BTNColour.PaleYellow;
            num = 3;
            str = "Your Ticket price is slightly low, while you could make more money, this will make your customers happy!";
            break;
          case Expensiveness.SLightlyTooExpensive:
            this.ExpenseString = "Slightly High";
            ThisHeading = "Slightly High";
            btnclr = BTNColour.PaleYellow;
            this.XecondaryRtingCL.SetAllColours(ColourData.Z_TextBrown);
            num = 4;
            str = "Your Ticket price is a little high. This might make your customers a little disappointed, but this might be a shrewd business move!";
            break;
          case Expensiveness.TooExpensive:
            this.ExpenseString = "Very High";
            ThisHeading = "Very High";
            this.XecondaryRtingCL.SetAllColours(ColourData.Z_TextRed);
            num = 2;
            str = "Your Ticket price is quite high! Less people will come to your zoo, but the people that come will earn you more money. Be careful, this might affect your zoo's customer ratings on ZooReview.com!~Consider reducing the cost.";
            break;
          default:
            this.ExpenseString = "About Right";
            ThisHeading = "About Right";
            btnclr = BTNColour.Green;
            num = 5;
            this.XecondaryRtingCL.SetAllColours(ColourData.Z_TextGreen);
            str = "Your ticket cost, is roughly in line with the global average.";
            break;
        }
        if (num == this.LastSet)
          return;
        this.screenhead = new ScreenHeading(ThisHeading);
        this.screenhead.header.SetAsButtonFrame(btnclr);
        this.charactertextbox = new SmartCharacterBox(str, AnimalType.Administrator, ShortenForCloseButton: true);
        this.charactertextbox.ForceEndLerp();
        this.LastSet = num;
        this.simpletextbox = new SimpleTextBox(str, 600f, false);
        this.simpletextbox.SetTextBoxToALternateColour(BTNColour.Cream);
        this.simpletextbox.text.AutoCompleteParagraph();
        this.screenhead.header.vLocation = new Vector2(512f, 200f);
      }
    }

    public void ForceExit() => this.Exiting = true;

    public bool UpdateSetTicketPriceManager(Player player, float DeltaTime, Vector2 Offset)
    {
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, DoNotClearInput: true);
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.priceadjuster.UpdatePriceAdjusterSet(player, DeltaTime, Offset, true))
      {
        player.Stats.SetTicketCost(this.priceadjuster.priceaduster.CurrentValue);
        this.StartCost = this.priceadjuster.priceaduster.CurrentValue;
        this.ChangedPrice = false;
      }
      if (this.StartCost != this.priceadjuster.priceaduster.CurrentValue)
      {
        this.StartCost = this.priceadjuster.priceaduster.CurrentValue;
        player.Stats.SetTicketCost(this.priceadjuster.priceaduster.CurrentValue);
        this.ChangedPrice = true;
        this.SetSTring(player);
      }
      return this.Exiting;
    }

    public void DrawSetTicketPriceManager(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      this.heading.DrawScreenHeading(Offset, AssetContainer.pointspritebatchTop05);
      this.priceadjuster.DrawPriceAdjusterSetCentral(Offset, "PRICE PER TICKET", this.ExpenseString, this.XecondaryRtingCL.GetColour());
      this.charactertextbox.DrawSmartCharacterBox(new Vector2(0.0f, 60f));
    }
  }
}
