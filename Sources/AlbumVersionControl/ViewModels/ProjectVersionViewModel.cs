using System;
using System.Collections.Generic;
using System.Diagnostics;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Extensions;
using AlbumVersionControl.Models;
using AlbumVersionControl.Models.GitHubApi;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Utils;
using DevExpress.Xpf.Grid;

namespace AlbumVersionControl.ViewModels
{
    public class ProjectVersionViewModel : ViewModelBase
    {
        public ProjectVersion CurrentVersion
        {
            get { return GetProperty(() => CurrentVersion); }
            private set { SetProperty(() => CurrentVersion, value); }
        }

        public List<ProjectVersionContent> VersionContents
        {
            get { return GetProperty(() => VersionContents); }
            private set { SetProperty(() => VersionContents, value); }
        }

        public TreeListNode TreeListNodesVersionContent
        {
            get { return GetProperty(() => TreeListNodesVersionContent); }
            private set { SetProperty(() => TreeListNodesVersionContent, value); }
        }

        public void CreateNewVersion(string message)
        {
            CurrentVersion.Project.GitRepository.CreateNewCommit(message);
        }
        public void OpenCuerrentVersionFolder()
        {
            if (CurrentVersion != null)
            {
                var gitHubContent = new GitHubContent(Program.GitHubService, CurrentVersion.CommitDetail.Key, CurrentVersion.CommitDetail.Value);
                gitHubContent.ClearFolder();
                gitHubContent.DownloadFiles();
                var folderPath = new AppConfiguration().VersionContentFolder;
                Process.Start($"file://{folderPath}");
            }
        }

        protected override void OnParameterChanged(object parameter)
        {
            if (parameter == null) parameter = new ProjectVersion();
            if (!(parameter is ProjectVersion currentProjectVersion)) throw new ArgumentException("Parameter type unknown", nameof(parameter));
            CurrentVersion = currentProjectVersion;
            base.OnParameterChanged(parameter);
        }

        [Command]
        public void OnMainTreeListViewLoaded(object sender)
        {
            if (sender is TreeListView treeListView)
            {
                treeListView.Nodes.Clear();
                var gitHubContent = new GitHubContent(Program.GitHubService, CurrentVersion.CommitDetail.Key, CurrentVersion.CommitDetail.Value);
                VersionContents = gitHubContent.GetProjectVersionContents();
                TreeListNodesVersionContent = new TreeListNode
                {
                    Content = new ProjectVersionContent {FileName = "Root"},
                    IsExpandButtonVisible = DefaultBoolean.True,
                    IsExpanded = true
                };
                TreeListNodesVersionContent.MapFromPorjectVersionContent(VersionContents);

                treeListView.Nodes.Add(TreeListNodesVersionContent);
            }
        }
    }
}