using WindowsGSH.Core.Modules;

namespace WindowsGSH.Modules.BeastsOfBermuda;

public sealed class BeastsOfBermudaModule : ManifestBackedGameServerModule, IModuleExistingServerImportCapability
{
    private const string ConfigPath = @"BeastsOfBermuda\Saved\Config\WindowsServer\Game.ini";
    public bool CanImport(string path) => ExistingInstallImport.CanImport(this, path);

    public Task<ModuleExistingServerImportProbe> PreviewImportAsync(string path, CancellationToken cancellationToken) =>
        ExistingInstallImport.PreviewAsync(this, path, cancellationToken);

    public override Task<IReadOnlyDictionary<string,object?>> ReadConfigFileSettingsAsync(ServerInstance instance,CancellationToken cancellationToken){cancellationToken.ThrowIfCancellationRequested();var result=new Dictionary<string,object?>();var path=Path.Combine(instance.InstallPath,ConfigPath);if(!File.Exists(path))return Task.FromResult<IReadOnlyDictionary<string,object?>>(result);var values=File.ReadLines(path).Select(x=>x.Split('=',2)).Where(x=>x.Length==2).ToDictionary(x=>x[0].Trim(),x=>x[1].Trim().TrimEnd('f','F'),StringComparer.OrdinalIgnoreCase);Copy(values,result,"GameMode","server.gameMode");CopyDecimal(values,result,"GrowthLimit","gameplay.growthLimit");CopyBool(values,result,"bConsoleLocked","security.consoleLocked");return Task.FromResult<IReadOnlyDictionary<string,object?>>(result);}
    public override Task WriteConfigFileSettingsAsync(ServerInstance instance,CancellationToken cancellationToken){cancellationToken.ThrowIfCancellationRequested();var path=Path.Combine(instance.InstallPath,ConfigPath);Directory.CreateDirectory(Path.GetDirectoryName(path)!);var lines=File.Exists(path)?File.ReadAllLines(path).ToList():new List<string>{"[/Script/BeastsOfBermuda.ServerGameInstance]"};Set(lines,"GameMode",GetSetting(instance,"server.gameMode","Life_Cycle"));Set(lines,"GrowthLimit",GetSetting(instance,"gameplay.growthLimit","2")+"f");Set(lines,"bConsoleLocked",GetSetting(instance,"security.consoleLocked","true").ToLowerInvariant());File.WriteAllLines(path,lines);return Task.CompletedTask;}
    private static void Set(List<string> lines,string key,string value){for(var i=0;i<lines.Count;i++){var p=lines[i].Split('=',2);if(p.Length==2&&p[0].Trim().Equals(key,StringComparison.OrdinalIgnoreCase)){lines[i]=$"{key}={value}";return;}}lines.Add($"{key}={value}");}private static void Copy(IDictionary<string,string>s,IDictionary<string,object?>d,string f,string t){if(s.TryGetValue(f,out var v))d[t]=v;}private static void CopyDecimal(IDictionary<string,string>s,IDictionary<string,object?>d,string f,string t){if(s.TryGetValue(f,out var v)&&decimal.TryParse(v,System.Globalization.NumberStyles.Number,System.Globalization.CultureInfo.InvariantCulture,out var n))d[t]=n;}private static void CopyBool(IDictionary<string,string>s,IDictionary<string,object?>d,string f,string t){if(s.TryGetValue(f,out var v)&&bool.TryParse(v,out var b))d[t]=b;}
}
