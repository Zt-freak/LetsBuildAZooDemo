// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.ChangeBuildingSkin.BuildingOnAButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuldMenu.ChangeBuildingSkin
{
  internal class BuildingOnAButton : ZGenericButton
  {
    private Vector2 size;
    private SimpleBuildingRenderer renderer;
    public TILETYPE tiletype;
    private CustomerFrame frame;
    private ZGenericText questionMark;
    private bool DrawQuestionMark;

    public BuildingOnAButton(
      TILETYPE tiletype_,
      float basescale_,
      bool greyedOut_ = false,
      float rawsizeX_ = 60f,
      float rawsizeY_ = 60f,
      bool _DrawQuestionMark = false)
      : base(basescale_, new Rectangle(0, 0, (int) rawsizeX_, (int) rawsizeY_), greyedOut_)
    {
      this.tiletype = tiletype_;
      this.size = this.uiscale.ScaleVector2(new Vector2(rawsizeX_, rawsizeY_));
      if (_DrawQuestionMark)
        this.SetAsQuestionMark();
      else
        this.SetNewBuilding(this.tiletype);
      this.frame = new CustomerFrame(this.size, true, basescale_);
      if (!this.greyedOut)
        return;
      this.frame.SetInactiveGrey();
    }

    public void SetNewBuilding(TILETYPE tileType)
    {
      this.questionMark = (ZGenericText) null;
      this.renderer = new SimpleBuildingRenderer(this.tiletype);
      this.renderer.SetSize(this.size.X * 0.8f, 2f);
    }

    public void SetAsQuestionMark()
    {
      this.renderer = (SimpleBuildingRenderer) null;
      this.questionMark = new ZGenericText("?", this.basescale, _UseOnePointFiveFont: true);
    }

    public void SetSelected(bool greyedOut_)
    {
      this.greyedOut = greyedOut_;
      if (this.greyedOut)
        this.frame.SetInactiveGrey();
      else
        this.frame.ResetColor();
    }

    public bool UpdateBuildingOnAButton(Player player, Vector2 offset, float DeltaTime) => this.UpdateZGenericButton(player, offset, DeltaTime);

    public override void DrawZGenericButton(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if (this.renderer != null)
        this.renderer.DrawSimpleBuildingRenderer(offset, spritebatch);
      if (this.questionMark == null)
        return;
      this.questionMark.DrawZGenericText(offset, spritebatch);
    }
  }
}
