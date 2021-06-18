// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.IAPAdvertPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.IAPScreen
{
  internal class IAPAdvertPanel
  {
    private TextButton Advertbutton;
    private GenericBox uxpanel;
    private SimpleTextHandler simpletext;
    public Vector2 Location;
    private bool AllowButton;
    private GameObject timeleftob;
    private string TME;

    public IAPAdvertPanel(Player player)
    {
      this.Advertbutton = new TextButton("Watch Advert", 100f);
      this.Advertbutton.vLocation = new Vector2(0.0f, 100f);
      this.uxpanel = new GenericBox(new Vector2(400f, 400f * Sengine.ScreenRationReductionMultiplier.Y));
      this.simpletext = new SimpleTextHandler("Use time travel control device. Cash earning rate 2X for 10 minutes!", true, 0.35f);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.Location.Y = -150f;
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      this.Location = new Vector2(780f, 490f);
      this.uxpanel.SetYellowOrange();
      this.timeleftob = new GameObject();
      this.timeleftob.SetAllColours(ColourData.FernLemon);
      if (player.Stats.TimeTravelIsActiveFromAdvert())
        return;
      this.AllowButton = true;
    }

    public bool UpdateIAPAdvertPanel(float DeltaTime, Player player, Vector2 Offset)
    {
      if (player.Stats.ADisabled(false, player))
      {
        if (this.Advertbutton != null)
        {
          this.Advertbutton = (TextButton) null;
          this.simpletext = (SimpleTextHandler) null;
        }
        return false;
      }
      Offset += this.Location;
      if (!player.Stats.TimeTravelIsActiveFromAdvert())
      {
        this.AllowButton = true;
        return this.Advertbutton.UpdateTextButton(player, Offset, DeltaTime);
      }
      this.AllowButton = false;
      this.TME = player.Stats.TimeTravelTimeLeft() + " " + SEngine.Localization.Localization.GetText(59);
      return false;
    }

    public void DrawIAPAdvertPanel(Vector2 Offset)
    {
      Offset += this.Location;
      this.uxpanel.DrawGenericBox(Offset);
      if (this.simpletext == null)
        return;
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      if (this.AllowButton)
        this.Advertbutton.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      else
        TextFunctions.DrawJustifiedText(this.TME, 3f, Offset + this.Advertbutton.vLocation, this.timeleftob.GetColour(), this.timeleftob.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}
