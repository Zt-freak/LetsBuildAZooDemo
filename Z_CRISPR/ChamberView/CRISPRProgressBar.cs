// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ChamberView.CRISPRProgressBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedResult.VariantDiscovered;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_CRISPR.ChamberView
{
  internal class CRISPRProgressBar
  {
    private ProgressBarWithPointer progressBar;
    public Vector2 Location;
    private DNAIcon dnaIcon;
    private float iconBarBuffer;
    private CrisprActiveBreed refBreed;

    public CRISPRProgressBar(
      CrisprActiveBreed breed,
      float BaseScale,
      bool DrawHeader = true,
      bool DrawDNAicon = false)
    {
      this.refBreed = breed;
      string empty = string.Empty;
      string headerText = string.Empty;
      string pointerText = (double) breed.DaysLeft != 1.0 ? breed.DaysLeft.ToString() + " days" : breed.DaysLeft.ToString() + " day";
      if (DrawHeader)
        headerText = "CRISPR Progress";
      this.progressBar = new ProgressBarWithPointer(pointerText, 0.0f, BaseScale, headerText);
      this.progressBar.SetTint(new Vector3(0.8901961f, 0.6196079f, 0.4666667f));
      this.progressBar.SetPointerColor(new Vector3(0.9686275f, 0.8509804f, 0.6235294f), false);
      this.SetProgress();
      if (!DrawDNAicon)
        return;
      this.iconBarBuffer = 15f * BaseScale;
      this.dnaIcon = new DNAIcon(BaseScale);
      this.dnaIcon.SetUpSimpleAnimation();
      this.dnaIcon.SetDrawOriginToCentre();
      this.dnaIcon.vLocation.X = (float) (-(double) this.progressBar.GetWidth() * 0.5 - (double) this.dnaIcon.GetSize().X * 0.5);
      this.dnaIcon.vLocation.X -= this.iconBarBuffer;
    }

    public Vector2 GetBarSize() => new Vector2(this.progressBar.GetWidth(), this.progressBar.GetHeight());

    public float GetExtraOffsetFromTop() => this.progressBar.GetExtraOffsetFromTop();

    private void SetProgress()
    {
      this.progressBar.SetNewPointerPosition(this.refBreed.GetFloatPercentProgress());
      if ((double) this.refBreed.DaysLeft > 0.0)
      {
        if ((double) this.refBreed.DaysLeft == 1.0)
          this.progressBar.SetNewPointerText(this.refBreed.DaysLeft.ToString() + " day");
        else
          this.progressBar.SetNewPointerText(this.refBreed.DaysLeft.ToString() + " days");
      }
      else
        this.progressBar.SetNewPointerText(Z_GameFlags.FormatFloatToTime(MathHelper.Max(this.refBreed.TimeLeft_Seconds / Z_GameFlags.SecondsInDay, 0.0f)));
    }

    public void UpdateCRISPRProgressBar(float DeltaTime)
    {
      if (this.dnaIcon != null)
        this.dnaIcon.UpdateDNAIconAnimation(DeltaTime);
      this.SetProgress();
    }

    public float GetExtraIconOffset()
    {
      float num = 0.0f;
      if (this.dnaIcon != null)
        num += this.dnaIcon.GetSize().X + this.iconBarBuffer;
      return num;
    }

    public void DrawCRISPRProgressBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      this.progressBar.DrawProgressBar(offset, spriteBatch);
      if (this.dnaIcon == null)
        return;
      this.dnaIcon.DrawDNAIcon(offset, spriteBatch);
    }
  }
}
