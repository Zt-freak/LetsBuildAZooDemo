// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Buttons.Z_BreakoutButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_ManagePen.Buttons
{
  internal class Z_BreakoutButton
  {
    private GameObject ICON;
    private GameObjectNineSlice gobject;
    private Z_BreakOutIcon beakouticon;
    private BreakOutButtonType ButtonType;
    private Vector2 VSCALE;
    public Vector2 Location;
    private GameObjectNineSlice MouseOverObj;
    private bool MouseOver;
    private GameObject Texticle;
    private string TextToWrite;
    public LerpHandler_Float lerper;
    public Vector2 LerperOffset;
    public float LerpMult;
    private float OVERALLMULT = 0.5f;
    private float BaseScale;

    public Z_BreakoutButton(BreakOutButtonType buttontype, float Lngth = 150f, float _OVERALLMULT = 1f)
    {
      this.OVERALLMULT = _OVERALLMULT;
      this.BaseScale = this.OVERALLMULT;
      this.LerpMult = 150f;
      this.LerperOffset = new Vector2(0.0f, 1f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      Lngth *= this.OVERALLMULT;
      this.ButtonType = buttontype;
      this.gobject = new GameObjectNineSlice(new Rectangle(885, 568, 21, 21), 7);
      this.MouseOverObj = new GameObjectNineSlice(new Rectangle(885, 568, 21, 21), 7);
      this.gobject.scale = 2f;
      this.MouseOverObj.scale = 3f;
      this.MouseOverObj.SetAlpha(0.3f);
      this.gobject.SetAllColours(ColourData.Z_Cream);
      this.beakouticon = new Z_BreakOutIcon(this.ButtonType);
      this.gobject.scale = this.BaseScale;
      this.VSCALE = new Vector2(Lngth, 32f * this.OVERALLMULT);
      this.beakouticon.scale = 1f * this.OVERALLMULT;
      this.beakouticon.vLocation = new Vector2(this.VSCALE.X * -0.5f, 0.0f);
      this.beakouticon.vLocation.X += (float) ((double) this.beakouticon.DrawRect.Width * (double) this.beakouticon.scale * 0.5);
      this.beakouticon.vLocation.X += this.VSCALE.Y - (float) this.beakouticon.DrawRect.Height * this.beakouticon.scale;
      this.Texticle = new GameObject();
      this.Texticle.scale = 1.5f * this.OVERALLMULT;
      this.Texticle.CloneColours((GameObject) this.beakouticon);
      this.Texticle.vLocation.X = this.beakouticon.vLocation.X + (float) ((double) this.beakouticon.DrawRect.Width * (double) this.beakouticon.scale * 0.5);
      this.Texticle.vLocation.X += 10f * this.OVERALLMULT;
      this.Texticle.vLocation.Y = (float) (-6.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * ((double) this.OVERALLMULT * 1.5));
      switch (buttontype)
      {
        case BreakOutButtonType.Back:
          this.TextToWrite = "Back";
          break;
        case BreakOutButtonType.CloseMenu:
          this.TextToWrite = "Close Menu";
          break;
        case BreakOutButtonType.Heading:
          this.TextToWrite = "NA";
          this.beakouticon = (Z_BreakOutIcon) null;
          this.LerpMult = 300f;
          this.LerperOffset = new Vector2(-1f, 0.0f);
          this.Texticle.vLocation.X = this.VSCALE.X * -0.5f;
          this.Texticle.vLocation.X += 10f * this.OVERALLMULT;
          this.Texticle.SetAllColours(ColourData.Z_Cream);
          this.gobject.SetAllColours(ColourData.Z_PaleBrown);
          break;
      }
    }

    public void SetText(string NewHeading) => this.TextToWrite = NewHeading;

    public void LerpOn() => this.lerper.SetLerp(false, 1f, 0.0f, 3f, true);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);

    public void UpdateTempExitLerp(float DeltaTime) => this.lerper.UpdateLerpHandler(DeltaTime);

    public bool MouseOverlapping(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      Offset += this.lerper.Value * this.LerperOffset * this.LerpMult;
      return MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCALE.X, this.VSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
    }

    public bool UpdateZ_BreakoutButton(Vector2 Offset, Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.ButtonType == BreakOutButtonType.Heading)
        return false;
      Offset += this.Location;
      Offset += this.lerper.Value * this.LerperOffset * this.LerpMult;
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCALE.X, this.VSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      if (this.MouseOver && (player.inputmap.PressedThisFrame[0] || (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0))
      {
        player.player.touchinput.ReleaseTapArray[0].X = -10000f;
        player.player.touchinput.ReleaseTapArray[0].Y = -10000f;
        player.inputmap.PressedThisFrame[0] = false;
        return true;
      }
      if (this.ButtonType != BreakOutButtonType.Back || !player.inputmap.PressedBackOnController())
        return false;
      player.inputmap.ReleasedThisFrame[7] = false;
      return true;
    }

    public void DrawZ_BreakoutButton(Vector2 Offset, SpriteBatch spritebatch, float ShrinkValue = 0.0f)
    {
      Offset += this.lerper.Value * this.LerperOffset * this.LerpMult;
      Offset += this.Location;
      float num = 1f;
      if ((double) ShrinkValue != 0.0)
      {
        Offset.Y += 120f * ShrinkValue * this.BaseScale;
        num = 1f - ShrinkValue;
        Offset.X -= (float) ((double) this.VSCALE.X * (double) ShrinkValue * 0.5);
      }
      this.gobject.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCALE * num * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.beakouticon != null)
        this.beakouticon.DrawZ_BreakOutIcon(Offset);
      TextFunctions.DrawTextWithDropShadow(this.TextToWrite, this.Texticle.scale * num, this.Texticle.vLocation * num + Offset, this.Texticle.GetColour(), this.Texticle.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      if (!this.MouseOver)
        return;
      this.MouseOverObj.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCALE * num * Sengine.ScreenRatioUpwardsMultiplier);
    }
  }
}
