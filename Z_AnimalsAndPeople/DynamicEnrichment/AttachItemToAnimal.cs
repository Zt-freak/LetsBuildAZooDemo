// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.AttachItemToAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.PenNav;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class AttachItemToAnimal
  {
    private bool Attached;
    public Vector2Int WorldSpace;
    public AnimalRenderMan AttachedToThisAnimal;
    private Vector2 OriginExtra;
    private float HoldForThisLong;
    private float BlockPickUpForThisLong;
    private bool WillPostDraw;
    private bool AnimalWillNotDraw;

    public AttachItemToAnimal(TILETYPE tiletype)
    {
      this.Attached = false;
      if (!TileData.IsThisEnrichmentItemMsking(tiletype))
        return;
      this.AnimalWillNotDraw = true;
    }

    public void SetWorldSpaceLocation(Vector2Int _WorldSpace) => this.WorldSpace = _WorldSpace;

    public bool GetCanAttach() => !this.Attached && (double) this.BlockPickUpForThisLong <= 0.0;

    public void UpdateAttachItemToAnimal(
      float DeltaTime,
      ref AnimatedGameObject EnrichmentObject,
      PenNavigator pennavigation)
    {
      if (this.Attached)
      {
        this.HoldForThisLong -= DeltaTime;
        EnrichmentObject.scale = 1f;
        if ((double) this.HoldForThisLong < 0.0 || this.AttachedToThisAnimal.REF_prisonerinfo.IsDead)
        {
          EnrichmentObject.SetDrawOriginToCentre();
          this.Attached = false;
          if (this.AnimalWillNotDraw)
            this.AttachedToThisAnimal.BlockRendering = false;
          this.AttachedToThisAnimal.HoldingToy = false;
          this.BlockPickUpForThisLong = (float) TinyZoo.Game1.Rnd.Next(5, 20);
          pennavigation.CurrentWorldSpaceLocation = TileMath.GetWorldSpaceToTile(EnrichmentObject.vLocation);
          this.WorldSpace = pennavigation.CurrentWorldSpaceLocation;
        }
        else
        {
          EnrichmentObject.SetDrawOriginToCentre();
          EnrichmentObject.vLocation = this.AttachedToThisAnimal.enemyrenderere.vLocation;
          if ((double) this.AttachedToThisAnimal.enemyrenderere.DirectionFacing.X == -1.0)
          {
            EnrichmentObject.DrawOrigin.X -= this.OriginExtra.X;
            EnrichmentObject.DrawOrigin.Y += this.OriginExtra.Y;
            EnrichmentObject.FlipRender = true;
          }
          else
          {
            EnrichmentObject.FlipRender = false;
            EnrichmentObject.DrawOrigin.X += this.OriginExtra.X;
            EnrichmentObject.DrawOrigin.Y += this.OriginExtra.Y;
          }
        }
      }
      else
      {
        if ((double) this.BlockPickUpForThisLong <= 0.0)
          return;
        this.BlockPickUpForThisLong -= DeltaTime;
      }
    }

    public void AttachToThisAnimal(AnimalRenderMan animalrenderer, int UID)
    {
      animalrenderer.LastInteractionPoint_UID = UID;
      this.AttachedToThisAnimal = animalrenderer;
      this.AttachedToThisAnimal.HoldingToy = true;
      if (this.AnimalWillNotDraw)
        this.AttachedToThisAnimal.BlockRendering = true;
      this.Attached = true;
      this.HoldForThisLong = (float) TinyZoo.Game1.Rnd.Next(10, 30);
      Vector2 HeadOffsetFromBody;
      Rectangle HeadRect;
      GeneData.GetHybrid(animalrenderer.REF_prisonerinfo.intakeperson.animaltype, animalrenderer.REF_prisonerinfo.intakeperson.HeadType, out Rectangle _, out HeadOffsetFromBody, out HeadRect, out Vector2 _, animalrenderer.REF_prisonerinfo.intakeperson.CLIndex, animalrenderer.REF_prisonerinfo.intakeperson.HeadVariant);
      this.OriginExtra.X = animalrenderer.enemyrenderere.DrawOrigin.X - HeadOffsetFromBody.X;
      this.OriginExtra.Y = animalrenderer.enemyrenderere.DrawOrigin.Y - HeadOffsetFromBody.Y;
      this.OriginExtra.X -= (float) (HeadRect.Width / 2);
    }

    public void DrawAttachmentToAnimal(AnimatedGameObject EnrichmentObject, Texture2D DrawWithThis)
    {
      if (this.Attached && this.AttachedToThisAnimal != null && DrawWithThis != null)
        this.AttachedToThisAnimal.AddAttachementToDraw(EnrichmentObject, DrawWithThis);
      else
        EnrichmentObject.WorldOffsetDraw(AssetContainer.pointspritebatch01, DrawWithThis, EnrichmentObject.vLocation, Vector2.One, 0.0f);
    }
  }
}
