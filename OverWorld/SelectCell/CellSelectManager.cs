// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.CellSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.SelectCell
{
  internal class CellSelectManager
  {
    private SmartCharacterBox charactertextbox;
    private CellInfoManager cellinfomanager;
    private BackButton Backbuuuton;
    private bool Exiting;

    public CellSelectManager(Player player)
    {
      FeatureFlags.BlockTimer = true;
      FeatureFlags.BlockCash = true;
      Z_GameFlags.TopBarIsBlockedForTutorial = true;
      this.cellinfomanager = new CellInfoManager(player);
      this.charactertextbox = new SmartCharacterBox(SEngine.Localization.Localization.GetText(962), AnimalType.Administrator);
      this.Backbuuuton = new BackButton();
    }

    public bool CheckMouseOver(Player player) => this.cellinfomanager.CheckMouseOver(player);

    public void UpdateCellSelectManager(Player player, float DeltaTime, ref bool ExitBackToGame)
    {
      this.cellinfomanager.UpdateCellInfoManager(player, DeltaTime, ref ExitBackToGame);
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, this.Exiting);
      this.Backbuuuton.UpdateBackButton(player, DeltaTime);
    }

    public void DrawCellSelectManager()
    {
      this.cellinfomanager.DrawCellInfoManager();
      this.charactertextbox.DrawSmartCharacterBox();
    }
  }
}
