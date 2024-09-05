# Atea Bitwise Parser assignment:
By Bjørn Kristiansen


## Project structure:
./Assignment: Contains the files given from Atea.

./BitwiseParser: Contains the program.

./BitwiseParserTest: Contains the unit test.

## Running the Program:
Before running, ensure 'input.txt' contains your circuit instructions, and it is placed in the same folder as your binaries. 

Run the program, and it will print the signal value for the given wire.

## How to test:
Open your favorite CLI and type in the following: 

dotnet test BitwiseParserTest/BitwiseParserTest.csproj

## Explanation:
wireSignals stores the computed signals of wires.

instructions keeps track of the raw instructions for each wire.

## GetSignal Method:
This method recursively computes the signal for a wire by processing its instruction.

Supporting direct assignment ( -> ), NOT operations, and binary operations (AND, OR, LSHIFT, RSHIFT).

## AddInstruction Method:
Parses and stores each instruction into the instructions dictionary.

## Main Method:
Reads all lines from input.txt.

Adds each instruction to the CircuitSolver.

Computes and prints the signal for the given wire.

## Result: 
'a': 46065
