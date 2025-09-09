using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff
{
    public class SeedService
    {
        private IAnimeRepository _animeRepository;
        private IGirlRepository _girlRepository;
        private IUserRepositrory _userRepositrory;

        public const string ADMIN_NAME = "Admin";

        public SeedService(IAnimeRepository animeRepository, IGirlRepository girlRepository, IUserRepositrory userRepositrory)
        {
            _animeRepository = animeRepository;
            _girlRepository = girlRepository;
            _userRepositrory = userRepositrory;
        }

        public void Seed()
        {
            FillUser();
            FillAnime();
            FillGirl();
        }

        private void FillUser()
        {
            var admin = _userRepositrory.GetByName(ADMIN_NAME);
            if (admin == null) {
                _userRepositrory.Registration(ADMIN_NAME, ADMIN_NAME);
                admin = _userRepositrory.GetByName(ADMIN_NAME);
                admin.Role = Enum.Role.Admin;
                _userRepositrory.Update(admin);
            }
        }

        private void FillGirl()
        {
            if (_girlRepository.Any())
            {
                return;
            }

            var girls = new List<Girl>();
            var admin = _userRepositrory.GetByName(ADMIN_NAME);
            var anime = _animeRepository.GetFirst();
            
            var lera = new Girl
            {
                Age = 19,
                Author = admin,
                Name = "Lera",
                Size = 4,
                Url = "https://i.pinimg.com/originals/b6/f7/48/b6f748a0e447c98a4d9da66d4bb95c4e.jpg"
            };
            girls.Add(lera);

            var olga = new Girl
            {
                Age = 25,
                Author = admin,
                Name = "Vika",
                Size = 3,
                Url = "https://i.pinimg.com/474x/f4/b1/2f/f4b12f08a2d065df757aa7033efc6cd5.jpg"
            };
            girls.Add(olga);

            var vika = new Girl
            {
                Age = 20,
                Author = admin,
                Name = "Vika",
                Size = 2,
                Url = "https://i.pinimg.com/736x/f7/b2/2e/f7b22e0bafac8e5f44d3518ed51061d6.jpg"
            };
            girls.Add(vika);

            foreach (var girl in girls)
            {
                _girlRepository.Add(girl);
            }

            anime.Characters = girls;
            _animeRepository.Update(anime);
        }

        private void FillAnime()
        {
            if (_animeRepository.Any())
            {
                return;
            }

            var deathNote = new Anime
            {
                Name = "Death Note",
                ReleasDate = DateTime.Now,
            };
            _animeRepository.Add(deathNote);

            var eva = new Anime
            {
                Name = "Eva",
                ReleasDate = DateTime.Now.AddYears(-5),
            };
            _animeRepository.Add(eva);
        }
    }
}
