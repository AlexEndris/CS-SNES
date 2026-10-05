namespace Hardware.Tests.Models;

using AwesomeAssertions;
using AwesomeAssertions.Execution;

using Cpu;

public class TestCpuBus : ICpuBus
{
    public Dictionary<uint, byte> Ram = new();
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
            Ram[ramEntry.Address] = ramEntry.Value;
        }
    }

    public void AssertRam(string name, RamEntry[] entries)
    {
        using (new AssertionScope(name))
        {
            foreach (RamEntry entry in entries)
            {
                Ram.Should().ContainKey(entry.Address, "RAM ${0:X6} should exist", entry.Address)
                    .WhoseValue.Should().Be(entry.Value, "RAM ${0:X6}", entry.Address);
            }

            
            var expected = entries.Select(e => e.Address).ToHashSet();
            var unexpected = Ram.Keys.Where(a => !expected.Contains(a)).Select(a => $"${a:X6}");
            unexpected.Should().BeEmpty("the bus holds addresses the test doesn't expect");
        }
    }

    public void AssertCycles(string name, Cycle[] cycles)
    {
        using (new AssertionScope())
        {
            var expected = cycles.Select(FormatCycle).ToList();
            var actual = Log.Select(FormatLog).ToList();
            
            actual.Should().Equal(expected);
        }
    }
    
    public byte Read(uint address)
    {
        byte value = Ram[address];
        Log.Add(new (){Address = address, Value = value, Kind = "read"});        
        return value;
    }

    public void Write(uint address, byte value)
    {
        Ram[address] = value;
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
