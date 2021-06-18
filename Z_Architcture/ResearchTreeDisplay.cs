// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.ResearchTreeDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_Architcture.ResearchTree;

namespace TinyZoo.Z_Architcture
{
  internal class ResearchTreeDisplay
  {
    private ResearchTreeManager treemanager;
    private MustBuilArchitect buildarchiect;

    public ResearchTreeDisplay(Player player)
    {
      if (!player.prisonlayout.HasAnArchitectureBuilding())
        this.buildarchiect = new MustBuilArchitect();
      else
        this.treemanager = new ResearchTreeManager(player);
    }

    public void UpdateResearchTreeDisplay(Player player, float DeltaTime)
    {
      if (this.buildarchiect != null)
        this.buildarchiect.UpdateMustBuilArchitect(DeltaTime, player);
      else
        this.treemanager.UpdateResearchTreeManager(player, DeltaTime);
    }

    public void DrawResearchTreeDisplay()
    {
      if (this.buildarchiect != null)
        this.buildarchiect.DrawMustBuilArchitect();
      else
        this.treemanager.DrawResearchTreeManager();
    }
  }
}
