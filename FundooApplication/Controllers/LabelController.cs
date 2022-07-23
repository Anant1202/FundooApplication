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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [HttpPost("Create")]
        public IActionResult Create(LabelModel labelModel, long noteid)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = labelBL.CreateLabel(labelModel, UserId, noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Label is Created Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Label is Not Created" });
            }
        }
        [HttpGet("Retrieve")]
        public IActionResult GetLabel()
        {
            var result = labelBL.GetAllLabel();
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Retrieve Successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Label is not Retrieved" });
            }
        }
        [HttpPut("Update")]
        public IActionResult Update(LabelModel labelModel, long LabelID)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = labelBL.UpdateLabel(labelModel,LabelID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Update is Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Update is Unsuccessful" });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long LabelID)
        {
            long UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
            var result = labelBL.DeleteLabel(LabelID);
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
            var cacheKey = "labelList";
            string serializedlabelList;
            var labelList = new List<LabelEntity>();
            var redislabelList = await distributedCache.GetAsync(cacheKey);
            if (redislabelList != null)
            {
                serializedlabelList = Encoding.UTF8.GetString(redislabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedlabelList);
            }
            else
            {
                labelList = (List<LabelEntity>)labelBL.GetAllLabel();
                serializedlabelList = JsonConvert.SerializeObject(labelList);
                redislabelList = Encoding.UTF8.GetBytes(serializedlabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redislabelList, options);
            }
            return Ok(labelList);
        }
    }
}
