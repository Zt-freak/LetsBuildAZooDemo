// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.RoundFramePortrait
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class RoundFramePortrait
  {
    private static Rectangle inversecirclerect = new Rectangle(300, 723, 49, 49);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private AnimalInFrame portrait;
    private ZGenericUIDrawObject invcircle;

    public RoundFramePortrait(
      float basescale_,
      AnimalType animaltype,
      AnimalType headtype = AnimalType.None,
      int variant = 0,
      int headvariant = -1,
      float FrameEdgeBuffer = 6f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.portrait = new AnimalInFrame(animaltype, headtype, variant, 49f * this.basescale, FrameEdgeBuffer, this.basescale, headvariant);
      this.invcircle = new ZGenericUIDrawObject(RoundFramePortrait.inversecirclerect, this.basescale, AssetContainer.UISheet);
      this.framescale = this.scalehelper.ScaleVector2(new Vector2((float) RoundFramePortrait.inversecirclerect.Width, (float) RoundFramePortrait.inversecirclerect.Height));
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateRoundFramePortrait(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawRoundFramePortrait(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.portrait.DrawAnimalInFrame(offset, spritebatch);
      this.invcircle.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}
