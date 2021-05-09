using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HumberAutoCarRental.Models
{
    public class IndividualButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }
        public string labelText { get; set; }

        public int? CartypeId { get; set; }
        public int? CarId { get; set; }
        public int? CustomerId { get; set; }
        public int? MembershipTypeId { get; set; }
        public string UserId { get; set; }

        public int? CarRentalId { get; set; }

        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");

                if (CarId != null && CarId > 0)
                {
                    param.Append(String.Format("{0}", CarId));
                }
                if (CartypeId != null && CartypeId > 0)
                {
                    param.Append(String.Format("{0}", CartypeId));
                }
                if (MembershipTypeId != null && MembershipTypeId > 0)
                {
                    param.Append(String.Format("{0}", MembershipTypeId));
                }
                if (CustomerId != null && CustomerId > 0)
                {
                    param.Append(String.Format("{0}", CustomerId));
                }
                if (UserId != null && UserId.Trim().Length > 0)
                {
                    param.Append(String.Format("{0}", UserId));
                }
                if (CarRentalId != null && CarRentalId > 0)
                {
                    param.Append(String.Format("{0}", CarRentalId));
                }

                return param.ToString();
            }
        }
    }
}