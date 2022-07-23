using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [HttpPost("Create")]
        public IActionResult Create(CollabModel collabModel, long noteid)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = collabBL.CreateCollab(collabModel, UserId,noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Collab is Created Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Collab is Not Created" });
            }
        }
        [HttpGet("Retrieve")]
        public IActionResult GetCollab()
        {
            var result = collabBL.GetCollabDetails();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Collab is not Retrieved" });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long CollabID)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = collabBL.DeleteCollab(CollabID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Delete is Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Delete is Unsuccessful" });
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedcollabList;
            var collabList = new List<CollabEntity>();
            var rediscollabList = await distributedCache.GetAsync(cacheKey);
            if (rediscollabList != null)
            {
                serializedcollabList = Encoding.UTF8.GetString(rediscollabList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedcollabList);
            }
            else
            {
                collabList = (List<CollabEntity>)collabBL.GetCollabDetails();
                serializedcollabList = JsonConvert.SerializeObject(collabList);
                rediscollabList = Encoding.UTF8.GetBytes(serializedcollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, rediscollabList, options);
            }
            return Ok(collabList);
        }
    }
}
