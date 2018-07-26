using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueYonder.Hotels.DAL.Models;
using BlueYonder.Hotels.DAL.Repository;
using BlueYonderHotels.Service.Attirbutes;
using Microsoft.AspNetCore.Mvc;

namespace BlueYonderHotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly HotelBookingRepository _hotelBookingRepository;

        public HotelBookingController()
        {
            _hotelBookingRepository = new HotelBookingRepository();
        }


        [HttpGet]
        [Cache("X-No-Cache")]
        public IEnumerable<Room> GetAvailability(DateTime dateTime)
        {
            return _hotelBookingRepository.GetAvaliabileByDate(dateTime);
        }
        
        [HttpGet]
        public IEnumerable<Reservation> GetReservation()
        {
            return _hotelBookingRepository.GetAllReservation();
        }

        
        [HttpDelete]
        public async Task DeleteReservation(int reservationId)
        {
            await _hotelBookingRepository.DeleteReservation(reservationId);
        }
       
    }
}
