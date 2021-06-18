// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ManageActive.CRISPR_StatusAndAction
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_CRISPR.ManageActive
{
  internal class CRISPR_StatusAndAction
  {
    public Vector2 location;
    private TextButton stopButton;
    private TextButton sellButton;
    private TextButton putInPenButton;
    private SimpleTextHandler statusText;
    private float totalHeight;
    private float forceWidth;
    private AnimalInFrame animalPreview;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private bool HasSellButton;
    private float BaseScale;

    public bool isNewCollection { get; private set; }

    public CRISPR_StatusAndAction(
      CrisprActiveBreed breed,
      float _BaseScale,
      float _forceWidth,
      Player player)
    {
      this.BaseScale = _BaseScale;
      this.forceWidth = _forceWidth;
      this.Create(breed, player);
    }

    public void Create(CrisprActiveBreed breed, Player player)
    {
      this.stopButton = (TextButton) null;
      this.sellButton = (TextButton) null;
      this.putInPenButton = (TextButton) null;
      this.statusText = (SimpleTextHandler) null;
      this.miniHeading = (MiniHeading) null;
      this.customerFrame = (CustomerFrame) null;
      float num1 = 10f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.totalHeight = 0.0f;
      this.totalHeight += num1;
      float num2 = 20f * this.BaseScale;
      Vector2 vector2_1 = new Vector2(10f, 10f);
      int num3 = breed.IsBorn_CanCollect ? 1 : 0;
      this.isNewCollection = !player.unlocks.GetIsHybridDiscovered(breed.resultBody, breed.resultHead, breed.resultBodyVariant, breed.resultHeadVariant);
      this.miniHeading = new MiniHeading(Vector2.Zero, "CRISPR Action", 1f, this.BaseScale);
      this.totalHeight += this.miniHeading.GetTextHeight() * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.totalHeight += vector2_1.Y * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      Vector3 zCream = ColourData.Z_Cream;
      CustomerFrameColors color = CustomerFrameColors.Brown;
      if (num3 != 0)
        color = CustomerFrameColors.Gold;
      if (num3 == 0)
      {
        this.totalHeight += num1;
        this.stopButton = new TextButton(this.BaseScale, "Stop", 50f, _OverrideFrameScale: this.BaseScale);
        this.stopButton.SetButtonColour(BTNColour.Red);
        this.stopButton.vLocation.Y = this.totalHeight;
        this.stopButton.vLocation.Y += this.stopButton.GetSize().Y * 0.5f;
        this.totalHeight += this.stopButton.GetSize().Y;
        this.totalHeight += num1;
      }
      else
      {
        this.animalPreview = new AnimalInFrame(breed.resultBody, breed.resultHead, breed.resultBodyVariant, 50f * this.BaseScale, BaseScale: (this.BaseScale * 2f), HeadVariant: breed.resultHeadVariant);
        this.animalPreview.Location.Y = this.totalHeight;
        this.animalPreview.Location.X -= num2 + this.animalPreview.GetSize().X * 0.5f;
        this.animalPreview.Location.Y += this.animalPreview.GetSize().Y * 0.5f;
        this.animalPreview.SetFrameColour(ColourData.Z_CreamFADED);
        if (this.isNewCollection)
          this.animalPreview.SetAnimalGreyedOut(true, false);
        this.totalHeight += this.animalPreview.GetSize().Y;
        if (this.HasSellButton)
          throw new Exception("TODO fix layout");
        this.putInPenButton = new TextButton(this.BaseScale, "Collect", 50f, _OverrideFrameScale: this.BaseScale);
        this.putInPenButton.vLocation.Y = this.animalPreview.Location.Y;
        this.putInPenButton.vLocation.X += (float) ((double) num2 * 0.5 + (double) this.putInPenButton.GetSize().X * 0.5);
        this.totalHeight += num1;
      }
      string empty = string.Empty;
      string TextToWrite = num3 != 0 ? "" : "You may stop the process anytime.";
      if (!string.IsNullOrEmpty(TextToWrite))
      {
        this.totalHeight += num1;
        this.statusText = new SimpleTextHandler(TextToWrite, true, (float) ((double) this.forceWidth / 1024.0 * 0.899999976158142), this.BaseScale);
        this.statusText.SetAllColours(zCream);
        this.statusText.Location.Y = this.totalHeight + this.statusText.GetHeightOfOneLine() * 0.5f;
        this.statusText.AutoCompleteParagraph();
        this.totalHeight += this.statusText.GetHeightOfParagraph();
        this.totalHeight += num1;
      }
      else
        this.totalHeight += num1;
      this.customerFrame = new CustomerFrame(new Vector2(this.forceWidth, this.totalHeight), color, this.BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_2 = new Vector2(0.0f, (float) (-(double) this.customerFrame.VSCale.Y * 0.5));
      if (this.stopButton != null)
      {
        TextButton stopButton = this.stopButton;
        stopButton.vLocation = stopButton.vLocation + vector2_2;
      }
      if (this.sellButton != null)
      {
        TextButton sellButton = this.sellButton;
        sellButton.vLocation = sellButton.vLocation + vector2_2;
      }
      if (this.putInPenButton != null)
      {
        TextButton putInPenButton = this.putInPenButton;
        putInPenButton.vLocation = putInPenButton.vLocation + vector2_2;
      }
      if (this.animalPreview != null)
        this.animalPreview.Location += vector2_2;
      if (this.statusText == null)
        return;
      this.statusText.Location += vector2_2;
    }

    public float GetHeight() => this.customerFrame.VSCale.Y;

    public bool UpdateCRISPR_StatusAndAction(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool throwBabyOut,
      out bool isSell,
      out bool isPutInPen)
    {
      offset += this.location;
      throwBabyOut = false;
      isSell = false;
      isPutInPen = false;
      if (this.stopButton != null && this.stopButton.UpdateTextButton(player, offset, DeltaTime))
      {
        throwBabyOut = true;
        return true;
      }
      if (this.sellButton != null && this.sellButton.UpdateTextButton(player, offset, DeltaTime))
      {
        isSell = true;
        return true;
      }
      if (this.putInPenButton == null || !this.putInPenButton.UpdateTextButton(player, offset, DeltaTime))
        return false;
      isPutInPen = true;
      return true;
    }

    public void DrawCRISPR_StatusAndAction(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      if (this.statusText != null)
        this.statusText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.stopButton != null)
        this.stopButton.DrawTextButton(offset, 1f, spriteBatch);
      if (this.sellButton != null)
        this.sellButton.DrawTextButton(offset, 1f, spriteBatch);
      if (this.putInPenButton != null)
        this.putInPenButton.DrawTextButton(offset, 1f, spriteBatch);
      if (this.animalPreview != null)
        this.animalPreview.DrawAnimalInFrame(offset, spriteBatch);
      if (this.statusText == null)
        return;
      this.statusText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
