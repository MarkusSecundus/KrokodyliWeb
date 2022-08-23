using Blazored.Modal;
using Blazored.Modal.Services;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend.Components;
using KrokodyliWeb.Utils;

namespace KrokodyliWeb.Frontend.Utils
{
    public static class ComponentUtils
    {
        private static readonly ModalOptions ModalOptions = new()
        {
            Animation = ModalAnimation.FadeIn(0.75d),
            HideHeader = true,
            ContentScrollable = true,
            Class = "blazored-modal bg-transparent"
        };
        public static IModalReference ShowImagePreview(this IModalService self, DoubleLinkedList<ImageDescriptor> index)
        {
            ImageCarousel? c;
            var parameters = new ModalParameters();
            parameters.Add(nameof(c.ActiveImage), index);

            return self.Show<ImageCarousel>("Image preview carousel", parameters, ModalOptions);
        }

        public static IModalReference ShowImagePreview(this IModalService self, ImageDescriptor image)
            => self.ShowImagePreview(image );

    }
}
