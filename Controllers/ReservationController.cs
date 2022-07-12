using BookingHotel.DTO;
using BookingHotel.Models;
using BookingHotel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepositoryReservation repositoryReservation;
        private readonly IRepositoryReservationRoom  repositoryReservationRoom;
        private readonly IRepositoryTempGuestRoom repositoryTempGuestRoom;
        private readonly IRepositoryRoom repositoryRoom;

        public ReservationController(IRepositoryReservation _repositoryReservation,
                                     IRepositoryRoom _repositoryRoom,
                                     IRepositoryReservationRoom _repositoryReservationRoom,
                                     IRepositoryTempGuestRoom _repositoryTempGuestRoom )
        {
            this.repositoryReservation = _repositoryReservation;
            this.repositoryRoom = _repositoryRoom;
            this.repositoryReservationRoom = _repositoryReservationRoom;
            repositoryTempGuestRoom = _repositoryTempGuestRoom;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = repositoryReservation.GetAll();
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
        [HttpGet("{id:int}", Name = "getOneRouteReservation")]
        public IActionResult GetOne(int id)
        {
            try
            {
                var reservation = repositoryReservation.GetOne(id);
                if (reservation != null)
                {
                    return Ok(reservation);
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
                int rowEffect = repositoryReservation.Delete(id);
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
        public IActionResult Insert(ReservationDto model)
        {
            Room room;
            double sumTotalPrice = 0;
            Reservation newreservation = new Reservation();
            try
            {
                if (ModelState.IsValid)
                {
                    newreservation.Guest_Id = model.Guest_Id;
                    newreservation.Date = DateTime.Now;
                    newreservation.TotalPrice = 0;
                    repositoryReservation.Add(newreservation);
                    foreach (var item in model.ReservationRoomInfo)
                    {
                        room = repositoryRoom.GetOne(item.RoomId);
                        repositoryReservationRoom.Add(new ReservationRoom
                        {
                            Reservation_Id = newreservation.Id,
                            Room_Id = item.RoomId,
                            DateIn = item.DateIn,
                            TotalPriceForOneRoom = item.NumberOfDays * room.Price,
                            NumberOfDays = item.NumberOfDays,
                            DateOut = item.DateOut,
                        });

                        sumTotalPrice += item.NumberOfDays * room.Price;
                    }
                    if (CheckGeustIfBookingBefore(model.Guest_Id) > 1)
                    {
                        newreservation.TotalPrice = (95.0 / 100.0) * sumTotalPrice;
                    }
                    else
                    {
                        newreservation.TotalPrice = sumTotalPrice;
                    }
                    repositoryReservation.Edit(newreservation.Id, newreservation);
                    repositoryTempGuestRoom.DeleteByGuestID(model.Guest_Id);
                    return Ok(newreservation);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        private int CheckGeustIfBookingBefore(string guestId)
        {
            try
            {
                List<Reservation> reservation = repositoryReservation.GetReservationsForGuest(guestId);
                if (reservation != null)
                {
                    return reservation.Count;

                }
                return 0;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        [HttpPatch]
        public IActionResult Edit(int id, Reservation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int rowEffect = repositoryReservation.Edit(id, model);
                    if (rowEffect > 0)
                    {
                        string url = Url.Link("getOneRouteReservation", new { id = id });
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

        
        [HttpPost("ConfirmReservation")]
        public IActionResult  ConfirmReservation(int reservationId)
        {
            try
            {
                var result = repositoryReservation.ConfirmReservation(reservationId);
                if(result.Status == true)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CancleReservationForOneRoom")]
        public IActionResult CancleReservationForOneRoom(int reservationId , int roomId )
        {
            try
            {
                var result = repositoryReservation.CancleReservation(reservationId,roomId);
                if (result)
                {
                    return Ok(new StatusResponse { Message ="Cancel success",Status=true});
                }
                return BadRequest(new StatusResponse { Message="Cancel faild",Status=false});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CancleReservationGuestForAllRooms")]
        public IActionResult CancleReservationGuestForAllRooms(int reservationId)
        {
            try
            {
                var result = repositoryReservation.CancleReservationForAllRooms(reservationId);
                if (result)
                {
                    return Ok(new StatusResponse { Message = "Reservation deleted", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Deleted faild", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ReservationDetailsForGuest/{guestId}")]
        public IActionResult ReservationDetailsForGuest(string guestId)
        {
            try
            {
                var data = repositoryReservation.GetReservationsForGuest(guestId);
                if (data != null)
                {
                    return Ok(data);
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
