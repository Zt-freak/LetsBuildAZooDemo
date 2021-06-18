// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SelectorBarButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class SelectorBarButton
  {
    private static Rectangle yellowcirclerect = new Rectangle(575, 583, 16, 16);
    private static Rectangle neutralcirclerect = new Rectangle(916, 662, 16, 16);
    private static Rectangle purplecirclerect = new Rectangle(575, 567, 16, 16);
    private static Rectangle dotrect = new Rectangle(946, 478, 6, 6);
    private static Rectangle arrowrect = new Rectangle(1004, 622, 10, 6);
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private CustomerFrame frame;
    private MouseoverHandler mouseoverhandler;
    public Vector2 location;
    private GameObject circleobj;
    private GameObject dotobj;
    private GameObject arrowobj;
    private BTNColour colour;
    private bool selected;
    private float scalemult = 1f;
    private float arrowoffset;
    private Vector2 arrowsize;
    private Vector2 circlesize;

    public bool Selected
    {
      get => this.selected;
      set => this.selected = value;
    }

    public SelectorBarButton(float basescale_, BTNColour colour_ = BTNColour.Cream)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.colour = colour_;
      this.circleobj = new GameObject();
      switch (this.colour)
      {
        case BTNColour.Green:
          this.circleobj.DrawRect = SelectorBarButton.neutralcirclerect;
          this.circleobj.SetAllColours(ColourData.Z_ButtonGreen);
          break;
        case BTNColour.Red:
          this.circleobj.DrawRect = SelectorBarButton.neutralcirclerect;
          this.circleobj.SetAllColours(ColourData.LogRed);
          break;
        case BTNColour.EvilPurple:
          this.circleobj.DrawRect = SelectorBarButton.purplecirclerect;
          break;
        case BTNColour.GoodYellow:
          this.circleobj.DrawRect = SelectorBarButton.yellowcirclerect;
          break;
        default:
          this.circleobj.DrawRect = SelectorBarButton.neutralcirclerect;
          break;
      }
      this.circleobj.SetDrawOriginToCentre();
      this.circlesize = this.uiscale.ScaleVector2(new Vector2((float) this.circleobj.DrawRect.Width, (float) this.circleobj.DrawRect.Height));
      this.dotobj = new GameObject();
      this.dotobj.DrawRect = SelectorBarButton.dotrect;
      this.dotobj.SetDrawOriginToCentre();
      this.dotobj.SetAllColours(1f, 0.9607843f, 0.8352941f);
      this.arrowoffset = 1.4f * defaultBuffer.Y;
      this.arrowobj = new GameObject();
      this.arrowobj.DrawRect = SelectorBarButton.arrowrect;
      this.arrowobj.SetDrawOriginToCentre();
      this.arrowobj.SetAllColours(1f, 0.9607843f, 0.8352941f);
      this.arrowsize = this.uiscale.ScaleVector2(new Vector2((float) this.arrowobj.DrawRect.Width, (float) this.arrowobj.DrawRect.Height));
      this.framescale = this.GetSize();
      this.mouseoverhandler = new MouseoverHandler(this.circlesize * 1.5f, this.basescale);
      this.frame = new CustomerFrame(this.circlesize, true, this.basescale);
      this.arrowobj.vLocation.Y += this.arrowoffset;
    }

    public Vector2 GetSize() => this.circlesize;

    public bool UpdateSelectorBarButton(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag1 = false;
      int num = this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime) ? 1 : 0;
      bool flag2 = flag1 | this.mouseoverhandler.Clicked;
      this.scalemult = num == 0 ? 1f : 1.25f;
      return flag2;
    }

    public void DrawSelectorBarButton(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.circleobj.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.basescale * this.scalemult, 1f);
      if (!this.selected)
        return;
      this.dotobj.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.basescale, 1f);
      this.arrowobj.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.basescale, 1f);
    }
  }
}
