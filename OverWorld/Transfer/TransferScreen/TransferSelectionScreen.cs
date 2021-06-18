// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Transfer.TransferScreen.TransferSelectionScreen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.HoldingCells;

namespace TinyZoo.OverWorld.Transfer.TransferScreen
{
  internal class TransferSelectionScreen
  {
    private StoreBGManager storeBG;
    private LerpHandler_Float lerper;
    private BackButton back;
    private bool Exiting;
    private PrisonerAndButtons prisonerandbuttons;
    private CharacterTextBox charactertextbox;
    private bool WillGoToGoToCellSelectFromTransferOnExit;
    private PrisonZone zone;
    private HoldingCellInfo holdingcell;

    public TransferSelectionScreen(
      PrisonZone _zone,
      HoldingCellInfo _holdingcell,
      Player player,
      bool IsReanimate,
      Vector2Int location)
    {
      this.zone = _zone;
      this.holdingcell = _holdingcell;
      this.WillGoToGoToCellSelectFromTransferOnExit = false;
      FeatureFlags.DemolishEnabled = false;
      this.storeBG = new StoreBGManager(this.holdingcell == null);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.back = new BackButton();
      this.prisonerandbuttons = new PrisonerAndButtons(this.zone, this.holdingcell, player);
      if (this.holdingcell == null)
        this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "Transfer people from this cellblock in to a holding cell?~This is the only way to move prisoners. After transfering them, select another cell block to move them to a new cell block.");
      else
        this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "The prisoners in the holding cells must be moved to a cellblock to start earning you income. Select the prisoners you wish to move in to a cell block together.");
    }

    public bool UpdateTransferSelectionScreen(
      float DeltaTime,
      Player player,
      ref bool GoToCellSelectFromTransfer)
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if ((double) TinyZoo.Game1.screenfade.fTargetAlpha != 1.0)
      {
        if (!this.Exiting)
        {
          int num = this.prisonerandbuttons.UpdatePrisonerAndButtons(DeltaTime, player, Offset, ref this.WillGoToGoToCellSelectFromTransferOnExit) ? 1 : 0;
          if (this.WillGoToGoToCellSelectFromTransferOnExit)
            this.Exit();
          if (num != 0)
          {
            if (this.zone != null && this.zone.prisonercontainer.prisoners.Count == 0)
              this.Exit();
            else if (this.holdingcell != null && this.holdingcell.prisonercontainer.prisoners.Count == 0)
              this.Exit();
            this.prisonerandbuttons = new PrisonerAndButtons(this.zone, this.holdingcell, player);
          }
        }
        if ((double) this.lerper.Value == 0.0 && this.back.UpdateBackButton(player, DeltaTime))
          this.Exit();
      }
      if (!this.Exiting || (double) this.lerper.Value != 1.0)
        return false;
      if (this.WillGoToGoToCellSelectFromTransferOnExit)
        GoToCellSelectFromTransfer = true;
      return true;
    }

    private void Exit()
    {
      if (this.Exiting)
        return;
      this.lerper.SetLerp(false, 1f, 1f, 3f, true);
      this.Exiting = true;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
    }

    public void DrawTransferSelectionScreen()
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.storeBG.DrawStoreBGManager(Offset);
      this.prisonerandbuttons.DrawPrisonerAndButtons(Offset);
      this.charactertextbox.DrawCharacterTextBox(new Vector2(512f, 150f) + Offset, AssetContainer.pointspritebatchTop05);
      this.back.DrawBackButton(Offset);
    }
  }
}
