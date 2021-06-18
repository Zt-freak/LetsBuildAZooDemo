// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CharacterSelect.SelectablePerson
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_CharacterSelect
{
  internal class SelectablePerson
  {
    private EnemyRenderer person;
    private bool WasSelected;
    public Vector2 Location;
    private LerpHandler_Float lerper;
    private float XValue;
    private int Variant;
    private AnimalType personType;
    private CustomerFrame mouseoverFrame;
    public bool IsMouseover;

    public SelectablePerson(float OffsetX, AnimalType _personType, float BaseScale, int _variant = 0)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.XValue = OffsetX;
      this.lerper = new LerpHandler_Float();
      this.Variant = _variant;
      this.personType = _personType;
      this.person = new EnemyRenderer(this.personType, this.Variant);
      this.person.scale = 2f * BaseScale;
      this.mouseoverFrame = new CustomerFrame(uiScaleHelper.ScaleVector2(new Vector2(40f, 55f)), CustomerFrameColors.DarkerCream, BaseScale);
      this.mouseoverFrame.location.Y -= this.mouseoverFrame.VSCale.Y * 0.5f;
      this.mouseoverFrame.location.Y += uiScaleHelper.DefaultBuffer.Y;
      this.mouseoverFrame.SetAlphaed();
    }

    public Vector2 GetSize() => this.mouseoverFrame.VSCale;

    public void Darken(bool Undarken = false)
    {
      if (Undarken)
      {
        this.person.SetAlpha(1f);
        this.person.SetAllColours(Color.White.ToVector3());
      }
      else
      {
        this.person.SetAlpha(0.7f);
        this.person.SetAllColours(Color.Gray.ToVector3());
      }
    }

    public void Exit(bool _WasSelected, bool DelayLerp = false)
    {
      this.lerper.SetLerp(false, 0.0f, 1f, 1.5f, true);
      if (DelayLerp)
      {
        this.lerper.SetDelay(0.2f);
        if (!_WasSelected)
          this.person.SetAlpha(false, 0.5f, 1f, 0.0f);
      }
      if (!_WasSelected)
        return;
      this.WasSelected = true;
    }

    public bool UpdateSelectablePerson(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool DisableClickOrMouseOver = false)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.person.UpdateAnimalRenderer(DeltaTime);
      this.person.UpdateColours(DeltaTime);
      if (!DisableClickOrMouseOver)
      {
        this.IsMouseover = MathStuff.CheckPointCollision(true, this.Location + this.mouseoverFrame.location + offset, 1f, this.mouseoverFrame.VSCale.X, this.mouseoverFrame.VSCale.Y, player.inputmap.PointerLocation);
        if (this.IsMouseover && ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0]))
          return true;
      }
      else
        this.IsMouseover = false;
      return false;
    }

    public int GetVariantIndex() => this.Variant;

    public AnimalType GetAnimalType() => this.personType;

    public void DrawSelectablePerson(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Vector2 vector2;
      if (this.WasSelected)
      {
        vector2 = new Vector2(this.lerper.Value * -this.XValue, 0.0f);
        Vector2 zero = Vector2.Zero;
        this.person.Shadow.fAlpha = this.person.fAlpha;
        this.person.ScreenSpaceDrawEnemyRendererShadow(this.Location + zero + Offset, spriteBatch);
        this.person.ScreenSpaceDrawEnemyRenderer(this.Location + zero + Offset, spriteBatch);
        vector2 = zero + this.person.vLocation;
      }
      else
      {
        vector2 = new Vector2(0.0f, this.lerper.Value * 500f);
        Vector2 zero = Vector2.Zero;
        if (this.IsMouseover)
        {
          this.mouseoverFrame.DrawCustomerFrame(this.Location + zero + Offset, spriteBatch);
          this.IsMouseover = false;
        }
        this.person.Shadow.fAlpha = this.person.fAlpha;
        this.person.ScreenSpaceDrawEnemyRendererShadow(this.Location + zero + Offset, spriteBatch);
        this.person.ScreenSpaceDrawEnemyRenderer(this.Location + zero + Offset, spriteBatch);
        vector2 = zero + this.person.vLocation;
      }
    }
  }
}
