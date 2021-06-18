// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPair.ArtificialInsemination
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Audio;
using TinyZoo.PlayerDir;
using TinyZoo.Z_BreedScreen.ConfirmBreed;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPair
{
  internal class ArtificialInsemination
  {
    private ZCheckBox checkbox;
    public Vector2 Location;
    private Parents_AndChild parents_and_child;
    private PotentialBabies potentialbabies;
    private ZGenericText text;

    public ArtificialInsemination(Parents_AndChild _parents_and_child, float BaseScale)
    {
      this.text = new ZGenericText("Artificial Insemination: ", BaseScale);
      this.parents_and_child = _parents_and_child;
      this.checkbox = new ZCheckBox(BaseScale);
      if (this.parents_and_child.IsArticificalInsemination)
        this.checkbox.SetTicked(true);
      float num = 7f * BaseScale;
      this.checkbox.location.X += this.text.GetSize().X * 0.5f + num;
      this.text.vLocation.X -= num;
    }

    public float GetHeight() => this.text.GetSize().Y;

    public bool UpdateArtificialInsemination(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.checkbox.UpdateCheckBox(player, Offset))
        return false;
      this.parents_and_child.IsArticificalInsemination = !this.parents_and_child.IsArticificalInsemination;
      this.checkbox.SetTicked(this.parents_and_child.IsArticificalInsemination);
      PlayerStats.LastSetIsArticificalInsemination = this.parents_and_child.IsArticificalInsemination;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
      return true;
    }

    public void DrawArtificialInsemination(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.text.DrawZGenericText(Offset, spritebatch);
      this.checkbox.DrawCheckBox(spritebatch, Offset);
    }
  }
}
