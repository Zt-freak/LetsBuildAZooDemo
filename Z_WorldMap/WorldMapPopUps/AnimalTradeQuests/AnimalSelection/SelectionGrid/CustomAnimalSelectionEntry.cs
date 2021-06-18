// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.SelectionGrid.CustomAnimalSelectionEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.SelectionGrid
{
  internal class CustomAnimalSelectionEntry
  {
    public PrisonerInfo refPrisonerInfo;
    public bool Darken_NotAvailable;
    public string MouseOverText;

    public CustomAnimalSelectionEntry(
      PrisonerInfo prisonerInfo,
      bool _Darken_NotAvailable = false,
      string _MouseOverText = "")
    {
      this.refPrisonerInfo = prisonerInfo;
      this.Darken_NotAvailable = _Darken_NotAvailable;
      this.MouseOverText = _MouseOverText;
    }
  }
}
