// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ZooTubeVideo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ZooTubeVideo
  {
    private static Rectangle nineslicerect = new Rectangle(948, 484, 21, 21);
    private static Rectangle playrect = new Rectangle(251, 827, 106, 106);
    private static Vector2 videorawsize = new Vector2(377f, 244f);
    private static Vector2 greyrawsize_rating = new Vector2(377f, 132f);
    private static Vector2 greyrawsize_play = new Vector2(377f, 196f);
    private ZGenericText recommended;
    private Vector2 greyrawsize;
    private LerpHandler_Float lerper;
    private Vector3 lightgrey = new Vector3(0.8235294f, 0.8235294f, 0.8235294f);
    private Vector3 darkgrey = new Vector3(0.3882353f, 0.3882353f, 0.3882353f);
    private GameObject greyscreen;
    private GameObjectNineSlice blackscreen;
    private ZGenericUIDrawObject play;
    private ZooTubeRating rating;
    private static float startstatedur = 0.5f;
    private static float playstatedur = 0.5f;
    private float timer;
    private ZooTubeVideo.ZooTubeState state;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private float lerperval;

    public ZooTubeVideo(ReportResultRank rank, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.greyscreen = new GameObject();
      this.greyscreen.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.greyscreen.SetAllColours(this.lightgrey);
      this.greyscreen.SetDrawOriginToCentre();
      this.blackscreen = new GameObjectNineSlice(ZooTubeVideo.nineslicerect, 7);
      this.blackscreen.SetAllColours(this.darkgrey);
      this.play = new ZGenericUIDrawObject(ZooTubeVideo.playrect, this.basescale, AssetContainer.UISheet);
      string _textToWrite = "";
      Vector3 Clr = new Vector3();
      float rating0to1 = 0.0f;
      switch (rank)
      {
        case ReportResultRank.A:
          _textToWrite = "HIGHLY RECOMMENDED";
          Clr = ColourData.Z_TextGreen;
          rating0to1 = 1f;
          break;
        case ReportResultRank.B:
          _textToWrite = "RECOMMENDED";
          Clr = ColourData.Z_TextGreen;
          rating0to1 = 0.666f;
          break;
        case ReportResultRank.C:
          _textToWrite = "DECENT";
          Clr = ColourData.IconYellow;
          rating0to1 = 0.333f;
          break;
        case ReportResultRank.F:
          _textToWrite = "NOT RECOMMENDED";
          Clr = ColourData.Z_ArrowAndTextRed;
          rating0to1 = 0.0f;
          break;
      }
      this.rating = new ZooTubeRating(rating0to1, this.basescale);
      this.rating.Alpha = 0.0f;
      this.lerper = new LerpHandler_Float();
      this.recommended = new ZGenericText(_textToWrite, 2f * this.basescale, _UseOnePointFiveFont: true);
      this.recommended.SetAllColours(Clr);
      this.recommended.SetAlpha(0.0f);
      this.framescale = this.scalehelper.ScaleVector2(ZooTubeVideo.videorawsize);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.greyrawsize = ZooTubeVideo.greyrawsize_play;
      this.recommended.vLocation.Y = this.scalehelper.ScaleY(95f);
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateZooTubeVideo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      switch (this.state)
      {
        case ZooTubeVideo.ZooTubeState.Start:
          this.timer += DeltaTime;
          if ((double) this.timer > (double) ZooTubeVideo.startstatedur)
          {
            this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);
            this.state = ZooTubeVideo.ZooTubeState.PlayScreen;
            break;
          }
          break;
        case ZooTubeVideo.ZooTubeState.PlayScreen:
          this.lerper.UpdateLerpHandler(DeltaTime);
          this.play.Alpha = this.lerper.Value;
          if (this.lerper.IsComplete())
          {
            this.lerper.SetLerp(true, 1f, 0.0f, 2f, true);
            this.state = ZooTubeVideo.ZooTubeState.Transition;
            this.lerperval = 0.0f;
            break;
          }
          break;
        case ZooTubeVideo.ZooTubeState.Transition:
          this.lerper.UpdateLerpHandler(DeltaTime);
          this.lerperval = this.lerper.Value;
          this.greyrawsize = ZooTubeVideo.greyrawsize_rating;
          this.greyrawsize.Y += this.lerper.Value * (ZooTubeVideo.greyrawsize_play.Y - ZooTubeVideo.greyrawsize_rating.Y);
          this.rating.Alpha = 1f - this.lerper.Value;
          if (this.lerper.IsComplete())
          {
            this.rating.StartAnimation();
            this.state = ZooTubeVideo.ZooTubeState.RateScreen;
            break;
          }
          break;
        case ZooTubeVideo.ZooTubeState.RateScreen:
          if (this.rating.UpdateZooTubeRating(player, offset, DeltaTime))
          {
            flag = true;
            this.recommended.SetAlpha(1f);
            this.state = ZooTubeVideo.ZooTubeState.End;
            break;
          }
          break;
      }
      return flag;
    }

    public void DrawZooTubeVideo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.blackscreen.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(ZooTubeVideo.videorawsize));
      this.greyscreen.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(this.greyrawsize));
      if (this.state <= ZooTubeVideo.ZooTubeState.PlayScreen)
        this.play.DrawZGenericUIDrawObject(spritebatch, offset);
      if (this.state >= ZooTubeVideo.ZooTubeState.Transition)
        this.rating.DrawZooTubeRating(spritebatch, offset);
      this.recommended.DrawZGenericText(offset, spritebatch);
    }

    private enum ZooTubeState
    {
      Start,
      PlayScreen,
      Transition,
      RateScreen,
      End,
    }
  }
}
