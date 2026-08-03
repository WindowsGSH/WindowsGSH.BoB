using WindowsGSH.Core.Modules;

namespace WindowsGSH.Modules.BeastsOfBermuda;

internal static class ExistingInstallImport
{
    public static bool CanImport(IGameServerModule module, string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return false;
        return File.Exists(Path.Combine(ResolveInstallPath(module, path), module.Runtime.StartPath));
    }

    public static Task<ModuleExistingServerImportProbe> PreviewAsync(IGameServerModule module, string path, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var sourcePath = Path.GetFullPath(path);
        var installPath = ResolveInstallPath(module, sourcePath);
        var settings = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        var warnings = new[] { "Launch settings are not recoverable reliably from Game.ini; review the module defaults before importing." };
        return Task.FromResult(new ModuleExistingServerImportProbe(Path.GetFileName(sourcePath), installPath, settings, warnings));
    }

    private static string ResolveInstallPath(IGameServerModule module, string path)
    {
        var sourcePath = Path.GetFullPath(path);
        if (File.Exists(Path.Combine(sourcePath, module.Runtime.StartPath))) return sourcePath;
        var serverFilesPath = Path.Combine(sourcePath, "serverfiles");
        return File.Exists(Path.Combine(serverFilesPath, module.Runtime.StartPath)) ? serverFilesPath : sourcePath;
    }
}
