// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.SuccesRate
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_TicketPrice.Panel;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage
{
  internal class SuccesRate
  {
    private PieChart piechart;
    public Vector2 Location;
    private ZGenericText pastSuccessText;
    private float[] FullnessValues;
    public CustomerFrame customerFrame;
    private MiniHeading miniHeading;

    public SuccesRate(Parents_AndChild parents_and_child, float forceWidth, float BaseScale)
    {
      float defaultYbuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      float num1 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, "Breeding History", 1f, BaseScale);
      float num2 = num1 + (this.miniHeading.GetTextHeight(true) + defaultYbuffer) + defaultYbuffer;
      this.piechart = new PieChart(new string[2]
      {
        "Births",
        "Pregnancy Attempts"
      }, BaseScale: BaseScale);
      this.FullnessValues = new float[2];
      if (parents_and_child.Attempts > 0)
      {
        this.FullnessValues[0] = (float) parents_and_child.Births / (float) parents_and_child.Attempts;
        this.FullnessValues[1] = 1f - this.FullnessValues[0];
      }
      else
      {
        this.FullnessValues[0] = 0.0f;
        this.FullnessValues[1] = 1f;
      }
      int births = parents_and_child.Births;
      int attempts = parents_and_child.Attempts;
      this.piechart.SetValues(this.FullnessValues);
      this.piechart.location.Y = num2;
      this.piechart.location.Y += this.piechart.GetSize().Y * 0.5f;
      float num3 = num2 + this.piechart.GetSize().Y + defaultYbuffer;
      this.pastSuccessText = new ZGenericText(BaseScale);
      this.pastSuccessText.textToWrite = string.Format("Success Rate: {0}/{1}", (object) births, (object) attempts);
      float y1 = this.pastSuccessText.GetSize().Y;
      this.pastSuccessText.vLocation.Y = num3 + y1 * 0.5f;
      float y2 = num3 + y1 + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(forceWidth, y2), CustomerFrameColors.Brown, BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.piechart.location.Y += vector2.Y;
      this.pastSuccessText.vLocation.Y += vector2.Y;
    }

    public void UpdateSuccesRate()
    {
    }

    public void DrawSuccesRate(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      this.miniHeading.DrawMiniHeading(Offset, spritebatch);
      this.pastSuccessText.DrawZGenericText(Offset, spritebatch);
      this.piechart.DrawPieChart(spritebatch, Offset);
    }
  }
}
