using QSF.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.RemoteSearchExample
{
    public class RemoteSearchViewModel : ExampleViewModel
    {
        public RemoteSearchViewModel()
        {
            this.Genres = new ObservableCollection<Genre>()
            {
                new Genre(Color.FromHex("#72CDFE"), "Action"),
                new Genre(Color.FromHex("#54CD7F"), "Adventure"),
                new Genre(Color.FromHex("#FFCE4D"), "Comedy"),
                new Genre(Color.FromHex("#9760FE"), "Crime"),
                new Genre(Color.FromHex("#C62F46"), "Drama"),
                new Genre(Color.FromHex("#FE6078"), "Epics"),
                new Genre(Color.FromHex("#FF714D"), "Musical"),
                new Genre(Color.FromHex("#9EA9B7"), "Science Fiction")
            };

            this.MovieSource = new ObservableCollection<Movie>()
            {
                new Movie("The Godfather", "Marlon Brando, Al Pacino, James Caan", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", Genres[3]),
                new Movie("The Shawshank Redemption", "Tim Robbins, Morgan Freeman, Bob Gunton", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", Genres[4]),
                new Movie("Casablanca", "Humphrey Bogart, Ingrid Bergman, Paul Henreid", "A cynical nightclub owner protects an old flame and her husband from Nazis in Morocco.", Genres[4]),
                new Movie("Forrest Gump", "Tom Hanks, Robin Wright, Gary Sinise", "The presidencies of Kennedy and Johnson, Vietnam, Watergate, and other history unfold through the perspective of an Alabama man with an IQ of 75.", Genres[4]),
                new Movie("Titanic", "Leonardo DiCaprio, Kate Winslet, Billy Zane", "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.", Genres[4]),
                new Movie("Unforgiven", "Clint Eastwood, Gene Hackman, Morgan Freeman", "Retired Old West gunslinger William Munny reluctantly takes on one last job, with the help of his old partner and a young man.", Genres[4]),
                new Movie("Rocky", "Sylvester Stallone, Talia Shire, Burt Young", "A small-time boxer gets a supremely rare chance to fight a heavy-weight champion in a bout in which he strives to go the distance for his self-respect.", Genres[4]),
                new Movie("Star Wars: Episode VI - Return of the Jedi", "Harrison Ford, Carrie Fisher, Mark Hamill", "After a daring mission to rescue Han Solo from Jabba the Hutt, the rebels dispatch to Endor to destroy a more powerful Death Star. Meanwhile, Luke struggles to help Vader back from the dark side without falling into the Emperor's trap.", Genres[5]),
                new Movie("The Matrix", "Keanu Reeves, Hugo Weaving, Carrie-Anne", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", Genres[7]),
                new Movie("The Hobbit: An Unexpected Journey", "Martin Freeman, Ian McKellen, Richard Armitage", " reluctant Hobbit, Bilbo Baggins, sets out to the Lonely Mountain with a spirited group of dwarves to reclaim their mountain home, and the gold within it from the dragon Smaug.", Genres[5]),
                new Movie("The Lord of the Rings", "Elijah Wood, Ian McKellen, Orlando Bloom", "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.", Genres[5]),
                new Movie("Gladiator", "Russell Crowe, Joaquin Phoenix, Connie Nielsen", "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.", Genres[5]),
                new Movie("Avatar", "Sam Worthington, Zoe Saldana, Sigourney Weaver", "A paraplegic marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.", Genres[7]),
                new Movie("Goodfellas", "Robert De Niro, Ray Liotta, Joe Pesci", "The story of Henry Hill and his life in the mob, covering his relationship with his wife Karen Hill and his mob partners Jimmy Conway and Tommy DeVito in the Italian-American crime syndicate.", Genres[3]),
                new Movie("The Departed", "Leonardo DiCaprio, Matt Damon, Jack Nicholson", "An undercover cop and a mole in the police attempt to identify each other while infiltrating an Irish gang in South Boston.", Genres[3]),
                new Movie("Rush Hour", "Jackie Chan, Chris Tucker, Ken Leung", "A loyal and dedicated Hong Kong inspector teams up with a reckless and loudmouthed LAPD detective to rescue the Chinese Consul's kidnapped daughter, while trying to arrest a dangerous crime lord along the way.", Genres[0]),
                new Movie("Taxi", "Samy Naceri, Frédéric Diefenthal, Marion Cotillard", "To work off his tarnished driving record, a hip taxi driver must chauffeur a loser police inspector on the trail of German bank robbers.", Genres[0]),
                new Movie("Meet the Parents", "Ben Stiller, Robert De Niro, Teri Polo", "Male nurse Greg Focker meets his girlfriend's parents before proposing, but her suspicious father is every date's worst nightmare.", Genres[2]),
                new Movie("Me, Myself and Irene", "Jim Carrey, Renée Zellweger, Anthony Anderson", "A nice-guy cop with dissociative identity disorder must protect a woman on the run from a corrupt ex-boyfriend and his associates.", Genres[2]),
                new Movie("The Mask", "Jim Carrey, Cameron Diaz, Peter Riegert", "Bank clerk Stanley Ipkiss is transformed into a manic superhero when he wears a mysterious mask.", Genres[2]),
                new Movie("Ace Ventura: When Nature Calls", "Jim Carrey, Ian McNeice, Simon Callow", "Ace Ventura, Pet Detective, returns from a spiritual quest to investigate the disappearance of a rare white bat, the sacred animal of a tribe in Africa.", Genres[1]),
                new Movie("Harry Potter", " Daniel Radcliffe, Rupert Grint, Richard Harris", "An orphaned boy enrolled in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.", Genres[5]),
                new Movie("Mamma Mia! Here We Go Again", "Lily James, Amanda Seyfried, Meryl Streep", "Five years after the events of Mamma Mia! (2008), Sophie prepares for the grand reopening of the Hotel Bella Donna as she learns more about her mother's past.", Genres[6]),
                new Movie("La La Land", "Ryan Gosling, Emma Stone, Rosemarie DeWitt", "While navigating their careers in Los Angeles, a pianist and an actress fall in love while attempting to reconcile their aspirations for the future.", Genres[6]),
                new Movie("Shrek", "Mike Myers, Eddie Murphy, Cameron Diaz", "After his swamp is filled with magical creatures, Shrek agrees to rescue Princess Fiona for a villainous lord in order to get his land back.", Genres[1]),
                new Movie("The Wizard of Oz", "Judy Garland, Frank Morgan, Ray Bolger", "Dorothy Gale is swept away from a farm in Kansas to a magical land of Oz in a tornado and embarks on a quest with her new friends to see the Wizard who can help her return home to Kansas and help her friends as well.", Genres[6])
            };
        }

        public ObservableCollection<Movie> MovieSource { get; set; }

        public ObservableCollection<Genre> Genres { get; set;}

        internal void ShowDetails(Movie item)
        {
            this.NavigationService.NavigateToConfigurationAsync<DetailsViewModel>(new DetailsViewModel(item));
        }
    }
}
