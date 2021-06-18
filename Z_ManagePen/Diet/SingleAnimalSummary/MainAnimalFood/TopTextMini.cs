// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.TopTextMini
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood
{
  internal class TopTextMini : GameObject
  {
    private string Txt;
    private string Txt2;
    private bool TwLines;
    private bool IsSmall;
    private float EX;
    public bool CenterJustify;
    private bool OffsetForSplit;

    public TopTextMini(
      string WriteMe,
      float BaseScale,
      float TextHeight,
      bool _IsSmall = true,
      bool IsExtraSmall = false)
    {
      this.EX = 7f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.IsSmall = _IsSmall;
      this.Txt = WriteMe;
      this.SetAllColours(ColourData.Z_Cream);
      this.scale = BaseScale * 2f;
      if (this.IsSmall | IsExtraSmall)
      {
        this.scale = BaseScale;
        if (IsExtraSmall)
          this.EX = 4f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      this.vLocation.Y = TextHeight;
    }

    public void SetNewText(string WriteMe)
    {
      this.Txt = WriteMe;
      this.TwLines = false;
      this.Txt2 = "";
    }

    public void SetAsSplit(int SPlitAtSpace = 1)
    {
      bool flag = false;
      string str1 = "";
      char ch;
      for (int index = 0; index < this.Txt.Length; ++index)
      {
        if (this.Txt[index] != ' ')
        {
          if (!flag)
          {
            string str2 = str1;
            ch = this.Txt[index];
            string str3 = ch.ToString();
            str1 = str2 + str3;
          }
          else
          {
            string txt2 = this.Txt2;
            ch = this.Txt[index];
            string str2 = ch.ToString();
            this.Txt2 = txt2 + str2;
          }
        }
        else
        {
          --SPlitAtSpace;
          if (SPlitAtSpace <= 0)
          {
            this.TwLines = true;
            flag = true;
          }
        }
      }
      this.Txt = str1;
    }

    public void DrawTopTextMini(Vector2 Offset)
    {
      if (!this.CenterJustify)
      {
        if (this.TwLines)
        {
          Offset.Y -= this.EX;
          if (this.IsSmall)
          {
            TextFunctions.DrawTextWithDropShadow(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
            Offset.Y += this.EX * 2f;
            TextFunctions.DrawTextWithDropShadow(this.Txt2, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
          }
          else
          {
            TextFunctions.DrawTextWithDropShadow(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
            Offset.Y += this.EX * 2f;
            TextFunctions.DrawTextWithDropShadow(this.Txt2, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
          }
        }
        else if (this.IsSmall)
          TextFunctions.DrawTextWithDropShadow(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
        else
          TextFunctions.DrawTextWithDropShadow(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      }
      else if (this.TwLines)
      {
        Offset.Y -= this.EX;
        if (this.IsSmall)
        {
          TextFunctions.DrawJustifiedText(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
          Offset.Y += this.EX * 2f;
          TextFunctions.DrawJustifiedText(this.Txt2, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
        }
        else
        {
          TextFunctions.DrawJustifiedText(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
          Offset.Y += this.EX * 2f;
          TextFunctions.DrawJustifiedText(this.Txt2, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
        }
      }
      else if (this.IsSmall)
        TextFunctions.DrawJustifiedText(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05);
      else
        TextFunctions.DrawJustifiedText(this.Txt, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}
