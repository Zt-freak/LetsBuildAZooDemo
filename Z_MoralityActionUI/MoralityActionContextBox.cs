// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralityActionUI.MoralityActionContextBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_MoralityActionUI
{
  internal class MoralityActionContextBox
  {
    private Rectangle rect = new Rectangle(945, 471, 6, 6);
    public Vector2 location;
    private float basescale;
    private bool isGood;
    private bool locked;
    private UIScaleHelper uiScale;
    private MoralityActionButton button;
    private GameObjectNineSlice nineslice;
    private string lockedStr;
    private SatisfactionBar bar;
    private MoralityScoreRequired scoreNeeded;
    private Vector2 darkframescale;
    private Vector2 customTextOffset;
    private Vector2 scoreSize;
    private Vector2 centerOffset;

    public MoralityActionContextBox(bool isGood_, bool locked_, float basescale_)
    {
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(this.basescale);
      this.isGood = isGood_;
      this.locked = locked_;
      this.button = new MoralityActionButton(this.isGood, "Do " + (this.isGood ? "Good" : "Evil"), 60f, this.basescale);
      this.nineslice = new GameObjectNineSlice(this.rect, 2);
      this.nineslice.SetAllColours(this.isGood ? ColourData.GoodYellowDark : ColourData.EvilPurpleDark);
      this.nineslice.scale = 2f * this.basescale;
      this.lockedStr = this.isGood ? "Not enough Good!" : "Not enough Evil!";
      this.darkframescale = this.uiScale.ScaleVector2(new Vector2(100f, 12f));
      this.bar = new SatisfactionBar(0.7f, this.basescale, BarSIze.Thin);
      this.bar.SetBarColours(this.isGood ? ColourData.GoodYellow : ColourData.EvilPurple);
      this.scoreNeeded = new MoralityScoreRequired(isGood_, 2500f, basescale_);
      this.customTextOffset = this.uiScale.ScaleVector2(new Vector2(0.0f, 1f));
      this.bar.vLocation.X = (float) (0.5 * (-(double) this.darkframescale.X + (double) this.bar.GetSize().X));
      this.bar.vLocation.Y = (float) (0.5 * (double) this.darkframescale.Y + 0.5 * (double) this.bar.GetSize().Y) + this.uiScale.ScaleY(10f);
      this.scoreNeeded.location.Y = this.bar.vLocation.Y;
      this.scoreNeeded.location.X = (float) (0.5 * ((double) this.darkframescale.X - (double) this.scoreNeeded.GetSize().X));
      this.centerOffset = Vector2.Zero;
      this.centerOffset.Y = (float) (-0.5 * (double) this.GetSize().Y + 0.5 * (double) this.darkframescale.Y);
      SatisfactionBar bar = this.bar;
      bar.vLocation = bar.vLocation + this.centerOffset;
      this.scoreNeeded.location += this.centerOffset;
    }

    public Vector2 GetSize()
    {
      Vector2 vector2 = new Vector2();
      Vector2 darkframescale = this.darkframescale;
      darkframescale.Y += this.scoreNeeded.GetSize().Y + this.uiScale.ScaleY(10f);
      return darkframescale;
    }

    public bool UpdateMoralityActionContextBox(Player player, Vector2 offset, float DeltaTime)
    {
      bool flag = false;
      if (!this.locked)
        flag |= this.button.UpdateMoralityActionButton(player, this.location + offset, DeltaTime);
      return flag;
    }

    public void DrawMoralityActionContextBox(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      if (!this.locked)
      {
        this.button.DrawMoralityActionButton(offset, spritebatch);
      }
      else
      {
        this.nineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset + this.centerOffset, this.darkframescale);
        TextFunctions.DrawJustifiedText(this.lockedStr, this.basescale, offset + this.customTextOffset + this.centerOffset, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
        this.bar.DrawSatisfactionBar(offset, spritebatch);
        this.scoreNeeded.DrawMoralityScoreRequired(offset, spritebatch);
      }
    }
  }
}
