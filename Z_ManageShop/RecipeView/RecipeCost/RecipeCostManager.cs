// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.RecipeCost.RecipeCostManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_ManageShop.RecipeView.RecipeCost
{
  internal class RecipeCostManager
  {
    private GameObjectNineSlice frameobject;
    public Vector2 Position;
    private ScreenHeading Name;
    private Vector2 VSCALE;
    private GameObject TEXTOBJECT;
    private int Cost;

    public RecipeCostManager()
    {
      Vector3 SecondaryColour;
      this.frameobject = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.Name = new ScreenHeading("RECIPE");
      this.Name.header.vLocation = Vector2.Zero;
      this.VSCALE = new Vector2(400f, 80f);
      this.frameobject.scale = 2f;
      this.TEXTOBJECT = new GameObject();
      this.TEXTOBJECT.SetAllColours(SecondaryColour);
      this.Position = new Vector2(512f, 280f);
    }

    public void UpdateRecipeCostManager(int _Cost) => this.Cost = _Cost;

    public void DrawRecipeCostManager(Vector2 Offset)
    {
      Offset += this.Position;
      this.frameobject.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.Name.DrawScreenHeading(Offset + new Vector2(0.0f, this.VSCALE.Y * -0.5f), AssetContainer.pointspritebatch03);
      TextFunctions.DrawTextWithDropShadow("COST PER SERVING", RenderMath.GetPixelSizeBestMatch(0.7f * Sengine.ScreenRationReductionMultiplier.Y), Offset + new Vector2(this.VSCALE.X * -0.5f, -10f) + new Vector2(20f, 0.0f), this.TEXTOBJECT.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false, false);
      TextFunctions.DrawTextWithDropShadow("$" + (object) this.Cost, RenderMath.GetPixelSizeBestMatch(1.5f * Sengine.ScreenRationReductionMultiplier.Y), Offset + new Vector2(-20f, 0.0f) + new Vector2(this.VSCALE.X * 0.5f, -20f), this.TEXTOBJECT.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false, true);
    }
  }
}
