using Blazored.Modal;
using Blazored.Modal.Services;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend.Components;
using KrokodyliWeb.Utils;
using MarkusSecundus.Util;

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
        public static IModalReference? ShowImagePreview(this IModalService self, ImageList.View view)
        {
            if (!view.IsValid) return null;
            //Console.WriteLine($"Starting carousel - imgs: [{view.Iterate().Select(n => n.Value.Source).MakeString("\n::")}]");
            ImageCarousel? c;
            var parameters = new ModalParameters();
            parameters.Add(nameof(c.ActiveImage), view);

            return self.Show<ImageCarousel>("Image preview carousel", parameters, ModalOptions);
        }

        public static IModalReference ShowImagePreview(this IModalService self, ImageDescriptor image)
            => self.ShowImagePreview(new ImageList().AddImage(image) );

    }
}
