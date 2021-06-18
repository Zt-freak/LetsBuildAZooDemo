// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.MedicalJournal.MedicalJournalManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.MedicalJournal
{
  internal class MedicalJournalManager
  {
    private BigBrownPanel BigBrownPanel;
    private Vector2 Position;
    private LerpHandler_Float lerper;
    private CurrentDiseaseList CurrentDiseaseList;

    public MedicalJournalManager(float BaseScale, Player player)
    {
      this.Position = new Vector2(512f, 300f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.BigBrownPanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Medical Journal", BaseScale);
      this.CurrentDiseaseList = new CurrentDiseaseList(player, BaseScale, true);
      this.BigBrownPanel.Finalize(this.CurrentDiseaseList.GetSize());
    }

    public bool CheckMouseOver(Player player) => this.BigBrownPanel.CheckMouseOver(player, this.Position);

    public bool UpdateMedicalJournalManager(Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      return (double) this.lerper.Value == 0.0 && this.BigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, this.Position);
    }

    public void DrawMedicalJournalManager(SpriteBatch spritebatch)
    {
      this.BigBrownPanel.DrawBigBrownPanel(this.Position, spritebatch);
      this.CurrentDiseaseList.DrawCurrentDiseaseList(this.Position, spritebatch);
    }
  }
}
