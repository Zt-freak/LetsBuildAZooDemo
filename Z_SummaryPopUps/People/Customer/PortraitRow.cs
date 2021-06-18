// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.PortraitRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class PortraitRow
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private int maxInRow;
    private float portraitsize;
    private List<AnimalInFrame> portraits;
    private bool useactiveicons;
    private bool drawframe;
    private List<string> customtexts;
    private List<bool> withoutframes;
    private List<float> customtextcalemults;

    public PortraitRow(
      int maxInRow_,
      float basescale_,
      float portraitsize_ = 40f,
      bool useactiveicons_ = false,
      bool drawframe_ = false)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.drawframe = drawframe_;
      this.useactiveicons = useactiveicons_;
      this.maxInRow = maxInRow_;
      this.maxInRow = this.maxInRow < 2 ? 2 : this.maxInRow;
      this.portraitsize = portraitsize_;
      this.customtexts = new List<string>();
      this.withoutframes = new List<bool>();
      this.customtextcalemults = new List<float>();
      this.portraits = new List<AnimalInFrame>();
      foreach (AnimalInFrame portrait in this.portraits)
        portrait.SetShowActiveIcon(this.useactiveicons);
      this.framescale = new Vector2();
      this.PositionAndSize();
    }

    public int Count => this.portraits.Count;

    public Vector2 GetPortraitRelativePosition(int index) => this.portraits[index].Location;

    public void PositionAndSize()
    {
      this.framescale = Vector2.Zero;
      if (this.portraits.Count < 1)
      {
        this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      }
      else
      {
        int num1 = Math.Min(this.maxInRow, this.portraits.Count);
        for (int index = 0; index < num1; ++index)
        {
          AnimalInFrame portrait = this.portraits[index];
          this.framescale.X += portrait.GetSize().X + 0.5f * this.uiscale.DefaultBuffer.X;
          this.framescale.Y = Math.Max(portrait.GetSize().Y, this.framescale.Y);
        }
        this.framescale.X -= 0.5f * this.uiscale.DefaultBuffer.X;
        float num2 = -0.5f * this.framescale.X;
        for (int index = 0; index < num1; ++index)
        {
          AnimalInFrame portrait = this.portraits[index];
          portrait.Location.X = num2 + 0.5f * portrait.GetSize().X;
          num2 += portrait.GetSize().X + 0.5f * this.uiscale.DefaultBuffer.X;
        }
        this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      }
    }

    public void Add(
      AnimalType animaltype,
      AnimalType headtype,
      int variant = 0,
      int headvariant = -1,
      bool withoutframe = false)
    {
      this.portraits.Add(new AnimalInFrame(animaltype, headtype, variant, this.portraitsize * this.basescale, BaseScale: (2f * this.basescale), HeadVariant: headvariant));
      this.customtexts.Add((string) null);
      this.withoutframes.Add(withoutframe);
      this.customtextcalemults.Add(1f);
      this.PositionAndSize();
    }

    public void Add(string text, bool withoutframe, float textscalemultiplier = 1f)
    {
      this.portraits.Add(new AnimalInFrame(AnimalType.Capybara, AnimalType.None, TargetSize: (this.portraitsize * this.basescale), BaseScale: (2f * this.basescale)));
      this.customtexts.Add(text);
      this.withoutframes.Add(withoutframe);
      this.customtextcalemults.Add(textscalemultiplier);
      this.PositionAndSize();
    }

    public void RemoveLast()
    {
      this.portraits.RemoveAt(this.portraits.Count - 1);
      this.PositionAndSize();
    }

    public Vector2 GetSize() => this.framescale;

    public void SetGreyedOut(int index, bool greyed)
    {
      this.portraits[index].Darken(greyed);
      this.portraits[index].SetAnimalGreyedOut(greyed);
      this.portraits[index].SetShowActiveIcon(!greyed && this.useactiveicons);
    }

    public void SetActiveIcon(int index, bool active) => this.portraits[index].SetActiveIconState(active);

    public void SetShowActiveIcon(int index, bool show) => this.portraits[index].SetShowActiveIcon(show);

    public bool UpdatePortraitRow(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawPortraitRow(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      int num = Math.Min(this.maxInRow, this.portraits.Count);
      if (num < 1)
        return;
      for (int index = 0; index < num - 1; ++index)
      {
        AnimalInFrame portrait = this.portraits[index];
        if (this.customtexts[index] == null)
          portrait.DrawAnimalInFrame(offset, spritebatch);
        else
          this.portraits[index].DrawPlusMore(offset, spritebatch, this.portraits.Count - this.maxInRow + 1, this.customtextcalemults[index] * this.basescale, AssetContainer.SpringFontX1AndHalf, customString: this.customtexts[index], withoutframe: this.withoutframes[index]);
      }
      if (this.portraits.Count > this.maxInRow)
        this.portraits[num - 1].DrawPlusMore(offset, spritebatch, this.portraits.Count - this.maxInRow + 1, this.basescale, AssetContainer.SpringFontX1AndHalf);
      else
        this.portraits[num - 1].DrawAnimalInFrame(offset, spritebatch);
    }
  }
}
