// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.SaveIconManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.FileInOut;
using TRC_Helper;

namespace TinyZoo.GenericUI
{
  internal class SaveIconManager
  {
    private bool HasRendered;
    private AutoSaveDisplay Autosaveicon;

    public void UpdateSaveIconManager(Player player, float DeltaTime)
    {
      if (Z_GameFlags.SaveNextFrame && this.HasRendered)
      {
        Z_GameFlags.SaveNextFrame = false;
        player.OldSaveThisPlayer(DelayUntilNextFrame: false, IsEndOfDay: true);
      }
      if (Z_GameFlags.SaveNextFrame && this.Autosaveicon == null)
      {
        this.HasRendered = false;
        this.Autosaveicon = new AutoSaveDisplay(false, AutoSaveIconLocation.TopRight, _DisplayText: true);
        this.Autosaveicon.MinimumTime = 2f;
      }
      if (Z_GameFlags.LoadNextFrame && this.Autosaveicon == null)
      {
        this.Autosaveicon = new AutoSaveDisplay(true, AutoSaveIconLocation.TopRight);
        this.Autosaveicon.MinimumTime = 2f;
        this.HasRendered = false;
      }
      if (Z_GameFlags.LoadNextFrame && this.HasRendered)
      {
        Z_GameFlags.LoadNextFrame = false;
        player.LoadThisPlayer();
      }
      if (this.Autosaveicon == null || !this.Autosaveicon.UpdateAutoSaveDisplay(DeltaTime))
        return;
      this.Autosaveicon = (AutoSaveDisplay) null;
      this.HasRendered = false;
      if (ThreadedSaveStatus.threadedWriteState == ThreadedWriteState.ReadError)
      {
        Logger.Print("[SaveIconManager] ReadError returned :(");
        player.OldSaveThisPlayer(IsEndOfDay: true);
      }
      else
      {
        if (ThreadedSaveStatus.threadedWriteState != ThreadedWriteState.ReadSuccess)
          return;
        Reader cloudreader = new Reader();
        cloudreader.FromString(ThreadedSaveStatus.LastLoadedString);
        player.LoadThisPlayer(cloudreader);
      }
    }

    public bool IsSaveEventBlocking() => this.Autosaveicon != null && this.Autosaveicon.IsSaveEventBlocking;

    public void DrawSaveIconManeger()
    {
      if (this.Autosaveicon == null)
        return;
      this.Autosaveicon.DrawAutoSaveDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, AssetContainer.springFont);
      this.HasRendered = true;
    }
  }
}
