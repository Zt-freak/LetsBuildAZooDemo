// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.OffspringPercentageBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class OffspringPercentageBar
  {
    private SatisfactionBar offspringposibilityBar;
    public Vector2 Location;

    public OffspringPercentageBar(Parents_AndChild Info, float BaseScale)
    {
      int MaleDupliacePercent;
      int FemaleDuplicatePercent;
      Info.GetPercentages(out int _, out MaleDupliacePercent, out FemaleDuplicatePercent);
      this.offspringposibilityBar = new SatisfactionBar(1f, BaseScale);
      if (MaleDupliacePercent > -1)
      {
        float Percentage = (float) MaleDupliacePercent * 0.01f;
        if (FemaleDuplicatePercent > -1)
          Percentage += (float) FemaleDuplicatePercent * 0.01f;
        this.offspringposibilityBar.AddNewBar(Percentage, new Vector3(0.7f, 0.7f, 0.3f), 1);
      }
      if (FemaleDuplicatePercent > -1)
        this.offspringposibilityBar.AddNewBar((float) FemaleDuplicatePercent * 0.01f, new Vector3(0.2f, 0.6f, 0.2f), 2);
      this.SetBarColours(ColourData.ThreeBluesForBabies[0], ColourData.ThreeBluesForBabies[1], ColourData.ThreeBluesForBabies[2]);
    }

    private void SetBarColours(Vector3 color1, Vector3 color2, Vector3 color3)
    {
      this.offspringposibilityBar.SetBarColours(color3);
      this.offspringposibilityBar.SetBarColours(color2, 1);
      this.offspringposibilityBar.SetBarColours(color1, 2);
    }

    public Vector2 GetSize() => this.offspringposibilityBar.GetSize();

    public void DrawOffspringPercentageBar(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.offspringposibilityBar.DrawSatisfactionBar(Offset, spritebatch);
    }
  }
}
