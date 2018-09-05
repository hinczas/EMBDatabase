using EMBDatabase.Classes;
using EMBDatabase.Context;
using EMBDatabase.Models;
using EMBDatabase.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMBDatabase.Controllers
{
    public class WebApiController : ApiController
    {
        private readonly DataService ds;

        public WebApiController()
        {
            ds = new DataService();
        }


        [Route("api/WebApi/AllParts")]
        [HttpGet]
        public List<ApiPart> AllParts()
        {
            return ds.GetParts();
        }

        [Route("api/WebApi/OnePart")]
        [HttpGet]
        public ApiPart OnePart(int id)
        {
            return ds.GetPart(id);
        }

        [Route("api/WebApi/AddPart")]
        [HttpPost]
        public GenericResponse AddPart([FromUri] ApiPart apiPart)
        {
            if(apiPart.Name==null || apiPart.Number==null)
            {
                return new GenericResponse()
                {
                    Type = "Warning",
                    Code = 2,
                    Message = "Cannot create part. Part 'Name' and 'Number' are required."
                };
            } else
            {
                bool saved = ds.AddPart(apiPart);

                if (saved)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = "Part saved."
                    };
                } else
                {
                    return new GenericResponse()
                    {
                        Type = "Error",
                        Code = 1,
                        Message = "Exception was thrown when trying to save part."
                    };
                }
            }
        }

        [Route("api/WebApi/UpPartValue")]
        [HttpGet]
        public GenericResponse UpPartValue(int id, string type, string value)
        {
            return new GenericResponse()
            {
                Type = "UpPart type value",
                Code = 2,
                Message = "UpPart type value"
            };
        }

        [Route("api/WebApi/UpPart")]
        [HttpGet]
        public GenericResponse UpPart([FromUri] ApiPart apiPart)
        {
            return new GenericResponse()
            {
                Type = "upPart ApiPart FromURI",
                Code = 2,
                Message = "upPart ApiPart FromURI"
            };
        }
    }
}
