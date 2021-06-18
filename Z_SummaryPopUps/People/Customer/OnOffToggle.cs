// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.OnOffToggle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class OnOffToggle
  {
    private static Rectangle onrect = new Rectangle(576, 950, 30, 16);
    private static Rectangle offrect = new Rectangle(546, 950, 30, 16);
    private static Rectangle knobrect = new Rectangle(606, 950, 16, 16);
    private float maxhandleoffset;
    private ZGenericUIDrawObject bar1;
    private ZGenericUIDrawObject bar2;
    private ZGenericUIDrawObject knob;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    public Vector2 location;
    private LerpHandler_Float lerper;
    private MouseoverHandler mouseoverhandler;
    private bool on;
    private bool sliding;

    public bool On => this.on;

    public OnOffToggle(float basescale_, bool startOn = false)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.maxhandleoffset = this.scalehelper.ScaleX((float) (0.5 * (double) OnOffToggle.offrect.Width - 0.5 * (double) OnOffToggle.knobrect.Width));
      this.bar1 = new ZGenericUIDrawObject(OnOffToggle.offrect, this.basescale, AssetContainer.SpriteSheet);
      this.bar2 = new ZGenericUIDrawObject(OnOffToggle.offrect, this.basescale, AssetContainer.SpriteSheet);
      this.knob = new ZGenericUIDrawObject(OnOffToggle.knobrect, this.basescale, AssetContainer.SpriteSheet);
      this.lerper = new LerpHandler_Float();
      this.on = startOn;
      this.framescale = this.bar1.GetSize();
      this.mouseoverhandler = new MouseoverHandler(this.framescale, this.basescale);
      this.knob.location = new Vector2();
      if (this.on)
        this.lerper.SetLerp(true, 1f, 1f, 3f);
      this.SetDrawRectsAndPosition(this.lerper.Value);
    }

    public Vector2 GetSize() => this.framescale;

    private void StartSlide(bool targetval)
    {
      if (!this.on & targetval)
      {
        this.lerper.SetLerp(true, 0.0f, 1f, 3f);
        this.sliding = true;
        this.on = targetval;
      }
      else
      {
        if (!this.on || targetval)
          return;
        this.lerper.SetLerp(true, 1f, 0.0f, 3f);
        this.sliding = true;
        this.on = targetval;
      }
    }

    public bool UpdateOnOffToggle(Player player, Vector2 offset, float DeltaTime)
    {
      bool flag = false;
      offset += this.location;
      this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      if (this.mouseoverhandler.Clicked)
      {
        flag = true;
        this.StartSlide(!this.on);
      }
      if (this.sliding)
      {
        this.lerper.UpdateLerpHandler(DeltaTime);
        if (this.lerper.IsComplete())
          this.sliding = false;
      }
      this.SetDrawRectsAndPosition(this.lerper.Value);
      return flag;
    }

    private void SetDrawRectsAndPosition(float lerpval)
    {
      this.knob.location.X = (float) (-(double) this.maxhandleoffset + (double) this.lerper.Value * 2.0 * (double) this.maxhandleoffset);
      int num = (int) ((double) lerpval * (double) OnOffToggle.onrect.Width);
      Rectangle onrect = OnOffToggle.onrect;
      onrect.Width = num;
      this.bar1.SetDrawRect(onrect);
      Rectangle offrect = OnOffToggle.offrect;
      offrect.Width -= num;
      offrect.X += num;
      this.bar2.SetDrawRect(offrect);
      this.bar2.location.X = this.scalehelper.ScaleX((float) num);
    }

    public void DrawOnOffToggle(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.bar1.DrawZGenericUIDrawObject(spritebatch, offset);
      this.bar2.DrawZGenericUIDrawObject(spritebatch, offset);
      this.knob.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}
