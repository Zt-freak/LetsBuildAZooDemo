// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.AvatarUI.Selection.MicroIconManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_OverWorld.AvatarUI.Selection
{
  internal class MicroIconManager
  {
    private List<CursorIcon> microicons;
    public bool bActive;

    public MicroIconManager()
    {
      this.microicons = new List<CursorIcon>();
      this.microicons.Add(new CursorIcon());
    }

    public void SetIcon(CURSOR_ACTIONTYPE _actiontype, Player player, Vector2Int location)
    {
      this.bActive = false;
      if (_actiontype != CURSOR_ACTIONTYPE.Count && _actiontype == CURSOR_ACTIONTYPE.CollectTrash)
        this.bActive = true;
      this.microicons[0].SetIcon(_actiontype, player, location);
    }

    public bool GetHasAction() => this.bActive && this.GetPrimaryAction() == CURSOR_ACTIONTYPE.CollectTrash;

    public CURSOR_ACTIONTYPE GetPrimaryAction() => this.bActive ? this.microicons[0].actiontype : CURSOR_ACTIONTYPE.Count;

    public void UpdateMicroIconManager()
    {
    }

    public void DrawMicroIconManager(Vector2 LOCS, SpriteBatch spriteBatch)
    {
      if (!this.bActive)
        return;
      this.microicons[0].DrawCursorIcon(LOCS, spriteBatch);
    }
  }
}
