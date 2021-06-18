// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButtons.NumberOfAlerts
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButtons
{
  internal class NumberOfAlerts
  {
    private static Rectangle boxrect = new Rectangle(295, 114, 13, 13);
    private GameObject box;
    private float basescale;
    public Vector2 location;
    private UIScaleHelper uiscale;
    private Vector2 boxsize;
    private static float scalemult = 1.6f;

    public NumberOfAlerts(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.box = new GameObject();
      this.box.DrawRect = NumberOfAlerts.boxrect;
      this.box.SetDrawOriginToCentre();
      this.box.scale = NumberOfAlerts.scalemult * this.basescale;
    }

    public Vector2 GetSize() => this.uiscale.ScaleVector2(new Vector2((float) NumberOfAlerts.boxrect.Width, (float) NumberOfAlerts.boxrect.Height)) * NumberOfAlerts.scalemult;

    public void DrawNumberOfAlerts(int numAlerts, Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.box.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
      numAlerts = numAlerts > 99 ? 99 : numAlerts;
      string stringToDraw = numAlerts.ToString();
      if (stringToDraw.Length <= 1)
        stringToDraw = "x" + stringToDraw;
      TextFunctions.DrawJustifiedText(stringToDraw, this.basescale, offset, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }
  }
}
