# RULE SET


## General
- pointer starts at 0, jumps 2 steps each iteration, unless we have jump opcodes
- we read this element as opcode and pass the next operand to it


## Operands

Literal operand
- the operand itself
- 7 = 7, 1 = 1

Combo operand
- 0-3 => literal values 0-3
- 4 => value of register A
- 5 => value of register B
- 6 => value of register C
- 7 reserved, will not appear in valid programs


## Instructions

opcode 0 adv
- register A / 2^(combo operand of the instruction)
- result is truncated to a integer and written to register A

opcode 1 bxl
- bitwise XOR of register B and the instructions literal operand
- result stored in register B

opcode 2 bst
- combo operand % 8
- result stored in register B

opcode 3 jnz
- if A = 0, do nothing
- if not, jumps by setting instruction pointer to the value of its literal operand
- if instruction jumps, the instruction pointer is not increased by 2 after this instruction

opcode 4 bxc
- bitwise XOR of register B and register C
- stores the result in register B
- does nothing to its operand

opcode 5 out
- combo operand % 8
- outputs the value + ,

opcode 6 bdv
- just like the adv instruction, but value stored in register B

opcode 7 cdv
- just like the adv instruction, but value stored in register C


## Implementation

Functions
- GetComboOperand(int value)
- Opcode1 - opcode7
- ResolveOpcode(int opcode)
- MovePointer()
- WriteOutput

Variables
- Register A, B, C
- Output
- pointerJumpOne
