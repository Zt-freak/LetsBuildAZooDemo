// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.AnimalEncounterPenStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class AnimalEncounterPenStats
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private MiniHeading heading;
    private PortraitRow portraits;
    private LabelledBar threat;
    private LabelledBar hunger;
    private LabelledBar amusement;
    private ZGenericText text;
    private bool penPicked;

    public AnimalEncounterPenStats(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.penPicked = false;
      this.portraits = new PortraitRow(6, this.basescale, this.uiscale.ScaleX(40f));
      this.hunger = new LabelledBar((float) Game1.Rnd.NextDouble(), ColourData.Z_TextOrange, "Animal Hunger", this.basescale);
      this.threat = new LabelledBar((float) Game1.Rnd.NextDouble(), ColourData.Z_TextRed, "Threat", this.basescale);
      this.amusement = new LabelledBar((float) Game1.Rnd.NextDouble(), ColourData.Z_TextGreen, "Customer Happiness", this.basescale);
      this.heading = new MiniHeading(Vector2.Zero, "Pen Information", 1f, this.basescale);
      this.text = new ZGenericText("No pen selected", this.basescale);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.portraits.Add(AnimalType.Capybara, AnimalType.None);
      this.framescale = this.hunger.GetBarSize();
      this.framescale.X = this.uiscale.ScaleX(255.5f);
      this.framescale.Y = 1f * defaultBuffer.Y;
      this.framescale.Y += this.hunger.GetSize().Y + defaultBuffer.Y;
      this.framescale.Y += this.threat.GetSize().Y + defaultBuffer.Y;
      this.framescale.Y += this.amusement.GetSize().Y + defaultBuffer.Y;
      this.framescale.Y += this.portraits.GetSize().Y + defaultBuffer.Y;
      this.framescale.Y += this.heading.GetTextHeight() + defaultBuffer.Y;
      this.heading.SetTextPosition(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale;
      vector2.Y += 1.5f * defaultBuffer.Y;
      vector2.Y += this.heading.GetSize().Y;
      this.portraits.location = vector2;
      this.portraits.location.X = 0.0f;
      this.portraits.location.Y += 0.5f * this.portraits.GetSize().Y;
      vector2.Y += this.portraits.GetSize().Y + defaultBuffer.Y;
      this.hunger.location = vector2;
      this.hunger.location.X = 0.0f;
      vector2.Y += this.hunger.GetSize().Y + defaultBuffer.Y;
      this.threat.location = vector2;
      this.threat.location.X = 0.0f;
      vector2.Y += this.threat.GetSize().Y + defaultBuffer.Y;
      this.amusement.location = vector2;
      this.amusement.location.X = 0.0f;
      vector2.Y += this.amusement.GetSize().Y + defaultBuffer.Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
    }

    public Vector2 GetSize() => this.framescale;

    public void SetMode(bool penPicked_) => this.penPicked = penPicked_;

    public bool UpdateAnimalEncounterPenStats(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawAnimalEncounterPenStats(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if (this.penPicked)
      {
        this.hunger.DrawLabelledBar(offset, spritebatch);
        this.threat.DrawLabelledBar(offset, spritebatch);
        this.amusement.DrawLabelledBar(offset, spritebatch);
        this.portraits.DrawPortraitRow(spritebatch, offset);
      }
      else
        this.text.DrawZGenericText(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
    }
  }
}
