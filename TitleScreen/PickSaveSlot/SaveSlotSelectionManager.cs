// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSlotSelectionManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.TitleScreen.PickSaveSlot.SaveSumary;
using TinyZoo.Z_Save.Header;

namespace TinyZoo.TitleScreen.PickSaveSlot
{
  internal class SaveSlotSelectionManager
  {
    private SaveSlotPanel saveslotpanel;
    private SaveSummaryManager savesummarymanager;
    private float BaseScale;
    private BackButton close;
    private bool WasBlocked;

    public SaveSlotSelectionManager(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.close = new BackButton(BaseScale: this.BaseScale);
      this.saveslotpanel = new SaveSlotPanel(this.BaseScale);
      this.savesummarymanager = new SaveSummaryManager(this.BaseScale, (HeaderInfo) null, this.saveslotpanel.tscreenframe.VScale);
    }

    public bool UpdateSaveSlotSelectionManager(Player player, float DeltaTime)
    {
      if (this.savesummarymanager.SkipDrawAndUpdateOfMainPanel())
      {
        this.WasBlocked = true;
      }
      else
      {
        if (this.WasBlocked)
        {
          this.WasBlocked = false;
          this.close = new BackButton(BaseScale: this.BaseScale);
        }
        this.saveslotpanel.UpdateSaveSlotPanel(player, DeltaTime, Vector2.Zero);
        if (this.close.UpdateBackButton(player, DeltaTime))
          return true;
      }
      this.savesummarymanager.UpdateSaveSummaryManager(player, DeltaTime);
      return false;
    }

    public void DrawSaveSlotSelectionManager()
    {
      if (!this.savesummarymanager.SkipDrawAndUpdateOfMainPanel())
      {
        this.saveslotpanel.DrawSaveSlotPanel(Vector2.Zero);
        this.close.DrawBackButton(Vector2.Zero);
      }
      this.savesummarymanager.DrawSaveSummaryManager(Vector2.Zero);
    }
  }
}
