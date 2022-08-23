using Blazored.Modal.Services;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend.Components;
using KrokodyliWeb.Utils;
using Microsoft.JSInterop;

namespace KrokodyliWeb.Frontend.Utils
{
    public record class JsImageClicker(IModalService Modals, ImagePool.Data ImagePool)
    {
        [JSInvokable]
        public DotNetObjectReference<ClickListener> MakeCallback(string src, string alt)
        {
            DoubleLinkedList<ImageDescriptor> descr = new ImageDescriptor(src, alt);
            ImagePool.Images = ImagePool.Images.AppendList(descr);
            return DotNetObjectReference.Create(new ClickListener(Modals, descr));
        }

        public record class ClickListener(IModalService Modals, DoubleLinkedList<ImageDescriptor> DescriptorView)
        {
            [JSInvokable]
            public void Click()
            {
                Modals.ShowImagePreview(DescriptorView);
            }
        }
    }
}
