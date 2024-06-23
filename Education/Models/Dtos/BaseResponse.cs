using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocean_Home.Models.Enums;

namespace Ocean_Home.Models.Dtos
{
    public class BaseResponse
    {

        private Errors Error = Errors.Success;
        public Errors ErrorCode
        {
            get
            {
                return Error;
            }
            set
            {
                Error = value;
                ErrorMessage = value.ToString();
            }
        }
        public string ErrorMessage { get; set; } = Errors.Success.ToString();
        public object Data { get; set; }
    }
}
