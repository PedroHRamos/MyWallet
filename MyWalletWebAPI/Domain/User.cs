using System;
using Microsoft.EntityFrameworkCore;

namespace MyWalletWebAPI.Domain;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string InvestorProfile { get; set; }
    public DateTime BirthDate { get; set; }
    public int Plan { get; set; }
}
