using System;

namespace NesEmu.Emulation
{
    public class Registers
    {
        public ushort ProgramCounter;
        public byte StackPointer = 0xff;
        public byte Accumulator;
        public byte IndexX;
        public byte IndexY;
        public bool Carry;
        public bool Zero;
        public bool InterruptDisable;
        public bool DecimalMode; // Unused in NES
        public bool Break;
        public bool Overflow;
        public bool Negative;
    }
}
