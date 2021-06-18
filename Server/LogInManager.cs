// Decompiled with JetBrains decompiler
// Type: TinyZoo.Server.LogInManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Spring.Comms;
using Spring.Comms.Errors;
using Spring.Comms.Interface;
using Spring.DataBinding.Data;
using SpringSocial;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TinyZoo.Server
{
  internal class LogInManager : SpringTCPSocketInterface
  {
    private bool _Connecting;
    private int _ServerPort;
    private string _ServerIP;
    private string _GameID;
    private Player _Player;
    public string _GameAccessToken = "";
    private bool _IsAppInBackground;
    private Thread DisconnectThread;

    public LogInManager(Player Player, Servers ServerToLogin)
    {
      this._Player = Player;
      this.SetupServerIP(ServerToLogin);
      SpringCommManager.Singleton.AddClientSpringComTCPSocketInterface((SpringTCPSocketInterface) this);
      this.EstablishConnection();
    }

    private void SetupServerIP(Servers server)
    {
      switch (server)
      {
        case Servers.LIVE:
          this._ServerIP = MainVariables.LIVE_SERVER_IP;
          break;
        case Servers.TEST:
          this._ServerIP = MainVariables.TEST_SERVER_IP;
          break;
        case Servers.LOCAL:
          this._ServerIP = MainVariables.LOCAL_SERVER_IP;
          break;
      }
      this._ServerPort = 11000;
    }

    public void EstablishConnection()
    {
      if (TinyZoo.DebugFlags.SkipServer || this._Connecting || SpringCommManager.Singleton.IsSessionConnected)
        return;
      this._Connecting = true;
      new Task((Action) (() =>
      {
        SpringCommManager.Singleton.Initialize();
        Thread.Sleep(5000);
        while (true)
        {
          do
            ;
          while (TinyZoo.Game1.gamestate == GAMESTATE.SplashScreenSetUp || TinyZoo.Game1.gamestate == GAMESTATE.SplashScreen);
          if (!SpringCommManager.Singleton.StartConnection(this._ServerIP, this._ServerPort))
            Thread.Sleep(1000);
          else
            break;
        }
        this._Connecting = false;
      })).Start();
    }

    private void LoginToGame()
    {
      if (TinyZoo.DebugFlags.SkipServer)
        return;
      new Task((Action) (() =>
      {
        SocialPair thisSocialPair;
        do
        {
          while (!SpringCommManager.Singleton.IsSessionConnected)
            Thread.Sleep(2000);
          bool HasThisSocial = false;
          thisSocialPair = this._Player.socialmanager.GetUser().GetThisSocialPair(MainVariables.ThisGame, out HasThisSocial);
        }
        while (thisSocialPair == null);
        this._GameAccessToken = thisSocialPair.GetAccessToken();
        if (this._GameAccessToken == null || this._GameAccessToken == "" || (this._GameAccessToken == "null" || this._GameAccessToken == "NULL"))
          this._GameAccessToken = "";
        this._GameID = thisSocialPair.UID;
        SpringCommManager.Singleton.LoginWithGameAccount(this._GameAccessToken, SocialTypes.PrependReturn(MainVariables.ThisGame, this._GameID), MainVariables.ThisGame);
      })).Start();
    }

    void SpringTCPSocketInterface.Connected() => SpringCommManager.Singleton.StartSession();

    void SpringTCPSocketInterface.Disconnected()
    {
      this._Connecting = false;
      this.EstablishConnection();
    }

    void SpringTCPSocketInterface.FailedConnection() => this.EstablishConnection();

    void SpringTCPSocketInterface.SessionConnected() => this.LoginToGame();

    void SpringTCPSocketInterface.SessionDisconnected() => SpringCommManager.Singleton.StartSession();

    void SpringTCPSocketInterface.LoginSucceded(LoginResponse inResponse)
    {
      if (!(this._GameAccessToken == "") && !(this._GameAccessToken == "null") && (!(this._GameAccessToken == "NULL") && this._GameAccessToken != null))
        return;
      this._GameAccessToken = inResponse.ATOK;
      bool HasThisSocial;
      SocialPair thisSocialPair = this._Player.socialmanager.GetUser().GetThisSocialPair(MainVariables.ThisGame, out HasThisSocial);
      if (!HasThisSocial)
        return;
      thisSocialPair.SetAccessTokenForTheFirstTime(this._GameAccessToken);
    }

    void SpringTCPSocketInterface.LoginFailed(ErrorDataPacket inEdp)
    {
      if (inEdp != null)
      {
        if (inEdp.MsgHeadStacks[0] == CommsError.NO_CONNECTION || inEdp.MsgHeadStacks[0] == CommsError.TIMEOUT)
          this.LoginToGame();
        else if (inEdp.MsgHeadStacks[0] == LoginError.FACEBOOK_NO_SUB_ACCOUNT)
        {
          this.LoginToGame();
        }
        else
        {
          int num = inEdp.MsgHeadStacks[0] == LoginError.GAME_PIX_ID_NOT_THERE ? 1 : 0;
        }
      }
      else
        this.LoginToGame();
    }

    public void OnAppPaused()
    {
      this._IsAppInBackground = true;
      this.DisconnectThread = new Thread((ThreadStart) (() =>
      {
        Thread.Sleep(60000);
        SpringCommManager.Singleton.ForceDisconnect();
      }));
      this.DisconnectThread.IsBackground = true;
      this.DisconnectThread.Start();
    }

    public void OnAppResumed()
    {
      this._IsAppInBackground = false;
      if (this.DisconnectThread == null || !this.DisconnectThread.IsAlive)
        return;
      this.DisconnectThread.Abort();
      this.DisconnectThread = (Thread) null;
    }
  }
}
