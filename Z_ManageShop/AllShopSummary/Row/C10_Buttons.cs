// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.AllShopSummary.Row.C10_Buttons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_ManageShop.AllShopSummary.Row
{
  internal class C10_Buttons
  {
    private TopTextMini toptextmini;
    private LittleSummaryButton locate;
    private LittleSummaryButton manage;
    public Vector2 Location;
    public bool PressedLocate;

    public C10_Buttons(float HeightForText, float BaseScale)
    {
      this.toptextmini = new TopTextMini("", BaseScale, HeightForText, false, true);
      this.toptextmini.CenterJustify = true;
      this.locate = new LittleSummaryButton(LittleSummaryButtonType.Locate);
      this.manage = new LittleSummaryButton(LittleSummaryButtonType.Manage);
      this.manage.vLocation.X = 40f;
    }

    public bool UpdateC10_Buttons(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      bool flag = false;
      if (this.manage.UpdateLittleSummaryButton(DeltaTime, player, Offset))
      {
        this.PressedLocate = false;
        flag = true;
      }
      else if (this.locate.UpdateLittleSummaryButton(DeltaTime, player, Offset))
      {
        this.PressedLocate = true;
        flag = true;
      }
      return flag;
    }

    public void DrawColumn(Vector2 Offset)
    {
      Offset += this.Location;
      this.manage.DrawLittleSummaryButton(Offset);
      this.locate.DrawLittleSummaryButton(Offset);
    }
  }
}
