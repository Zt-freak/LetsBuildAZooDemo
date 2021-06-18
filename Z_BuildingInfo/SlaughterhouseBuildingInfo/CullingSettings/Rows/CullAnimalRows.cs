// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows.CullAnimalRows
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows
{
  internal class CullAnimalRows
  {
    public Vector2 location;
    private List<CullAnimalRow> rows;
    private Z_ScrollHelper scrollHelper;
    private Vector2 size;
    private Vector2 scroll_offsetToTopLeft;

    public CullAnimalRows(Player player, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.rows = new List<CullAnimalRow>();
      for (int index = 0; index < 56; ++index)
      {
        CullAnimalRow cullAnimalRow = new CullAnimalRow((AnimalType) index, player, BaseScale);
        cullAnimalRow.location.Y = this.size.Y;
        cullAnimalRow.location.Y += cullAnimalRow.GetSize().Y * 0.5f;
        this.size.Y += cullAnimalRow.GetSize().Y;
        if (index != 55)
          this.size.Y += defaultBuffer.Y * 0.5f;
        this.rows.Add(cullAnimalRow);
      }
      this.size.X += this.rows[0].GetSize().X;
    }

    public void AddScroll(float maxHeight)
    {
      this.scrollHelper = new Z_ScrollHelper(this.size, maxHeight);
      this.scroll_offsetToTopLeft.X = (float) (-(double) this.size.X * 0.5);
    }

    public Vector2 GetSizeOfOneRow() => this.rows[0].GetSize();

    public Vector2 GetSize() => this.size;

    public AnimalType UpdateCullAnimalRows(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      this.scrollHelper.UpdateZ_ScrollHelper(player, offset + this.scroll_offsetToTopLeft);
      offset.Y += this.scrollHelper.YscrollOffset;
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (this.rows[index].UpdateCullAnimalRow(player, DeltaTime, offset))
          return this.rows[index].refAnimalType;
      }
      return AnimalType.None;
    }

    public void DrawCullAnimalRows(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.scrollHelper.YscrollOffset;
      for (int index = 0; index < this.rows.Count; ++index)
      {
        Vector2 size = this.rows[index].GetSize();
        float TopLocation = this.rows[index].location.Y - size.Y * 0.5f;
        float BottomLocation = TopLocation + size.Y;
        if (this.scrollHelper.CheckIfShouldDrawThis(TopLocation, BottomLocation))
          this.rows[index].DrawCullAnimalRow(offset, spriteBatch);
      }
    }
  }
}
