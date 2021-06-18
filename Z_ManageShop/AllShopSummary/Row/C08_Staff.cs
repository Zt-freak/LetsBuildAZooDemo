// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C08_Staff
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C08_Staff
  {
    private TopTextMini toptextmini;
    private TopTextMini toptextminiTwo;
    public Vector2 Location;

    public C08_Staff(float HeightForText, float MidTextHeight, float BaseScale)
    {
      this.toptextmini = new TopTextMini("Staff", BaseScale, HeightForText, false, true);
      this.toptextmini.CenterJustify = true;
      this.toptextminiTwo = new TopTextMini("x1", BaseScale, MidTextHeight);
      this.toptextminiTwo.CenterJustify = true;
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.toptextmini.DrawTopTextMini(Offset);
      this.toptextminiTwo.DrawTopTextMini(Offset);
    }
  }
}
