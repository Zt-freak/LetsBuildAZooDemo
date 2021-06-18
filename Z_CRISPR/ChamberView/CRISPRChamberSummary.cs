// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ChamberView.CRISPRChamberSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedScreen.BreedChambers;

namespace TinyZoo.Z_CRISPR.ChamberView
{
  internal class CRISPRChamberSummary
  {
    public Vector2 location;
    private TextButton manageButton;
    private CRISPRProgressBar progressBar;
    private float totalHeight;
    private AnimalInTube animalInTube;
    private ActiveIcon activeIcon;
    private CrisprActiveBreed refBreed;
    private bool DrawActiveIcon;

    public CRISPRChamberSummary(CrisprActiveBreed breed, float BaseScale)
    {
      this.refBreed = breed;
      this.totalHeight = 0.0f;
      float num = 5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.animalInTube = new AnimalInTube(breed.resultBody, breed.resultHead, breed.resultBodyVariant, breed.resultHeadVariant, BaseScale, breed.GetFloatPercentProgress());
      this.animalInTube.location.Y = this.totalHeight;
      this.animalInTube.location.Y += this.animalInTube.GetSize().Y * 0.5f;
      this.totalHeight += this.animalInTube.GetSize().Y;
      this.totalHeight += num;
      this.progressBar = new CRISPRProgressBar(breed, BaseScale, DrawDNAicon: true);
      this.progressBar.Location.Y = this.totalHeight;
      this.progressBar.Location.Y += this.progressBar.GetExtraOffsetFromTop();
      this.totalHeight += this.progressBar.GetBarSize().Y - this.progressBar.GetExtraOffsetFromTop();
      this.totalHeight += num;
      this.activeIcon = new ActiveIcon(true, BaseScale);
      this.activeIcon.vLocation = this.animalInTube.location;
      this.activeIcon.vLocation.X += this.animalInTube.GetSize().X * 0.5f;
      this.activeIcon.vLocation.X += 50f * BaseScale;
      this.manageButton = new TextButton(BaseScale, "Manage", 50f, _OverrideFrameScale: BaseScale);
      this.manageButton.vLocation.Y = this.totalHeight + this.manageButton.GetSize().Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.totalHeight += this.manageButton.GetSize().Y * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.SetState();
    }

    public float GetHeight() => this.totalHeight;

    private void SetState() => this.DrawActiveIcon = this.refBreed.IsBorn_CanCollect;

    public bool UpdateActiveSummaryChamber(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.progressBar.UpdateCRISPRProgressBar(DeltaTime);
      this.animalInTube.UpdateAnimalInTube(DeltaTime);
      this.SetState();
      return this.manageButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawActiveSummaryChamber(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.manageButton.DrawTextButton(offset, 1f, spriteBatch);
      this.animalInTube.DrawAnimalInTube(offset, spriteBatch);
      this.progressBar.DrawCRISPRProgressBar(offset, spriteBatch);
      if (!this.DrawActiveIcon)
        return;
      this.activeIcon.DrawActiveIcon(spriteBatch, offset);
    }
  }
}
