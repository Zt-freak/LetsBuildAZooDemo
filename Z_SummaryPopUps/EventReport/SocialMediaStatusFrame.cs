// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.SocialMediaStatusFrame
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
  internal class SocialMediaStatusFrame : ReportStatusFrame
  {
    private CustomerFrame frame;
    private static Rectangle logorect = new Rectangle(0, 859, 108, 26);
    private ZGenericUIDrawObject logo;
    private ZGenericText title;
    private ZGenericText clickbait;
    private ZGenericText uploaded;
    private ZGenericText viewcount;
    private ZGenericText subscribercount;
    private ZGenericText name;
    private RoundFramePortrait portrait;
    private ZooTubeVideo video;

    public SocialMediaStatusFrame(AnimalType persontype, ReportResultRank rank, float basescale_)
      : base(basescale_)
    {
      this.video = new ZooTubeVideo(rank, this.basescale);
      this.logo = new ZGenericUIDrawObject(SocialMediaStatusFrame.logorect, this.basescale, AssetContainer.UISheet);
      this.title = new ZGenericText("MY TERRIBLE DAY AT THE ZOO!!", this.basescale, false, _UseOnePointFiveFont: true);
      this.clickbait = new ZGenericText("(YOU WON'T BELIEVE WHAT HAPPENED)", this.basescale, false, _UseOnePointFiveFont: true);
      this.uploaded = new ZGenericText("Uploaded 13 mins ago", this.basescale, false, _UseOnePointFiveFont: true);
      this.viewcount = new ZGenericText("201,754 views", this.basescale, false, true, true);
      this.portrait = new RoundFramePortrait(this.basescale, persontype);
      Vector3 Clr = new Vector3(0.07843138f);
      this.title.SetAllColours(Clr);
      this.clickbait.SetAllColours(Clr);
      this.name = new ZGenericText("DEWPIEDIE", 1.5f * this.basescale, AssetContainer.SpringFontX1AndHalf, false);
      this.name.SetAllColours(Clr);
      this.subscribercount = new ZGenericText("121K subscribers", this.basescale, false, _UseOnePointFiveFont: true);
      this.subscribercount.SetAllColours(ColourData.Z_DarkTextGray);
      this.uploaded.SetAllColours(ColourData.Z_DarkTextGray);
      this.viewcount.SetAllColours(ColourData.Z_DarkTextGray);
      this.framescale = this.video.GetSize();
      this.framescale.Y += this.title.GetSize().Y;
      this.framescale.Y += this.clickbait.GetSize().Y;
      this.framescale.Y += this.logo.GetSize().Y + this.pad.Y;
      this.framescale.Y += this.portrait.GetSize().Y + this.pad.Y;
      this.framescale = this.framescale + 6f * this.pad;
      this.frame = new CustomerFrame(this.framescale, new Vector3(1f), this.basescale);
      Vector2 vector2_1 = -0.5f * this.framescale + this.pad;
      this.logo.location = vector2_1 + 0.5f * this.logo.GetSize();
      vector2_1.Y += this.logo.GetSize().Y;
      Vector2 vector2_2 = vector2_1 + 2f * this.pad;
      this.title.vLocation = vector2_2;
      vector2_2.Y += this.title.GetSize().Y;
      this.clickbait.vLocation = vector2_2;
      vector2_2.Y += this.clickbait.GetSize().Y;
      this.video.location = vector2_2 + 0.5f * this.video.GetSize();
      vector2_2.Y += this.video.GetSize().Y;
      this.uploaded.vLocation = vector2_2;
      this.viewcount.vLocation = vector2_2;
      this.viewcount.vLocation.X += this.video.GetSize().X;
      vector2_2.Y += this.viewcount.GetSize().Y + this.pad.Y;
      this.portrait.location = vector2_2 + 0.5f * this.portrait.GetSize();
      vector2_2.X += this.portrait.GetSize().X + this.pad.X;
      vector2_2.Y += 0.5f * this.portrait.GetSize().Y;
      this.name.vLocation = vector2_2;
      this.name.vLocation.Y -= this.name.GetSize().Y;
      this.subscribercount.vLocation = vector2_2;
      vector2_2.Y += this.portrait.GetSize().Y;
    }

    public override bool UpdateReportStatusFrame(Player player, Vector2 offset, float DeltaTime) => (0 | (this.video.UpdateZooTubeVideo(player, offset, DeltaTime) ? 1 : 0)) != 0;

    public override void DrawReportStatusFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.portrait.DrawRoundFramePortrait(spritebatch, offset);
      this.name.DrawZGenericText(offset, spritebatch);
      this.subscribercount.DrawZGenericText(offset, spritebatch);
      this.video.DrawZooTubeVideo(spritebatch, offset);
      this.logo.DrawZGenericUIDrawObject(spritebatch, offset);
      this.title.DrawZGenericText(offset, spritebatch);
      this.clickbait.DrawZGenericText(offset, spritebatch);
      this.uploaded.DrawZGenericText(offset, spritebatch);
      this.viewcount.DrawZGenericText(offset, spritebatch);
    }
  }
}
