// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.BaseScaled.P_And_L
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_ManageShop.BaseScaled
{
  internal class P_And_L
  {
    private GameObject ColumnObj;
    private GameObject UnderObj;
    private GameObject TextObjTop;
    private GameObject TextObjLow;
    private Vector2 ColumnVScale1;
    private string ASTring;
    private string BStreing;
    public Vector2 Location;
    private float BaseScale;

    public P_And_L(float VScaleY, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.ColumnObj = new GameObject();
      this.ColumnObj.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.ColumnObj.SetDrawOriginToCentre();
      this.ColumnObj.SetAllColours(0.0f, 0.0f, 0.0f);
      this.ColumnObj.SetAlpha(0.4f);
      this.UnderObj = new GameObject(this.ColumnObj);
      this.ColumnVScale1 = new Vector2(150f * this.BaseScale, VScaleY * 0.5f);
      this.TextObjTop = new GameObject();
      this.TextObjTop.SetAllColours(ColourData.Z_Cream);
      this.TextObjTop.scale = this.BaseScale;
      this.TextObjTop.vLocation.Y -= this.BaseScale * 10f;
      this.TextObjLow = new GameObject();
      this.TextObjLow.SetAllColours(ColourData.Z_Cream);
      this.TextObjLow.scale = this.BaseScale * 1f;
      this.TextObjLow.vLocation.Y = this.BaseScale * 20f;
    }

    public void SetStatus(string Top, string Middle, bool IsLoss = false, bool IsProfit = false)
    {
      this.TextObjTop.vLocation.Y = (float) (-(double) this.BaseScale * 6.0);
      this.TextObjLow.vLocation.Y = this.BaseScale * 6f;
      this.ColumnVScale1.Y = this.BaseScale * 30f;
      this.ASTring = Top;
      this.BStreing = Middle;
      if (IsProfit)
      {
        this.ColumnObj.SetAlpha(1f);
        this.ColumnObj.SetAllColours(new Vector3(0.0f, 0.4f, 0.0f));
        this.UnderObj.SetAllColours(new Vector3(0.2f, 0.8f, 0.2f));
      }
      else if (IsLoss)
      {
        this.ColumnObj.SetAlpha(1f);
        this.ColumnObj.SetAllColours(new Vector3(0.5f, 0.0f, 0.0f));
        this.UnderObj.SetAllColours(new Vector3(1f, 0.5f, 0.5f));
      }
      else
      {
        this.TextObjTop.vLocation.Y = 0.0f;
        this.ColumnObj.SetAlpha(1f);
        this.ColumnObj.SetAllColours(new Vector3(0.0f, 0.0f, 0.4f));
        this.UnderObj.SetAllColours(new Vector3(0.5f, 0.5f, 1f));
      }
    }

    public void UpdateP_And_L()
    {
    }

    public void DrawP_And_L(Vector2 Offset)
    {
      Offset += this.Location;
      this.UnderObj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.ColumnVScale1 + new Vector2(this.BaseScale * 4f, this.BaseScale * 4f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.ColumnObj.fAlpha);
      this.ColumnObj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.ColumnVScale1, this.ColumnObj.fAlpha);
      TextFunctions.DrawJustifiedText(this.ASTring, this.TextObjTop.scale, this.TextObjTop.vLocation + Offset, this.TextObjTop.GetColour(), this.TextObjTop.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText(this.BStreing, this.TextObjLow.scale, this.TextObjLow.vLocation + Offset, this.TextObjLow.GetColour(), this.TextObjLow.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
    }
  }
}
