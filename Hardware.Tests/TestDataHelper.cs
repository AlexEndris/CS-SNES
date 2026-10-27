namespace Hardware.Tests;

using System.Text.Json;

using Cpu.Models;

public static class TestDataHelper
{
    public static readonly string DataDirectory65618 = Path.Combine(GetRepoRoot(), "tests","SingleStepTests","65618","v1");
    public static readonly string DataDirectorySpc700 = Path.Combine(GetRepoRoot(), "tests","SingleStepTests","spc700","v1");

    private static string GetRepoRoot()
    {
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..","..","..",".."));
    }
    
    public static string GetFilePath(string directory, string filenameWithoutExtension)
    {
        return Path.Combine(directory, $"{filenameWithoutExtension}.json");
    }

    public static IAsyncEnumerable<T?> ReadTestData<T>(string directory, string filename)
    {
        var file = GetFilePath(directory, filename);
        var data = JsonSerializer.DeserializeAsyncEnumerable<T>(File.OpenRead(file), JsonSerializerOptions.Web);
        return data;
    }
}
