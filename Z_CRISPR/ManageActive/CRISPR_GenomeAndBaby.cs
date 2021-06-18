// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ManageActive.CRISPR_GenomeAndBaby
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_CRISPR.ChamberView;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_CRISPR.ManageActive
{
  internal class CRISPR_GenomeAndBaby
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private CRISPR_GenomePair genomePair;
    private AnimalInTube animalInTube;
    private CRISPRProgressBar progressBar;
    private CrisprActiveBreed refBreed;
    private ZGenericText textObj;
    private bool WasReady;

    public CRISPR_GenomeAndBaby(CrisprActiveBreed breed, float BaseScale, float forceWidth)
    {
      this.refBreed = breed;
      float num1 = 0.0f;
      float num2 = 10f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num3 = num1 + num2;
      this.genomePair = new CRISPR_GenomePair(breed, BaseScale);
      this.genomePair.location.Y = num3;
      float num4 = num3 + this.genomePair.GetHeight() + num2;
      this.animalInTube = new AnimalInTube(breed.resultBody, breed.resultHead, breed.resultBodyVariant, breed.resultHeadVariant, BaseScale, breed.GetFloatPercentProgress());
      this.animalInTube.location.Y = num4;
      this.animalInTube.location.Y += this.animalInTube.GetSize().Y * 0.5f;
      float num5 = num4 + this.animalInTube.GetSize().Y + num2;
      this.progressBar = new CRISPRProgressBar(breed, BaseScale, DrawDNAicon: true);
      this.progressBar.Location.Y = num5;
      this.progressBar.Location.Y += this.progressBar.GetExtraOffsetFromTop();
      float num6 = num5 + (this.progressBar.GetBarSize().Y + (this.progressBar.GetExtraOffsetFromTop() - this.progressBar.GetBarSize().Y * 0.5f)) + num2 * 0.5f;
      this.textObj = new ZGenericText("X", BaseScale);
      float y1 = this.textObj.GetSize().Y;
      this.textObj.vLocation.Y = num6;
      float y2 = num6 + y1;
      this.customerFrame = new CustomerFrame(new Vector2(forceWidth, y2), BaseScale: BaseScale);
      Vector2 vector2 = new Vector2(0.0f, (float) (-(double) this.customerFrame.VSCale.Y * 0.5));
      this.genomePair.location.Y += vector2.Y;
      this.animalInTube.location.Y += vector2.Y;
      this.progressBar.Location.Y += vector2.Y;
      if (this.textObj != null)
        this.textObj.vLocation.Y += vector2.Y;
      this.SetNewProgressState();
    }

    private void SetNewProgressState()
    {
      string str = string.Empty;
      if ((double) this.refBreed.DaysLeft > 0.0)
        str = "Your animal is being grown.";
      else if (this.refBreed.IsBorn_CanCollect)
        str = "Your new animal is ready!";
      this.textObj.textToWrite = str;
      this.WasReady = this.refBreed.IsBorn_CanCollect;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateCRISPR_GenomeAndBaby(float DeltaTime)
    {
      this.genomePair.UpdateCRISPR_GenomePair(DeltaTime);
      this.animalInTube.UpdateAnimalInTube(DeltaTime);
      this.progressBar.UpdateCRISPRProgressBar(DeltaTime);
      if (this.WasReady || !this.refBreed.IsBorn_CanCollect)
        return;
      this.SetNewProgressState();
    }

    public void DrawCRISPR_GenomeAndBaby(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.genomePair.DrawCRISPR_GenomePair(offset, spriteBatch);
      this.animalInTube.DrawAnimalInTube(offset, spriteBatch);
      this.progressBar.DrawCRISPRProgressBar(offset, spriteBatch);
      if (this.textObj == null)
        return;
      this.textObj.DrawZGenericText(offset, spriteBatch);
    }
  }
}
