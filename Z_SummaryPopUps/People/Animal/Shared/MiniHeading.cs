// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.Shared.MiniHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.Shared
{
  internal class MiniHeading : GameObject
  {
    private string Text;
    private float BaseScale;
    private bool SetBottomRight;

    public MiniHeading(
      Vector2 VSCaleForFrame,
      string text,
      float MasterMult,
      float _BaseScale = -1f,
      bool _SetBottomRight = false)
    {
      this.SetBottomRight = _SetBottomRight;
      this.Text = text;
      this.BaseScale = _BaseScale;
      this.SetAllColours(ColourData.Z_Cream);
      if ((double) this.BaseScale > -1.0)
        this.scale = this.BaseScale;
      else
        this.scale = MiniHeading.GetScale(MasterMult);
      this.SetTextPosition(VSCaleForFrame);
    }

    public void SetNewText(string text) => this.Text = text;

    public float GetTextHeight(bool GetWithScreenMult = false) => GetWithScreenMult ? AssetContainer.SpringFontX1AndHalf.MeasureString(this.Text).Y * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y : AssetContainer.SpringFontX1AndHalf.MeasureString(this.Text).Y * this.scale;

    public Vector2 GetSize() => AssetContainer.SpringFontX1AndHalf.MeasureString(this.Text) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void SetTextPosition(Vector2 VSCaleForFrame, float Xbuffer = 10f, float Ybuffer = 10f)
    {
      this.vLocation.X = 0.0f;
      this.vLocation.Y = 0.0f;
      if ((double) this.BaseScale != -1.0)
      {
        this.vLocation.X -= VSCaleForFrame.X * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.X;
        this.vLocation.X += Xbuffer * this.BaseScale;
        if (this.SetBottomRight)
        {
          this.vLocation.Y += VSCaleForFrame.Y * 0.5f;
          this.vLocation.Y -= Ybuffer * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        }
        else
        {
          this.vLocation.Y -= VSCaleForFrame.Y * 0.5f;
          this.vLocation.Y += Ybuffer * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        }
      }
      else
      {
        this.vLocation.X -= VSCaleForFrame.X * 0.5f;
        this.vLocation.X += AnimalPopUpManager.VerticalBuffer;
        if (this.SetBottomRight)
        {
          this.vLocation.Y += VSCaleForFrame.Y * 0.5f;
          this.vLocation.Y -= AnimalPopUpManager.VerticalBuffer;
        }
        else
        {
          this.vLocation.Y -= VSCaleForFrame.Y * 0.5f;
          this.vLocation.Y += AnimalPopUpManager.VerticalBuffer;
        }
      }
    }

    public static float GetHeight(float masterMult = 1.2f)
    {
      float scale = MiniHeading.GetScale(masterMult);
      return (float) (((double) AnimalPopUpManager.TopAreaBuffer + (double) AnimalPopUpManager.VerticalBuffer) * (double) scale / 2.0) * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public static float GetScale(float MasterMult) => RenderMath.GetPixelSizeBestMatch((float) (2.0 * (double) MasterMult * 0.666000008583069));

    public void EditText(string newText) => this.Text = newText;

    public void DrawMiniHeading(Vector2 centerOfPanel) => this.DrawMiniHeading(centerOfPanel, AssetContainer.pointspritebatchTop05);

    public void DrawMiniHeading(Vector2 centerOfPanel, SpriteBatch spriteBatch)
    {
      if (!this.SetBottomRight)
        TextFunctions.DrawTextWithDropShadow(this.Text, this.scale, centerOfPanel + this.vLocation, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spriteBatch, false);
      else
        TextFunctions.DrawTextWithDropShadow(this.Text, this.scale, centerOfPanel + this.vLocation, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spriteBatch, false, true);
    }
  }
}
