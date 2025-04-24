// Apply SOLID design principles on the following code samples for better design
//1.  
using System.Net.Mail;

public class UserService  
{  
   public void Register(string email, string password)  
   {  
      if (!ValidateEmail(email))  
         throw new ValidationException("Email is not an email");  
      
      var user = new User(email, password);  
  
      SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject="HEllo foo" });  
   }

   public virtual bool ValidateEmail(string email)  
   {  
     return email.Contains("@");  
   }  
   public bool SendEmail(MailMessage message)  
   {  
     _smtpClient.Send(message);  
   }  
}

//-----------------------------------------------  SRP & OCP

public interface IEmailValidator
{
    public bool ValidateEmail(string email);
}
public class EmailValidator : IEmailValidator
{
    public bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
}


public interface IEmailSender
{
    public void Send(MailMessage message);
}
public class EmailSender : IEmailSender
{
    private SmtpClient _smtpClient = new SmtpClient();

    public void Send(MailMessage message)
    {
        _smtpClient.Send(message);
    }
}
public class User
{
    public string Email { get; }
    public string Password { get; }
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class UserService : IEmailValidator
{
    private IEmailValidator _emailValidator;
    private IEmailSender _emailSender;  

    public UserService(IEmailValidator emailValidator, IEmailSender emailSender)
    {
        _emailValidator = emailValidator;
        _emailSender = emailSender;
    }
    

    public void Register(string email, string password)
    {
        if (!_emailValidator.ValidateEmail(email))
            throw new ValidationException("Email is not an email");
        var user = new User(email, password);

        var message = new MailMessage("mysite@nowhere.com", email)
        {
            Subject = "HEllo foo"
        }
         _emailSender.Send(message);
    }
}
// 2.
//a. Add Square & Triangle & Cube
//b. Add function to get volume for the supported shapes
//c. noting that cube shape only support volume calculation

public abstract class Shape
{
    public abstract double calculateArea();
}
public class Rectangle: Shape{  

    public double Height {get;set;}  
    public double Wight {get;set; }
    public override double calculateArea()
    {
        return Height * Width;
    }

}  
public class Circle: Shape{  
  public double Radius {get;set;}
    public override double calculateArea()
    {
        return Radius * Radius * Math.PI;
    }
}
public class Square : Shape
{
    public double Side { get; set; }
    public override double calculateArea()
    {
        return Side * Side;
    }
}
public class Triangle: Shape
{
    public double Side1 { get; set; }
    public double Side2 { get; set; }
    public double Side3 { get; set; }

    public override double calculateArea()
    {
        double s = (Side1 + Side2 + Side3) / 2;
        return Math.Sqrt(s * (s - Side1) * (s - Side2) * (s - Side3));
    }
}
public abstract class ThreeDShape : Shape
{
    public abstract double calculateVolume();
}

public class Cube: ThreeDShape
{
    public double Side { get; set; }
    public override double calculateArea()
    {
        return 6 * Side * Side;
    }
    public override double calculateVolume()
    {
        return Side * Side * Side;
    }

}
public class AreaAndVolumeCalculator  
{  
    public double TotalArea(Shape[] arrObjects)  
    {  
        double area = 0;
        foreach(var obj in arrObjects)  
        {  
        area += obj.calculateArea();
        }  
        return area;  
    }  
    public double TotalVolume(ThreeDShape[] arrObjects)
    {
        double volume = 0;
        foreach (var obj in arrObjects)
        {
            volume += obj.calculateVolume();
        }
        return volume;
    }
}


// 3.
public abstract class Shape
{
    public double Length { get; set; }
}
class Rectangle:Shape
{
    public double Width { get; set; }
}
class Square:Shape
{
}

class Rectangle
 def initialize(width, height)
 @width, @height = width, height
 end
 def set_width(width)
 @width = width
 end
 def set_height(height)
 @height = height
 end
end
class Square<Rectangle "inherits"
 def set_width(width)
 super(width)
 @height = height
 end
 def set_height(height)
 super(height)
 @width = width
 end
end