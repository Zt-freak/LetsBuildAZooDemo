// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments.StatusIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments
{
  internal class StatusIcon : GameObject
  {
    public StatusIconType statusicontype;
    public bool IsImportant;
    private GameObject MOUSEOVER;

    public void SetStatucIconType(StatusIconType statusicon)
    {
      this.statusicontype = statusicon;
      this.MOUSEOVER = new GameObject((GameObject) this);
      switch (statusicon)
      {
        case StatusIconType.Caffiene:
          this.DrawRect = new Rectangle(410, 516, 20, 22);
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          this.DrawOrigin.Y += 17f;
          break;
        case StatusIconType.BlackMarket:
          this.DrawRect = new Rectangle(387, 516, 22, 22);
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          this.DrawOrigin.Y += 17f;
          this.IsImportant = true;
          this.MOUSEOVER.DrawRect = new Rectangle(387, 493, 22, 22);
          break;
      }
      this.MOUSEOVER.DrawOrigin = this.DrawOrigin;
    }

    public bool CheckMouseOver(Vector2 PersonLocation, Vector2 Mouse_WORLDLOC)
    {
      PersonLocation.Y -= 28f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      return MathStuff.CheckPointCollision(true, ref PersonLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, ref Mouse_WORLDLOC);
    }

    public void UpdateStatusIcon()
    {
    }

    public void DrawStatusIcon(Vector2 Offset, bool MouseOver)
    {
      if (GameFlags.PhotoMode)
        return;
      this.vLocation = Offset;
      if (MouseOver)
        this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.UISheet);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.UISheet);
    }
  }
}
