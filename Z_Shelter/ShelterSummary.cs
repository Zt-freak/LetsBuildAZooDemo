// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Shelter.ShelterSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Shelter
{
  internal class ShelterSummary
  {
    private CustomerFrame customerframe;
    private SimpleTextHandler simpletext;
    public Vector2 Location;

    public ShelterSummary(Player player)
    {
      this.SetUpPara(RenderMath.GetNearestPerfectPixelZoom(1f), 0.8f, player);
      this.customerframe = new CustomerFrame(new Vector2(900f, 40f), true);
    }

    public ShelterSummary(
      float BaseScale,
      float paraWidthPercent,
      Player player,
      bool IsBlackMarket = false)
    {
      this.SetUpPara(BaseScale, paraWidthPercent, player, IsBlackMarket);
    }

    private void SetUpPara(float scale, float paraWidthPercent, Player player, bool IsBlackMarket = false)
    {
      string TextToWrite = "Here is where animals that have been rescued from abuse, or saved due to erosion of their natural habitats, are looked after. Can you offer them a new home?";
      if (IsBlackMarket)
      {
        TextToWrite = "You won't find these animals anywhere else. They are sure to add variety to your zoo, and are selling at some of the best rates ever.";
        if (Z_DebugFlags.IsBetaVersion && Player.criticalchoices.BadChoicesMade > 0)
          TextToWrite = "Since you chose to turn to the dark side by painting that Horse to look like a zebra, I think I can give you a better deal";
      }
      this.simpletext = new SimpleTextHandler(TextToWrite, true, paraWidthPercent, scale);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.Y += this.simpletext.GetHeightOfOneLine() * 0.5f;
    }

    public float GetHeight()
    {
      if (this.customerframe != null)
        return this.customerframe.VSCale.Y;
      return this.simpletext.paragraph.linemaker.GetNumberOfLines() == 0 ? 0.0f : this.simpletext.GetHeightOfParagraph();
    }

    public void UpdateShelterSummary()
    {
    }

    public void DrawShelterSummary(Vector2 Offset) => this.DrawShelterSummary(Offset, AssetContainer.pointspritebatch03);

    public void DrawShelterSummary(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      if (this.customerframe != null)
        this.customerframe.DrawCustomerFrame(Offset, spriteBatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spriteBatch);
    }
  }
}
