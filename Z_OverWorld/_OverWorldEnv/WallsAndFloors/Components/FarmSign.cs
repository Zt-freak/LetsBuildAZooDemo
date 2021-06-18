// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.FarmSign
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_Farms;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class FarmSign : RenderComponent
  {
    private bool Active;
    private GameObject Sign;
    private GameObject SignHead;
    public int PrisonUID;
    public Vector2Int Location;
    private CROPTYPE croptype;

    public FarmSign(TileRenderer parent, int _PrisonUID, CROPTYPE _croptype)
      : base(parent, RenderComponentType.FarmSign)
    {
      this.Location = new Vector2Int(parent.TileLocation);
      this.croptype = _croptype;
      this.PrisonUID = _PrisonUID;
      this.Sign = new GameObject();
      this.Sign.DrawRect = new Rectangle(1834, 290, 16, 20);
      this.Sign.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.Sign.DrawOrigin.Y -= 8f;
    }

    public void SetUpOrderStatus(Player player, int PrisonUID)
    {
      this.Active = (uint) this.croptype > 0U;
      this.SignHead = new GameObject();
      this.SignHead.DrawRect = CropData.GetCropTypeToSeedPacketRectangle(this.croptype);
      this.SignHead.DrawRect.X += 4;
      this.SignHead.DrawRect.Width -= 8;
      this.SignHead.DrawRect.Y += 8;
      this.SignHead.DrawRect.Height -= 11;
      if (this.SignHead.DrawRect.Height > 10)
      {
        int num = this.SignHead.DrawRect.Height - 10;
        this.SignHead.DrawRect.Height = 10;
        this.SignHead.DrawRect.Y += num / 2;
      }
      if (this.SignHead.DrawRect.Width > 12)
      {
        int num = this.SignHead.DrawRect.Width - 12;
        this.SignHead.DrawRect.Width = 12;
        this.SignHead.DrawRect.X += num / 2;
      }
      this.SignHead.SetDrawOriginToCentre();
      this.SignHead.DrawOrigin.Y += 3f;
    }

    public override void DrawRenderComponent(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      bool IsTopLayer)
    {
      parent.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      parent.WorldOffsetDraw(spritebatch, drawWIthThis, 1f);
      this.Sign.vLocation = parent.vLocation;
      this.Sign.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, 1f);
      this.SignHead.vLocation = parent.vLocation;
      this.SignHead.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, 1f);
      RenderMath.TranslateWorldSpaceToScreenSpace(parent.vLocation);
    }
  }
}
