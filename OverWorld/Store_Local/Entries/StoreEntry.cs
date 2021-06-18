// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Store_Local.Entries.StoreEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.OverWorld.Store_Local.Entries
{
  internal class StoreEntry
  {
    private StoreIcon storeicon;
    private GameObject FrameBack;
    private GameObject FrameFrame;
    private GameObject MouseOverObj;
    public Vector2 Location;
    private Vector2 FrameScale;
    private Vector2 FrameScaleMiddle;
    private IConHeading isonheading;
    private bool MouseOver;

    public StoreEntry(StoreEntryType storeicontype)
    {
      this.storeicon = new StoreIcon(storeicontype);
      this.storeicon.scale = 2f;
      this.FrameBack = new GameObject();
      this.FrameBack.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FrameBack.SetDrawOriginToCentre();
      this.FrameBack.SetAllColours(ColourData.DeepPurple);
      this.FrameScale = new Vector2(300f, 60f);
      this.FrameScaleMiddle = this.FrameScale;
      this.FrameFrame = new GameObject(this.FrameBack);
      this.FrameFrame.SetAllColours(ColourData.YellowHighlight);
      this.FrameScaleMiddle.Y -= 4f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.FrameScaleMiddle.X -= 4f;
      this.MouseOverObj = new GameObject(this.FrameBack);
      this.MouseOverObj.fAlpha = 0.2f;
      this.storeicon.vLocation.X = this.FrameScale.X * -0.5f;
      this.storeicon.vLocation.X += 4f;
      this.storeicon.vLocation.X += (float) ((double) this.storeicon.scale * (double) this.storeicon.DrawRect.Width * 0.5);
      this.MouseOverObj.SetAllColours(1f, 1f, 1f);
      this.isonheading = new IConHeading(storeicontype);
    }

    public bool UpdateStoreEntry(Player player, Vector2 Offset)
    {
      if (MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
        this.MouseOver = true;
      else if (MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y, player.inputmap.PointerLocation))
        this.MouseOver = true;
      return MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawStoreEntry(Vector2 Offset, bool Selected, SpriteBatch DrawWithThis)
    {
      if (GameFlags.HasNotch)
        Offset.X += 10f;
      if (Selected)
      {
        Offset.Y -= 2f;
        Offset.X += 2f;
      }
      this.FrameFrame.Draw(DrawWithThis, AssetContainer.SpriteSheet, this.Location + Offset, this.FrameScale * Sengine.ScreenRatioUpwardsMultiplier);
      this.FrameBack.Draw(DrawWithThis, AssetContainer.SpriteSheet, this.Location + Offset, this.FrameScaleMiddle * Sengine.ScreenRatioUpwardsMultiplier);
      if (Selected)
        this.MouseOverObj.Draw(DrawWithThis, AssetContainer.SpriteSheet, this.Location + Offset, this.FrameScaleMiddle * Sengine.ScreenRatioUpwardsMultiplier);
      this.storeicon.DrawStoreIcon(this.Location + Offset, DrawWithThis);
      this.isonheading.DrawIConHeading(this.Location + Offset, DrawWithThis);
    }
  }
}
