// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.FeatureUnlockSpeechPack
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class FeatureUnlockSpeechPack
  {
    public string textToSay;
    public AnimalType person;
    public string panelHeader;

    public FeatureUnlockSpeechPack(string _textToSay, AnimalType _person, string _panelHeader = "")
    {
      this.textToSay = _textToSay;
      this.person = _person;
      this.panelHeader = _panelHeader;
    }
  }
}
