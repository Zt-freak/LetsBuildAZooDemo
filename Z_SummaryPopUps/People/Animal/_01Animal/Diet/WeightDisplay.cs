// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet.WeightDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet
{
  internal class WeightDisplay
  {
    public Vector2 location;
    private ZGenericText header;
    private WeightBar weightBar;
    private Vector2 size;

    public WeightDisplay(PrisonerInfo prisonerInfo, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.header = new ZGenericText(string.Format("Weight: {0} kg", (object) Math.Round((double) prisonerInfo.GetCurrentWeightInKG(), 1)), BaseScale, false, _UseOnePointFiveFont: true);
      this.weightBar = new WeightBar(prisonerInfo, BaseScale);
      this.size = Vector2.Zero;
      this.header.vLocation = this.size;
      this.size.X += this.header.GetSize().X;
      this.size.X += defaultBuffer.X;
      this.header.vLocation.Y -= this.header.GetSize().Y * 0.5f;
      this.header.vLocation.Y += this.weightBar.GetHeightToCenterOfBar();
      this.weightBar.location = this.size;
      this.weightBar.location.X += this.weightBar.GetSize().X * 0.5f;
      this.size.Y = this.weightBar.GetSize().Y;
      this.size.Y += defaultBuffer.Y * 0.5f;
    }

    public Vector2 GetSize() => this.size;

    public void DrawWeightDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.weightBar.DrawWeightBar(offset, spriteBatch);
    }
  }
}
