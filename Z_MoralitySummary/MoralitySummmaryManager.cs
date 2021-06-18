// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralitySummmaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralitySummmaryManager
  {
    private static Vector2 REFERENCE_SCREEN_RES = new Vector2(Sengine.ReferenceScreenRes.X, Sengine.ReferenceScreenRes.Y);
    private List<MoralitySummaryCategory> categories = new List<MoralitySummaryCategory>();
    private StoreBGManager bg = new StoreBGManager();
    private BackButton backbutton = new BackButton();
    private ScreenHeading screenheading;
    private Vector2 offset = Vector2.Zero;
    private MoralityScoreDisplay_Overall scoreDisplay;
    private Vector2 scoreDisplayPadding = new Vector2(20f, 0.1f * MoralitySummmaryManager.REFERENCE_SCREEN_RES.X);
    private float SPACING = 10f;
    private float offsetYMin;
    private float moralityScore;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;
    private Vector2 top;

    public MoralitySummmaryManager(Player player)
    {
      float num = Z_GameFlags.GetBaseScaleForUI() * 1f;
      this.scalehelper = new UIScaleHelper(num);
      this.screenheading = new ScreenHeading(SEngine.Localization.Localization.GetText(985).ToUpper(), 57.5f * num);
      this.scoreDisplay = new MoralityScoreDisplay_Overall(num);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.framescale = new Vector2();
      for (int index = 0; index < 6; ++index)
      {
        MoralitySummaryCategory moralitySummaryCategory = new MoralitySummaryCategory(player, (MoralityCategory) index, num);
        this.categories.Add(moralitySummaryCategory);
        this.framescale.X = Math.Max(this.framescale.X, moralitySummaryCategory.GetSize().X);
        this.framescale.Y += moralitySummaryCategory.GetSize().Y + defaultBuffer.Y;
      }
      this.framescale.Y -= defaultBuffer.Y;
      Vector2 zero = Vector2.Zero;
      foreach (MoralitySummaryCategory category in this.categories)
      {
        category.location = zero;
        zero.Y += category.GetSize().Y + defaultBuffer.Y;
      }
      this.scoreDisplay.location = this.scalehelper.ScaleVector2(new Vector2(20f, 80f)) + 0.5f * this.scoreDisplay.GetSize();
      this.top = Sengine.HalfReferenceScreenRes;
      this.top.Y = this.scalehelper.ScaleY(80f) + 0.5f * this.categories[0].GetSize().Y;
    }

    public void UpdateMoralitySummary(Player player, float DeltaTime)
    {
      this.bg.UpdateStoreBGManager(DeltaTime);
      foreach (MoralitySummaryCategory category in this.categories)
        category.UpdateMoralitySummaryCategory(DeltaTime);
      if (this.backbutton.UpdateBackButton(player, DeltaTime))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuClose);
        TinyZoo.Game1.screenfade.BeginFade(true);
        TinyZoo.Game1.SetNextGameState(TinyZoo.Game1.Previousgamestate);
      }
      this.offset.Y += 0.7f * player.inputmap.momentumwheel.MovementThisFrame;
      this.offset.Y = (double) this.offset.Y > 0.0 ? 0.0f : ((double) this.offset.Y < (double) MathHelper.Min(0.0f, this.offsetYMin) ? MathHelper.Min(0.0f, this.offsetYMin) : this.offset.Y);
    }

    public void DrawMoralitySummary()
    {
      this.bg.DrawStoreBGManager(Vector2.Zero);
      this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.scoreDisplay.DrawMoralityScoreDisplay_Overall(Vector2.Zero);
      foreach (MoralitySummaryCategory category in this.categories)
        category.DrawMoralitySummaryCategory(this.offset + this.top);
      this.backbutton.DrawBackButton(Vector2.Zero);
    }
  }
}
