// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.ParentsAndOffspringDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class ParentsAndOffspringDisplay
  {
    public CustomerFrame customerFrame;
    private ParentsDisplay parentsdisplay;
    private PotentialBabies potentialbabies;
    public Vector2 location;

    public ParentsAndOffspringDisplay(Parents_AndChild PandC, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = 0.0f;
      float num2 = 0.0f;
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num3 = num1 + defaultYbuffer;
      float num4 = num2 + defaultXbuffer;
      this.parentsdisplay = new ParentsDisplay(PandC.FemaleParentVariant, PandC.MaleParentVariant, PandC.animaltype, BaseScale, FatherDead: PandC.FatherDead, MotherIsDead: PandC.MotherIsDead);
      this.parentsdisplay.Location.Y = (float) ((double) num3 + (double) this.parentsdisplay.GetHeight() * 0.5 + (double) this.parentsdisplay.GetTextOffset() * 0.5);
      float num5 = num3 + this.parentsdisplay.GetHeight() + defaultYbuffer;
      this.potentialbabies = new PotentialBabies(PandC, BaseScale);
      this.potentialbabies.Location.Y = num5 + this.potentialbabies.customerFrame.VSCale.Y * 0.5f;
      float num6 = num5 + this.potentialbabies.customerFrame.VSCale.Y;
      float num7 = num4 + this.potentialbabies.customerFrame.VSCale.X;
      float y = num6 + defaultYbuffer;
      this.customerFrame = new CustomerFrame(new Vector2(num7 + defaultXbuffer, y), true, BaseScale);
      Vector2 vector2 = new Vector2(0.0f, (float) (-(double) this.customerFrame.VSCale.Y * 0.5));
      this.parentsdisplay.Location += vector2;
      this.potentialbabies.Location += vector2;
    }

    public void DrawParentsAndOffspringDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.parentsdisplay.DrawParentsDisplay(offset, spriteBatch);
      this.potentialbabies.DrawPotentialBabies(offset, spriteBatch);
    }
  }
}
