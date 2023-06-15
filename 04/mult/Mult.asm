// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)
//
// This program only needs to handle arguments that satisfy
// R0 >= 0, R1 >= 0, and R0*R1 < 32768.

// Put your code here.

@0
D = M
@i
M = D
@1
D = M
@j
M = D
@sum
M = 0
@i
D = M
@DONE
D;JEQ
@j
D = M
@DONE
D;JEQ
(ADD)
    @i // set the address to variable i
    D = M // load the data from memory to the register D
    @DONE
    D;JEQ // if the counter is zero it means the multiplication is done
    D = D - 1 // decrease the counter
    @i
    M = D // store the new counter value back to memory
    @sum // load the value of the sum variable
    D=M // load the value into the register
    @j // set the addres value to the j variable
    D = D + M // add the variable to the sum
    @sum // store the sum back to memory    
    M=D
    @ADD
    0;JMP
(DONE)
    @sum
    D = M
    @2
    M = D
    @DONE
    0;JMP

