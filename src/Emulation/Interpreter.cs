using System;

namespace NesEmu.Emulation
{
    public class Interpreter
    {
        private readonly InstructionCollection _instructions;
        private readonly Registers _registers;
        private readonly byte[] _memory = new byte[0xffff];

        public Interpreter(InstructionCollection instructionCollection, Registers registers)
        {
            _instructions = instructionCollection;
            _registers = registers;
        }

        public void LoadProgram(byte[] program)
        {
            if (program.Length > _memory.Length)
            {
                throw new ArgumentException("Program does not fit into memory.");
            }
            else
            {
                Array.Copy(program, _memory, program.Length);
            }
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
