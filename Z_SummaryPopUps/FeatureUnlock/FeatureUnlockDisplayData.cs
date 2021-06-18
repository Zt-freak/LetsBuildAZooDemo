// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.FeatureUnlockDisplayData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class FeatureUnlockDisplayData
  {
    public static string GetPanelHeaderForThis(FeatureUnlockDisplayType type)
    {
      switch (type)
      {
        case FeatureUnlockDisplayType.ResearchHubUnlocked:
        case FeatureUnlockDisplayType.LandExpansion:
        case FeatureUnlockDisplayType.CRIPSRUnlocked:
          return "News Report";
        case FeatureUnlockDisplayType.Loans:
          return "Loans";
        case FeatureUnlockDisplayType.MoralityUsed:
          return "Morality";
        case FeatureUnlockDisplayType.VIPIntro:
          return "VIPs";
        default:
          return "PANEL HEADER GOES HERE";
      }
    }

    public static string GetNewsHeaderForThis(FeatureUnlockDisplayType type)
    {
      switch (type)
      {
        case FeatureUnlockDisplayType.ResearchHubUnlocked:
          return "Plans for Research Hub in Local Zoo";
        case FeatureUnlockDisplayType.LandExpansion:
          return "Mayor Approves Expansion Plans";
        case FeatureUnlockDisplayType.CRIPSRUnlocked:
          return "Local Zoo Develops Animal-Splicing Technology";
        case FeatureUnlockDisplayType.MoralityUsed:
          return "Your Choices Matter!";
        case FeatureUnlockDisplayType.VIPIntro:
          return "There is a VIP in your zoo!";
        default:
          return "NEWS HEADER GOES HERE";
      }
    }

    public static string GetTitleOfBodyTextForThisNews(FeatureUnlockDisplayType type)
    {
      switch (type)
      {
        case FeatureUnlockDisplayType.ResearchHubUnlocked:
        case FeatureUnlockDisplayType.LandExpansion:
        case FeatureUnlockDisplayType.CRIPSRUnlocked:
          return "Harold Smith | Editor";
        default:
          return "NEWS BODY TITLE GOES HERE";
      }
    }

    public static string GetBodyParagraphForNews(FeatureUnlockDisplayType type)
    {
      string str = "PARAGRAPH GOES HERE";
      switch (type)
      {
        case FeatureUnlockDisplayType.ResearchHubUnlocked:
          str = "There has been a leak that the local zoo has upcoming plans for a research hub. Once that is built, one can expect new and exciting items to appear in the zoo. ~~Zoo management says that there are currently no estimates on when it will be built. However, they reassure us that after it is constructed, the researchers will be working hard to bring new items and updates to the zoo.  ~~We spoke to the locals, the reactions are mostly positive. 'New stuff is always exciting!' says Jane, a housewife with kids. ~~They will be waiting for new updates, and hopes that the research hub will be built quickly.";
          break;
        case FeatureUnlockDisplayType.LandExpansion:
          str = "Due to the expanding popularity of the zoo, the mayor has approved their plans for expansion.~~They have cut the nightmare of paperwork to purchase land, shortened the process and made it fast and easy - as long as you have the money.~~Our mayor is very delighted by the zoo and excited for the upcoming plans.~~'I hope that we will be able to get a huge sprawling zoo, in the middle of our local city that we can proudly call our own,' she states.~~We have yet to hear from the zoo management team with regards to their expansion plans, but we are presuming it will happen in the near future.";
          break;
        case FeatureUnlockDisplayType.CRIPSRUnlocked:
          str = "Many scientists have put their heads together and come up with ground-breaking technology that will achieve the impossible - transferring animal heads and bodies to splice them together.~~When interviewed, the staff and management were very secretive about the process, but they claim that it is nothing illegal.~~Some people are concerned about what this means for the future, as well as the ethical issues this may occur. For others, this a large step forwards for science that will hopefully bring the zoo and humanity to greater heights.~~It is unknown when the zoo intends to build the full facility and start their venture into animal splicing.";
          break;
        case FeatureUnlockDisplayType.MoralityUsed:
          str = "The morality choices you make will impact the buildings and actions you can use, and may be reflected in certain people's behavior. Choose wisely.";
          break;
        case FeatureUnlockDisplayType.VIPIntro:
          str = "Occasionally, VIPs will visit your zoo. You will be able to interact with them by locating them and speaking to them.";
          break;
      }
      return str;
    }

    public static string GetCaptionForThisPicture(PictureType pictureType)
    {
      switch (pictureType)
      {
        case PictureType.DNABuilding:
          return "Draft plan of CRISPR Splicing building";
        case PictureType.ResearchHub:
          return "Draft plan of the research hub";
        case PictureType.HybridAnimal:
          return "Examples of possible splines";
        case PictureType.HyrbidAnimalDrawings:
          return "Illustrated guide of splice of rabbit and snake";
        case PictureType.ResearchHubDrawings:
          return "Blueprint of research hub";
        case PictureType.LandForSale:
          return "Photo of the mayor with land for sale";
        case PictureType.MoralityPreview_Good:
        case PictureType.MoralityPreview_Bad:
        case PictureType.VIPThreeInOne:
          return string.Empty;
        default:
          return "CAPTION GOES HERE";
      }
    }

    public static string GetTextForSpeech(FeatureUnlockDisplayType type)
    {
      string str = string.Empty;
      if (type == FeatureUnlockDisplayType.Loans)
        str = "This is your first loan from the bank!~~You will automatically take a loan if you are unable to pay your staff or receive a fine, but you can also apply for one on your own.~~There is no set date to repay us, but a monthly interest of 1% will be deducted while the full sum of the loan is not paid.~~Please contact me again if you need any more info!";
      return str;
    }
  }
}
