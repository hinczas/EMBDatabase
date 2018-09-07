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
        private readonly DataService _ds;

        public WebApiController()
        {
            _ds = new DataService();
        }


        [Route("api/WebApi/GetAllParts")]
        [HttpGet]
        public List<ApiPart> GetAllParts()
        {
            return _ds.GetParts();
        }
        [Route("api/WebApi/GetPart")]
        [HttpGet]
        public ApiPart GetPart(int id)
        {
            return _ds.GetPart(id);
        }


        [Route("api/WebApi/GetAllManufacturers")]
        [HttpGet]
        public List<ExportManufacturer> GetAllManufacturers()
        {
            return _ds.GetManufacturers();
        }
        [Route("api/WebApi/GetManufacturer")]
        [HttpGet]
        public ExportManufacturer GetManufacturer(int id)
        {
            return _ds.GetManufacturer(id);
        }


        [Route("api/WebApi/GetAllLocations")]
        [HttpGet]
        public List<ExportLocation> GetAllLocations()
        {
            return _ds.GetLocations();
        }
        [Route("api/WebApi/GetLocation")]
        [HttpGet]
        public ExportLocation GetLocation(int id)
        {
            return _ds.GetLocation(id);
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
                bool saved = _ds.AddPart(apiPart);

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

        /// <summary>
        /// Generic API to update given param for specified Model Type
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <param name="name">Field name to update</param>
        /// <param name="value">New Field Value</param>
        /// <param name="type">Model Type [Part|Manufacturer|etc]</param>
        /// <returns></returns>
        [Route("api/WebApi/UpdateItemValue")]
        [HttpPut]
        public GenericResponse UpdateItemValue(int id, string name, string value, string type)
        {
            type = type.ToLower();
            bool updated = false;
            
            // Check if required params were provided
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                return new GenericResponse()
                {
                    Type = "Warning",
                    Code = 2,
                    Message = "Name and Type cannot be empty."
                };
            } else
            {
                switch (type)
                {
                    case "part":
                        updated = _ds.UpdateItem<Part> (id, name, value);
                        break;
                    case "manufacturer":
                        updated = _ds.UpdateItem<Manufacturer>(id, name, value);
                        break;
                    case "location":
                        updated = _ds.UpdateItem<Location>(id, name, value);
                        break;
                    case "package":
                        updated = _ds.UpdateItem<Package>(id, name, value);
                        break;
                    case "type":
                        updated = _ds.UpdateItem<Models.Type>(id, name, value);
                        break;
                }

                if (updated)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = string.Format("Updated {0} from Id", type.ToTitleCase())
                    };
                } else
                {
                    return new GenericResponse()
                    {
                        Type = "Error",
                        Code = 1,
                        Message = string.Format("Something went wrong. Failed to update {0}", type.ToTitleCase())
                    };
                }

            }

        }

    }
}
