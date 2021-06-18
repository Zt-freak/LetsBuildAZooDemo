// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.SeedPicker.SeedPickManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Farms.CropSum.SeedPicker
{
  internal class SeedPickManager
  {
    private BigBrownPanel brownpanel;
    private SeedPackCollection seedpackcollection;
    private Vector2 Location;

    public SeedPickManager(float BaseScale, Player player)
    {
      this.seedpackcollection = new SeedPackCollection(BaseScale, player);
      this.brownpanel = new BigBrownPanel(this.seedpackcollection.GetSize(), true, "Agriculture", BaseScale);
      this.brownpanel.Finalize(this.seedpackcollection.GetSize());
      this.Location = new Vector2(512f, 300f);
    }

    public Vector2 GetSize() => this.seedpackcollection.GetSize();

    public bool CheckMouseOver(Player player) => this.brownpanel.CheckMouseOver(player, this.Location);

    public bool UpdateSeedPickManager(Player player, float DeltaTime)
    {
      bool ForceClosePanel;
      this.seedpackcollection.UpdateSeedPackCollection(player, DeltaTime, this.Location, out ForceClosePanel);
      if (ForceClosePanel)
        return true;
      this.brownpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      return this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, this.Location);
    }

    public void DrawSeedPickManager()
    {
      this.brownpanel.DrawBigBrownPanel(this.Location);
      this.seedpackcollection.DrawSeedPackCollection(this.Location);
    }
  }
}
