// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BetaEnd.BetaLock;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class CustomerFrame
  {
    public GameObjectNineSlice frame;
    private GameObjectNineSlice darkOverlayFrame;
    private MouseoverHandler mouseoverFrame;
    public Vector2 VSCale;
    private float basescale;
    public Vector2 location;
    private MiniHeading miniHeading;
    private UIScaleHelper scaleHelper;
    private float savedalpha;
    private bool active = true;
    private Texture2D spritesheet;
    private BetaLockedDisplay betaLockDisplay;

    public bool Active
    {
      get => this.active;
      set => this.active = value;
    }

    public CustomerFrame(Vector2 _VSCale, CustomerFrameColors color, float BaseScale)
    {
      this.basescale = BaseScale;
      this.VSCale = _VSCale;
      this.scaleHelper = new UIScaleHelper(this.basescale);
      Rectangle MainRect = new Rectangle(0, 0, 0, 0);
      this.spritesheet = AssetContainer.SpriteSheet;
      int CornerSizeAndEdgeSize = 7;
      switch (color)
      {
        case CustomerFrameColors.DarkBrown:
          MainRect = new Rectangle(948, 528, 21, 21);
          break;
        case CustomerFrameColors.Brown:
          MainRect = new Rectangle(961, 350, 21, 21);
          break;
        case CustomerFrameColors.Cream:
          MainRect = new Rectangle(885, 568, 21, 21);
          break;
        case CustomerFrameColors.DarkerCream:
          MainRect = new Rectangle(885, 546, 21, 21);
          break;
        case CustomerFrameColors.Yellow:
          MainRect = new Rectangle(970, 568, 21, 21);
          break;
        case CustomerFrameColors.Purple:
          MainRect = new Rectangle(970, 590, 21, 21);
          break;
        case CustomerFrameColors.GoodYellowFrame:
          MainRect = new Rectangle(909, 722, 21, 21);
          break;
        case CustomerFrameColors.EvilPurpleFrame:
          MainRect = new Rectangle(909, 744, 21, 21);
          break;
        case CustomerFrameColors.CreamWithBorder:
          MainRect = new Rectangle(877, 350, 21, 21);
          break;
        case CustomerFrameColors.GoodYellowHeader:
          MainRect = new Rectangle(343, 900, 21, 21);
          break;
        case CustomerFrameColors.EvilPurpleHeader:
          MainRect = new Rectangle(368, 934, 21, 21);
          break;
        case CustomerFrameColors.NeutralBlueHeader:
          MainRect = new Rectangle(390, 935, 21, 21);
          break;
        case CustomerFrameColors.BlueWithLighterBlueBorder:
          MainRect = new Rectangle(907, 582, 21, 21);
          break;
        case CustomerFrameColors.GoodYellowChoice:
          MainRect = new Rectangle(471, 518, 21, 21);
          this.spritesheet = AssetContainer.UISheet;
          break;
        case CustomerFrameColors.EvilPurpleChoice:
          MainRect = new Rectangle(471, 496, 21, 21);
          this.spritesheet = AssetContainer.UISheet;
          break;
        case CustomerFrameColors.NeutralGrayChoice:
          MainRect = new Rectangle(190, 911, 21, 21);
          this.spritesheet = AssetContainer.UISheet;
          break;
        case CustomerFrameColors.CrownGold:
          MainRect = new Rectangle(259, 479, 63, 63);
          this.spritesheet = AssetContainer.UISheet;
          CornerSizeAndEdgeSize = 21;
          break;
        case CustomerFrameColors.WhiteOval:
          MainRect = new Rectangle(535, 263, 21, 21);
          this.spritesheet = AssetContainer.SpriteSheet;
          break;
        case CustomerFrameColors.White:
          MainRect = new Rectangle(948, 484, 21, 21);
          break;
        case CustomerFrameColors.PaperWithPawPrint:
        case CustomerFrameColors.Paper:
          MainRect = new Rectangle(0, 1431, 252, 252);
          CornerSizeAndEdgeSize = 84;
          this.spritesheet = AssetContainer.UISheet;
          break;
        case CustomerFrameColors.Gold:
          MainRect = new Rectangle(909, 766, 21, 21);
          break;
      }
      this.frame = new GameObjectNineSlice(MainRect, CornerSizeAndEdgeSize);
      if (color == CustomerFrameColors.Paper)
        this.frame.OverrideRectangle(NineSlices.BottomLeft, new Rectangle(456, 1364, 84, 84));
      this.frame.scale = BaseScale;
      this.mouseoverFrame = new MouseoverHandler(this.VSCale, this.basescale);
      this.CreateDarkOverlay();
    }

    public CustomerFrame(Vector2 _VSCale, Vector3 color, float BaseScale)
    {
      this.basescale = BaseScale;
      this.VSCale = _VSCale;
      this.scaleHelper = new UIScaleHelper(this.basescale);
      Rectangle MainRect = new Rectangle(948, 484, 21, 21);
      this.spritesheet = AssetContainer.SpriteSheet;
      this.frame = new GameObjectNineSlice(MainRect, 7);
      this.frame.scale = BaseScale;
      this.frame.SetAllColours(color);
      this.mouseoverFrame = new MouseoverHandler(this.VSCale, this.basescale);
      this.CreateDarkOverlay();
    }

    public CustomerFrame(Vector2 _VSCale, bool IsDarker = false, float BaseScale = -1f, bool UseTinyRect = false)
    {
      this.basescale = BaseScale;
      this.VSCale = _VSCale;
      this.scaleHelper = new UIScaleHelper(this.basescale);
      this.spritesheet = AssetContainer.SpriteSheet;
      if (UseTinyRect)
      {
        this.frame = new GameObjectNineSlice(new Rectangle(945, 471, 6, 6), 2);
        this.frame.SetAllColours(ColourData.Z_FrameMidBrown);
        if (IsDarker)
          this.frame.SetAllColours(ColourData.Z_FrameDarkBrown);
      }
      else
        this.frame = !IsDarker ? new GameObjectNineSlice(new Rectangle(961, 350, 21, 21), 7) : new GameObjectNineSlice(new Rectangle(948, 528, 21, 21), 7);
      if ((double) BaseScale == -1.0)
        this.frame.scale = 2f;
      else
        this.frame.scale = BaseScale;
      this.mouseoverFrame = new MouseoverHandler(this.VSCale, this.basescale);
      this.CreateDarkOverlay();
    }

    private void CreateDarkOverlay()
    {
      this.darkOverlayFrame = new GameObjectNineSlice(new Rectangle(948, 484, 21, 21), 7);
      this.darkOverlayFrame.SetAllColours(Color.Black.ToVector3());
      this.darkOverlayFrame.SetAlpha(0.5f);
    }

    public void OverrideDarkOverlayAlpha(float alpha = 0.5f) => this.darkOverlayFrame.SetAlpha(alpha);

    public void AddMiniHeading(string text)
    {
      this.miniHeading = new MiniHeading(this.VSCale, text, 1f, this.basescale);
      this.miniHeading.SetTextPosition(this.VSCale);
    }

    public float GetMiniHeadingHeight(bool IncludeBuffer = true)
    {
      float textHeight = this.miniHeading.GetTextHeight(true);
      if (IncludeBuffer)
        textHeight += this.scaleHelper.DefaultBuffer.Y;
      return textHeight;
    }

    public Vector2 GetMiniHeadingSize(bool IncludeBuffer = true)
    {
      Vector2 size = this.miniHeading.GetSize();
      if (IncludeBuffer)
        size += this.scaleHelper.DefaultBuffer;
      return size;
    }

    public void Resize(Vector2 _vScale)
    {
      if (this.miniHeading != null)
        this.miniHeading.SetTextPosition(_vScale);
      this.VSCale = _vScale;
    }

    public bool CheckCollicion(Vector2 ScreenSpaceLocation, Vector2 offset)
    {
      offset += this.location;
      return MathStuff.CheckPointCollision(true, offset + this.frame.vLocation, 1f, this.VSCale.X, this.VSCale.Y, ScreenSpaceLocation);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return MathStuff.CheckPointCollision(true, offset, 1f, this.VSCale.X, this.VSCale.Y, player.inputmap.PointerLocation);
    }

    public bool UpdateForMouseOverAndClick(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool mouseOvered)
    {
      offset += this.location;
      this.mouseoverFrame.UpdateMouseoverHandler(player, offset, DeltaTime);
      mouseOvered = this.mouseoverFrame.mouseover;
      int num = mouseOvered ? 1 : 0;
      return this.mouseoverFrame.Clicked;
    }

    public void SetColour(Vector3 colour) => this.frame.SetAllColours(colour);

    public void SetAlertRed() => this.frame.SetAllColours(new Vector3(1f, 0.0f, 0.0f));

    public void SetBackToBrown(bool IsDarker)
    {
      float scale = this.frame.scale;
      this.frame = !IsDarker ? new GameObjectNineSlice(new Rectangle(961, 350, 21, 21), 7) : new GameObjectNineSlice(new Rectangle(948, 528, 21, 21), 7);
      this.frame.scale = scale;
    }

    public void SetDullAlertRed() => this.frame.SetAllColours(new Vector3(0.8f, 0.2f, 0.2f));

    public void SetInactiveGrey() => this.frame.SetAllColours(Color.DarkGray.ToVector3());

    public void ResetColor() => this.frame.SetAllColours(Color.White.ToVector3());

    public void SetAlphaed(float alpha = 0.6f) => this.frame.SetAlpha(alpha);

    public void LockForBeta()
    {
      this.active = false;
      this.betaLockDisplay = new BetaLockedDisplay(Z_GameFlags.GetBaseScaleForUI(), this.VSCale.X);
      this.betaLockDisplay.location.X -= this.betaLockDisplay.GetSize().X * 0.5f;
    }

    public void DrawCustomerFrame(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      this.frame.DrawGameObjectNineSlice(spritebatch, this.spritesheet, Offset, this.VSCale);
      if (this.miniHeading == null)
        return;
      this.miniHeading.DrawMiniHeading(Offset, spritebatch);
    }

    public void DrawCustomerFrameWithScaleMult(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float ScaleMultiplier)
    {
      Offset += this.location;
      this.frame.DrawGameObjectNineSlice(spritebatch, this.spritesheet, Offset, this.VSCale * ScaleMultiplier);
    }

    public void PostDrawMouseoverOverlay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.mouseoverFrame.DrawMouseOverHandler(spriteBatch, offset);
    }

    public void DrawDarkOverlay(Vector2 offset, SpriteBatch spritebatch, float alphamult = 1f)
    {
      if (this.active)
        return;
      offset += this.location;
      this.darkOverlayFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.VSCale);
      if (this.betaLockDisplay == null)
        return;
      this.betaLockDisplay.DrawBetaLockedDisplay(offset, spritebatch);
    }
  }
}
