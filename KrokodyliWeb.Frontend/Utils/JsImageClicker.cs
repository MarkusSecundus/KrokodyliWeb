using Blazored.Modal.Services;
using KrokodyliWeb.Data;
using KrokodyliWeb.Utils;
using Microsoft.JSInterop;

namespace KrokodyliWeb.Frontend.Utils
{
    public record class JsImageClicker(IModalService Modals, List<ImageDescriptor> ImagePool)
    {
        [JSInvokable]
        public DotNetObjectReference<ClickListener> MakeCallback(string src, string alt)
        {
            return DotNetObjectReference.Create(new ClickListener(Modals, ImagePool.AddElement(new(src, alt))));
        }

        public record class ClickListener(IModalService Modals, ListElementView<ImageDescriptor> DescriptorView)
        {
            [JSInvokable]
            public void Click()
            {
                Modals.ShowImagePreview(DescriptorView.Index, DescriptorView.List);
            }
        }
    }
}
