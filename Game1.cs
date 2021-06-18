// Decompiled with JetBrains decompiler
// Type: TinyZoo.Game1
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using AnalyticsWrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Events;
using SEngine.FileInOut;
using SEngine.Input;
using SEngine.Main;
using SEngine.Rendering;
using SEngine.Utils;
using Spring.Comms;
using SpringIAP;
using System;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Server;
using TinyZoo.Utils;
using TinyZoo.Utils.FileDownloader;
using TinyZoo.Utils.Logger;
using TinyZoo.Z_Threading;
using TRC_Helper;
using TRC_Helper.MessageRenderer;

namespace TinyZoo
{
  internal class Game1 : Game
  {
    internal static int VersionNumber = 43;
    public GraphicsDeviceManager graphics;
    internal static ContentManager MusicMngr;
    internal static ContentManager News_ContentManager;
    internal static ResourceContentManager SFXMngr;
    private GameStateManager gamestatemanager;
    internal static SaveIconManager saveiconmanager;
    private Player[] players;
    private float DeltaTime;
    internal static CLSColour ClsCLR;
    internal static ScreenFade screenfade;
    internal static Random Rnd = new Random();
    internal static Rectangle WhitePixelRect = new Rectangle(1, 1, 1, 1);
    internal static GAMESTATE gamestate;
    internal static GAMESTATE Previousgamestate;
    internal static bool ForceSwitchToNextGameState;
    private static GAMESTATE nextgamestate;
    private bool TRCMainJustChanged;
    public float timeFromStart;
    internal static string BUILDNUMBER = "M7-0.13.1";
    internal static DebugLogger logger;

    public Game1()
    {
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.Content = (ContentManager) new ResourceContentManager((IServiceProvider) this.Services, Resource1.ResourceManager);
      this.Window.Title = "Let's Build A Zoo";
      this.graphics.HardwareModeSwitch = false;
      Config.loadConfig();
      ScreenRes.initializeScreenRes(this.graphics, this.GraphicsDevice);
      this.timeFromStart = 0.0f;
    }

    internal static GAMESTATE GetNextGameState() => Game1.nextgamestate;

    internal static void SetNextGameState(GAMESTATE _nextgamestate) => Game1.nextgamestate = _nextgamestate;

    protected override void Initialize()
    {
      Game1.MusicMngr = new ContentManager((IServiceProvider) this.Services, "Content");
      Game1.SFXMngr = new ResourceContentManager((IServiceProvider) this.Services, Resource1.ResourceManager);
      Game1.News_ContentManager = new ContentManager((IServiceProvider) this.Services, "Content");
      Initialization.InitializeSEngine(this.GraphicsDevice);
      Initialization.InitializeSengineRendering(this.graphics, this.Window);
      Initialization.GetScreenResolution(this.GraphicsDevice);
      AssetContainer.LoadAssets(this.Content);
      this.gamestatemanager = new GameStateManager();
      if (Z_GameFlags.UsingCustomMouse)
        this.IsMouseVisible = false;
      else
        this.IsMouseVisible = true;
      GameVariables.InitializeGameVariables(ref this.players);
      base.Initialize();
    }

    internal static void InitializeDuringSplash(Player[] players)
    {
      if (ThreadedSaveStatus.GetIsThreadedSave())
        Game1.saveiconmanager = new SaveIconManager();
      Spring.Data.Core.Entry.InitializeDataAndWorkers();
      Spring.Data.Social.Entry.InitializeDataAndWorkers();
      AdminCommManager.Instance.Init(players[0], Servers.LIVE);
    }

