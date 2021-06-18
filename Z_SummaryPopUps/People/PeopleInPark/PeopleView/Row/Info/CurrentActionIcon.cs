// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.CurrentActionIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class CurrentActionIcon : GameObject
  {
    private float BaseScale;

    public CurrentActionIcon(
      CustomerQuest quest,
      bool WalkingToBus,
      bool LeftPark,
      bool IsDead,
      float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.SetAction(quest, WalkingToBus, LeftPark, IsDead);
    }

    public CurrentActionIcon(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.SetAction(CustomerQuest.Count, false, false, false);
    }

    public void SetAction(CustomerQuest quest, bool WalkingToBus, bool LeftPark, bool IsDead)
    {
      this.scale = this.BaseScale;
      if (LeftPark | WalkingToBus)
        this.DrawRect = new Rectangle(946, 607, 15, 14);
      else if (IsDead)
      {
        this.DrawRect = new Rectangle(93, 636, 11, 11);
      }
      else
      {
        switch (quest)
        {
          case CustomerQuest.SeekingBin:
            this.DrawRect = new Rectangle(928, 471, 11, 12);
            break;
          case CustomerQuest.SeekingBathroom:
            this.DrawRect = new Rectangle(0, 774, 12, 14);
            break;
          case CustomerQuest.SeekingDrink:
            this.DrawRect = new Rectangle(228, 735, 10, 14);
            break;
          case CustomerQuest.SeekingFood:
            this.DrawRect = new Rectangle(0, 761, 14, 12);
            break;
          case CustomerQuest.SeekingSouvenier:
          case CustomerQuest.SeekingBuyingSouvenirsBeforeLeavingPark:
            this.DrawRect = new Rectangle(0, 590, 12, 13);
            break;
          case CustomerQuest.SeekingIceCream:
            this.DrawRect = new Rectangle(118, 616, 8, 12);
            break;
          case CustomerQuest.SeekingBench:
            this.DrawRect = new Rectangle(193, 636, 9, 14);
            break;
          case CustomerQuest.WantsToSeeAnimal:
          case CustomerQuest.LookingAtAnimal:
            this.DrawRect = new Rectangle(82, 636, 10, 11);
            break;
          case CustomerQuest.SeekingATM:
            this.DrawRect = new Rectangle(386, 859, 9, 11);
            break;
          case CustomerQuest.Count:
            this.DrawRect = new Rectangle(0, 789, 13, 14);
            break;
          case CustomerQuest.InQueueForShop:
            this.DrawRect = new Rectangle(0, 789, 13, 14);
            break;
          case CustomerQuest.IsBeingServedAtShop:
            this.DrawRect = new Rectangle(0, 789, 13, 14);
            break;
          default:
            this.DrawRect = TinyZoo.Game1.WhitePixelRect;
            this.scale = 16f * this.BaseScale;
            break;
        }
      }
      this.SetDrawOriginToCentre();
    }

    public void Deactivate()
    {
      this.SetAllColours(Color.LightGray.ToVector3());
      this.SetAlpha(0.8f);
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawCurrentActionIcon(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}
