// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPServicesPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class VIPServicesPanel
  {
    private List<VIPService> services;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private LabelledCheckboxList checkboxes;
    private SimpleTextHandler text;

    public VIPServicesPanel(WalkingPerson person, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.checkboxes = new LabelledCheckboxList(this.basescale);
      this.services = new List<VIPService>();
      if (person.simperson.customertype == CustomerType.Teacher)
      {
        this.checkboxes.AddCheckbox(VIPService.TourGuide.GetString());
        this.services.Add(VIPService.TourGuide);
      }
      else
      {
        for (int index = 0; index < 7; ++index)
        {
          this.checkboxes.AddCheckbox(((VIPService) index).GetString());
          this.services.Add((VIPService) index);
        }
      }
      this.text = new SimpleTextHandler("Services to provide:", this.uiscale.ScaleX(200f), _Scale: this.basescale, _UseFontOnePointFive: true);
      this.text.AutoCompleteParagraph();
      this.text.SetAllColours(ColourData.Z_Cream);
      this.framescale = 2f * defaultBuffer;
      this.framescale.X += this.uiscale.ScaleX(180f);
      this.framescale.Y += this.checkboxes.GetSize().Y + defaultBuffer.Y + this.text.GetSize().Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      this.text.Location = vector2;
      vector2.Y += this.text.GetSize().Y + defaultBuffer.Y;
      this.checkboxes.location = vector2 + 0.5f * this.checkboxes.GetSize();
      vector2.Y += this.checkboxes.GetSize().Y + 2f * defaultBuffer.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateVIPServicesPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.checkboxes.UpdateLabelledCheckboxList(player, offset, DeltaTime);
      return false;
    }

    public void DrawVIPServicesPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.checkboxes.DrawLabelledCheckboxList(spritebatch, offset);
    }
  }
}
