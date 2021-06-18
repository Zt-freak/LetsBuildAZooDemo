// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing.NursingBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.ActiveBreedPair;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing
{
  internal class NursingBar
  {
    private ProgressBarWithPointer progressBar;
    public Vector2 Location;

    public NursingBar(
      Parents_AndChild parents_and_child,
      Player player,
      float BaseScale,
      float barWidth = 158f)
    {
      int nursingDayOptionToDays = TimeWithParents.GetNursingDayOptionToDays((TIMEWITHPARENTS) parents_and_child.NursingDaysOption);
      this.progressBar = new ProgressBarWithPointer(parents_and_child.NursingDays.ToString() + " days", (float) parents_and_child.NursingDays / (float) nursingDayOptionToDays, BaseScale, "Nursing Progress".ToUpper(), barWidth);
      this.progressBar.SetTint(new Vector3(1f, 0.7843137f, 0.7843137f));
    }

    public Vector2 GetSize() => new Vector2(this.progressBar.GetWidth(), this.progressBar.GetHeight());

    public float GetOffsetFromTop() => this.progressBar.GetExtraOffsetFromTop();

    public void UpdatePregnancyBar()
    {
    }

    public void DrawNursingBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      this.progressBar.DrawProgressBar(offset, spriteBatch);
    }
  }
}
