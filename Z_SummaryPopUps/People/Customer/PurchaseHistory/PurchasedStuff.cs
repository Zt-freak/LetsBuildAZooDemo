// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.PurchaseHistory.PurchasedStuff
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.PurchaseHistory
{
  internal class PurchasedStuff
  {
    public CustomerFrame customerframe;
    public Vector2 Location;
    private SimpleTextHandler text;
    private MiniHeading miniheading;
    private GameObject CLRRR;
    private string TopText;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 textpos;
    private Vector2 framescale;

    public PurchasedStuff(MemberOfThePublic memberofthepublic, float basescale_, float ForcedWidth)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.TopText = "CASH HELD: $" + MoneyRenderer.FormatCentsToDollars(memberofthepublic.CashHeld);
      this.TopText = this.TopText + "   /   CASH SPENT: $" + MoneyRenderer.FormatCentsToDollars(memberofthepublic.StartingCash - memberofthepublic.CashHeld);
      string TextToWrite = "Purchases:";
      for (int index = 0; index < memberofthepublic.purchaseledger.purchaseledgers.Count; ++index)
      {
        string str = "$" + MoneyRenderer.FormatCentsToDollars(memberofthepublic.purchaseledger.purchaseledgers[index].Cost);
        TextToWrite = TextToWrite + "~" + FoodIconData.GetFoodTypeToString(memberofthepublic.purchaseledger.purchaseledgers[index].Thingpurchased) + " " + str;
      }
      this.framescale = new Vector2(ForcedWidth, this.uiscale.ScaleY(120f));
      this.customerframe = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      this.text = new SimpleTextHandler(TextToWrite, false, 0.5f, this.basescale, false, false);
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      this.CLRRR = new GameObject();
      this.CLRRR.SetAllColours(ColourData.Z_Cream);
      this.miniheading = new MiniHeading(Vector2.Zero, this.TopText ?? "", 1f, this.basescale);
      this.miniheading.SetTextPosition(this.customerframe.VSCale);
      this.textpos = new Vector2();
      this.textpos.X = this.customerframe.VSCale.X * -0.5f + this.uiscale.GetDefaultXBuffer();
      this.textpos.Y = (float) ((double) this.customerframe.VSCale.Y * -0.5 + 2.0 * (double) this.uiscale.GetDefaultYBuffer());
      this.textpos.Y += this.miniheading.GetTextHeight(true);
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void DrawPurchasedStuff(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.miniheading.DrawMiniHeading(Offset);
      this.text.DrawSimpleTextHandler(Offset + this.textpos, 1f, spritebatch);
    }
  }
}
