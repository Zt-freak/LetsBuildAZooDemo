// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.VariantDiscovered.VariantDiscoveredPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedResult.VariantDiscovered
{
  internal class VariantDiscoveredPanel
  {
    private BigBrownPanel panel;
    private VariantDiscoveredInfoBox infoBox;
    private GenomesBar genomeBar;
    public Vector2 location;

    public VariantDiscoveredPanel(Player player, AnimalType type, int variant, float basescale)
    {
      float num = new UIScaleHelper(basescale).ScaleY(10f);
      this.infoBox = new VariantDiscoveredInfoBox(type, variant, basescale);
      this.genomeBar = new GenomesBar(player, type, variant, basescale);
      Vector2 vector2 = new Vector2();
      vector2.X = MathHelper.Max(this.infoBox.GetSize().X, this.genomeBar.GetSize().X);
      vector2.Y = this.infoBox.GetSize().Y + num + this.genomeBar.GetSize().Y;
      this.panel = new BigBrownPanel(vector2, true, "New!", basescale);
      this.infoBox.location = this.location;
      this.infoBox.location.Y = (float) (-0.5 * (double) vector2.Y + 0.5 * (double) this.infoBox.GetSize().Y);
      this.genomeBar.location.Y = (float) ((double) this.infoBox.location.Y + 0.5 * (double) this.infoBox.GetSize().Y + 0.5 * (double) this.genomeBar.GetSize().Y) + num;
      this.panel.Finalize(vector2);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateVariantDiscoveredPanel(Player player, float DeltaTime)
    {
      int num = 0 | (this.panel.UpdatePanelCloseButton(player, DeltaTime, this.location) ? 1 : 0);
      this.infoBox.UpdateVariantDiscoveredInfoBox(DeltaTime);
      this.genomeBar.UpdateGenomesBar(player, this.location, DeltaTime);
      return num != 0;
    }

    public void DrawVariantDiscoveredPanel(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.infoBox.DrawVariantDiscoveredInfoBox(offset, spritebatch);
      this.genomeBar.DrawGenomesBar(offset, spritebatch);
    }
  }
}
