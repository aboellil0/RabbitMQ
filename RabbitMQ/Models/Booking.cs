using System;
using System.Collections.Generic;

namespace RabbitMQ.Models;

public partial class Booking
{
    public int id { get; set; }

    public string PassengerName { get; set; }

    public string PassportNb { get; set; }

    public string RFrom { get; set; }

    public string RTo { get; set; }

    public string Status { get; set; }
}
