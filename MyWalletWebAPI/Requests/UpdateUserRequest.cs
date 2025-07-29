namespace MyWalletWebAPI.Requests;

public class UpdateUserRequest
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string InvestorProfile { get; set; }
    public DateTime BirthDate { get; set; }
    public int Plan { get; set; }
}
