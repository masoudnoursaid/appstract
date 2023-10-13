namespace Appstract.Front.Domain.Models;

public class ProfileInfo
{
    public Guid Guid { get; set; }
    public bool IsAuthenticated { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool EmailVerified { get; set; } = false;
}