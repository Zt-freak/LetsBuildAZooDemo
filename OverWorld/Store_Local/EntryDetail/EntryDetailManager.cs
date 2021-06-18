// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Store_Local.EntryDetail.EntryDetailManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.Store_Local.Entries;

namespace TinyZoo.OverWorld.Store_Local.EntryDetail
{
  internal class EntryDetailManager
  {
    private LerpHandler_Float lerper;
    private EntryDetailPanel entrypanel;
    private StoreEntryType Next_entrytype;
    private StoreEntryType SelectedEntrytype;

    public EntryDetailManager()
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 1f, 3f);
      this.Next_entrytype = StoreEntryType.Count;
      this.SelectedEntrytype = StoreEntryType.Count;
    }

    public void SelectedNewIcon(StoreEntryType entrytype, bool Instant, Player player)
    {
      this.Next_entrytype = entrytype;
      if ((double) this.lerper.TargetValue != 1.0)
        this.lerper.SetLerp(false, 0.0f, 1f, 3.5f, true);
      if (!Instant)
        return;
      this.lerper.SetLerp(false, 0.0f, 0.0f, 3.5f, true);
      this.CreateNewPanel(player);
    }

    private void CreateNewPanel(Player player)
    {
      this.entrypanel = new EntryDetailPanel(this.Next_entrytype, player);
      this.entrypanel.Location = new Vector2(660f, 530f);
      this.SelectedEntrytype = this.Next_entrytype;
      this.Next_entrytype = StoreEntryType.Count;
    }

    public void UpdateEntryDetailManager(float DeltaTime, Player player)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 1.0 && this.Next_entrytype != StoreEntryType.Count)
      {
        this.CreateNewPanel(player);
        this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      }
      if (this.entrypanel == null)
        return;
      this.entrypanel.UpdateEntryDetailPanel(DeltaTime, player, (double) this.lerper.Value == 0.0);
    }

    public void DrawEntryDetailManager(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      if ((double) this.lerper.Value >= 1.0)
        return;
      this.entrypanel.DrawEntryDetailPanel(new Vector2(this.lerper.Value * 912f, 0.0f) + Offset, DrawWithThis);
    }
  }
}
