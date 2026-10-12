using System.Runtime.CompilerServices;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LeetCode.Common
{
    /// <summary>
    /// Adds trait "Folder" = path of the source file folder relative to the project (e.g. "2026/10").
    /// </summary>
    [TraitDiscoverer("LeetCode.Common.FolderTraitDiscoverer", "LeetCode")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class FolderAttribute : Attribute, ITraitAttribute
    {
        public FolderAttribute([CallerFilePath] string filePath = "")
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }

    public sealed class FolderTraitDiscoverer : ITraitDiscoverer
    {
        private const string ProjectFolder = "/LeetCode/";

        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var filePath = traitAttribute.GetConstructorArguments().FirstOrDefault() as string ?? "";
            yield return new KeyValuePair<string, string>("Folder", GetFolder(filePath));
        }

        private static string GetFolder(string filePath)
        {
            var dir = (Path.GetDirectoryName(filePath) ?? "").Replace('\\', '/') + "/";
            var index = dir.LastIndexOf(ProjectFolder, StringComparison.OrdinalIgnoreCase);
            var folder = index >= 0 ? dir[(index + ProjectFolder.Length)..] : dir;
            return folder.TrimEnd('/') is { Length: > 0 } f ? f : "/";
        }
    }
}
