// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.ActivePairManage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.AbortConfirm;
using TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing;
using TinyZoo.Z_BreedScreen.ActiveBreedPairManage.ReturnToPen;
using TinyZoo.Z_BreedScreen.ConfirmBreed;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage
{
  internal class ActivePairManage
  {
    public Vector2 Location;
    public BigBrownPanel bigBrownPanel;
    private ParentsAndOffspringDisplay parentsAndOffspringDisplay;
    private SuccesRate successrate;
    private PotentialBabies potentialbabies;
    private PredictionTable predictiontable;
    private AdjustBreedingVariablesFrame adjustVariablesFrame;
    private ReturnToPenmanager returntopen;
    private NursingManager nursingmanager;
    private LerpHandler_Float lerper;
    public Parents_AndChild REF_parents_and_child;
    private bool VariablesChanged;

    public ActivePairManage(
      Parents_AndChild parents_and_child,
      ActiveBreed breed,
      Player player,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = 0.0f;
      float num2 = 0.0f;
      this.REF_parents_and_child = parents_and_child;
      this.parentsAndOffspringDisplay = new ParentsAndOffspringDisplay(parents_and_child, BaseScale);
      this.parentsAndOffspringDisplay.location.Y = num1 + this.parentsAndOffspringDisplay.customerFrame.VSCale.Y * 0.5f;
      float num3 = num1 + this.parentsAndOffspringDisplay.customerFrame.VSCale.Y;
      float x = num2 + this.parentsAndOffspringDisplay.customerFrame.VSCale.X;
      float num4 = num3 + defaultYbuffer;
      if (parents_and_child.Attempts > 0)
      {
        this.successrate = new SuccesRate(parents_and_child, this.parentsAndOffspringDisplay.customerFrame.VSCale.X, BaseScale);
        this.successrate.Location.Y = num4 + this.successrate.customerFrame.VSCale.Y * 0.5f;
        num4 = num4 + this.successrate.customerFrame.VSCale.Y + defaultYbuffer;
      }
      this.adjustVariablesFrame = new AdjustBreedingVariablesFrame(parents_and_child, this.parentsAndOffspringDisplay.customerFrame.VSCale.X, BaseScale);
      this.adjustVariablesFrame.location.Y = num4 + this.adjustVariablesFrame.customerFrame.VSCale.Y * 0.5f;
      float num5 = num4 + this.adjustVariablesFrame.customerFrame.VSCale.Y;
      float y;
      if (parents_and_child.HeldBaby != null)
      {
        float num6 = num5 + defaultYbuffer;
        this.nursingmanager = new NursingManager(parents_and_child.HeldBaby, player, parents_and_child, this.parentsAndOffspringDisplay.customerFrame.VSCale.X, breed, BaseScale);
        this.nursingmanager.location.Y = num6 + this.nursingmanager.GetHeight() * 0.5f;
        y = num6 + this.nursingmanager.GetHeight();
      }
      else if (parents_and_child.HeldBaby == null && breed == null)
      {
        float num6 = num5 + defaultYbuffer;
        this.returntopen = new ReturnToPenmanager(player, this.parentsAndOffspringDisplay.customerFrame.VSCale.X, breed, BaseScale);
        this.returntopen.Location.Y = num6 + this.returntopen.GetHeight() * 0.5f;
        y = num6 + this.returntopen.GetHeight();
      }
      else
      {
        PrisonerInfo thisNotInPenAnimal = player.prisonlayout.GetThisNotInPenAnimal(parents_and_child.FemaleUID);
        float num6 = num5 + defaultYbuffer;
        this.predictiontable = new PredictionTable(PredictionTableType.Abortion, player, breed, thisNotInPenAnimal, this.parentsAndOffspringDisplay.customerFrame.VSCale.X, BaseScale);
        this.predictiontable.Location.Y = num6 + this.predictiontable.customerframe.VSCale.Y * 0.5f;
        y = num6 + this.predictiontable.customerframe.VSCale.Y;
      }
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Breeding Status", BaseScale);
      this.bigBrownPanel.Finalize(new Vector2(x, y));
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.parentsAndOffspringDisplay.location.Y += frameOffsetFromTop.Y;
      if (this.nursingmanager != null)
        this.nursingmanager.location.Y += frameOffsetFromTop.Y;
      if (this.returntopen != null)
        this.returntopen.Location.Y += frameOffsetFromTop.Y;
      if (this.predictiontable != null)
        this.predictiontable.Location.Y += frameOffsetFromTop.Y;
      if (this.successrate != null)
        this.successrate.Location.Y += frameOffsetFromTop.Y;
      this.adjustVariablesFrame.location.Y += frameOffsetFromTop.Y;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn()
    {
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.VariablesChanged = false;
    }

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      offset += this.Location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateActivePairManage(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      ref AbortConfirmationManager abortconfirm,
      out bool ReturnAnimalsToPen,
      out bool SkipNursing,
      out bool variablesChanged)
    {
      ReturnAnimalsToPen = false;
      SkipNursing = false;
      variablesChanged = this.VariablesChanged;
      Offset += this.Location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0 && (abortconfirm == null || abortconfirm != null & !abortconfirm.BlockUnderPanelUpdate()))
      {
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, Offset))
          return true;
        if (this.adjustVariablesFrame.UpdateAdjustBreedingVariablesFrame(player, DeltaTime, Offset))
          this.VariablesChanged = true;
        if (this.predictiontable != null)
        {
          if (this.predictiontable.UpdatePredictionTable(Offset, player, DeltaTime))
          {
            abortconfirm = new AbortConfirmationManager(this.REF_parents_and_child);
            abortconfirm.Location = new Vector2(512f, 384f);
          }
        }
        else if (this.returntopen != null)
        {
          if (this.returntopen.UpdateReturnToPenmanager(player, DeltaTime, Offset))
          {
            ReturnAnimalsToPen = true;
            return true;
          }
        }
        else if (this.nursingmanager != null && this.nursingmanager.UpdateNursingManager(Offset, player, DeltaTime))
        {
          SkipNursing = true;
          return true;
        }
      }
      return false;
    }

    public void DrawActivePairManage(Vector2 Offset, SpriteBatch spritebatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      Offset += this.Location;
      Offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      this.bigBrownPanel.DrawBigBrownPanel(Offset, spritebatch);
      this.parentsAndOffspringDisplay.DrawParentsAndOffspringDisplay(Offset, spritebatch);
      if (this.successrate != null)
        this.successrate.DrawSuccesRate(Offset, spritebatch);
      this.adjustVariablesFrame.DrawAdjustBreedingVariablesFrame(Offset, spritebatch);
      if (this.predictiontable != null)
        this.predictiontable.DrawPredictionTable(Offset, spritebatch);
      if (this.returntopen != null)
        this.returntopen.DrawReturnToPenmanager(spritebatch, Offset);
      if (this.nursingmanager == null)
        return;
      this.nursingmanager.DrawNursingManager(Offset, spritebatch);
    }
  }
}
