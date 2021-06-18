// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_NewGameSettings.NewGameSettingsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.Z_NewGameSettings
{
  internal class NewGameSettingsManager
  {
    private StoreBGManager storeBG;
    private BackButton close;
    private NewSettingsButtons newsettingsbuttons;
    private InformationPanel informationpanel;
    private int LastSelected;
    private PlayPanel playpanel;
    private ScreenHeading screenhead;

    public NewGameSettingsManager()
    {
      this.screenhead = new ScreenHeading("SET UP NEW GAME", 90f);
      this.LastSelected = -1;
      this.storeBG = new StoreBGManager();
      this.newsettingsbuttons = new NewSettingsButtons();
      this.informationpanel = new InformationPanel();
      this.playpanel = new PlayPanel();
    }

    public void UpdateNewGameSettingsManager(Player player, float DeltaTime)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      bool flag = this.newsettingsbuttons.UpdateNewSettingsButtons(player, DeltaTime);
      if (this.playpanel.UpdatePlayPanel(player, DeltaTime))
      {
        Game1.screenfade.BeginFade(true);
        Game1.SetNextGameState(GAMESTATE.CharacterSelectSetUp);
      }
      if (!(this.LastSelected != this.newsettingsbuttons.CurrentSelection | flag))
        return;
      this.LastSelected = this.newsettingsbuttons.CurrentSelection;
      this.informationpanel.SetText(this.newsettingsbuttons.GetSelectedString());
      this.playpanel.SetMode(this.newsettingsbuttons.AllAreDefault());
    }

    public void DrawNewGameSettingsManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      this.screenhead.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.newsettingsbuttons.DrawNewSettingsButtons();
      this.informationpanel.DrawInformationPanel();
      this.playpanel.DrawPlayPanel();
    }
  }
}
