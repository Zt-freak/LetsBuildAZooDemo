// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.UseOfForcePanel
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
  internal class UseOfForcePanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private SelectorBarWithLabels selector;
    private MiniHeading heading;

    public int Selected => this.selector.Selected;

    public UseOfForcePanel(float moralityrating, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.selector = new SelectorBarWithLabels(this.basescale);
      this.selector.Add((double) moralityrating > 10.0 ? "Warnings" : "Locked: Good too low", BTNColour.GoodYellow);
      this.selector.Add("Submission");
      this.selector.Add("Non-lethal Force");
      this.selector.Add((double) moralityrating < -10.0 ? "Lethal Force" : "Locked: Evil too low", BTNColour.EvilPurple);
      this.heading = new MiniHeading(Vector2.Zero, "FORCE LEVEL", 1f, this.basescale);
      this.framescale = 2f * defaultBuffer;
      this.framescale = this.selector.GetSize();
      this.framescale.Y += 2f * defaultBuffer.Y;
      this.framescale.Y += this.heading.GetSize().Y;
      this.framescale.X = this.uiscale.ScaleX(220f);
      this.heading.SetTextPosition(this.framescale);
      this.frame = new CustomerFrame(this.framescale, BaseScale: (1f * this.basescale));
      Vector2 vector2 = defaultBuffer;
      vector2.Y += -0.5f * this.framescale.Y;
      vector2.Y += this.heading.GetSize().Y;
      vector2.Y += defaultBuffer.Y;
      this.selector.location = vector2;
      this.selector.location.X = 0.0f;
      this.selector.location.Y += 0.5f * this.selector.GetSize().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateUseOfForcePanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.selector.UpdateSelectorBarWithLabels(player, offset, DeltaTime);
      return false;
    }

    public void DrawUseOfForcePanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.selector.DrawSelectorBarWithLabels(spritebatch, offset);
    }
  }
}
