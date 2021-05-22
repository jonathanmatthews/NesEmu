using System;

namespace NesEmu.Emulation
{
    public partial class InstructionCollection
    {
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
        /// Add with carry the accumulator and the memory at address, store result in accumulator
        /// </summary>
        public void ADC(ushort operandAddress)
        {
            var result = _registers.Accumulator + _memory[operandAddress];

            _registers.Carry = result > 255;
            _registers.Overflow = (result & 0b10000000) != (_registers.Accumulator & 0b10000000);
            _registers.Accumulator = (byte)result;
            _registers.Negative = _registers.Accumulator > 127;
            _registers.Zero = _registers.Accumulator == 0;
        }

        /// <summary>
        /// Add with carry the accumulator and data given explicitly, store result in accumulator
        /// </summary>
        public void ADC(byte data)
        {
            var result = _registers.Accumulator + data;

            _registers.Carry = result > 255;
            _registers.Overflow = (result & 0b10000000) != (_registers.Accumulator & 0b10000000);
            _registers.Accumulator = (byte)result;
            _registers.Negative = _registers.Accumulator > 127;
            _registers.Zero = _registers.Accumulator == 0;
        }

        /// <summary>
        /// Compare accumulator with data at address
        /// </summary>
        public void CMP(ushort operandAddress)
        {
            var data = _memory[operandAddress];
            var result = _registers.Accumulator - data;

            _registers.Negative = result > 127;
            _registers.Zero = result == 0;
            _registers.Carry = data <= _registers.Accumulator;
        }

        /// <summary>
        /// Compare accumulator with data given explicitly
        /// </summary>
        public void CMP(byte data)
        {
            var result = _registers.Accumulator - data;

            _registers.Negative = result > 127;
            _registers.Zero = result == 0;
            _registers.Carry = data <= _registers.Accumulator;
        }

        /// <summary>
        /// Compare Y with data at address
        /// </summary>
        public void CPY(ushort operandAddress)
        {
            var data = _memory[operandAddress];
            var result = _registers.IndexY - data;

            _registers.Negative = result > 127;
            _registers.Zero = result == 0;
            _registers.Carry = data <= _registers.IndexY;
        }
        
        /// <summary>
        /// Compare Y with data given explicitly
        /// </summary>
        public void CPY(byte data)
        {
            var result = _registers.IndexY - data;

            _registers.Negative = result > 127;
            _registers.Zero = result == 0;
            _registers.Carry = data <= _registers.IndexY;
        }
        
        /// <summary>
        /// Compare X with data at address
        /// </summary>
        public void CPX(ushort operandAddress)
        {
            var data = _memory[operandAddress];
            var result = _registers.IndexX - data;

            _registers.Negative = result > 127;
            _registers.Zero = result == 0;
            _registers.Carry = data <= _registers.IndexX;
        }
        
        /// <summary>
        /// Compare X with data given explicitly
        /// </summary>
        public void CPX(byte data)
        {
            var result = _registers.IndexX - data;

            _registers.Negative = result > 127;
            _registers.Zero = result == 0;
            _registers.Carry = data <= _registers.IndexX;
        }
    }
}
