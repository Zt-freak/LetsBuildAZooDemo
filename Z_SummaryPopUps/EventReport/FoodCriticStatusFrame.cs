// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.FoodCriticStatusFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class FoodCriticStatusFrame : ReportStatusFrame
  {
    private CustomerFrame frame;
    private static Rectangle frameTop = new Rectangle(609, 121, 416, 44);
    private static Rectangle frameSide = new Rectangle(0, 359, 1, 331);
    private static Rectangle frameBottom = new Rectangle(608, 166, 416, 1);
    private CarTireBackground bg;
    private ZGenericText name;
    private CarTireVideo video;
    private static Rectangle logorect = new Rectangle(0, 859, 108, 26);
    private ZGenericText title;
    private ZGenericText uploaded;

    public FoodCriticStatusFrame(AnimalType persontype, ReportResultRank rank, float basescale_)
      : base(basescale_)
    {
      this.video = new CarTireVideo(rank, this.basescale);
      this.title = new ZGenericText("CAR TIRE FOOD REVIEW", 2f * this.basescale, _UseOnePointFiveFont: true);
      this.uploaded = new ZGenericText("Posted 24th May", this.basescale, false, _UseOnePointFiveFont: true);
      this.bg = new CarTireBackground(this.basescale);
      Vector3 Clr = new Vector3(0.07843138f);
      this.name = new ZGenericText("DEWPIEDIE", 1.5f * this.basescale, AssetContainer.SpringFontX1AndHalf, false);
      this.name.SetAllColours(Clr);
      this.uploaded.SetAllColours(ColourData.Z_DarkTextGray);
      this.title.SetAllColours(ColourData.Z_DarkTextGray);
      this.framescale = this.bg.GetSize();
      Vector2 framescale = this.framescale;
      this.framescale = this.framescale + 6f * this.pad;
      this.frame = new CustomerFrame(this.framescale, new Vector3(1f), this.basescale);
      Vector2 vector2 = -0.5f * framescale;
      vector2.X = 0.0f;
      vector2.Y += 2f * this.pad.Y;
      this.title.vLocation = vector2;
      this.title.vLocation.Y += 0.5f * this.title.GetSize().Y;
      vector2.Y += this.title.GetSize().Y;
      this.video.location = vector2;
      this.video.location.Y += 0.5f * this.video.GetSize().Y;
      vector2.Y += this.video.GetSize().Y;
      vector2.X = (float) (-0.5 * (double) framescale.X + 2.0 * (double) this.pad.X);
      this.uploaded.vLocation = vector2;
      this.name.vLocation = vector2;
      this.name.vLocation.Y -= this.name.GetSize().Y;
    }

    public override bool UpdateReportStatusFrame(Player player, Vector2 offset, float DeltaTime) => (0 | (this.video.UpdateCarTireVideo(player, offset, DeltaTime) ? 1 : 0)) != 0;

    public override void DrawReportStatusFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.bg.DrawCarTireBackground(spritebatch, offset);
      this.video.DrawCarTireVideo(spritebatch, offset);
      this.title.DrawZGenericText(offset, spritebatch);
      this.uploaded.DrawZGenericText(offset, spritebatch);
    }
  }
}
