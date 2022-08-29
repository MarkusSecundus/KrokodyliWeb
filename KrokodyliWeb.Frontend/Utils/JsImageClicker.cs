using Blazored.Modal.Services;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend.Components;
using KrokodyliWeb.Utils;
using MarkusSecundus.Util;
using Microsoft.JSInterop;

namespace KrokodyliWeb.Frontend.Utils
{
    public class JsImageClicker : IDisposable
    {
        public IModalService Modals { get; }
        public ImagePool.Data ImagePool { get; }

        private readonly ImageList added;

        private bool IsDisabled = false;

        public JsImageClicker(IModalService modals, ImagePool.Data imagePool)
        {
            (Modals, ImagePool) = (modals, imagePool);
            ImagePool.Images = ImagePool.Images.AppendList(added = new());
        }



        [JSInvokable]
        public DotNetObjectReference<ClickListener> MakeCallback(string src, string alt)
        {
            var descr = added.AddImage(new (src, alt));

            return DotNetObjectReference.Create(new ClickListener(this, descr));
        }

        public void Dispose()
        {
            ImagePool.Images = added.Dispose();
            IsDisabled = true;
        }

        public record class ClickListener(JsImageClicker Parent, ImageList.View DescriptorView)
        {
            [JSInvokable]
            public void Click()
            {
                if (Parent.IsDisabled) return;
                Console.WriteLine($"Clicked img: {DescriptorView.Value.Source}");
                Parent.Modals.ShowImagePreview(DescriptorView);
            }
        }
    }
}
