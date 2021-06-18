// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.JobList.JobButtonManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.OverWorld.Intake.JobList
{
  internal class JobButtonManager
  {
    private ButtonRepeater repeater;
    public int Selected;
    private List<JobButton> jobbuttons;

    public JobButtonManager(Player player)
    {
      if (player.prisonlayout.IsNotEarningMoney(player))
      {
        if (player.intakes.intakeinfos[0].People.Count > 1 || player.intakes.intakeinfos[0].People[0].animaltype != AnimalType.Rabbit)
        {
          player.intakes.intakeinfos[0] = new IntakeInfo();
          player.intakes.intakeinfos[0].People.Add(new IntakePerson(AnimalType.Rabbit, LiveStats.reqforpeople));
        }
        if (player.intakes.intakeinfos[0].GetRecCost(true) > player.Stats.GetCashHeld())
          player.Stats.GiveCash(player.intakes.intakeinfos[0].GetRecCost(true), player);
      }
      this.repeater = new ButtonRepeater();
      this.jobbuttons = new List<JobButton>();
      for (int index = 0; index < player.intakes.intakeinfos.Count; ++index)
      {
        this.jobbuttons.Add(new JobButton(player.intakes.intakeinfos[index]));
        this.jobbuttons[index].Location = new Vector2(130f, (float) (390.0 * (double) Sengine.ScreenRationReductionMultiplier.Y + (double) (index * 80) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
    }

    public bool UpdateEntryManager(float DeltaTime, Vector2 Offset, Player player)
    {
      bool flag = false;
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
      {
        switch (Direction)
        {
          case DirectionPressed.Up:
            if (this.Selected > 0)
            {
              --this.Selected;
              flag = true;
              break;
            }
            break;
          case DirectionPressed.Down:
            if (this.Selected < this.jobbuttons.Count - 1)
            {
              ++this.Selected;
              flag = true;
              break;
            }
            break;
        }
      }
      for (int index = 0; index < this.jobbuttons.Count; ++index)
      {
        if (this.jobbuttons[index].UpdateJobButton(Offset, player) && this.Selected != index)
        {
          this.Selected = index;
          flag = true;
        }
      }
      return flag;
    }

    public void DraEntryManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      for (int index = 0; index < this.jobbuttons.Count; ++index)
        this.jobbuttons[index].DrawJobButton(Offset, this.Selected == index, spritebatch);
    }
  }
}
