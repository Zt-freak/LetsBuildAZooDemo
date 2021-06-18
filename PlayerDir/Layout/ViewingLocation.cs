// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.ViewingLocation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;

namespace TinyZoo.PlayerDir.Layout
{
  internal class ViewingLocation
  {
    public Vector2Int Location;
    public float Privacy;
    private float Privacy_lowerBetterForViewer;
    public DirectionPressed faceThisWayToLookAtPen;

    public ViewingLocation(
      int XLoc,
      int Yloc,
      float Privacy,
      DirectionPressed _faceThisWayToLookAtPen)
    {
      this.Location = new Vector2Int(XLoc, Yloc);
      this.Privacy_lowerBetterForViewer = Privacy;
      this.faceThisWayToLookAtPen = _faceThisWayToLookAtPen;
    }
  }
}
