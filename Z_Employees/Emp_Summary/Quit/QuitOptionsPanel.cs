// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.Quit.QuitOptionsPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_Employees.Emp_Summary.Quit.Option;
using TinyZoo.Z_Employees.QuickPick;

namespace TinyZoo.Z_Employees.Emp_Summary.Quit
{
  internal class QuitOptionsPanel
  {
    public Vector2 location;
    private List<QuitOption> optionsFrames;
    private List<SimpleTextHandler> ORtext;

    public QuitOptionsPanel(QuickEmployeeDescription employee)
    {
      float num = 100f;
      this.optionsFrames = new List<QuitOption>(2);
      this.ORtext = new List<SimpleTextHandler>(1);
      for (int index = 0; index < 2; ++index)
      {
        if (index != 0)
        {
          SimpleTextHandler simpleTextHandler = new SimpleTextHandler("OR", true, _Scale: 2f);
          simpleTextHandler.Location = new Vector2(0.0f, (float) ((double) num * (double) index * 0.5));
          simpleTextHandler.SetAllColours(ColourData.Z_Cream);
          simpleTextHandler.AutoCompleteParagraph();
          this.ORtext.Add(simpleTextHandler);
        }
        this.optionsFrames.Add(new QuitOption((QuitOptions) index, employee)
        {
          location = new Vector2(0.0f, num * (float) index)
        });
      }
    }

    public QuitOptions UpdateQuitOptionsPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out int NewPayIfApplicable)
    {
      offset += this.location;
      NewPayIfApplicable = 0;
      for (int index = 0; index < this.optionsFrames.Count; ++index)
      {
        QuitOptions quitOptions = this.optionsFrames[index].UpdateQuitOption(player, DeltaTime, offset, out NewPayIfApplicable);
        if (quitOptions != QuitOptions.Count)
          return quitOptions;
      }
      return QuitOptions.Count;
    }

    public void DrawQuitOptionsPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.ORtext.Count; ++index)
        this.ORtext[index].DrawSimpleTextHandler(offset, 1f, spriteBatch);
      for (int index = 0; index < this.optionsFrames.Count; ++index)
        this.optionsFrames[index].DrawQuitOption(offset, spriteBatch);
    }
  }
}
