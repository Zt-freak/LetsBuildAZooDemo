// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components.ParkGate
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_DayNight;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components
{
  internal class ParkGate : RenderComponent
  {
    private GameObject GetLeft;
    private GameObject GateRight;
    private float TImer;
    private static bool DoNotReconstrct;
    private static bool GateIsOpen;
    private static List<WalkingPerson> GatePeople;

    public ParkGate(TileRenderer parent)
      : base(parent, RenderComponentType.ParkGate)
    {
      if (ParkGate.GatePeople == null)
        ParkGate.GatePeople = new List<WalkingPerson>();
      if (ParkGate.GatePeople == null)
        ParkGate.GatePeople = new List<WalkingPerson>();
      int count = ParkGate.GatePeople.Count;
      this.GetLeft = new GameObject();
      this.GateRight = new GameObject();
      this.GateRight.DrawRect = new Rectangle(258, 55, 24, 14);
      this.GetLeft.DrawRect = new Rectangle(282, 55, 24, 14);
      this.GateRight.DrawOrigin = new Vector2(0.0f, 12f);
      this.GetLeft.DrawOrigin = new Vector2(24f, 12f);
    }

    internal static void Reset() => ParkGate.GatePeople = new List<WalkingPerson>();

    internal static void NewEmployeeWantsToGoThoughGate(WalkingPerson NewEmployee)
    {
      if (ParkGate.GatePeople == null)
        ParkGate.GatePeople = new List<WalkingPerson>();
      ParkGate.GatePeople.Add(NewEmployee);
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (ParkGate.GateIsOpen || ParkGate.GatePeople.Count > 0)
      {
        for (int index = ParkGate.GatePeople.Count - 1; index > -1; --index)
        {
          if (ParkGate.GatePeople[index].pathnavigator.CurrentTile.Y <= 49)
            ParkGate.GatePeople.RemoveAt(index);
        }
        if (this.GetLeft.DrawRect.Width > 0)
        {
          this.TImer += DeltaTime;
          if ((double) this.TImer > 0.100000001490116)
          {
            --this.GateRight.DrawRect.Width;
            this.TImer = 0.0f;
            this.SetRects();
          }
        }
      }
      else if (this.GetLeft.DrawRect.Width < 24)
      {
        this.TImer += DeltaTime;
        if ((double) this.TImer > 0.100000001490116)
        {
          ++this.GateRight.DrawRect.Width;
          this.TImer = 0.0f;
          this.SetRects();
        }
      }
      return false;
    }

    private void SetRects()
    {
      this.GetLeft.DrawRect = new Rectangle(282, 55, 24, 14);
      this.GetLeft.DrawRect.Width = this.GateRight.DrawRect.Width;
      this.GateRight.DrawOrigin = new Vector2(0.0f, 12f);
      this.GateRight.DrawOrigin.X -= (float) (24 - this.GateRight.DrawRect.Width);
      this.GetLeft.DrawOrigin = new Vector2(24f, 12f);
      this.GetLeft.DrawRect.X += 24 - this.GateRight.DrawRect.Width;
    }

    internal static void OpenGate(bool OpenIt) => ParkGate.GateIsOpen = OpenIt;

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
      if (DebugFlags.HideAllUI_DEBUG || this.GateRight.DrawRect.Width <= 0)
        return;
      this.GetLeft.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.GateRight.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.GetLeft.vLocation = parent.vLocation;
      this.GateRight.vLocation = parent.vLocation;
      this.GateRight.WorldOffsetDraw(spritebatch, drawWIthThis);
      this.GetLeft.WorldOffsetDraw(spritebatch, drawWIthThis);
    }
  }
}
