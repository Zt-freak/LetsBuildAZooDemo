// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PickSex.CharacterSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tutorials;
using TinyZoo.Z_CharacterSelect;
using TinyZoo.Z_CharacterSelect.SelectedPreview;

namespace TinyZoo.Z_PickSex
{
  internal class CharacterSelectManager
  {
    private SmartCharacterBox character;
    private PeopleSelector people;
    private float Delay;
    private SelectionPreviewManager selectionpreviewmanager;
    private float BaseScale;

    public CharacterSelectManager()
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      Game1.ClsCLR.SetAllColours(new Vector3((float) byte.MaxValue, 250f, 228f) / (float) byte.MaxValue);
      this.character = !Z_DebugFlags.IsBetaVersion ? new SmartCharacterBox(SEngine.Localization.Localization.GetText(753), AnimalType.Administrator) : new SmartCharacterBox("Hi! I am off on holiday for two weeks. While I am gone, who should run my new zoo?", AnimalType.SpecialEvent_PipTheZooKeeper);
      this.people = new PeopleSelector();
      Z_GameFlags.TempBlockVirtualMouse = true;
    }

    public void UpdateCharacterSelectManager(Player player, float DeltaTime)
    {
      Z_GameFlags.BlockPointer = true;
      this.character.UpdateSmartCharacterBox(DeltaTime, player, true);
      if (this.people.UpdatePeopleSelector(DeltaTime, player))
      {
        this.Delay = 3f;
        if (Z_DebugFlags.IsBetaVersion)
          this.character.AddNewText(new textBoxPair(SEngine.Localization.Localization.GetText(754), AnimalType.SpecialEvent_PipTheZooKeeper));
        else
          this.character.AddNewText(new textBoxPair(SEngine.Localization.Localization.GetText(754), AnimalType.Administrator));
        this.character.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
        this.selectionpreviewmanager = new SelectionPreviewManager(this.BaseScale, this.people.custmomerframe.VSCale.X);
      }
      if ((double) this.Delay <= 0.0)
        return;
      this.selectionpreviewmanager.UpdateSelectionPreviewManager(DeltaTime, player);
    }

    public void DrawCharacterSelectManager()
    {
      this.character.DrawSmartCharacterBox();
      this.people.DrawPeopleSelector();
      if (this.selectionpreviewmanager == null)
        return;
      this.selectionpreviewmanager.DrawSelectionPreviewManager();
    }
  }
}
