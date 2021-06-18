// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.LedgerDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class LedgerDisplay
  {
    private SimpleTextHandler text;

    public LedgerDisplay(SimPerson simperson)
    {
      string TextToWrite = "CASH HELD: " + (object) simperson.memberofthepublic.CashHeld + "~CASH SPENT:" + (object) (simperson.memberofthepublic.StartingCash - simperson.memberofthepublic.CashHeld) + "~~Purchases:";
      for (int index = 0; index < simperson.memberofthepublic.purchaseledger.purchaseledgers.Count; ++index)
        TextToWrite = TextToWrite + "~" + FoodIconData.GetFoodTypeToString(simperson.memberofthepublic.purchaseledger.purchaseledgers[index].Thingpurchased) + " $" + (object) simperson.memberofthepublic.purchaseledger.purchaseledgers[index].Cost + "~";
      this.text = new SimpleTextHandler(TextToWrite, false, 0.4f, RenderMath.GetPixelSizeBestMatch(GameFlags.GetSmallTextScale()), false, false);
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
    }

    public void UpdateLedgerDisplay(float DeltaTime)
    {
    }

    public void DrawLedgerDisplay(Vector2 Offset) => this.text.DrawSimpleTextHandler(Offset + new Vector2(0.0f, 150f), 1f, AssetContainer.pointspritebatchTop05);
  }
}
