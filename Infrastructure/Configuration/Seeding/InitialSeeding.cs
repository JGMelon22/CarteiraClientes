namespace CarteiraClientes.Infrastructure.Configuration.Seeding;

public class InitialSeeding
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Clients
        var client1 = new Client
        {
            ClientId = 1,
            FullName = "Allissa Victória da Silva",
            Age = 18,
            Gender = Gender.Female,
            Document = "123456789",
            IsOverdue = false
        };

        var client2 = new Client
        {
            ClientId = 2,
            FullName = "João Oliveira",
            Age = 25,
            Gender = Gender.Male,
            Document = "987654321",
            IsOverdue = true
        };

        var client3 = new Client
        {
            ClientId = 3,
            FullName = "Maria Santos",
            Age = 30,
            Gender = Gender.Female,
            Document = "456789123",
            IsOverdue = false
        };
        var client4 = new Client
        {
            ClientId = 4,
            FullName = "Carlos Pereira",
            Age = 22,
            Gender = Gender.Male,
            Document = "234567890",
            IsOverdue = true
        };

        var client5 = new Client
        {
            ClientId = 5,
            FullName = "Ana Oliveira",
            Age = 28,
            Gender = Gender.Female,
            Document = "345678901",
            IsOverdue = false
        };

        var client6 = new Client
        {
            ClientId = 6,
            FullName = "Pedro Silva",
            Age = 35,
            Gender = Gender.Male,
            Document = "456789012",
            IsOverdue = true
        };

        var client7 = new Client
        {
            ClientId = 7,
            FullName = "Camila Santos",
            Age = 40,
            Gender = Gender.Female,
            Document = "567890123",
            IsOverdue = false
        };

        var client8 = new Client
        {
            ClientId = 8,
            FullName = "Felipe Oliveira",
            Age = 32,
            Gender = Gender.Male,
            Document = "678901234",
            IsOverdue = true
        };

        var client9 = new Client
        {
            ClientId = 9,
            FullName = "Juliana Silva",
            Age = 27,
            Gender = Gender.Female,
            Document = "789012345",
            IsOverdue = false
        };

        var client10 = new Client
        {
            ClientId = 10,
            FullName = "Lucas Pereira",
            Age = 24,
            Gender = Gender.Male,
            Document = "890123456",
            IsOverdue = true
        };

        // Company
        var company1 = new Company
        {
            CompanyId = 1,
            CompanyName = "Banco do Brasil",
            FoundedDate = new DateTime(1922, 01, 01),
            Revenue = 28_820_000_000.00M
        };

        var company2 = new Company
        {
            CompanyId = 2,
            CompanyName = "Tech Solutions Inc.",
            FoundedDate = new DateTime(2000, 05, 15),
            Revenue = 15500_000_000.00M
        };

        var company3 = new Company
        {
            CompanyId = 3,
            CompanyName = "Global Motors",
            FoundedDate = new DateTime(1985, 10, 30),
            Revenue = 10_200_000_000.00M
        };

        var company4 = new Company
        {
            CompanyId = 4,
            CompanyName = "Food Delights Co.",
            FoundedDate = new DateTime(2010, 03, 12),
            Revenue = 5_750_000_000.00M
        };

        var company5 = new Company
        {
            CompanyId = 5,
            CompanyName = "Fashion Trends Ltd.",
            FoundedDate = new DateTime(1995, 07, 22),
            Revenue = 8_300_000_000.00M
        };

        var company6 = new Company
        {
            CompanyId = 6,
            CompanyName = "Green Energy Solutions",
            FoundedDate = new DateTime(2008, 12, 05),
            Revenue = 3_600_000_000.00M
        };

        // Many-to-many seed
        // Client Company
        var bancoDoBrasil1 = new ClientCompany
        {
            ClientId = client1.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil2 = new ClientCompany
        {
            ClientId = client2.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil3 = new ClientCompany
        {
            ClientId = client3.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil4 = new ClientCompany
        {
            ClientId = client4.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil5 = new ClientCompany
        {
            ClientId = client5.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil6 = new ClientCompany
        {
            ClientId = client6.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil7 = new ClientCompany
        {
            ClientId = client7.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil8 = new ClientCompany
        {
            ClientId = client8.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil9 = new ClientCompany
        {
            ClientId = client9.ClientId,
            CompanyId = company1.CompanyId
        };

        var bancoDoBrasil10 = new ClientCompany
        {
            ClientId = client10.ClientId,
            CompanyId = company1.CompanyId
        };

        // Tech Solutions Inc.
        var techSolutionsInc1 = new ClientCompany
        {
            ClientId = client1.ClientId,
            CompanyId = company2.CompanyId
        };

        var techSolutionsInc2 = new ClientCompany
        {
            ClientId = client2.ClientId,
            CompanyId = company2.CompanyId
        };

        var techSolutionsInc3 = new ClientCompany
        {
            ClientId = client3.ClientId,
            CompanyId = company2.CompanyId
        };

        var techSolutionsInc4 = new ClientCompany
        {
            ClientId = client4.ClientId,
            CompanyId = company2.CompanyId
        };

        var techSolutionsInc5 = new ClientCompany
        {
            ClientId = client5.ClientId,
            CompanyId = company2.CompanyId
        };

        var techSolutionsInc6 = new ClientCompany
        {
            ClientId = client6.ClientId,
            CompanyId = company2.CompanyId
        };
        var techSolutionsInc7 = new ClientCompany
        {
            ClientId = client7.ClientId,
            CompanyId = company2.CompanyId
        };
        var techSolutionsInc8 = new ClientCompany
        {
            ClientId = client8.ClientId,
            CompanyId = company2.CompanyId
        };

        // Global Motors
        var globalMotors1 = new ClientCompany
        {
            ClientId = client1.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors2 = new ClientCompany
        {
            ClientId = client2.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors3 = new ClientCompany
        {
            ClientId = client3.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors4 = new ClientCompany
        {
            ClientId = client4.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors5 = new ClientCompany
        {
            ClientId = client5.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors6 = new ClientCompany
        {
            ClientId = client6.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors7 = new ClientCompany
        {
            ClientId = client7.ClientId,
            CompanyId = company3.CompanyId
        };

        var globalMotors8 = new ClientCompany
        {
            ClientId = client8.ClientId,
            CompanyId = company3.CompanyId
        };

        // Food Delights Co.
        var foodDelights1 = new ClientCompany
        {
            ClientId = client1.ClientId,
            CompanyId = company4.CompanyId
        };

        var foodDelights2 = new ClientCompany
        {
            ClientId = client2.ClientId,
            CompanyId = company4.CompanyId
        };

        var foodDelights3 = new ClientCompany
        {
            ClientId = client3.ClientId,
            CompanyId = company4.CompanyId
        };

        var foodDelights4 = new ClientCompany
        {
            ClientId = client4.ClientId,
            CompanyId = company4.CompanyId
        };

        var foodDelights5 = new ClientCompany
        {
            ClientId = client5.ClientId,
            CompanyId = company4.CompanyId
        };

        var foodDelights6 = new ClientCompany
        {
            ClientId = client6.ClientId,
            CompanyId = company4.CompanyId
        };

        var foodDelights7 = new ClientCompany
        {
            ClientId = client7.ClientId,
            CompanyId = company4.CompanyId
        };

        // Fashion Trends Ltd.
        var fashionTrends1 = new ClientCompany
        {
            ClientId = client10.ClientId,
            CompanyId = company5.CompanyId
        };

        var fashionTrends2 = new ClientCompany
        {
            ClientId = client9.ClientId,
            CompanyId = company5.CompanyId
        };

        var fashionTrends3 = new ClientCompany
        {
            ClientId = client8.ClientId,
            CompanyId = company5.CompanyId
        };

        var fashionTrends4 = new ClientCompany
        {
            ClientId = client7.ClientId,
            CompanyId = company5.CompanyId
        };

        var fashionTrends5 = new ClientCompany
        {
            ClientId = client6.ClientId,
            CompanyId = company5.CompanyId
        };

        var fashionTrends6 = new ClientCompany
        {
            ClientId = client5.ClientId,
            CompanyId = company5.CompanyId
        };

        var fashionTrends7 = new ClientCompany
        {
            ClientId = client4.ClientId,
            CompanyId = company5.CompanyId
        };

        // Green Energy Solutions
        var energySolutions1 = new ClientCompany
        {
            ClientId = client4.ClientId,
            CompanyId = company6.CompanyId
        };

        var energySolutions2 = new ClientCompany
        {
            ClientId = client5.ClientId,
            CompanyId = company6.CompanyId
        };

        var energySolutions3 = new ClientCompany
        {
            ClientId = client6.ClientId,
            CompanyId = company6.CompanyId
        };

        var energySolutions4 = new ClientCompany
        {
            ClientId = client7.ClientId,
            CompanyId = company6.CompanyId
        };

        var energySolutions5 = new ClientCompany
        {
            ClientId = client8.ClientId,
            CompanyId = company6.CompanyId
        };

        // Popular!
        modelBuilder.Entity<Client>().HasData(
            client1, client2, client3, client4, client5,
            client6, client7, client8, client9, client10);

        modelBuilder.Entity<Company>().HasData(
            company1, company2, company3,
            company4, company5, company6
        );

        modelBuilder.Entity<ClientCompany>().HasData(
            bancoDoBrasil1, bancoDoBrasil2, bancoDoBrasil3, bancoDoBrasil4, bancoDoBrasil5,
            bancoDoBrasil6, bancoDoBrasil7, bancoDoBrasil8, bancoDoBrasil9, bancoDoBrasil10,
            techSolutionsInc1, techSolutionsInc2, techSolutionsInc3, techSolutionsInc4,
            techSolutionsInc5, techSolutionsInc6, techSolutionsInc7, techSolutionsInc8,
            globalMotors1, globalMotors2, globalMotors3, globalMotors4,
            globalMotors5, globalMotors6, globalMotors7, globalMotors8,
            foodDelights1, foodDelights2, foodDelights3, foodDelights4,
            foodDelights5, foodDelights6, foodDelights7,
            fashionTrends1, fashionTrends2, fashionTrends3, fashionTrends4,
            fashionTrends5, fashionTrends6, fashionTrends7,
            energySolutions1, energySolutions2, energySolutions3,
            energySolutions4, energySolutions5
        );
    }
}