using System;

namespace NesEmu.Emulation
{
    public partial class InstructionCollection
    {
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
        /// Store the accumulator at the address given
        /// </summary>
        public void STA(ushort absoluteAddress)
        {
            _memory[absoluteAddress] = _registers.Accumulator;
        }

        /// <summary>
        /// Load the accumulator from the address given
        /// </summary>
        public void LDA(ushort absoluteAddress)
        {
            _registers.Accumulator = _memory[absoluteAddress];
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// Store the Y register at the address given
        /// </summary>
        public void STY(ushort absoluteAddress)
        {
            _memory[absoluteAddress] = _registers.IndexY;
        }

        /// <summary>
        /// Load the Y register from the address given
        /// </summary>
        public void LDY(ushort absoluteAddress)
        {
            _registers.IndexY = _memory[absoluteAddress];
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// Store the X register at the address given
        /// </summary>
        public void STX(ushort absoluteAddress)
        {
            _memory[absoluteAddress] = _registers.IndexX;
        }

        /// <summary>
        /// Load the X register from the address given
        /// </summary>
        public void LDX(ushort absoluteAddress)
        {
            _registers.IndexX = _memory[absoluteAddress];
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// Transfer X to the accumulator
        /// </summary>
        public void TXA()
        {
            _registers.Accumulator = _registers.IndexX;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// Transfer the accumulator to X
        /// </summary>
        public void TAX()
        {
            _registers.IndexX = _registers.Accumulator;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// Transfer Y to the accumulator
        /// </summary>
        public void TYA()
        {
            _registers.Accumulator = _registers.IndexY;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }

        /// <summary>
        /// Transfer the accumulator to Y
        /// </summary>
        public void TAY()
        {
            _registers.IndexY = _registers.Accumulator;
            _registers.Zero = _registers.Accumulator == 0;
            _registers.Negative = _registers.Accumulator > 127;
        }
    }
}
