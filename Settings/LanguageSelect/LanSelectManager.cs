// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.LanguageSelect.LanSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Spring.UI.LanguageSelect;
using TinyZoo.Utils;

namespace TinyZoo.Settings.LanguageSelect
{
  internal class LanSelectManager
  {
    private LanguageSelectManager languageselect;

    public LanSelectManager(Player player) => this.languageselect = new LanguageSelectManager(player.Stats.GetLanguage(), LanguageInformation.GetSupportedLanguages());

    public bool UpdateLanguageSelectManager(Player player, float DeltaTime)
    {
      if (!this.languageselect.UpdateLanguageSelection(player.player, DeltaTime, AssetContainer.springFont, Vector2.Zero, out bool _, ref player.inputmap.ReleasedThisFrame[7], player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], player.inputmap.PressedThisFrame[0], GameFlags.IsUsingController))
        return false;
      if (this.languageselect.LanguageSwitched)
      {
        player.Stats.SetLanguage(this.languageselect.CurrentLanguage, player);
        SEngine.Localization.Localization.ReloadLocalization();
      }
      return true;
    }

    public void DrawLanguageSelectManager() => this.languageselect.DrawLanguageSelection(Vector2.Zero, AssetContainer.pointspritebatchTop05, AssetContainer.springFont);
  }
}
