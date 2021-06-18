// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CharacterSelect.SelectionFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_CharacterSelect
{
  internal class SelectionFrame
  {
    public GameObject[] Corners;
    public float Width;
    public float Height;

    public SelectionFrame(int _width, int _height, float _scale = 1f, bool UseWhite = false)
    {
      this.Corners = new GameObject[4];
      for (int index = 0; index < this.Corners.Length; ++index)
      {
        GameObject gameObject = new GameObject();
        switch (index)
        {
          case 0:
            gameObject.DrawRect = !UseWhite ? new Rectangle(128, 12, 8, 8) : new Rectangle(119, 30, 9, 9);
            gameObject.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
            break;
          case 1:
            gameObject.DrawRect = !UseWhite ? new Rectangle(137, 12, 8, 8) : new Rectangle(129, 30, 9, 9);
            gameObject.SetDrawOriginToPoint(DrawOriginPosition.TopRight);
            break;
          case 2:
            gameObject.DrawRect = !UseWhite ? new Rectangle(128, 21, 8, 8) : new Rectangle(119, 40, 9, 9);
            gameObject.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
            break;
          case 3:
            gameObject.DrawRect = !UseWhite ? new Rectangle(137, 21, 8, 8) : new Rectangle(129, 40, 9, 9);
            gameObject.SetDrawOriginToPoint(DrawOriginPosition.BottomRight);
            break;
        }
        this.Corners[index] = gameObject;
        this.Corners[index].scale = _scale;
        if (!UseWhite)
          this.Corners[index].SetAllColours(0.0f, 0.0f, 0.0f);
      }
      this.Width = (float) _width;
      this.Height = (float) _height;
    }

    public void UpdateSelectionFrame(float DeltaTime) => this.Corners[0].UpdateColours(DeltaTime);

    public void ScreenSpaceDrawInBuild(Vector2 Location, SpriteBatch spriteBatch)
    {
      this.Corners[0].vLocation = Location + new Vector2((float) (-(double) this.Width * 0.5) * Sengine.WorldOriginandScale.Z, (float) (-(double) this.Height * 0.5) * Sengine.WorldOriginandScale.Z * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.Corners[1].vLocation = Location + new Vector2(this.Width * 0.5f * Sengine.WorldOriginandScale.Z, (float) (-(double) this.Height * 0.5) * Sengine.WorldOriginandScale.Z * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.Corners[2].vLocation = Location + new Vector2((float) (-(double) this.Width * 0.5) * Sengine.WorldOriginandScale.Z, this.Height * 0.5f * Sengine.WorldOriginandScale.Z * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.Corners[3].vLocation = Location + new Vector2(this.Width * 0.5f * Sengine.WorldOriginandScale.Z, this.Height * 0.5f * Sengine.WorldOriginandScale.Z * Sengine.ScreenRatioUpwardsMultiplier.Y);
      for (int index = 0; index < this.Corners.Length; ++index)
        this.Corners[index].Draw(spriteBatch, AssetContainer.SpriteSheet, this.Corners[index].vLocation, Sengine.WorldOriginandScale.Z * 0.5f, 0.0f, true, this.Corners[index].DrawRect, this.Corners[0].fAlpha * 0.5f, this.Corners[0].GetColour());
    }

    public void DrawSelectionFrame(Vector2 Location, SpriteBatch spriteBatch)
    {
      this.Corners[0].vLocation = Location + new Vector2((float) (-(double) this.Width * 0.5), (float) (-(double) this.Height * 0.5));
      this.Corners[1].vLocation = Location + new Vector2(this.Width * 0.5f, (float) (-(double) this.Height * 0.5));
      this.Corners[2].vLocation = Location + new Vector2((float) (-(double) this.Width * 0.5), this.Height * 0.5f);
      this.Corners[3].vLocation = Location + new Vector2(this.Width * 0.5f, this.Height * 0.5f);
      for (int index = 0; index < this.Corners.Length; ++index)
        this.Corners[index].Draw(spriteBatch, AssetContainer.SpriteSheet, this.Corners[index].vLocation, this.Corners[0].scale, 0.0f, true, this.Corners[index].DrawRect, this.Corners[0].fAlpha, this.Corners[0].GetColour());
    }

    public void DrawSelectionFrame(Vector2 Location) => this.DrawSelectionFrame(Location, AssetContainer.pointspritebatchTop05);
  }
}
