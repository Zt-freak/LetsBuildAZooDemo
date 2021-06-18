// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.StrikeInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class StrikeInfo : VIPInfo
  {
    private static Rectangle paperbg = new Rectangle(0, 1025, 455, 405);
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private ZGenericUIDrawObject background;
    private ScrollingList list;
    private PortraitRow portraits;
    private ZGenericText noticeheading;
    private ZGenericText striketext;
    private ZGenericText numdaystext;
    private ZGenericText strikeconfidencetext;
    private ZGenericText bestoffertext;
    private ZGenericText numpeopletext;
    private ZGenericText reportheading;
    private ZGenericText reasonheading;
    private SimpleTextHandler reasontext;
    private int numdays;
    private int numpeople;

    public StrikeInfo(WalkingPerson person, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.numdays = 6;
      float num1 = 0.7f;
      float num2 = 50f;
      this.noticeheading = new ZGenericText("NOTICE", this.basescale, false, _UseOnePointFiveFont: true);
      this.striketext = new ZGenericText("Your staff is on strike!", this.basescale, false);
      this.reasonheading = new ZGenericText("REASON", this.basescale, false, _UseOnePointFiveFont: true);
      this.numdaystext = new ZGenericText("Days on strike: " + (object) this.numdays, this.basescale, false);
      this.strikeconfidencetext = new ZGenericText("Strike Confidence: " + (object) (float) ((double) num1 * 100.0), this.basescale, false);
      this.bestoffertext = new ZGenericText("Best Offer: " + (object) num2, this.basescale, false);
      this.numpeopletext = new ZGenericText("People on strike: " + (object) this.numpeople, this.basescale, false);
      this.reportheading = new ZGenericText("STAFF STRIKE REPORT", this.basescale, false, _UseOnePointFiveFont: true);
      this.noticeheading.SetAllColours(ColourData.Z_DarkTextGray);
      this.striketext.SetAllColours(ColourData.Z_DarkTextGray);
      this.strikeconfidencetext.SetAllColours(ColourData.Z_DarkTextGray);
      this.bestoffertext.SetAllColours(ColourData.Z_DarkTextGray);
      this.numdaystext.SetAllColours(ColourData.Z_DarkTextGray);
      this.numpeopletext.SetAllColours(ColourData.Z_DarkTextGray);
      this.reportheading.SetAllColours(ColourData.Z_DarkTextGray);
      this.reasonheading.SetAllColours(ColourData.Z_DarkTextGray);
      this.background = new ZGenericUIDrawObject(StrikeInfo.paperbg, this.basescale, AssetContainer.UISheet);
      this.portraits = new PortraitRow(9, this.basescale);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.portraits.Add(AnimalType.SlaughterhouseEmployeeAsian, AnimalType.None);
      this.list = new ScrollingList(this.basescale, 6, true, CustomerFrameColors.DarkerCream, 0.5f);
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee1", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee2", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee3", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee4", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee5", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee6", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee7", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee8", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee9", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee10", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee11", 100, this.basescale));
      this.list.Add((ScrollingListEntry) new StrikeListEntry("employee12", 100, this.basescale));
      this.list.SetEmptyEntrySize(new StrikeListEntry("EMPLOYEE", 100, this.basescale).GetSize());
      this.framescale = new Vector2();
      this.framescale = this.scalehelper.ScaleVector2(new Vector2((float) StrikeInfo.paperbg.Width, (float) StrikeInfo.paperbg.Height));
      this.framescale += 2f * defaultBuffer;
      this.reasontext = new SimpleTextHandler("We are overworked and underpaid and i need more text to check if this is scaled properly", this.framescale.X - 6f * defaultBuffer.X, _Scale: this.basescale);
      this.reasontext.SetAllColours(ColourData.Z_DarkTextGray);
      this.reasontext.AutoCompleteParagraph();
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      Vector2 vector2_1 = -0.5f * this.framescale + defaultBuffer + 2f * defaultBuffer;
      vector2_1.X += 1f * defaultBuffer.X;
      this.reportheading.vLocation = vector2_1;
      vector2_1.Y += this.reportheading.GetSize().Y;
      this.numpeopletext.vLocation = vector2_1;
      vector2_1.Y += this.numpeopletext.GetSize().Y;
      this.numdaystext.vLocation = vector2_1;
      vector2_1.Y += this.numdaystext.GetSize().Y;
      this.strikeconfidencetext.vLocation = vector2_1;
      vector2_1.Y += this.strikeconfidencetext.GetSize().Y;
      this.bestoffertext.vLocation = vector2_1;
      vector2_1.Y += this.bestoffertext.GetSize().Y;
      Vector2 vector2_2 = -0.5f * this.framescale;
      vector2_2.Y += this.scalehelper.ScaleY(94f);
      vector2_2.Y += defaultBuffer.Y;
      vector2_2.X += 4f * defaultBuffer.X;
      this.portraits.location = vector2_2 + 0.5f * this.portraits.GetSize();
      this.portraits.location.X = 0.0f;
      vector2_2.Y += this.portraits.GetSize().Y + defaultBuffer.Y;
      this.list.location = vector2_2 + 0.5f * this.list.GetSize();
      this.list.location.X = 0.0f;
      vector2_2.Y += this.list.GetSize().Y + defaultBuffer.Y;
      this.reasonheading.vLocation = vector2_2;
      vector2_2.Y += this.reasonheading.GetSize().Y;
      this.reasontext.Location = vector2_2;
      vector2_2.Y += this.reasontext.GetSize().Y + defaultBuffer.Y;
      Vector2 vector2_3 = -0.5f * this.framescale + this.scalehelper.ScaleVector2(new Vector2(260f, 30f));
      this.noticeheading.vLocation = vector2_3;
      vector2_3.Y += this.noticeheading.GetSize().Y;
      this.striketext.vLocation = vector2_3;
      vector2_3.Y += this.striketext.vLocation.Y;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.list.UpdateScrollingList(player, offset, DeltaTime);
      return false;
    }

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.background.DrawZGenericUIDrawObject(spritebatch, offset);
      this.portraits.DrawPortraitRow(spritebatch, offset);
      this.list.DrawScrollingList(spritebatch, offset);
      this.noticeheading.DrawZGenericText(offset, spritebatch);
      this.reportheading.DrawZGenericText(offset, spritebatch);
      this.strikeconfidencetext.DrawZGenericText(offset, spritebatch);
      this.numdaystext.DrawZGenericText(offset, spritebatch);
      this.striketext.DrawZGenericText(offset, spritebatch);
      this.bestoffertext.DrawZGenericText(offset, spritebatch);
      this.numpeopletext.DrawZGenericText(offset, spritebatch);
      this.reasonheading.DrawZGenericText(offset, spritebatch);
      this.reasontext.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
