// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments.PersonAttachmentManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments
{
  internal class PersonAttachmentManager
  {
    private List<AttachmentRenderer> attachementrenderers;
    private int Frame = -1;
    private bool HasTimeUpdate;
    private DirectionPressed directionfacing = DirectionPressed.None;
    private int CurrentLegSlice;
    private Vector2 RenderOffset;
    private bool CharacterAnimationBlocked;

    public PersonAttachmentManager() => this.attachementrenderers = new List<AttachmentRenderer>();

    public void RemoveAttachment(PersonAttachementType attachmenttype)
    {
      for (int index = 0; index < this.attachementrenderers.Count; ++index)
      {
        if (this.attachementrenderers[index].REF_attachmentinfo.attachmenttype == attachmenttype)
        {
          this.attachementrenderers.RemoveAt(index);
          break;
        }
      }
    }

    public bool AddItem(PersonAttachementType attachmenttype, WalkingPerson parent)
    {
      AttachmentInfo attachInfo = PeopleAttachmentData.GetAttachInfo(attachmenttype);
      for (int index = 0; index < this.attachementrenderers.Count; ++index)
      {
        if (this.attachementrenderers[index].REF_attachmentinfo.attachmentlocation == attachInfo.attachmentlocation)
          return false;
      }
      this.attachementrenderers.Add(new AttachmentRenderer(PeopleAttachmentData.GetAttachInfo(attachmenttype)));
      this.attachementrenderers[this.attachementrenderers.Count - 1].SetFrame(parent, parent.Frame, ref this.CurrentLegSlice, ref this.RenderOffset, this.CharacterAnimationBlocked);
      this.HasTimeUpdate |= this.attachementrenderers[this.attachementrenderers.Count - 1].REF_attachmentinfo.HasIdle;
      this.CharacterAnimationBlocked |= this.attachementrenderers[this.attachementrenderers.Count - 1].REF_attachmentinfo.HasIdle;
      return true;
    }

    public void UpdatePersonAttachmentManager(WalkingPerson parent, float DeltaTime)
    {
      if (this.Frame != parent.Frame || this.directionfacing != parent.directionmoving)
      {
        if (this.CharacterAnimationBlocked && parent.Frame != 0)
        {
          parent.Frame = 0;
          parent.SetAnimationDrawRectangle();
        }
        this.CurrentLegSlice = 0;
        this.Frame = parent.Frame;
        this.directionfacing = parent.directionmoving;
        this.RenderOffset = Vector2.Zero;
        for (int index = 0; index < this.attachementrenderers.Count; ++index)
          this.attachementrenderers[index].SetFrame(parent, parent.Frame, ref this.CurrentLegSlice, ref this.RenderOffset, this.CharacterAnimationBlocked, true);
      }
      if (!this.HasTimeUpdate)
        return;
      for (int index = 0; index < this.attachementrenderers.Count; ++index)
        this.attachementrenderers[index].UpdateAttachmentRenderer(parent, DeltaTime, ref this.CurrentLegSlice, ref this.RenderOffset, ref this.CharacterAnimationBlocked);
    }

    internal static PersonAttachementType GetFOODTYPEToPersonAttachementType(
      FOODTYPE foodtype)
    {
      switch (foodtype)
      {
        case FOODTYPE.HotDog:
        case FOODTYPE.CornDog:
        case FOODTYPE.VeganDog:
          return PersonAttachementType.HotDog;
        case FOODTYPE.KeyChain:
        case FOODTYPE.FridgeMagnet:
        case FOODTYPE.Mug:
          return PersonAttachementType.ShoppingBag;
        case FOODTYPE.Hat:
          switch (Game1.Rnd.Next(0, 2))
          {
            case 0:
              return PersonAttachementType.CrocHat;
            default:
              return PersonAttachementType.FoxHat;
          }
        case FOODTYPE.TShirt:
        case FOODTYPE.Plushie:
          return PersonAttachementType.ShoppingBag;
        case FOODTYPE.SingleScoop:
          return PersonAttachementType.SingleScoop;
        case FOODTYPE.SnowCone:
          return PersonAttachementType.SnowCone;
        case FOODTYPE.Popsicle:
          return PersonAttachementType.Popsicle;
        case FOODTYPE.Sundae:
          return PersonAttachementType.Sundae;
        case FOODTYPE.BananaSplit:
          return PersonAttachementType.BananaSplit;
        case FOODTYPE.Parfait:
          return PersonAttachementType.Parfait;
        case FOODTYPE.CoconutJuice:
          return PersonAttachementType.Coconut;
        case FOODTYPE.FruitPunch:
        case FOODTYPE.Mocktail:
          return PersonAttachementType.Mocktail;
        case FOODTYPE.JuniorBurger:
        case FOODTYPE.VegetarianBurger:
        case FOODTYPE.CholesterolKingBurger:
          return PersonAttachementType.Burger;
        case FOODTYPE.Cola:
          return PersonAttachementType.Cola;
        case FOODTYPE.Coffee:
        case FOODTYPE.KopiLuwak:
          return PersonAttachementType.Coffee;
        case FOODTYPE.Crisps:
          return PersonAttachementType.Crisps;
        case FOODTYPE.Chocolate:
          return PersonAttachementType.Chocolate;
        case FOODTYPE.Slurpz:
          return PersonAttachementType.Slushie;
        case FOODTYPE.Margarita:
        case FOODTYPE.Hawaiian:
          return PersonAttachementType.Pizza;
        case FOODTYPE.PinkCottonCandy:
        case FOODTYPE.AnimalCandy:
          return PersonAttachementType.CottonCandy;
        case FOODTYPE.AnimalBalloon:
          switch (Game1.Rnd.Next(0, 3))
          {
            case 0:
              return PersonAttachementType.HippoBalloon;
            case 1:
              return PersonAttachementType.BearBalloon;
            default:
              return PersonAttachementType.PigBalloon;
          }
        case FOODTYPE.Churros:
        case FOODTYPE.FrostedChurros:
          return PersonAttachementType.Churros;
        case FOODTYPE.PopCorn:
          return PersonAttachementType.Popcorn;
        case FOODTYPE.CraftBeer:
          return PersonAttachementType.CraftBeer;
        case FOODTYPE.FreedomFries:
          return PersonAttachementType.Fries;
        case FOODTYPE.Pretzel:
          return PersonAttachementType.Pretzel;
        case FOODTYPE.BeefTaco:
          return PersonAttachementType.Taco;
        default:
          return PersonAttachementType.Count;
      }
    }

    public void DrawUpdatePersonAttachmentManager(WalkingPerson parent)
    {
      if (this.CharacterAnimationBlocked && parent.Frame != 0)
      {
        parent.Frame = 0;
        parent.SetAnimationDrawRectangle();
      }
      WalkingPerson walkingPerson1 = parent;
      walkingPerson1.vLocation = walkingPerson1.vLocation + this.RenderOffset;
      for (int index = 0; index < this.attachementrenderers.Count; ++index)
        this.attachementrenderers[index].PreDraw(parent);
      if (this.CurrentLegSlice > 0)
        parent.DrawRect.Height -= this.CurrentLegSlice;
      parent.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
      if (this.CurrentLegSlice > 0)
        parent.DrawRect.Height += this.CurrentLegSlice;
      for (int index = 0; index < this.attachementrenderers.Count; ++index)
        this.attachementrenderers[index].PostDraw(parent);
      WalkingPerson walkingPerson2 = parent;
      walkingPerson2.vLocation = walkingPerson2.vLocation - this.RenderOffset;
    }
  }
}
