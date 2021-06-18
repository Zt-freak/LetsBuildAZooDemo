// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.BaseCube
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WeekOver.V2.Cubes
{
  internal class BaseCube : GameObject
  {
    public bool AlsoWaitForCashBeforeMovingOn;
    public Vector2 Location;
    public float BaseScale;
    public CustomerFrame customerframe;
    private MiniHeading miniheading;
    public LerpHandler_Float lerperBaseCube;

    public BaseCube(float _BaseScale, bool WillLerp, Vector3 Colour)
    {
      this.lerperBaseCube = new LerpHandler_Float();
      if (WillLerp)
        this.lerperBaseCube.SetLerp(true, 0.0f, 1f, 3f);
      else
        this.lerperBaseCube.SetLerp(true, 1f, 1f, 3f);
      this.BaseScale = _BaseScale;
      this.customerframe = new CustomerFrame(new Vector2(EndPOfWeekSummaryManager.SIZE * this.BaseScale, EndPOfWeekSummaryManager.SIZE * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y), Colour, _BaseScale);
    }

    public void SetToLocation(Vector2 GridLocation) => this.Location = new Vector2(GridLocation.X * EndPOfWeekSummaryManager.SIZE * this.BaseScale, GridLocation.Y * EndPOfWeekSummaryManager.SIZE);

    public virtual void UpdateBaseCube(float DeltaTime, Player player, Vector2 Offset) => this.lerperBaseCube.UpdateLerpHandler(DeltaTime);

    public void AddMiniHeading(string TextForHeading, Vector3 Colour, bool SetBottomRight = false)
    {
      this.miniheading = new MiniHeading(this.customerframe.VSCale, TextForHeading, this.BaseScale, this.BaseScale, SetBottomRight);
      this.miniheading.SetAllColours(Colour);
    }

    public void AddMiniHeading(string TextForHeading, bool SetBottomRight = false) => this.AddMiniHeading(TextForHeading, ColourData.Z_Cream, SetBottomRight);

    public virtual bool LerpComplete(CurrentFinances currentfinances) => (currentfinances == null || !this.AlsoWaitForCashBeforeMovingOn || !currentfinances.MoneyIsLerping()) && (double) this.lerperBaseCube.Value == 1.0;

    public virtual void DrawBaseCube(Vector2 Offset, SpriteBatch spritebatch)
    {
      if ((double) this.lerperBaseCube.Value < 1.0)
      {
        if ((double) this.lerperBaseCube.Value <= 0.0)
          return;
        this.customerframe.DrawCustomerFrameWithScaleMult(Offset, spritebatch, this.lerperBaseCube.Value);
      }
      else
      {
        this.customerframe.DrawCustomerFrame(Offset, spritebatch);
        if (this.miniheading == null)
          return;
        this.miniheading.DrawMiniHeading(Offset);
      }
    }
  }
}
