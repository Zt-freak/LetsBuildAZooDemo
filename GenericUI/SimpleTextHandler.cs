// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.SimpleTextHandler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Text;
using TinyZoo.PlayerDir;

namespace TinyZoo.GenericUI
{
  internal class SimpleTextHandler
  {
    public ParagraphTeletext paragraph;
    private bool CentreJustify;
    public Vector2 Location;
    private SpringFont fontpointer;
    private float width;

    public SimpleTextHandler(
      string TextToWrite,
      float width_,
      bool _CentreJustify = false,
      float _Scale = 1f,
      bool _UseFontOnePointFive = false,
      bool AutoComplete = false,
      SpringFont ForceThisFont = null)
    {
      this.width = width_;
      if (_UseFontOnePointFive)
        this.fontpointer = AssetContainer.SpringFontX1AndHalf;
      else if (ForceThisFont != null)
      {
        this.fontpointer = ForceThisFont;
      }
      else
      {
        this.fontpointer = AssetContainer.springFont;
        if ((double) PlayerStats.UXMult % 2.0 == 0.0)
          this.fontpointer = AssetContainer.SinglePixelFontX1AndHalf;
        _Scale = Z_GameFlags.GetBaseScaleForCustomFont(_Scale);
      }
      float PercentagePfScreenWidth = this.width / Sengine.ReferenceScreenRes.X;
      this.SetUp(TextToWrite, _CentreJustify, PercentagePfScreenWidth, _Scale, AutoComplete);
    }

    public SimpleTextHandler(
      string TextToWrite,
      bool _CentreJustify = false,
      float PercentagePfScreenWidth = 0.9f,
      float _Scale = 3f,
      bool _UseFontOnePointFive = false,
      bool AutoComplete = false)
    {
      this.width = PercentagePfScreenWidth * Sengine.ReferenceScreenRes.X;
      if (_UseFontOnePointFive)
      {
        this.fontpointer = AssetContainer.SpringFontX1AndHalf;
      }
      else
      {
        this.fontpointer = AssetContainer.springFont;
        if ((double) PlayerStats.UXMult % 2.0 == 0.0)
          this.fontpointer = AssetContainer.SinglePixelFontX1AndHalf;
        _Scale = Z_GameFlags.GetBaseScaleForCustomFont(_Scale);
      }
      this.SetUp(TextToWrite, _CentreJustify, PercentagePfScreenWidth, _Scale, AutoComplete);
    }

    public void AddLine(string AddThis) => this.paragraph.AddLine(AddThis, this.fontpointer);

    public void AddLine(string AddThis, Vector3 Clr) => this.paragraph.AddLine(AddThis, this.fontpointer, Clr);

    private void SetUp(
      string TextToWrite,
      bool _CentreJustify,
      float PercentagePfScreenWidth,
      float TextSize,
      bool AutoComplete = false)
    {
      this.CentreJustify = _CentreJustify;
      float ActualTextScale = TextSize;
      this.paragraph = new ParagraphTeletext(false);
      double num = (double) this.paragraph.SetUpParagraphScaleCorrect(this.fontpointer, TextToWrite, Sengine.ReferenceScreenRes.X * PercentagePfScreenWidth, ActualTextScale, TinyZoo.FlagSettings.LineHeightModifier);
      this.paragraph.SetUpperCase(false);
      this.paragraph.linemaker.SetTeletypeSpeed(3f);
      if (!AutoComplete)
        return;
      this.AutoCompleteParagraph();
    }

    public void SetLineLimit_BeforeScroll(
      int LineMax,
      bool AutoScroll = false,
      float _HoldBeforeAutoScroll = -1f)
    {
      this.paragraph.SetLineLimit_BeforeScroll(LineMax, AutoScroll, _HoldBeforeAutoScroll);
    }

    public void SetAllColours(Vector3 Colourrr) => this.paragraph.linemaker.SetAllColours(Colourrr);

    public void SetAlpha(float NewAlphs) => this.paragraph.linemaker.SetAlpha(NewAlphs);

    public void AutoCompleteParagraph() => this.paragraph.AutoCompleteParagraph();

    public void UpdateSimpleTextHandler(float DeltaTime) => this.paragraph.UpdateParagraphTeleText(DeltaTime);

    public float GetSubSCrollerOffset() => this.paragraph.GetSubSCrollerOffset();

    public bool HasIncompleteSubScroller() => this.paragraph.HasIncompleteSubScroller();

    public bool IsComplete() => this.paragraph.ParagraphIsComplete;

    public bool TryToCompleteParagraph()
    {
      bool flag = false;
      if (this.paragraph.linemaker.ParagraphComplete)
        flag = true;
      else
        this.paragraph.AutoCompleteParagraph();
      return flag;
    }

    public void DrawSimpleTextHandler(Vector2 Offset, float Alpha, SpriteBatch UseThis)
    {
      Offset += this.Location;
      this.paragraph.linemaker.fAlpha = Alpha;
      this.paragraph.DrawParagraphTeletext(this.fontpointer, UseThis, Offset, this.CentreJustify);
    }

    public void DrawSimpleTextHandler(Vector2 Offset, float Alpha = 1f, bool UseTopBatch = true)
    {
      if (UseTopBatch)
        this.DrawSimpleTextHandler(Offset, Alpha, AssetContainer.pointspritebatch03);
      else
        this.DrawSimpleTextHandler(Offset, Alpha, AssetContainer.pointspritebatch03);
    }

    public Vector2 GetSize(bool DoThisCorrectly = false) => DoThisCorrectly ? new Vector2(this.paragraph.GetSize(DoThisCorrectly).X, this.GetHeightOfParagraph()) : new Vector2(this.width, this.GetHeightOfParagraph());

    public float GetHeightOfParagraph() => this.paragraph.GetHeightOfParagraph();

    public float GetHeightOfOneLine() => this.paragraph.GetHeightOfLines(1);
  }
}
