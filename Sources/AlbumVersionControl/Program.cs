using AlbumVersionControl.Models.GitHubApi;
using AlbumVersionControl.ViewModels;

namespace AlbumVersionControl
{
    // Todo: избавиться от этого класса
    public static class Program
    {
        public static ProjectViewModel ProjectViewModel { get; set; }

        public static GitHubService GitHubService { get; set; }
    }
}