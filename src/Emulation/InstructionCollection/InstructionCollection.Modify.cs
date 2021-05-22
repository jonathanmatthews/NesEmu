using System;

namespace NesEmu.Emulation
{
    public partial class InstructionCollection
    {
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
        /// Rotate right the memory at address with carry flag
        /// </summary>
        public void ROR(ushort operandAddress)
        {
            var data = _memory[operandAddress];
            var result = (byte)(data >> 1);
            if (_registers.Carry) result += 0b10000000;

            _registers.Carry = (data & 1) == 1;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }

        /// <summary>
        /// Rotate right the value in the accumulator with carry flag
        /// </summary>
        public void ROR()
        {
            var result = (byte)(_registers.Accumulator >> 1);
            if (_registers.Carry) result += 0b10000000;

            _registers.Carry = (_registers.Accumulator & 1) == 1;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;

            _registers.Accumulator = (byte)result;
        }

        /// <summary>
        /// Decrement memory at given address
        /// </summary>
        public void DEC(ushort operandAddress)
        {
            var result = --_memory[operandAddress];
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }

        /// <summary>
        /// Increment memory at given address
        /// </summary>
        public void INC(ushort operandAddress)
        {
            var result = ++_memory[operandAddress];
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }

        /// <summary>
        /// Decrement Y register
        /// </summary>
        public void DEY()
        {
            var result = --_registers.IndexY;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }

        /// <summary>
        /// Increment Y register
        /// </summary>
        public void INY()
        {
            var result = ++_registers.IndexY;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }

        /// <summary>
        /// Decrement X register
        /// </summary>
        public void DEX()
        {
            var result = --_registers.IndexX;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }

        /// <summary>
        /// Increment X register
        /// </summary>
        public void INX()
        {
            var result = ++_registers.IndexX;
            _registers.Zero = result == 0;
            _registers.Negative = result > 127;
        }
    }
}
