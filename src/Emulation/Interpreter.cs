using System;

namespace NesEmu.Emulation
{
    public class Interpreter
    {
        private readonly InstructionCollection _instructions;
        private readonly Registers _registers;
        private readonly NesMemory _memory;
        private readonly IndexedToAbsoluteConverterService _addressConverter;

        public Interpreter(
            InstructionCollection instructionCollection,
            Registers registers,
            NesMemory memory,
            IndexedToAbsoluteConverterService addressConverter)
        {
            _instructions = instructionCollection;
            _registers = registers;
            _memory = memory;
            _addressConverter = addressConverter;
        }

        public void DecodeAndExecute(byte opcode)
        {
            switch (opcode)
            {
                case 0x00:
                case 0x01:
                default:
                    throw new NotImplementedException($"Instruction 0x{opcode.ToString("X2")} not implemented.");
            }
        }
    }
}
