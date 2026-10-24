namespace Hardware.Apu;

public partial class Processor
{
    private void Execute(byte opcode)
    {
        switch (opcode)
        {
            default:
                throw new NotImplementedException($"Opcode ${opcode:X2}");
        }
    }
}
