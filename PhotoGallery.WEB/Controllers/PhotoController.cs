﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.BLL.DTO;
using PhotoGallery.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoGallery.WEB.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        [Route("api/albums/{id}/photos")]
        public async Task<ActionResult<IEnumerable<PhotoDTO>>> GetPhotos(int id)
        {
            IEnumerable<PhotoDTO> photoDTOs;           

            try
            {
                photoDTOs = await _photoService.GetPhotosAsync(id, 1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(photoDTOs);
        }

        [HttpGet]
        [Route("api/photos/{id}")]
        public async Task<ActionResult<PhotoDTO>> GetPhoto(int id)
        {
            PhotoDTO photoDTO;

            try
            {
                photoDTO = await _photoService.GetPhotoAsync(id, 1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(photoDTO);
        }

        [HttpPost]
        [Route("api/albums/{id}/photos")]
        public async Task<ActionResult> PostPhoto(int id, [FromBody] PhotoAddDTO photoAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photoAddDTO.AlbumId)
            {
                return BadRequest();
            }

            PhotoDTO photoDTO;

            try
            {
                photoDTO = await _photoService.AddPhotoAsync(photoAddDTO, 1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(photoDTO);
        }

        [HttpPost]
        [Route("api/photos/{id}/like")]
        public async Task<ActionResult> LikePhoto(int id)
        {
            try
            {
                await _photoService.LikePhotoAsync(id, 1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/photos/{id}")]
        public async Task<ActionResult> PutPhoto(int id, [FromBody] PhotoUpdateDTO photoUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (photoUpdateDTO.Id != id)
            {
                return BadRequest();
            }

            try
            {
                await _photoService.UpdatePhotoAsync(photoUpdateDTO, 1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("api/photos/{id}")]
        public async Task<ActionResult> DeletePhoto(int id)
        {
            try
            {
                await _photoService.RemovePhotoAsync(id, 1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
