using System;

namespace NesEmu.Emulation
{
    public partial class InstructionCollection
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
    }
}
