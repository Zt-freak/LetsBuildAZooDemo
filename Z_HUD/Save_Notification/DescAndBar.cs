// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Save_Notification.DescAndBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Save_Notification
{
  internal class DescAndBar
  {
    private CustomerFrame frame;
    private SimpleTextHandler text;

    public DescAndBar(Player player, float BaseScale)
    {
      float num1 = 0.0f;
      this.text = new SimpleTextHandler("SAVING GAME:~The game saves at the end of every calendar day." + ("~~Total Days In Business: " + (object) Player.financialrecords.GetDaysPassed() + "~Current Cash: $" + (object) player.Stats.GetCashHeld() + "~Lifetime Profit: $" + (object) Player.financialrecords.GetLifetimeProfit()), true, (float) (350.0 * (double) BaseScale / 1024.0), BaseScale);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      this.text.Location.Y = num1 + this.text.GetHeightOfOneLine() * 0.5f;
      this.text.Location.Y -= this.text.GetHeightOfParagraph() * 0.5f;
      float num2 = num1 + this.text.GetHeightOfParagraph();
      this.text.Location.Y += 5f * BaseScale;
      float y = num2 + 10f * BaseScale;
      this.frame = new CustomerFrame(new Vector2(370f * BaseScale, y), BaseScale: BaseScale);
    }

    public Vector2 GetSize() => this.frame.VSCale;

    public void DrawDescAndBar(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.frame.DrawCustomerFrame(Offset, spritebatch);
      this.text.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}
