using System.Xml.Serialization;

namespace MovieApp.Application.DTOs
{

    [XmlType("Actor")]
    public class ActorDto
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("BirthDate")]
        public DateOnly BirthDate { get; set; }

        public ActorDto() { }
    }
}
