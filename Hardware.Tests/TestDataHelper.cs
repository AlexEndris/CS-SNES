namespace Hardware.Tests;

using System.Text.Json;

using Models;

public static class TestDataHelper
{
    public static readonly string DataDirectory = Path.Combine(GetRepoRoot(), "tests\\SingleStepTests\\65618\\v1\\");

    private static string GetRepoRoot()
    {
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\"));
    }
    
    public static string GetFilePath(string filenameWithoutExtension)
    {
        return Path.Combine(DataDirectory, $"{filenameWithoutExtension}.json");
    }

    public static IAsyncEnumerable<InstructionTestData?> ReadTestData(string filename)
    {
        var file = GetFilePath(filename);
        var data = JsonSerializer.DeserializeAsyncEnumerable<InstructionTestData>(File.OpenRead(file), JsonSerializerOptions.Web);
        return data;
    }
}
