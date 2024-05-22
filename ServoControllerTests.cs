using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

//namespace Steer_Stay.Tests;
namespace SteerStayBorescope;

[TestFixture]
public class ServoControllerTests
{
    private ServoController _servoController { get; set; } = null!;
    [SetUp]
    public void SetUp()
    {
        _servoController= new ServoController();
    }
    [Test]
    public void HandleJoystick_NoMovementWithinDeadband_ServoPositionsUnchanged()
    {
        // Arrange
        ServoController servoController = new ServoController();
        int initialX = 500;
        int initialY = 500;

        // Act
        servoController.ParseCommand("joystick 500 500"); // Joystick position within deadband

        // Assert
        Assert.That(initialX, Is.EqualTo(servoController.ServoX));
        Assert.That(initialY, Is.EqualTo(servoController.ServoY));
    }

    [Test]
    public void HandleJoystick_MovementWithinDeadband_ServoPositionsAdjusted()
    {
        // Arrange
        ServoController servoController = new ServoController();
        int initialX =500;
        int initialY =500;

        // Act
        servoController.ParseCommand("joystick 550 550"); // Joystick position outside deadband

        // Assert
        Assert.That(initialX, Is.Not.EqualTo(servoController.ServoX));
        Assert.That(initialY, Is.Not.EqualTo(servoController.ServoY));
    }

    // Add more tests for other scenarios...

    [Test]
    public void CalibrateJoystick_CenterDirection_CenterCalibrationUpdated()
    {
        // Arrange
        ServoController servoController = new ServoController();
        int initialCenterX = servoController.JoystickCenterX;
        int initialCenterY = servoController.JoystickCenterY;

        // Act
        servoController.ParseCommand("calibrate 600 600 CENTER");

        // Assert
        Assert.That(600, Is.EqualTo(servoController.JoystickCenterX));
        Assert.That(600, Is.EqualTo(servoController.JoystickCenterY));
    }
}