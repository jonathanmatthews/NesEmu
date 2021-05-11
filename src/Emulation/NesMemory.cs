using System;

namespace NesEmu.Emulation
{
    public class NesMemory
    {
        private readonly byte[] _memory = new byte[0x10000];

        public byte this[ushort pointer]
        {
            get => _memory[pointer];
            set => _memory[pointer] = value;
        }

        public byte GetStackValue(byte stackAddress)
            => _memory[0x0100 + stackAddress];

        public void SetStackValue(byte stackAddress, byte value)
            => _memory[0x0100 + stackAddress] = value;
    }
}