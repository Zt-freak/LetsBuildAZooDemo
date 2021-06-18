// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.Column6_Restock
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class Column6_Restock
  {
    public Vector2 Location;
    private LittleSummaryButton littlebutton;
    private TopTextMini toptextmini;
    private TopTextMini MidText;

    public Column6_Restock(
      float HeightForText,
      float Stock,
      float BaseScale,
      float HeightForMiddleText,
      int ShortestDays)
    {
      this.littlebutton = new LittleSummaryButton(LittleSummaryButtonType.Restock);
      this.toptextmini = new TopTextMini("On Order:", BaseScale, HeightForText);
      string str = string.Concat((object) Math.Round((double) Stock));
      if ((double) Stock == 0.0)
      {
        this.MidText = new TopTextMini("--", BaseScale, HeightForMiddleText);
      }
      else
      {
        this.MidText = new TopTextMini(str + " ", BaseScale, HeightForMiddleText);
        this.MidText.SetNewText(str + " units " + (object) ShortestDays + " days");
        this.MidText.SetAsSplit(2);
      }
    }

    public bool UpdateColumn6_Restock(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      return false;
    }

    public void DrawColumn6_Restock(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.MidText.DrawTopTextMini(Offset);
    }
  }
}
