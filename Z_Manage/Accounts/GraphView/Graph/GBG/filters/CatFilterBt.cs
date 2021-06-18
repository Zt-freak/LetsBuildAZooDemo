// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG.filters.CatFilterBt
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG.filters
{
  internal class CatFilterBt : GameObject
  {
    private GameObject ColourKey;
    private string Name;
    public bool IsOn;
    private GameObject FRAME;
    private Vector2 VSCALE;

    public CatFilterBt(string _Name, Vector3 Colour, Vector3 StrigCOlour)
    {
      this.IsOn = true;
      this.Name = _Name;
      this.ColourKey = new GameObject();
      this.ColourKey.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.ColourKey.scale = 4f;
      this.ColourKey.SetAllColours(Colour);
      this.ColourKey.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.ColourKey.vLocation.X = 4f;
      this.SetAllColours(StrigCOlour);
      this.FRAME = new GameObject();
      this.FRAME.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FRAME.SetDrawOriginToCentre();
      this.VSCALE = new Vector2(250f, 20f);
      this.FRAME.vLocation = new Vector2(this.VSCALE.X * 0.5f, 0.0f);
    }

    public bool UpdateCatFilterBt(Vector2 Offset, Player player)
    {
      if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 || !MathStuff.CheckPointCollision(true, Offset + new Vector2(this.VSCALE.X * 0.5f, 0.0f), 1f, this.VSCALE.X, this.VSCALE.Y, player.player.touchinput.ReleaseTapArray[0]))
        return false;
      this.IsOn = !this.IsOn;
      if (this.IsOn)
      {
        this.FRAME.SetAllColours(0.0f, 0.0f, 0.0f);
        this.FRAME.SetAlpha(0.2f);
        this.ColourKey.SetAlpha(1f);
      }
      else
      {
        this.ColourKey.SetAlpha(0.5f);
        this.FRAME.SetAllColours(1f, 1f, 1f);
        this.FRAME.SetAlpha(1f);
      }
      return true;
    }

    public void DrawCatFilterBt(Vector2 Offset)
    {
      this.ColourKey.scale = 15f;
      this.FRAME.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.ColourKey.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawTextWithDropShadow(this.Name, 3f * Sengine.ScreenRationReductionMultiplier.Y, Offset + new Vector2(25f, -10f), this.GetColour(), this.ColourKey.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
    }
  }
}
