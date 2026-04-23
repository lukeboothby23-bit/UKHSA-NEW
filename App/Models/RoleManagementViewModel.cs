namespace UKHSA.Models
{
    public class RoleManagementViewModel
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = ""; 
        public string Email { get; set; } = ""; 
        public bool IsUser { get; set; } 
        public bool IsApprover { get; set; } 
        public bool IsAdmin { get; set; } 
    }
}