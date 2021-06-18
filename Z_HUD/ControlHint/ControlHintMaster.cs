// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.ControlHint.ControlHintMaster
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldRenderer;

namespace TinyZoo.Z_HUD.ControlHint
{
  internal class ControlHintMaster
  {
    private LerpHandler_Float lerper;
    private MicroOpenClose microopen;
    private ControlHintManager controlhintmanager;
    private ControllerHintSummary summarytype;
    private Vector2 HideWholeThingOffset;
    private LerpHandler_Float HideWholeThingLerper;
    private bool[] TempForcedStates;
    private float BaseScale;
    private float XpopOutDistance;
    private static bool WasOut;
    private bool IsCustomControls;

    public ControlHintMaster()
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.TempForcedStates = new bool[7];
      this.HideWholeThingLerper = new LerpHandler_Float();
      this.HideWholeThingLerper.SetLerp(true, -1f, 0.0f, 3f);
      this.microopen = new MicroOpenClose(this.BaseScale);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, -1f, 3f);
      this.summarytype = ControllerHintSummary.BaseNavigation;
      this.CreateNewHintManager(this.summarytype);
      this.microopen.SetAsArrow();
      this.XpopOutDistance = this.controlhintmanager.GetSize().X;
      this.microopen.vLocation.Y = this.controlhintmanager.GetHeaderSize().Y * 0.5f;
      if (!ControlHintMaster.WasOut)
        return;
      Z_GameFlags.ForceControllerHintsToThe = this.GetCurrentHitType();
    }

    private void CreateNewHintManager(ControllerHintSummary _summaryType)
    {
      this.controlhintmanager = new ControlHintManager(_summaryType, this.BaseScale);
      this.controlhintmanager.Location = new Vector2(10f, 200f);
      this.controlhintmanager.Location += this.controlhintmanager.GetSize() * 0.5f;
    }

    public bool CheckMouseOver(Player player) => (0 | (this.controlhintmanager.CheckMouseOver(player, this.HideWholeThingOffset + new Vector2(this.lerper.Value * 200f * this.BaseScale, 0.0f)) ? 1 : 0) | (this.microopen.CheckMouseOver(player, this.HideWholeThingOffset + this.controlhintmanager.GetOffset() + new Vector2((this.lerper.Value + 1f) * this.XpopOutDistance, 0.0f)) ? 1 : 0)) != 0;

    public void UpdateControlHintMaster(float DeltaTime, Player player)
    {
      this.HideWholeThingLerper.UpdateLerpHandler(DeltaTime);
      this.HideWholeThingOffset.X = this.HideWholeThingLerper.Value * 300f;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (!player.Stats.TutorialsComplete[29] && !this.TempForcedStates[(int) this.GetCurrentHitType()] && OverWorldManager.zoopopupHolder.IsNull())
      {
        Z_GameFlags.ForceControllerHintsToThe = this.GetCurrentHitType();
        this.TempForcedStates[(int) this.GetCurrentHitType()] = true;
      }
      if ((double) this.lerper.TargetValue == 0.0 && this.summarytype != this.GetCurrentHitType())
        Z_GameFlags.ForceControllerHintsToThe = this.GetCurrentHitType();
      if (!this.IsCustomControls)
      {
        if (Z_GameFlags.ForceControllerHintsToThe != ControllerHintSummary.Count)
        {
          this.lerper.SetLerp(true, -1f, 0.0f, 3f, true);
          this.microopen.SetAsClose();
          ControlHintMaster.WasOut = true;
          this.CreateNewHintManager(Z_GameFlags.ForceControllerHintsToThe);
          this.summarytype = Z_GameFlags.ForceControllerHintsToThe;
          Z_GameFlags.ForceControllerHintsToThe = ControllerHintSummary.Count;
        }
        if (FeatureFlags.FullyBlockControlHint && !FeatureFlags.ForceAllowControlHint)
        {
          if ((double) this.HideWholeThingLerper.TargetValue != -1.0)
            this.HideWholeThingLerper.SetLerp(false, 0.0f, -1f, 3f, true);
        }
        else if ((double) this.HideWholeThingLerper.TargetValue != 0.0)
          this.HideWholeThingLerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      }
      if (this.controlhintmanager.UpdateControlHintManager(player, this.HideWholeThingOffset + new Vector2(this.lerper.Value * 200f * this.BaseScale, 0.0f), DeltaTime))
      {
        this.IsCustomControls = true;
        ++this.summarytype;
        if (this.summarytype == ControllerHintSummary.Count)
          this.summarytype = ControllerHintSummary.BaseNavigation;
        this.HideWholeThingLerper.SetLerp(false, -1f, 0.0f, 3f, true);
        this.CreateNewHintManager(this.summarytype);
      }
      if (!this.microopen.UpdateMicroOpenClose(player, DeltaTime, this.HideWholeThingOffset + this.controlhintmanager.GetOffset() + new Vector2((this.lerper.Value + 1f) * this.XpopOutDistance, 0.0f)))
        return;
      if ((double) this.lerper.TargetValue == 0.0)
      {
        ControlHintMaster.WasOut = false;
        this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
        this.microopen.SetAsArrow();
      }
      else
      {
        ControlHintMaster.WasOut = true;
        this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
        this.microopen.SetAsClose();
      }
    }

    private ControllerHintSummary GetCurrentHitType()
    {
      switch (OverWorldManager.overworldstate)
      {
        case OverWOrldState.MainMenu:
          return ControllerHintSummary.BaseNavigation;
        case OverWOrldState.Build:
          switch (OverworldBuildManager.currentbuildstate)
          {
            case BUILDSTATEFORCONTROLLERHINT.NothinngOpened:
              return ControllerHintSummary.UseBuildMenu;
            case BUILDSTATEFORCONTROLLERHINT.Pen:
              return ControllerHintSummary.BuildPen;
            case BUILDSTATEFORCONTROLLERHINT.Structure:
              return ControllerHintSummary.BuildStructure;
            case BUILDSTATEFORCONTROLLERHINT.PlaceGate:
              return ControllerHintSummary.PlaceGate;
            case BUILDSTATEFORCONTROLLERHINT.MovePen:
              return ControllerHintSummary.MovePen;
            default:
              return ControllerHintSummary.BuildStructure;
          }
        case OverWOrldState.CellSelect:
          return ControllerHintSummary.CellSelect;
        default:
          return ControllerHintSummary.BaseNavigation;
      }
    }

    public void DrawControlHintMaster(SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value > -1.0)
        this.controlhintmanager.DrawControlHintManager(this.HideWholeThingOffset + new Vector2(this.lerper.Value * 200f * this.BaseScale, 0.0f), spriteBatch);
      this.microopen.DrawMicroOpenClose(this.HideWholeThingOffset + this.controlhintmanager.GetOffset() + new Vector2((this.lerper.Value + 1f) * this.XpopOutDistance, 0.0f), spriteBatch);
    }
  }
}
