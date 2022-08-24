using KrokodyliWeb.Data;
using KrokodyliWeb.Utils;

namespace KrokodyliWeb.Frontend.Utils
{
    public class ImageList : IIntrusiveLinkedList<ImageList>
    {
        public IReadOnlyList<ImageDescriptor> Images => images;

        private readonly List<ImageDescriptor> images = new();

        public ImageList()
        {
            Next = Last = this;
        }

        public ImageList Next { get; set; }
        public ImageList Last { get; set; }

        public ImageList Remove()
        {
            images.Clear();
            return this.RemoveFirst();
        }

        public View AddImage(ImageDescriptor img) => new View(this, images.AddElement(img).Index);

        public ImageList AddImages(IEnumerable<ImageDescriptor> imgs)
        {
            images.AddRange(imgs);
            return this;
        }

        public IEnumerable<View> IterateViews()
        {
            for (int i = 0; i < Images.Count; ++i) yield return new View(this, i);
        }

        public record struct View(ImageList Node, int Index)
        {
            public ImageDescriptor Value => Node.Images[Index];
            public IEnumerable<View> Iterate()
            {
                for (int i = Index; i < Node.Images.Count; ++i) yield return new View(Node,i);
                for (ImageList node = Node.Next; node != Node; node = node.Next)
                    for(int i = 0;i<node.Images.Count;++i) yield return new View(node, i);
                for (int i = 0; i < Index; ++i) yield return new View(Node, i);
            }

            public bool IsSimple => Node.Iterate().Sum(v=>v.Images.Count) <= 1;

            public bool IsValid => Node?.Images != null && Index >= 0 && Index < Node.Images.Count;
        }
    }
}
