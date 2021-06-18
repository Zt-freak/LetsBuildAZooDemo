// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.Z_ResearchManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Camera;
using System;
using TinyZoo.Z_Research_.IconGrid;
using TinyZoo.Z_Research_.ResearcherInfo;
using TinyZoo.Z_Research_.Selected;

namespace TinyZoo.Z_Research_
{
  internal class Z_ResearchManager
  {
    private R_IconGrid ricongrid;
    private float MinX;
    private float MaxX;
    private float MinY;
    private float MaxY;
    private SelectedRentryManager SelectionItem;
    private LerpHandler_Float camlerper;
    private bool IsLerping;
    private Vector2 LerpTarget;
    private Vector2 LerpStart;
    private ResearchPointsDisplay researchPointDisplay;

    public Z_ResearchManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.ricongrid = new R_IconGrid(ref this.MinX, ref this.MaxX, ref this.MinY, ref this.MaxY, player);
      this.researchPointDisplay = new ResearchPointsDisplay(player, baseScaleForUi);
      this.researchPointDisplay.location = new Vector2(7f, 17f);
      this.researchPointDisplay.location += this.researchPointDisplay.GetSize() * 0.5f;
      this.ricongrid.GetRingScrollLimit(out this.MinX, out this.MaxX, out this.MinY, out this.MaxY);
      Math.Round(((double) this.MaxX - (double) this.MinX) / (double) R_Icon.IconSpacing);
      Math.Round(((double) this.MaxY - (double) this.MinY) / (double) R_Icon.IconSpacing);
      Sengine.WorldOriginandScale = new Vector3((float) (((double) this.MinX + (double) this.MaxX) * 0.5), (float) (((double) this.MinY + (double) this.MaxY) * 0.5), RenderMath.GetNearestPerfectPixelZoom(Math.Min((float) (1024.0 / ((double) this.MaxX - (double) this.MinX + (double) R_Icon.IconSpacing)), (float) (768.0 * (double) Sengine.ScreenRationReductionMultiplier.Y / ((double) this.MaxY - (double) this.MinY + (double) R_Icon.IconSpacing)))));
      Sengine.WorldOriginandScale.Z = MathHelper.Clamp(Sengine.WorldOriginandScale.Z, RenderMath.GetNearestPerfectPixelZoom(1f) * 0.5f, RenderMath.GetNearestPerfectPixelZoom(2f));
      CameraManager.HardSetCameraPosition(Sengine.WorldOriginandScale);
    }

    public void UpdateReasearchView(Player player, float DeltaTime)
    {
      this.researchPointDisplay.UpdateResearchPointsDisplay(player, DeltaTime, Vector2.Zero);
      if ((double) player.inputmap.momentumwheel.MovementThisFrame != 0.0 || this.IsLerping || (player.player.touchinput.DragVectorThisFrame != Vector2.Zero || (double) player.inputmap.ZoomValue != 0.0))
      {
        if (this.IsLerping)
        {
          if (player.player.touchinput.DragVectorThisFrame != Vector2.Zero)
          {
            this.IsLerping = false;
            this.camlerper.SetLerp(true, 0.0f, 0.0f, 3f);
          }
          else
          {
            this.camlerper.UpdateLerpHandler(DeltaTime);
            Vector2 vector2 = (this.LerpTarget - this.LerpStart) * this.camlerper.Value + this.LerpStart;
            Sengine.WorldOriginandScale.X = vector2.X;
            Sengine.WorldOriginandScale.Y = vector2.Y;
            if ((double) this.camlerper.Value == 1.0)
            {
              Sengine.WorldOriginandScale.X = this.LerpTarget.X;
              Sengine.WorldOriginandScale.Y = this.LerpTarget.Y;
              this.IsLerping = false;
            }
          }
        }
        Sengine.WorldOriginandScale.Z += player.inputmap.momentumwheel.MovementThisFrame * (1f / 1000f);
        Sengine.WorldOriginandScale.Z += (float) ((double) player.inputmap.ZoomValue * (double) DeltaTime * 1.29999995231628);
        Sengine.WorldOriginandScale.Z = MathHelper.Clamp(Sengine.WorldOriginandScale.Z, RenderMath.GetNearestPerfectPixelZoom(1f) * 0.5f, RenderMath.GetNearestPerfectPixelZoom(2f));
        double num1 = (double) Sengine.WorldOriginandScale.X - (double) player.player.touchinput.DragVectorThisFrame.X / (double) Sengine.WorldOriginandScale.Z;
        float num2 = 512f / Sengine.WorldOriginandScale.Z - 100f;
        double num3 = (double) this.MinX - (double) num2;
        double num4 = (double) this.MaxX + (double) num2;
        double num5 = (double) MathHelper.Clamp((float) num1, (float) num3, (float) num4);
        float num6 = 384f / Sengine.WorldOriginandScale.Z - 100f;
        double num7 = (double) MathHelper.Clamp(Sengine.WorldOriginandScale.Y - player.player.touchinput.DragVectorThisFrame.Y / Sengine.WorldOriginandScale.Z, this.MinY - num6, this.MaxY + num6);
        double z = (double) Sengine.WorldOriginandScale.Z;
        CameraManager.HardSetCameraPosition(new Vector3((float) num5, (float) num7, (float) z));
      }
      bool flag = false;
      if (this.SelectionItem != null)
        flag = this.SelectionItem.BlockMouse(player);
      bool BlockMouse = false;
      bool NewControllerSelect;
      R_Icon selection = this.ricongrid.UpdateR_IconGrid(player, DeltaTime, out NewControllerSelect, this.SelectionItem, BlockMouse);
      if (NewControllerSelect)
      {
        this.IsLerping = true;
        this.camlerper = new LerpHandler_Float();
        this.camlerper.SetLerp(true, 0.0f, 1f, 3f);
        this.LerpStart.X = Sengine.WorldOriginandScale.X;
        this.LerpStart.Y = Sengine.WorldOriginandScale.Y;
        this.LerpTarget = this.ricongrid.GetLerpTarget();
      }
      if (selection != null)
      {
        this.SelectionItem = new SelectedRentryManager(selection, player);
        this.researchPointDisplay.OnTryingToSpendResearchPoints(this.SelectionItem.CanAfford);
      }
      if (this.SelectionItem == null)
        return;
      bool RescanRanges;
      if (this.SelectionItem.UpdateSelectedRentryManager(player, DeltaTime, this.ricongrid, out RescanRanges))
        this.SelectionItem.LerpOff();
      if (!RescanRanges)
        return;
      this.ricongrid.GetRingScrollLimit(out this.MinX, out this.MaxX, out this.MinY, out this.MaxY);
    }

    public void DrawReseaechView()
    {
      this.ricongrid.DrawR_IconGrid(this.SelectionItem);
      this.researchPointDisplay.DrawResearchPointsDisplay(Vector2.Zero, AssetContainer.pointspritebatch03);
      if (this.SelectionItem == null)
        return;
      this.SelectionItem.DrawSelectedRentryManager();
    }
  }
}
