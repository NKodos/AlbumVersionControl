using System;
using System.Windows.Controls;

namespace AlbumVersionControl.Controls
{
    public class RepeatMediaElement : MediaElement
    {
        public bool IsPause { get; set; }

        public RepeatMediaElement()
        {
            LoadedBehavior = MediaState.Manual;
            UnloadedBehavior = MediaState.Manual;
            Pause();
            IsPause = true;

            MediaEnded += (s, e) =>
            {
                Position = TimeSpan.FromSeconds(0);
                Play();
            };

            MouseDown += (s, e) =>
            {
                if (IsPause)
                {
                    Play();
                }
                else
                {
                    Pause();
                }
                IsPause = !IsPause;
            };
        }
    }
}