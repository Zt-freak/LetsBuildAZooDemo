// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.IntakeStuff.IntakePerson
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Blance;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.IntakeStuff
{
  internal class IntakePerson
  {
    public string Name;
    public AnimalType animaltype;
    public int CLIndex;
    public int P_PerDay;
    public int CrimeIndex = -1;
    public bool WrongCell;
    public bool HasBeenAddedToTotalCaptured;
    public int Birthday = -1;
    public int NameIndex;
    private int IdentityIndex = -1;
    public bool IsAGirl;
    public int UID;
    public AnimalType HeadType;
    public int HeadVariant;

    public IntakePerson(
      AnimalType _enemytype,
      ReqForPeople reqforpeople = null,
      bool _IsAGirl = false,
      int Variant = 0,
      AnimalType _HeadType = AnimalType.None,
      int _HeadVariant = -1)
    {
      this.HeadType = _HeadType;
      this.IsAGirl = _IsAGirl;
      this.HeadVariant = _HeadVariant;
      if (reqforpeople != null)
      {
        this.P_PerDay = reqforpeople.wantsbyperson[(int) _enemytype].VPM.GetUnvallidatedValue() + TinyZoo.Game1.Rnd.Next(-2, 2);
        this.P_PerDay = MathHelper.Clamp(this.P_PerDay, 1, this.P_PerDay);
      }
      this.CLIndex = Variant;
      this.animaltype = _enemytype;
      this.GenerateName();
      int animaltype = (int) this.animaltype;
    }

    public void SetConsumption(ConsumptionStatus consumptionstatus)
    {
      for (int index = 0; index < LiveStats.reqforpeople.wantsbyperson[(int) this.animaltype].Uses.Length; ++index)
      {
        if (LiveStats.reqforpeople.wantsbyperson[(int) this.animaltype].Uses[index] > 0)
          consumptionstatus.ConsumptionValues[index] += (float) LiveStats.reqforpeople.wantsbyperson[(int) this.animaltype].Uses[index];
      }
    }

    public void ResetForLanguage() => this.Name = PeopleNames.GetName(this.IdentityIndex == 0, out this.NameIndex, this.animaltype, this.NameIndex);

    public void SaveIntakePerson(Writer writer)
    {
      if (this.IdentityIndex == -1)
        this.GenerateName();
      writer.WriteInt("p", this.UID);
      writer.WriteInt("i", this.NameIndex);
      writer.WriteInt("i", this.IdentityIndex);
      writer.WriteInt("i", (int) this.animaltype);
      writer.WriteInt("i", this.CLIndex);
      writer.WriteInt("i", this.P_PerDay);
      writer.WriteInt("i", this.CrimeIndex);
      writer.WriteBool("i", this.HasBeenAddedToTotalCaptured);
      writer.WriteInt("i", this.Birthday);
      writer.WriteInt("i", this.HeadVariant);
      writer.WriteInt("i", (int) this.HeadType);
      writer.WriteBool("i", this.IsAGirl);
    }

    private void GenerateName()
    {
      this.IdentityIndex = TinyZoo.Game1.Rnd.Next(0, 2);
      this.Name = PeopleNames.GetName(this.IdentityIndex == 0, out this.NameIndex, this.animaltype);
    }

    public IntakePerson(Reader reader)
    {
      int num1 = (int) reader.ReadInt("p", ref this.UID);
      int num2 = (int) reader.ReadInt("i", ref this.NameIndex);
      int num3 = (int) reader.ReadInt("i", ref this.IdentityIndex);
      int _out = 0;
      int num4 = (int) reader.ReadInt("i", ref _out);
      this.animaltype = (AnimalType) _out;
      int num5 = (int) reader.ReadInt("i", ref this.CLIndex);
      int num6 = (int) reader.ReadInt("i", ref this.P_PerDay);
      int num7 = (int) reader.ReadInt("i", ref this.CrimeIndex);
      this.Name = PeopleNames.GetName(this.IdentityIndex == 0, out this.NameIndex, this.animaltype, this.NameIndex);
      int num8 = (int) reader.ReadBool("i", ref this.HasBeenAddedToTotalCaptured);
      int num9 = (int) reader.ReadInt("i", ref this.Birthday);
      int num10 = (int) reader.ReadInt("i", ref this.HeadVariant);
      int num11 = (int) reader.ReadInt("i", ref _out);
      this.HeadType = (AnimalType) _out;
      int num12 = (int) reader.ReadBool("i", ref this.IsAGirl);
    }
  }
}
