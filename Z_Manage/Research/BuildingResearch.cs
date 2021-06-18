// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Research.BuildingResearch
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_Architcture;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Research_;
using TinyZoo.Z_Research_.IconGrid.BG3D;

namespace TinyZoo.Z_Manage.Research
{
  internal class BuildingResearch
  {
    private Z_ArchitectManager achitectureviewmanager;
    private ScreenHeading screenhead;
    private BackButton closebutton;
    private bool Exiting;
    private StoreBGManager storeBGManager;
    private BG3DManager threedeemanager;
    private Z_ResearchManager researchmanager;

    public BuildingResearch(Player player, bool IsWholeScreen = false)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      if (IsWholeScreen)
      {
        this.storeBGManager = new StoreBGManager(true);
        this.closebutton = new BackButton();
        this.threedeemanager = new BG3DManager();
      }
      OverWorldCamera.SetReturnForCamera();
      this.screenhead = new ScreenHeading("RESEARCH", 30f, BaseScale: baseScaleForUi, UseSmallerOnePointFiveFont: true);
      this.researchmanager = new Z_ResearchManager(player);
    }

    public void UpdateBuildingResearch(Player player, float DeltaTime)
    {
      NotificationBubbleManager.Instance.UpdateNotificationBubbleManager(player, DeltaTime);
      this.researchmanager.UpdateReasearchView(player, DeltaTime);
      if (this.closebutton == null)
        return;
      this.threedeemanager.UpdateBG3DManager(DeltaTime);
      if (!this.closebutton.UpdateBackButton(player, DeltaTime) || this.Exiting)
        return;
      this.Exiting = true;
      Game1.SetNextGameState(GAMESTATE.OverWorld);
      Game1.screenfade.BeginFade(true);
    }

    public void DrawBuildingResearch()
    {
      if (this.threedeemanager != null)
        this.threedeemanager.DrawBG3DManager();
      this.researchmanager.DrawReseaechView();
      if (this.screenhead != null)
        this.screenhead.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      if (this.closebutton != null)
        this.closebutton.DrawBackButton(Vector2.Zero);
      NotificationBubbleManager.Instance.DrawNotificationBubbleManager();
    }
  }
}
