namespace Hardware.Tests.Apu.Models;

public class InstructionTestData
{
    public string Name { get; set; }

    public ApuState Initial { get; set; }

    public ApuState Final { get; set; }

    public Cycle[] Cycles { get; set; }
}