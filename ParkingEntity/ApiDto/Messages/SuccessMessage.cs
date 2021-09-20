using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingEntity.ApiDto.Messages
{
    public static class SuccessMessage
    {
        public const string VALID_ENTRY = "Date is valid";

        public const string NO_RATE_FOUND = "Unable to find any matching rate.";
        public const string MATCHING_RATE_FOUND = "Matching Rate found.";
    }
}
