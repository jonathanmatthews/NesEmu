using System;

namespace NesEmu.Emulation
{
    public class IndexedToAbsoluteConverterService
    {
        private readonly Registers _registers;
        private readonly NesMemory _memory;

        public IndexedToAbsoluteConverterService(
            Registers registers,
            NesMemory memory)
        {
            _registers = registers;
            _memory = memory;
        }

        public ushort AbsoluteIndexedX(byte baseAddressInsignificant, byte baseAddressSignificant)
            => (ushort)(_registers.IndexX + baseAddressInsignificant + (baseAddressSignificant << 8));

        public ushort AbsoluteIndexedY(byte baseAddressInsignificant, byte baseAddressSignificant)
            => (ushort)(_registers.IndexY + baseAddressInsignificant + (baseAddressSignificant << 8));

        public ushort ZeroPagedIndexedX(byte baseAddress)
            => (byte)(_registers.IndexX + baseAddress); // Modulo 0x100
        
        public ushort ZeroPagedIndexedY(byte baseAddress)
            => (byte)(_registers.IndexY + baseAddress); // Modulo 0x100

        public ushort IndexedIndirectX(byte baseAddress)
        {
            var insignificantByte = (ushort)(_registers.IndexX + baseAddress);
            var significantByte = (ushort)(insignificantByte + 1);
            var absoluteAddress = _memory[insignificantByte] + (_memory[significantByte] << 8);
            return (ushort)absoluteAddress;
        }

        public ushort IndirectIndexedY(byte baseAddress)
        {
            var insignificantByte = (ushort)baseAddress;
            var significantByte = (ushort)(insignificantByte + 1);
            var partialAddress = _memory[insignificantByte] + (_memory[significantByte] << 8);
            var absoluteAddress = partialAddress + _registers.IndexY;
            return (ushort)absoluteAddress;
        }
    }
}
