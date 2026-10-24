namespace Hardware.Tests.Apu.Models;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Hardware.Apu;

public class TestApuBus : IApuBus
{
    public Dictionary<string, byte> Ram = new();
    public List<BusLogEntry> Log = new();
    public List<string> ExpectedReads { get; set; }
    
    
    public void ResetBus()
    {
        Ram.Clear();
        Log.Clear();
    }

    public void SetRam(RamEntry[] entries)
    {
        foreach (RamEntry ramEntry in entries)
        {
            Ram[$"${ramEntry.Address:X4}"] = ramEntry.Value;
        }
    }

    public void AssertRam(string name, RamEntry[] entries)
    {
        using (new AssertionScope(name))
        {
            foreach (RamEntry entry in entries)
            {
                Ram.Should().ContainKey($"${entry.Address:X4}", "RAM ${0:X4} should exist", entry.Address)
                    .WhoseValue.Should().Be(entry.Value, "RAM ${0:X4}", entry.Address);
            }

            
            var expected = entries.Select(e => $"${e.Address:X4}").ToHashSet();
            var unexpected = Ram.Keys.Where(a => !expected.Contains(a));
            unexpected.Should().BeEmpty("the bus holds addresses the test doesn't expect");
        }
    }

    public void AssertCycles(string name, Cycle[] cycles)
    {
        using (new AssertionScope(name))
        {
            Log.Count.Should().Be(cycles.Length);

            for (int i = 0; i < cycles.Length; i++)
            {
                FormatLog(Log[i]).Should().Be(FormatCycle(cycles[i]));
            }
        }
    }

    public byte Read(ushort address)
    {
        if (Ram.TryGetValue($"${address:X4}", out byte value))
        {
            Log.Add(new() { Address = address, Value = value, Kind = "read" });
            return value;
        }

        if (ExpectedReads.Contains($"${address:X4}"))
        {
            Log.Add(new() { Address = address, Value = null, Kind = "read" });
            return 0;
        }

        AssertionChain.GetOrCreate().FailWith($"{{context}}: ${address:X4} not found.\r\nExisting Keys: [{string.Join(", ", Ram.Keys)}]");
        return 0;
    }

    public void Write(ushort address, byte value)
    {
        Ram[$"${address:X6}"] = value;
        Log.Add(new (){Address = address, Value = value, Kind = "write"});        
    }

    public void Wait()
    {
        Log.Add(new (){Kind = "wait"});       
    }
    
    private static string FormatCycle(Cycle c) => c.Outputs switch
    {
        "wait" => "wait",
        var k  => $"{k,-5} ${c.Address:X4} {c.Value:X2}",
    };

    private static string FormatLog(BusLogEntry e) => e.Kind switch
    {
        "wait" => "wait",
        var k  => $"{k,-5} ${e.Address:X4} {e.Value:X2}",
    };
}
