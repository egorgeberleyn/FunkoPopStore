using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.ShopCarts.Common;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.ShopCartAggregate;
using MapsterMapper;

namespace KittyStore.Application.ShopCarts.Commands.AddItem
{
    public class AddShopCartItemCommandHandler : IRequestHandler<AddShopCartItemCommand, ErrorOr<ShopCart>>
    {
        private readonly ICacheService _cacheService;
        private readonly IUserRepository _userRepository;
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;

        public AddShopCartItemCommandHandler(ICacheService cacheService, IMapper mapper, IUserRepository userRepository, ICatRepository catRepository)
        {
            _cacheService = cacheService;
            _mapper = mapper;
            _userRepository = userRepository;
            _catRepository = catRepository;
        }

        public async Task<ErrorOr<ShopCart>> Handle(AddShopCartItemCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByIdAsync(command.UserId) is not { } user)
                return Errors.User.NotFound;
        
            var cart = _mapper.Map<ShopCart>(
                           await _cacheService.GetDataAsync<ShopCartDto>(user.Id.ToString()))
                           ?? ShopCart.Create(user.Id);

            //Check that the cat is in the database but not in the cart
            if ( await _catRepository.GetCatByIdAsync(command.CatId) is not { } cat)
                return Errors.Cat.NotFound;
            if (cart.ShopCartItems.FirstOrDefault(item => item.CatId == cat.Id) is not null)
                return Errors.Cat.AlreadyExist;
       
            cart.AddItem(command.Price, cat.Id);
        
            await _cacheService.SetDataAsync(user.Id.ToString(), _mapper.Map<ShopCartDto>(cart),
                DateTimeOffset.Now.AddDays(10));
            return cart;
        }
    }
}