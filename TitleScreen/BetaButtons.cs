// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.BetaButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.TitleScreen
{
  internal class BetaButtons
  {
    private DiscordButton discordbutton;
    private DiscordButton steambutton;
    private BigBrownPanel brownie;
    private LerpHandler_Float lerper;
    private Vector2 LOCATION;
    private SpringFont font;
    private float TScale;
    private GameObject obj;
    private CustomerFrame customerframe;

    public BetaButtons()
    {
      this.discordbutton = new DiscordButton();
      this.steambutton = new DiscordButton(false);
      this.steambutton.vLocation = new Vector2(-70f, 10f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.discordbutton.vLocation = new Vector2(70f, 10f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.LOCATION = new Vector2(200f, 380f);
      this.brownie = new BigBrownPanel(new Vector2(1f, 1f), addHeaderText: "Follow us", _BaseScale: Z_GameFlags.GetBaseScaleForUI());
      this.brownie.Finalize(new Vector2(300f, 100f * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      this.lerper.SetDelay(0.6f);
      this.TScale = Z_GameFlags.GetBaseScaleForUI();
      this.font = Z_GameFlags.GetSmallFont(ref this.TScale);
      this.obj = new GameObject();
      this.obj.SetAllColours(ColourData.Z_Cream);
      this.obj.vLocation = new Vector2(0.0f, -50f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.customerframe = new CustomerFrame(new Vector2(300f, 100f * Sengine.ScreenRatioUpwardsMultiplier.Y), BaseScale: Z_GameFlags.GetBaseScaleForUI());
    }

    public void UpdateBetaButtons(Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0)
        return;
      if (this.discordbutton.UpdateDiscordButton(DeltaTime, player, this.LOCATION))
        AppStoreLinker.LaunchBrowser("https://discord.com/invite/letsbuildazoo");
      if (!this.steambutton.UpdateDiscordButton(DeltaTime, player, this.LOCATION))
        return;
      AppStoreLinker.LaunchBrowser("https://store.steampowered.com/app/1547890/Lets_Build_a_Zoo/");
    }

    public void DrawBetaButtons()
    {
      Vector2 vector2 = new Vector2(this.lerper.Value * 900f, 0.0f);
      this.brownie.DrawBigBrownPanel(vector2 + this.LOCATION);
      Vector2 Offset = vector2 + this.LOCATION;
      this.customerframe.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText("Wishlist us on steam", this.TScale, Offset + this.steambutton.vLocation + this.obj.vLocation, this.obj.GetColour(), 1f, this.font, AssetContainer.pointspritebatchTop05);
      this.steambutton.DrawDiscordButton(Offset);
      TextFunctions.DrawJustifiedText("Join our Discord", this.TScale, Offset + this.discordbutton.vLocation + this.obj.vLocation, this.obj.GetColour(), 1f, this.font, AssetContainer.pointspritebatchTop05);
      this.discordbutton.DrawDiscordButton(Offset);
    }
  }
}
