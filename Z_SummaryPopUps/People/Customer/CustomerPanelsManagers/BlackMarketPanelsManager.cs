// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers.BlackMarketPanelsManager
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
  internal class BlackMarketPanelsManager
  {
    private VIPProfile profile;
    private BlackMarketInfo infoBox;
    private CustomerActionList actions;
    private Vector2 size;
    public static bool SomethingChanged_Refresh;

    public BlackMarketPanelsManager(WalkingPerson person, float basescale_, Player player)
    {
      Vector2 defaultBuffer = new UIScaleHelper(basescale_).DefaultBuffer;
      Vector2 zero1 = Vector2.Zero;
      Vector2 zero2 = Vector2.Zero;
      this.infoBox = new BlackMarketInfo(person, player, basescale_);
      this.profile = new VIPProfile(person, person.simperson.memberofthepublic.Name, basescale_, this.infoBox.GetSize().X);
      this.profile.location = this.profile.GetSize() * 0.5f;
      zero1.Y += this.profile.GetSize().Y + defaultBuffer.Y;
      this.infoBox.location = zero1 + this.infoBox.GetSize() * 0.5f;
      Vector2 vector2_1 = zero1 + this.infoBox.GetSize();
      vector2_1.X += defaultBuffer.X;
      zero2.X = vector2_1.X;
      this.actions = new CustomerActionList(basescale_);
      this.actions.AddAction(CustomerActionType.BuyAnimals);
      this.actions.AddAction(CustomerActionType.SellAnimals);
      this.actions.AddAction(CustomerActionType.Report);
      this.actions.location = zero2 + this.actions.GetSize() * 0.5f;
      for (int index = 0; index < this.actions.Count; ++index)
      {
        bool IsLockedBecauseofBeta;
        this.actions.LockThisAction(this.actions.customerActions[index].actiontype, GenericPanelsManager.IsThisActionEnabled(this.actions.customerActions[index].actiontype, out IsLockedBecauseofBeta), IsLockedBecauseofBeta);
      }
      Vector2 vector2_2 = zero2 + this.actions.GetSize();
      this.size.X = vector2_2.X;
      this.size.Y = Math.Max(vector2_1.Y, vector2_2.Y);
      Vector2 vector2_3 = -this.size * 0.5f;
      this.profile.location += vector2_3;
      this.infoBox.location += vector2_3;
      this.actions.location += vector2_3;
      BlackMarketPanelsManager.SomethingChanged_Refresh = false;
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateBlackMarketPanelsManager(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTActionType)
    {
      if (BlackMarketPanelsManager.SomethingChanged_Refresh)
      {
        this.infoBox.RefreshValues();
        BlackMarketPanelsManager.SomethingChanged_Refresh = false;
      }
      this.profile.UpdateVIPProfile(player, offset, DeltaTime);
      return this.actions.UpdateCustomerActionsList(player, offset, DeltaTime, out OUTActionType);
    }

    public void DrawBlackMarketPanelsManager(SpriteBatch spritebatch, Vector2 offset)
    {
      this.profile.DrawVIPProfile(spritebatch, offset);
      this.infoBox.DrawBlackMarketInfo(spritebatch, offset);
      this.actions.DrawCustomerActionsList(spritebatch, offset);
    }
  }
}
