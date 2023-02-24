using Loto3000.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Ticket : IEntity
    {
        public Ticket()
        {

        }

        public Ticket(User user, Draw draw, string combinationNumbers)
        {
            Draw = draw;
            User = user;
            DateOfCreateTicket = DateTime.Now;
            CombinationNumbers = combinationNumbers;
            IsActive = true;
            HasPrice = false;
            TicketOwner = user.LastName;
            Prize = TicketPrize.Nothing;
        }
       

        public int Id { get; set; }
        public User? User { get; set; }
        public Draw? Draw { get; set; }
        public DateTime DateOfCreateTicket = DateTime.Now;
        public string CombinationNumbers { get; set; } = string.Empty; 
        public bool IsActive { get; set; } 
        public bool HasPrice { get; set; }
        public TicketPrize Prize { get; set; } = TicketPrize.Nothing; 

        public string TicketOwner = string.Empty;


  
    }
}