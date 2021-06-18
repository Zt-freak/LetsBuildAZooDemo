// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.NewsCaster.NewsText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Text;

namespace TinyZoo.Z_Events.NewsCaster
{
  internal class NewsText
  {
    private GameObject MainBar;
    private Vector2 VScale;
    private GameObject LOGO;
    private StringScroller scroller;
    private string Heading;
    private LerpHandler_Float lerper;
    private float BaseScale;

    public NewsText(string _Heading, string Body)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.Heading = "Breaking News!      " + _Heading;
      this.MainBar = new GameObject();
      this.MainBar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.MainBar.SetDrawOriginToCentre();
      this.VScale = new Vector2(1024f, 100f);
      this.MainBar.vLocation = new Vector2(512f, 718f);
      this.LOGO = new GameObject();
      this.LOGO.DrawRect = new Rectangle(0, 216, 80, 45);
      this.LOGO.SetDrawOriginToCentre();
      this.scroller = new StringScroller((float) (870.0 * (1.0 / (double) this.BaseScale)), Body, AssetContainer.springFont, 2f, 2f);
      this.LOGO.scale = this.BaseScale;
      this.lerper = new LerpHandler_Float();
    }

    public void SetNewBodyText(string Body) => this.scroller = new StringScroller((float) (870.0 * (1.0 / (double) this.BaseScale)), Body, AssetContainer.springFont, 2f, 2f);

    public void UpdateNewsText(float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0)
        return;
      this.scroller.UpdateStringScroller(DeltaTime);
    }

    public void DrawNewsText()
    {
      this.MainBar.SetAllColours(1f, 1f, 1f);
      this.MainBar.vLocation = new Vector2((float) (512.0 + (double) this.lerper.Value * 1024.0), (float) (768.0 - 25.0 * (double) this.BaseScale));
      this.MainBar.fAlpha = 0.6f;
      this.MainBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, new Vector2(1024f, 50f * this.BaseScale));
      this.MainBar.SetAllColours(1f, 1f, 0.9f);
      this.MainBar.fAlpha = 1f;
      this.MainBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, new Vector2((float) (1024.0 - 10.0 * (double) this.BaseScale), 40f * this.BaseScale));
      this.MainBar.vLocation.X = 55f * this.BaseScale;
      this.MainBar.SetAllColours(0.7f, 0.1f, 0.1f);
      this.MainBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, new Vector2(100f * this.BaseScale, 40f * this.BaseScale));
      this.LOGO.scale = this.BaseScale * 0.5f;
      this.LOGO.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.MainBar.vLocation);
      if ((double) this.lerper.Value != 0.0)
        return;
      TextFunctions.DrawTextWithDropShadow(this.Heading, this.BaseScale, new Vector2(115f * this.BaseScale, this.MainBar.vLocation.Y - 15f * this.BaseScale), this.MainBar.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, true);
      TextFunctions.DrawTextWithDropShadow(this.scroller.GetString(), this.BaseScale, new Vector2(115f * this.BaseScale, this.MainBar.vLocation.Y - 0.0f), Color.DarkBlue, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
    }
  }
}
