// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.MoralityUnlocksFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks
{
  internal class MoralityUnlocksFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MoralityUnlockGridDisplay gridDisplay;

    public MoralityUnlocksFrame(bool isGoodNotEvil, Player player, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.gridDisplay = new MoralityUnlockGridDisplay(isGoodNotEvil, player, BaseScale, 3);
      Vector2 vector2 = Vector2.Zero + defaultBuffer;
      this.gridDisplay.location = vector2;
      this.customerFrame = new CustomerFrame(vector2 + this.gridDisplay.GetSize() + defaultBuffer, ColourData.Z_FrameDarkBrown, BaseScale);
      this.gridDisplay.location += -this.customerFrame.VSCale * 0.5f;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void RefreshValues(Player player) => this.gridDisplay.RefreshValues(player);

    public void UpdateMoralityUnlocksFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.gridDisplay.UpdateMoralityUnlockGridDisplay(player, DeltaTime, offset);
    }

    public void DrawMoralityUnlocksFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.gridDisplay.DrawMoralityUnlockGridDisplay(offset, spriteBatch);
    }
  }
}
