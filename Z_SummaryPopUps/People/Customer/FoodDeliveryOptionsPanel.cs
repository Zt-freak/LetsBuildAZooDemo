// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.FoodDeliveryOptionsPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class FoodDeliveryOptionsPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private TextButton button;
    private ZGenericText price;
    private SelectorBarWithLabels selector;
    private MiniHeading heading;

    public int Selected => this.selector.Selected;

    public FoodDeliveryOptionsPanel(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.price = new ZGenericText("sOmE Arbitrary texT", this.basescale, _UseOnePointFiveFont: true);
      this.button = new TextButton(this.basescale, "Order", 40f);
      this.selector = new SelectorBarWithLabels(this.basescale);
      this.selector.Add("Cheap");
      this.selector.Add("Decent");
      this.selector.Add("Above Average");
      this.selector.Add("Good");
      this.selector.Add("Gourmet");
      this.heading = new MiniHeading(Vector2.Zero, "FOOD QUALITY", 1f, this.basescale);
      this.framescale = 2f * defaultBuffer;
      this.framescale += this.selector.GetSize();
      this.framescale.Y += defaultBuffer.Y;
      this.framescale.Y += this.heading.GetSize().Y;
      this.framescale.Y += this.price.GetSize().Y;
      this.framescale.Y += this.button.GetSize_True().Y + defaultBuffer.Y;
      this.framescale.X = this.scalehelper.ScaleX(220f);
      this.heading.SetTextPosition(this.framescale);
      this.frame = new CustomerFrame(this.framescale, BaseScale: (1f * this.basescale));
      Vector2 vector2 = defaultBuffer;
      vector2.Y += -0.5f * this.framescale.Y;
      vector2.Y += this.heading.GetSize().Y;
      vector2.Y += defaultBuffer.Y;
      this.price.vLocation = vector2;
      this.price.vLocation.X = 0.0f;
      this.price.vLocation.Y += 0.5f * this.price.GetSize().Y;
      vector2.Y += this.price.GetSize().Y;
      this.selector.location = vector2;
      this.selector.location.X = 0.0f;
      this.selector.location.Y += 0.5f * this.selector.GetSize().Y;
      vector2.Y += this.selector.GetSize().Y + defaultBuffer.Y;
      this.button.vLocation = vector2;
      this.button.vLocation.Y += 0.5f * this.button.GetSize_True().Y;
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.button.GetSize_True().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateFoodDeliveryOptionsPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.selector.UpdateSelectorBarWithLabels(player, offset, DeltaTime);
      if (this.selector.Selected == -1)
      {
        this.price.textToWrite = "";
        this.button.SetButtonColour(BTNColour.Grey);
      }
      else
      {
        flag |= this.button.UpdateTextButton(player, offset, DeltaTime);
        this.button.SetButtonColour(BTNColour.Green);
        if (this.selector.Selected == 0)
          this.price.textToWrite = "$20";
        else if (this.selector.Selected == 1)
          this.price.textToWrite = "$40";
        else if (this.selector.Selected == 2)
          this.price.textToWrite = "$80";
        else if (this.selector.Selected == 3)
          this.price.textToWrite = "$160";
        else if (this.selector.Selected == 4)
          this.price.textToWrite = "$320";
      }
      return flag;
    }

    public void DrawFoodDeliveryOptionsPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.selector.DrawSelectorBarWithLabels(spritebatch, offset);
      this.price.DrawZGenericText(offset, spritebatch);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
