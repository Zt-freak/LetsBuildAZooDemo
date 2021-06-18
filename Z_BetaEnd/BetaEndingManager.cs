// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BetaEnd.BetaEndingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.TitleScreen;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BetaEnd
{
  internal class BetaEndingManager
  {
    private TitleImage BGO;
    private DiscordButton discordbutton;
    private DiscordButton steambutton;
    private SimpleTextHandler text;
    private SimpleTextHandler Score;
    private BigBrownPanel panel;
    private CustomerFrame customerframe;
    private Vector2 Loc = new Vector2(512f, 250f);
    private float Delay;
    private ZGenericUIDrawObject pictureoffolks;
    private float basescale;
    private UIScaleHelper scalehelper;
    private Vector2 pad;
    private TextButton textbuttton;
    private LerpHandler_Float lerper;

    public BetaEndingManager(Player player)
    {
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.Loc = new Vector2(512f, 330f);
      this.discordbutton = new DiscordButton();
      this.steambutton = new DiscordButton(false);
      MusicManager.playsong(SongTitle.TrailerTheme, true);
      this.steambutton.vLocation = this.scalehelper.ScaleVector2(new Vector2(250f, 50f));
      this.discordbutton.vLocation = this.scalehelper.ScaleVector2(new Vector2(-250f, 50f));
      this.Score = new SimpleTextHandler("Beta Summary:~~Total Income: " + (object) Player.financialrecords.GetLifetimeRevenue() + "~~Total Animals: " + (object) Player.financialrecords.GetTotalAnimalsInZoo() + "~~" ?? "", 800f, true, Z_GameFlags.GetBaseScaleForUI(), true);
      this.pictureoffolks = new ZGenericUIDrawObject(new Rectangle(0, 256, 1024, 769), 0.1f * this.basescale, AssetContainer.LogoSheet);
      this.BGO = new TitleImage();
      this.text = new SimpleTextHandler("Hi there, this is a message from Yvonne, Christine, Jin, Cindy and James. We are the developers at Springloaded, and are working probably at this very moment, on some new thing to make Let's Build A Zoo one step closer to launch.~~You just played through the very first version of the game to make it outside of our secret lair, and while we have lots to add, and so many things to improve on / fix, we hope you liked what you played.~~We are really interested in hearing your thoughts and ideas that will help us make the game better! If you want to hang out and chat about the game with us and other people, then join the game's official discord! If you are feeling extra generous you could fill out the feedback form(which should take around 3 minutes). Finally, if you haven't already, please wishlist the game on Steam, so you will be kept up to date on it's release!", this.scalehelper.ScaleX(800f), true, 1f * this.basescale);
      this.text.AutoCompleteParagraph();
      this.Score.AutoCompleteParagraph();
      this.panel = new BigBrownPanel(new Vector2(0.0f, 0.0f), true, "Thanks for playing!", this.basescale);
      float num = this.scalehelper.ScaleY(150f);
      Vector2 vector2_1 = new Vector2() + 2f * this.pad;
      vector2_1.Y += this.Score.GetSize().Y;
      vector2_1.Y += this.pad.Y;
      Vector2 vector2_2 = vector2_1 + this.text.GetSize();
      vector2_2.Y += num;
      this.customerframe = new CustomerFrame(vector2_2, true, Z_GameFlags.GetBaseScaleForUI());
      Vector2 vector2_3 = new Vector2();
      vector2_3.Y = -0.5f * vector2_2.Y;
      vector2_3.Y += 3f * this.pad.Y;
      this.Score.Location = vector2_3;
      vector2_3.Y += this.Score.GetSize().Y + this.pad.Y;
      this.text.Location = vector2_3;
      vector2_3.Y += this.text.GetSize().Y + this.pad.Y;
      this.pictureoffolks.location = vector2_3;
      this.panel.BlockCloseButton = true;
      this.panel.Finalize(vector2_2);
      this.Delay = 4f;
      this.textbuttton = new TextButton("Exit Game", 100f);
      this.textbuttton.vLocation = new Vector2(512f, 650f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);
    }

    public void UpdateBetaEndingManager(float DeltaTime, Player player)
    {
      Vector2 referenceScreenRes = Sengine.HalfReferenceScreenRes;
      this.Delay -= DeltaTime;
      if ((double) this.Delay <= 0.0)
      {
        this.lerper.UpdateLerpHandler(DeltaTime);
        if ((double) this.lerper.Value == 0.0 && this.textbuttton.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          GameVariables.QuitNextFrame = true;
      }
      if (this.discordbutton.UpdateDiscordButton(DeltaTime, player, referenceScreenRes))
        AppStoreLinker.LaunchBrowser("https://discord.com/invite/letsbuildazoo");
      if (!this.steambutton.UpdateDiscordButton(DeltaTime, player, referenceScreenRes))
        return;
      AppStoreLinker.LaunchBrowser("https://store.steampowered.com/app/1547890/Lets_Build_a_Zoo/");
    }

    public void DrawBetaEndingManager()
    {
      Vector2 referenceScreenRes = Sengine.HalfReferenceScreenRes;
      this.BGO.Draw(AssetContainer.spritebacth, AssetContainer.TitleScreen);
      this.panel.DrawBigBrownPanel(this.Loc);
      this.customerframe.DrawCustomerFrame(this.Loc, AssetContainer.pointspritebatchTop05);
      this.Score.DrawSimpleTextHandler(this.Loc, 1f, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(this.Loc, 1f, AssetContainer.pointspritebatchTop05);
      this.steambutton.DrawDiscordButton(referenceScreenRes);
      this.discordbutton.DrawDiscordButton(referenceScreenRes);
      this.pictureoffolks.DrawZGenericUIDrawObject(AssetContainer.pointspritebatchTop05, referenceScreenRes);
      if ((double) this.Delay > 0.0)
        return;
      this.textbuttton.DrawTextButton(new Vector2(0.0f, this.lerper.Value * 500f), 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
