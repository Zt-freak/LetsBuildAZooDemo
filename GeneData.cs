// Decompiled with JetBrains decompiler
// Type: TinyZoo.GeneData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Gene_Data;

namespace TinyZoo
{
  internal class GeneData
  {
    private static GeneDrawHeadInfo[] HeadsInfo;

    internal static void GetHybrid(
      AnimalType BodyAnimal,
      AnimalType HeadAnimal,
      out Rectangle BodyRect,
      out Vector2 HeadOffsetFromBody,
      out Rectangle HeadRect,
      out Vector2 HeadOrigin,
      int BodyVariant = 0,
      int HeadVariant = 0)
    {
      if (HeadAnimal == AnimalType.None)
        HeadAnimal = BodyAnimal;
      if (HeadVariant == -1)
        HeadVariant = 0;
      BodyRect = new Rectangle(0, 0, 10, 10);
      HeadRect = new Rectangle(0, 0, 10, 10);
      HeadOffsetFromBody = Vector2.Zero;
      HeadOrigin = Vector2.Zero;
      GeneDataBodies.GetGeneDataBody(BodyAnimal, HeadAnimal, BodyVariant, HeadVariant, out BodyRect, out HeadOffsetFromBody);
      if (GeneData.HeadsInfo == null)
        GeneData.HeadsInfo = new GeneDrawHeadInfo[70];
      if (GeneData.HeadsInfo[(int) HeadAnimal] == null)
      {
        switch (HeadAnimal)
        {
          case AnimalType.Rabbit:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(11, 404, 6, 10), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(421, 306, 8, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(970, 81, 8, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(970, 93, 8, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(970, 105, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1112, 121, 13, 10), new Vector2(5f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1126, 122, 11, 9), new Vector2(3f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1138, 121, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1282, 64, 10, 13), new Vector2(3f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1280, 52, 12, 11), new Vector2(4f, 7f));
            break;
          case AnimalType.Goose:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(29, 406, 6, 8), new Vector2(1f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(430, 306, 6, 6), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(438, 305, 6, 7), new Vector2(1f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(368, 307, 6, 6), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1225, 51, 6, 8), new Vector2(1f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1035, 122, 8, 7), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1232, 51, 6, 8), new Vector2(1f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1158, 32, 6, 9), new Vector2(1f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1239, 51, 6, 8), new Vector2(1f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1148, 121, 7, 10), new Vector2(1f, 10f));
            break;
          case AnimalType.Capybara:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(36, 406, 10, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(970, 118, 9, 9), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1271, 66, 10, 11), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(649, 233, 11, 9), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1593, 45, 10, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1593, 55, 10, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1604, 45, 10, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1604, 55, 10, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1593, 65, 10, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1545, 65, 13, 11), new Vector2(4f, 6f));
            break;
          case AnimalType.Pig:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(27, 415, 10, 10), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(998, 122, 10, 10), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(987, 122, 10, 10), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1513, 144, 10, 10), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(981, 112, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1524, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1536, 144, 11, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(649, 243, 8, 8), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1548, 144, 11, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1560, 144, 10, 10), new Vector2(2f, 8f));
            break;
          case AnimalType.Duck:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(38, 416, 6, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1027, 122, 7, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1137, 64, 6, 5), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1137, 70, 6, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1020, 122, 6, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1013, 122, 6, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(873, 89, 7, 6), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1243, 59, 6, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1246, 51, 8, 7), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(713, 69, 9, 9), new Vector2(5f, 9f));
            break;
          case AnimalType.Snake:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(40, 423, 8, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(630, 136, 8, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(768, 97, 7, 5), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(630, 145, 8, 8), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1172, 126, 7, 5), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(639, 136, 8, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(639, 145, 8, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(646, 0, 8, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1549, 76, 8, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1250, 59, 7, 6), new Vector2(2f, 5f));
            break;
          case AnimalType.Badger:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(52, 432, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1255, 51, 11, 7), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(915, 0, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(915, 9, 9, 7), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1513, 135, 10, 8), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1524, 136, 11, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1536, 135, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(816, 94, 8, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1546, 136, 8, 7), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1555, 136, 8, 7), new Vector2(2f, 5f));
            break;
          case AnimalType.Hyena:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(0, 426, 11, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1524, 124, 11, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1536, 124, 11, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(981, 102, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1548, 124, 9, 11), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1571, 144, 11, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1546, 112, 11, 11), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1564, 136, 10, 7), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1583, 144, 11, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1546, 101, 11, 10), new Vector2(3f, 6f));
            break;
          case AnimalType.Porcupine:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(532, 96, 8, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1595, 144, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1165, 126, 6, 5), new Vector2(2f, 2f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1575, 136, 8, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1584, 136, 7, 7), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(980, 124, 6, 8), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(646, 9, 7, 6), new Vector2(1f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(925, 0, 7, 8), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1258, 59, 8, 6), new Vector2(2f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1550, 85, 7, 6), new Vector2(1f, 4f));
            break;
          case AnimalType.Bear:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(12, 426, 12, 11), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1558, 124, 12, 11), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1571, 124, 12, 11), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1559, 69, 15, 11), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1558, 114, 12, 9), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1621, 29, 14, 13), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1558, 103, 12, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1575, 69, 13, 11), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1558, 81, 16, 13), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1575, 81, 13, 11), new Vector2(3f, 7f));
            break;
          case AnimalType.Meerkat:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(0, 437, 8, 7), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(925, 9, 7, 6), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1592, 136, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1602, 136, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(655, 0, 9, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1612, 136, 8, 7), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1156, 125, 7, 6), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1549, 93, 8, 7), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1621, 137, 8, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1630, 136, 8, 7), new Vector2(2f, 5f));
            break;
          case AnimalType.Horse:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(532, 103, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1605, 144, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1604, 65, 9, 9), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(655, 7, 8, 9), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1516, 125, 7, 9), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1615, 144, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1625, 144, 9, 10), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1584, 124, 8, 11), new Vector2(1f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1635, 144, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1270, 51, 9, 13), new Vector2(1f, 6f));
            break;
          case AnimalType.Armadillo:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(41, 432, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(881, 90, 6, 5), new Vector2(1f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1645, 144, 11, 10), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1571, 114, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1582, 114, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1593, 126, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1589, 75, 9, 8), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1599, 75, 9, 8), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1558, 95, 10, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1589, 84, 9, 8), new Vector2(2f, 5f));
            break;
          case AnimalType.Donkey:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(532, 114, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1593, 114, 9, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1571, 103, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1603, 114, 9, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1613, 114, 9, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1605, 126, 8, 9), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1581, 103, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1591, 103, 9, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1636, 29, 9, 12), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1649, 0, 13, 12), new Vector2(4f, 8f));
            break;
          case AnimalType.Cow:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(12, 438, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1663, 0, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1676, 0, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1689, 0, 12, 12), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1623, 114, 14, 11), new Vector2(5f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1649, 13, 16, 14), new Vector2(6f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1638, 114, 13, 11), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1652, 114, 14, 11), new Vector2(5f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1657, 144, 14, 10), new Vector2(5f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1639, 126, 22, 17), new Vector2(9f, 12f));
            break;
          case AnimalType.Tapir:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(230, 306, 13, 15), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1666, 13, 12, 13), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1646, 29, 13, 12), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1679, 13, 13, 14), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1702, 0, 13, 12), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1693, 13, 13, 14), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1662, 128, 13, 15), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1676, 128, 13, 15), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1667, 114, 13, 13), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1615, 45, 13, 11), new Vector2(6f, 7f));
            break;
          case AnimalType.Ostrich:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(669, 68, 7, 11), new Vector2(3f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1629, 43, 7, 13), new Vector2(4f, 12f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1660, 28, 6, 13), new Vector2(2f, 13f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1575, 93, 7, 9), new Vector2(3f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1583, 93, 9, 9), new Vector2(3f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1681, 116, 7, 11), new Vector2(3f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1716, 0, 7, 12), new Vector2(5f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1593, 93, 7, 9), new Vector2(3f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1637, 42, 7, 14), new Vector2(3f, 14f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1724, 0, 8, 12), new Vector2(4f, 12f));
            break;
          case AnimalType.Tortoise:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(41, 442, 8, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(122, 193, 5, 4), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(99, 193, 7, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(99, 186, 7, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1599, 84, 9, 7), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(400, 426, 6, 6), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(107, 186, 8, 6), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(986, 96, 6, 5), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1601, 92, 7, 5), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1601, 98, 7, 5), new Vector2(2f, 5f));
            break;
          case AnimalType.Chicken:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(50, 441, 6, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(979, 95, 6, 6), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(986, 88, 6, 7), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(979, 88, 6, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(979, 81, 5, 6), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1601, 104, 7, 7), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1569, 95, 5, 7), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1615, 57, 7, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(648, 136, 5, 5), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(648, 142, 5, 4), new Vector2(2f, 3f));
            break;
          case AnimalType.Camel:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(181, 420, 8, 8), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1609, 106, 7, 7), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1627, 126, 11, 9), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1609, 75, 9, 8), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1614, (int) sbyte.MaxValue, 11, 8), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1609, 84, 8, 8), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1672, 145, 10, 9), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1618, 84, 11, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1614, 65, 10, 9), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1609, 93, 8, 12), new Vector2(2f, 7f));
            break;
          case AnimalType.Penguin:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(190, 420, 9, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1617, 107, 9, 6), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1627, 107, 9, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1637, 107, 7, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1618, 101, 8, 5), new Vector2(4f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1627, 101, 9, 5), new Vector2(4f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1645, 107, 8, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1654, 107, 7, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1662, 107, 8, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1637, 101, 9, 5), new Vector2(5f, 4f));
            break;
          case AnimalType.Antelope:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(0, 454, 10, 12), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1671, 107, 7, 6), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1645, 42, 11, 13), new Vector2(5f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1683, 144, 10, 10), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1751, 0, 10, 12), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1667, 27, 12, 13), new Vector2(6f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1657, 42, 10, 13), new Vector2(4f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1680, 28, 10, 12), new Vector2(4f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1690, (int) sbyte.MaxValue, 11, 16), new Vector2(5f, 12f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1707, 13, 15, 13), new Vector2(7f, 9f));
            break;
          case AnimalType.Panther:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(11, 451, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1619, 75, 10, 8), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1625, 65, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1636, 65, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1689, 116, 9, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1648, 65, 10, 9), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1645, 56, 10, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1656, 56, 10, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1659, 65, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1671, 65, 11, 9), new Vector2(4f, 6f));
            break;
          case AnimalType.Seal:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(11, 472, 9, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1623, 57, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1647, 101, 8, 5), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1618, 93, 8, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1627, 92, 11, 8), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1639, 92, 9, 8), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1656, 101, 7, 5), new Vector2(2f, 1f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1633, 57, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1649, 93, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1694, 144, 11, 10), new Vector2(3f, 7f));
            break;
          case AnimalType.Wolf:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(0, 467, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1667, 55, 11, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1706, 144, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1679, 55, 9, 9), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1717, 144, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1728, 144, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1739, 144, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1750, 144, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1761, 144, 10, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1772, 144, 10, 10), new Vector2(3f, 7f));
            break;
          case AnimalType.Lemur:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(34, 450, 13, 10), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1679, 107, 10, 7), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1659, 93, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1690, 107, 10, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1630, 75, 10, 7), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1641, 75, 11, 7), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1664, 101, 7, 5), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1683, 65, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1653, 75, 11, 7), new Vector2(5f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1630, 83, 10, 8), new Vector2(4f, 6f));
            break;
          case AnimalType.Alpaca:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(11, 461, 9, 10), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1665, 75, 8, 7), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1689, 55, 10, 9), new Vector2(2f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1694, 65, 10, 9), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1674, 75, 8, 6), new Vector2(0.0f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1700, 55, 12, 9), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1699, 116, 9, 10), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1705, 65, 11, 9), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1713, 55, 10, 9), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1709, 116, 9, 10), new Vector2(2f, 8f));
            break;
          case AnimalType.KomodoDragon:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(200, 421, 10, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1641, 83, 11, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1683, 75, 10, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1691, 28, 10, 6), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1694, 75, 9, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1704, 75, 9, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1701, 107, 11, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1714, 75, 8, 6), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1723, 75, 9, 6), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1733, 75, 9, 6), new Vector2(1f, 4f));
            break;
          case AnimalType.Walrus:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(23, 450, 10, 9), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1713, 107, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1783, 144, 10, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1717, 65, 11, 9), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1729, 65, 10, 9), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1723, 107, 9, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1724, 55, 9, 9), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1705, 27, 9, 11), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1734, 55, 10, 9), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1653, 83, 10, 9), new Vector2(2f, 5f));
            break;
          case AnimalType.Orangutan:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(677, 67, 11, 12), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1762, 0, 12, 12), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1723, 13, 11, 14), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1664, 83, 9, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1735, 13, 11, 14), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1715, 28, 12, 11), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1775, 0, 11, 12), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1747, 13, 11, 14), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1787, 0, 10, 12), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1674, 82, 9, 10), new Vector2(3f, 7f));
            break;
          case AnimalType.PolarBear:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(543, 82, 12, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1740, 65, 11, 9), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1745, 55, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1684, 82, 12, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1752, 65, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1757, 55, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1697, 82, 11, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1733, 107, 10, 8), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1764, 65, 13, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1709, 82, 12, 10), new Vector2(3f, 6f));
            break;
          case AnimalType.Skunk:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(48, 449, 9, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1669, 93, 10, 7), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1680, 93, 8, 7), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1743, 75, 10, 6), new Vector2(3f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1689, 93, 9, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1699, 93, 9, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1744, 107, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1709, 93, 9, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1719, 93, 9, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1729, 93, 9, 7), new Vector2(2f, 4f));
            break;
          case AnimalType.Crocodile:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(57, 443, 13, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1702, (int) sbyte.MaxValue, 13, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1754, 75, 8, 6), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1702, 136, 13, 7), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1672, 101, 13, 5), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1687, 101, 12, 5), new Vector2(1f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1763, 75, 11, 6), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1754, 107, 13, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1768, 107, 14, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1783, 107, 13, 8), new Vector2(1f, 4f));
            break;
          case AnimalType.WildBoar:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(71, 443, 12, 13), new Vector2(2f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1798, 0, 10, 12), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1728, 28, 11, 11), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1722, 82, 16, 10), new Vector2(7f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1797, 107, 10, 8), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1668, 41, 14, 13), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1740, 28, 12, 11), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1753, 28, 13, 11), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1683, 41, 11, 13), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1695, 41, 11, 13), new Vector2(3f, 7f));
            break;
          case AnimalType.Peacock:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(556, 82, 7, 11), new Vector2(3f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1748, 41, 7, 13), new Vector2(3f, 13f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1829, 13, 6, 10), new Vector2(2f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1908, 64, 6, 11), new Vector2(2f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1909, 76, 7, 12), new Vector2(3f, 12f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1915, 64, 6, 11), new Vector2(2f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1922, 64, 7, 11), new Vector2(3f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1917, 76, 6, 13), new Vector2(2f, 12f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1930, 64, 7, 11), new Vector2(3f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1938, 64, 7, 11), new Vector2(3f, 11f));
            break;
          case AnimalType.Platypus:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(0, 478, 10, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1917, 55, 10, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1928, 55, 10, 8), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1998, 145, 10, 9), new Vector2(1f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1939, 55, 10, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1836, 13, 13, 10), new Vector2(1f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1924, 76, 11, 12), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1767, 24, 10, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1778, 24, 10, 8), new Vector2(1f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1789, 24, 10, 8), new Vector2(1f, 4f));
            break;
          case AnimalType.Deer:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(586, 82, 10, 10), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(2009, 145, 9, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1946, 64, 10, 11), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1756, 40, 10, 14), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1768, 42, 10, 13), new Vector2(4f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1833, 129, 13, 15), new Vector2(5f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1818, 91, 10, 14), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1829, 91, 10, 14), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1840, 91, 13, 15), new Vector2(6f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1854, 91, 19, 14), new Vector2(8f, 10f));
            break;
          case AnimalType.Monkey:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(556, 94, 12, 9), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1809, 99, 8, 6), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1767, 33, 9, 7), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1850, 13, 9, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1777, 33, 8, 9), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1800, 24, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1786, 33, 12, 9), new Vector2(5f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1810, 24, 9, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1820, 24, 12, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1779, 43, 13, 10), new Vector2(5f, 6f));
            break;
          case AnimalType.Flamingo:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(244, 306, 8, 11), new Vector2(5f, 11f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1793, 44, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1802, 44, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1799, 33, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1808, 33, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1817, 33, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1826, 33, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1833, 24, 8, 8), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1835, 33, 8, 10), new Vector2(4f, 10f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1860, 13, 8, 10), new Vector2(4f, 10f));
            break;
          case AnimalType.Gorilla:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(47, 457, 10, 12), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1811, 44, 10, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1936, 76, 9, 12), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1822, 44, 10, 10), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1946, 76, 10, 13), new Vector2(3f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1957, 76, 10, 12), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1957, 64, 10, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1833, 44, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1982, 0, 11, 13), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1842, 43, 9, 11), new Vector2(2f, 6f));
            break;
          case AnimalType.Tiger:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(597, 82, 10, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1869, 13, 10, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1880, 13, 10, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1891, 13, 12, 10), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1842, 24, 9, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1844, 33, 10, 9), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1904, 13, 12, 10), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1917, 13, 12, 10), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1847, 131, 11, 13), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1930, 13, 12, 10), new Vector2(4f, 6f));
            break;
          case AnimalType.Kangaroo:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(532, 125, 9, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1855, 33, 9, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1852, 24, 8, 8), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1865, 33, 9, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1861, 24, 9, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1871, 24, 9, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1875, 33, 9, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1885, 33, 9, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1881, 24, 9, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1895, 33, 9, 9), new Vector2(3f, 6f));
            break;
          case AnimalType.Beavers:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(532, 135, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1849, 57, 8, 6), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1858, 55, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1868, 56, 8, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1877, 55, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1887, 55, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1897, 55, 9, 8), new Vector2(2f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1987, 145, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1809, 91, 8, 7), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1907, 55, 9, 8), new Vector2(2f, 5f));
            break;
          case AnimalType.RedPanda:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(569, 94, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1895, 64, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1789, 55, 11, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1779, 93, 9, 6), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1801, 55, 11, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1813, 57, 9, 6), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1823, 55, 13, 8), new Vector2(5f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1837, 55, 11, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1963, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1975, 145, 11, 9), new Vector2(3f, 6f));
            break;
          case AnimalType.Zebra:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(608, 82, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1768, 13, 8, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1821, 130, 11, 12), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1759, 13, 8, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1777, 13, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1786, 13, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1795, 13, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1804, 13, 7, 10), new Vector2(1f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1812, 13, 8, 10), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1821, 13, 7, 10), new Vector2(1f, 6f));
            break;
          case AnimalType.Fox:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(581, 94, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1889, 117, 11, 10), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1806, 131, 14, 11), new Vector2(6f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1888, 107, 12, 9), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1901, 117, 11, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1901, 107, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1860, 64, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1872, 64, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1883, 64, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1951, 144, 11, 10), new Vector2(4f, 7f));
            break;
          case AnimalType.Raccoon:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(592, 93, 11, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1778, 55, 10, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1790, 64, 10, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1877, 117, 11, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1876, 107, 11, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1801, 64, 10, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1812, 64, 11, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1824, 64, 11, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1836, 64, 11, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1848, 64, 11, 9), new Vector2(3f, 7f));
            break;
          case AnimalType.Elephant:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(254, 300, 17, 16), new Vector2(6f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1806, 74, 17, 16), new Vector2(6f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1773, 128, 14, 15), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1824, 74, 18, 16), new Vector2(7f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1788, 128, 17, 15), new Vector2(6f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1843, 74, 17, 16), new Vector2(6f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1790, 92, 18, 14), new Vector2(8f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1861, 74, 15, 16), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1877, 74, 15, 16), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1893, 74, 15, 16), new Vector2(3f, 8f));
            break;
          case AnimalType.Cheetah:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(604, 93, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1855, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1867, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1778, 65, 11, 8), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1879, 144, 11, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1891, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1903, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1915, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1927, 145, 11, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1939, 145, 11, 9), new Vector2(3f, 6f));
            break;
          case AnimalType.Otter:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(211, 421, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1775, 74, 11, 7), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1787, 74, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1761, 100, 9, 6), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1797, 74, 8, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1763, 128, 9, 7), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1771, 100, 9, 6), new Vector2(3f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1781, 100, 8, 6), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1699, 101, 7, 5), new Vector2(2f, 3f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1763, 136, 8, 7), new Vector2(2f, 4f));
            break;
          case AnimalType.Owl:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(617, 82, 11, 10), new Vector2(4f, 9f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1819, 107, 9, 8), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1829, 107, 12, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1833, 145, 9, 9), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1843, 145, 11, 9), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1842, 107, 10, 8), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1762, 92, 7, 7), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1770, 92, 8, 7), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1853, 107, 12, 8), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1866, 107, 9, 8), new Vector2(4f, 7f));
            break;
          case AnimalType.Rhino:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(58, 452, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1865, 116, 11, 11), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1892, 0, 11, 12), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1904, 0, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1817, 143, 15, 11), new Vector2(6f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1917, 0, 13, 12), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1931, 0, 11, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1943, 0, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1956, 0, 12, 12), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1969, 0, 12, 12), new Vector2(4f, 7f));
            break;
          case AnimalType.Panda:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(58, 465, 12, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1801, 116, 12, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1795, 82, 9, 9), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1865, 0, 13, 12), new Vector2(4f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1879, 0, 12, 12), new Vector2(2f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1806, 144, 10, 10), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1814, 116, 13, 11), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1828, 116, 11, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1840, 116, 12, 11), new Vector2(4f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1853, 116, 11, 11), new Vector2(2f, 7f));
            break;
          case AnimalType.Giraffe:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(47, 470, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1739, 82, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1750, 82, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1761, 82, 10, 9), new Vector2(3f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1794, 144, 11, 10), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1772, 82, 10, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1783, 82, 11, 9), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1739, 92, 10, 8), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1735, 41, 12, 13), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1750, 92, 11, 8), new Vector2(4f, 6f));
            break;
          case AnimalType.Hippopotamus:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(71, 457, 12, 12), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1742, 116, 9, 9), new Vector2(3f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1823, 0, 14, 12), new Vector2(4f, 6f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1752, 116, 11, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1769, 56, 8, 8), new Vector2(2f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1764, 116, 11, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1838, 0, 13, 12), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1776, 116, 12, 11), new Vector2(3f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1789, 116, 11, 11), new Vector2(2f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1852, 0, 12, 12), new Vector2(3f, 7f));
            break;
          case AnimalType.Lion:
            GeneData.HeadsInfo[(int) HeadAnimal] = new GeneDrawHeadInfo(new Rectangle(243, 318, 13, 14), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(1, new Rectangle(1719, 116, 11, 9), new Vector2(4f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(2, new Rectangle(1808, 107, 10, 8), new Vector2(4f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(3, new Rectangle(1716, 126, 15, 17), new Vector2(7f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(4, new Rectangle(1732, 126, 15, 17), new Vector2(5f, 8f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(5, new Rectangle(1731, 116, 10, 9), new Vector2(4f, 4f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(6, new Rectangle(1707, 40, 13, 14), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(7, new Rectangle(1809, 0, 13, 12), new Vector2(5f, 5f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(8, new Rectangle(1748, 128, 14, 15), new Vector2(5f, 7f));
            GeneData.HeadsInfo[(int) HeadAnimal].AddHead(9, new Rectangle(1721, 40, 13, 14), new Vector2(5f, 7f));
            break;
        }
      }
      GeneData.HeadsInfo[(int) HeadAnimal].GetHead(out HeadRect, out HeadOrigin, HeadVariant);
    }

    internal static Rectangle GetHeadRect(AnimalType animal, int Variant)
    {
      Vector2 HeadOrigin;
      if (GeneData.HeadsInfo == null || GeneData.HeadsInfo[(int) animal] == null)
      {
        Rectangle HeadRect;
        GeneData.GetHybrid(animal, animal, out Rectangle _, out Vector2 _, out HeadRect, out HeadOrigin, Variant, Variant);
        return HeadRect;
      }
      Rectangle HeadRect1;
      GeneData.HeadsInfo[(int) animal].GetHead(out HeadRect1, out HeadOrigin, Variant);
      return HeadRect1;
    }
  }
}
