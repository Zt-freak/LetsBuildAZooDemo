// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity.LongevityBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity
{
  internal class LongevityBar : GameObject
  {
    private PointerAndText pointerandtext;
    private static int StartZone = 2;
    private static int Childhood = 23;
    private static int FertilePeriod = 72;
    private static int Death = 29;
    public Vector2 Location;

    public LongevityBar(PrisonerInfo animal, float MasterMult, float BaseScale = -1f)
    {
      this.DrawRect = new Rectangle(756, 602, 128, 10);
      this.scale = 1.5f * MasterMult;
      if ((double) BaseScale > -1.0)
      {
        MasterMult = 1f;
        this.scale = BaseScale;
      }
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.scale);
      float startZone = (float) LongevityBar.StartZone;
      string Text;
      float num1;
      if (!animal.GetIsABaby())
      {
        float num2 = startZone + (float) LongevityBar.Childhood;
        if (animal.GetCanHaveBaby())
        {
          Text = "mature";
          num1 = num2 + animal.GetPercentThroughAdulthood() * (float) LongevityBar.Death;
        }
        else
        {
          Text = "";
          num1 = num2 + (float) LongevityBar.FertilePeriod + animal.GetPercentThroughOldAge() * (float) LongevityBar.Death;
        }
      }
      else
      {
        Text = "Child";
        num1 = startZone + animal.GetPercentThroughChildhood() * (float) LongevityBar.Childhood;
      }
      float num3 = uiScaleHelper.ScaleY(6f);
      this.pointerandtext = new PointerAndText(Text, MasterMult, this.scale, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y + num3);
      this.pointerandtext.vLocation.X = num1 * this.scale;
      this.pointerandtext.vLocation.Y += (float) (((double) uiScaleHelper.ScaleY((float) this.DrawRect.Height) + (double) num3) * 0.5);
    }

    public Vector2 GetSize()
    {
      Vector2 vector2 = new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;
      return new Vector2(0.0f, this.pointerandtext.GetLineAndTextHeight() - vector2.Y) + vector2;
    }

    public void UpdateLongevityBar()
    {
    }

    public void DrawLongevityBar(Vector2 offset) => this.DrawLongevityBar(offset, AssetContainer.pointspritebatchTop05);

    public void DrawLongevityBar(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset);
      this.pointerandtext.DrawPointerAndText(Offset, spriteBatch);
    }
  }
}
