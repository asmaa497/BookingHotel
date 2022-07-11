using BookingHotel.DTO;
using BookingHotel.Models;
using BookingHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRepositoryRoom repositoryRoom;

        public RoomController(IRepositoryRoom _repositoryRoom)
        {
            this.repositoryRoom = _repositoryRoom;
        }
        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = repositoryRoom.GetAll();
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound(new StatusResponse { Message = "No data found", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllAvialable")]
        public IActionResult GetAllAvialable()
        {
            try
            {
                var data = repositoryRoom.GetAvialable();
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound(new StatusResponse { Message = "No data found", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "getOneRouteRoom")]
        public IActionResult GetOne(int id)
        {
            try
            {
                var branch = repositoryRoom.GetOne(id);
                if (branch != null)
                {
                    return Ok(branch);
                }
                return NotFound(new StatusResponse { Message = "faild no found this branch", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int rowEffect = repositoryRoom.Delete(id);
                if (rowEffect > 0)
                {
                    return Ok(new StatusResponse { Message = $"Branch deleted", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Not found any branch", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(Room model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(repositoryRoom.Add(model));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        public IActionResult Edit(int id, Room model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int rowEffect = repositoryRoom.Edit(id, model);
                    if (rowEffect > 0)
                    {
                        string url = Url.Link("getOneRouteRoom", new { id = id });
                        return Created(url, model);
                    }
                }
                return BadRequest(new StatusResponse { Message = "Edit faild", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRoomsByBranchId/{branchId:int}")]
        public IActionResult GetRoomsByBranchId(int branchId)
        {
            try
            {
                var rooms = repositoryRoom.GetRoomsByBranchId(branchId);
                if(rooms != null)
                {
                    return Ok(rooms);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
