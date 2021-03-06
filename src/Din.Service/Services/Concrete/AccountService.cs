﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Din.Data;
using Din.Data.Entities;
using Din.Service.Dto;
using Din.Service.Dto.Account;
using Din.Service.Dto.Context;
using Din.Service.Services.Abstractions;
using Din.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Din.Service.Services.Concrete
{
    /// <inheritdoc cref="IAccountService" />
    public class AccountService : BaseService, IAccountService
    {
        private readonly DinContext _context;
        private readonly IMapper _mapper;

        public AccountService(DinContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataDto> GetAccountDataAsync(int id)
        {
            return new DataDto
            {
                User = _mapper.Map<UserDto>(await _context.User.FirstAsync(u => u.Account.Id.Equals(id))),
                Account = _mapper.Map<AccountDto>(await _context.Account.Include(a => a.Image).FirstAsync(a => a.Id.Equals(id))),
                AddedContent = _mapper.Map<IEnumerable<AddedContentDto>>(
                    (await _context.AddedContent.Where(ac => ac.Account.Id.Equals(id)).ToListAsync()).AsEnumerable())
            };
        }

        public async Task<ResultDto> UploadAccountImageAsync(int id, string name, byte[] data)
        {
            try
            {
                var accountEntity = await _context.Account.Include(a => a.Image).FirstAsync(a => a.Id.Equals(id));
                _context.Attach(accountEntity);

                if (accountEntity.Image != null)
                {
                    accountEntity.Image.Name = name;
                    accountEntity.Image.Data = data;
                }
                else
                {
                    accountEntity.Image = new AccountImageEntity
                    {
                        Data = data,
                        Name = name
                    };
                }

                await _context.SaveChangesAsync();
                return GenerateResultDto("Image uploaded successfully",
                    "Your image is now visible on your account tab.", ResultDtoStatus.Successful);
            }
            catch
            {
                return GenerateResultDto("Image not uploaded", "Something went wrong on my side, try again later",
                    ResultDtoStatus.Unsuccessful);
            }
        }

        public async Task<ResultDto> UpdatePersonalInformation(int id, UserDto user)
        {
            try
            {
                var userEntity = await _context.User.FirstAsync(u => u.Account.Id.Equals(id));
                _context.Attach(userEntity);

                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;

                await _context.SaveChangesAsync();

                return GenerateResultDto("Update successful", "Your user information has been updated.",
                    ResultDtoStatus.Successful);
            }
            catch
            {
                return GenerateResultDto("Update unsuccessful", "Something went wrong 😵 Try again later!",
                    ResultDtoStatus.Unsuccessful);
            }
        }

        public async Task<ResultDto> UpdateAccountInformation(int id, string username, string hash)
        {
            try
            {
                var accountEntity = await _context.Account.FirstAsync(a => a.Id.Equals(id));
                _context.Attach(accountEntity);

                accountEntity.Username = username;
                accountEntity.Hash = hash;

                await _context.SaveChangesAsync();

                return GenerateResultDto("Update successful", "Your account information has been updated.",
                    ResultDtoStatus.Successful);
            }
            catch
            {
                return GenerateResultDto("Update unsuccessful", "Something went wrong 😵 Try again later!",
                    ResultDtoStatus.Unsuccessful);
            }
        }
    }
}