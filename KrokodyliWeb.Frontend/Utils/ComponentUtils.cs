using Blazored.Modal;
using Blazored.Modal.Services;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend.Components;

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
        public static IModalReference ShowImagePreview(this IModalService self, int index, IReadOnlyList<ImageDescriptor> images)
        {
            ImageCarousel? c;
            var parameters = new ModalParameters();
            parameters.Add(nameof(c.Images), images);
            parameters.Add(nameof(c.ActiveImageIndex), index);

            return self.Show<ImageCarousel>("Image preview carousel", parameters, ModalOptions);
        }

        public static IModalReference ShowImagePreview(this IModalService self, ImageDescriptor image)
            => self.ShowImagePreview(0, new[] { image });
    }
}
