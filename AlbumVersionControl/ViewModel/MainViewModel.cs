using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AlbumVersionControl.Model;
using DevExpress.Mvvm;
using Microsoft.Win32;

namespace AlbumVersionControl.ViewModel
{
    public class MainViewModel : BaseVm
    {
        public MainViewModel()
        {
            OverlayService.GetInstance().Show = str => { OverlayService.GetInstance().Text = str; };
        }

        public ObservableCollection<Video> Videos { get; set; }
        public ICollectionView VideosView { get; set; }
        public Video SelectedVideo { get; set; }


        private string _searchText { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                VideosView.Filter = obj => false;
                VideosView.Refresh();
            }
        }

        public ICommand Sort
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (VideosView.SortDescriptions.Count > 0)
                        VideosView.SortDescriptions.Clear();
                    else
                        VideosView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                });
            }
        }

        public ICommand DeleteVideo
        {
            get
            {
                return new DelegateCommand<Video>(video =>
                {
                    Videos.Remove(video);
                    SelectedVideo = Videos.FirstOrDefault();
                }, video => video != null);
            }
        }

        public ICommand AddItem
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var opd = new OpenFileDialog {Multiselect = true};
                    if (opd.ShowDialog() == true)
                        await Task.Factory.StartNew(() => { });
                });
            }
        }

        public ICommand TematicClick
        {
            get
            {
                return new DelegateCommand<string>(tematic =>
                {
                    if (tematic != null) SearchText = "#" + tematic;
                });
            }
        }

        public ICommand GoToUrl
        {
            get
            {
                return new DelegateCommand<string>(url =>
                {
                    if (new Uri(url).IsFile)
                        Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + url));
                    else
                        Process.Start(url);
                });
            }
        }

        [Obsolete]
        public ICommand EditVideo
        {
            get { return new DelegateCommand<Video>(video => { }, video => video != null); }
        }

        public ICommand OpenImage
        {
            get { return new DelegateCommand<string>(image => { }); }
        }

        public ICommand KeyWordClick
        {
            get
            {
                return new DelegateCommand<KeyWordItem>(word =>
                {
                    if (word != null) SearchText = "@" + word.Value;
                });
            }
        }

        public ICommand ChannelClick
        {
            get
            {
                return new DelegateCommand<string>(channel =>
                {
                    if (channel != null) SearchText = "!" + channel;
                });
            }
        }

        public ICommand DataClick
        {
            get { return new DelegateCommand<DateTime>(date => { SearchText = "$" + date.Date.ToShortDateString(); }); }
        }
    }

    internal class KeyWordItem
    {
        public string Value { get; internal set; }
    }

    public class Video
    {
    }
}