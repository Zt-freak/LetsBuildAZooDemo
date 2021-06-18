// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy.PregnancyManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy
{
  internal class PregnancyManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private PregnancyBar pregnancybar;
    private Mate mate;
    private SimpleTextHandler notofage;
    private ContraceptionToggle contraception;
    private string MAIN;

    public PregnancyManager(float width, PrisonerInfo animal, Player player, float BaseScale)
    {
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      string text = "Reproduction";
      if (animal.IsPregnant)
      {
        text = "Pregnancy";
        this.mate = new Mate(animal, player, BaseScale);
        this.pregnancybar = new PregnancyBar(animal, player, BaseScale, false);
      }
      else if (animal.GetIsABaby())
        this.notofage = new SimpleTextHandler("Too young to concieve", width, _Scale: BaseScale, AutoComplete: true);
      else if (!animal.GetCanHaveBaby())
        this.notofage = new SimpleTextHandler("Too old to have children", width, _Scale: BaseScale, AutoComplete: true);
      else
        this.contraception = new ContraceptionToggle(animal, BaseScale);
      this.customerframe.AddMiniHeading(text);
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      Vector2 vector2_1 = zero + defaultBuffer;
      if (this.notofage != null)
      {
        this.notofage.SetAllColours(ColourData.Z_Cream);
        this.notofage.Location = vector2_1;
        vector2_1.Y += this.notofage.GetSize().Y;
        vector2_1.Y += defaultBuffer.Y;
      }
      if (this.contraception != null)
      {
        this.contraception.location = vector2_1;
        vector2_1.Y += this.contraception.GetSize().Y;
        vector2_1.Y += defaultBuffer.Y;
      }
      if (this.pregnancybar != null)
      {
        this.mate.Location.X = width;
        this.mate.Location += new Vector2(-defaultBuffer.X, defaultBuffer.Y);
        this.mate.Location.X -= this.mate.GetSize().X * 0.5f;
        this.mate.Location.Y = defaultBuffer.Y;
        this.pregnancybar.Location.X = vector2_1.X;
        this.pregnancybar.Location.X += this.pregnancybar.GetWidth() * 0.5f;
        this.pregnancybar.Location.X += defaultBuffer.X;
        this.pregnancybar.Location.Y += this.pregnancybar.GetOffsetFromTop();
        vector2_1.Y += this.mate.GetSize().Y - this.mate.Location.Y;
      }
      this.customerframe.Resize(new Vector2(width, vector2_1.Y));
      Vector2 vector2_2 = -this.customerframe.VSCale * 0.5f;
      if (this.notofage != null)
        this.notofage.Location += vector2_2;
      if (this.contraception != null)
        this.contraception.location += vector2_2;
      if (this.pregnancybar == null)
        return;
      this.pregnancybar.Location.X += vector2_2.X;
      this.mate.Location += vector2_2;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdatePregnancyManager(Vector2 TopCenter, Player player, float DeltaTime)
    {
      TopCenter += this.location;
      if (this.contraception == null)
        return;
      this.contraception.UpdateDrawContraceptionToggle(TopCenter, player, DeltaTime);
    }

    public void DrawPregnancyManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      if (this.contraception != null)
        this.contraception.DrawContraceptionToggle(offset, spriteBatch);
      else if (this.notofage != null)
      {
        this.notofage.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      }
      else
      {
        this.pregnancybar.DrawPregnancyBar(offset, spriteBatch);
        if (this.mate == null)
          return;
        this.mate.DrawMate(offset, spriteBatch);
      }
    }
  }
}
