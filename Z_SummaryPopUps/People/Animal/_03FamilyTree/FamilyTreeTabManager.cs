// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree.FamilyTreeTabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree.Children;
using TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree.Parents;
using TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree.Source;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree
{
  internal class FamilyTreeTabManager
  {
    public Vector2 location;
    internal static float SourceHeight = 100f;
    internal static float ParentsHeight = 100f;
    internal static float ChildrenHeight = 100f;
    private SourceManager sourcemanager;
    private ParentsManager parentsmanager;
    private ChildrenManager childrenmanager;
    private Vector2 size;

    public FamilyTreeTabManager(PrisonerInfo animal, Player player, float width, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.size = Vector2.Zero;
      this.sourcemanager = new SourceManager(width, BaseScale);
      this.sourcemanager.location.Y = this.size.Y;
      this.sourcemanager.location.Y += this.sourcemanager.GetSize().Y * 0.5f;
      this.size.Y += this.sourcemanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.parentsmanager = new ParentsManager(width, BaseScale);
      this.parentsmanager.location.Y = this.size.Y;
      this.parentsmanager.location.Y += this.parentsmanager.GetSize().Y * 0.5f;
      this.size.Y += this.parentsmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.childrenmanager = new ChildrenManager(width, BaseScale);
      this.childrenmanager.location.Y = this.size.Y;
      this.childrenmanager.location.Y += this.childrenmanager.GetSize().Y * 0.5f;
      this.size.Y += this.childrenmanager.GetSize().Y;
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateFamilyTreeTabManager()
    {
    }

    public void DrawFamilyTreeTabManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.sourcemanager.DrawSourceManager(offset, spriteBatch);
      this.parentsmanager.DrawParentsManager(offset, spriteBatch);
      this.childrenmanager.DrawChildrenManager(offset, spriteBatch);
    }
  }
}
