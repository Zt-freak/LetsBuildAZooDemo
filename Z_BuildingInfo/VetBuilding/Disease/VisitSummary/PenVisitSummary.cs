// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VisitSummary.PenVisitSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VisitSummary
{
  internal class PenVisitSummary
  {
    private BigBrownPanel bigBrownPanel;
    private Vector2 Position;
    private LerpHandler_Float lerper;
    private VetCoverageSummary vetvisitation;

    public PenVisitSummary(float BaseScale)
    {
      this.Position = new Vector2(512f, 300f);
      this.lerper = new LerpHandler_Float();
      this.bigBrownPanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Vet Coverage", BaseScale);
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.vetvisitation = new VetCoverageSummary(BaseScale);
      this.bigBrownPanel.Finalize(this.vetvisitation.GetSize());
    }

    public bool CheckMouseOver(Player player) => this.bigBrownPanel.CheckMouseOver(player, this.Position);

    public bool UpdatePenVisitSummary(Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      return (double) this.lerper.Value == 0.0 && this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, this.Position);
    }

    public void DrawPenVisitSummary(SpriteBatch spritebatch)
    {
      this.bigBrownPanel.DrawBigBrownPanel(this.Position, spritebatch);
      this.vetvisitation.DrawVetCoverageSummary(this.Position, spritebatch);
    }
  }
}
