using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;

namespace Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationContext db;
        readonly Lazy<IRepository<Guid, Tour>> _toursRepository;
        readonly Lazy<IRepository<Guid, TourEvent>> _tourEventsRepository;
        readonly Lazy<IRepository<Guid, Location>> _locationsRepository;
        readonly Lazy<IRepository<Guid, Hotel>> _hotelsRepository;
        readonly Lazy<IRepository<Guid, Country>> _countriesRepository;


        public IRepository<Guid, Tour> ToursRepository => _toursRepository.Value;
        public IRepository<Guid, TourEvent> TourEventsRepository => _tourEventsRepository.Value;
        public IRepository<Guid, Location> LocationsRepository => _locationsRepository.Value;
        public IRepository<Guid, Hotel> HotelsRepository => _hotelsRepository.Value;
        public IRepository<Guid, Country> CountriesRepository => _countriesRepository.Value;

        public UnitOfWork(ApplicationContext context)
        {
            db = context;
            _toursRepository = new Lazy<IRepository<Guid, Tour>>(() => new ToursRepository(context));
            _tourEventsRepository = new Lazy<IRepository<Guid, TourEvent>>(() => new TourEventsRepository(context));
            _locationsRepository = new Lazy<IRepository<Guid, Location>>(() => new LocationsRepository(context));
            _hotelsRepository = new Lazy<IRepository<Guid, Hotel>>(() => new HotelsRepository(context));
            _countriesRepository = new Lazy<IRepository<Guid, Country>>(() => new CountriesRepository(context));

            //DbInit();
        }

        private void DbInit()
        {
            Meal meal1 = new Meal { Name = "Breakfast" };
            Meal meal2 = new Meal { Name = "Lunch" };
            Meal meal3 = new Meal { Name = "Dinner" };
            db.Meals.Add(meal1);
            db.Meals.Add(meal2);
            db.Meals.Add(meal3);

            Country country1 = new Country { Name = "United States" };
            Country country2 = new Country { Name = "Italy" };
            Country country3 = new Country { Name = "Ireland" };
            Country country4 = new Country { Name = "France" };
            Country country5 = new Country { Name = "Spain" };
            db.Countries.Add(country1);
            db.Countries.Add(country2);
            db.Countries.Add(country3);
            db.Countries.Add(country4);
            db.Countries.Add(country5);
            db.SaveChanges();

            #region Tour #1 (5*)
            Hotel hotel1 = new Hotel
            {
                Name = "Boston Park Plaza",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel2 = new Hotel
            {
                Name = "Hampton Inn & Suites Manchester",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal3 }
            };
            Hotel hotel3 = new Hotel
            {
                Name = "Doubletree Burlington by Hilton",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal3 }
            };
            Hotel hotel4 = new Hotel
            {
                Name = "Red Jacket Mountain View Resort",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            db.Hotels.Add(hotel1);
            db.Hotels.Add(hotel2);
            db.Hotels.Add(hotel3);
            db.Hotels.Add(hotel4);

            Location location1 = new Location { Name = "Boston (MA)" };
            Location location2 = new Location { Name = "Manchester" };
            Location location3 = new Location { Name = "Burlington (VT)" };
            Location location4 = new Location { Name = "North Conway" };
            db.Locations.Add(location1);
            db.Locations.Add(location2);
            db.Locations.Add(location3);
            db.Locations.Add(location4);
            db.SaveChanges();

            TourEvent tourEvent1 = new TourEvent
            {
                Name = "Stroll Through The Red Streets Of Boston",
                Description = "Welcome to Boston, the Cradle of Liberty and once home to such famous sons as Samuel Adams, Paul Revere and Benjamin Franklin. Amble up and down the City on the Hill on your terms and immerse yourself in its revolutionary history or enjoy a stroll along the waterfront. This evening, we’ll meet our Travel Director and fellow travelers for a Welcome Reception to kick-start our color-filled journey through New England landscapes.",
                DayNumber = 1,
                HotelId = hotel1.Id,
                LocationId = location1.Id
            };
            TourEvent tourEvent2 = new TourEvent
            {
                Name = "Take A Historical Drive Through New England",
                Description = "March to independence, exploring Boston's historic sites. We’ll spend the morning with our Dive Into Culture, weaving along the Freedom Trail which reveals the rich history of the American Revolution. Stop at Lexington and Concord, the site where 'the shot heard round the world' was fired. Next, we will continue to Dive Into Culture at Old Sturbridge Village, a living museum which recreates life in rural New England during the 1790s through 1830s. In the afternoon, drive along the Mohawk Trail, an early Native American and colonial trade route, and the first designated scenic drive in New England.",
                DayNumber = 2,
                HotelId = hotel2.Id,
                LocationId = location2.Id
            };
            TourEvent tourEvent3 = new TourEvent
            {
                Name = "Uncover The Craftmanship Of Vermont",
                Description = "Today we will experience the best Yankee ingenuity has to offer with a visit to the Vermont Country Store opened In 1946 by Vrest and Ellen Orton in the quaint village of Weston, Vermont. Next we'll drive past picture-perfect farmlands and old-world towns en route to the historic Quechee. Dive Into Culture and watch craftsmen create the delicate glassware that has made Simon Pearce glassblowing workshop famous, delving into the art and science of glassblowing. Then, savor a Regional Meal lunch featuring local ingredients arranged on Simon Pearce's beautiful stem and flatware.",
                DayNumber = 3,
                HotelId = hotel3.Id,
                LocationId = location3.Id
            };
            TourEvent tourEvent4 = new TourEvent
            {
                Name = "Start Your Day With Ice Cream",
                Description = "Visit Ben and Jerry’s Ice Cream Factory during your Dive Into Culture, where you’ll have an opportunity to learn about the company's history, a peek into the manufacturing process, and a Q&A in their flavor room with of course, a sample of one of their euphoric flavors. We continue today's adventure to North Conway, where you'll learn about the local flora and fauna with a Local Specialist for an ecological look at Autumn in the North Woods. Begin the adventure at the summit of Mt. Washington where man has been able to adapt in order to survive some of the most severe weather ever recorded. Then descend into the forest and valley below to take a closer look at some of the wildlife that calls New Hampshire home as well as an overview of the spectacular fall colors. The Mt. Washington Valley provides the scenic backdrop as we take this fascinating closer look at autumn.",
                DayNumber = 4,
                HotelId = hotel4.Id,
                LocationId = location4.Id
            };
            TourEvent tourEvent5 = new TourEvent
            {
                Name = "Embrace Autumn Hues In North Conway",
                Description = "Journey along 'The Kanc', an American Scenic Byway that cuts through the White Mountain National Forest, admiring the glowing fall colors. Then this afternoon you will have an optional opportunity to learn the true nature and history of Mount Washington and the historical Auto Road. Driven by accomplished and dedicated tour guides, your “stage driver” will offer stories, anecdotes, legends, history and insight into the ecological wonder of Mount Washington, as well as point out interesting features and scenic opportunities from the base to the 6,288-foot summit.",
                DayNumber = 5,
                HotelId = hotel4.Id,
                LocationId = location4.Id
            };
            db.TourEvents.Add(tourEvent1);
            db.TourEvents.Add(tourEvent2);
            db.TourEvents.Add(tourEvent3);
            db.TourEvents.Add(tourEvent4);
            db.TourEvents.Add(tourEvent5);
            db.SaveChanges();

            Tour tour1 = new Tour
            {
                Name = "Autumn Colors",
                Country = country1,
                Rating = 5,
                DaysCount = 5,
                Price = 2250,
                 Photo = "https://www.getours.com/media/1840/usa-vermont-barn.jpg?center=0.16355140186915887,0.49221183800623053&mode=crop&width=640&height=386&rnd=132714468570000000",
                TourEvents = new List<TourEvent> { tourEvent1, tourEvent2, tourEvent3, tourEvent4, tourEvent5 }
            };
            db.Tours.Add(tour1);
            db.SaveChanges();
            #endregion

            #region Tour #2 (5*)
            Hotel hotel5 = new Hotel
            {
                Name = "Clift Royal Sonesta",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel6 = new Hotel
            {
                Name = "Embassy Suites Napa Valley",
                Rating = 4,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel7 = new Hotel
            {
                Name = "Lake Tahoe Resort",
                Rating = 4,
                Meals = new List<Meal> { meal1, meal3 }
            };
            Hotel hotel8 = new Hotel
            {
                Name = "Yosemite Valley Lodge",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2 }
            };
            db.Hotels.Add(hotel5);
            db.Hotels.Add(hotel6);
            db.Hotels.Add(hotel7);
            db.Hotels.Add(hotel8);

            Location location5 = new Location { Name = "San Francisco" };
            Location location6 = new Location { Name = "Napa" };
            Location location7 = new Location { Name = "Lake Tahoe" };
            Location location8 = new Location { Name = "Virginia City" };
            db.Locations.Add(location5);
            db.Locations.Add(location6);
            db.Locations.Add(location7);
            db.Locations.Add(location8);
            db.SaveChanges();

            TourEvent tourEvent6 = new TourEvent
            {
                Name = "Hello San Francisco",
                Description = "From the iconic to the unexpected, the city of San Francisco never ceases to surprise. Kick-start your effortlessly delivered Northern California holiday in the cosmopolitan hills of ‘The City’. Join your Travel Director and fellow travelers for a Welcome Reception at your hotel.",
                DayNumber = 1,
                HotelId = hotel5.Id,
                LocationId = location5.Id
            };
            TourEvent tourEvent7 = new TourEvent
            {
                Name = "Dive Into Eclectic San Francisco",
                Description = "Fuel up for a full day of indulging in San Francisco’s many sites. Admire the eclectic mix of Victorian and Modern architecture, vibrant culture, and cuisines of one of the world's most livable and iconic cities. Embark on a morning exploration tour that takes you from the lively hotspot of Fisherman's Wharf to Union Square, a historical landmark linked to the American Civil War. This afternoon’s Your Choice Sightseeing offers a deeper look into two of the city’s iconic spots. A guided Golden Gate Bridge walk takes you across the 1.7-mile span of the magnificent 'bridge that couldn't be built.' As you gaze across the water (one the Seven Wonders of the Modern World, learn how this iconic engineering marvel was constructed). You’ll arrive in the quaint seaside community of Sausalito with time for shopping or lunch before finishing up with a different view of the bay: a relaxing ferry cruise back to the city. If you choose to explore Golden Gate Park with your Travel Director, you’ll love learning how this urban oasis of over 1,000 acres supports 7 distinct ecosystems as you stroll the green lawns, bridle paths, and lakes of this expansive park right in the heart of San Francisco.",
                DayNumber = 2,
                HotelId = hotel5.Id,
                LocationId = location5.Id
            };
            TourEvent tourEvent8 = new TourEvent
            {
                Name = "Journey To Napa Wine Country",
                Description = "Start off your day with breakfast in San Francisco before we head out to Sonoma and Napa Valley. Today, we'll visit a local winery and enjoy a unique experience and exploration of wine with a tasting. Enjoy an included lunch this afternoon with fellow travelers.",
                DayNumber = 3,
                HotelId = hotel6.Id,
                LocationId = location6.Id
            };
            TourEvent tourEvent9 = new TourEvent
            {
                Name = "Entice Your Senses At Lake Tahoe",
                Description = "Travel through the scenic fertile Sacramento River Valley to find yourself in the alpine splendor of Lake Tahoe. Its clear blue waters surrounded by evergreen pines and rocky outcrops provides you with a picturesque scenery to behold. This evening, enjoy a Regional Dinner as you cruise on the beautiful Lake Tahoe.",
                DayNumber = 4,
                HotelId = hotel7.Id,
                LocationId = location7.Id
            };
            TourEvent tourEvent10 = new TourEvent
            {
                Name = "Embrace The Wilderness Of Yosemite National Park",
                Description = "Touch the sky and embark on a scenic road-trip into the Sierra Nevada mountain range, winding along the Tioga Pass to California’s UNESCO-listed Yosemite National Park. (Weather conditions may cause the Tioga Pass to be closed to traffic, typically from mid October to the end of May. In this case, an alternate entry point to Yosemite will be used). Marvel at the incredible natural beauty that inspired naturalist John Muir to dub Yosemite Valley the “Incomparable Valley”, passing the colossal granite faces of El Capitan and the misty splendor of Bridalveil Fall. Tonight, our Stays with Stories takes us overnight in the park.",
                DayNumber = 5,
                HotelId = hotel8.Id,
                LocationId = location8.Id
            };
            db.TourEvents.Add(tourEvent6);
            db.TourEvents.Add(tourEvent7);
            db.TourEvents.Add(tourEvent8);
            db.TourEvents.Add(tourEvent9);
            db.TourEvents.Add(tourEvent10);
            db.SaveChanges();

            Tour tour2 = new Tour
            {
                Name = "Northern California",
                Country = country1,
                Rating = 5,
                DaysCount = 5,
                Price = 2150,
                Photo = "https://www.getours.com/media/2160/usa-california-lake-tahoe.jpg?crop=0,0,0,0.095251199734607009&cropmode=percentage&width=640&height=386&rnd=132295375490000000",
                TourEvents = new List<TourEvent> { tourEvent6, tourEvent7, tourEvent8, tourEvent9, tourEvent10 }
            };
            db.Tours.Add(tour2);
            db.SaveChanges();
            #endregion

            #region Tour #3 (4*)
            Hotel hotel9 = new Hotel
            {
                Name = "Rio All Suite Hotel and Casino",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal3 }
            };
            Hotel hotel10 = new Hotel
            {
                Name = "Days Hotel by Wyndham",
                Rating = 4,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel11 = new Hotel
            {
                Name = "Quality Inn View of Lake Powell - Page",
                Rating = 4,
                Meals = new List<Meal> { meal1, meal3 }
            };
            Hotel hotel12 = new Hotel
            {
                Name = "Kayenta Monument Valley Inn",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2 }
            };
            db.Hotels.Add(hotel9);
            db.Hotels.Add(hotel10);
            db.Hotels.Add(hotel11);
            db.Hotels.Add(hotel12);

            Location location9 = new Location { Name = "Las Vegas (NV)" };
            Location location10 = new Location { Name = "Flagstaff" };
            Location location11 = new Location { Name = "Grand Canyon" };
            Location location12 = new Location { Name = "Page (Lake Powell)" };
            Location location13 = new Location { Name = "Kayenta" };
            db.Locations.Add(location9);
            db.Locations.Add(location10);
            db.Locations.Add(location11);
            db.Locations.Add(location12);
            db.Locations.Add(location13);
            db.SaveChanges();

            TourEvent tourEvent11 = new TourEvent
            {
                Name = "Arrive Las Vegas",
                Description = "An entertainment oasis surrounded by desert, with a reputation almost as big as the fortune's many have made (and lost) here, Sin City is a place unlike any other. The entertainment capital of the world, Vegas is famed for its glittering casinos, all you can eat buffets and shotgun weddings ordained by none other than Elvis himself! After settling in to your hotel and meeting up with your Travel Director this afternoon, the bright lights and lively antics of Vegas will undoubtedly lure you in. Take a walk down the Strip, where the city's finest hotels and grandest casinos can be found, or head over to the Downtown area to explore the artsy Fremont East district.",
                DayNumber = 1,
                HotelId = hotel9.Id,
                LocationId = location9.Id
            };
            TourEvent tourEvent12 = new TourEvent
            {
                Name = "Las Vegas – Flagstaff (2 Nights)",
                Description = "Are you ready to get your kicks on Route 66? If the answer is yes, today's the day of your tour to do so! Heading east, you'll take your place in road tripping history as you cruise down the most iconic road in the United States. Along the way, you'll stop off in the famous towns of Kingman and Seligman, known as the “Birthplace of Historic Route 66”, for a glimpse of all things Americana, complete with classic motels, kitsch petrol pumps and steaming slices of apple pie. Arrive in Flagstaff and rest up before tomorrow's full-day excursion to see the extraordinary landscapes of the Grand Canyon.",
                DayNumber = 2,
                HotelId = hotel10.Id,
                LocationId = location10.Id
            };
            TourEvent tourEvent13 = new TourEvent
            {
                Name = "Grand Canyon Excursion",
                Description = "Nothing can quite prepare you for your first glimpse of the Grand Canyon. One of the world's 7 natural wonders and a UNESCO World Heritage site, it's estimated to be around 2 billion years old, with a layer of rock at the bottom of the canyon said to be some of the oldest exposed rock on the planet. Take your time exploring the incredible geology of the land, or to make your visit extra special, why not treat yourself to an Optional Experience^ that could see you flying in an aircraft over the North and South rims. Feeling distinctly in awe you'll then visit the Grand Canyon Village, which boasts some impressive national historic landmarks and historical buildings.",
                DayNumber = 3,
                HotelId = hotel11.Id,
                LocationId = location11.Id
            };
            TourEvent tourEvent14 = new TourEvent
            {
                Name = "Flagstaff – Lake Powell – Page",
                Description = "This morning before you depart your Flagstaff hotel, enjoy an Iconic Breakfast at Charly's Pub & Grill. You'll then make a stop at Wupatki National Monument, a landscape dotted with over 2700 pueblo ruins built by the ancestors of the Hopi, Zuni and Navajo people. Driving on through the Painted Desert, named for its rainbow of colourful sedimentary layers stretching some 418 kilometers into the horizon, you'll then arrive at sparkling Lake Powell. America's second largest man made reservoir and something of a mirage in an otherwise landlocked desert, you'll have the chance to board a cruise that will open up the fascinating geography of the area. Travelling through canyons and spotting towering Navajo sandstone formations that boggle the mind, this is an optional not to be missed. Tonight, you'll spend the evening in the Arizonan city of Page.",
                DayNumber = 4,
                HotelId = hotel11.Id,
                LocationId = location12.Id
            };
            TourEvent tourEvent15 = new TourEvent
            {
                Name = "Page – Horseshoe Bend – Monument Valley – Kayenta",
                Description = "Giving the term ‘iconic' a whole new meaning, Horseshoe Bend is first up on today's roll call of phenomenal sites. Carving a perfect horseshoe shape through the 1200 feet (366 m) sandstone cliffs, test your nerve as you peak over the edge before heading on to explore Monument Valley in an open air-vehicle. Immortalised as the setting for countless movies and TV shows, Monument Valley is an emblem of the wild west, with its rusty red spindles and grand buttes thrusting 1200 feet (366 m) skyward from the barren valley below. Named the ‘Valley Between the Rocks' by the Navajo people who have lived here for centuries, you'll likely feel very small and insignificant as you learn about the geology and history of the area.",
                DayNumber = 5,
                HotelId = hotel12.Id,
                LocationId = location13.Id
            };
            db.TourEvents.Add(tourEvent11);
            db.TourEvents.Add(tourEvent12);
            db.TourEvents.Add(tourEvent13);
            db.TourEvents.Add(tourEvent14);
            db.TourEvents.Add(tourEvent15);
            db.SaveChanges();

            Tour tour3 = new Tour
            {
                Name = "Canyon Country Showcase",
                Country = country1,
                Rating = 4,
                DaysCount = 5,
                Price = 950,
                Photo = "https://www.getours.com/media/10990/usa-utah-bryce-canyon-national-park-scenic.jpg?crop=0,0.013333333333333334,0,0.38365159128978221&cropmode=percentage&width=640&height=386&rnd=132350565090000000",
                TourEvents = new List<TourEvent> { tourEvent11, tourEvent12, tourEvent13, tourEvent14, tourEvent15 }
            };
            db.Tours.Add(tour3);
            db.SaveChanges();
            #endregion

            #region Tour #4 (5*)
            Hotel hotel13 = new Hotel
            {
                Name = "Hotel Romanico Palace, Rome.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel14 = new Hotel
            {
                Name = "Hotel Adler Cavalieri, Florence.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel15 = new Hotel
            {
                Name = "Splendid Venice - Starhotels Collezione.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            db.Hotels.Add(hotel13);
            db.Hotels.Add(hotel14);
            db.Hotels.Add(hotel15);

            Location location14 = new Location { Name = "Rome" };
            Location location15 = new Location { Name = "Florence" };
            Location location16 = new Location { Name = "Venice" };
            db.Locations.Add(location14);
            db.Locations.Add(location15);
            db.Locations.Add(location16);
            db.SaveChanges();

            TourEvent tourEvent16 = new TourEvent
            {
                Name = "Welcome to the Eternal City of Rome",
                Description = "Welcome to Rome, a glorious city filled with classic antiquities, medieval buildings, Renaissance palaces and Baroque churches. Transfers depart from Rome's Fiumicino Leonardo da Vinci Airport for your hotel at 09:30, 12:30 and 15:30.Relax and settle in before joining your Travel Director at 17:30, and get to know your fellow travellers over a delicious Welcome Dinner with wine at a local restaurant.(Dinner with Wine)",
                DayNumber = 1,
                HotelId = hotel13.Id,
                LocationId = location14.Id
            };
            TourEvent tourEvent17 = new TourEvent
            {
                Name = "VIP Vatican and Rome with a Local)",
                Description = "The day is devoted to the Eternal City. Join a Local Expert for an in-depth visit to the Vatican Museums, full of priceless art treasures collected by the Popes. Insight has arranged special VIP entry to avoid the lines and gain behind-the-scenes access to the Bramante Staircase, a marvel of Renaissance architecture, normally off-limits to visitors. Visit the Sistine Chapel, where the Popes are elected, to admire Michelangelo’s famous ceiling. Continue to St. Peter’s Basilica, to wonder at another of his works, the emotive ‘Pietà,’ and stand beneath the dome completed after his death. Cross the Tiber river to enjoy ancient Rome. See the incredible Circus Maximus. Come up close and personal with the beautifully preserved Arch of Constantine, walking on the ancient pavement. Discover the history, the construction and the legends of the iconic Colosseum as you stroll around the exterior of this amazing structure. Later, perhaps continue your exploration of Rome to see the Pantheon, toss a coin in Trevi Fountain and enjoy free time around Piazza Navona?(Breakfast)",
                DayNumber = 2,
                HotelId = hotel13.Id,
                LocationId = location14.Id
            };
            TourEvent tourEvent18 = new TourEvent
            {
                Name = "Tuscan Hills & Medieval Florence",
                Description = "Travel through Tuscany to the house of the Italian Renaissance diplomat and writer, Niccolo Machiavelli. Indulge in a private picnic of seasonal produce on the grounds of the estate. Drive to the Chianti Hills and the city of Florence. On arrival, you will be privy to the secrets of traditional Florentine leatherwork when you visit a local workshop, and see a gold demonstration.(Breakfast / Lunch with Wine / Dinner with Wine)",
                DayNumber = 3,
                HotelId = hotel14.Id,
                LocationId = location15.Id
            };
            TourEvent tourEvent19 = new TourEvent
            {
                Name = "Florence, Birthplace of the Renaissance",
                Description = "Explore Florence with Insight Choice. Choose between a visit to the Accademia Museum with an art historian to see Michelangelo's David or alternatively, wander through the market stalls on a guided shopping tour and enjoy all the treats and trinkets on show. Walk on to the multi-coloured marble cathedral, bell tower and baptistery, where Ghiberti’s Gates of Paradise have been recreated in all their glory. Visit Piazza della Signoria, still the political heart of Florence and an open-air gallery of Renaissance masterpieces. Spend time gazing at statues such as Neptune as well as Hercules and Cacus, along the front of the Palazzo Vecchio.(Breakfast)",
                DayNumber = 4,
                HotelId = hotel14.Id,
                LocationId = location15.Id
            };
            TourEvent tourEvent20 = new TourEvent
            {
                Name = "Pisa to Venice, the Queen of the Adriatic",
                Description = "Journey to the coastal town of Pisa and see the famous Leaning Tower in the Square of Miracles, as well as the baptistery and colonnaded bell tower behind the cathedral. Relax and enjoy the scenery as you cross the spectacular Apennine Mountains, through the Emilia-Romagna region - one of Europe's most intensely farmed fertile plains. Continue through the marshes of the Venetian Lagoon before crossing the causeway into the floating city of Venice. As evening falls, day-trippers desert the city to return to the mainland. Appreciate the peace and tranquillity of Venice from your centrally located hotel. This evening, indulge in a delicious dinner at a local restaurant.(Breakfast / Dinner with Wine)",
                DayNumber = 5,
                HotelId = hotel15.Id,
                LocationId = location16.Id
            };
            db.TourEvents.Add(tourEvent16);
            db.TourEvents.Add(tourEvent17);
            db.TourEvents.Add(tourEvent18);
            db.TourEvents.Add(tourEvent19);
            db.TourEvents.Add(tourEvent20);
            db.SaveChanges();

            Tour tour4 = new Tour
            {
                Name = "Best of Italy (Premium)",
                Country = country2,
                Rating = 5,
                DaysCount = 5,
                Price = 2950,
                Photo = "https://www.getours.com/media/3908/italy-vatican-st-peters-with-street-square.jpg?anchor=center&mode=crop&width=640&height=386&rnd=132294480630000000",
                TourEvents = new List<TourEvent> { tourEvent16, tourEvent17, tourEvent18, tourEvent19, tourEvent20 }
            };
            db.Tours.Add(tour4);
            db.SaveChanges();
            #endregion

            #region Tour #5 (3*)
            Hotel hotel16 = new Hotel
            {
                Name = "Ergife Palace",
                Rating = 3,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel17 = new Hotel
            {
                Name = "La Panoramica",
                Rating = 3,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel18 = new Hotel
            {
                Name = "Frate Sole",
                Rating = 3,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel19 = new Hotel
            {
                Name = "Il Burchiello",
                Rating = 3,
                Meals = new List<Meal> { meal1 }
            };
            db.Hotels.Add(hotel16);
            db.Hotels.Add(hotel17);
            db.Hotels.Add(hotel18);
            db.Hotels.Add(hotel19);

            Location location17 = new Location { Name = "Rome" };
            Location location18 = new Location { Name = "Rome, Sorrento" };
            Location location19 = new Location { Name = "Assisi" };
            Location location20 = new Location { Name = "Assisi, Venice" };
            db.Locations.Add(location17);
            db.Locations.Add(location18);
            db.Locations.Add(location19);
            db.Locations.Add(location20);
            db.SaveChanges();

            TourEvent tourEvent21 = new TourEvent
            {
                Name = "Arrive Rome (2 Nights)",
                Description = "The 'Eternal City' has captivated travellers for centuries and you'll not be disappointed as it prepares you for an exhilarating journey across Italy. On arrival in Rome, check in to your hotel, then get ready for your very own introduction to Rome or relax at your hotel and recover from your journey. Later this evening, you'll meet your Travel Director and fellow travellers for a glimpse into what lies ahead along your journey. Soak up the city's effervescent atmosphere during an optional sightseeing tour followed by a light dinner of Italian specialties.",
                DayNumber = 1,
                HotelId = hotel16.Id,
                LocationId = location17.Id
            };
            TourEvent tourEvent22 = new TourEvent
            {
                Name = "Rome sightseeing and free time",
                Description = "With so much to see and do, you could explore Rome for an eternity without getting bored. Join an Optional Experience to Michelangelo's famous Sistine Chapel and the Vatican Museums or start your day the Italian way - slow and relaxed. Get to grips with the city's main attractions, joining a Local Specialist at the Vatican City, the world's smallest country. Stand in St. Peter's Square, sharing your space with devoted pilgrims and pigeons. Visit St. Peter's Basilica and view Michelangelo's greatest work, the sorrowful Pietà. Cross the Tiber next, to view the mighty Colosseum and the countless other ancient sites which featured prominently in the days of the powerful Roman Empire",
                DayNumber = 2,
                HotelId = hotel16.Id,
                LocationId = location17.Id
            };
            TourEvent tourEvent23 = new TourEvent
            {
                Name = "Rome – Sorrento – Bay of Naples (1 Night)",
                Description = "The Bay of Naples beckons and our first stop is in the picture-perfect resort town of Sorrento. Pastel-hued residences cling impossibly to craggy cliffs that plunge into the deep blue waters of the Tyrrhenian Sea. Enjoy your time soaking up the sun-drenched views. You won't need to venture far to catch the scent of the 'world's best lemons', which you'll see in abundance at the local markets.Taste some of the mouth - watering fresh produce grown locally or consider an Optional Experience to the artists' paradise of Positano, followed by a scenic drive along the Amalfi coast. The Sirens of Sorrento may be nowhere in sight, but that doesn't mean their alluring call will be possible to resist",
                DayNumber = 3,
                HotelId = hotel17.Id,
                LocationId = location18.Id
            };
            TourEvent tourEvent24 = new TourEvent
            {
                Name = "Bay of Naples – Cassino – Assisi (1 Night)",
                Description = "Soak up the atmosphere as you explore the beautiful Bay of Naples during your free time this morning. You could choose instead to join an Optional Experience guided walking tour of Pompeii, buried under the volcanic ash for centuries after Mount Vesuvius erupted. Leaving the Bay of Naples behind, journey past Cassino and view the monastery which featured as a backdrop for some of the fiercest battles of World War II. This poignant reminder of war gives way to a place of pilgrimage, as you head north to Assisi where an orientation reveals a medieval town that has changed very little since the time of St. Francis.",
                DayNumber = 4,
                HotelId = hotel18.Id,
                LocationId = location19.Id
            };
            TourEvent tourEvent25 = new TourEvent
            {
                Name = "Assisi – Venice (Oriago)",
                Description = "After a leisurely morning, your journey continues across the Apennines and through the Emilia-Romagna region, credited with being the origin of tortellini, mortadella and parmesan cheese. Before you know it, you'll have arrived in Venice, the Queen of the Adriatic, where an Optional Experience will see you gliding through the city canals in a gondola.",
                DayNumber = 5,
                HotelId = hotel19.Id,
                LocationId = location20.Id
            };
            db.TourEvents.Add(tourEvent21);
            db.TourEvents.Add(tourEvent22);
            db.TourEvents.Add(tourEvent23);
            db.TourEvents.Add(tourEvent24);
            db.TourEvents.Add(tourEvent25);
            db.SaveChanges();

            Tour tour5 = new Tour
            {
                Name = "Italian Scene",
                Country = country2,
                Rating = 3,
                DaysCount = 5,
                Price = 850,
                Photo = "https://www.getours.com/media/5473/italy-gelato-florence.jpg?anchor=center&mode=crop&width=640&height=386&rnd=132294432960000000",
                TourEvents = new List<TourEvent> { tourEvent21, tourEvent22, tourEvent23, tourEvent24, tourEvent25 }
            };
            db.Tours.Add(tour5);
            db.SaveChanges();
            #endregion

            #region Tour #6 (5*)
            Hotel hotel23 = new Hotel
            {
                Name = "Clayton Hotel Burlington/Clayton Hotel Charlemont, Dublin.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel24 = new Hotel
            {
                Name = "Maldron Hotel South Mall, Cork.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel25 = new Hotel
            {
                Name = "Killarney Avenue Hotel.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            db.Hotels.Add(hotel23);
            db.Hotels.Add(hotel24);
            db.Hotels.Add(hotel25);

            Location location24 = new Location { Name = "Dublin" };
            Location location25 = new Location { Name = "Kildare, Cork" };
            Location location26 = new Location { Name = "Killarney" };
            db.Locations.Add(location24);
            db.Locations.Add(location25);
            db.Locations.Add(location26);
            db.SaveChanges();

            TourEvent tourEvent31 = new TourEvent
            {
                Name = "Welcome to Dublin",
                Description = "On arrival at Dublin airport, your airport transfer service leaves for your hotel at 08:30, 11:00 and 13:00. Join your Travel Director at 18:30 for a warm and friendly Welcome Dinner, an opportunity to get to know each other over a meal with wine.(Dinner with Wine)",
                DayNumber = 1,
                HotelId = hotel23.Id,
                LocationId = location24.Id
            };
            TourEvent tourEvent32 = new TourEvent
            {
                Name = "In Dublin’s Fair City",
                Description = "An orientation with your Travel Director will take you past the Custom House and travel along the River Liffey. See the Guinness Storehouse, Christchurch Cathedral and St. Patrick's Cathedral. Enjoy your VIP priority entrance to the 9th century Book of Kells and a walk through the cobbled courtyards of Trinity College accompanied by a university insider, gaining their insight into modern life and the establishment's impressive history.(Breakfast)",
                DayNumber = 2,
                HotelId = hotel23.Id,
                LocationId = location24.Id
            };
            TourEvent tourEvent33 = new TourEvent
            {
                Name = "Kildare and Cork",
                Description = "Witness life behind the scenes of the National Stud Farm, where many legend racehorses are bred, before tracing the journey of the soul through life in the finest Japanese gardens in Europe, designed by master horticulturists, Tassa and Minoru. Continue on to Cork, Ireland's second most populous city, built on the River Lee. Travel along Grand Parade to see Cork's City Hall and Opera House.(Breakfast)",
                DayNumber = 3,
                HotelId = hotel24.Id,
                LocationId = location25.Id
            };
            TourEvent tourEvent34 = new TourEvent
            {
                Name = "Blarney Castle and Killarney",
                Description = "Visit the ruined 15th century Blarney Castle, home of the famous Stone of Eloquence - once kissed, never forgotten! Or enjoy a sensory walk in the extensive and varied estate gardens. The rest of the afternoon is at leisure.(Breakfast / Dinner with Wine)",
                DayNumber = 4,
                HotelId = hotel25.Id,
                LocationId = location26.Id
            };
            TourEvent tourEvent35 = new TourEvent
            {
                Name = "Ring of Kerry Experience",
                Description = "Explore Killarney with Insight Choice. Choose between a memorable ride on a horse-drawn jaunting car with the local jarveys, or join a gentle hike with a Local Expert along the lake shore and past the grounds of Ross Castle, admiring the incredible scenery of the National Park. While there aren't any mountains to climb, walkers should be fit, have good walking shoes or boots, sunscreen and rain gear.Tour the superb seascapes, towering cliffs and spectacular mountains of the Ring of Kerry. Journey past lush lakelands and through delightful villages like Sneem. Cross the peat bogs of the Black Mountains to Moll's Gap and admire Queen Victoria's Ladies View. Upon returning in the mid-afternoon, the rest of the day is at leisure.(Breakfast)",
                DayNumber = 5,
                HotelId = hotel25.Id,
                LocationId = location26.Id
            };
            db.TourEvents.Add(tourEvent31);
            db.TourEvents.Add(tourEvent32);
            db.TourEvents.Add(tourEvent33);
            db.TourEvents.Add(tourEvent34);
            db.TourEvents.Add(tourEvent35);
            db.SaveChanges();

            Tour tour6 = new Tour
            {
                Name = "Best of Ireland and Scotland (Premium)",
                Country = country3,
                Rating = 5,
                DaysCount = 5,
                Price = 2550,
                Photo = "https://www.getours.com/media/10818/scotland-edinburgh-castle-tourists-with-cannon.jpg?anchor=center&mode=crop&width=640&height=386&rnd=132443138230000000",
                TourEvents = new List<TourEvent> { tourEvent31, tourEvent32, tourEvent33, tourEvent34, tourEvent35 }
            };
            db.Tours.Add(tour6);
            db.SaveChanges();
            #endregion

            #region Tour #7 (4*)
            Hotel hotel26 = new Hotel
            {
                Name = "Clayton Hotel Burlington Road, Dublin.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel27 = new Hotel
            {
                Name = "Killarney Plaza Hotel and Spa.",
                Rating = 4,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel28 = new Hotel
            {
                Name = "Connemara Coast Hotel, Galway",
                Rating = 4,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            db.Hotels.Add(hotel26);
            db.Hotels.Add(hotel27);
            db.Hotels.Add(hotel28);

            Location location27 = new Location { Name = "Dublin" };
            Location location28 = new Location { Name = "Cashel" };
            Location location29 = new Location { Name = "Killarney" };
            Location location30 = new Location { Name = "Galway" };
            db.Locations.Add(location27);
            db.Locations.Add(location28);
            db.Locations.Add(location29);
            db.Locations.Add(location30);
            db.SaveChanges();

            TourEvent tourEvent36 = new TourEvent
            {
                Name = "In Dublin’s Fair city",
                Description = "Welcome to Dublin. On arrival at Dublin airport, your airport transfer shuttle service leaves for your hotel at 08:30, 11:00 and 13:00. Meet your Travel Director at 14:30 for an orientation tour that brings to life Dublin’s architectural treasures and history, stretching back more than 1,000 years. Travel along the River Liffey, see Custom House, Guinness Storehouse, Christchurch Cathedral and St. Patrick's Cathedral. Enjoy your VIP priority entry to the 9th century Book of Kells and a walk through the cobbled courtyards of Trinity College, accompanied by a university insider who will share their insights into modern life and the establishment’s impressive history. Back at your hotel, join your Travel Director for a warm and friendly Welcome Reception with drinks and the opportunity to mingle and get to know each other over a pleasant evening of dinner and wine.(Dinner with Wine)",
                DayNumber = 1,
                HotelId = hotel26.Id,
                LocationId = location27.Id
            };
            TourEvent tourEvent37 = new TourEvent
            {
                Name = "Cashel and Blarney, heading for Killarney",
                Description = "Depart Dublin, and pass the famous Curragh Racecourse and green meadows where many of the finest Irish racehorses are bred. Explore Blarney with Insight Choice. Visit Blarney Castle where you climb to the ramparts and kiss the magical stone to ensure a lifetime of eloquence or meet a senior gardener for a tour of Blarney Gardens.Head to Killarney, where you may fancy a ‘pint of porter.' This town on the lakes is famous for its lively pubs so in the evening why not join in the ‘craic’?(Breakfast / Dinner with Wine)",
                DayNumber = 2,
                HotelId = hotel27.Id,
                LocationId = location28.Id
            };
            TourEvent tourEvent38 = new TourEvent
            {
                Name = "Ring of Kerry Experience",
                Description = "In the morning, marvel at spectacular mountain and sea views as you travel the road that winds around the beautiful Iveragh Peninsula, better known as the Ring of Kerry. The rest of the day is at leisure.(Breakfast)",
                DayNumber = 3,
                HotelId = hotel27.Id,
                LocationId = location29.Id
            };
            TourEvent tourEvent39 = new TourEvent
            {
                Name = "The Cliffs of Moher and on to Galway",
                Description = "Cross the Shannon Estuary by ferry and enter County Clare. Stop at the Exhibition Centre and enjoy a walk along the Cliffs of Moher, raised approximately 700 feet above the pounding Atlantic waves. Then travel north across the Burren, a suddenly stark and barren landscape, scarred with interesting limestone creases. Journey to Galway, the City of the Tribes, and visit the lovely cathedral. Take in the scenery from your hotel, which is spectacularly set on the shores of Galway Bay.(Breakfast / Dinner with Wine)",
                DayNumber = 4,
                HotelId = hotel28.Id,
                LocationId = location30.Id
            };
            TourEvent tourEvent40 = new TourEvent
            {
                Name = "Captivating Connemara",
                Description = "Sitting on the edge of Lough Corrib and nestled on the border of County Galway and County Mayo, is the delightful Cong. You may recognise much of the town as it was used as the location in the 1952 film, 'The Quiet Man.' With free time to explore this quintessential Irish village, see the statue of the lead characters played by John Wayne and Maureen O'Hara, and the ruins of the Medieval Cong Abbey. Then a short drive down the road leads you to Ashford Castle, the highlight of your travels. As you arrive, lone piper escorts you over the drawbridge.Your exquisite hotel provides a romantic backdrop in superior surroundings, offering both relaxation and comfort. There are many opportunities to explore the vast estate, pamper yourself at the spa, or get to know the hawks and woodland surroundings at the hotel's falconry school. Dining at Ashford Castle ranks among the finest culinary experiences in the world. Creative cooking, locally sourced ingredients and an innovative menu await at your Celebration Dinner. Gentlemen, a jacket is required.(Breakfast / Dinner with Wine)",
                DayNumber = 5,
                HotelId = hotel28.Id,
                LocationId = location30.Id
            };
            db.TourEvents.Add(tourEvent36);
            db.TourEvents.Add(tourEvent37);
            db.TourEvents.Add(tourEvent38);
            db.TourEvents.Add(tourEvent39);
            db.TourEvents.Add(tourEvent40);
            db.SaveChanges();

            Tour tour7 = new Tour
            {
                Name = "Irish Elegance",
                Country = country3,
                Rating = 4,
                DaysCount = 5,
                Price = 1350,
                Photo = "https://www.getours.com/media/4263/ireland-ashford-castle-and-grounds.jpg?center=0.5280898876404494,0.45&mode=crop&width=640&height=386&rnd=132294388530000000",
                TourEvents = new List<TourEvent> { tourEvent36, tourEvent37, tourEvent38, tourEvent39, tourEvent40 }
            };
            db.Tours.Add(tour7);
            db.SaveChanges();
            #endregion

            #region Tour #8 (5*)
            Hotel hotel29 = new Hotel
            {
                Name = "Paris Marriott Rive Gauche Hotel.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel30 = new Hotel
            {
                Name = "Grand Hotel Dijon La Cloche, Dijon.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel31 = new Hotel
            {
                Name = "Hotel Mercure Chamonix.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel32 = new Hotel
            {
                Name = "Park Hotel Grenoble Mgallery, Grenoble.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            db.Hotels.Add(hotel29);
            db.Hotels.Add(hotel30);
            db.Hotels.Add(hotel31);
            db.Hotels.Add(hotel32);

            Location location31 = new Location { Name = "Paris" };
            Location location32 = new Location { Name = "Dijon" };
            Location location33 = new Location { Name = "Chamonix" };
            Location location34 = new Location { Name = "Annecy, Grenoble" };
            db.Locations.Add(location31);
            db.Locations.Add(location32);
            db.Locations.Add(location33);
            db.Locations.Add(location34);
            db.SaveChanges();

            TourEvent tourEvent41 = new TourEvent
            {
                Name = "Welcome to Paris",
                Description = "On arrival at Charles de Gaulle Airport, transfers leave for the hotel at 08:30, 11:00 and 13:30. After checking in, the capital’s grand boulevards and world-famous landmarks are yours to explore. At 17:30, join your Travel Director and fellow guests for a Welcome Dinner at Rural, a local restaurant with a menu inspired from home-made country recipes.(Dinner with Wine)",
                DayNumber = 1,
                HotelId = hotel29.Id,
                LocationId = location31.Id
            };
            TourEvent tourEvent42 = new TourEvent
            {
                Name = "Champagne Region and on to Dijon",
                Description = "Journey through the Champagne region, famous for its magnificent core of medieval buildings, appropriately shaped like a champagne cork. Stop for a taste of the bubbles which have made this region famous worldwide, with an expert vintner. Continue on to your hotel in Dijon.(Breakfast / Dinner with Wine)",
                DayNumber = 2,
                HotelId = hotel30.Id,
                LocationId = location32.Id
            };
            TourEvent tourEvent43 = new TourEvent
            {
                Name = "Free Time in Dijon",
                Description = "Enjoy the Burgundy capital of Dijon. Its many handsome buildings recall the time when the Dukes of Burgundy were more powerful than the Kings of France. Perhaps take the opportunity to visit the wine capital of Burgundy, Beaune? In the evening, don't miss the chance of an optional Burgundy wine tasting and traditional dinner in the Premier Cru village of Fixin.(Breakfast)",
                DayNumber = 3,
                HotelId = hotel30.Id,
                LocationId = location32.Id
            };
            TourEvent tourEvent44 = new TourEvent
            {
                Name = "Chamonix",
                Description = "Travel towards the Mâconnais and Beaujolais before turning east into the Alps. A scenic symphony awaits you as you ascend into the high Alps and climb towards Mont Blanc, Western Europe’s highest mountain. Your destination is the fashionable ski resort of Chamonix. In the evening you may choose to try a local pierrade at a local trendy eatery in a traditional chalet. This Optional Experience provides the chance to try a typical regional meal, cooked on a table barbecue using on a hot stone.(Breakfast)",
                DayNumber = 4,
                HotelId = hotel31.Id,
                LocationId = location33.Id
            };
            TourEvent tourEvent45 = new TourEvent
            {
                Name = "Lakeside Annecy and Grenoble",
                Description = "In the morning, embark on a beautiful journey to the lakeside town of Annecy, dramatically situated between lake and mountains, it's a medieval spectacle and a summer getaway for the French and the Swiss locals. Enjoy free time for lunch in Little Venice, aptly named for the network of canals criss-crossing through the Old Town. Explore the city’s many lovely bridges at your leisure. This evening, savour a delicious raclette dinner at a local restaurant.(Breakfast / Dinner with Wine)",
                DayNumber = 5,
                HotelId = hotel32.Id,
                LocationId = location34.Id
            };
            db.TourEvents.Add(tourEvent41);
            db.TourEvents.Add(tourEvent42);
            db.TourEvents.Add(tourEvent43);
            db.TourEvents.Add(tourEvent44);
            db.TourEvents.Add(tourEvent45);
            db.SaveChanges();

            Tour tour8 = new Tour
            {
                Name = "Country Roads of France",
                Country = country4,
                Rating = 5,
                DaysCount = 5,
                Price = 3350,
                Photo = "https://www.getours.com/media/1207/france-provence-lavender-field.jpg?center=0.5043478260869565,0.48753462603878117&mode=crop&width=640&height=386&rnd=132291906710000000",
                TourEvents = new List<TourEvent> { tourEvent41, tourEvent42, tourEvent43, tourEvent44, tourEvent45 }
            };
            db.Tours.Add(tour8);
            db.SaveChanges();
            #endregion

            #region Tour #9 (4*)
            Hotel hotel33 = new Hotel
            {
                Name = "Crowne Plaza République",
                Rating = 4,
                Meals = new List<Meal> { meal1, meal2 }
            };
            db.Hotels.Add(hotel33);

            Location location35 = new Location { Name = "Paris" };
            Location location36 = new Location { Name = "Versailles" };
            db.Locations.Add(location35);
            db.Locations.Add(location36);
            db.SaveChanges();

            TourEvent tourEvent46 = new TourEvent
            {
                Name = "Welcome to Paris",
                Description = "Paris is yours to explore on this in-depth City Explorer. Check in to your centrally located hotel and spend the afternoon getting to know your surrounds. Wander through the leafy boulevards and winding lanes of the French capital, stopping for a leisurely café au lait while watching sophisticated Parisians go by. This evening, join your fellow travellers and Travel Director for a quintessentially Parisian Welcome Reception.",
                DayNumber = 1,
                HotelId = hotel33.Id,
                LocationId = location35.Id
            };
            TourEvent tourEvent47 = new TourEvent
            {
                Name = "Paris and the Eiffel Tower",
                Description = "Join your Travel Director this morning for a tour of this mesmerising city. View the grand Champs Élysées, Place de la Concorde and Arc de Triomphe, built by Napoleon in the 19th century. Delve into its hidden squares and secret passages, visiting one of Paris's most historic neighbourhoods and a favourite local haunt that remains a secret to outsiders. Next you'll Dive Into Culture at Fragonard Parfumerie for a visit with a certified 'nose' who will guide you in the creation of your own unique fragrance. Later, you'll experience a true Parisian highlight. Ascend to the second level of the Eiffel Tower for incredible views of the city then enjoy an afternoon tea at the Tower's restaurant.",
                DayNumber = 2,
                HotelId = hotel33.Id,
                LocationId = location35.Id
            };
            TourEvent tourEvent48 = new TourEvent
            {
                Name = "Consider an Optional D-Day Experience",
                Description = "Spend the day soaking up the sights and sounds of this elegant European capital. Your Travel Director will suggest activities and attractions that will ensure a memorable day spent discovering Paris. You may wish to experience a full-day Optional Experience on the Normandy coast to uncover the poignant history of the D-Day Landings. Spend your evening at leisure exploring the city's glittering sights at night.",
                DayNumber = 3,
                HotelId = hotel33.Id,
                LocationId = location35.Id
            };
            TourEvent tourEvent49 = new TourEvent
            {
                Name = "A Day to Explore Versailles",
                Description = "Tread in the footsteps of French nobility and explore the opulence and splendour of Versailles. Join a Local Specialist who will guide you through the palace's extraordinary Hall of Mirrors and Battles Gallery. Stroll through the picturesque Royal Gardens and visit the Petit Trianon to see first-hand how the French Royal Family lived in private, before discovering Marie Antoinette's Hamlet. Return to Paris later for an evening at leisure.",
                DayNumber = 4,
                HotelId = hotel33.Id,
                LocationId = location36.Id
            };
            TourEvent tourEvent50 = new TourEvent
            {
                Name = "Experience the Marais and French Cuisine",
                Description = "Explore the intriguing and beautiful Marais district on a walking tour. See the mansions and courtyards of this delightful corner of Paris. Then, Dive Into Culture, savouring the exquisite flavours of French cuisine as you meet a Parisian chef for an unforgettable lunch experience. Delve into local culinary secrets, and delicious flavours and aromas, which you will have an opportunity to create yourself and enjoy afterwards. Spend the rest of the day at leisure. This evening, perhaps connect with the pageantry of a traditional cabaret performance at the world-famous Moulin Rouge.",
                DayNumber = 5,
                HotelId = hotel33.Id,
                LocationId = location36.Id
            };
            db.TourEvents.Add(tourEvent46);
            db.TourEvents.Add(tourEvent47);
            db.TourEvents.Add(tourEvent48);
            db.TourEvents.Add(tourEvent49);
            db.TourEvents.Add(tourEvent50);
            db.SaveChanges();

            Tour tour9 = new Tour
            {
                Name = "Paris Explorer",
                Country = country4,
                Rating = 4,
                DaysCount = 5,
                Price = 1350,
                Photo = "https://www.getours.com/media/3871/france-paris-louve-triangle.jpg?anchor=center&mode=crop&width=640&height=386&rnd=132291899520000000",
                TourEvents = new List<TourEvent> { tourEvent46, tourEvent47, tourEvent48, tourEvent49, tourEvent50 }
            };
            db.Tours.Add(tour9);
            db.SaveChanges();
            #endregion

            #region Tour #10 (4*)
            Hotel hotel34 = new Hotel
            {
                Name = "Barceló Torre de Madrid/Melia Serrano Madrid.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel35 = new Hotel
            {
                Name = "NH Collection Santiago de Compostela",
                Rating = 4,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel36 = new Hotel
            {
                Name = "Barceló Oviedo Cervantes.",
                Rating = 4,
                Meals = new List<Meal> { meal1 }
            };
            db.Hotels.Add(hotel34);
            db.Hotels.Add(hotel35);
            db.Hotels.Add(hotel36);

            Location location37 = new Location { Name = "Madrid" };
            Location location38 = new Location { Name = "Santiago de Compostela" };
            Location location39 = new Location { Name = "Oviedo" };
            db.Locations.Add(location37);
            db.Locations.Add(location38);
            db.Locations.Add(location39);
            db.SaveChanges();

            TourEvent tourEvent51 = new TourEvent
            {
                Name = "Welcome to Madrid",
                Description = "On arrival at Madrid airport, your airport transfer leaves for your hotel at 09:00, 11:30, 13:30 or 16:00. Relax and settle in before meeting your Travel Director at 17:30. Your Welcome Dinner is at a local restaurant and provides the opportunity to mingle with your fellow guests over a pleasant evening of good food and wine.(Dinner with Wine)",
                DayNumber = 1,
                HotelId = hotel34.Id,
                LocationId = location37.Id
            };
            TourEvent tourEvent52 = new TourEvent
            {
                Name = "Madrid of the Bourbons",
                Description = "Enjoy sightseeing with your Local Expert as they take you along the main historical areas in the capital of Spain, known as Madrid of the Bourbons. Your drive continues through the Old Quarter, discovering the origins of the city.A highlight of your escorted journey will be to visit the Prado Museum. Discover countless works by Velázquez, Goya, Titian, Rubens and others as you are guided by a local art historian. The afternoon is free for you to relax at a café or shop in the boutiques.(Breakfast)",
                DayNumber = 2,
                HotelId = hotel34.Id,
                LocationId = location37.Id
            };
            TourEvent tourEvent53 = new TourEvent
            {
                Name = "Through the Guadarrama Mountains to Santiago",
                Description = "Journey through the Guadarrama Mountains to the infinite horizons of Old Castile and pass the impressive Castillo de la Mota. Some of Spain's most fashionable white wines are produced in Rueda. This morning we have the opportunity to learn how they are made and sample the delights of the Verdejo grape. Continue past historic Tordesillas, and on to Santiago de Compostela.(Breakfast / Dinner with Wine)",
                DayNumber = 3,
                HotelId = hotel35.Id,
                LocationId = location38.Id
            };
            TourEvent tourEvent54 = new TourEvent
            {
                Name = "Saintly Santiago de Compostela",
                Description = "Explore Santiago de Compostela with Insight Choice. Choose between joining a city tour with a Local Expert and visit the cathedral, festooned with architectural frills or alternatively, embark on a scenic walk on part of the Way of St James, through parks and gardens.Relax during your free time, or explore the narrow streets and old squares of this city.(Breakfast)",
                DayNumber = 4,
                HotelId = hotel35.Id,
                LocationId = location38.Id
            };
            TourEvent tourEvent55 = new TourEvent
            {
                Name = "Along the Costa Verde to Oviedo",
                Description = "Pass through the ancient forests of Galicia and cross to the Green Coast of Asturias - one of Spain's most beautiful, named after the colour of the sea and the pine and eucalyptus trees that line the shore. Meet a true local, Flor, who will invite you to join her for lunch. Hear about daily life as you enjoy a delicious meal, typical of the region, that Flor has freshly prepared from locally sourced ingredients, accompanied by delicious local cider. Your route reveals glorious coastal vistas before turning inland to travel through old villages and deep valleys en route to Oviedo. Continue on to Oviedo where a Local Expert will show you the 9th century churches built when the city was the capital of Christian Spain. Visit the Cathedral of the Holy Saviour in the stately square of Plaza Alfonso II. Admire the incredible altarpiece, one of the finest in Europe.(Breakfast / Lunch with Wine)",
                DayNumber = 5,
                HotelId = hotel36.Id,
                LocationId = location39.Id
            };
            db.TourEvents.Add(tourEvent51);
            db.TourEvents.Add(tourEvent52);
            db.TourEvents.Add(tourEvent53);
            db.TourEvents.Add(tourEvent54);
            db.TourEvents.Add(tourEvent55);
            db.SaveChanges();

            Tour tour10 = new Tour
            {
                Name = "Northern Spain (Premium)",
                Country = country5,
                Rating = 4,
                DaysCount = 5,
                Price = 1150,
                Photo = "https://www.getours.com/media/3789/spain-pamplona-streetview.jpg?anchor=center&mode=crop&width=640&height=386&rnd=132295300200000000",
                TourEvents = new List<TourEvent> { tourEvent51, tourEvent52, tourEvent53, tourEvent54, tourEvent55 }
            };
            db.Tours.Add(tour10);
            db.SaveChanges();
            #endregion

            #region Tour #11 (4*)
            Hotel hotel37 = new Hotel
            {
                Name = "NH Collection Constanza / Hilton Barcelona.",
                Rating = 5,
                Meals = new List<Meal> { meal1, meal2, meal3 }
            };
            Hotel hotel38 = new Hotel
            {
                Name = "Barceló Valencia.",
                Rating = 4,
                Meals = new List<Meal> { meal1 }
            };
            Hotel hotel39 = new Hotel
            {
                Name = "Meliá Granada.",
                Rating = 4,
                Meals = new List<Meal> { meal1, meal2 }
            };
            Hotel hotel40 = new Hotel
            {
                Name = "Meliá Sevilla, Seville.",
                Rating = 4,
                Meals = new List<Meal> { meal1 }
            };
            db.Hotels.Add(hotel37);
            db.Hotels.Add(hotel38);
            db.Hotels.Add(hotel39);
            db.Hotels.Add(hotel40);

            Location location40 = new Location { Name = "Barcelona" };
            Location location41 = new Location { Name = "Valencia" };
            Location location42 = new Location { Name = "Granada" };
            Location location43 = new Location { Name = "Seville" };
            db.Locations.Add(location40);
            db.Locations.Add(location41);
            db.Locations.Add(location42);
            db.Locations.Add(location43);
            db.SaveChanges();

            TourEvent tourEvent56 = new TourEvent
            {
                Name = "Welcome to Barcelona",
                Description = "Your complimentary, private transfer takes you to your local departure airport. Airport transfers depart from Barcelona airport for your hotel at 09:30, 12:30 and 15:00. Join your Travel Director and fellow travellers at 18:00 for your Welcome Dinner in a local restaurant.(Dinner with Wine)",
                DayNumber = 1,
                HotelId = hotel37.Id,
                LocationId = location40.Id
            };
            TourEvent tourEvent57 = new TourEvent
            {
                Name = "Barcelona, the City of Gaudí",
                Description = "With your Local Expert, explore the narrow lanes and old squares behind the cathedral in the Barri Gòtic, Gothic Quarter. See the lively Las Ramblas and then head along the Passeig de Gràcia, adorned with elegant wrought-iron street lamps and some of the most flamboyant Modernist buildings. Continue through the Eixample district for a visit inside Gaudí’s extraordinary masterpiece, the Sagrada Família, to see the central nave with its giant, tree-like pillars, spectacular vaulting and the beautiful rainbows flooding in through the intricate stained-glass windows. Enjoy the after at leisure to explore or perhaps join an optional experience to Montserrat & The Black Madonna.(Breakfast)",
                DayNumber = 2,
                HotelId = hotel37.Id,
                LocationId = location40.Id
            };
            TourEvent tourEvent58 = new TourEvent
            {
                Name = "Along the coast to Valencia",
                Description = "Follow the sunny Costa Dorada, past the rice fields of the Ebro Delta to stop at the seaside resort of Peñíscola and see the hilltop castle, used in the film 'El Cid'. Pass through the famous orange groves to Valencia, see the City of Arts and Sciences. Pass the Old City and see its medieval gates to reach your hotel.Valencia is home to one of the world's greatest dishes, paella. Meet the chef of a local family-run restaurant in the heart of the Old City and learn the traditional recipe during a Cooking Demonstration, before savouring the delicious flavours of this classic dish, washed down with a glass of sangria, during your Highlight Dinner.(Breakfast / Dinner with Wine)",
                DayNumber = 3,
                HotelId = hotel38.Id,
                LocationId = location41.Id
            };
            TourEvent tourEvent59 = new TourEvent
            {
                Name = "Exquisite Granada and Alhambra",
                Description = "Travel south toward Andalucía, passing the castle at Lorca and troglodyte dwellings hollowed out of the soft rock near Guadix. Enjoy views of the Sierra Nevada from a distance as you reach glorious Granada. This historic city was a Moorish Kingdom for almost 8 centuries and was the last stronghold to fall to the Catholic Monarchs of Spain in 1492. Overlooking Granada stands one of the most remarkable fortresses ever built, the Alhambra. Led by your Local Expert, explore this exquisite palace, built as a citadel by the Moors in the 13th century - a fantasy of arabesque gardens, fountains and stone cut like lace. Stroll through the elegant exotic gardens of the Generalife, the Royal summer residence.(Breakfast / Dinner with Wine)",
                DayNumber = 4,
                HotelId = hotel39.Id,
                LocationId = location42.Id
            };
            TourEvent tourEvent60 = new TourEvent
            {
                Name = "A Scenic Drive to Seville",
                Description = "The morning is at your leisure to explore as you wish. Perhaps join an Optional Experience to the Albaicín Quarter with a Local Expert and fall in love with Granada as you learn about its pivotal role in European history? Continue your travels to Seville, past gleaming white villages and olive groves that stretch as far as the eye can see.(Breakfast)",
                DayNumber = 5,
                HotelId = hotel40.Id,
                LocationId = location43.Id
            };
            db.TourEvents.Add(tourEvent56);
            db.TourEvents.Add(tourEvent57);
            db.TourEvents.Add(tourEvent58);
            db.TourEvents.Add(tourEvent59);
            db.TourEvents.Add(tourEvent60);
            db.SaveChanges();

            Tour tour11 = new Tour
            {
                Name = "Highlights of Spain",
                Country = country5,
                Rating = 4,
                DaysCount = 5,
                Price = 1150,
                Photo = "https://www.getours.com/media/1315/spain-valencia-museum-of-arts-and-sciences.jpg?anchor=center&mode=crop&width=640&height=386&rnd=132295303810000000",
                TourEvents = new List<TourEvent> { tourEvent56, tourEvent57, tourEvent58, tourEvent59, tourEvent60 }
            };
            db.Tours.Add(tour11);
            db.SaveChanges();
            #endregion
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return db.SaveChangesAsync();
        }
    }
}
