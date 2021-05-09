using System;

namespace NesEmu.Emulation
{
    public class InstructionCollection
    {
        private Registers _registers;

        public InstructionCollection(Registers registers)
        {
            _registers = registers;
        }
    }
}
