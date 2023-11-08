namespace Api.Models.ModelsForExternalApis
{
    public class Company
    {
        public class EmpresaInfo
        {
            internal object alias;

            public CompanyInfo company { get; set; }
            public AddressInfo address { get; set; }
            public List<PhoneInfo> phones { get; set; }
            public List<EmailInfo> emails { get; set; }
        }

        public class CompanyInfo
        {
            public string name { get; set; }
        }

        public class AddressInfo
        {
            public string municipality;
            public string details;

            public string street { get; set; }
            public string number { get; set; }
            public string district { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zip { get; set; }
        }

        public class PhoneInfo
        {
            public string area { get; set; }
            public string number { get; set; }
        }

        public class EmailInfo
        {
            public string address { get; set; }
            public string domain { get; set; }
        }

    }
}
