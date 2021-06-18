// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.IAPIAPPAnel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SpringIAP;
using TinyZoo.GenericUI;
using TinyZoo.Utils;

namespace TinyZoo.IAPScreen
{
  internal class IAPIAPPAnel
  {
    private TextButton Advertbutton;
    private GenericBox uxpanel;
    private SimpleTextHandler simpletext;
    public Vector2 Location;
    private bool Purchased;

    public IAPIAPPAnel(SpringIAPManager springIAPmanager)
    {
      this.Purchased = false;
      string costString = springIAPmanager.GetCostString(AssetContainer.springFont, IAPHolder.GetIAPIDentifier(IAPTYPE.DisableAdverts));
      this.Advertbutton = new TextButton(SEngine.Localization.Localization.GetText(9) + " " + costString, 100f);
      this.Advertbutton.vLocation = new Vector2(0.0f, 100f);
      this.uxpanel = new GenericBox(new Vector2(400f, 400f * Sengine.ScreenRationReductionMultiplier.Y));
      this.simpletext = new SimpleTextHandler("- Disable all adverts. ~-Unlock permanent time control. ~- Unlock the magic space goats to boost your income by 5%.", true, 0.35f);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.Location.Y = -150f;
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      this.Location = new Vector2(244f, 490f);
      this.uxpanel.SetYellowOrange();
    }

    public bool UpdateIAPIAPPAnel(float DeltaTime, Player player, Vector2 Offset)
    {
      Offset += this.Location;
      if (!player.Stats.ADisabled(true, player))
        return this.Advertbutton.UpdateTextButton(player, Offset, DeltaTime);
      if (!this.Purchased)
      {
        this.Purchased = true;
        this.simpletext = new SimpleTextHandler("Thank you for supporting us, may your rule over the Prison Planet become a thing of legend!", true, 0.35f);
        this.simpletext.Location.Y = -150f;
        this.simpletext.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      }
      this.simpletext.UpdateSimpleTextHandler(DeltaTime);
      return false;
    }

    public void DrawIAPIAPPAnel(Vector2 Offset)
    {
      Offset += this.Location;
      this.uxpanel.DrawGenericBox(Offset);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      if (this.Purchased)
        return;
      this.Advertbutton.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
