using System;

namespace NesEmu.Emulation
{
    public class NesMemory
    {
        private readonly byte[] _internalMemory = new byte[0x800];
        private readonly byte[] _ppuRegisters = new byte[0x8];
        private readonly byte[] _apuIoRegisters = new byte[0x18];
        private readonly byte[] _cartridgeSpace = new byte[0xbfe0];
        private readonly Registers _registers;

        public NesMemory(Registers registers)
        {
            _registers = registers;
        }

        public byte this[ushort pointer]
        {
            get
            {
                if (pointer < 0x2000) return _internalMemory[pointer % 0x800];
                else if (pointer < 0x4000) return _ppuRegisters[pointer % 0x8];
                else if (pointer < 0x4018) return _apuIoRegisters[pointer - 0x4000];
                else if (pointer < 0x4020)
                    throw new InvalidOperationException("CPU test mode disabled");
                else return _cartridgeSpace[pointer - 0xbfe0];
            }

            set
            {
                if (pointer < 0x2000) _internalMemory[pointer % 0x800] = value;
                else if (pointer < 0x4000) _ppuRegisters[pointer % 0x8] = value;
                else if (pointer < 0x4018) _apuIoRegisters[pointer - 0x4000] = value;
                else if (pointer < 0x4020)
                    throw new InvalidOperationException("CPU test mode disabled");
                else _cartridgeSpace[pointer - 0xbfe0] = value;
            }
        }

        public byte PopStack()
            => _internalMemory[++_registers.StackPointer + 0x100];

        public byte PushStack(byte data)
            => _internalMemory[_registers.StackPointer-- + 0x100] = data;

        public void PushStatusToStack(bool isInterrupt)
        {
            byte status = 0;
            if (_registers.Negative) status += 0b10000000;
            if (_registers.Overflow) status += 0b01000000;
            status += 0b00100000; // Always set to 1
            if (!isInterrupt) status += 0b00010000; // Set by instructions, not interrupts
            if (_registers.DecimalMode) status += 0b00001000;
            if (_registers.InterruptDisable) status += 0b00000100;
            if (_registers.Zero) status += 0b00000010;
            if (_registers.Carry) status += 0b00000001;

            PushStack(status);
        }

        public void PopStatusFromStack()
        {
            byte status = PopStack();

            _registers.Negative = (status & 0b10000000) > 0;
            _registers.Overflow = (status & 0b01000000) > 0;
            // Don't load B flag in bits 5 and 4
            _registers.DecimalMode = (status & 0b00001000) > 0;
            _registers.InterruptDisable = (status & 0b00000100) > 0;
            _registers.Zero = (status & 0b00000010) > 0;
            _registers.Carry = (status & 0b00000001) > 0;
        }
    }
}