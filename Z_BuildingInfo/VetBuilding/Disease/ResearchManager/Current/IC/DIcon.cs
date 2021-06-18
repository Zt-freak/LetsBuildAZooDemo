// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC.DIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC
{
  internal class DIcon : GameObject
  {
    public DIcon(DiseaseType diseasetype, float BaseScale)
    {
      switch (diseasetype)
      {
        case DiseaseType.KnownDisease:
          this.DrawRect = new Rectangle(376, 438, 19, 20);
          break;
        case DiseaseType.UnknownDisease:
          this.DrawRect = new Rectangle(128, 615, 17, 21);
          break;
        default:
          this.DrawRect = new Rectangle(712, 920, 22, 19);
          break;
      }
      this.scale = BaseScale;
      this.SetDrawOriginToCentre();
    }

    public void DrawDIcon(SpriteBatch spritebatch, Vector2 Offset) => this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
  }
}
