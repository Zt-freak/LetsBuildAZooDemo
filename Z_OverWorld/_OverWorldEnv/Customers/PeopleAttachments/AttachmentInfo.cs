// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments.AttachmentInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments
{
  internal class AttachmentInfo
  {
    public PersonAttachementType attachmenttype;
    public AttachmentLocation attachmentlocation;
    public bool MoveInCode;
    public Vector2 RelativeDrawOrigin_Down;
    public Vector2 RelativeDrawOrigin_Right;
    public Vector2 RelativeDrawOrigin_Up;
    public Vector2 RelativeDrawOrigin_Left;
    public Rectangle Front;
    public Rectangle Right;
    public Rectangle Up;
    public bool HasCustomBackRect;
    public bool BlocksCharacterAnimation;
    public bool HasIdle;
    public int LegSliceLeft;
    public int LegSliceRight;
    public int LegSliceUp;
    public int LegSliceDown;
    public bool WillSliceLegs;
    public Vector2 OffsetLeft;
    public Vector2 OffsetRight;
    public Vector2 OffsetUp;
    public Vector2 OffsetDown;
    public bool UsesOffsets;

    public AttachmentInfo(
      PersonAttachementType _attachmenttype,
      AttachmentLocation _attachmentlocation,
      Rectangle _RectDown,
      Rectangle _Right,
      Vector2 _RelativeDrawOrigin_Down,
      Vector2 _RelativeDrawOrigin_Right,
      Vector2 _RelativeDrawOrigin_Left,
      Vector2 _RelativeDrawOrigin_Up,
      bool _MoveInCode = true)
    {
      this.attachmentlocation = _attachmentlocation;
      this.attachmenttype = _attachmenttype;
      this.MoveInCode = _MoveInCode;
      this.RelativeDrawOrigin_Down = _RelativeDrawOrigin_Down;
      this.RelativeDrawOrigin_Right = _RelativeDrawOrigin_Right;
      this.RelativeDrawOrigin_Up = _RelativeDrawOrigin_Up;
      this.RelativeDrawOrigin_Left = _RelativeDrawOrigin_Left;
      this.Front = _RectDown;
      this.Right = _Right;
    }

    public void SetCustomBackRectangle(Rectangle BackRect)
    {
      this.HasCustomBackRect = true;
      this.Up = BackRect;
    }

    public void SliceLegsOff(
      int _LegSliceRight,
      int _LegSliceUp,
      int _LegSliceDown,
      int _LegSliceLeft = -1)
    {
      this.LegSliceRight = _LegSliceRight;
      this.LegSliceUp = _LegSliceUp;
      this.LegSliceDown = _LegSliceDown;
      this.LegSliceLeft = _LegSliceLeft <= -1 ? this.LegSliceRight : _LegSliceLeft;
      this.WillSliceLegs = true;
    }

    public void SetOffsets(
      Vector2 _OffsetLeft,
      Vector2 _OffsetRight,
      Vector2 _OffsetUp,
      Vector2 _OffsetDown)
    {
      this.UsesOffsets = true;
      this.OffsetLeft = _OffsetLeft;
      this.OffsetRight = _OffsetRight;
      this.OffsetUp = _OffsetUp;
      this.OffsetDown = _OffsetDown;
    }
  }
}
