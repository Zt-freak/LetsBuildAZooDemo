// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Corpse.CorpseData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_AnimalsAndPeople.Corpse
{
  internal class CorpseData
  {
    internal static Vector3 GetCorpseInfo(
      AnimalType animal,
      out CorpseSize corpsesize,
      int Variant)
    {
      corpsesize = CorpseSize.Medium;
      switch (animal)
      {
        case AnimalType.Rabbit:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(228f, 229f, 211f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(194f, 143f, 100f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(211f, 135f, 95f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(66f, 84f, 84f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(247f, 236f, 217f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(129f, 134f, 102f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(214f, 166f, 100f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(186f, 122f, 108f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(104f, 88f, 71f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(129f, 134f, 137f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Goose:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(198f, 80f, 39f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(211f, 135f, 95f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(90f, 86f, 65f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(145f, 81f, 40f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(152f, 93f, 57f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(163f, 159f, 137f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(126f, 122f, 97f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Capybara:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(196f, 125f, 45f) / (float) byte.MaxValue;
            case 1:
              return new Vector3((float) sbyte.MaxValue, 123f, 98f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(199f, 154f, 93f) / (float) byte.MaxValue;
            case 3:
              return new Vector3((float) sbyte.MaxValue, 123f, 98f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(199f, 154f, 93f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(232f, 229f, 204f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(196f, 125f, 45f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(160f, 80f, 58f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(196f, 125f, 45f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(196f, 125f, 45f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Pig:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(236f, 198f, 194f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(236f, 198f, 194f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(142f, 140f, 129f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(203f, 163f, 106f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(228f, 110f, 46f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(236f, 198f, 194f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(94f, 104f, 98f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(191f, 150f, 90f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(80f, 87f, 88f) / (float) byte.MaxValue;
            case 9:
              return new Vector3((float) byte.MaxValue, 244f, 94f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Duck:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(162f, 112f, 48f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(231f, 241f, 242f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(246f, 163f, 51f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(219f, 108f, 48f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(102f, 38f, 18f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(215f, 215f, 215f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(99f, 71f, 55f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(51f, 66f, 135f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(159f, 75f, 30f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Snake:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(99f, 166f, 41f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(168f, 139f, 85f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(159f, 39f, 39f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(143f, 110f, 85f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(40f, 112f, 74f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(41f, 104f, 120f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(85f, 85f, 92f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(114f, 166f, 41f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(229f, 211f, 72f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(201f, 89f, 29f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Badger:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(151f, 149f, 137f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(168f, 144f, 115f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(91f, 90f, 82f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(140f, 117f, 84f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(170f, 106f, 44f) / (float) byte.MaxValue;
            case 5:
              return new Vector3((float) byte.MaxValue, 248f, 248f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(139f, 77f, 29f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(128f, 124f, 103f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(182f, 162f, 140f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(97f, 96f, 91f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Hyena:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(141f, 118f, 85f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(232f, 219f, 199f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(97f, 47f, 21f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(169f, 122f, 52f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(168f, 145f, 105f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(161f, 129f, 83f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(189f, 113f, 31f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(239f, 235f, 226f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(149f, 97f, 54f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(135f, 97f, 67f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Porcupine:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(126f, 82f, 58f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(65f, 60f, 58f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(88f, 70f, 7f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(149f, 151f, 63f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(82f, 68f, 60f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(55f, 55f, 55f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(86f, 77f, 69f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(123f, 96f, 77f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(151f, 90f, 49f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(223f, 146f, 64f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Bear:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(162f, 112f, 48f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(63f, 59f, 57f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(186f, 202f, 208f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(162f, 112f, 48f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(52f, 45f, 42f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(66f, 61f, 58f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(232f, 214f, 192f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(90f, 91f, 105f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(101f, 94f, 88f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(75f, 58f, 37f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Meerkat:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(199f, 154f, 93f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(193f, 135f, 72f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(230f, 146f, 52f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(173f, 146f, 84f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(190f, 110f, 85f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(238f, 220f, 142f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(148f, 84f, 47f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(226f, 152f, 49f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(224f, 179f, 117f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(245f, 230f, 209f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Horse:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(181f, 125f, 54f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(231f, 186f, 132f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(184f, 91f, 51f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(254f, 215f, 163f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(244f, 219f, 189f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(55f, 49f, 41f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(223f, 212f, 164f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(63f, 60f, 57f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(82f, 82f, 94f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(114f, 86f, 54f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Armadillo:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(177f, 131f, 92f) / (float) byte.MaxValue;
            case 1:
              return new Vector3((float) byte.MaxValue, 208f, 181f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(143f, 145f, 148f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(229f, 223f, 188f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(90f, 104f, 126f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(146f, 90f, 54f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(241f, 196f, 111f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(141f, 181f, 78f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(142f, 84f, 46f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(150f, 150f, 79f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Donkey:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(185f, 174f, 144f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(96f, 91f, 75f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(138f, 101f, 57f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(199f, 152f, 104f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(209f, 202f, 183f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(246f, 244f, 238f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(199f, 152f, 104f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(101f, 69f, 27f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(158f, 170f, 169f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(147f, 74f, 34f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Cow:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(246f, 244f, 238f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(160f, 70f, 21f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(234f, 205f, 163f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(188f, 139f, 66f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(205f, 183f, 150f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(146f, 92f, 19f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(160f, 70f, 21f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(80f, 87f, 88f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(246f, 244f, 238f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(160f, 70f, 21f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Tapir:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(111f, 109f, 94f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(119f, 106f, 89f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(111f, 109f, 94f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(183f, 156f, 132f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(98f, 111f, 113f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(111f, 109f, 94f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(95f, 86f, 71f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(108f, 83f, 49f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(102f, 102f, 102f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Ostrich:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(100f, 98f, 82f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(100f, 98f, 82f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(123f, 105f, 84f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(82f, 80f, 66f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(88f, 98f, 118f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(135f, 114f, 87f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(67f, 78f, 77f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(186f, 92f, 64f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(100f, 98f, 82f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Tortoise:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(187f, 170f, 97f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(92f, 67f, 43f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(102f, 61f, 22f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(63f, 64f, 58f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(99f, 77f, 46f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(87f, 108f, 112f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(104f, 107f, 84f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(44f, 44f, 44f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(133f, 126f, 78f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(25f, 60f, 65f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Chicken:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(177f, 116f, 22f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(177f, 116f, 22f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(97f, 104f, 97f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(101f, 101f, 101f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(167f, 107f, 14f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(97f, 104f, 97f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Camel:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(232f, 193f, 47f) / (float) byte.MaxValue;
            case 1:
              corpsesize = CorpseSize.Small;
              return new Vector3(189f, 148f, 97f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(189f, 176f, 97f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(223f, 153f, 55f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(233f, 204f, 151f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(80f, 87f, 88f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(219f, 214f, 189f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(179f, 77f, 41f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(129f, 131f, 108f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(135f, 121f, 122f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Penguin:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(78f, 76f, 61f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(73f, 72f, 64f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(64f, 63f, 55f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(85f, 84f, 65f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(51f, 69f, 103f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(67f, 67f, 60f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(61f, 61f, 54f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(51f, 51f, 46f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(59f, 50f, 29f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(84f, 162f, 186f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Antelope:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(244f, 141f, 8f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(221f, 120f, 0.0f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(218f, 125f, 25f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(233f, 177f, 103f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(204f, 178f, 117f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(194f, 177f, 162f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(116f, 62f, 0.0f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(210f, 106f, 36f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(98f, 94f, 88f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(82f, 82f, 82f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Panther:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(101f, 98f, 81f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(89f, 87f, 73f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(70f, 69f, 61f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(132f, 83f, 28f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(78f, 75f, 59f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(97f, 93f, 86f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(101f, 89f, 81f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(84f, 104f, 98f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(101f, 98f, 81f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(101f, 98f, 81f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Seal:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(128f, 152f, 183f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(98f, 71f, 30f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(196f, 120f, 53f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(112f, 99f, 77f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(107f, 106f, 104f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(92f, 73f, 45f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(90f, 87f, 82f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(218f, 212f, 191f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(56f, 56f, 56f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(74f, 77f, 77f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Wolf:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(124f, 119f, 84f) / (float) byte.MaxValue;
            case 1:
              return new Vector3((float) sbyte.MaxValue, 121f, 85f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(98f, 112f, 115f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(193f, 111f, 66f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(116f, 111f, 78f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(144f, 117f, 80f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(80f, 87f, 88f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(237f, 235f, 220f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(244f, 249f, 250f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(193f, 161f, 70f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Lemur:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(171f, 166f, 132f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(163f, 147f, 132f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(137f, 111f, 77f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(76f, 76f, 76f) / (float) byte.MaxValue;
            case 4:
              return new Vector3((float) byte.MaxValue, 248f, 235f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(82f, 77f, 74f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(181f, 131f, 68f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(198f, 93f, 37f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(249f, 205f, 122f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(245f, 236f, 236f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Alpaca:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(214f, 197f, 177f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(192f, 154f, 114f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(162f, 98f, 76f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(134f, 78f, 67f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(199f, 152f, 104f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(89f, 76f, 52f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(241f, 211f, 136f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(196f, 107f, 45f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.KomodoDragon:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(173f, 90f, 117f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(182f, 102f, 40f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(101f, 128f, 31f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(165f, 101f, 52f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(140f, 75f, 42f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(59f, 58f, 68f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(115f, 165f, 65f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(213f, 181f, 98f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(170f, 93f, 93f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(161f, 74f, 33f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Walrus:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(148f, 146f, 103f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(148f, 135f, 103f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(137f, 119f, 113f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(107f, 106f, 104f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(246f, 244f, 238f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(136f, 135f, 114f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(142f, 132f, 109f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(96f, 96f, 124f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(74f, 77f, 77f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(96f, 91f, 75f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Orangutan:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(239f, 96f, 0.0f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(208f, 89f, 7f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(213f, 61f, 19f) / (float) byte.MaxValue;
            case 3:
              corpsesize = CorpseSize.Small;
              return new Vector3(239f, 96f, 0.0f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(239f, 96f, 0.0f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(208f, 83f, 33f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(248f, 116f, 44f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(239f, 96f, 0.0f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(203f, 79f, 11f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(204f, 77f, 0.0f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.PolarBear:
          corpsesize = CorpseSize.Big;
          return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
        case AnimalType.Skunk:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(112f, 108f, 91f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(107f, 107f, 107f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(132f, 86f, 36f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(103f, 103f, 97f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(104f, 104f, 94f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(73f, 73f, 73f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(70f, 71f, 74f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(49f, 46f, 42f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Crocodile:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(57f, 172f, 55f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(135f, 142f, 99f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(64f, 138f, 64f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(131f, 155f, 32f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(163f, 184f, 55f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(184f, 121f, 72f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(107f, 117f, 59f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(100f, 112f, 112f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(136f, 163f, 67f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(219f, 220f, 205f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.WildBoar:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(183f, 103f, 64f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(112f, 135f, 133f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(203f, 163f, 106f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(227f, 140f, 26f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(175f, 96f, 57f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(131f, 93f, 75f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(151f, 76f, 45f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(110f, 124f, 133f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(76f, 80f, 91f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(219f, 187f, 139f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Peacock:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(75f, 110f, 189f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(54f, 134f, 179f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(48f, 79f, 134f) / (float) byte.MaxValue;
            case 3:
              return new Vector3((float) byte.MaxValue, (float) byte.MaxValue, (float) byte.MaxValue) / (float) byte.MaxValue;
            case 4:
              return new Vector3((float) byte.MaxValue, 247f, 229f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(154f, 205f, 185f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(116f, 77f, 34f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(75f, 189f, 185f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(48f, 184f, 103f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Platypus:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(172f, 129f, 71f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(194f, 170f, 68f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(196f, 162f, 133f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(173f, 143f, 92f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(168f, 155f, 135f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(172f, 129f, 71f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(123f, 87f, 47f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(158f, 178f, 112f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(160f, 107f, 101f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(89f, 132f, 94f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Deer:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(181f, 125f, 54f) / (float) byte.MaxValue;
            case 1:
              corpsesize = CorpseSize.Small;
              return new Vector3(165f, 101f, 23f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(199f, 154f, 93f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(181f, 125f, 54f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(196f, 107f, 45f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(242f, 230f, 212f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(177f, 111f, 77f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(89f, 76f, 52f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(114f, 98f, 67f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(181f, 125f, 54f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Monkey:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(155f, 81f, 41f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(123f, 89f, 52f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(77f, 74f, 71f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(80f, 77f, 77f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(186f, 96f, 72f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(232f, 134f, 53f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(189f, 186f, 178f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(66f, 62f, 55f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(87f, 97f, 98f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(91f, 91f, 91f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Flamingo:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3((float) byte.MaxValue, 193f, 208f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(252f, 218f, 228f) / (float) byte.MaxValue;
            case 2:
              return new Vector3((float) byte.MaxValue, 193f, 208f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 5:
              return new Vector3((float) byte.MaxValue, 165f, 141f) / (float) byte.MaxValue;
            case 6:
              return new Vector3((float) byte.MaxValue, 139f, 139f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(252f, 218f, 228f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(114f, 111f, 92f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Gorilla:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(113f, 93f, 82f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(95f, 99f, 106f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(93f, 104f, 108f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(113f, 93f, 82f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(125f, 108f, 101f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(119f, 80f, 59f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(64f, 54f, 49f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(85f, 94f, 98f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(113f, 93f, 82f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(134f, 100f, 81f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Tiger:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(247f, 189f, 77f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(233f, 215f, 145f) / (float) byte.MaxValue;
            case 2:
              return new Vector3((float) byte.MaxValue, 177f, 76f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(155f, 76f, 0.0f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(240f, 176f, 57f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(254f, 253f, 252f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(254f, 253f, 252f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(239f, 199f, 123f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(233f, 133f, 26f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(188f, 147f, 135f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Kangaroo:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(199f, 154f, 93f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(208f, 129f, 84f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(177f, 150f, 115f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(161f, 148f, 131f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(161f, 148f, 131f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(123f, 81f, 46f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(107f, 110f, 110f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(199f, 154f, 93f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(161f, 148f, 131f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(246f, 244f, 238f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Beavers:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(172f, 129f, 71f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(158f, 104f, 41f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(163f, 100f, 57f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(146f, 103f, 45f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(211f, (float) sbyte.MaxValue, 58f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(241f, 226f, 215f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(145f, 87f, 29f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(202f, 149f, 78f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(135f, 99f, 49f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(144f, 89f, 36f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.RedPanda:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(200f, 99f, 67f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(225f, 156f, 64f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(106f, 41f, 31f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(156f, 71f, 31f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(200f, 99f, 67f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(241f, 152f, 109f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(189f, 100f, 97f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(209f, 83f, 33f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(225f, 156f, 64f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(176f, 83f, 68f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Zebra:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(141f, 137f, 111f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(105f, 99f, 72f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(248f, 232f, 203f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(208f, 181f, 147f) / (float) byte.MaxValue;
            case 8:
              return new Vector3((float) byte.MaxValue, 243f, 234f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(220f, 135f, 72f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Fox:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(253f, 132f, 0.0f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(199f, 160f, 88f) / (float) byte.MaxValue;
            case 2:
              return new Vector3((float) byte.MaxValue, 178f, 67f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(246f, 189f, 132f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(246f, 238f, 216f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(234f, 161f, 51f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(246f, 253f, (float) byte.MaxValue) / (float) byte.MaxValue;
            case 7:
              return new Vector3(141f, 141f, 141f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(104f, 104f, 104f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(205f, (float) sbyte.MaxValue, 10f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Raccoon:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(173f, 157f, 138f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(176f, 162f, 134f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(194f, 182f, 168f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(241f, 238f, 227f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(188f, 107f, 79f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(176f, 162f, 134f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(116f, 105f, 95f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(116f, 105f, 95f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(222f, 181f, 133f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(97f, 105f, 113f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Elephant:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(171f, 166f, 132f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(156f, 136f, 87f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(113f, 118f, 130f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(138f, 165f, 156f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(154f, 153f, 148f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(150f, 133f, 170f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(213f, 206f, 172f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(152f, 122f, 105f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(171f, 166f, 132f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(180f, 103f, 62f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Cheetah:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(247f, 218f, 89f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(225f, 208f, 149f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(202f, 149f, 67f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(234f, 229f, 192f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(228f, 202f, 118f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(247f, 218f, 89f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(231f, 204f, 143f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(230f, 224f, 147f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(247f, 218f, 89f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(206f, 192f, 130f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Otter:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(172f, 129f, 71f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(94f, 104f, 98f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(115f, 65f, 51f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(129f, 90f, 90f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(109f, 75f, 58f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(152f, 133f, 117f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(119f, 77f, 63f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(116f, 79f, 26f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(170f, 109f, 75f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(232f, 205f, 168f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Owl:
          corpsesize = CorpseSize.Small;
          switch (Variant)
          {
            case 0:
              return new Vector3(142f, 128f, 110f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(181f, 168f, 139f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(138f, 96f, 69f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(137f, 134f, 129f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(205f, 151f, 91f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(181f, 168f, 139f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(124f, 67f, 57f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(137f, 134f, 129f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(58f, 51f, 36f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Rhino:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(171f, 166f, 132f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(141f, 161f, 151f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(150f, 135f, 110f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(128f, 150f, 183f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(166f, 135f, 139f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(191f, 117f, 105f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(109f, 106f, 90f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(207f, 202f, 163f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(138f, 101f, 57f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(232f, 229f, 204f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Panda:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(206f, 182f, 154f) / (float) byte.MaxValue;
            case 2:
              corpsesize = CorpseSize.Small;
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(214f, 191f, 172f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(251f, 247f, 238f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Giraffe:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(243f, 219f, 93f) / (float) byte.MaxValue;
            case 1:
              return new Vector3(238f, 225f, 172f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(221f, 212f, 158f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(236f, 213f, 143f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(243f, 235f, 189f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(243f, 219f, 93f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(243f, 185f, 69f) / (float) byte.MaxValue;
            case 7:
              return new Vector3((float) byte.MaxValue, 238f, 203f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(241f, 230f, 192f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(236f, 213f, 143f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Hippopotamus:
          corpsesize = CorpseSize.Big;
          switch (Variant)
          {
            case 0:
              return new Vector3(162f, 121f, 151f) / (float) byte.MaxValue;
            case 1:
              corpsesize = CorpseSize.Medium;
              return new Vector3(146f, 131f, 152f) / (float) byte.MaxValue;
            case 2:
              return new Vector3(132f, 129f, 154f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(143f, 135f, 132f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(128f, 111f, 103f) / (float) byte.MaxValue;
            case 5:
              return new Vector3(169f, 146f, 119f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(160f, 134f, 120f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(94f, 104f, 98f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(124f, 142f, 158f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(253f, 252f, 252f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        case AnimalType.Lion:
          corpsesize = CorpseSize.Medium;
          switch (Variant)
          {
            case 0:
              return new Vector3(240f, 194f, 101f) / (float) byte.MaxValue;
            case 1:
              return new Vector3((float) byte.MaxValue, 171f, 102f) / (float) byte.MaxValue;
            case 2:
              corpsesize = CorpseSize.Small;
              return new Vector3((float) byte.MaxValue, 204f, 102f) / (float) byte.MaxValue;
            case 3:
              return new Vector3(142f, 87f, 13f) / (float) byte.MaxValue;
            case 4:
              return new Vector3(176f, 119f, 30f) / (float) byte.MaxValue;
            case 5:
              corpsesize = CorpseSize.Small;
              return new Vector3((float) byte.MaxValue, 204f, 102f) / (float) byte.MaxValue;
            case 6:
              return new Vector3(250f, 224f, 171f) / (float) byte.MaxValue;
            case 7:
              return new Vector3(224f, 136f, 68f) / (float) byte.MaxValue;
            case 8:
              return new Vector3(173f, 78f, 41f) / (float) byte.MaxValue;
            case 9:
              return new Vector3(67f, 57f, 53f) / (float) byte.MaxValue;
            default:
              throw new Exception("MISSED VARIANT");
          }
        default:
          throw new Exception("NO WAY");
      }
    }
  }
}
