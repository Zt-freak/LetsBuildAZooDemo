// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Buttons.Z_ManageButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.MainButtons;

namespace TinyZoo.Z_ManagePen.Buttons
{
  internal class Z_ManageButton
  {
    private GameObjectNineSlice BTNFrame;
    private GameObjectNineSlice BTNMouseOver;
    private Vector2 VSCale;
    private SpecialIcon specialIcon;
    public Vector2 Location;
    private string Text;
    public bool MouseMover;
    private GameObject TextObj;
    public ManageButtonType buttontype;

    public Z_ManageButton(ManageButtonType managebuttontype)
    {
      this.buttontype = managebuttontype;
      Vector3 SecondaryColour;
      this.BTNFrame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.VSCale = new Vector2(230f, 50f);
      this.BTNFrame.scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
      this.TextObj = new GameObject();
      this.TextObj.scale = 1f;
      this.TextObj.SetAllColours(SecondaryColour);
      this.specialIcon = new SpecialIcon(managebuttontype);
      this.specialIcon.scale = 0.8f;
      this.specialIcon.SetDrawOriginToCentre();
      this.specialIcon.vLocation.X = -40f;
      this.specialIcon.scale = 0.5f;
      this.TextObj.vLocation.X = 20f;
      this.TextObj.vLocation.Y = 5f;
      this.BTNMouseOver = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out SecondaryColour), 7);
      this.BTNMouseOver.scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
      this.BTNMouseOver.fAlpha = 0.3f;
      switch (managebuttontype)
      {
        case ManageButtonType.CleanPen:
          this.Text = "Cleanliness";
          break;
        case ManageButtonType.Feed:
          this.Text = "Feeding";
          break;
        case ManageButtonType.FoodChain:
          this.Text = "Food Chain";
          break;
        case ManageButtonType.PenSummary:
          this.Text = "Summary";
          break;
      }
    }

    public bool UpdateManageButtons(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      this.MouseMover = MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCale.X, this.VSCale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      return MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCale.X, this.VSCale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawManageButtons(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.BTNFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCale);
      if (this.MouseMover)
        this.BTNMouseOver.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCale);
      TextFunctions.DrawJustifiedText(this.Text, this.TextObj.scale * Sengine.ScreenRationReductionMultiplier.Y, this.TextObj.vLocation + Offset, this.TextObj.GetColour(), this.TextObj.fAlpha, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
      this.specialIcon.DrawSpecialIcon(Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
