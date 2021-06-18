// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Speech.SpeechData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_OverWorld.Speech
{
  internal class SpeechData
  {
    internal static string GetThisNotificationToSpeech(
      Z_NotificationType notificationtype,
      ZTipType TipType)
    {
      switch (notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "A new baby has been born!";
            case 1:
              return "I hear a baby was born.";
            case 2:
              return "I want to see the new baby!";
            case 3:
              return "I read that there~is a new baby!";
            case 4:
              return "We just have to go see the new baby!";
          }
          break;
        case Z_NotificationType.A_AnimalBirthInBreedingRoom:
          switch (Game1.Rnd.Next(0, 3))
          {
            case 0:
              return "A new animal was~born in the breeding house.";
            case 1:
              return "The breeding program has a new face!";
            default:
              return "Really? They bred a new animal?";
          }
        case Z_NotificationType.A_AnimalHunger:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "Some animals are not~getting enough food.";
            case 1:
              return "Some of these animals~look hungry.";
            case 2:
              return "I don't think they are feeding these animals properly.";
            case 3:
              return "Who feeds these animals?";
            case 4:
              return "When is feeding time?";
            case 5:
              return "These animals look undernourished.";
            default:
              return "Why are the animals hungry?";
          }
        case Z_NotificationType.A_AnimalDeath:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "I can't believe~that an animal died.";
            case 1:
              return "Seeing an animal die~is the saddest thing ever!";
            case 2:
              return "An animal is dead?!";
            case 3:
              return "Why did the animal die?";
            case 4:
              return "Why couldn't they save that animal's life?";
            default:
              return "Shocking, they let that animal die.";
          }
        case Z_NotificationType.A_CRISPR_HybridBirth:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "Wow, science made a~new animal! I want to see.";
            case 1:
              return "They used CRISPR to make a new animal?";
            case 2:
              return "It's amazing, a new type of animal has been made.";
            case 3:
              return "Hybrids are astounding!~I want to see the new one.";
            case 4:
              return "Cross breeding goes against nature!~But I still want to see the new one!";
            case 5:
              return "A new Cross-breed,~a nightmare or a miracle!";
            default:
              return "Making new animals!~Who do they think they are?";
          }
        case Z_NotificationType.F_BuildArchitect:
          return "This zoo should start~a research program.";
        case Z_NotificationType.A_NoWater:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "Where are these animals~getting their water from?";
            case 1:
              return "Some of these animals~look very thirsty.";
            case 2:
              return "This zoo is not~watering their animals.";
            case 3:
              return "Maybe I should buy some cola for these~animals, they don't have anything to drink.";
            case 4:
              return "Why don't these~animals have~anything to drink!?";
            case 5:
              return "This is outrageous!~How can there not be water for these animals.";
            default:
              return "What an appalling way~to treat their animals.";
          }
        case Z_NotificationType.A_NoEnrichment:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "These poor animals~look so bored.";
            case 1:
              return "I wish the animals had~more things to play with.";
            case 2:
              return "What a boring enclosure.";
            case 3:
              return "Other zoos have things~for the animals to do.";
            case 4:
              return "This zoo is not enriching~these animals with activities.";
            default:
              return "Animals are so much more fun to~watch when they are playing with things.";
          }
        case Z_NotificationType.A_BuildAnotherPen:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "This zoo needs more enclosures!";
            case 1:
              return "I love this zoo, but I wish it had more exhibits.";
            case 2:
              return "I dream of this zoo being~bigger, I wish it had more animals!";
            case 3:
              return "I love animals so much!~I just wish there were more enclosures to see.";
            case 4:
              return "If I owned this zoo,~I would build more animal exhibits.";
          }
          break;
        case Z_NotificationType.F_BuildAnyShop:
          return Game1.Rnd.Next(0, 2) == 0 ? "This place doesn't have any shops!" : "There is nothing to buy here, that's unusual.";
        case Z_NotificationType.F_BuildAFoodShop:
          return Game1.Rnd.Next(0, 2) == 0 ? "Why isn't there anything to eat around here?" : "This place needs~somewhere to get snacks.";
        case Z_NotificationType.F_BuildAGiftShop:
          switch (Game1.Rnd.Next(0, 3))
          {
            case 0:
              return "I wish I could take home something cool, like a mug or T-Shirt.";
            case 1:
              return "This place needs a gift shop.";
            default:
              return "This zoo should sell souvenirs.";
          }
        case Z_NotificationType.F_AShopNeedsAnEmployee:
          switch (Game1.Rnd.Next(0, 3))
          {
            case 0:
              return "There are shops here with~nobody working in them.";
            case 1:
              return "Why is that shop empty?";
            default:
              return "Why aren't there any~people working in that shop?";
          }
        case Z_NotificationType.F_AJobHasApplicants:
          switch (Game1.Rnd.Next(0, 3))
          {
            case 0:
              return "I heard my friend has~applied for a job here!";
            case 1:
              return "I want to work here, it could be fun.";
            default:
              return "My uncle saw a job listing for this place!~He told me he applied this morning.";
          }
        case Z_NotificationType.A_NoWaterConnection:
          return Game1.Rnd.Next(0, 2) == 0 ? "That pen has no water supply." : "Why doesnt the water supply~reach all the enclosures?";
        case Z_NotificationType.F_TicketPrice:
          int num = Game1.Rnd.Next(0, 5);
          if (TipType == ZTipType.TicketPriceTooCheap)
          {
            switch (num)
            {
              case 0:
                return "The tickets to this~are a steal!";
              case 1:
                return "I think someone is going top loose their job!~The Ticket price seems very cheap.";
              case 2:
                return "The ticket price is so cheap~Surely it's a mistake?";
              case 3:
                return "The entry cost to this zoo is a bargain!";
              default:
                return "It's very cheap coming here, I will tell my friends.";
            }
          }
          else
          {
            switch (num)
            {
              case 0:
                return "The tickets to this~zoo seem overpriced.";
              case 1:
                return "This zoo is too much,~The ticket price is farcical.";
              case 2:
                return "Are these tickets golden?~The price would make you think so.";
              case 3:
                return "I could eat for a month for~the price of a ticket here!";
              default:
                return "The price of tickets is ridiculous.~And by that, I mean too much.";
            }
          }
      }
      return "No String";
    }

    internal static string GetThisSpeech(SpeechEvent speechevent)
    {
      switch (speechevent)
      {
        case SpeechEvent.NoWaterInPen:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "These poor animals~have no water.";
            case 1:
              return "There is nothing~for these animals to drink";
            case 2:
              return "They look thirsty.";
            case 3:
              return "Where do these~animals drink?";
            default:
              return "Here is~their water?";
          }
        case SpeechEvent.ISeeDeadThings:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "I am horrified!";
            case 1:
              return "Such a beautiful animal,~taken before its time.";
            case 2:
              return "Why are there~dead animals here?";
            case 3:
              return "Dead animals upset me.";
            default:
              return "I prefer living~animals to dead ones.";
          }
        case SpeechEvent.ISeePoop:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "What is that smell?";
            case 1:
              return "Someone needs to~clean this pen.";
            case 2:
              return "Those animals can't be~happy in their own filth.";
            case 3:
              return "This is inhumane.";
            default:
              return "This enclosure~is disgusting.";
          }
        case SpeechEvent.NothigLeftToDo:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "I guess that there~is nothing left to see.";
            case 1:
              return "I wish there was more to do.";
            case 2:
              return "I have seen everything!";
            case 3:
              return "I am leaving, there~isn't anything else to do.";
            case 4:
              return "There is nothing~more for me here.";
            case 5:
              return "Is that all there is here?";
            case 6:
              return "This zoo needs more things to do.";
            default:
              return "I hope they add more~things to see in future.";
          }
        case SpeechEvent.CustomerThirst:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "I am so thirsty!";
            case 1:
              return "Can I get a drink~around here?";
            case 2:
              return "I need a drink.";
            case 3:
              return "Where is the~drinks stall?";
            default:
              return "I am parched!";
          }
        case SpeechEvent.CustomerBench:
          switch (Game1.Rnd.Next(0, 5))
          {
            case 0:
              return "I wish I could~find a bench?";
            case 1:
              return "I am so tired~I need to sit down.";
            case 2:
              return "I need a seat.";
            case 3:
              return "Why can't I find~a place to relax.";
            default:
              return "This zoo~needs benches.";
          }
        case SpeechEvent.CustomerToilet:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "Any idea where~the bathroom is?";
            case 1:
              return "It's embarrassing,~but I need the toilet.";
            case 2:
              return "I need to powder my nose.";
            case 3:
              return "I can't find the toilet!";
            case 4:
              return "Nature calls!";
            default:
              return "I have to go...~but where can I go?";
          }
        case SpeechEvent.CustomerHunger:
          switch (Game1.Rnd.Next(0, 6))
          {
            case 0:
              return "I am hungry!";
            case 1:
              return "Where is the food?";
            case 2:
              return "Does this zoo~have any restaraunts?";
            case 3:
              return "There isn't enough food in this place.";
            case 4:
              return "So hungry!";
            default:
              return "I need food!";
          }
        case SpeechEvent.NoATM_NoMoney:
          switch (Game1.Rnd.Next(0, 7))
          {
            case 0:
              return "Can anyone lend me some money,~I seem to be running low?";
            case 1:
              return "Is there an ATM here?";
            case 2:
              return "I didnt bring enough cash.";
            case 3:
              return "why doesnt this place have an ATM?";
            case 4:
              return "This place still uses paper money, but for some reason I cant find an ATM!";
            case 5:
              return "this zoo requires old fashioned Cash? Who has that these days?";
            default:
              return "If I could pay with bitcoin I would be OK, but not real money";
          }
        default:
          return "No TEXT";
      }
    }
  }
}
