// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.TrampolineManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class TrampolineManager
  {
    private List<TrampolineBouncer> currentbouncers;
    private Vector2 LocationInWorldSpace;
    private Vector2Int Location;
    private TileRenderer tilerenderer;
    private int Frame;
    private Rectangle rect;
    private float Time;
    private bool HasSet;

    public TrampolineManager(Vector2Int _Location, TileRenderer _tilerenderer)
    {
      this.HasSet = false;
      this.Location = _Location;
      this.LocationInWorldSpace = TileMath.GetTileToWorldSpace(this.Location);
      this.currentbouncers = new List<TrampolineBouncer>();
      this.tilerenderer = _tilerenderer;
      this.rect = this.tilerenderer.DrawRect;
      this.Frame = 0;
      if (this.tilerenderer.rendercomponent == null)
        return;
      for (int index = 0; index < this.tilerenderer.rendercomponent.Count; ++index)
        this.rect = this.tilerenderer.DrawRect;
    }

    private void SetTileRenderer()
    {
    }

    public void AttachAnimalToTrampoline(AnimalRenderMan animalrenderer, int UID)
    {
      for (int index = this.currentbouncers.Count - 1; index > -1; --index)
      {
        if (this.currentbouncers[index].animalrenderer == animalrenderer)
        {
          animalrenderer.LastInteractionPoint_UID = UID;
          return;
        }
      }
      animalrenderer.BlockWalkingAndRendering = true;
      this.currentbouncers.Add(new TrampolineBouncer(animalrenderer, this.LocationInWorldSpace));
      animalrenderer.LastInteractionPoint_UID = UID;
    }

    public void ForceDetach(AnimalRenderMan animalrenderer, GameObject EnrichmentObject)
    {
      for (int index = this.currentbouncers.Count - 1; index > -1; --index)
      {
        if (this.currentbouncers[index].animalrenderer == animalrenderer)
          this.currentbouncers.RemoveAt(index);
      }
      if (this.currentbouncers.Count != 0)
        return;
      this.Frame = 0;
      this.SetFrame(EnrichmentObject);
    }

    public void UpdateTrampolineManager(float DeltaTime, GameObject EnrichmentObject)
    {
      if (!this.HasSet)
      {
        this.HasSet = true;
        this.rect = EnrichmentObject.DrawRect;
      }
      for (int index = this.currentbouncers.Count - 1; index > -1; --index)
      {
        bool StartedJump;
        if (this.currentbouncers[index].UpdateTrampolineBouncer(DeltaTime, out StartedJump))
        {
          this.currentbouncers[index].animalrenderer.enemyrenderere.animator.UnStopJumping();
          this.currentbouncers[index].animalrenderer.BlockWalkingAndRendering = false;
          this.currentbouncers.RemoveAt(index);
        }
        if (StartedJump)
        {
          this.Time = 0.0f;
          this.Frame = 1;
          this.SetFrame(EnrichmentObject);
        }
      }
      if (this.Frame == 0)
        return;
      this.Time += DeltaTime;
      if ((double) this.Time <= 0.100000001490116)
        return;
      this.Time = 0.0f;
      ++this.Frame;
      if (this.Frame == 3)
        this.Frame = 0;
      this.SetFrame(EnrichmentObject);
    }

    private void SetFrame(GameObject EnrichmentObject) => EnrichmentObject.DrawRect.X = this.rect.X + (this.rect.Width + 1) * this.Frame;

    public void ForceDetatch()
    {
      for (int index = this.currentbouncers.Count - 1; index > -1; --index)
        this.currentbouncers[index].ForceDetach();
    }

    public void DrawTrampolineManager(bool IsPreDraw)
    {
      for (int index = 0; index < this.currentbouncers.Count; ++index)
        this.currentbouncers[index].DrawTrampolineBouncer(IsPreDraw);
    }
  }
}
