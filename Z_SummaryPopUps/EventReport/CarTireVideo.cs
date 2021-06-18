// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.CarTireVideo
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
  internal class CarTireVideo
  {
    private static Rectangle yellowbgrect = new Rectangle(647, 169, 377, 244);
    private static Rectangle nineslicerect = new Rectangle(948, 484, 21, 21);
    private static Rectangle playrect = new Rectangle(251, 827, 106, 106);
    private static Vector2 videorawsize = new Vector2(377f, 244f);
    private static Vector2 greyrawsize_rating = new Vector2(377f, 132f);
    private static Vector2 greyrawsize_play = new Vector2(377f, 196f);
    private ZGenericText finalopinion;
    private Vector2 greyrawsize;
    private LerpHandler_Float lerper;
    private Vector3 lightgrey = new Vector3(0.8235294f, 0.8235294f, 0.8235294f);
    private Vector3 darkgrey = new Vector3(0.3882353f, 0.3882353f, 0.3882353f);
    private GameObject greyscreen;
    private GameObjectNineSlice blackscreen;
    private ZGenericUIDrawObject yellowbg;
    private CarTireGuy tireguy;
    private ZooTubeRating rating;
    private static float startstatedur = 1f;
    private static float playstatedur = 0.5f;
    private float timer;
    private CarTireVideo.CarTireState state;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 recoffset;
    private static Vector2 recmaxoffset;
    private float lerperval;

    public CarTireVideo(ReportResultRank rank, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.greyscreen = new GameObject();
      this.greyscreen.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.greyscreen.SetAllColours(this.lightgrey);
      this.greyscreen.SetDrawOriginToCentre();
      this.blackscreen = new GameObjectNineSlice(CarTireVideo.nineslicerect, 7);
      this.blackscreen.SetAllColours(this.darkgrey);
      this.yellowbg = new ZGenericUIDrawObject(CarTireVideo.yellowbgrect, this.basescale, AssetContainer.UISheet);
      this.tireguy = new CarTireGuy(rank, this.basescale);
      CarTireVideo.recmaxoffset = this.scalehelper.ScaleVector2(new Vector2(0.0f, 30f));
      this.recoffset = new Vector2();
      float rating0to1 = 0.0f;
      switch (rank)
      {
        case ReportResultRank.A:
          rating0to1 = 1f;
          break;
        case ReportResultRank.B:
          rating0to1 = 0.666f;
          break;
        case ReportResultRank.C:
          rating0to1 = 0.333f;
          break;
        case ReportResultRank.F:
          rating0to1 = 0.0f;
          break;
      }
      this.rating = new ZooTubeRating(rating0to1, this.basescale, true);
      this.rating.Alpha = 0.0f;
      this.lerper = new LerpHandler_Float();
      this.finalopinion = new ZGenericText("YUCK OR YUM?", 2f * this.basescale, _UseOnePointFiveFont: true);
      this.finalopinion.SetAlpha(0.0f);
      this.finalopinion.SetAllColours(new Vector3(0.07843138f));
      this.framescale = this.scalehelper.ScaleVector2(CarTireVideo.videorawsize);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.greyrawsize = CarTireVideo.greyrawsize_play;
      this.finalopinion.vLocation.Y = -this.scalehelper.ScaleY(95f);
      this.tireguy.location.Y = (float) (0.5 * (double) this.framescale.Y - 0.5 * (double) this.tireguy.GetSize().Y - 2.0 * (double) defaultBuffer.Y);
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCarTireVideo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.tireguy.UpdateCarTireGuy(player, offset, DeltaTime);
      switch (this.state)
      {
        case CarTireVideo.CarTireState.Start:
          this.timer += DeltaTime;
          if ((double) this.timer > (double) CarTireVideo.startstatedur)
          {
            this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);
            this.state = CarTireVideo.CarTireState.Transition;
            break;
          }
          break;
        case CarTireVideo.CarTireState.Transition:
          this.lerper.UpdateLerpHandler(DeltaTime);
          this.lerperval = this.lerper.Value;
          this.greyrawsize = CarTireVideo.greyrawsize_rating;
          this.greyrawsize.Y += this.lerper.Value * (CarTireVideo.greyrawsize_play.Y - CarTireVideo.greyrawsize_rating.Y);
          this.rating.Alpha = 1f - this.lerper.Value;
          this.finalopinion.SetAlpha(1f - this.lerper.Value);
          this.tireguy.Alpha = this.lerper.Value;
          this.yellowbg.Alpha = this.lerper.Value;
          if (this.lerper.IsComplete())
          {
            this.rating.StartAnimation();
            this.state = CarTireVideo.CarTireState.RateScreen;
            break;
          }
          break;
        case CarTireVideo.CarTireState.RateScreen:
          if (this.rating.UpdateZooTubeRating(player, offset, DeltaTime))
          {
            flag = true;
            this.lerper.SetLerp(true, 0.0f, 1f, 2f);
            this.state = CarTireVideo.CarTireState.Result;
            this.tireguy.ChangeScreen();
            this.recoffset = CarTireVideo.recmaxoffset;
            break;
          }
          break;
        case CarTireVideo.CarTireState.Result:
          this.lerper.UpdateLerpHandler(DeltaTime);
          this.recoffset = (1f - this.lerper.Value) * CarTireVideo.recmaxoffset;
          this.tireguy.Alpha = this.lerper.Value;
          this.yellowbg.Alpha = 0.5f * this.lerper.Value;
          if (this.lerper.IsComplete())
          {
            this.state = CarTireVideo.CarTireState.End;
            break;
          }
          break;
      }
      return flag;
    }

    public void DrawCarTireVideo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.blackscreen.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(CarTireVideo.videorawsize));
      this.greyscreen.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(this.greyrawsize));
      if (this.state >= CarTireVideo.CarTireState.Transition)
        this.rating.DrawZooTubeRating(spritebatch, offset);
      this.finalopinion.DrawZGenericText(offset, spritebatch);
      this.yellowbg.DrawZGenericUIDrawObject(spritebatch, offset);
      this.tireguy.DrawCarTireGuy(spritebatch, offset + this.recoffset);
    }

    private enum CarTireState
    {
      Start,
      PlayScreen,
      Transition,
      RateScreen,
      Result,
      End,
    }
  }
}
