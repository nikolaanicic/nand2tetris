// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel;
// the screen should remain fully black as long as the key is pressed. 
// When no key is pressed, the program clears the screen, i.e. writes
// "white" in every pixel;
// the screen should remain fully clear as long as no key is pressed.

// Put your code here.

@i
M = 0   

@status
M = 0

@ARG
M = 0


(LOOP)

    @POLL_KBD
    0;JMP

    (CHECK_STATUS)
        @ARG
        D = M
        @status
        D = D - M
        // checks if the screen should be filled with 
        @LOOP       // a diff colour 
        D;JEQ

    @ARG
    D = M
    @status
    M=D  
    @8192
    D=A
    @SCREEN
    D = A + D
    @i
    M = D

    (DRAW_LOOP)
        @i     // set the pointer address
        D = M  // load the pointer
        D = D - 1 // decrease the pointer
        M=D       // save the pointer to memory
        @16383    // load the constant @SCREEN-1
        D=A  
        @i     
        D = M - D // check if the drawing is done
        @LOOP
        D;JLE     

        @status  // load the current screen status
        D = M    
        @i
        A = M  // set the current pointer address and set the pixel
        M = D
        @DRAW_LOOP
        0;JMP
        

(POLL_KBD)
    @KBD
    D = M
    @SET_STATUS_BLACK
    D;JNE
    @SET_STATUS_WHITE
    D;JEQ

(SET_STATUS_BLACK)
    @ARG
    M = -1
    @CHECK_STATUS
    0;JMP

(SET_STATUS_WHITE)
    @ARG
    M = 0
    @CHECK_STATUS
    0;JMP
