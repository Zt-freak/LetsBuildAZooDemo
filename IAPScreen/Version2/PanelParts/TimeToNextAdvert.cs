// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.PanelParts.TimeToNextAdvert
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.IAPScreen.Version2.PanelParts
{
  internal class TimeToNextAdvert
  {
    private GameObject timeleftob;
    public bool AllowButton;
    private string TME;
    private WatchVideoButton watchvideo;

    public TimeToNextAdvert()
    {
      this.TME = "";
      this.timeleftob = new GameObject();
      this.timeleftob.SetAllColours(ColourData.FernLemon);
      this.watchvideo = new WatchVideoButton("Watch");
      this.watchvideo.scale *= Sengine.UltraWideSreenDownardsMultiplier;
      this.watchvideo.TextScale *= Sengine.UltraWideSreenDownardsMultiplier;
      this.watchvideo.AddControllerButton(ControllerButton.XboxA, true);
    }

    public bool UpdateTimeToNextAdvert(Player player, Vector2 Offset)
    {
      Offset.Y -= 50f;
      if (!player.Stats.TimeTravelIsActiveFromAdvert())
      {
        this.AllowButton = true;
        return this.watchvideo.UpdateWatchVideoButton(player, Offset, true);
      }
      this.AllowButton = false;
      this.TME = player.Stats.TimeTravelTimeLeft() + " " + SEngine.Localization.Localization.GetText(59);
      return false;
    }

    public void DrawTimeToNextAdvert(Vector2 Offset)
    {
      Offset.Y -= 50f;
      if (this.AllowButton)
        this.watchvideo.DrawWatchVideoButton(Offset, GameFlags.IsUsingController);
      else
        TextFunctions.DrawJustifiedText(this.TME, 3f, Offset + this.watchvideo.vLocation, this.timeleftob.GetColour(), this.timeleftob.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}