    protected override void LoadContent()
    {
      AssetContainer.PointBlendBatch02 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.pointspritebatch01 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.pointspritebatch03 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.PointBlendBatch04 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.pointspritebatchTop05 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.spritebatch06 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.pointspritebatch07Final = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.pointspritebatch0 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.PointBlendBatch01 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.spritebacth = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.FloorBatch = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.WF_FloorBatch = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.WF_FloorBatch2 = new SpriteBatch(this.GraphicsDevice);
      AssetContainer.WF_FloorBatch3 = new SpriteBatch(this.GraphicsDevice);
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      SpringCommManager.Singleton.PumpAndDump();
      this.DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
      this.timeFromStart += this.DeltaTime;
      if (Z_GameFlags.ForceResolutionNextFrame)
      {
        if (!Z_DebugFlags.WillBlockLoadScreenRes)
        {
          PlayerStats.UXMult = -1f;
          PCScreenResolutionManager.SetNewScreenresolution(this.graphics, this.players[0].Stats.gfxresolution, this.GraphicsDevice);
        }
        Z_GameFlags.ForceResolutionNextFrame = false;
      }
      GameFlags.RefDeltaTime = this.DeltaTime;
      if (GameVariables.QuitNextFrame)
        this.Exit();
      Updates.UpdateEveryFrame(this.DeltaTime, this.GraphicsDevice);
      this.UpdateEveryFrameVGame(this.players[0], this.graphics);
      if (TRC_Main.UpdateTRCMain(this.DeltaTime, AssetContainer.springFont, ref this.TRCMainJustChanged, this.players[0].player))
        this.DeltaTime = 0.0f;
      if (Z_DebugFlags.HasOnScreenLog && Game1.logger != null)
      {
        if (PC_KeyState.LeftControl_PressedThisFrame)
          Z_DebugFlags.SimulationIsVerbose = !Z_DebugFlags.SimulationIsVerbose;
        if (Z_DebugFlags.SimulationIsVerbose)
          Game1.logger.UpdateHintLogger(this.DeltaTime, this.players[0]);
      }
      if (this.TRCMainJustChanged)
      {
        this.TRCMainJustChanged = false;
        if (TRC_Main.GetExitResult() == ExitResult.Quit)
        {
          int num = 0;
          while (num < this.players.Length)
            ++num;
          TRC_Main.BusyCheck.DeactvateBusyControllerCheck();
        }
      }
      Game1.ClsCLR.UpdateCLSColour(this.DeltaTime);
      this.gamestatemanager.UpdateGameStateManager(this.DeltaTime, ref this.players, this.Content, SpringIAPManager.Instance, this.graphics, this.GraphicsDevice);
      if (Z_GameFlags.UsingCustomMouse)
        this.IsMouseVisible = false;
      else
        this.IsMouseVisible = GameFlags.IsUsingMouse;
      Initialization.GetScreenResolution(this.GraphicsDevice);
      RenderMath.SetUpScreenRatio();
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      this.GraphicsDevice.Clear(Game1.ClsCLR.GetColour());
      this.DrawSplitScreen();
      base.Draw(gameTime);
      this.players[0].socialmanager.screenshotmanager.UpdatescreenshotstatusAfterBaseDraw(this.GraphicsDevice);
    }

    protected override void OnActivated(object sender, EventArgs args)
    {
      base.OnActivated(sender, args);
      MusicManager.ResumedApplication();
      SoundEffectsManager.OnGameActivated();
      SystemResume.OnSystemResumeFromSuspended();
      AdminCommManager.Instance.OnAppResumed();
    }

    protected override void OnDeactivated(object sender, EventArgs args)
    {
      base.OnDeactivated(sender, args);
      SoundEffectsManager.OnGameDeactivated();
      SystemResume.OnSystemEnterBackground();
      AdminCommManager.Instance.OnAppPaused();
    }

