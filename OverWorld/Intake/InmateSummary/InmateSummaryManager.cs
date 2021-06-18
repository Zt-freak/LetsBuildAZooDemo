// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.InmateSummary.InmateSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.OverWorld.Intake.InmateSummary
{
  internal class InmateSummaryManager
  {
    private LerpHandler_Float lerper;
    public int SelectedEntrytype;
    public int Next_entrytype;
    private PrisonersPanel entrypanel;

    public InmateSummaryManager()
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 1f, 3f);
    }

    public void SelectedNewIcon(int Index, bool Instant, Player player)
    {
      this.Next_entrytype = Index;
      if ((double) this.lerper.TargetValue != 1.0)
        this.lerper.SetLerp(false, 0.0f, 1f, 3.5f, true);
      if (!Instant)
        return;
      this.lerper.SetLerp(false, 0.0f, 0.0f, 3.5f, true);
      this.CreateNewPanel(player);
    }

    private void CreateNewPanel(Player player)
    {
      this.SelectedEntrytype = this.Next_entrytype;
      this.entrypanel = new PrisonersPanel(player.intakes.intakeinfos[this.SelectedEntrytype], player);
      this.entrypanel.Location = new Vector2(660f, (float) (400.0 + 130.0 * (double) Sengine.ScreenRationReductionMultiplier.Y));
      this.Next_entrytype = -1;
    }

    public void UpdateInmateSummaryManager(
      float DeltaTime,
      Player player,
      out bool GoToCellBlockSelect)
    {
      GoToCellBlockSelect = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 1.0 && this.Next_entrytype > -1)
      {
        this.CreateNewPanel(player);
        this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      }
      if (this.entrypanel == null)
        return;
      this.entrypanel.UpdatePrisonersPanel(DeltaTime, player, (double) this.lerper.Value == 0.0, out GoToCellBlockSelect);
    }

    public void DrawInmateSummaryManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      if ((double) this.lerper.Value >= 1.0)
        return;
      this.entrypanel.DrawEntryDetailPanel(new Vector2(this.lerper.Value * 912f, 0.0f) + Offset, spritebatch);
    }
  }
}
