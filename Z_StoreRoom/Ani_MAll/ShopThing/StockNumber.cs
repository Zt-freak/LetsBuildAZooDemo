// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing.StockNumber
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing
{
  internal class StockNumber
  {
    private GameObject gameobject;
    private GameObject Over;
    public int Value;
    public Vector2 Location;
    private float BaseScale;

    public StockNumber(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.gameobject = new GameObject();
      this.gameobject.DrawRect = new Rectangle(2, 343, 64, 64);
      this.gameobject.SetDrawOriginToCentre();
      this.gameobject.SetAllColours(0.8392157f, 0.3568628f, 0.2980392f);
      this.Over = new GameObject(this.gameobject);
      this.Over.SetAllColours(1f, 1f, 1f);
      this.Over.SetAlpha(0.0f);
      this.gameobject.scale = _BaseScale * 0.5f;
      this.Over.scale = _BaseScale * 0.5f;
    }

    public void UpdateStockNumber(float DeltaTime) => this.Over.UpdateColours(DeltaTime);

    public void Clear()
    {
      this.Over.SetAlpha(false, 0.1f, 1f, 0.0f);
      this.Value = 0;
    }

    public void AddValue(int ADDTHIS)
    {
      this.Over.SetAlpha(false, 0.1f, 1f, 0.0f);
      this.Value += ADDTHIS;
    }

    public void ForceSetValue(int _Value) => this.Value = _Value;

    public void DrawStockNumber(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      Offset += this.Location;
      this.gameobject.Draw(DrawWithThis, AssetContainer.UISheet, Offset);
      TextFunctions.DrawJustifiedText(string.Concat((object) this.Value), this.BaseScale, Offset + new Vector2(1f, 1f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, DrawWithThis);
      this.Over.Draw(DrawWithThis, AssetContainer.UISheet, Offset);
    }
  }
}
