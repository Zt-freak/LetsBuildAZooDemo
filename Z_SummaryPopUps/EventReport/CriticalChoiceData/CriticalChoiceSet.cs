// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData.CriticalChoiceSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;

namespace TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData
{
  internal class CriticalChoiceSet
  {
    public string HeadingText;
    public string BodyText;
    public string ObjectiveText;
    public CustomerType customertype;
    public AnimalType animaltypeforprotrait;
    public CriticalChoiceCharacter criticalcharacter;
    public List<CriticalChoiceAction> CriticalActions;

    public CriticalChoiceSet(CustomerType _customertype, CriticalChoiceCharacter thischoiceclass)
    {
      this.criticalcharacter = thischoiceclass;
      this.CriticalActions = new List<CriticalChoiceAction>();
      this.customertype = _customertype;
    }

    public void SetMainText(string _HeadingText, string _ObjectiveText, string _BodyText)
    {
      this.HeadingText = _HeadingText;
      this.ObjectiveText = _ObjectiveText;
      this.BodyText = _BodyText;
    }
  }
}
