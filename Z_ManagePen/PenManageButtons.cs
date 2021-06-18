// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.PenManageButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_Manage.MainButtons;
using TinyZoo.Z_ManagePen.Buttons;

namespace TinyZoo.Z_ManagePen
{
  internal class PenManageButtons
  {
    private mainButtonsManager mainbuttons;
    private List<Z_ManageButton> managebuttons;

    public PenManageButtons(Player player)
    {
      this.managebuttons = new List<Z_ManageButton>();
      this.managebuttons.Add(new Z_ManageButton(ManageButtonType.Feed));
      this.managebuttons.Add(new Z_ManageButton(ManageButtonType.CleanPen));
      this.managebuttons.Add(new Z_ManageButton(ManageButtonType.PenSummary));
      this.managebuttons.Add(new Z_ManageButton(ManageButtonType.FoodChain));
      for (int index = 0; index < this.managebuttons.Count; ++index)
        this.managebuttons[index].Location = new Vector2(130f, (float) (150 + index * 100));
    }

    public ManageButtonType UpdatePenManageButtons(
      Player player,
      float DeltaTime,
      Vector2 Offset)
    {
      ManageButtonType manageButtonType = ManageButtonType.Count;
      for (int index = 0; index < this.managebuttons.Count; ++index)
      {
        if (this.managebuttons[index].UpdateManageButtons(player, Vector2.Zero))
          manageButtonType = this.managebuttons[index].buttontype;
      }
      return manageButtonType;
    }

    public void DrawPenManageButtons(Vector2 Offset)
    {
      for (int index = 0; index < this.managebuttons.Count; ++index)
        this.managebuttons[index].DrawManageButtons(Offset, AssetContainer.pointspritebatch03);
    }
  }
}
