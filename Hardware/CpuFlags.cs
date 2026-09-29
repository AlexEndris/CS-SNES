namespace Hardware;

public enum CpuFlags : byte
{
    Carry = 1, // 0b00000001
    Zero = 1 << 1, // 0b00000010
    IrqDisable = 1 << 2, // 0b00000100
    Decimal = 1 << 3, // 0b00001000
    XSizeBreak = 1 << 4, // 0b00010000
    ASize = 1 << 5, // 0b00100000
    Overflow = 1 << 6, // 0b01000000
    Negative = 1 << 7 // 0b10000000  
}
