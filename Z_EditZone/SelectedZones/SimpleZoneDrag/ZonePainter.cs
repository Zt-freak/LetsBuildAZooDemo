// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag.ZonePainter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_Employees.WorkZonePane;

namespace TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag
{
  internal class ZonePainter
  {
    private DragFrameDrawer dragframedraw;
    private Vector2Int StartLocation_V2I;
    public Vector2Int LastSetTopLeft;
    public Vector2Int LastDraggedLoc;
    public Vector2 TopLeft;
    public Vector2 BottomRight;
    public bool MouseOver;

    public ZonePainter(Player player)
    {
      this.StartLocation_V2I = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTouchLocations[0]);
      this.dragframedraw = new DragFrameDrawer();
      this.LastSetTopLeft = new Vector2Int(this.StartLocation_V2I);
      this.LastDraggedLoc = new Vector2Int(this.StartLocation_V2I.X + 1, this.StartLocation_V2I.Y + 1);
    }

    public ZonePainter(WorkZone workzone)
    {
      this.StartLocation_V2I = new Vector2Int(workzone.TopLeft);
      this.LastDraggedLoc = new Vector2Int(workzone.BottomRight);
      this.dragframedraw = new DragFrameDrawer();
      this.LastSetTopLeft = new Vector2Int(this.StartLocation_V2I);
      this.LastDraggedLoc = new Vector2Int(workzone.BottomRight);
      this.TopLeft = TileMath.GetTileToWorldSpace(this.StartLocation_V2I);
      this.BottomRight = TileMath.GetTileToWorldSpace(this.LastDraggedLoc);
    }

    public void UpdateZonePainter(Player player, float DeltaTime)
    {
      Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTouchLocations[0]);
      if (spaceToTileLocation.CompareMatches(this.StartLocation_V2I))
      {
        this.TopLeft = TileMath.GetTileToWorldSpace(this.StartLocation_V2I);
        this.TopLeft.X -= 8f;
        this.TopLeft.Y -= 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.BottomRight = this.TopLeft;
        this.BottomRight.X += 16f;
        this.BottomRight.Y += 16f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.LastSetTopLeft = new Vector2Int(spaceToTileLocation);
        this.LastDraggedLoc = new Vector2Int(spaceToTileLocation.X + 1, spaceToTileLocation.Y + 1);
      }
      else
      {
        this.TopLeft = TileMath.GetTileToWorldSpace(this.StartLocation_V2I);
        this.BottomRight = TileMath.GetTileToWorldSpace(spaceToTileLocation);
        this.LastSetTopLeft = new Vector2Int(spaceToTileLocation);
        this.LastDraggedLoc = new Vector2Int(spaceToTileLocation.X, spaceToTileLocation.Y);
        if (this.StartLocation_V2I.X >= spaceToTileLocation.X && this.StartLocation_V2I.X > spaceToTileLocation.X)
        {
          int x1 = this.LastSetTopLeft.X;
          this.LastSetTopLeft.X = this.LastDraggedLoc.X;
          this.LastDraggedLoc.X = x1;
          float x2 = this.TopLeft.X;
          this.TopLeft.X = this.BottomRight.X;
          this.BottomRight.X = x2;
        }
        if (this.StartLocation_V2I.Y >= spaceToTileLocation.Y && this.StartLocation_V2I.Y > spaceToTileLocation.Y)
        {
          int y1 = this.LastSetTopLeft.Y;
          this.LastSetTopLeft.Y = this.LastDraggedLoc.Y;
          this.LastDraggedLoc.Y = y1;
          float y2 = this.TopLeft.Y;
          this.TopLeft.Y = this.BottomRight.Y;
          this.BottomRight.Y = y2;
        }
        this.TopLeft.X -= 8f;
        this.BottomRight.X += 8f;
        this.TopLeft.Y -= 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.BottomRight.Y += 8f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        ++this.BottomRight.X;
        ++this.BottomRight.Y;
      }
    }

    public void UpdateMouseOver(Vector2 PointerLocation)
    {
      if (!this.Overlaps(RenderMath.TranslateScreenSpaceToWorldSpace(PointerLocation)))
        return;
      this.MouseOver = true;
      this.dragframedraw.gameobject.SetAllColours(0.2f, 1f, 0.2f);
    }

    public bool Overlaps(Vector2 WorldSpavceLoc) => (double) WorldSpavceLoc.X >= (double) this.TopLeft.X && (double) WorldSpavceLoc.X <= (double) this.BottomRight.X && ((double) WorldSpavceLoc.Y >= (double) this.TopLeft.Y && (double) WorldSpavceLoc.Y <= (double) this.BottomRight.Y);

    public void DrawZonePainter()
    {
      this.dragframedraw.DrawDragFrame(this.TopLeft, this.BottomRight);
      if (!this.MouseOver)
        return;
      this.MouseOver = false;
      this.dragframedraw.gameobject.SetAllColours(1f, 1f, 1f);
    }
  }
}
