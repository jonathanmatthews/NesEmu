using System;

namespace NesEmu.Emulation
{
    public class InstructionCollection
    {
        private readonly Registers _registers;
        private readonly NesMemory _memory;

        public InstructionCollection(
            Registers registers,
            NesMemory memory)
        {
            _registers = registers;
            _memory = memory;
        }

        /// <summary>
        /// Break
        /// </summary>
        public void BRK()
        {
            throw new NotImplementedException("BRK not implemented.");
        }

        /// <summary>
        /// OR data at memory address with accumulator and store in accumulator
        /// </summary>
        public void ORA(ushort operandAddress)
        {
            _registers.Accumulator |= _memory[operandAddress];
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// OR data given explicitly with accumulator and store in accumulator
        /// </summary>
        public void ORA(byte data)
        {
            _registers.Accumulator |= data;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// XOR data at memory address with accumulator and store in accumulator
        /// </summary>
        public void EOR(ushort operandAddress)
        {
            _registers.Accumulator ^= _memory[operandAddress];
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// XOR data given explicitly with accumulator and store in accumulator
        /// </summary>
        public void EOR(byte data)
        {
            _registers.Accumulator ^= data;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// AND of accumulator and memory at address, stores result in accumulator
        /// </summary>
        public void AND(ushort operandAddress)
        {
            _registers.Accumulator &= _memory[operandAddress];
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// AND data given explicitly with accumulator, store result in accumulator
        /// </summary>
        public void AND(byte data)
        {
            _registers.Accumulator &= data;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// BIT test data at address without modifying
        /// </summary>
        public void BIT(ushort operandAddress)
        {
            var data = _memory[operandAddress];
            var result = _registers.Accumulator & data;

            _registers.Zero = result == 0;
            _registers.Negative = data > 127;
            _registers.Overflow = (data & 0b01000000) > 0;
        }

        /// <summary>
        /// Bit shift left the memory at address by 1
        /// </summary>
        public void ASL(ushort operandAddress)
        {
            var input = _memory[operandAddress];
            var result = (byte)(input << 1);

            _registers.Carry = (input & 0b10000000) > 0;
            _registers.Negative = (result & 0b10000000) > 0;
            _registers.Zero = result == 0;
            _memory[operandAddress] = result;
        }

        /// <summary>
        /// Bit shift left the accumulator by 1
        /// </summary>
        public void ASL()
        {
            _registers.Carry = (_registers.Accumulator & 0b10000000) > 0;
            _registers.Accumulator <<= 1;
            _registers.Negative = (_registers.Accumulator & 0b10000000) > 0;
            _registers.Zero = _registers.Accumulator == 0;
        }

        /// <summary>
        /// Logical shift right the data at the address by 1
        /// </summary>
        public void LSR(ushort operandAddress)
        {
            var input = _memory[operandAddress];
            var result = (byte)(input >> 1);

            _registers.Carry = (input & 0b00000001) > 0;
            _registers.Negative = false;
            _registers.Zero = result == 0;
            _memory[operandAddress] = result;
        }
        
        /// <summary>
        /// Logical shift right the data in the accumulator by 1
        /// </summary>
        public void LSR()
        {
            _registers.Carry = (_registers.Accumulator & 0b00000001) > 0;
            _registers.Accumulator >>= 1;
            _registers.Negative = false;
            _registers.Zero = _registers.Accumulator == 0;
        }

        /// <summary>
        /// Rotate left the memory at address with carry flag
        /// </summary>
        public void ROL(ushort operandAddress)
        {
            var data = _memory[operandAddress];
            var result = (byte)(data << 1);
            if (_registers.Carry) result++;

            _registers.Carry = data > 127;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;

            _memory[operandAddress] = (byte)result;
        }

        /// <summary>
        /// Rotate left the value in the accumulator with carry flag
        /// </summary>
        public void ROL()
        {
            var result = (byte)(_registers.Accumulator << 1);
            if (_registers.Carry) result++;

            _registers.Carry = _registers.Accumulator > 127;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;

            _registers.Accumulator = (byte)result;
        }

        /// <summary>
        /// Push processor status onto stack
        /// </summary>
        public void PHP()
        {
            _memory.PushStatusToStack(false);
        }

        /// <summary>
        /// Pull processor status from stack
        /// </summary>
        public void PLP()
        {
            _memory.PopStatusFromStack();
        }

        /// <summary>
        /// Push accumulator to stack
        /// </summary>
        public void PHA()
        {
            _memory.PushStack(_registers.Accumulator);
        }

        /// <summary>
        /// Pull accumulator from stack
        /// </summary>
        public void PLA()
        {
            _registers.Accumulator = _memory.PopStack();
            _registers.Negative = _registers.Accumulator > 127;
            _registers.Zero = _registers.Accumulator == 0;
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
