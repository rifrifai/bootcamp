using SOLID_and_KISS_Principles.Models;


namespace SOLID_and_KISS_Principles.BadExamples;
// <summary>
// BAD EXAMPLE - Violates Single Responsibility Principle (SRP)
// This class does WAY too many things!
// 1. User Registration
// 2. Email Validation
// 3. Sending emails
// 4. Database operations
// 
// Problem: If email logic changes, we have to modify this class
// Problem: If validation rules changes, we have to modify this class
// Problem: Hard to test individual pieces
// Problem: Hard to reuse email functionality elsewhere
// </summary>

public class BadUserService
{
  // look at all these different responsibilities in one class!

  public void RegisterUser(string email, string password)
  {
    // validation logic - should be seperate
    if (!ValidateEmail(email))
    {
      throw new ArgumentException("Invalid email format!");
    }

    if (string.IsNullOrEmpty(password) || password.Length < 6)
    {
      throw new ArgumentException("Password must be at least 6 characters long!");
    }

    // user creation logic
    var user = new User(email, password);

    // database logic, should be seperate
    SaveUserToDatabase(user);

    // email logic, should be seperate
    SendWelcomeEmail(email);

    // logging logic, should be seperate
    LogUserRegistration(email);
  }

  // email validation mixed with user logic
  private bool ValidateEmail(string email)
  {
    return email.Contains('@') && email.Contains('.');
  }

  // database operations mixed in
  private void SaveUserToDatabase(User user)
  {
    // simulated database save
    Console.WriteLine($"Saving user: {user.Email} to database...");
    // in real life, this would be database code
  }

  // email sending mixed in
  private void SendWelcomeEmail(string email)
  {
    // email logic - should be in email service
    Console.WriteLine($"Sending welcome email to {email}");
    // in real life, this would be email SMTP code
  }

  // logging mixed in
  private void LogUserRegistration(string email)
  {
    Console.WriteLine($"User registered: {email} at {DateTime.Now}");
  }
}


/// <summary>
/// BAD EXAMPLE - Violates Open/Closed Principle
/// Every time we add a new shape, we have to modify this class
/// This makes the code fragile and harder to maintain
/// </summary>