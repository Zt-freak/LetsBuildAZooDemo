// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.TopBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.Elements;

namespace TinyZoo.Z_HUD.TopBar
{
  internal class TopBarManager
  {
    private TinyZoo.Z_HUD.TopBar.TopBar topbar;
    private TopBarElements topBarElements;
    private float BaseScale;
    private float BarHeight;
    internal static float TopBarLerpDistance;
    private LerpHandler_Float lerper;
    public float YOffset;
    private static float Middle = -1f;

    public TopBarManager(Player player)
    {
      this.BarHeight = 45f;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      this.topbar = new TinyZoo.Z_HUD.TopBar.TopBar(this.BarHeight, this.BaseScale);
      TopBarManager.Middle = uiScaleHelper.ScaleY(this.BarHeight) * 0.5f;
      this.topBarElements = new TopBarElements(player, this.BaseScale, uiScaleHelper.ScaleY(this.BarHeight));
      TopBarManager.TopBarLerpDistance = this.topbar.VSCale.Y + 1f;
    }

    internal static float GetMiddleOfBar() => TopBarManager.Middle;

    public bool CheckMouseOver(Player player)
    {
      this.YOffset = this.lerper.Value * (this.topbar.VSCale.Y + 1f);
      return (double) player.inputmap.PointerLocation.Y < (double) this.YOffset + (double) TopBarManager.Middle * 2.0 || this.topBarElements.CheckMouseOver(player, new Vector2(0.0f, this.YOffset));
    }

    public void UpdateTopBarManager(Player player, float DeltaTime)
    {
      if (Z_GameFlags.GetAllowTopBar())
      {
        if ((double) this.lerper.TargetValue != 0.0)
          this.lerper.SetLerp(false, -1f, 0.0f, 3f);
      }
      else if ((double) this.lerper.TargetValue != -1.0)
        this.lerper.SetLerp(false, -1f, -1f, 3f);
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.YOffset = this.lerper.Value * (this.topbar.VSCale.Y + 1f);
      Vector2 offset = new Vector2(0.0f, this.YOffset);
      this.topbar.UpdateTopBar(DeltaTime);
      this.topBarElements.UpdateTopBarElements(player, DeltaTime, offset);
    }

    public void DrawTopBarManager(OverwoldMainButtons overworldbuttons)
    {
      this.YOffset = this.lerper.Value * (this.topbar.VSCale.Y + 1f);
      Vector2 vector2 = new Vector2(0.0f, this.YOffset);
      SpriteBatch pointspritebatch03 = AssetContainer.pointspritebatch03;
      this.topBarElements.PreDrawTopBarElements(vector2, pointspritebatch03);
      this.topbar.DrawTopBar(pointspritebatch03, vector2);
      this.topBarElements.DrawTopBarElements(vector2, pointspritebatch03);
      overworldbuttons.DrawOverwoldMainButtons(pointspritebatch03, vector2);
    }
  }
}
