using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {

        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        public DeliveryAddressController(IDeliveryAddressRepository deliveryAddressRepository)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
        }
        [HttpPost("AddressFK")]
        [Authorize]
        public async Task<IActionResult> AddDeliveryAddress([FromBody] DeliveryAddressFKResponse address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }

            var newAddress = new DeliveryAddressModel
            {
                UserId = userId,
                TypeOfAddress = address.Title,
                AreaDetails = address.Street,
                City = address.City,
                State = address.State,
                PinCode = address.Pincode,
                MobileNumber = address.Phone,
                AlternateMobileNumber = address.Phone,
                FullName = address.Title,
                HouseNo = "",
                Landmark = "",
                IsActive = true,
                createAt = DateTime.UtcNow,
                updateAt = DateTime.UtcNow
            };

            var addedAddress = await _deliveryAddressRepository.AddDeliveryAddress(newAddress);

            var response = new DeliveryAddressFKResponse
            {
                Title = addedAddress.FullName,
                Street = addedAddress.AreaDetails,
                City = addedAddress.City,
                Pincode = addedAddress.PinCode,
                Phone = addedAddress.MobileNumber
            };

            return CreatedAtAction(nameof(GetAddressById), new { id = addedAddress.AddId }, response);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] DeliveryDTO address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deliveryAddress = new DeliveryAddressModel
            {
                UserId = address.UserId,
                FullName = address.FullName,
                MobileNumber = address.MobileNumber,
                AlternateMobileNumber = address.AlternateMobileNumber,
                PinCode = address.PinCode,
                HouseNo = address.HouseNo,
                AreaDetails = address.AreaDetails,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                TypeOfAddress = address.TypeOfAddress,
                IsActive = true,
                createAt = DateTime.UtcNow,
                updateAt = DateTime.UtcNow
            };
            var addedProduct = await _deliveryAddressRepository.AddDeliveryAddress(deliveryAddress);

            var response = new DeliveryAddressModel
            {
                UserId = deliveryAddress.UserId,
                FullName = deliveryAddress.FullName,
                MobileNumber = deliveryAddress.MobileNumber,
                AlternateMobileNumber = deliveryAddress.AlternateMobileNumber,
                PinCode = deliveryAddress.PinCode,
                HouseNo = deliveryAddress.HouseNo,
                AreaDetails = deliveryAddress.AreaDetails,
                Landmark = deliveryAddress.Landmark,
                City = deliveryAddress.City,
                State = deliveryAddress.State,
                TypeOfAddress = deliveryAddress.TypeOfAddress,
                IsActive = deliveryAddress.IsActive,
                createAt = DateTime.UtcNow,
                updateAt = DateTime.UtcNow
            };

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Route("DeliveryaddressFKget")]

        public async Task<IActionResult> GetDeliveryAddressFKByUserId()
        {
            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            var DeliverAddress = await _deliveryAddressRepository.GetDeliveryAddressByUserId(userId);

            if (DeliverAddress == null)
            {
                return Ok("Still No Address Added");
            }

            var responses = DeliverAddress.Select(deliveryAddres => new DeliveryAddressFKResponseGet
            {

                user_id = deliveryAddres.UserId,
                id = deliveryAddres.AddId,
                Title = deliveryAddres.FullName,
                Phone = deliveryAddres.MobileNumber,
                Pincode = deliveryAddres.PinCode,
                Street = deliveryAddres.AreaDetails,
                State = deliveryAddres.State,
                City = deliveryAddres.City,

            }).ToList();

            return Ok(responses);
        }


        [HttpGet]
        [Authorize]
        [Route("Deliveryaddress/{userId:int}")]

        public async Task<IActionResult> GetDeliveryAddressByUserId([FromRoute] int userId)
        {
            var DeliverAddress = await _deliveryAddressRepository.GetDeliveryAddressByUserId(userId);

            if(DeliverAddress == null)
            {
                return Ok("Still No Address Added");
            }

            var responses = DeliverAddress.Select(deliveryAddres => new DeliveryAddressModel
            {
                AddId = deliveryAddres.AddId,
                UserId = deliveryAddres.UserId,
                FullName = deliveryAddres.FullName,
                MobileNumber = deliveryAddres.MobileNumber,
                AlternateMobileNumber = deliveryAddres.AlternateMobileNumber,
                PinCode = deliveryAddres.PinCode,
                HouseNo = deliveryAddres.HouseNo,
                AreaDetails = deliveryAddres.AreaDetails,
                Landmark = deliveryAddres.Landmark,
                City = deliveryAddres.City,
                State = deliveryAddres.State,
                TypeOfAddress = deliveryAddres.TypeOfAddress,
                IsActive = deliveryAddres.IsActive,

            }).ToList();

            return Ok(responses);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            var address = await _deliveryAddressRepository.GetDeliveryAddressById(id);
            if (address == null)
            {
                return NotFound();
            }

            var response = new DeliveryAddressModel
            {
                AddId = address.AddId,
                UserId = address.UserId,
                FullName = address.FullName,
                MobileNumber = address.MobileNumber,
                AlternateMobileNumber = address.AlternateMobileNumber,
                PinCode = address.PinCode,
                HouseNo = address.HouseNo,
                AreaDetails = address.AreaDetails,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                TypeOfAddress = address.TypeOfAddress,
                IsActive = address.IsActive,
                updateAt = address.updateAt,
            };

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [Route("DeliveryaddressFKput/{id:int}")]

        public async Task<IActionResult> UpdateDeliveryAddressFK([FromRoute] int id, [FromBody] DeliveryAddressFKResponse deliveryAddress)
        {
            var existingdeliveryAddress = await _deliveryAddressRepository.GetDeliveryAddressById(id);
            if (existingdeliveryAddress == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst("userId");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }

            existingdeliveryAddress.UserId = userId;
            existingdeliveryAddress.FullName = deliveryAddress.Title;
            existingdeliveryAddress.MobileNumber = deliveryAddress.Phone;
            existingdeliveryAddress.AlternateMobileNumber = deliveryAddress.Phone;
            existingdeliveryAddress.PinCode = deliveryAddress.Pincode;
            
            existingdeliveryAddress.AreaDetails = deliveryAddress.Street;
          
            existingdeliveryAddress.City = deliveryAddress.City;
            existingdeliveryAddress.State = deliveryAddress.State;
            existingdeliveryAddress.TypeOfAddress = deliveryAddress.Title;
            existingdeliveryAddress.IsActive = true;
            existingdeliveryAddress.updateAt = DateTime.UtcNow;

            var updatedAddress = await _deliveryAddressRepository.UpdateDeliveryAddress(existingdeliveryAddress);

            var response = new DeliveryAddressModel
            {
                AddId = existingdeliveryAddress.AddId,
                UserId = existingdeliveryAddress.UserId,
                FullName = existingdeliveryAddress.FullName,
                MobileNumber = existingdeliveryAddress.MobileNumber,
                AlternateMobileNumber = existingdeliveryAddress.AlternateMobileNumber,
                PinCode = existingdeliveryAddress.PinCode,
                HouseNo = existingdeliveryAddress.HouseNo,
                AreaDetails = existingdeliveryAddress.AreaDetails,
                Landmark = existingdeliveryAddress.Landmark,
                City = existingdeliveryAddress.City,
                State = existingdeliveryAddress.State,
                TypeOfAddress = existingdeliveryAddress.TypeOfAddress,
                IsActive = existingdeliveryAddress.IsActive,
                updateAt = existingdeliveryAddress.updateAt,
            };

            return Ok(response);
        }

        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateDeliveryAddress([FromRoute] int id, [FromBody] DeliveryAddressModel deliveryAddress)
        {
            var existingdeliveryAddress = await _deliveryAddressRepository.GetDeliveryAddressById(id);
            if (existingdeliveryAddress == null)
            {
                return NotFound();
            }

            existingdeliveryAddress.UserId = deliveryAddress.UserId;
            existingdeliveryAddress.FullName = deliveryAddress.FullName;
            existingdeliveryAddress.MobileNumber = deliveryAddress.MobileNumber;
            existingdeliveryAddress.AlternateMobileNumber = deliveryAddress.AlternateMobileNumber;
            existingdeliveryAddress.PinCode = deliveryAddress.PinCode;
            existingdeliveryAddress.HouseNo = deliveryAddress.HouseNo;
            existingdeliveryAddress.AreaDetails = deliveryAddress.AreaDetails;
            existingdeliveryAddress.Landmark = deliveryAddress.Landmark;
            existingdeliveryAddress.City = deliveryAddress.City;
            existingdeliveryAddress.State = deliveryAddress.State;
            existingdeliveryAddress.TypeOfAddress = deliveryAddress.TypeOfAddress;
            existingdeliveryAddress.IsActive = true;
            existingdeliveryAddress.updateAt = DateTime.UtcNow;

            var updatedAddress = await _deliveryAddressRepository.UpdateDeliveryAddress(existingdeliveryAddress);

            var response = new DeliveryAddressModel
            {
            AddId = existingdeliveryAddress.AddId,
            UserId = existingdeliveryAddress.UserId,
            FullName = existingdeliveryAddress.FullName,
            MobileNumber = existingdeliveryAddress.MobileNumber,
            AlternateMobileNumber = existingdeliveryAddress.AlternateMobileNumber,
            PinCode = existingdeliveryAddress.PinCode,
            HouseNo = existingdeliveryAddress.HouseNo,
            AreaDetails = existingdeliveryAddress.AreaDetails,
            Landmark = existingdeliveryAddress.Landmark,
            City = existingdeliveryAddress.City,
            State = existingdeliveryAddress.State,
            TypeOfAddress = existingdeliveryAddress.TypeOfAddress,
            IsActive = existingdeliveryAddress.IsActive,
            updateAt =existingdeliveryAddress.updateAt,
            };

            return Ok(response);
        }



        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteDeliveryAddress([FromRoute] int id)
        {
            var isDeleted = await _deliveryAddressRepository.DeleteDeliveryAddress(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
