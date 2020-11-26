using System;

namespace AlbumVersionControl.Model
{
    public class OverlayService
    {
        private static OverlayService _Instance = new OverlayService();
        public static OverlayService GetInstance() => _Instance;

        private OverlayService() { }

        public Action<string> Show { get; set; }

        public string Text { get; set; } = "";

        public void Close()
        {
            Text = "";
        }
    }
}