// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ManageActive.CRISPR_ManageActivePanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedScreen;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer;

namespace TinyZoo.Z_CRISPR.ManageActive
{
  internal class CRISPR_ManageActivePanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private LerpHandler_Float lerper;
    private CRISPR_GenomeAndBaby genomeAndBaby;
    private CRISPR_StatusAndAction statusAndAction;
    private CrisprActiveBreed ref_breed;
    private bool WasReadyToCollect;
    private NewThingPanel newThingPanel;

    public CRISPR_ManageActivePanel(CrisprActiveBreed breed, float BaseScale, Player player)
    {
      this.ref_breed = breed;
      float num1 = 0.0f;
      float num2 = 10f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num3 = 300f * BaseScale;
      this.WasReadyToCollect = breed.IsBorn_CanCollect;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "CRISPR Status", BaseScale);
      this.genomeAndBaby = new CRISPR_GenomeAndBaby(breed, BaseScale, num3);
      this.genomeAndBaby.location.Y = num1 + this.genomeAndBaby.GetSize().Y * 0.5f;
      float num4 = num1 + this.genomeAndBaby.GetSize().Y + num2;
      this.statusAndAction = new CRISPR_StatusAndAction(breed, BaseScale, num3, player);
      this.statusAndAction.location.Y = num4;
      this.statusAndAction.location.Y += this.statusAndAction.GetHeight() * 0.5f;
      float y = num4 + this.statusAndAction.GetHeight();
      this.bigBrownPanel.Finalize(new Vector2(num3, y));
      Vector2 vector2 = -this.bigBrownPanel.vScale * 0.5f + this.bigBrownPanel.GetEdgeBuffer() * BaseScale * Sengine.ScreenRatioUpwardsMultiplier - this.bigBrownPanel.InternalOffset;
      this.genomeAndBaby.location.Y += vector2.Y;
      this.statusAndAction.location.Y += vector2.Y;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public CrisprActiveBreed GetBreedForThisPanel() => this.ref_breed;

    public bool CheckMouseOver(Player player) => this.bigBrownPanel.CheckMouseOver(player, this.location);

    public bool UpdateCRISPR_ManageActivePanel(
      Player player,
      float DeltaTime,
      out bool Cancel,
      out bool throwBabyOut,
      out bool isSell,
      out bool isPutInPen)
    {
      Vector2 location = this.location;
      Cancel = false;
      throwBabyOut = false;
      isSell = false;
      isPutInPen = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.genomeAndBaby.UpdateCRISPR_GenomeAndBaby(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
          Cancel = true;
        if (this.statusAndAction.UpdateCRISPR_StatusAndAction(player, DeltaTime, location, out throwBabyOut, out isSell, out isPutInPen))
        {
          if (!(this.statusAndAction.isNewCollection & isPutInPen))
            return true;
          this.CreateNewThingPanel();
        }
        if (this.newThingPanel != null)
        {
          this.bigBrownPanel.Active = false;
          if (this.newThingPanel.UpdateNewThingPanel(player, DeltaTime, location))
          {
            isPutInPen = true;
            return true;
          }
        }
      }
      if (!this.WasReadyToCollect && this.ref_breed.IsBorn_CanCollect)
      {
        this.statusAndAction.Create(this.ref_breed, player);
        this.WasReadyToCollect = true;
      }
      return false;
    }

    private void CreateNewThingPanel()
    {
      this.newThingPanel = new NewThingPanel(new List<AnimalRenderDescriptor>()
      {
        new AnimalRenderDescriptor(this.ref_breed.resultBody, this.ref_breed.resultBodyVariant, this.ref_breed.resultHead, this.ref_breed.resultHeadVariant, _IsFemale: (!this.ref_breed.isBoy))
      }, true);
      this.newThingPanel.LerpIn();
    }

    public void DrawCRISPR_ManageActivePanel(SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      Vector2 location = this.location;
      location.X += this.lerper.Value * BreedPopUp.LerpDistance;
      this.bigBrownPanel.DrawBigBrownPanel(location, spriteBatch);
      this.genomeAndBaby.DrawCRISPR_GenomeAndBaby(location, spriteBatch);
      this.statusAndAction.DrawCRISPR_StatusAndAction(location, spriteBatch);
      this.bigBrownPanel.DrawDarkOverlay(location, spriteBatch);
      if (this.newThingPanel == null)
        return;
      this.newThingPanel.DrawNewThingPanel(spriteBatch, location);
    }
  }
}
