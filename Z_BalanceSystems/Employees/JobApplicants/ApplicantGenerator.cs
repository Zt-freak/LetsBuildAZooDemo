// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Employees.JobApplicants.ApplicantGenerator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_BalanceSystems.Employees.JobApplicants
{
  internal class ApplicantGenerator
  {
    public static int MaxApplicantsForDisplay = 10;

    internal static void SetQuickPickEmployee(
      QuickEmployeeDescription preconstructed,
      Player player)
    {
      preconstructed.Level = (int) (100.0 * ((double) ApplicantGenerator.GetNewEmployeeLevel(player) * 0.600000023841858));
      if (preconstructed.Level >= 1)
        return;
      preconstructed.Level = 1;
    }

    private static float GetNewEmployeeLevel(Player player)
    {
      float num = player.unions.GetTotalEmployeeSatisfactionScore(player);
      if ((double) num < 0.0)
        num = 0.0f;
      return (float) ((double) Game1.Rnd.Next((int) ((double) num * 20.0), 50) * (double) num * 0.00999999977648258);
    }

    public static Applicant CreateNewApplicant(
      TILETYPE tileType,
      EmployeeType roamingemplyeetype,
      bool IsAgencyHire,
      Player player)
    {
      bool IsAGirl;
      AnimalType animalType = roamingemplyeetype == EmployeeType.None ? EmployeeData.GetBuildingtoEmployee(tileType, out IsAGirl) : TinyZoo.PlayerDir.Employees.GetEmployee(roamingemplyeetype, out IsAGirl);
      float _StarRating = ApplicantGenerator.GetNewEmployeeLevel(player) * 6f;
      Seniority _SeniorityLevel = Seniority.Junior;
      int NameStringIndex;
      string name = PeopleNames.GetName(!IsAGirl, out NameStringIndex, animalType);
      return new Applicant(animalType, _StarRating, _SeniorityLevel, IsAGirl, name, IsAgencyHire, NameStringIndex);
    }
  }
}
