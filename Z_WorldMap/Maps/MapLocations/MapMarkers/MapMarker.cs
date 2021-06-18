// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers.MapMarker
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tutorials;

namespace TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers
{
  internal class MapMarker : GameObject
  {
    private Arrow arrow;
    public CityName city;
    private RedLight redlight;

    public MapMarker(CityName _city, Player player)
    {
      this.city = _city;
      this.vLocation = QuestData.GetCityLocation(this.city);
      this.vLocation = this.vLocation - new Vector2(8f, 8f);
      this.vLocation.Y *= Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.arrow = new Arrow();
      this.arrow.SetAlpha(0.0f);
      bool _IsFlashing = true;
      if (this.city == CityName.Shelter && player.shelterstocks.shelteredanimal.Count == 0)
        _IsFlashing = false;
      this.redlight = new RedLight(_IsFlashing, this.city == CityName.Shelter);
    }

    public void SetSmall() => this.redlight.SetSmall();

    public bool UpdateMapMarker(float DeltaTime, Player player, bool MouseIsOverPopUpPanel)
    {
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(this.vLocation);
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !MouseIsOverPopUpPanel && MathStuff.CheckPointCollision(true, screenSpace, 1f, 60f, 60f, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawMapMarker()
    {
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(this.vLocation);
      this.redlight.DrawRedLight(AssetContainer.pointspritebatch0, AssetContainer.PointBlendBatch02, screenSpace);
      bool WasOffScreen;
      Vector2 ScreenEdge;
      float Rotation;
      MathStuff.GetPointingOffScreen(screenSpace, out WasOffScreen, out ScreenEdge, out Rotation, 40f);
      if (!WasOffScreen)
        return;
      this.arrow.scale = 2f;
      this.arrow.fAlpha = 1f;
      this.arrow.SetDrawOriginToCentre();
      this.arrow.Rotation = Rotation;
      this.arrow.DrawArrow(ScreenEdge);
    }
  }
}
