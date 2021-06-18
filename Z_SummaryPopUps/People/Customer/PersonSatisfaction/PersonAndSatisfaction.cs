// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.PersonSatisfaction.PersonAndSatisfaction
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.PersonSatisfaction
{
  internal class PersonAndSatisfaction
  {
    private AnimalInFrame animalinframe;
    public CustomerFrame customerframe;
    public Vector2 Location;
    private SatisfactionBarManager satisfactionbarmanager;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 nameloc;
    private string name;
    private Vector2 framescale;

    public PersonAndSatisfaction(
      WalkingPerson person,
      float basescale_,
      Vector2 MasterVScale,
      bool IsEmployee,
      string name_)
    {
      this.name = name_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.framescale = this.uiscale.ScaleVector2(new Vector2(210f, 160f));
      this.animalinframe = new AnimalInFrame(person.thispersontype, AnimalType.None, TargetSize: (50f * this.basescale), BaseScale: (2f * this.basescale));
      this.customerframe = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      this.satisfactionbarmanager = new SatisfactionBarManager(person.simperson, IsEmployee, this.basescale);
      float num = this.uiscale.ScaleY(AssetContainer.SpringFontX1AndHalf.MeasureString("Some y Name").Y);
      Vector2 vector2 = -0.5f * this.customerframe.VSCale + new Vector2(this.uiscale.GetDefaultXBuffer(), this.uiscale.GetDefaultYBuffer());
      this.animalinframe.Location = vector2 + 0.5f * this.animalinframe.GetSize();
      vector2.X += this.animalinframe.GetSize().X + this.uiscale.GetDefaultXBuffer();
      this.nameloc = vector2;
      vector2.X += this.uiscale.ScaleX(100f);
      vector2.Y += num + this.uiscale.GetDefaultYBuffer();
      this.satisfactionbarmanager.Location = vector2;
      this.satisfactionbarmanager.Location.Y += 0.5f * this.satisfactionbarmanager.GetBarSize().Y;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdatePersonAndSatisfaction(Vector2 Offset, Player player)
    {
      Offset += this.Location;
      this.satisfactionbarmanager.UpdateSatisfactionBarManager(Offset, player);
    }

    public void DrawPersonAndSatisfaction(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.animalinframe.DrawAnimalInFrame(Offset);
      TextFunctions.DrawTextWithDropShadow(this.name, this.basescale, Offset + this.nameloc, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      this.satisfactionbarmanager.DrawSatisfactionBarManager(Offset, spritebatch);
    }
  }
}
