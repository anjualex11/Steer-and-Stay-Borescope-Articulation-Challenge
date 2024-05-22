# Servo Control Program

## Overview

This repository contains a C# implementation of a program to control the articulation of two tip-articulating servomotors (servos) in a video borescope. The program accepts input commands from a file passed as a command line argument or from STDIN and moves the servos accordingly. The input commands can be used to simulate joystick movements, touchscreen taps, servo calibration, and resetting the servo positions to their home position.

## Design Decisions and Assumptions

### Design Decisions:
- The program is implemented as a console application.
- Input commands are processed sequentially, with each command resulting in the movement of the servo positions and outputting their current positions.
- Constants are used to define parameters such as servo range, joystick dead band, and maximum servo position change percentage.
- Servo positions are clamped within their allowable range to ensure they do not exceed their limits.
- Joystick calibration allows setting minimum and maximum values for both X and Y axes.
- Touchscreen taps are converted to servo position adjustments based on distance and angle from the center of the screen.

### Assumptions:
- All input will be valid, and there's no need to check for illegal characters or malformed commands.
- Only one command input per line.
- Commands are not case-sensitive.
- Float math is used internally, but results are presented as integers.
- No timed component is included in the program; joystick and touchscreen inputs are sampled every 100ms.

## Programming Language Choice

I chose C# for this implementation due to several reasons:
- C# is well-suited for building console applications and handling input/output operations.
- Also I have strong command over C# programming language.
- It offers strong support for object-oriented programming, making it easier to organize the code into manageable components.
- C# has a rich standard library that provides functionalities for file handling, string manipulation, and mathematical calculations, which are essential for implementing this program.
- Additionally, C# offers good performance and memory management, ensuring efficient execution of the program.

## Running the Code and Tests

### Requirements:
- .NET Core SDK installed on your machine.

### Running the Code:
1. Clone the repository to your local machine. or you can directly clone from Visual Studio by providing the repository link.
2. Navigate to the directory containing the source code.
3. Run the program with an input file. or run the .exe file from bin/Release/SteerStayBorescope.exe.
4. Run the code in terminal "SteerStayBorescop.exe <input.txt>"
5. PLease use the sample input.txt file provided
6. Provide `<input_file_path>` with the path to the input file containing commands. Alternatively, you can provide input commands through STDIN.

### Running the Tests:
1. Ensure you have NUnit and NUnit3TestAdapter installed in your development environment.
2. Navigate to the directory containing the test project and run test cases.
