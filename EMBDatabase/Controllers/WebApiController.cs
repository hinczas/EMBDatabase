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


        [Route("api/WebApi/GetAllPackages")]
        [HttpGet]
        public List<ExportPackage> GetAllPackages()
        {
            return _ds.GetPackages();
        }
        [Route("api/WebApi/GetPackage")]
        [HttpGet]
        public ExportPackage GetPackage(int id)
        {
            return _ds.GetPackage(id);
        }

        [Route("api/WebApi/GetAllTypes")]
        [HttpGet]
        public List<ExportType> GetAllTypes()
        {
            return _ds.GetTypes();
        }
        [Route("api/WebApi/GetPackage")]
        [HttpGet]
        public ExportType GetType(int id)
        {
            return _ds.GetType(id);
        }

        [Route("api/WebApi/AddPart")]
        [HttpPost]
        public GenericResponse AddPart([FromBody] ApiPart apiPart)
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
                long returnedId = _ds.AddPart(apiPart);

                if (returnedId > 0)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = "Part saved.",
                        ItemId = returnedId
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

        [Route("api/WebApi/AddManufacturer")]
        [HttpPost]
        public GenericResponse AddManufacturer([FromBody] Manufacturer manuf)
        {
            if (manuf.Name == null || manuf.Full_Name == null)
            {
                return new GenericResponse()
                {
                    Type = "Warning",
                    Code = 2,
                    Message = "Cannot create manufacturer. 'Name' and 'Full_Name' are required."
                };
            }
            else
            {

                long returnedId = _ds.AddItem<Manufacturer>(manuf);

                if (returnedId > 0)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = "Item saved.",
                        ItemId = returnedId
                    };
                }
                else
                {
                    return new GenericResponse()
                    {
                        Type = "Error",
                        Code = 1,
                        Message = "Exception was thrown when trying to save an item."
                    };
                }
            }
        }

        [Route("api/WebApi/AddType")]
        [HttpPost]
        public GenericResponse AddType([FromBody] Models.Type type)
        {
            if (type.Name == null)
            {
                return new GenericResponse()
                {
                    Type = "Warning",
                    Code = 2,
                    Message = "Cannot create type. 'Name' is required."
                };
            }
            else
            {

                long returnedId = _ds.AddItem<Models.Type>(type);

                if (returnedId > 0)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = "Item saved.",
                        ItemId = returnedId
                    };
                }
                else
                {
                    return new GenericResponse()
                    {
                        Type = "Error",
                        Code = 1,
                        Message = "Exception was thrown when trying to save an item."
                    };
                }
            }
        }

        [Route("api/WebApi/AddLocation")]
        [HttpPost]
        public GenericResponse AddLocation([FromBody] Location loc)
        {
            if (loc.Name == null)
            {
                return new GenericResponse()
                {
                    Type = "Warning",
                    Code = 2,
                    Message = "Cannot create location. 'Name' is required."
                };
            }
            else
            {
                long returnedId = _ds.AddItem<Location>(loc);

                if (returnedId > 0)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = "Item saved.",
                        ItemId = returnedId
                    };
                }
                else
                {
                    return new GenericResponse()
                    {
                        Type = "Error",
                        Code = 1,
                        Message = "Exception was thrown when trying to save an item."
                    };
                }
            }
        }

        [Route("api/WebApi/AddPackage")]
        [HttpPost]
        public GenericResponse AddPackage([FromBody] Package pckg)
        {
            if (pckg.Name == null || pckg.Number == null)
            {
                return new GenericResponse()
                {
                    Type = "Warning",
                    Code = 2,
                    Message = "Cannot create package. 'Name' is required."
                };
            }
            else
            {
                long returnedId = _ds.AddItem<Package>(pckg);

                if (returnedId > 0)
                {
                    return new GenericResponse()
                    {
                        Type = "Success",
                        Code = 0,
                        Message = "Item saved.",
                        ItemId = returnedId
                    };
                }
                else
                {
                    return new GenericResponse()
                    {
                        Type = "Error",
                        Code = 1,
                        Message = "Exception was thrown when trying to save an item."
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
