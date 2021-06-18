// Decompiled with JetBrains decompiler
// Type: TinyZoo.Server.AdminCommManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Spring.DataBinding.Data;

namespace TinyZoo.Server
{
  internal class AdminCommManager
  {
    private static AdminCommManager _Instance;
    public ErrorDataPacket NotConnectedPacket;
    private LogInManager logInManager;

    public static AdminCommManager Instance
    {
      get
      {
        if (AdminCommManager._Instance == null)
          AdminCommManager._Instance = new AdminCommManager();
        return AdminCommManager._Instance;
      }
    }

    private AdminCommManager()
    {
      this.NotConnectedPacket = new ErrorDataPacket();
      this.NotConnectedPacket.MsgHeadStacks.Add("NOT CONNECTED TO SERVER");
      this.NotConnectedPacket.MsgBodyStacks.Add("NOT CONNECTED TO SERVER");
    }

    public void Init(Player player, Servers serverToLogin) => this.logInManager = new LogInManager(player, serverToLogin);

    public void InitiateConnectionToServer() => this.logInManager.EstablishConnection();

    public void OnAppPaused()
    {
      if (this.logInManager == null)
        return;
      this.logInManager.OnAppPaused();
    }

    public void OnAppResumed()
    {
      if (this.logInManager == null)
        return;
      this.logInManager.OnAppResumed();
    }
  }
}
