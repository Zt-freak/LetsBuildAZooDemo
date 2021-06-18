// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary.SpinningInProgressIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary
{
  internal class SpinningInProgressIcon : AnimatedGameObject
  {
    public SpinningInProgressIcon(float BaseScale)
    {
      this.DrawRect = new Rectangle(369, 81, 23, 23);
      this.scale = BaseScale;
      this.SetDrawOriginToCentre();
      this.SetUpSimpleAnimation(8, 0.2f);
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void UpdateSpinningInProgressIcon(float DeltaTime) => this.UpdateAnimation(DeltaTime);

    public void DrawSpinningInProgressIcon(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.UISheet, offset);
  }
}
