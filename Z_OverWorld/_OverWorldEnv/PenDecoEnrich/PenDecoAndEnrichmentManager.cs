// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.PenDecoAndEnrichmentManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.DecoManagers;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich
{
  internal class PenDecoAndEnrichmentManager
  {
    private PrisonZone Ref_prisonzone;
    private PenDecoTile MainGateTile;
    private List<BaseDecoObject> Food;

    public PenDecoAndEnrichmentManager(PrisonZone prisonzone, Player player)
    {
      this.Ref_prisonzone = prisonzone;
      this.MainGateTile = new PenDecoTile(prisonzone.GetSpaceBehindGate(player));
      this.Food = new List<BaseDecoObject>();
      if (Z_DebugFlags.AutoFood == AnimalFoodType.Count)
        return;
      this.Food.Add((BaseDecoObject) new AnimalFoodMngr(new UsedFoodCollection()
      {
        usedfoodentries = {
          new UsedFoodEntry(Z_DebugFlags.AutoFood, 10f)
        }
      }));
      this.MainGateTile.AddDecoObject(this.Food[this.Food.Count - 1]);
    }

    public void Feed(UsedFoodCollection foodused)
    {
      this.Food.Add((BaseDecoObject) new AnimalFoodMngr(foodused));
      this.MainGateTile.AddDecoObject(this.Food[this.Food.Count - 1]);
    }

    public void UpdatePenDecoAndEnrichmentManager(float DeltaTime) => this.MainGateTile.UpdatePenDecoTile(DeltaTime);

    public void DrawPenDecoAndEnrichmentManager() => this.MainGateTile.DrawPenDecoTile();
  }
}
