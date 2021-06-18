// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.CRISPRMainPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedScreen;

namespace TinyZoo.Z_CRISPR
{
  internal class CRISPRMainPanel : AllChambers
  {
    public CRISPRMainPanel(
      CRISPRBuilding building,
      Player player,
      int TotalChambers,
      float BaseScale)
      : base(building, player, TotalChambers, BaseScale)
    {
    }

    public bool CheckMouseOver(Player player) => this.CheckMouseOver(player, Vector2.Zero);

    public int UpdateCRISPRMainPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool Cancel,
      out bool GoToManage)
    {
      return this.UpdateAllChambers(player, DeltaTime, offset, out Cancel, out GoToManage);
    }

    public void DrawCRISPRMainPanel(Vector2 offset, SpriteBatch spriteBatch) => this.DrawAllChamber(offset, spriteBatch);
  }
}
