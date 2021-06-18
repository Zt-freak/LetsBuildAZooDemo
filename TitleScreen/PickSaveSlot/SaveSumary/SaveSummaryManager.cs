// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.SaveSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.Existing;
using TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New;
using TinyZoo.Z_Save.Header;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary
{
  internal class SaveSummaryManager
  {
    private TScreenFrame tscreenframe;
    public Vector2 Location;
    private MiniHeading minheading;
    private NewGameOptions newgameoptions;
    private ExistingGameOptions existinggameobtions;

    public SaveSummaryManager(float BaseScale, HeaderInfo headerinf, Vector2 Size)
    {
      this.tscreenframe = new TScreenFrame(BaseScale);
      this.tscreenframe.VScale = new Vector2(220f * BaseScale, Size.Y);
      this.Location = new Vector2(880f, 300f);
      this.Location.Y -= 20f * BaseScale;
      string text = "Start New Game";
      if (headerinf != null)
      {
        text = "Existing Save";
        this.existinggameobtions = new ExistingGameOptions(BaseScale, headerinf);
      }
      else
        this.newgameoptions = new NewGameOptions(BaseScale);
      this.minheading = new MiniHeading(this.tscreenframe.VScale, text, 1f, BaseScale);
      this.minheading.SetAllColours(0.0f, 0.0f, 0.0f);
    }

    public bool SkipDrawAndUpdateOfMainPanel() => this.newgameoptions != null && this.newgameoptions.gamecustomizationscreen != null && this.newgameoptions.gamecustomizationscreen.Active;

    public void UpdateSaveSummaryManager(Player player, float DeltaTime)
    {
      if (this.newgameoptions == null)
        return;
      this.newgameoptions.UpdateNewGameOptions(player, DeltaTime, this.Location, this.minheading);
    }

    public void DrawSaveSummaryManager(Vector2 Offset)
    {
      Offset += this.Location;
      if (this.SkipDrawAndUpdateOfMainPanel())
      {
        this.newgameoptions.DrawNewGameOptions(this.Location);
      }
      else
      {
        this.tscreenframe.DrawTScreenFrame(Offset);
        this.minheading.DrawMiniHeading(Offset + this.tscreenframe.tframe.vLocation);
        if (this.newgameoptions == null)
          return;
        this.newgameoptions.DrawNewGameOptions(this.Location);
      }
    }
  }
}
