using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Web.Mvc.Extensibility;

namespace MvcHaack.ViewMobilizer {
    public class ViewMobilizerModel {
        public ViewMobilizerModel(ProjectFolder folder) {
            var views = new Dictionary<string, ProjectFile>(StringComparer.OrdinalIgnoreCase);
            CollectViews(folder, views);
            Views = views;
            SelectedViews = new List<Tuple<ProjectFile, string>>();
        }

        private void CollectViews(ProjectFolder folder, Dictionary<string, ProjectFile> views) {
            foreach (var file in folder.Files) {
                if (file.Name.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase)) {
                    if (!IsMobileWithCorrespondingDesktop(file, views)) {
                        views.Add(file.RelativePath, file);
                    }
                }
            }
            foreach (var subfolder in folder.Folders) {
                CollectViews(subfolder, views);
            }
        }

        private bool IsMobileWithCorrespondingDesktop(ProjectFile file, Dictionary<string, ProjectFile> views) {
            string path = file.RelativePath;
            if (path.EndsWith(".mobile.cshtml", StringComparison.OrdinalIgnoreCase)) {
                int preMobileIndex = path.Length - ".mobile.cshtml".Length;
                string desktopPath = path.Substring(0, preMobileIndex) + ".cshtml";
                if (views.ContainsKey(desktopPath)) {
                    views.Remove(desktopPath);
                    return true;
                }
            }
            return false;
        }

        public Dictionary<string, ProjectFile> Views { get; private set; }

        public List<Tuple<ProjectFile, string>> SelectedViews { get; private set; }
    }
}
