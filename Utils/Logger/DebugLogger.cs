// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.Logger.DebugLogger
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Utils.DeveloperMenu;

namespace TinyZoo.Utils.Logger
{
  internal class DebugLogger
  {
    private List<SimpleTextHandler> TextHandlers;
    private SimpleTextHandler CustomerLogText;
    private BlackOut blackout;
    private GameObject LogMessage;
    private string LOGMessageText;
    private bool Active;
    private DevMenuManager devmenu;
    private Vector2[] SCROLLLOCs;
    private LogType CurrentLogtype = LogType.DevMenu;

    public DebugLogger()
    {
      this.Active = true;
      this.blackout = new BlackOut();
      this.blackout.vLocation.Y = 0.0f;
      TinyZoo.FlagSettings.LineHeightModifier = Sengine.DifferenceInXWidthWhenInPortrait_UpwardsMultiplierEquivilant;
      this.TextHandlers = new List<SimpleTextHandler>();
      for (int index = 0; index < 7; ++index)
      {
        SimpleTextHandler simpleTextHandler = new SimpleTextHandler("Begin Log:", false, 0.9f, RenderMath.GetPixelSizeBestMatch(1.5f), false, false);
        simpleTextHandler.SetLineLimit_BeforeScroll(35, true);
        this.TextHandlers.Add(simpleTextHandler);
      }
      this.SCROLLLOCs = new Vector2[this.TextHandlers.Count];
      this.devmenu = new DevMenuManager();
      this.LogMessage = new GameObject();
      this.LogMessage.scale = RenderMath.GetPixelSizeBestMatch(2.5f);
      this.LogMessage.vLocation = new Vector2(1024f, 10f);
    }

    public void AddLine(string AddThis, Vector3 Colr, LogType logtype = LogType.Animals)
    {
      this.TextHandlers[1].AddLine(AddThis, Colr);
      this.TextHandlers[(int) logtype].AddLine(AddThis, Colr);
    }

    public void ClearLogs()
    {
      this.TextHandlers = new List<SimpleTextHandler>();
      for (int index = 0; index < 7; ++index)
      {
        SimpleTextHandler simpleTextHandler = new SimpleTextHandler("Cleared Log:", false, 0.9f, RenderMath.GetPixelSizeBestMatch(1.5f), false, false)
        {
          paragraph = {
            linemaker = {
              ColoursByString = new List<Vector3>()
            }
          }
        };
        simpleTextHandler.paragraph.linemaker.ColoursByString.Add(new Vector3(1f, 0.0f, 0.0f));
        simpleTextHandler.SetLineLimit_BeforeScroll(35, true);
        this.TextHandlers.Add(simpleTextHandler);
      }
      this.SCROLLLOCs = new Vector2[this.TextHandlers.Count];
    }

    public void UpdateHintLogger(float DeltaTime, Player player)
    {
      if (this.CurrentLogtype == LogType.DevMenu)
        this.devmenu.UpdateDevMenuManager(player, DeltaTime);
      if ((double) player.inputmap.momentumwheel.MovementThisFrame != 0.0 && PC_KeyState.LShift_held)
      {
        this.SCROLLLOCs[(int) this.CurrentLogtype].Y += player.inputmap.momentumwheel.MovementThisFrame * 40f * DeltaTime;
        if ((double) this.SCROLLLOCs[(int) this.CurrentLogtype].Y < 0.0)
        {
          this.SCROLLLOCs[(int) this.CurrentLogtype].Y = 0.0f;
          this.TextHandlers[(int) this.CurrentLogtype].paragraph.linemaker.microScroller.DisableAlphaTrim(false);
        }
        else
          this.TextHandlers[(int) this.CurrentLogtype].paragraph.linemaker.microScroller.DisableAlphaTrim(true);
        double subScrollerOffset = (double) this.TextHandlers[(int) this.CurrentLogtype].GetSubSCrollerOffset();
        if ((double) this.SCROLLLOCs[(int) this.CurrentLogtype].Y > -(double) this.TextHandlers[(int) this.CurrentLogtype].GetSubSCrollerOffset() + 60.0)
          this.SCROLLLOCs[(int) this.CurrentLogtype].Y = (float) (-(double) this.TextHandlers[(int) this.CurrentLogtype].GetSubSCrollerOffset() + 60.0);
        player.inputmap.momentumwheel.MovementThisFrame = 0.0f;
      }
      if (PC_KeyState.Tilde_PressedThisFrame)
        this.Active = !this.Active;
      if (PC_KeyState.Tab_PressedThisFrame)
      {
        ++this.CurrentLogtype;
        if (this.CurrentLogtype == LogType.Count)
          this.CurrentLogtype = LogType.Simple;
        this.LogMessage.SetAlpha(false, 1f, 1f, 0.0f);
        this.LogMessage.SetColourDelay(1f);
        switch (this.CurrentLogtype)
        {
          case LogType.Simple:
            this.LOGMessageText = "LOGGING: SIMPLE";
            this.LogMessage.SetAllColours(ColourData.LogGreen);
            break;
          case LogType.Full:
            this.LOGMessageText = "LOGGING: ALL";
            this.LogMessage.SetAllColours(Vector3.One);
            break;
          case LogType.Animals:
            this.LOGMessageText = "LOGGING: ANIMALS";
            this.LogMessage.SetAllColours(ColourData.LogGreen);
            break;
          case LogType.Customers:
            this.LOGMessageText = "LOGGING: VISITORS";
            this.LogMessage.SetAllColours(ColourData.LogOrange);
            break;
          case LogType.ParkStats:
            this.LOGMessageText = "LOGGING: PARK STATS";
            this.LogMessage.SetAllColours(ColourData.LogPurple);
            break;
          case LogType.Employees:
            this.LOGMessageText = "LOGGING: EMPLOYEES";
            this.LogMessage.SetAllColours(ColourData.LogBlue);
            break;
        }
        this.SCROLLLOCs[(int) this.CurrentLogtype].Y = 0.0f;
        this.TextHandlers[(int) this.CurrentLogtype].paragraph.linemaker.microScroller.DisableAlphaTrim(false);
      }
      this.LogMessage.UpdateColours(DeltaTime);
      if (PC_KeyState.FwdSlash_PressedThisFrame)
        this.ClearLogs();
      for (int index = 0; index < this.TextHandlers.Count; ++index)
        this.TextHandlers[index].UpdateSimpleTextHandler(DeltaTime);
    }

    public void DrawHintLogger()
    {
      if (Z_DebugFlags.HasOnScreenLog && this.Active)
      {
        this.blackout.fAlpha = 0.5f;
        this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch07Final);
        if (this.CurrentLogtype == LogType.DevMenu)
        {
          this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch07Final);
          this.devmenu.DrawDevMenuManager();
          return;
        }
        if ((double) this.LogMessage.fAlpha > 0.0)
          TextFunctions.DrawTextWithDropShadow(this.LOGMessageText, this.LogMessage.scale, this.LogMessage.vLocation, this.LogMessage.GetColour(), this.LogMessage.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatch07Final, false, true);
        this.TextHandlers[(int) this.CurrentLogtype].DrawSimpleTextHandler(this.blackout.vLocation + this.SCROLLLOCs[(int) this.CurrentLogtype], 1f, AssetContainer.pointspritebatch07Final);
      }
      TextFunctions.DrawTextWithDropShadow("Build: " + TinyZoo.Game1.BUILDNUMBER, 2f, new Vector2(1020f, 740f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch07Final, false, true);
    }
  }
}
