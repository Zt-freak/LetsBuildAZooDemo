// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.DemandsActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_Quests.CharacterQuests.QuestList;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class DemandsActionPopUp : CustomerActionPopUp
  {
    private List<HeroQuestsDisplayEntry> quests;

    public DemandsActionPopUp(Player player, float basescale_)
      : base(basescale_)
    {
      this.quests = new List<HeroQuestsDisplayEntry>();
      throw new Exception("IS THIS A TEST? HOW WOULD WE KNOW TO ADD THESE");
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      foreach (HeroQuestsDisplayEntry quest in this.quests)
        flag |= quest.UpdateHeroQuestsDisplayEntry(offset, DeltaTime, player);
      return flag;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      foreach (HeroQuestsDisplayEntry quest in this.quests)
        quest.DrawHeroQuestsDisplayEntry(offset, spritebatch);
    }
  }
}
