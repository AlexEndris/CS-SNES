namespace Hardware.Apu;

using static Operand;

public partial class Processor
{
    private Operand Immediate()
    {
        ushort address = Registers.PC;
        Registers.PC++;
        return CrossPage(address);
    }

    private Operand DirectPage()
    {
        byte address = FetchByte();
        byte page = Page();
        return FixedPage(page, address);
    }

    private Operand DirectPageY()
    {
        byte address = FetchByte();
        byte page = Page();
        Wait();
        return FixedPage(page, (byte)(address + Registers.Y));
    }

    private Operand DirectPageX()
    {
        byte address = FetchByte();
        byte page = Page();
        Wait();
        return FixedPage(page, (byte)(address + Registers.X));
    }

    private Operand Absolute()
    {
        byte lowAddress = FetchByte();
        byte highAddress = FetchByte();

        ushort address = (ushort)(highAddress << 8 | lowAddress);

        return CrossPage(address);
    }

    private Operand AbsoluteX()
    {
        byte lowAddress = FetchByte();
        byte highAddress = FetchByte();

        Wait();
        ushort address = (ushort)((highAddress << 8 | lowAddress) + Registers.X);
        
        return CrossPage(address);
    }

    private Operand AbsoluteY()
    {
        byte lowAddress = FetchByte();
        byte highAddress = FetchByte();

        Wait();
        ushort address = (ushort)((highAddress << 8 | lowAddress) + Registers.Y);

        return CrossPage(address);
    }
}
