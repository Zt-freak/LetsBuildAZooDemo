// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.CatSelector.Z_CatSelect
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.CatSelector
{
  internal class Z_CatSelect
  {
    private Vector2 VSCALE;
    private GameObjectNineSlice Frame;
    private LerpHandler_Float lerper;
    private Vector2 Offset;
    private Z_CatButtons catbuttons;

    public Z_CatSelect(Player player)
    {
      Vector3 SecondaryColour;
      this.Frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.VSCALE = new Vector2(175f, 768f - Z_BuildingIconPanel.MinHeight);
      this.Frame.vLocation = new Vector2(512f, (float) ((768.0 - (double) Z_BuildingIconPanel.MinHeight) * 0.5));
      this.Frame.vLocation.X = this.VSCALE.X * 0.5f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.catbuttons = new Z_CatButtons(SecondaryColour, player);
    }

    public void Exit() => this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);

    public void TryToCycleSelection(DirectionPressed direction) => this.catbuttons.TryToCycleSelection(direction);

    public CATEGORYTYPE UpdateZ_CatSelect(
      float DeltaTime,
      Player player,
      CATEGORYTYPE SelectedCategory)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.Offset.Y = Z_BuildingIconPanel.MinHeight + this.lerper.Value * 200f;
      return this.catbuttons.UpdateZ_CatButtons(player, DeltaTime, this.Offset + this.Frame.vLocation, SelectedCategory);
    }

    public void DrawZ_CatSelect(CATEGORYTYPE selectedcat)
    {
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Offset, this.VSCALE);
      this.catbuttons.DrawZ_CatButtons(this.Offset + this.Frame.vLocation, selectedcat);
    }
  }
}
