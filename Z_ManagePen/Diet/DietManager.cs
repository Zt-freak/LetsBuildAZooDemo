// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.DietManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_ManagePen.Diet.AssignedStaff;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary;

namespace TinyZoo.Z_ManagePen.Diet
{
  internal class DietManager
  {
    private SingleAnimalManager singleanimalmanager;
    private AssignedStaffManager assignedstaffmanager;
    private GameObjectNineSlice Frame;
    public Vector2 Vscale;
    private LerpHandler_Float lerper;
    private PrisonZone Ref_SelectedEnclosure;

    public DietManager(Player player, PrisonZone SelectedEnclosure, float MasterMult)
    {
      this.Ref_SelectedEnclosure = SelectedEnclosure;
      this.Vscale = new Vector2(720f, 600f);
      this.Frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.singleanimalmanager = new SingleAnimalManager(this.Vscale, SelectedEnclosure, player, MasterMult);
      this.assignedstaffmanager = new AssignedStaffManager(player, SelectedEnclosure);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Frame.vLocation = new Vector2(1024f - (float) (((double) this.Vscale.X + 65.0) * 0.5), 384f);
    }

    public void UpdateDietManager(float DeltaTime, Player player)
    {
      Vector2 TopMiddle = new Vector2(this.lerper.Value * 1024f, 0.0f) + this.Frame.vLocation;
      TopMiddle.Y -= this.Vscale.Y * 0.5f;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.singleanimalmanager == null)
        return;
      this.singleanimalmanager.UpdateSingleAnimalManager(TopMiddle, player, DeltaTime, this.Ref_SelectedEnclosure, out bool _);
    }

    public void DrawDietManager()
    {
      Vector2 TopMiddle = new Vector2(this.lerper.Value * 1024f, 0.0f) + this.Frame.vLocation;
      TopMiddle.Y -= this.Vscale.Y * 0.5f;
      if (this.singleanimalmanager == null)
        return;
      this.singleanimalmanager.DrawSingleAnimalManager(TopMiddle);
    }
  }
}
