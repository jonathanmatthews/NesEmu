using System;

namespace NesEmu.Emulation
{
    public class Interpreter
    {
        private readonly InstructionCollection _instructions;
        private readonly Registers _registers;
        private readonly NesMemory _memory;

        public Interpreter(InstructionCollection instructionCollection,
            Registers registers, NesMemory memory)
        {
            _instructions = instructionCollection;
            _registers = registers;
            _memory = memory;
        }

        public void DecodeAndExecute(byte opcode)
        {
            switch (opcode)
            {
                case 0x00:
                case 0x01:
                default:
                    throw new NotImplementedException("Instruction not yet implemented");
            }
        }
    }
}
