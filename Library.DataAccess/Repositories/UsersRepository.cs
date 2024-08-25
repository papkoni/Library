using System;
using AutoMapper;

namespace Library.DataAccess.Repositories
{
	public class UsersRepository
	{

        private readonly LibraryDbContext _context;

        private readonly IMapper _mapper;

        public UsersRepository(LibraryDbContext context, IMapper mapper)
		{
            _context = context;

            _mapper = mapper;
        }



	}
}

