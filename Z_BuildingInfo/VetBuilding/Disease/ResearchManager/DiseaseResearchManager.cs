// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.DiseaseResearchManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DiseaseDetailView;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager
{
  internal class DiseaseResearchManager
  {
    private BigBrownPanel bigBrownPanel;
    private Vector2 Position;
    private LerpHandler_Float lerper;
    private float BaseScale;
    private CurrentDiseaseList currentdiseases;
    private DiseaseDetailViewManager diseasedetailview;

    public DiseaseResearchManager(float _BaseScale, Player player)
    {
      this.BaseScale = _BaseScale;
      this.Position = new Vector2(512f, 300f);
      this.lerper = new LerpHandler_Float();
      this.bigBrownPanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Disease Research", this.BaseScale, true);
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.currentdiseases = new CurrentDiseaseList(player, this.BaseScale);
      this.bigBrownPanel.Finalize(this.currentdiseases.GetSize());
      this.bigBrownPanel.HasPreviousButton = false;
    }

    public bool CheckMouseOver(Player player) => this.bigBrownPanel.CheckMouseOver(player, this.Position);

    public bool UpdateDiseaseResearchManager(Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.currentdiseases != null && this.currentdiseases.UpdateCurrentDiseaseList(this.Position, player, DeltaTime))
      {
        this.diseasedetailview = new DiseaseDetailViewManager(this.currentdiseases.GetSelectedDisease(), this.BaseScale, player);
        this.currentdiseases = (CurrentDiseaseList) null;
        this.bigBrownPanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Desease Research", this.BaseScale, true);
        this.bigBrownPanel.Finalize(this.diseasedetailview.GetSize());
        this.bigBrownPanel.HasPreviousButton = true;
      }
      if (this.diseasedetailview != null)
        this.diseasedetailview.UpdateDiseaseDetailViewManager(player, this.Position, DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, this.Position))
          return true;
        if (this.bigBrownPanel.HasPreviousButton && this.bigBrownPanel.UpdatePanelpreviousButton(player, DeltaTime, this.Position) && this.diseasedetailview != null)
        {
          this.diseasedetailview = (DiseaseDetailViewManager) null;
          this.currentdiseases = new CurrentDiseaseList(player, this.BaseScale);
          this.bigBrownPanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Desease Research", this.BaseScale, true);
          this.bigBrownPanel.Finalize(this.currentdiseases.GetSize());
          this.bigBrownPanel.HasPreviousButton = false;
        }
      }
      return false;
    }

    public void DrawDiseaseResearchManager(SpriteBatch spritebatch)
    {
      this.bigBrownPanel.DrawBigBrownPanel(this.Position, spritebatch);
      if (this.currentdiseases != null)
        this.currentdiseases.DrawCurrentDiseaseList(this.Position, spritebatch);
      if (this.diseasedetailview == null)
        return;
      this.diseasedetailview.DrawDiseaseDetailViewManager(this.Position, spritebatch);
    }
  }
}
