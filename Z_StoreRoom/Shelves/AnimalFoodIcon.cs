// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.AnimalFoodIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Text;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid;

namespace TinyZoo.Z_StoreRoom.Shelves
{
  internal class AnimalFoodIcon : GameObject
  {
    private string ThingName;
    private StringScroller stringscroller;
    private float TextScale;
    private float basescale;
    private SlashCross cross;

    public AnimalFoodType refFoodType { get; private set; }

    public AnimalFoodIcon(AnimalFoodType foodtype, float _Scale, float basescale_ = -1f)
    {
      this.refFoodType = foodtype;
      this.scale = 1f;
      this.basescale = basescale_;
      if ((double) basescale_ == -1.0)
      {
        this.basescale = RenderMath.GetPixelSizeBestMatch((float) (1.33329999446869 * ((double) this.scale * 0.400000005960464)));
        this.TextScale = RenderMath.GetPixelSizeBestMatch((float) (1.33329999446869 * ((double) this.scale * 0.400000005960464)));
      }
      else
      {
        this.scale = _Scale * this.basescale;
        this.TextScale = _Scale * this.basescale;
      }
      this.DrawRect = AnimalFoodData.GetAnimalFoodInfo(foodtype).DrawRect;
      this.SetDrawOriginToCentre();
      this.ThingName = AnimalFoodData.GetAnimalFoodTypeToString(foodtype);
      this.stringscroller = new StringScroller(this.scale * (1f / this.TextScale) * 70f, this.ThingName, AssetContainer.SpringFontX1AndHalf);
    }

    public Vector2 GetSize_IconOnly(bool withoutScreenRatioMult = false)
    {
      Vector2 vector2 = new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * this.basescale;
      if (!withoutScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }

    public void SetIsUndiscovered()
    {
      this.SetAllColours(Color.Black.ToVector3());
      this.SetAlpha(0.4f);
    }

    public void SetIsUnavailable() => this.cross = new SlashCross(this.basescale);

    public void SetIsGreyedOut()
    {
      this.SetAllColours(Color.Gray.ToVector3());
      this.SetAlpha(0.5f);
    }

    public void SetIsInActive(bool IsActive)
    {
      if (IsActive)
        this.SetAllColours(Color.White.ToVector3());
      else
        this.SetAllColours(ColourData.Z_Gray);
    }

    public void UpdateStringScroll(float DeltaTime) => this.stringscroller.UpdateStringScroller(DeltaTime);

    public void DrawAnimalFoodIcon(Vector2 Offset, bool DrawName) => this.DrawAnimalFoodIcon(Offset, DrawName, AssetContainer.pointspritebatch03);

    public void DrawAnimalFoodIcon(Vector2 Offset, bool DrawName, SpriteBatch speitebatch)
    {
      this.Draw(speitebatch, AssetContainer.AnimalSheet, Offset);
      if (DrawName)
        TextFunctions.DrawJustifiedText(this.stringscroller.GetString(), this.TextScale, Offset + this.vLocation + new Vector2(0.0f, 35f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.scale), new Color(ColourData.Z_DarkTextGray), 1f, AssetContainer.SpringFontX1AndHalf, speitebatch);
      if (this.cross == null)
        return;
      this.cross.DrawSlashCross(Offset + this.vLocation, speitebatch);
    }
  }
}
