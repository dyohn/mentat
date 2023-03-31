namespace Mentat.Repository.Models
{
    public class CardDatabaseSettings : ICardDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://feature-tags-user:cFVOCEplar0AgjOq@cluster0.5mowjp1.mongodb.net/test";

        public string DatabaseName { get; set; } = "fcApp";

        public string CardCollectionName { get; set; } = "FlashCards";
    }
}
