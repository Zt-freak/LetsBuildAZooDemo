// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal_Dead.DeathMainFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal_Dead
{
  internal class DeathMainFrame
  {
    private GameObjectNineSlice Frame;
    private BackButton Close;
    public Vector2 Vscale;
    private LerpHandler_Float lerper;
    public Vector2 TopCenter;

    public DeathMainFrame(float MasterMult)
    {
      this.Vscale = new Vector2(500f, 600f) * MasterMult;
      this.Frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Close = new BackButton(true);
      this.Close.vLocation = new Vector2(220f * MasterMult, 23f);
      this.TopCenter = new Vector2(768f, 170f);
      this.Frame.vLocation.Y = this.Vscale.Y * 0.5f;
    }

    public bool UpdateDeathMainFrame(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.TopCenter;
      this.lerper.UpdateLerpHandler(DeltaTime);
      return (double) this.lerper.Value == 0.0 && this.Close.UpdateBackButton(player, DeltaTime, Offset);
    }

    public void DrawTabFrameManager(Vector2 Offset)
    {
      Offset += this.TopCenter;
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.Vscale);
      this.Close.DrawBackButton(Offset);
    }
  }
}
