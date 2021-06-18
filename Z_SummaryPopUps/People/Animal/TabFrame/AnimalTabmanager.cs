// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame.AnimalTabmanager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.PointAtThings;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame
{
  internal class AnimalTabmanager
  {
    public Vector2 location;
    private TabFrameButton[] tabbuttons;
    private float refBaseScale;
    private float refWidthForTabs;
    public static int PreferredWidthOfEachTab_Raw = 50;
    private bool ForceNextFrame;
    private bool ForceLeft;

    public AnimalTabmanager(float BaseScale, float WidthForTabs, params TabType[] tabs) => this.Create(BaseScale, WidthForTabs, tabs);

    private void Create(float BaseScale, float WidthForTabs, params TabType[] tabs)
    {
      this.refBaseScale = BaseScale;
      this.refWidthForTabs = WidthForTabs;
      int length = tabs.Length;
      this.tabbuttons = new TabFrameButton[length];
      float num1 = 2f;
      if ((double) BaseScale != -1.0)
        num1 *= BaseScale;
      float num2 = (WidthForTabs + num1) / (float) length;
      for (int index = 0; index < this.tabbuttons.Length; ++index)
      {
        this.tabbuttons[index] = new TabFrameButton(tabs[index], num2 - num1, BaseScale);
        this.tabbuttons[index].Location.X = num2 * (float) index;
        this.tabbuttons[index].Location.X -= num2 * ((float) (length - 1) * 0.5f);
      }
      this.tabbuttons[0].Selected = true;
    }

    public void ForceTabSwitch(bool IsLeft)
    {
      this.ForceNextFrame = true;
      this.ForceLeft = IsLeft;
    }

    public void ForceTabSwitch(int tabIndex)
    {
      for (int index = 0; index < this.tabbuttons.Length; ++index)
        this.tabbuttons[index].Selected = false;
      this.tabbuttons[tabIndex].Selected = true;
    }

    public float GetHeightOfTabs() => this.tabbuttons.Length != 0 ? this.tabbuttons[0].GetSize().Y : 0.0f;

    public void SetToTopLeftOfThisPanel(BigBrownPanel bigBrownPanel)
    {
      this.location = Vector2.Zero;
      Vector2 frameOffsetFromTop = bigBrownPanel.GetFrameOffsetFromTop(true);
      this.location.Y += frameOffsetFromTop.Y;
      this.location.X += frameOffsetFromTop.X;
      this.location.X += this.refWidthForTabs * 0.5f;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      bool flag = false;
      for (int index = 0; index < this.tabbuttons.Length; ++index)
        flag |= this.tabbuttons[index].CheckMouseOver(player, offset);
      return flag;
    }

    public void AddRedLight(OffscreenPointerType type, int tabIndex, bool RemoveInstead = false) => this.tabbuttons[tabIndex].AddRedLight(type, RemoveInstead);

    public void AddNotificationIcon(OffscreenPointerType type, int tabIndex, bool RemoveInstead = false) => this.tabbuttons[tabIndex].AddNotificationIcon(type, RemoveInstead);

    public TabType UpdateAnimalTabmanager(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.location;
      TabType tabType = TabType.Count;
      if (this.ForceNextFrame)
      {
        this.ForceNextFrame = false;
        for (int index = 0; index < this.tabbuttons.Length; ++index)
        {
          if (this.tabbuttons[index].Selected)
          {
            if (this.ForceLeft && index > 0)
              this.tabbuttons[index - 1].ForceReturnTruNextUpdate_FORCONTROLLER = true;
            else if (!this.ForceLeft && index < this.tabbuttons.Length - 1)
              this.tabbuttons[index + 1].ForceReturnTruNextUpdate_FORCONTROLLER = true;
          }
        }
      }
      for (int index1 = 0; index1 < this.tabbuttons.Length; ++index1)
      {
        if (this.tabbuttons[index1].UpdateTabFrameButton(player, Offset) && !this.tabbuttons[index1].Selected)
        {
          for (int index2 = 0; index2 < this.tabbuttons.Length; ++index2)
            this.tabbuttons[index2].Selected = false;
          this.tabbuttons[index1].Selected = true;
          tabType = this.tabbuttons[index1].refTabType;
        }
      }
      return tabType;
    }

    public void PreDrawAnimalTabmanager(Vector2 Offset)
    {
      Offset += this.location;
      this.PreDrawAnimalTabmanager(Offset, AssetContainer.pointspritebatchTop05);
    }

    public void PreDrawAnimalTabmanager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.location;
      for (int index = 0; index < this.tabbuttons.Length; ++index)
        this.tabbuttons[index].PrerawTabFrameButton(spriteBatch, Offset);
    }

    public void DrawAnimalTabmanager(Vector2 Offset)
    {
      Offset += this.location;
      this.DrawAnimalTabmanager(Offset, AssetContainer.pointspritebatchTop05);
    }

    public void DrawAnimalTabmanager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.location;
      for (int index = 0; index < this.tabbuttons.Length; ++index)
        this.tabbuttons[index].PostDrawTabFrameButton(spriteBatch, Offset);
    }
  }
}
