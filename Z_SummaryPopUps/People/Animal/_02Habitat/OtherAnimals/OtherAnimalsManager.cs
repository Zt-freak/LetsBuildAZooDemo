// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.OtherAnimals.OtherAnimalsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.Animals.Cohabitation;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.OtherAnimals
{
  internal class OtherAnimalsManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private SimpleTextHandler desc;

    public OtherAnimalsManager(
      PrisonZone prisonZone,
      PrisonerInfo prisonerInfo,
      float width,
      float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      CohabThreatPack otherAnimalThreats = Cohabitation_Calculator.GetOtherAnimalThreats(prisonZone, prisonerInfo.intakeperson);
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("Species Compatibility");
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      zero.X += defaultBuffer.X;
      zero.Y += defaultBuffer.Y;
      HashSet<AnimalType> animalTypeSet1 = new HashSet<AnimalType>();
      float num = 0.0f;
      HashSet<AnimalType> animalTypeSet2 = new HashSet<AnimalType>();
      foreach (AnimalCohabitRelationship cohabitRelationship in otherAnimalThreats.cohabitationrelationshipsJustQuerried)
      {
        if (cohabitRelationship.IsCarnivreThreat)
        {
          animalTypeSet1.Add(cohabitRelationship.aimaltype);
          num += cohabitRelationship.CarnivoreThreatValue;
        }
        if (cohabitRelationship.IsWeakerCarnivore)
          animalTypeSet2.Add(cohabitRelationship.aimaltype);
      }
      string str = string.Empty + "Threat Level: " + OtherAnimalsManager.GetThreatLevelString(num) + "~~";
      string TextToWrite;
      if (animalTypeSet1.Count > 0)
      {
        TextToWrite = str + "This animal is in fear of:";
        foreach (AnimalType enemytype in animalTypeSet1)
          TextToWrite = TextToWrite + "~" + EnemyData.GetEnemyTypeName(enemytype);
      }
      else
        TextToWrite = str + "This animal feels at ease in its pen.";
      this.desc = new SimpleTextHandler(TextToWrite, width - defaultBuffer.X, _Scale: BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location = zero;
      zero.Y += this.desc.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y;
      zero.X = width;
      this.customerframe.Resize(zero);
      this.desc.Location += -this.customerframe.VSCale * 0.5f;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public static string GetThreatLevelString(float value)
    {
      if ((double) value > 30.0)
        return "Very High";
      if ((double) value > 15.0)
        return "High";
      if ((double) value > 5.0)
        return "Medium";
      return (double) value > 0.0 ? "Low" : "None";
    }

    public void UpdateOtherAnimalsManager()
    {
    }

    public void DrawOtherAnimalsManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
