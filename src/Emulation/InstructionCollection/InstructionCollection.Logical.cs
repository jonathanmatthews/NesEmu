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
    }
}
