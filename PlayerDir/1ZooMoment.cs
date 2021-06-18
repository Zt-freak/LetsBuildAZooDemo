// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.ZooMoment
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir
{
  internal class ZooMoment
  {
    public ZOOMOMENT zoomoment;
    public int TimeOfDay;
    public int UID;
    public bool StatsFlag;
    public int PenUID;
    public bool AllowUpdateDuringPopUp;
    public int tileType_int;
    public int roamingemployeetype;

    public ZooMoment(ZOOMOMENT _zoomoment, int _TimeOfDay = -1, int _UID = -1, bool _AllowUpdateDuringPopUp = false)
    {
      this.AllowUpdateDuringPopUp = _AllowUpdateDuringPopUp;
      if (_TimeOfDay == -1)
        this.SetRandomTimeOfDay();
      else
        this.TimeOfDay = _TimeOfDay;
      this.UID = _UID;
      this.zoomoment = _zoomoment;
      if (_zoomoment != ZOOMOMENT.CRISPR_Birth)
        return;
      this.AllowUpdateDuringPopUp = true;
    }

    public ZooMoment(
      ZOOMOMENT _zoomoment,
      TILETYPE tileType,
      EmployeeType RoamingEmployeeType,
      bool UseNowAsStartTime = false)
    {
      this.zoomoment = _zoomoment;
      this.tileType_int = (int) tileType;
      this.roamingemployeetype = (int) RoamingEmployeeType;
      this.SetRandomTimeOfDay(UseNowAsStartTime);
      if (_zoomoment != ZOOMOMENT.NewApplicant)
        return;
      this.AllowUpdateDuringPopUp = true;
    }

    public ZooMoment(ZOOMOMENT _zoomoment, bool IsFamilyFight, int _PenUID = -1, int _UID = -1)
    {
      this.PenUID = _PenUID;
      this.zoomoment = _zoomoment;
      this.UID = _UID;
      this.StatsFlag = IsFamilyFight;
      this.SetRandomTimeOfDay();
    }

    private void SetRandomTimeOfDay(bool UseNowAsStartTime = false)
    {
      if (UseNowAsStartTime)
      {
        int maxValue = (int) ((double) Z_GameFlags.GetTimeThatParkOpensInMorning_Seconds() + ((double) Z_GameFlags.SecondsZooOpenPerDay - 2.0));
        if ((double) maxValue > (double) Z_GameFlags.DayTimer)
        {
          this.TimeOfDay = Game1.Rnd.Next((int) Z_GameFlags.DayTimer, maxValue);
          return;
        }
      }
      this.TimeOfDay = Game1.Rnd.Next(0, (int) ((double) Z_GameFlags.SecondsZooOpenPerDay - 2.0));
      this.TimeOfDay += (int) Z_GameFlags.GetTimeThatParkOpensInMorning_Seconds();
    }
  }
}
