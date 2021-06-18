// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.Characters.TalkingHead
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.GenericUI.Characters
{
  internal class TalkingHead : GameObject
  {
    private GameObject PortraitIcon;
    private EnemyRenderer enemyrenderer;

    public TalkingHead(
      AnimalType character,
      int Variant,
      float ScaleMULT = 1f,
      AnimalType ReplacementHead = AnimalType.None,
      float BaseScale = -1f)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.scale = 100f * ScaleMULT;
      if (character < AnimalType.SecretAnimalsCount)
      {
        this.enemyrenderer = new EnemyRenderer(character, Variant, ReplacementHead);
        this.enemyrenderer.scale = (float) Math.Max(this.enemyrenderer.DrawRect.Width + 2, this.enemyrenderer.DrawRect.Height + 10);
        this.enemyrenderer.scale = this.scale / this.enemyrenderer.scale;
        this.enemyrenderer.SetDrawOriginToCentre();
      }
      this.PortraitIcon = new GameObject();
      this.PortraitIcon.DrawRect = EnemyData.GetEnemyPortraitIcon(character, Variant);
      this.PortraitIcon.SetDrawOriginToCentre();
      if ((double) BaseScale != -1.0)
        this.PortraitIcon.scale = BaseScale;
      else
        this.PortraitIcon.scale = 2f * ScaleMULT;
    }

    public Vector2 GetSize() => new Vector2((float) this.PortraitIcon.DrawRect.Width, (float) this.PortraitIcon.DrawRect.Width) * this.PortraitIcon.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void SetLocked() => this.PortraitIcon.SetAllColours(0.0f, 0.0f, 0.0f);

    public void SetLemon() => this.SetAllColours(ColourData.FernLemon);

    public void SetEdgeRed() => this.SetAllColours(ColourData.FernRed);

    public void DrawTalkingHead(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.PortraitIcon.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation);
      if (this.enemyrenderer == null)
        return;
      this.enemyrenderer.ScreenSpaceDrawEnemyRenderer(Offset + this.vLocation);
    }
  }
}
