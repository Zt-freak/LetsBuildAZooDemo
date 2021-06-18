// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.ScrollingList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class ScrollingList
  {
    private static Rectangle arrowRect = new Rectangle(0, 570, 12, 7);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private CustomerFrame hideframe1;
    private CustomerFrame hideframe2;
    private Vector2 framescale;
    private Vector2 trayscale;
    private GameObject uparrow;
    private GameObject downarrow;
    private ScrollingListScroll scroll;
    private ConfirmationDialog cancelconfirm;
    private MouseoverHandler mouseoverhandler;
    private Vector2 emptyentrysize;
    private bool needsresize;
    private bool hasEmptyEntrySize;
    private CustomerFrameColors framecolour;

    public ScrollingList(
      float basescale_,
      int maxrows_ = 8,
      bool alternatecolours = false,
      CustomerFrameColors framecolour_ = CustomerFrameColors.Brown,
      float betweenPadMultiplier = 0.0f)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.framecolour = framecolour_;
      this.scroll = new ScrollingListScroll(this.basescale, maxrows_, alternatecolours, betweenPadMultiplier);
      this.uparrow = new GameObject();
      this.uparrow.DrawRect = ScrollingList.arrowRect;
      this.uparrow.SetDrawOriginToCentre();
      this.downarrow = new GameObject();
      this.downarrow.DrawRect = ScrollingList.arrowRect;
      this.downarrow.SetDrawOriginToCentre();
      this.needsresize = true;
    }

    public void SizeAndPosition()
    {
      this.framescale = this.scroll.GetSize();
      float y = this.emptyentrysize.Y;
      this.framescale.Y += 2f * y;
      this.hideframe1 = new CustomerFrame(new Vector2(this.framescale.X, y), this.framecolour, this.basescale);
      this.hideframe2 = new CustomerFrame(new Vector2(this.framescale.X, y), this.framecolour, this.basescale);
      this.frame = new CustomerFrame(this.framescale, this.framecolour, this.basescale);
      this.mouseoverhandler = new MouseoverHandler(this.framescale, this.basescale);
      this.hideframe1.frame.vLocation.Y += (float) (-0.5 * (double) this.framescale.Y + 0.5 * (double) y);
      this.hideframe2.frame.vLocation.Y = (float) (0.5 * (double) this.framescale.Y - 0.5 * (double) y);
      this.uparrow.vLocation = this.hideframe1.frame.vLocation;
      this.downarrow.vLocation = this.hideframe2.frame.vLocation;
    }

    public void Add(ScrollingListEntry entry)
    {
      this.scroll.Add(entry);
      this.needsresize = true;
    }

    public void SetEmptyEntrySize(Vector2 size)
    {
      this.emptyentrysize = size;
      this.scroll.SetEmptyEntrySize(size);
      this.hasEmptyEntrySize = true;
    }

    public Vector2 GetSize()
    {
      if (this.needsresize)
      {
        this.SizeAndPosition();
        this.needsresize = false;
      }
      return this.framescale;
    }

    public bool UpdateScrollingList(Player player, Vector2 offset, float DeltaTime)
    {
      if (this.needsresize)
      {
        this.SizeAndPosition();
        this.needsresize = false;
      }
      Vector2 offset1 = offset + this.location;
      if (this.mouseoverhandler.UpdateMouseoverHandler(player, offset1, DeltaTime))
      {
        this.scroll.UpdateScroller(player);
        Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = true;
      }
      this.scroll.UpdateScrollingListScroll(player, offset1, DeltaTime);
      return false;
    }

    public void DrawScrollingList(SpriteBatch spritebatch, Vector2 offset)
    {
      Vector2 vector2 = offset + this.location;
      this.frame.DrawCustomerFrame(vector2, spritebatch);
      this.scroll.DrawScrollingListScroll(spritebatch, vector2);
      this.hideframe1.DrawCustomerFrame(vector2, spritebatch);
      this.hideframe2.DrawCustomerFrame(vector2, spritebatch);
      if (!this.scroll.maxedup)
        this.uparrow.Draw(spritebatch, AssetContainer.SpriteSheet, vector2 + this.uparrow.vLocation, this.basescale, 0.0f, true);
      if (this.scroll.maxeddown)
        return;
      this.downarrow.Draw(spritebatch, AssetContainer.SpriteSheet, vector2 + this.downarrow.vLocation, this.basescale, 3.141593f, true);
    }
  }
}
