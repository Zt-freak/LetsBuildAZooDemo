// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.QuestComplete.StampPrint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Quests.QuestComplete
{
  internal class StampPrint
  {
    public Vector2 location;
    private GameObject print;

    public StampPrint(float BaseScale, StampPrintType printType)
    {
      this.print = new GameObject();
      switch (printType)
      {
        case StampPrintType.TaskComplete:
          this.print.DrawRect = new Rectangle(195, 108, 94, 94);
          break;
        case StampPrintType.Rejected:
          this.print.DrawRect = new Rectangle(425, 89, 154, 52);
          break;
        case StampPrintType.Approved:
          this.print.DrawRect = new Rectangle(524, 28, 154, 53);
          break;
      }
      this.print.scale = BaseScale;
      this.print.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.print.DrawRect.Width, (float) this.print.DrawRect.Height) * this.print.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void SetRotation(float rotation) => this.print.Rotation = rotation;

    public void UpdateStampPrint()
    {
    }

    public void DrawStampPrint(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.print.Draw(spriteBatch, AssetContainer.UISheet, offset);
    }
  }
}
