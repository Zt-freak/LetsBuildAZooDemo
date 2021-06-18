// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers.HealthInspectorPanelsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions;
using TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers
{
  internal class HealthInspectorPanelsManager
  {
    private VIPProfile profile;
    private CustomerActionList actions;
    private HealthInspectorInfo info;
    private Vector2 size;

    public HealthInspectorPanelsManager(WalkingPerson person, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.profile = new VIPProfile(person, person.simperson.GetName(), BaseScale);
      this.info = new HealthInspectorInfo(person, BaseScale, this.profile.GetSize().X);
      this.actions = new CustomerActionList(BaseScale);
      this.actions.AddAction(CustomerActionType.AnimalEncounter);
      this.actions.AddAction(CustomerActionType.Bribe);
      this.actions.AddAction(CustomerActionType.ReassignWorkers);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      this.profile.location += this.profile.GetSize() * 0.5f;
      zero.Y += this.profile.GetSize().Y + defaultBuffer.Y;
      this.info.location = zero;
      HealthInspectorInfo info1 = this.info;
      info1.location = info1.location + this.info.GetSize() * 0.5f;
      Vector2 vector2_1 = zero + this.info.GetSize();
      vector2_1.X += defaultBuffer.X;
      Vector2 vector2_2 = new Vector2(vector2_1.X, 0.0f);
      this.actions.location = vector2_2;
      this.actions.location += this.actions.GetSize() * 0.5f;
      Vector2 vector2_3 = vector2_2 + this.actions.GetSize();
      this.size = new Vector2(vector2_3.X, Math.Max(vector2_1.Y, vector2_3.Y));
      Vector2 vector2_4 = -this.size * 0.5f;
      this.profile.location += vector2_4;
      HealthInspectorInfo info2 = this.info;
      info2.location = info2.location + vector2_4;
      this.actions.location += vector2_4;
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateHealthInspectorPanelsManager(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTactionType)
    {
      return this.actions.UpdateCustomerActionsList(player, offset, DeltaTime, out OUTactionType);
    }

    public void DrawHealthInspectorPanelsManager(SpriteBatch spriteBatch, Vector2 offset)
    {
      this.profile.DrawVIPProfile(spriteBatch, offset);
      this.info.DrawHealthInspectorInfo(offset, spriteBatch);
      this.actions.DrawCustomerActionsList(spriteBatch, offset);
    }
  }
}
