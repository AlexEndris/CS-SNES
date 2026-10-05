namespace Hardware.Tests.Models;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Cpu;

public class TestCpuBus : ICpuBus
{
    public Dictionary<string, byte> Ram = new();
    public List<BusLogEntry> Log = new();

    public void ResetBus()
    {
        Ram.Clear();
        Log.Clear();
    }

    public void SetRam(RamEntry[] entries)
    {
        foreach (RamEntry ramEntry in entries)
        {
            Ram[$"${ramEntry.Address:X6}"] = ramEntry.Value;
        }
    }

    public void AssertRam(string name, RamEntry[] entries)
    {
        using (new AssertionScope(name))
        {
            foreach (RamEntry entry in entries)
            {
                Ram.Should().ContainKey($"${entry.Address:X6}", "RAM ${0:X6} should exist", entry.Address)
                    .WhoseValue.Should().Be(entry.Value, "RAM ${0:X6}", entry.Address);
            }

            
            var expected = entries.Select(e => $"${e.Address:X6}").ToHashSet();
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
    
    public byte Read(uint address)
    {
        try
        {
            byte value = Ram[$"${address:X6}"];
            Log.Add(new (){Address = address, Value = value, Kind = "read"});      
            return value;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public void Write(uint address, byte value)
    {
        Ram[$"${address:X6}"] = value;
        Log.Add(new (){Address = address, Value = value, Kind = "write"});        
    }

    public void Idle()
    {
        Log.Add(new (){Kind = "idle"});       
    }
    
    private static string FormatCycle(Cycle c) => KindOf(c) switch
    {
        "idle" => "idle",
        var k  => $"{k,-5} ${c.Address:X6} {c.Value:X2}",
    };

    private static string FormatLog(BusLogEntry e) => e.Kind switch
    {
        "idle" => "idle",
        var k  => $"{k,-5} ${e.Address:X6} {e.Value:X2}",
    };
    
    private static string KindOf(Cycle c)
    {
        var f = c.Outputs;
        bool valid = f[0] == 'd' || f[1] == 'p' || f[2] == 'v';

        if (!valid)
            return "idle";

        return f[3] == 'w' ? "write" : "read";
    }
}
