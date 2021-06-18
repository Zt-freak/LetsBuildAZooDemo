// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments.PeopleAttachmentData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments
{
  internal class PeopleAttachmentData
  {
    internal static AttachmentInfo[] attachmententries;

    internal static AttachmentInfo GetAttachInfo(PersonAttachementType attachementtype)
    {
      if (PeopleAttachmentData.attachmententries == null)
        PeopleAttachmentData.attachmententries = new AttachmentInfo[39];
      if (PeopleAttachmentData.attachmententries[(int) attachementtype] == null)
      {
        switch (attachementtype)
        {
          case PersonAttachementType.HotDog:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.HotDog, AttachmentLocation.LeftHand, new Rectangle(406, 413, 6, 6), new Rectangle(406, 413, 6, 6), new Vector2(-2f, 8f), new Vector2(-1f, 8f), new Vector2(5f, 8f), new Vector2(8f, 8f));
            break;
          case PersonAttachementType.RedBalloon:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.RedBalloon, AttachmentLocation.RightHand, new Rectangle(308, 452, 11, 22), new Rectangle(57, 595, 10, 21), new Vector2(11f, 24f), new Vector2(4f, 26f), new Vector2(4f, 24f), new Vector2(11f, 24f), false);
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(356, 452, 11, 22));
            PeopleAttachmentData.attachmententries[(int) attachementtype].HasIdle = true;
            break;
          case PersonAttachementType.Crisps:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Crisps, AttachmentLocation.LeftHand, new Rectangle(142, 449, 4, 5), new Rectangle(138, 449, 3, 5), new Vector2(-2f, 8f), new Vector2(-1f, 8f), new Vector2(3f, 9f), new Vector2(7f, 8f));
            break;
          case PersonAttachementType.Cola:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Cola, AttachmentLocation.LeftHand, new Rectangle(147, 448, 4, 6), new Rectangle(147, 448, 4, 6), new Vector2(-2f, 8f), new Vector2(-1f, 9f), new Vector2(3f, 8f), new Vector2(6f, 8f));
            break;
          case PersonAttachementType.Churros:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Churros, AttachmentLocation.LeftHand, new Rectangle(100, 304, 6, 6), new Rectangle(100, 304, 6, 6), new Vector2(-2f, 8f), new Vector2(0.0f, 9f), new Vector2(3f, 8f), new Vector2(8f, 9f));
            break;
          case PersonAttachementType.SingleScoop:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.SingleScoop, AttachmentLocation.LeftHand, new Rectangle(60, 514, 4, 7), new Rectangle(60, 514, 4, 7), new Vector2(-2f, 8f), new Vector2(-1f, 9f), new Vector2(5f, 9f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.SnowCone:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.SnowCone, AttachmentLocation.LeftHand, new Rectangle(221, 421, 4, 7), new Rectangle(221, 421, 4, 7), new Vector2(-2f, 8f), new Vector2(-1f, 9f), new Vector2(4f, 9f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Popsicle:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Popsicle, AttachmentLocation.LeftHand, new Rectangle(65, 514, 4, 6), new Rectangle(95, 304, 4, 6), new Vector2(-2f, 10f), new Vector2(-1f, 9f), new Vector2(2f, 9f), new Vector2(7f, 10f));
            break;
          case PersonAttachementType.Sundae:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Sundae, AttachmentLocation.LeftHand, new Rectangle(40, 473, 6, 6), new Rectangle(406, 420, 6, 6), new Vector2(-2f, 10f), new Vector2(-1f, 9f), new Vector2(6f, 10f), new Vector2(8f, 10f));
            break;
          case PersonAttachementType.BananaSplit:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.BananaSplit, AttachmentLocation.LeftHand, new Rectangle(293, 298, 5, 4), new Rectangle(293, 298, 5, 4), new Vector2(-2f, 7f), new Vector2(-1f, 8f), new Vector2(5f, 7f), new Vector2(7f, 8f));
            break;
          case PersonAttachementType.Parfait:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Parfait, AttachmentLocation.LeftHand, new Rectangle(228, 391, 4, 5), new Rectangle(228, 391, 4, 5), new Vector2(-2f, 7f), new Vector2(-2f, 8f), new Vector2(4f, 8f), new Vector2(6f, 8f));
            break;
          case PersonAttachementType.Coconut:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Coconut, AttachmentLocation.LeftHand, new Rectangle(305, 399, 7, 6), new Rectangle(305, 399, 7, 6), new Vector2(-1f, 9f), new Vector2(0.0f, 9f), new Vector2(7f, 9f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.FruitPunch:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.FruitPunch, AttachmentLocation.LeftHand, new Rectangle(305, 392, 7, 6), new Rectangle(305, 392, 7, 6), new Vector2(-1f, 9f), new Vector2(-1f, 9f), new Vector2(6f, 9f), new Vector2(8f, 9f));
            break;
          case PersonAttachementType.Mocktail:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Mocktail, AttachmentLocation.LeftHand, new Rectangle(88, 304, 6, 6), new Rectangle(88, 304, 6, 6), new Vector2(-2f, 9f), new Vector2(-1f, 9f), new Vector2(6f, 9f), new Vector2(8f, 9f));
            break;
          case PersonAttachementType.Burger:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Burger, AttachmentLocation.LeftHand, new Rectangle(128, 264, 6, 6), new Rectangle(128, 264, 6, 6), new Vector2(-2f, 8f), new Vector2(-1f, 9f), new Vector2(6f, 9f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Pizza:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Pizza, AttachmentLocation.LeftHand, new Rectangle(135, 264, 6, 5), new Rectangle(135, 264, 6, 5), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(4f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.CottonCandy:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.CottonCandy, AttachmentLocation.LeftHand, new Rectangle(89, 295, 7, 8), new Rectangle(89, 295, 7, 8), new Vector2(0.0f, 11f), new Vector2(1f, 10f), new Vector2(4f, 13f), new Vector2(9f, 12f));
            break;
          case PersonAttachementType.Slushie:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Slushie, AttachmentLocation.LeftHand, new Rectangle(82, 313, 5, 7), new Rectangle(82, 313, 5, 7), new Vector2(-2f, 10f), new Vector2(-1f, 10f), new Vector2(5f, 10f), new Vector2(7f, 10f));
            break;
          case PersonAttachementType.CrocHat:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.CrocHat, AttachmentLocation.Head, new Rectangle(378, 475, 12, 10), new Rectangle(391, 475, 15, 10), new Vector2(6f, 19f), new Vector2(6f, 19f), new Vector2(8f, 18f), new Vector2(6f, 18f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(71, 485, 12, 9));
            break;
          case PersonAttachementType.ShoppingBag:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.ShoppingBag, AttachmentLocation.RightHand, new Rectangle(121, 449, 4, 5), new Rectangle(305, 406, 7, 6), new Vector2(6f, 6f), new Vector2(4f, 6f), new Vector2(6f, 9f), new Vector2(6f, 6f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(347, 321, 4, 6));
            break;
          case PersonAttachementType.HippoBalloon:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.HippoBalloon, AttachmentLocation.RightHand, new Rectangle(1065, 132, 15, 22), new Rectangle(1129, 132, 15, 22), new Vector2(12f, 24f), new Vector2(9f, 25f), new Vector2(4f, 26f), new Vector2(13f, 27f), false);
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(1193, 130, 14, 24));
            PeopleAttachmentData.attachmententries[(int) attachementtype].HasIdle = true;
            break;
          case PersonAttachementType.BearBalloon:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.BearBalloon, AttachmentLocation.RightHand, new Rectangle(1253, 132, 15, 22), new Rectangle(1253, 391, 12, 21), new Vector2(14f, 24f), new Vector2(7f, 24f), new Vector2(4f, 24f), new Vector2(12f, 24f), false);
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(1193, 391, 14, 21));
            PeopleAttachmentData.attachmententries[(int) attachementtype].HasIdle = true;
            break;
          case PersonAttachementType.PigBalloon:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.PigBalloon, AttachmentLocation.RightHand, new Rectangle(1317, 129, 16, 25), new Rectangle(1353, 432, 15, 23), new Vector2(12f, 27f), new Vector2(7f, 27f), new Vector2(7f, 26f), new Vector2(12f, 26f), false);
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(1417, 432, 16, 22));
            PeopleAttachmentData.attachmententries[(int) attachementtype].HasIdle = true;
            break;
          case PersonAttachementType.ElephantScooter:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.ElephantScooter, AttachmentLocation.Waist, new Rectangle(124, 615, 16, 20), new Rectangle(250, 714, 16, 17), new Vector2(8f, 12f), new Vector2(6f, 11f), new Vector2(10f, 11f), new Vector2(8f, 12f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(93, 652, 16, 21));
            PeopleAttachmentData.attachmententries[(int) attachementtype].BlocksCharacterAnimation = true;
            PeopleAttachmentData.attachmententries[(int) attachementtype].SliceLegsOff(3, 4, 3);
            break;
          case PersonAttachementType.TigerScooter:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.TigerScooter, AttachmentLocation.Waist, new Rectangle(356, 475, 10, 19), new Rectangle(360, 497, 16, 16), new Vector2(5f, 10f), new Vector2(6f, 10f), new Vector2(10f, 10f), new Vector2(5f, 15f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(367, 475, 10, 21));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SliceLegsOff(3, 4, 3);
            break;
          case PersonAttachementType.AlpacaScooter:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.AlpacaScooter, AttachmentLocation.Waist, new Rectangle(490, 529, 10, 19), new Rectangle(480, 549, 16, 19), new Vector2(5f, 11f), new Vector2(6f, 12f), new Vector2(10f, 12f), new Vector2(5f, 18f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(394, 510, 10, 24));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SliceLegsOff(3, 4, 3);
            break;
          case PersonAttachementType.HorseScooter:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.HorseScooter, AttachmentLocation.Waist, new Rectangle(895, 772, 10, 20), new Rectangle(473, 531, 16, 17), new Vector2(5f, 11f), new Vector2(6f, 11f), new Vector2(10f, 11f), new Vector2(5f, 16f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(346, 328, 10, 22));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SliceLegsOff(3, 4, 3);
            break;
          case PersonAttachementType.LionScooter:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.LionScooter, AttachmentLocation.Waist, new Rectangle(325, 307, 10, 20), new Rectangle(443, 313, 16, 16), new Vector2(5f, 11f), new Vector2(5f, 10f), new Vector2(11f, 10f), new Vector2(5f, 16f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(335, 328, 10, 22));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SliceLegsOff(3, 4, 3);
            break;
          case PersonAttachementType.UnicornScooter:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.UnicornScooter, AttachmentLocation.Waist, new Rectangle(336, 307, 10, 20), new Rectangle(445, 293, 16, 18), new Vector2(5f, 11f), new Vector2(6f, 12f), new Vector2(10f, 12f), new Vector2(5f, 16f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(324, 328, 10, 22));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SliceLegsOff(3, 4, 3);
            break;
          case PersonAttachementType.Coffee:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Coffee, AttachmentLocation.LeftHand, new Rectangle(347, 314, 4, 6), new Rectangle(347, 314, 4, 6), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(4f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Popcorn:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Popcorn, AttachmentLocation.LeftHand, new Rectangle(299, 406, 5, 6), new Rectangle(299, 406, 5, 6), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(5f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Fries:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Fries, AttachmentLocation.LeftHand, new Rectangle(407, 427, 5, 5), new Rectangle(407, 427, 5, 5), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(5f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.CraftBeer:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.CraftBeer, AttachmentLocation.LeftHand, new Rectangle(82, 307, 5, 5), new Rectangle(82, 307, 5, 5), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(5f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Pretzel:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Pretzel, AttachmentLocation.LeftHand, new Rectangle(249, 425, 5, 4), new Rectangle(245, 425, 3, 4), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(5f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Taco:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Taco, AttachmentLocation.LeftHand, new Rectangle(240, 425, 4, 4), new Rectangle(235, 425, 4, 4), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(5f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.Chocolate:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.Chocolate, AttachmentLocation.LeftHand, new Rectangle(438, 313, 4, 5), new Rectangle(438, 313, 4, 5), new Vector2(-2f, 7f), new Vector2(-1f, 9f), new Vector2(4f, 8f), new Vector2(7f, 9f));
            break;
          case PersonAttachementType.FoxHat:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.FoxHat, AttachmentLocation.Head, new Rectangle(389, 413, 14, 11), new Rectangle(542, 140, 12, 10), new Vector2(7f, 20f), new Vector2(6f, 20f), new Vector2(5f, 19f), new Vector2(7f, 20f));
            PeopleAttachmentData.attachmententries[(int) attachementtype].SetCustomBackRectangle(new Rectangle(407, 473, 14, 10));
            break;
          case PersonAttachementType.DeliveryPackage:
            PeopleAttachmentData.attachmententries[(int) attachementtype] = new AttachmentInfo(PersonAttachementType.DeliveryPackage, AttachmentLocation.BothHands, new Rectangle(1569, 617, 10, 8), new Rectangle(1580, 617, 13, 8), new Vector2(5f, 11f), new Vector2(3f, 9f), new Vector2(10f, 9f), new Vector2(5f, 11f));
            break;
        }
      }
      return PeopleAttachmentData.attachmententries[(int) attachementtype];
    }
  }
}
