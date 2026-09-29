namespace Hardware.Tests.Models;

public class InstructionTestData
{
    public string Name { get; set; }

    public CpuState Initial { get; set; }

    public CpuState Final { get; set; }

    public Cycle[] Cycles { get; set; }
}