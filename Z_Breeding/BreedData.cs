// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Breeding.BreedData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.FileInOut;
using System;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_Breeding
{
  internal class BreedData
  {
    internal static BreedInfo[] breedinfo;

    internal static void SetUpBreedData()
    {
      if (BreedData.breedinfo != null)
        return;
      CSVFileReader csvFileReader = new CSVFileReader();
      csvFileReader.Read("Brd.csv");
      int Column1 = -1;
      int Column2 = -1;
      int Column3 = -1;
      int Column4 = -1;
      int Column5 = -1;
      int Column6 = -1;
      int Column7 = -1;
      int Column8 = -1;
      int Column9 = -1;
      int Column10 = -1;
      int Column11 = -1;
      for (int Column12 = 0; Column12 < csvFileReader.GetColumnCount(); ++Column12)
      {
        if (csvFileReader.GetCell(0, Column12) == "Animal")
          Column1 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant1")
          Column2 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant2")
          Column3 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant3")
          Column4 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant4")
          Column5 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant5")
          Column6 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant6")
          Column7 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant7")
          Column8 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant8")
          Column9 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Variant9")
          Column10 = Column12;
        if (csvFileReader.GetCell(0, Column12) == "Genome")
          Column11 = Column12;
      }
      BreedData.breedinfo = new BreedInfo[70];
      for (int Row = 1; Row < csvFileReader.GetRowCount(); Row = Row + 2 + 1)
      {
        AnimalType stringToEnemyType = EnemyData.GetStringToEnemyType(csvFileReader.GetCell(Row, Column1));
        BreedData.breedinfo[(int) stringToEnemyType] = new BreedInfo();
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(-1, -1, 100));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column2)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column2)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column2))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column3)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column3)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column3))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column4)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column4)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column4))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column5)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column5)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column5))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column6)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column6)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column6))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column7)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column7)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column7))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column8)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column8)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column8))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column9)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column9)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column9))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column10)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column10)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column10))));
        BreedData.breedinfo[(int) stringToEnemyType].breedentries.Add(new BreedEntry(Convert.ToInt32(csvFileReader.GetCell(Row, Column11)), Convert.ToInt32(csvFileReader.GetCell(Row + 1, Column11)), Convert.ToInt32(csvFileReader.GetCell(Row + 2, Column11))));
      }
    }

    internal static void GetParentsAndPercent(
      AnimalType Child,
      int VariantOfThisChild,
      out int Mother,
      out int Father,
      out int PercentChance)
    {
      if (BreedData.breedinfo == null)
        BreedData.SetUpBreedData();
      Mother = BreedData.breedinfo[(int) Child].breedentries[VariantOfThisChild].Parent1_girl;
      Father = BreedData.breedinfo[(int) Child].breedentries[VariantOfThisChild].Parent2;
      PercentChance = BreedData.breedinfo[(int) Child].breedentries[VariantOfThisChild].PercentChanceOfThisChild;
    }
  }
}
