// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Confirmation.DestroyPenConfirmationPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuldMenu.Confirmation
{
  internal class DestroyPenConfirmationPopUp
  {
    private ConfirmationDialog dialog;
    private PrisonZone refPrisonZone;

    public DestroyPenConfirmationPopUp(PrisonZone prisonzone)
    {
      this.refPrisonZone = prisonzone;
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      string bodytext = "Are you sure you want to destroy this pen?~You will lose any animals or items placed inside.";
      if (prisonzone.IsFarm)
        bodytext = "Are you sure you want to this farm?~You will lose any crops currently growing inside.";
      this.dialog = new ConfirmationDialog("Destroy Pen?", bodytext, baseScaleForUi, customTextX_raw: 300f);
      this.dialog.location = new Vector2(512f, 384f);
    }

    public bool UpdateDestroyPenConfirmation(Player player, float DeltaTime)
    {
      bool confirmed;
      if (!this.dialog.UpdateConfirmationDialog(player, Vector2.Zero, DeltaTime, out confirmed))
        return false;
      if (confirmed)
      {
        LayoutEntry thisDungeonTile = player.prisonlayout.GetThisDungeonTile(this.refPrisonZone.GetTopLeft().X, this.refPrisonZone.GetTopLeft().Y);
        SellUIManager.DestroyEnclosure(this.refPrisonZone, player, OverWorldManager.overworldenvironment, thisDungeonTile);
        OverWorldManager.overworldstate = OverWOrldState.MainMenu;
      }
      return true;
    }

    public void DrawDestroyPenConfirmation() => this.dialog.DrawConfirmationDialog(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}
