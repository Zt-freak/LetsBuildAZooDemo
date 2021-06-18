// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.Rendering.RenderManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_TitleScreen.NewsFeed.DataManage;

namespace TinyZoo.Z_TitleScreen.NewsFeed.Rendering
{
  internal class RenderManager
  {
    private List<PageEntry> pageentries;
    public float FullYSize;
    private Surrounding surrounding;
    private LerpHandler_Float ALphaLerper;
    private int ThisPage;
    private bool Active;

    public RenderManager()
    {
      this.Active = true;
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.pageentries = new List<PageEntry>();
      for (int index = 0; index < S_Directory.newentries.Count; ++index)
      {
        this.pageentries.Add(new PageEntry(S_Directory.newentries[index], baseScaleForUi, 300f));
        this.FullYSize += this.pageentries[index].GetHeight();
      }
      if (this.pageentries.Count > 0)
        this.surrounding = new Surrounding(baseScaleForUi, this.pageentries[0].GetHeight());
      else
        this.Active = false;
      this.ALphaLerper = new LerpHandler_Float();
      this.ALphaLerper.SetLerp(false, 1f, 1f, 3f);
    }

    public void UpdateRenderManager(Player player, float DeltaTime)
    {
      if (!this.Active)
        return;
      bool CLOSED;
      if (this.surrounding.UpdateSurrounding(player, DeltaTime, Vector2.Zero, out CLOSED))
      {
        ++this.ThisPage;
        if (this.ThisPage > this.pageentries.Count - 1)
          this.ThisPage = 0;
        this.surrounding.SetNewHeight(this.pageentries[this.ThisPage].GetHeight());
        this.ALphaLerper.SetLerp(true, 0.0f, 1f, 1f, true);
      }
      this.ALphaLerper.UpdateLerpHandler(DeltaTime);
      if (!CLOSED)
        return;
      this.Active = false;
    }

    public void DrawRenderManager()
    {
      if (!this.Active)
        return;
      this.surrounding.DrawSurrounding(Vector2.Zero, this.ThisPage, this.pageentries.Count);
      this.pageentries[this.ThisPage].DrawPageEntry(this.surrounding.GetDrawLocation(), this.ALphaLerper.Value);
    }
  }
}
