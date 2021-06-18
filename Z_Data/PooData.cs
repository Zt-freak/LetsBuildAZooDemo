// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Data.PooData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_Data
{
  internal class PooData
  {
    internal static Rectangle GetAnimalToPoopRectangle(AnimalType animaltype)
    {
      switch (animaltype)
      {
        case AnimalType.Rabbit:
          return new Rectangle(1899, 347, 12, 7);
        case AnimalType.Goose:
          return new Rectangle(42, 298, 8, 5);
        case AnimalType.Capybara:
          return new Rectangle(33, 305, 8, 7);
        case AnimalType.Pig:
          return new Rectangle(23, 298, 8, 7);
        case AnimalType.Duck:
          return new Rectangle(859, 88, 13, 7);
        case AnimalType.Snake:
          return new Rectangle(404, 452, 7, 6);
        case AnimalType.Badger:
          return new Rectangle(404, 459, 8, 7);
        case AnimalType.Hyena:
          return new Rectangle(458, 565, 10, 8);
        case AnimalType.Porcupine:
          return new Rectangle(469, 562, 10, 6);
        case AnimalType.Bear:
          return new Rectangle(51, 298, 9, 8);
        case AnimalType.Meerkat:
          return new Rectangle(42, 305, 8, 6);
        case AnimalType.Horse:
          return new Rectangle(124, 272, 10, 6);
        case AnimalType.Armadillo:
          return new Rectangle(51, 307, 9, 5);
        case AnimalType.Donkey:
          return new Rectangle(469, 555, 10, 6);
        case AnimalType.Cow:
          return new Rectangle(458, 555, 10, 9);
        case AnimalType.Tapir:
          return new Rectangle(287, 370, 9, 7);
        case AnimalType.Ostrich:
          return new Rectangle(531, 167, 10, 6);
        case AnimalType.Tortoise:
          return new Rectangle(287, 378, 8, 5);
        case AnimalType.Chicken:
          return new Rectangle(405, 529, 11, 5);
        case AnimalType.Camel:
          return new Rectangle(732, 64, 10, 6);
        case AnimalType.Penguin:
          return new Rectangle(403, 317, 13, 10);
        case AnimalType.Antelope:
          return new Rectangle(417, 529, 10, 5);
        case AnimalType.Panther:
          return new Rectangle(428, 529, 10, 5);
        case AnimalType.Seal:
          return new Rectangle(439, 529, 8, 5);
        case AnimalType.Wolf:
          return new Rectangle(51, 313, 9, 7);
        case AnimalType.Lemur:
          return new Rectangle(287, 384, 9, 6);
        case AnimalType.Alpaca:
          return new Rectangle(448, 529, 11, 5);
        case AnimalType.KomodoDragon:
          return new Rectangle(616, 93, 12, 8);
        case AnimalType.Walrus:
          return new Rectangle(607, 103, 10, 5);
        case AnimalType.Orangutan:
          return new Rectangle(41, 311, 9, 7);
        case AnimalType.PolarBear:
          return new Rectangle(731, 71, 8, 7);
        case AnimalType.Skunk:
          return new Rectangle(170, 96, 9, 5);
        case AnimalType.Crocodile:
          return new Rectangle(32, 313, 8, 5);
        case AnimalType.WildBoar:
          return new Rectangle(170, 64, 9, 6);
        case AnimalType.Peacock:
          return new Rectangle(287, 391, 9, 7);
        case AnimalType.Platypus:
          return new Rectangle(287, 399, 9, 6);
        case AnimalType.Deer:
          return new Rectangle(381, 509, 12, 4);
        case AnimalType.Monkey:
          return new Rectangle(404, 467, 8, 5);
        case AnimalType.Flamingo:
          return new Rectangle(58, 485, 12, 8);
        case AnimalType.Gorilla:
          return new Rectangle((int) sbyte.MaxValue, 449, 10, 5);
        case AnimalType.Tiger:
          return new Rectangle(297, 392, 8, 8);
        case AnimalType.Kangaroo:
          return new Rectangle(763, 79, 12, 6);
        case AnimalType.Beavers:
          return new Rectangle(460, 529, 12, 5);
        case AnimalType.RedPanda:
          return new Rectangle(618, 102, 10, 6);
        case AnimalType.Zebra:
          return new Rectangle(23, 306, 9, 7);
        case AnimalType.Fox:
          return new Rectangle(233, 391, 7, 5);
        case AnimalType.Raccoon:
          return new Rectangle(763, 71, 10, 7);
        case AnimalType.Elephant:
          return new Rectangle(417, 318, 12, 11);
        case AnimalType.Cheetah:
          return new Rectangle(287, 406, 9, 6);
        case AnimalType.Otter:
          return new Rectangle(32, 298, 9, 6);
        case AnimalType.Owl:
          return new Rectangle(80, 295, 8, 5);
        case AnimalType.Rhino:
          return new Rectangle(430, 319, 12, 10);
        case AnimalType.Panda:
          return new Rectangle(93, 642, 11, 9);
        case AnimalType.Giraffe:
          return new Rectangle(1180, 126, 12, 5);
        case AnimalType.Hippopotamus:
          return new Rectangle(801, 88, 14, 13);
        case AnimalType.Lion:
          return new Rectangle(816, 87, 11, 6);
        default:
          return new Rectangle(0, 0, 1, 1);
      }
    }
  }
}
