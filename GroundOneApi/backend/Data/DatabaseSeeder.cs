using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class DatabaseSeeder
    {
        // Metoda dla DbContext (runtime seeding)
        public static void SeedData(AppDbContext context)
        {
            // Sprawdź czy dane już istnieją
            if (context.Vehicles.Any())
            {
                return; // Baza już zawiera dane
            }

            var (vehicles, compartments, items) = GetSeedData();
            
            context.Vehicles.AddRange(vehicles);
            context.Compartments.AddRange(compartments);
            context.Items.AddRange(items);
            
            context.SaveChanges();
        }

        // Metoda dla ModelBuilder (w OnModelCreating)
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var (vehicles, compartments, items) = GetSeedData();
            
            modelBuilder.Entity<Vehicle>().HasData(vehicles);
            modelBuilder.Entity<Compartment>().HasData(compartments);
            modelBuilder.Entity<Item>().HasData(items);
        }

        // Wspólna metoda generująca dane
        private static (Vehicle[] vehicles, Compartment[] compartments, List<Item> items) GetSeedData()
        {
            // ===== VEHICLES =====
            var vehicles = new[]
            {
                new Vehicle
                {
                    Id = 1,
                    Name = "GBA 2/16-1",
                    Type = VehicleType.GBA.ToString(),
                    Cryptonym = "451-25",
                    RegistrationNumber = "EOP 5241",
                    YearOfManufacture = 2019,
                    LastInspection = new DateTime(2024, 8, 15),
                    NextInspection = new DateTime(2025, 8, 15),
                    Status = VehicleStatus.Available,
                    Notes = "Podstawowy samochód gaśniczy - pełna sprawność operacyjna"
                },
                new Vehicle
                {
                    Id = 2,
                    Name = "SLRt-1",
                    Type = VehicleType.SRt.ToString(),
                    Cryptonym = "452-41",
                    RegistrationNumber = "EOP 7892",
                    YearOfManufacture = 2021,
                    LastInspection = new DateTime(2024, 9, 10),
                    NextInspection = new DateTime(2025, 9, 10),
                    Status = VehicleStatus.Available,
                    Notes = "Samochód ratownictwa technicznego - kompletne wyposażenie"
                },
                new Vehicle
                {
                    Id = 3,
                    Name = "SD-30",
                    Type = VehicleType.SD.ToString(),
                    Cryptonym = "451-51",
                    RegistrationNumber = "EOP 3456",
                    YearOfManufacture = 2017,
                    LastInspection = new DateTime(2024, 7, 20),
                    NextInspection = new DateTime(2025, 7, 20),
                    Status = VehicleStatus.Available,
                    Notes = "Drabina mechaniczna 30m - po ostatnim przeglądzie UDT"
                },
                new Vehicle
                {
                    Id = 4,
                    Name = "GCBA 4/32",
                    Type = VehicleType.GCBA.ToString(),
                    Cryptonym = "451-25",
                    RegistrationNumber = "EOP 1123",
                    YearOfManufacture = 2020,
                    LastInspection = new DateTime(2024, 6, 5),
                    NextInspection = new DateTime(2025, 6, 5),
                    Status = VehicleStatus.UnderMaintenance,
                    Notes = "W trakcie przeglądu głównego pompy"
                },
                new Vehicle
                {
                    Id = 5,
                    Name = "SLRR-1",
                    Type = VehicleType.SLRR.ToString(),
                    Cryptonym = "470-41",
                    RegistrationNumber = "EOP 9876",
                    YearOfManufacture = 2022,
                    LastInspection = new DateTime(2024, 10, 1),
                    NextInspection = new DateTime(2025, 10, 1),
                    Status = VehicleStatus.Available,
                    Notes = "Najnowszy pojazd w jednostce"
                }
            };

            // ===== COMPARTMENTS =====
            var compartments = new[]
            {
                // GBA 2/16-1 (Vehicle Id = 1)
                new Compartment { Id = 1, Name = "Skrytka przednia lewa", Description = "Główny sprzęt gaśniczy", Location = CompartmentLocation.FrontLeft, VehicleId = 1 },
                new Compartment { Id = 2, Name = "Skrytka przednia prawa", Description = "Sprzęt ratowniczy podstawowy", Location = CompartmentLocation.FrontRight, VehicleId = 1 },
                new Compartment { Id = 3, Name = "Skrytka środkowa lewa", Description = "Węże i osprzęt wodny", Location = CompartmentLocation.MiddleLeft, VehicleId = 1 },
                new Compartment { Id = 4, Name = "Skrytka środkowa prawa", Description = "Narzędzia i osprzęt", Location = CompartmentLocation.MiddleRight, VehicleId = 1 },
                new Compartment { Id = 5, Name = "Skrytka tylna lewa", Description = "Drabiny i sprzęt pomocniczy", Location = CompartmentLocation.RearLeft, VehicleId = 1 },
                new Compartment { Id = 6, Name = "Skrytka tylna prawa", Description = "Sprzęt ochrony osobistej", Location = CompartmentLocation.RearRight, VehicleId = 1 },
                new Compartment { Id = 7, Name = "Kabina załogi", Description = "Środki łączności i dokumenty", Location = CompartmentLocation.Cabin, VehicleId = 1 },

                // SLRt-1 (Vehicle Id = 2)
                new Compartment { Id = 8, Name = "Skrytka przednia lewa", Description = "Narzędzia hydrauliczne", Location = CompartmentLocation.FrontLeft, VehicleId = 2 },
                new Compartment { Id = 9, Name = "Skrytka przednia prawa", Description = "Sprzęt ratownictwa drogowego", Location = CompartmentLocation.FrontRight, VehicleId = 2 },
                new Compartment { Id = 10, Name = "Skrytka środkowa lewa", Description = "Agregaty i oświetlenie", Location = CompartmentLocation.MiddleLeft, VehicleId = 2 },
                new Compartment { Id = 11, Name = "Skrytka środkowa prawa", Description = "Sprzęt ratownictwa technicznego", Location = CompartmentLocation.MiddleRight, VehicleId = 2 },
                new Compartment { Id = 12, Name = "Skrytka tylna lewa", Description = "Podnośniki i rozpieraki", Location = CompartmentLocation.RearLeft, VehicleId = 2 },
                new Compartment { Id = 13, Name = "Skrytka tylna prawa", Description = "Liny i sprzęt wspinaczkowy", Location = CompartmentLocation.RearRight, VehicleId = 2 },
                new Compartment { Id = 14, Name = "Dach pojazdu", Description = "Drabiny i żuraw", Location = CompartmentLocation.Roof, VehicleId = 2 },

                // SD-30 (Vehicle Id = 3)
                new Compartment { Id = 15, Name = "Skrytka przednia lewa", Description = "Sprzęt gaśniczy podstawowy", Location = CompartmentLocation.FrontLeft, VehicleId = 3 },
                new Compartment { Id = 16, Name = "Skrytka przednia prawa", Description = "Narzędzia ratownicze", Location = CompartmentLocation.FrontRight, VehicleId = 3 },
                new Compartment { Id = 17, Name = "Kabina operatora", Description = "Panel sterowania drabiną", Location = CompartmentLocation.Cabin, VehicleId = 3 },

                // GCBA 4/32 (Vehicle Id = 4)
                new Compartment { Id = 18, Name = "Skrytka przednia lewa", Description = "Sprzęt gaśniczy ciężki", Location = CompartmentLocation.FrontLeft, VehicleId = 4 },
                new Compartment { Id = 19, Name = "Skrytka przednia prawa", Description = "Węże magistralne", Location = CompartmentLocation.FrontRight, VehicleId = 4 },
                new Compartment { Id = 20, Name = "Skrytka środkowa lewa", Description = "Prądownice i rozdzielacze", Location = CompartmentLocation.MiddleLeft, VehicleId = 4 },
                new Compartment { Id = 21, Name = "Skrytka środkowa prawa", Description = "Sprzęt pianowy", Location = CompartmentLocation.MiddleRight, VehicleId = 4 },
                new Compartment { Id = 22, Name = "Skrytka tylna", Description = "Osprzęt pompy głównej", Location = CompartmentLocation.RearLeft, VehicleId = 4 },

                // SLRR-1 (Vehicle Id = 5)
                new Compartment { Id = 23, Name = "Skrytka przednia", Description = "Podstawowy sprzęt ratowniczy", Location = CompartmentLocation.FrontLeft, VehicleId = 5 },
                new Compartment { Id = 24, Name = "Skrytka tylna", Description = "Sprzęt medyczny i ewakuacyjny", Location = CompartmentLocation.RearLeft, VehicleId = 5 },
                new Compartment { Id = 25, Name = "Kabina", Description = "Łączność i pierwsza pomoc", Location = CompartmentLocation.Cabin, VehicleId = 5 }
            };

            // ===== ITEMS =====
            var items = new List<Item>();
            
            // ===== SPRZĘT DLA GBA 2/16-1 =====
            // Skrytka przednia lewa (Id = 1)
            items.AddRange(new[]
            {
                new Item { Id = 1, Name = "Prądownica uniwersalna WU-25", Manufacturer = "Tyco", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 2, LastInspection = new DateTime(2024, 8, 1), NextInspection = new DateTime(2025, 2, 1), Status = ItemStatus.Available, CompartmentId = 1 },
                new Item { Id = 2, Name = "Prądownica pianowa", Manufacturer = "Akron Brass", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 7, 15), NextInspection = new DateTime(2025, 1, 15), Status = ItemStatus.Available, CompartmentId = 1 },
                new Item { Id = 3, Name = "Rozdzielacz 4-drożny", Manufacturer = "Drager", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 2, LastInspection = new DateTime(2024, 8, 1), NextInspection = new DateTime(2025, 2, 1), Status = ItemStatus.Available, CompartmentId = 1 },
                new Item { Id = 4, Name = "Klucz do hydrantów", Manufacturer = "Local", YearOfManufacture = 2018, Category = EquipmentCategory.Tool, Quantity = 3, Status = ItemStatus.Available, CompartmentId = 1 }
            });

            // Skrytka przednia prawa (Id = 2)
            items.AddRange(new[]
            {
                new Item { Id = 5, Name = "Topór strażacki", Manufacturer = "Prymos", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 2, Status = ItemStatus.Available, CompartmentId = 2 },
                new Item { Id = 6, Name = "Łom strażacki", Manufacturer = "Prymos", YearOfManufacture = 2018, Category = EquipmentCategory.Tool, Quantity = 2, Status = ItemStatus.Available, CompartmentId = 2 },
                new Item { Id = 7, Name = "Szufla", Manufacturer = "Fiskars", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 1, Status = ItemStatus.Available, CompartmentId = 2 },
                new Item { Id = 8, Name = "Piła łańcuchowa", Manufacturer = "Stihl", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 9, 1), NextInspection = new DateTime(2024, 12, 1), Status = ItemStatus.Available, Notes = "Wymaga serwisu co 3 miesiące", CompartmentId = 2 }
            });

            // Skrytka środkowa lewa (Id = 3) - Węże
            items.AddRange(new[]
            {
                new Item { Id = 9, Name = "Wąż ssawny Ø110 - 2,5m", Manufacturer = "Texport", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 4, LastInspection = new DateTime(2024, 6, 1), NextInspection = new DateTime(2024, 12, 1), Status = ItemStatus.Available, CompartmentId = 3 },
                new Item { Id = 10, Name = "Wąż tłoczny Ø75 - 20m", Manufacturer = "Texport", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 6, LastInspection = new DateTime(2024, 5, 15), NextInspection = new DateTime(2024, 11, 15), Status = ItemStatus.Available, CompartmentId = 3 },
                new Item { Id = 11, Name = "Wąż tłoczny Ø52 - 15m", Manufacturer = "Texport", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 4, LastInspection = new DateTime(2024, 5, 15), NextInspection = new DateTime(2024, 11, 15), Status = ItemStatus.Available, CompartmentId = 3 },
                new Item { Id = 12, Name = "Kosz ssawny", Manufacturer = "Local", YearOfManufacture = 2018, Category = EquipmentCategory.Tool, Quantity = 1, Status = ItemStatus.Available, CompartmentId = 3 }
            });

            // Skrytka środkowa prawa (Id = 4)
            items.AddRange(new[]
            {
                new Item { Id = 13, Name = "Agregat prądotwórczy 2kW", Manufacturer = "Honda", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 8, 10), NextInspection = new DateTime(2025, 2, 10), Status = ItemStatus.Available, CompartmentId = 4 },
                new Item { Id = 14, Name = "Reflektor LED 50W", Manufacturer = "Osram", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 4, Status = ItemStatus.Available, CompartmentId = 4 },
                new Item { Id = 15, Name = "Przedłużacz 25m", Manufacturer = "Generic", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 2, Status = ItemStatus.Available, CompartmentId = 4 },
                new Item { Id = 16, Name = "Motopompa pływająca", Manufacturer = "Honda", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 7, 1), NextInspection = new DateTime(2025, 1, 1), Status = ItemStatus.Available, CompartmentId = 4 }
            });

            // Skrytka tylna lewa (Id = 5) - Drabiny
            items.AddRange(new[]
            {
                new Item { Id = 17, Name = "Drabina 3-elementowa 8m", Manufacturer = "Krause", YearOfManufacture = 2018, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 4, 1), NextInspection = new DateTime(2024, 10, 1), Status = ItemStatus.Available, CompartmentId = 5 },
                new Item { Id = 18, Name = "Drabina hakowa 4m", Manufacturer = "Krause", YearOfManufacture = 2019, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 4, 1), NextInspection = new DateTime(2024, 10, 1), Status = ItemStatus.Available, CompartmentId = 5 },
                new Item { Id = 19, Name = "Lina ratownicza 30m", Manufacturer = "Edelrid", YearOfManufacture = 2020, Category = EquipmentCategory.Safety, Quantity = 2, LastInspection = new DateTime(2024, 6, 1), NextInspection = new DateTime(2024, 12, 1), Status = ItemStatus.Available, CompartmentId = 5 },
                new Item { Id = 20, Name = "Pasy ratownicze", Manufacturer = "Petzl", YearOfManufacture = 2021, Category = EquipmentCategory.Safety, Quantity = 4, LastInspection = new DateTime(2024, 3, 1), NextInspection = new DateTime(2024, 9, 1), Status = ItemStatus.Available, CompartmentId = 5 }
            });

            // Skrytka tylna prawa (Id = 6) - Ochrona osobista
            items.AddRange(new[]
            {
                new Item { Id = 21, Name = "Hełm strażacki", Manufacturer = "Rosenbauer", YearOfManufacture = 2020, Category = EquipmentCategory.Safety, Quantity = 6, Status = ItemStatus.Available, CompartmentId = 6 },
                new Item { Id = 22, Name = "Ubranie specjalne", Manufacturer = "Fire-Dex", YearOfManufacture = 2021, Category = EquipmentCategory.Safety, Quantity = 6, Status = ItemStatus.Available, CompartmentId = 6 },
                new Item { Id = 23, Name = "Buty strażackie", Manufacturer = "Haix", YearOfManufacture = 2020, Category = EquipmentCategory.Safety, Quantity = 6, Status = ItemStatus.Available, CompartmentId = 6 },
                new Item { Id = 24, Name = "Rękawice techniczne", Manufacturer = "Seiz", YearOfManufacture = 2021, Category = EquipmentCategory.Safety, Quantity = 8, Status = ItemStatus.Available, CompartmentId = 6 }
            });

            // Kabina (Id = 7)
            items.AddRange(new[]
            {
                new Item { Id = 25, Name = "Radiotelefon Motorola", Manufacturer = "Motorola", YearOfManufacture = 2020, Category = EquipmentCategory.Communication, Quantity = 4, Status = ItemStatus.Available, CompartmentId = 7 },
                new Item { Id = 26, Name = "Apteczka pierwszej pomocy", Manufacturer = "DIN 13164", YearOfManufacture = 2023, Category = EquipmentCategory.Medical, Quantity = 1, LastInspection = new DateTime(2024, 9, 1), NextInspection = new DateTime(2025, 3, 1), Status = ItemStatus.Available, CompartmentId = 7 }
            });

            // ===== SPRZĘT DLA SLRt-1 =====
            // Skrytka przednia lewa (Id = 8) - Narzędzia hydrauliczne
            items.AddRange(new[]
            {
                new Item { Id = 27, Name = "Nożyce hydrauliczne", Manufacturer = "Holmatro", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 8, 1), NextInspection = new DateTime(2025, 2, 1), Status = ItemStatus.Available, Notes = "Sprawdzić poziom oleju co miesiąc", CompartmentId = 8 },
                new Item { Id = 28, Name = "Rozpieraki hydrauliczne", Manufacturer = "Holmatro", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 2, LastInspection = new DateTime(2024, 8, 1), NextInspection = new DateTime(2025, 2, 1), Status = ItemStatus.Available, CompartmentId = 8 },
                new Item { Id = 29, Name = "Pompa hydrauliczna", Manufacturer = "Holmatro", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 8, 1), NextInspection = new DateTime(2025, 2, 1), Status = ItemStatus.Available, CompartmentId = 8 },
                new Item { Id = 30, Name = "Przewody hydrauliczne", Manufacturer = "Holmatro", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 8, Status = ItemStatus.Available, CompartmentId = 8 }
            });

            // Dodaj przykłady w pozostałych skrytkach
            items.AddRange(new[]
            {
                new Item { Id = 31, Name = "Zestaw poduszek pneumatycznych", Manufacturer = "Vetter", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 7, 1), NextInspection = new DateTime(2025, 1, 1), Status = ItemStatus.Available, CompartmentId = 9 },
                new Item { Id = 32, Name = "Agregat prądotwórczy 5kW", Manufacturer = "Honda", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 9, 1), NextInspection = new DateTime(2025, 3, 1), Status = ItemStatus.Available, CompartmentId = 10 },
                new Item { Id = 33, Name = "Piła tarczowa do metalu", Manufacturer = "Stihl", YearOfManufacture = 2021, Category = EquipmentCategory.Tool, Quantity = 1, LastInspection = new DateTime(2024, 8, 15), NextInspection = new DateTime(2024, 11, 15), Status = ItemStatus.Available, CompartmentId = 11 }
            });

            // ===== SPRZĘT DLA SD-30 =====
            items.AddRange(new[]
            {
                new Item { Id = 34, Name = "Kosz ratowniczy", Manufacturer = "Rosenbauer", YearOfManufacture = 2017, Category = EquipmentCategory.Safety, Quantity = 1, LastInspection = new DateTime(2024, 7, 20), NextInspection = new DateTime(2025, 7, 20), Status = ItemStatus.Available, CompartmentId = 15 },
                new Item { Id = 35, Name = "Monitor wody", Manufacturer = "Akron Brass", YearOfManufacture = 2017, Category = EquipmentCategory.Tool, Quantity = 1, Status = ItemStatus.Available, CompartmentId = 15 },
                new Item { Id = 36, Name = "Prądownica koszowa", Manufacturer = "Tyco", YearOfManufacture = 2018, Category = EquipmentCategory.Tool, Quantity = 2, Status = ItemStatus.Available, CompartmentId = 16 }
            });

            // ===== SPRZĘT W RÓŻNYCH STATUSACH =====
            items.AddRange(new[]
            {
                new Item { Id = 37, Name = "Pompa główna", Manufacturer = "Rosenbauer", YearOfManufacture = 2020, Category = EquipmentCategory.Tool, Quantity = 1, Status = ItemStatus.UnderMaintenance, Notes = "Przegląd główny - wymiana uszczelek", CompartmentId = 22 },
                new Item { Id = 38, Name = "Aparat oddechowy", Manufacturer = "Drager", YearOfManufacture = 2019, Category = EquipmentCategory.Safety, Quantity = 1, Status = ItemStatus.Damaged, Notes = "Uszkodzony zawór - do wymiany", CompartmentId = 6 },
                new Item { Id = 39, Name = "Radiotelefon przenośny", Manufacturer = "Motorola", YearOfManufacture = 2018, Category = EquipmentCategory.Communication, Quantity = 1, Status = ItemStatus.InUse, Notes = "W użyciu podczas akcji", CompartmentId = 7 }
            });

            return (vehicles, compartments, items);
        }
    }
}