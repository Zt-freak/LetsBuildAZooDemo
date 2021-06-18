// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Cleanliness.CleanlinessManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_ManagePen.Cleanliness
{
  internal class CleanlinessManager
  {
    private GameObjectNineSlice Frame;
    public Vector2 Vscale;
    private LerpHandler_Float lerper;

    public CleanlinessManager(PrisonZone SelectedEnclosure)
    {
      this.Vscale = new Vector2(720f, 600f);
      this.Frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Frame.vLocation = new Vector2(1024f - (float) (((double) this.Vscale.X + 65.0) * 0.5), 384f);
    }

    public void UpdateCleanlinessManager(Player player, float DeltaTime) => this.lerper.UpdateLerpHandler(DeltaTime);

    public void DrawCleanlinessManager()
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.Vscale);
    }
  }
}