    public void UpdateEveryFrameVGame(Player player, GraphicsDeviceManager graphics)
    {
      if (player.inputmap.PressedThisFrame != null && player.inputmap.PressedThisFrame[24])
      {
        Z_DebugFlags.UseRenderThreading = !Z_DebugFlags.UseRenderThreading;
        Console.WriteLine("Threading is: " + Z_DebugFlags.UseRenderThreading.ToString());
      }
      FileGetManager.UpdateFileGetManager();
      if (Game1.saveiconmanager != null)
        Game1.saveiconmanager.UpdateSaveIconManager(player, this.DeltaTime);
      for (int index = 0; index < this.players.Length; ++index)
        this.players[index].UpdateGamePlayer(this.DeltaTime);
      if (this.players[0].Stats.NeedToDoPixFix)
        FixPixID_ToNewServer.UpdateFixPixID_ToNewServer(this.players[0]);
      if (PC_KeyState.LeftControl_Held && PC_KeyState.S_PressedThisFrame)
        this.players[0].OldSaveThisPlayer();
      MusicManager.UpdateMusic(this.DeltaTime, ref Game1.MusicMngr);
      SoundEffectsManager.CleanSoundEffects(this.DeltaTime);
      if (!SpringCommManager.Singleton.HasLogs())
        return;
      foreach (string log in SpringCommManager.Singleton.GetLogs(true))
        AnalyticsEventLog.TrackAnaylticsEvent("SpringComms", "logger", log.Substring(0, 100));
    }

    public void DrawSplitScreen()
    {
      int num = 0;
      foreach (Viewport viewport in SplitScreenControler.viewports)
      {
        if (FlagSettings.PlatformIsDualScreen)
        {
          SEngine.Game1.ScreenResolution = num != 0 ? new Vector2(640.5f, 360f) : new Vector2(960f, 540f);
          RenderMath.SetUpScreenRatio();
        }
        AssetContainer.pointspritebatch0.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        if (Z_GameFlags.UseMipMap())
        {
          AssetContainer.FloorBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
          AssetContainer.WF_FloorBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
          AssetContainer.WF_FloorBatch2.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
          AssetContainer.WF_FloorBatch3.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        }
        else
        {
          AssetContainer.FloorBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
          AssetContainer.WF_FloorBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
          AssetContainer.WF_FloorBatch2.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
          AssetContainer.WF_FloorBatch3.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        }
        AssetContainer.PointBlendBatch01.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.spritebacth.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.pointspritebatch01.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.PointBlendBatch02.Begin(blendState: BlendState.Additive, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.pointspritebatch03.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.PointBlendBatch04.Begin(blendState: BlendState.Additive, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.pointspritebatchTop05.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.spritebatch06.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        AssetContainer.pointspritebatch07Final.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.Default, rasterizerState: RasterizerState.CullNone);
        this.gamestatemanager.DrawGameStateManager(this.players[0], this.DeltaTime);
        TRC_Main.DrawTRC_Main(AssetContainer.springFont, AssetContainer.SpriteSheet, AssetContainer.pointspritebatchTop05);
        if (Game1.saveiconmanager != null)
          Game1.saveiconmanager.DrawSaveIconManeger();
        if (Z_DebugFlags.HasOnScreenLog && Z_DebugFlags.SimulationIsVerbose && Game1.logger != null)
          Game1.logger.DrawHintLogger();
        ++num;
        if (Z_DebugFlags.UseRenderThreading)
        {
          while (!ThreadFlags.THREAD_UnderGroundDraw || !ThreadFlags.THREAD_FloorDraw || (!ThreadFlags.THREAD_FloorDraw3 || !ThreadFlags.THREAD_FloorDraw2))
            ;
        }
        AssetContainer.pointspritebatch0.End();
        AssetContainer.FloorBatch.End();
        AssetContainer.WF_FloorBatch.End();
        AssetContainer.WF_FloorBatch2.End();
        AssetContainer.WF_FloorBatch3.End();
        AssetContainer.spritebacth.End();
        AssetContainer.PointBlendBatch01.End();
        AssetContainer.pointspritebatch01.End();
        AssetContainer.PointBlendBatch02.End();
        AssetContainer.pointspritebatch03.End();
        AssetContainer.PointBlendBatch04.End();
        AssetContainer.spritebatch06.End();
        AssetContainer.pointspritebatchTop05.End();
        AssetContainer.pointspritebatch07Final.End();
      }
    }
  }
}
