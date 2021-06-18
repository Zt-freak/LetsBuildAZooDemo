// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.Existing.ExistingGameOptions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_Save.Header;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.Existing
{
  internal class ExistingGameOptions
  {
    private SimpleTextHandler simpletext;
    private LittleSummaryButton button;
    private TextButton PlayThis;
    public Vector2 Location;

    public ExistingGameOptions(float BaseScale, HeaderInfo headerinf)
    {
      this.simpletext = new SimpleTextHandler(SEngine.Localization.Localization.GetText(983) + ": " + (object) headerinf.DaysPlayed + "~" + SEngine.Localization.Localization.GetText(984) + ": " + (object) Math.Round((double) headerinf.PercentComplete, 1) + "%~" + SEngine.Localization.Localization.GetText(985) + ": " + headerinf.MoralityRating + "~" + SEngine.Localization.Localization.GetText(986) + ": $ " + (object) headerinf.CashHeld + "~" + SEngine.Localization.Localization.GetText(987) + ": x" + (object) headerinf.LandUnlocked + "~" + SEngine.Localization.Localization.GetText(988) + ": " + (object) headerinf.ResearchDiscovered, BaseScale * 100f, true, BaseScale, true, true);
      this.button = new LittleSummaryButton(LittleSummaryButtonType.Remove, _BaseScale: BaseScale);
      this.PlayThis = new TextButton(SEngine.Localization.Localization.GetText(60), 100f * BaseScale);
      this.PlayThis.SetButtonColour(BTNColour.Grey);
    }

    public void UpdateExistingGameOptions(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      this.PlayThis.UpdateTextButton(player, Offset, DeltaTime);
      this.button.UpdateLittleSummaryButton(DeltaTime, player, Offset);
    }

    public void DrawExistingGameOptions(Vector2 Offset)
    {
      Offset += this.Location;
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatch03);
      this.PlayThis.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatch03);
    }
  }
}
