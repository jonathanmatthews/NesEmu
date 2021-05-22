using System;

namespace NesEmu.Emulation
{
    public partial class InstructionCollection
    {
        /// <summary>
        /// Break
        /// </summary>
        public void BRK()
        {
            throw new NotImplementedException("BRK not implemented.");
        }

        /// <summary>
        /// Branch on plus, returns new PC value
        /// </summary>
        public ushort BPL(byte relativeAddress)
        {
            if (!_registers.Negative)
            {
                var PC = (ushort)(_registers.ProgramCounter + relativeAddress);
                if (relativeAddress > 127) PC -= 256;
                return PC;
            }
            else
                return (ushort)(_registers.ProgramCounter + 2);
        }

        /// <summary>
        /// Branch on minus, returns new PC value
        /// </summary>
        public ushort BMI(byte relativeAddress)
        {
            if (_registers.Negative)
            {
                var PC = (ushort)(_registers.ProgramCounter + relativeAddress);
                if (relativeAddress > 127) PC -= 256;
                return PC;
            }
            else
                return (ushort)(_registers.ProgramCounter + 2);
        }

        /// <summary>
        /// Branch on overflow clear
        /// </summary>
        public ushort BVC(byte relativeAddress)
        {
            if (!_registers.Overflow)
            {
                var PC = (ushort)(_registers.ProgramCounter + relativeAddress);
                if (relativeAddress > 127) PC -= 256;
                return PC;
            }
            else
                return (ushort)(_registers.ProgramCounter + 2);
        }

        /// <summary>
        /// Branch on overflow set
        /// </summary>
        public ushort BVS(byte relativeAddress)
        {
            if (_registers.Overflow)
            {
                var PC = (ushort)(_registers.ProgramCounter + relativeAddress);
                if (relativeAddress > 127) PC -= 256;
                return PC;
            }
            else
                return (ushort)(_registers.ProgramCounter + 2);
        }

        /// <summary>
        /// Branch on carry clear
        /// </summary>
        public ushort BCC(byte relativeAddress)
        {
            if (!_registers.Carry)
            {
                var PC = (ushort)(_registers.ProgramCounter + relativeAddress);
                if (relativeAddress > 127) PC -= 256;
                return PC;
            }
            else
                return (ushort)(_registers.ProgramCounter + 2);
        }

        /// <summary>
        /// Branch on carry set
        /// </summary>
        public ushort BCS(byte relativeAddress)
        {
            if (_registers.Carry)
            {
                var PC = (ushort)(_registers.ProgramCounter + relativeAddress);
                if (relativeAddress > 127) PC -= 256;
                return PC;
            }
            else
                return (ushort)(_registers.ProgramCounter + 2);
        }

        /// <summary>
        /// Clear carry flag
        /// </summary>
        public void CLC()
        {
            _registers.Carry = false;
        }

        /// <summary>
        /// Set carry flag
        /// </summary>
        public void SEC()
        {
            _registers.Carry = true;
        }

        /// <summary>
        /// Clear the overflow flag
        /// </summary>
        public void CLV()
        {
            _registers.Overflow = false;
        }

        /// <summary>
        /// Clear the interrupt disable flag
        /// </summary>
        public void CLI()
        {
            _registers.InterruptDisable = false;
        }

        /// <summary>
        /// Set the interrupt disable flag
        /// </summary>
        public void SEI()
        {
            _registers.InterruptDisable = true;
        }

        /// <summary>
        /// Jump to absolute or indirect address, returns new program counter
        /// </summary>
        public ushort JMP(ushort address, bool isIndirectAddress)
        {
            return isIndirectAddress ?
                (ushort)(_memory[address] + (_memory[(ushort)(address + 1)] << 8)) :
                address;
        }

        /// <summary>
        /// Jump to subroutine, returns new PC value
        /// </summary>
        public ushort JSR(ushort subroutineAddress)
        {
            // Technically, this is the address right before the next instruction,
            // but RTS will increment by 1 to correct for this.
            var nextInstruction = (ushort)(_registers.ProgramCounter + 2);
            var significantByte = (byte)(nextInstruction >> 4);
            var insignificantByte = (byte)nextInstruction; // Modulo 0x100

            // Push order must be inverse of RTS
            _memory.PushStack(significantByte);
            _memory.PushStack(insignificantByte);

            return subroutineAddress;
        }

        /// <summary>
        /// Return from subroutine, returns new PC value
        /// </summary>
        public ushort RTS()
        {
            // Pop order must be inverse of JSR
            var insignificantByte = _memory.PopStack();
            var significantByte = _memory.PopStack() << 4;
            return (ushort)(insignificantByte + significantByte + 1);
        }

        /// <summary>
        /// Return from interrupt
        /// </summary>
        public ushort RTI()
        {
            // Pop order must be inverse of interrupt order
            PLP();
            return RTS(); // RTS effectively just pops PC from stack
        }
    }
}
