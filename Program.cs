using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        ServoController servoController = new ServoController();
        string line;

        // Read input from file or STDIN
        if (args.Length > 0)
        {
            string fileName = args[0];
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                servoController.ParseCommand(line);
            }
            file.Close();
        }
        else
        {
            while ((line = Console.ReadLine()) != null)
            {
                servoController.ParseCommand(line);
            }
        }
    }
}

public class ServoController
{
    public int ServoX { get; private set; }
    public int ServoY { get; private set; }

    public int JoystickCenterX { get; private set; }
    public int JoystickCenterY { get; set; }

    private int servoX = 180;
    private int servoY = 180;
    private int joystickCenterX = 500;
    private int joystickCenterY = 500;
    private int joystickMinX = 0;
    private int joystickMinY = 0;
    private int joystickMaxX = 1023;
    private int joystickMaxY = 1023;
    private int joystickDeadband = 50;
    private int servoRange = 80;
    private int maxSlewRate = 25;

    public void ParseCommand(string command)
    {
        string[] parts = command.Split(' ');
        string cmd = parts[0].ToLower();

        switch (cmd)
        {
            case "joystick":
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);
                HandleJoystick(x, y);
                break;
            case "touch":
                int touchX = int.Parse(parts[1]);
                int touchY = int.Parse(parts[2]);
                int maxX = int.Parse(parts[3]);
                int maxY = int.Parse(parts[4]);
                HandleTouch(touchX, touchY, maxX, maxY);
                break;
            case "calibrate":
                int calX = int.Parse(parts[1]);
                int calY = int.Parse(parts[2]);
                string direction = parts[3].ToUpper();
                CalibrateJoystick(calX, calY, direction);
                break;
            case "home":
                ResetToHome();
                break;
            default:
                break;
        }

        OutputServoPositions();
    }

    private void HandleJoystick(int x, int y)
    {
        // Adjust joystick position based on calibration
        x -= joystickCenterX;
        y -= joystickCenterY;

        // Apply deadband
        if (Math.Abs(x) < joystickDeadband)
            x = 0;
        if (Math.Abs(y) < joystickDeadband)
            y = 0;

        // Calculate maximum position change
        int maxXChange = (int)(servoRange * 0.025);
        int maxYChange = (int)(servoRange * 0.025);

        // Limit position change
        x = Math.Min(Math.Max(x, -maxXChange), maxXChange);
        y = Math.Min(Math.Max(y, -maxYChange), maxYChange);

        // Update servo positions
        servoX = Math.Min(Math.Max(servoX + x, 180 - servoRange), 180 + servoRange);
        servoY = Math.Min(Math.Max(servoY + y, 180 - servoRange), 180 + servoRange);
    }

    private void HandleTouch(int touchX, int touchY, int maxX, int maxY)
    {
        // Calculate distance and angle from center
        double distance = Math.Sqrt(Math.Pow(touchX - maxX / 2, 2) + Math.Pow(touchY - maxY / 2, 2));
        double angle = Math.Atan2(touchY - maxY / 2, touchX - maxX / 2) * 180 / Math.PI;

        // Calculate servo position change
        int maxChange = (int)(servoRange * 0.1);
        int minChange = (int)(servoRange * 0.01);
        int change = (int)Math.Min(Math.Max(distance / (maxX / 2) * maxChange, minChange), maxChange);

        // Update servo positions based on angle
        if (angle >= -45 && angle < 45)
            servoX = Math.Min(Math.Max(servoX + change, 180 - servoRange), 180 + servoRange);
        else if (angle >= 45 && angle < 135)
            servoY = Math.Min(Math.Max(servoY + change, 180 - servoRange), 180 + servoRange);
        else if (angle >= -135 && angle < -45)
            servoY = Math.Min(Math.Max(servoY - change, 180 - servoRange), 180 + servoRange);
        else
            servoX = Math.Min(Math.Max(servoX - change, 180 - servoRange), 180 + servoRange);
    }

    private void CalibrateJoystick(int x, int y, string direction)
    {
        switch (direction)
        {
            case "UP":
                joystickMaxY = y;
                break;
            case "DOWN":
                joystickMinY = y;
                break;
            case "LEFT":
                joystickMinX = x;
                break;
            case "RIGHT":
                joystickMaxX = x;
                break;
            case "CENTER":
                joystickCenterX = x;
                joystickCenterY = y;
                break;
            default:
                break;
        }
    }

    private void ResetToHome()
    {
        servoX = 180;
        servoY = 180;
    }

    private void OutputServoPositions()
    {
        Console.WriteLine($"SERVO POS: {servoX} {servoY}");
    }
}
