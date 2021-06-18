// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalIconsScalingDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalIconsScalingDisplay
  {
    private float basescale;
    private UIScaleHelper uiScale;
    private CustomerFrame frame;
    private Vector2 unscaledframesize;
    private Vector2 refframesize;
    private List<AnimalInFrame> icons = new List<AnimalInFrame>();
    private float iconscale;
    private float clampScale;
    public Vector2 location;
    private Vector2 referenceAnimalFrameSize;
    private static int numPerRow_ref = 3;
    private static int numPerRow_clamp = 4;
    private int maxPerRow;

    public AnimalIconsScalingDisplay(List<PrisonerInfo> infolist, float basescale_)
    {
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(this.basescale);
      float num1 = 50f;
      float num2 = 10f;
      float num3 = this.uiScale.ScaleX(num2);
      this.referenceAnimalFrameSize = new AnimalInFrame(infolist[0].intakeperson.animaltype, infolist[0].intakeperson.HeadType, infolist[0].intakeperson.CLIndex, num1 * this.basescale, BaseScale: (2f * this.basescale), HeadVariant: infolist[0].intakeperson.HeadVariant).GetSize();
      this.unscaledframesize.X = (float) ((double) infolist.Count * (double) this.referenceAnimalFrameSize.X + (double) (infolist.Count - 1) * (double) num3);
      this.unscaledframesize.Y = this.referenceAnimalFrameSize.Y;
      this.refframesize.X = this.uiScale.ScaleX((float) (3.0 * (double) num1 + 2.0 * (double) num2));
      this.refframesize.Y = this.uiScale.ScaleY(num1);
      this.iconscale = this.refframesize.X / this.unscaledframesize.X;
      this.clampScale = (float) (((double) AnimalIconsScalingDisplay.numPerRow_ref * (double) num1 + (double) (AnimalIconsScalingDisplay.numPerRow_ref - 1) * (double) num2) / ((double) AnimalIconsScalingDisplay.numPerRow_clamp * (double) num1 + (double) (AnimalIconsScalingDisplay.numPerRow_clamp - 1) * (double) num2)) * Sengine.ScreenRatioUpwardsMultiplier.X;
      this.iconscale = Math.Min(this.iconscale, this.clampScale);
      int num4 = 1;
      this.maxPerRow = infolist.Count;
      int num5 = 2;
      if ((double) this.iconscale < 0.5 * (double) this.clampScale)
      {
        num4 = 2;
        this.maxPerRow = AnimalIconsScalingDisplay.numPerRow_clamp * 2;
        this.iconscale = (float) (((double) AnimalIconsScalingDisplay.numPerRow_ref * (double) num1 + (double) (AnimalIconsScalingDisplay.numPerRow_ref - 1) * (double) num2) / ((double) this.maxPerRow * (double) num1 + (double) (this.maxPerRow - 1) * (double) num2));
      }
      if ((double) this.iconscale < 1.0 * (double) this.clampScale)
        num5 = 1;
      Vector2 vector2_1 = new Vector2();
      for (int index = 0; index < infolist.Count && index / this.maxPerRow <= 1; ++index)
      {
        if (index % this.maxPerRow == 0)
        {
          int num6 = index / this.maxPerRow;
          vector2_1 = Vector2.Zero;
          vector2_1.X -= num3 * this.iconscale;
          vector2_1.Y = (float) num6 * this.iconscale * this.uiScale.ScaleY(num2 + num1);
        }
        vector2_1.X += num3 * this.iconscale;
        PrisonerInfo prisonerInfo = infolist[index];
        AnimalType animaltype = prisonerInfo.intakeperson.animaltype;
        AnimalType headType = prisonerInfo.intakeperson.HeadType;
        int clIndex = prisonerInfo.intakeperson.CLIndex;
        int headVariant = prisonerInfo.intakeperson.HeadVariant;
        AnimalInFrame animalInFrame = new AnimalInFrame(animaltype, headType, clIndex, num1 * this.basescale * this.iconscale, BaseScale: ((float) num5 * this.basescale), HeadVariant: headVariant);
        animalInFrame.Location = vector2_1;
        this.icons.Add(animalInFrame);
        vector2_1.X += animalInFrame.GetSize().X;
      }
      Vector2 vector2_2 = new Vector2();
      Vector2 vector2_3 = 0.5f * this.icons[0].GetSize() - 0.5f * this.GetSize();
      float num7 = (float) (0.5 * (double) this.icons[0].GetSize().X - 0.5 * (double) vector2_1.X);
      for (int index = 0; index < this.icons.Count; ++index)
      {
        AnimalInFrame icon = this.icons[index];
        icon.Location.Y += vector2_3.Y;
        if (index / this.maxPerRow == num4 - 1)
          icon.Location.X += num7;
        else
          icon.Location.X += vector2_3.X;
      }
    }

    public Vector2 GetSize() => new Vector2(this.refframesize.X, this.referenceAnimalFrameSize.Y * this.clampScale);

    public bool UpdateAnimalIconsScalingDisplay(float DeltaTime) => false;

    public void DrawAnimalIconsScalingDisplay(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      foreach (AnimalInFrame icon in this.icons)
        icon.DrawAnimalInFrame(offset, spritebatch);
    }
  }
}
