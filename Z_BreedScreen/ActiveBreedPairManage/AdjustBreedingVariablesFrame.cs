// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.AdjustBreedingVariablesFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.ActiveBreedPair;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage
{
  internal class AdjustBreedingVariablesFrame
  {
    public Vector2 location;
    public CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private TimeWithParents timewithparents;
    private TimeWithParents Production;
    private ArtificialInsemination AI;

    public AdjustBreedingVariablesFrame(
      Parents_AndChild parents_and_child,
      float forceWidth,
      float BaseScale)
    {
      float defaultYbuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      float num1 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, "Configuration", 1f, BaseScale);
      float num2 = num1 + (this.miniHeading.GetTextHeight(true) + defaultYbuffer);
      this.timewithparents = new TimeWithParents(parents_and_child, BaseScale, ForceOption: parents_and_child.NursingDaysOption);
      this.timewithparents.Location.Y = num2 + this.timewithparents.totalHeight * 0.5f;
      float num3 = num2 + this.timewithparents.totalHeight + defaultYbuffer;
      this.Production = new TimeWithParents(parents_and_child, BaseScale, true, parents_and_child.ProductionTargetOption);
      this.Production.Location.Y = num3 + this.Production.totalHeight * 0.5f;
      float num4 = num3 + this.Production.totalHeight + defaultYbuffer;
      this.AI = new ArtificialInsemination(parents_and_child, BaseScale);
      this.AI.Location.Y = num4 + this.AI.GetHeight() * 0.5f;
      float y = num4 + this.AI.GetHeight() + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(forceWidth, y), CustomerFrameColors.Brown, BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.timewithparents.Location.Y += vector2.Y;
      this.Production.Location.Y += vector2.Y;
      this.AI.Location.Y += vector2.Y;
    }

    public bool UpdateAdjustBreedingVariablesFrame(Player player, float DeltaTime, Vector2 Offset)
    {
      bool flag = false;
      Offset += this.location;
      if (this.AI.UpdateArtificialInsemination(player, DeltaTime, Offset))
        flag = true;
      if (this.timewithparents.UpdateTimeWithParents(player, Offset, DeltaTime))
        flag = true;
      if (this.Production.UpdateTimeWithParents(player, Offset, DeltaTime))
        flag = true;
      return flag;
    }

    public void DrawAdjustBreedingVariablesFrame(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      this.miniHeading.DrawMiniHeading(Offset, spritebatch);
      this.timewithparents.DrawTimeWithParents(Offset, spritebatch);
      this.Production.DrawTimeWithParents(Offset, spritebatch);
      this.AI.DrawArtificialInsemination(Offset, spritebatch);
    }
  }
}
