// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.SimpleArrowPageButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI
{
  internal class SimpleArrowPageButtons
  {
    private PageArrowButton Left;
    private PageArrowButton Right;
    public Vector2 Location;
    private CustomerFrame surround;
    private bool DoNotDrawFrame;
    private bool DisableLeft;
    private bool DisableRight;

    public SimpleArrowPageButtons(float BaseScale, bool isDarker = false, bool _DoNotDrawFrame = false)
    {
      this.DoNotDrawFrame = _DoNotDrawFrame;
      this.Left = new PageArrowButton(false, BaseScale);
      this.Left.vLocation.X = BaseScale * -7f;
      this.Left.vLocation.X -= 5f * BaseScale;
      this.Right = new PageArrowButton(true, BaseScale);
      this.Right.vLocation.X = -this.Left.vLocation.X;
      this.surround = new CustomerFrame(new Vector2(BaseScale * 40f, BaseScale * 22f), isDarker, BaseScale * 2f, true);
    }

    public void SetAsDisabled(bool isLeft, bool isDisable)
    {
      if (isLeft)
      {
        this.Left.SetGrey(isDisable);
        this.DisableLeft = isDisable;
      }
      else
      {
        this.Right.SetGrey(isDisable);
        this.DisableRight = isDisable;
      }
    }

    public Vector2 GetSize(bool GetWithMultiplier = false) => GetWithMultiplier ? this.surround.VSCale * Sengine.ScreenRatioUpwardsMultiplier : this.surround.VSCale;

    public int UpdateSimpleArrowPageButtons(float DeltaTime, Player player, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.DisableLeft && this.Left.UpdatePageArrowButton(DeltaTime, player, Offset))
        return -1;
      return !this.DisableRight && this.Right.UpdatePageArrowButton(DeltaTime, player, Offset) ? 1 : 0;
    }

    public void DrawSimpleArrowPageButtons(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      if (!this.DoNotDrawFrame)
        this.surround.DrawCustomerFrame(Offset, spritebatch);
      this.Left.DrawPageArrowButton(Offset, spritebatch);
      this.Right.DrawPageArrowButton(Offset, spritebatch);
    }
  }
}
